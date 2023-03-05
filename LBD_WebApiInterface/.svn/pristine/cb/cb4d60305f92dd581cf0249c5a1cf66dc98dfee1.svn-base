using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LG.IntelligentCourse.WebService.Common.Model;
using System.ServiceModel;
using System.IO;
using LG.IntelligentCourse.WebService.Model;
using LBD_WebApiInterface.Models.TeachCenter;

namespace LBD_WebApiInterface.Other
{
    public class IntelCoursewareI
    {
        private const string _mask = "S10|S10";

        public bool SetZhiNengBeiKeUrl(string strIPandPort)
        {
            try
            {
                if (string.IsNullOrEmpty(strIPandPort))
                {
                    return false;
                }
                string uri = "http://" + strIPandPort + "/CoursewareNoAuthBasic.svc";
                CoursewareNoAuthBasicSvcProxy.Instance.SetAddress(uri);
                return true;
            }
            catch (Exception e)
            {
                WriteErrorMessage("SetZhiNengBeiKeUrl", e.Message);
                return false;
            }
        }

        /// <summary>
        /// 执行接口
        /// </summary>
        private T Invoke<T>(Func<T> func)
        {
            try
            {
                if (func != null)
                    return func();
                return default(T);
            }
            catch (TimeoutException timeProblem)//操作超时  
            {
                Console.WriteLine("服务器操作超时 " + timeProblem.Message);
                return default(T);
            }
            catch (FaultException<BasicFault> fasicFault)
            {
                if (fasicFault.Detail.FaultCode == "500")//服务器内部存储
                    Console.WriteLine(fasicFault.Detail.Message);
                else if (fasicFault.Detail.FaultCode == "1")//mask验证失败
                    Console.WriteLine(fasicFault.Detail.Message);
                else if (fasicFault.Detail.FaultCode == "501")//接口输入参数有错误
                    Console.WriteLine(fasicFault.Detail.Message);
                else
                    Console.WriteLine(fasicFault.Detail.Message);
                return default(T);
            }
            catch (CommunicationException commProblem) //网络通信错误
            {
                Console.WriteLine("网络通信错误： " + commProblem.Message);
                return default(T);
            }
            catch (Exception unknownFault)//未知异常  
            {
                Console.WriteLine(unknownFault.Message);
                return default(T);
            }

        }

        //获取FTP、HTTP
        public IntelCoursewareSysConfig GetSysConfig()
        {
            try
            {
                IDictionary<string, string> sysConfigs = Invoke<IDictionary<string, string>>
                (() =>
                {
                    return CoursewareNoAuthBasicSvcProxy.Instance.GetSysConfigs(_mask);
                });
                //解密ftp服务器配置
                if (sysConfigs == null)
                {
                    return null;
                }
                IntelCoursewareSysConfig sysInfo = new IntelCoursewareSysConfig();

                string resFtp = lancoo.cp.lgmgrcenterdotdll.CP_ClsParamsEncDec.LgMgr_ParamDecrypt(sysConfigs["IntelligentCourse.Res.Ftp.Server"]);
                string[] arr = resFtp.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                //ftp地址
                sysInfo.FtpPath = arr[0];
                //ftp用户名
                sysInfo.FtpUserName = arr[1];
                //ftp密码
                sysInfo.FtpUserPwd = arr[2];
                //解密http服务器配置
                sysInfo.HttpPath = lancoo.cp.lgmgrcenterdotdll.CP_ClsParamsEncDec.LgMgr_ParamDecrypt(sysConfigs["IntelligentCourse.Res.Http.Server"]);
                
                return sysInfo;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetSysConfig", e.Message);
                return null;
            }
        }
        
        //根据ID查找课件
        public lgdt_courseware GetCoursewareByID(string strTeacherID, string strCoursewareID)
        {
            try
            {
                Guid id=new Guid(strCoursewareID);
                if (id == null)
                {
                    return null;
                }
                lgdt_courseware courseware = Invoke<lgdt_courseware>
                (() =>
                {
                    return CoursewareNoAuthBasicSvcProxy.Instance.GetCoursewareById(_mask, strTeacherID, id);
                });
                return courseware;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetCoursewareByID", e.Message);
                return null;
            }
        }

        //不需要根据用户ID
        public lgdt_courseware GetCoursewareByID(string strCoursewareID)
        {
            try
            {
                Guid id = new Guid(strCoursewareID);
                if (id == null)
                {
                    return null;
                }
                lgdt_courseware courseware = Invoke<lgdt_courseware>
                (() =>
                {
                    return CoursewareNoAuthBasicSvcProxy.Instance.GetCoursewareById(_mask, null, id);
                });
                return courseware;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetCoursewareByID", e.Message);
                return null;
            }
        }

        //根据多种条件查找课件
        public lgdt_courseware[] GetCourseware(string strTeacherID, string strTerm, string strCoursewareType, string strKeyword, int iPageSize, int iPageIndex, string strSortField,
            LG.IntelligentCourse.WebService.Common.Model.CommonEnum.SortType AscOrDesc, out int SumCount)
        {
            SumCount = 0;
            try
            {
                IList<lgdt_courseware> coursewares = CoursewareNoAuthBasicSvcProxy.Instance.SearchCourseware(_mask,
                        strTeacherID,
                        strCoursewareType,
                        strTerm,
                        strKeyword,
                        strSortField,
                        AscOrDesc,
                        iPageIndex,
                        iPageSize, 
                        out SumCount);

                return coursewares.ToArray();
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetCourseware", e.Message);
                return null;
            }
        }

        /// <summary>
        /// 批量删除课件
        /// </summary>
        /// <param name="arrCoursewareID">需要删除的课件的ID</param>
        /// <param name="strTeacherID">执行此操作的教师的ID</param>
        /// <returns></returns>
        public bool DeleteCourseware(string[] arrCoursewareID, string strTeacherID)
        {
            try
            {
                if (arrCoursewareID == null)
                {
                    return false;
                }
                Guid[] deleteIds = new Guid[arrCoursewareID.Length];
                for (int i = 0; i < arrCoursewareID.Length; i++)
                {
                    deleteIds[i] = new Guid(arrCoursewareID[i]);
                }
                //已被删除的ID
                deleteIds = Invoke<Guid[]>
                (() =>
                {
                    return CoursewareNoAuthBasicSvcProxy.Instance.DeleteCoursewares(_mask, strTeacherID, deleteIds);
                });

                if (deleteIds == null || deleteIds.Length != arrCoursewareID.Length)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("DeleteCourseware", e.Message);
                return false;
            }
        }

        /// <summary>
        /// 批量共享课件
        /// </summary>
        /// <param name="arrCoursewareID">需共享的课件的ID</param>
        /// <param name="IsShared">true-打开共享，false-关闭共享</param>
        /// <param name="strTeacherID">执行此操作的教师ID</param>
        /// <returns>true-操作成功，false-操作失败</returns>
        public bool ShareCourseware(string[] arrCoursewareID, bool IsShared, string strTeacherID)
        {
            try
            {
                if (arrCoursewareID == null)
                {
                    return false;
                }
                Guid[] ids = new Guid[arrCoursewareID.Length];
                for (int i = 0; i < arrCoursewareID.Length; i++)
                {
                    ids[i] = new Guid(arrCoursewareID[i]);
                }
                bool bResult = Invoke<bool>
                (() =>
                {
                    return CoursewareNoAuthBasicSvcProxy.Instance.SetCoursewareShareFlag(_mask, strTeacherID, ids, IsShared);
                });
                if (bResult == IsShared)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("ShareCourseware", e.Message);
                return false;
            }
        }

        private void WriteErrorMessage(string strMethodName, string strErrorMessage)
        {
            DirectoryInfo clsPath = new DirectoryInfo(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory));
            DirectoryInfo clsSubPath = clsPath.CreateSubdirectory("Errlog\\LBD_WebApiInterface\\Other");

            if (clsSubPath.Exists)
            {
                DateTime clsDate = DateTime.Now;
                string strPath = clsSubPath.FullName + "\\IntelCoursewareI(" + String.Format("{0:yyyy-MM-dd}", clsDate) + ").log";
                StreamWriter clsWriter = new StreamWriter(strPath, true);
                clsWriter.WriteLine(String.Format("{0:HH:mm:ss}", clsDate) + " " + strMethodName + ": " + strErrorMessage);
                clsWriter.Flush();
                clsWriter.Close();
            }
        }
    }
}
