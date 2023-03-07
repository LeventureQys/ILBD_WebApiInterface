using lancoo.cp.basic.sysbaseclass;
using LBD_WebApiInterface.Models.CloudPreparation;
using LBD_WebApiInterface.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;

//using LBD_WebApiInterface.Other;
//using LG.IntelligentCourse.WebService.Model;

namespace LBD_WebApiInterface.Api
{
    /// <summary>
    /// 云备课系统（教学中心2.0）接口
    /// 接口维护人：李萌芽、秦坤
    /// 维护日期：2017-12-19
    /// 维护记录如下：
    /// 1.删除GetIntelCourseware、GetClassTest、GetOptionalCourseware三个接口;@2017-12-19
    /// 2.mCloudPreparationSrvInfo改为私有变量;@2017-12-19
    /// </summary>
    public class CloudPreparationI
    {
        #region 私有变量

        private string mTeachCenterApiUrl;
        private string mHomeworkApiUrl;
        private bool mInit;

        //private IntelCoursewareI mIntelCoursewareI;
        private CloudPreparationSrvInfoM[] mCloudPreparationSrvInfo;

        #endregion 私有变量

        #region 公有变量、属性

        public CloudPreparationSrvInfoM[] CloudPreparationSrvInfo
        { get { return mCloudPreparationSrvInfo; } }

        #endregion 公有变量、属性

        #region 初始化

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="CloudPreparationIP">云备课WebApi的IP</param>
        /// <param name="CloudPreparationPort">云备课WebApi的Port</param>
        /// <returns>true-初始化成功，false-初始化失败</returns>
        public bool Initialize(string CloudPreparationIP, string CloudPreparationPort)
        {
            try
            {
                WriteTrackLog("Initialize【Start】", "CloudPreparationIP=" + CloudPreparationIP + ",CloudPreparationPort=" + CloudPreparationPort);
                if (string.IsNullOrEmpty(CloudPreparationIP) || string.IsNullOrEmpty(CloudPreparationPort))
                {
                    mInit = false;
                    WriteErrorMessage("Initialize", "教学云备课Ip或端口为空，初始化失败！");
                    return false;
                }
                else
                {
                    mInit = true;
                }

                mTeachCenterApiUrl = "http://" + CloudPreparationIP + ":" + CloudPreparationPort;
                WriteTrackLog("Initialize", "mTeachCenterApiUrl=" + mTeachCenterApiUrl);
                //mIntelCoursewareI = new IntelCoursewareI();
                mCloudPreparationSrvInfo = GetCPSrvInfo();
                return true;
            }
            catch (Exception e)
            {
                WriteErrorMessage("Initialize", e.Message);
            }
            return false;
        }

        public bool Initialize(string CloudPreparationIP, string CloudPreparationPort, string HomeworkWsIP, string HomeworkWsPort)
        {
            try
            {
                WriteTrackLog("Initialize【Start】", "CloudPreparationIP=" + CloudPreparationIP + ",CloudPreparationPort=" + CloudPreparationPort + ",HomeworkWsIP=" + HomeworkWsIP + ",HomeworkWsPort=" + HomeworkWsPort);
                if (string.IsNullOrEmpty(CloudPreparationIP) || string.IsNullOrEmpty(CloudPreparationPort) || string.IsNullOrEmpty(HomeworkWsIP) || string.IsNullOrEmpty(HomeworkWsPort))
                {
                    mInit = false;
                    WriteErrorMessage("Initialize", "教学云备课Ip或端口为空，初始化失败！");
                    return false;
                }
                else
                {
                    mInit = true;
                }

                mTeachCenterApiUrl = "http://" + CloudPreparationIP + ":" + CloudPreparationPort;
                mHomeworkApiUrl = "http://" + HomeworkWsIP + ":" + HomeworkWsPort; ;
                WriteTrackLog("Initialize", "mTeachCenterApiUrl=" + mTeachCenterApiUrl + ",mHomeworkApiUrl=" + mHomeworkApiUrl);
                //mIntelCoursewareI = new IntelCoursewareI();
                mCloudPreparationSrvInfo = GetCPSrvInfo();
                return true;
            }
            catch (Exception e)
            {
                WriteErrorMessage("Initialize", e.Message);
            }
            return false;
        }

        #endregion 初始化

        #region 获取云备课服务器信息

        /// <summary>
        ///  获取云备课服务器信息
        /// </summary>
        /// <returns></returns>
        ///
        private CloudPreparationSrvInfoM[] GetCPSrvInfo()
        {
            // int SumCount = 0;
            try
            {
                if (mInit == false) return null;
                string strTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mTeachCenterApiUrl + "/CouldPreparation/SystemServerInfo/queryAll");
                sbUrl.Append("?TimeStamp=" + strTimeStamp);
                sbUrl.Append("&Key=" + CP_MD5Helper.GetMd5Hash(strTimeStamp));
                WriteTrackLog("GetCPSrvInfo", "sbUrl = " + sbUrl);
                string strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
                WriteTrackLog("GetCPSrvInfo", "strResult = " + strResult);
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteDebugInfo("GetCPSrvInfoD", "返回值为空。查询条件为：" + sbUrl.ToString());

                    return null;
                }

                CLP_ApiResultM<CloudPreparationSrvInfoM[]> result = JsonFormatter.JsonDeserialize<CLP_ApiResultM<CloudPreparationSrvInfoM[]>>(strResult);
                if (result.ErrorFlag != 0)
                {
                    WriteErrorMessage("GetCPSrvInfoD", "查询失败，ErrorFlag=" + result.ErrorFlag + "，Message=" + result.Message);

                    return null;
                }

                if (result.Data != null)
                {
                    //SumCount = result.Data.Count;
                    if (result.Data == null)
                    {
                        return null;
                    }
                    else
                    {
                        return result.Data;
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetCPSrvInfoD", e.Message);
            }
            return null;
        }

        #endregion 获取云备课服务器信息

        #region 查询课堂教学方案

        /// <summary>
        /// 查找教学方案（分页查询），带Token
        /// </summary>
        /// <param name="TeacherID">教师ID</param>
        /// <param name="SchoolID">学校ID</param>
        /// <param name="SubjectID">学科ID</param>
        /// <param name="Term">学期ID</param>
        /// <param name="ClassID">班级ID（可选）</param>
        /// <param name="Keyword">关键词</param>
        /// <param name="PageNo">分页查询页码</param>
        /// <param name="PageSize">分页查询每页大小</param>
        /// <param name="Token">token</param>
        /// <param name="SumCount">教学方案总量</param>
        /// 20190927
        /// <returns>教学方案列表</returns>
        public TeachProgramM[] GetTeachingProgram(string TeacherID, string SchoolID, string SubjectID, string Term, string ClassID, string Keyword, int PageNo, int PageSize, string Token, out int SumCount)
        {
            SumCount = 0;
            try
            {
                if (mInit == false) return null;
                string strTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mTeachCenterApiUrl + "/CouldPreparation/TeachingProgram/queryAll");
                sbUrl.Append("?TeacherID=" + TeacherID);
                sbUrl.Append("&SubjectID=" + SubjectID);
                sbUrl.Append("&Term=" + Term);
                sbUrl.Append("&ClassID=" + ClassID);
                sbUrl.Append("&SchoolID=" + SchoolID);
                sbUrl.Append("&Token=" + Token);
                sbUrl.Append("&pageNo=" + PageNo);
                sbUrl.Append("&pageSize=" + PageSize);
                sbUrl.Append("&KeyWord=" + Keyword);
                sbUrl.Append("&TimeStamp=" + strTimeStamp);
                sbUrl.Append("&Key=" + CP_MD5Helper.GetMd5Hash(strTimeStamp + TeacherID));

                string strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteDebugInfo("GetTeachingProgram", "返回值为空。查询条件为：" + sbUrl.ToString());

                    return null;
                }

                CLP_ApiResultM<TeachProgramSetM> result = JsonFormatter.JsonDeserialize<CLP_ApiResultM<TeachProgramSetM>>(strResult);
                if (result.ErrorFlag != 0)
                {
                    WriteErrorMessage("GetTeachingProgram", "查询失败，ErrorFlag=" + result.ErrorFlag + "，Message=" + result.Message);

                    return null;
                }

                if (result.Data != null)
                {
                    SumCount = result.Data.Count;
                    if (result.Data.TeachProgram == null)
                    {
                        return null;
                    }
                    else
                    {
                        return result.Data.TeachProgram.ToArray();
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetTeachingProgram", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 查找教学方案（分页查询）
        /// </summary>
        /// <param name="TeacherID">教师ID</param>
        /// <param name="SchoolID">学校ID</param>
        /// <param name="SubjectID">学科ID</param>
        /// <param name="Term">学期ID</param>
        /// <param name="ClassID">班级ID（可选）</param>
        /// <param name="Keyword">关键词</param>
        /// <param name="PageNo">分页查询页码</param>
        /// <param name="PageSize">分页查询每页大小</param>
        /// <param name="SumCount">教学方案总量</param>
        /// 20190927
        /// <returns>教学方案列表</returns>
        public TeachProgramM[] GetTeachingProgram(string TeacherID, string SchoolID, string SubjectID, string Term, string ClassID, string Keyword, int PageNo, int PageSize, out int SumCount)
        {
            SumCount = 0;
            try
            {
                if (mInit == false) return null;
                string strTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mTeachCenterApiUrl + "/CouldPreparation/TeachingProgram/queryAll");
                sbUrl.Append("?TeacherID=" + TeacherID);
                sbUrl.Append("&SubjectID=" + SubjectID);
                sbUrl.Append("&Term=" + Term);
                sbUrl.Append("&ClassID=" + ClassID);
                sbUrl.Append("&SchoolID=" + SchoolID);
                sbUrl.Append("&pageNo=" + PageNo);
                sbUrl.Append("&pageSize=" + PageSize);
                sbUrl.Append("&KeyWord=" + Keyword);
                sbUrl.Append("&TimeStamp=" + strTimeStamp);
                sbUrl.Append("&Key=" + CP_MD5Helper.GetMd5Hash(strTimeStamp + TeacherID));

                string strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteDebugInfo("GetTeachingProgram", "返回值为空。查询条件为：" + sbUrl.ToString());

                    return null;
                }

                CLP_ApiResultM<TeachProgramSetM> result = JsonFormatter.JsonDeserialize<CLP_ApiResultM<TeachProgramSetM>>(strResult);
                if (result.ErrorFlag != 0)
                {
                    WriteErrorMessage("GetTeachingProgram", "查询失败，ErrorFlag=" + result.ErrorFlag + "，Message=" + result.Message);

                    return null;
                }

                if (result.Data != null)
                {
                    SumCount = result.Data.Count;
                    if (result.Data.TeachProgram == null)
                    {
                        return null;
                    }
                    else
                    {
                        return result.Data.TeachProgram.ToArray();
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetTeachingProgram", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 根据教学方案id查找教学方案
        /// </summary>
        /// <param name="sTeachingProgramID"></param>
        /// <returns></returns>
        public TeachProgramM GetTeachingProgramByID(string sTeachingProgramID)
        {
            // int SumCount = 0;
            try
            {
                if (mInit == false) return null;
                string strTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mTeachCenterApiUrl + "/CouldPreparation/TeachingProgram/queryByTeachProgramID");
                sbUrl.Append("?TeachProgramID=" + sTeachingProgramID);
                sbUrl.Append("&TimeStamp=" + strTimeStamp);
                sbUrl.Append("&Key=" + CP_MD5Helper.GetMd5Hash(strTimeStamp + sTeachingProgramID));

                string strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteErrorMessage("GetTeachingProgramByID", "返回值为空。查询条件为：" + sbUrl.ToString());

                    return null;
                }

                CLP_ApiResultM<TeachProgramM> result = JsonFormatter.JsonDeserialize<CLP_ApiResultM<TeachProgramM>>(strResult);
                if (result.ErrorFlag != 0)
                {
                    WriteErrorMessage("GetTeachingProgramByID", "查询失败，ErrorFlag=" + result.ErrorFlag + "，Message=" + result.Message);

                    return null;
                }

                if (result.Data != null)
                {
                    //SumCount = result.Data.Count;
                    if (result.Data == null)
                    {
                        return null;
                    }
                    else
                    {
                        return result.Data;
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetTeachingProgramByID", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 查找教学方案（不分页，查询所有）,带Token
        /// </summary>
        /// <param name="TeacherID">教师ID</param>
        /// <param name="SchoolID">学校ID</param>
        /// <param name="SubjectID">学科ID</param>
        /// <param name="Term">学期ID</param>
        /// <param name="ClassID">班级ID（可选）</param>
        /// <param name="Token">token</param>
        /// <returns>教学方案列表</returns>
        public TeachProgramM[] GetTeachingProgram(string TeacherID, string SchoolID, string SubjectID, string Term, string ClassID, string Token)
        {
            try
            {
                if (mInit == false) return null;
                string strTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mTeachCenterApiUrl + "/CouldPreparation/TeachingProgram/queryAll");
                sbUrl.Append("?TeacherID=" + TeacherID);
                sbUrl.Append("&SubjectID=" + SubjectID);
                sbUrl.Append("&Term=" + Term);
                sbUrl.Append("&ClassID=" + ClassID);
                sbUrl.Append("&SchoolID=" + SchoolID);
                sbUrl.Append("&Token=" + Token);
                sbUrl.Append("&pageNo=0");
                sbUrl.Append("&pageSize=0");
                sbUrl.Append("&KeyWord=");
                sbUrl.Append("&TimeStamp=" + strTimeStamp);
                sbUrl.Append("&Key=" + CP_MD5Helper.GetMd5Hash(strTimeStamp + TeacherID));

                WriteTrackLog("GetTeachingProgram，不分页", "sbUrl=" + sbUrl.ToString());
                string strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
                WriteTrackLog("GetTeachingProgram，不分页", "strResult:" + strResult);
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteErrorMessage("GetTeachingProgram，不分页", "返回值为空。查询条件为：" + sbUrl.ToString());

                    return null;
                }
                WriteTrackLog("GetTeachingProgram", "1");
                CLP_ApiResultM<TeachProgramSetM> result = JsonFormatter.JsonDeserialize<CLP_ApiResultM<TeachProgramSetM>>(strResult);
                if (result.ErrorFlag != 0)
                {
                    WriteErrorMessage("GetTeachingProgram", "查询失败，ErrorFlag=" + result.ErrorFlag + "，Message=" + result.Message);

                    return null;
                }
                WriteTrackLog("GetTeachingProgram", "2");
                if (result.Data != null)
                {
                    if (result.Data.TeachProgram == null)
                    {
                        WriteErrorMessage("GetTeachingProgram，不分页", "result.Data.TeachProgram为空！");
                        return null;
                    }
                    else
                    {
                        WriteTrackLog("GetTeachingProgram", "3");
                        return result.Data.TeachProgram.ToArray();
                    }
                }
                WriteErrorMessage("GetTeachingProgram，不分页", "异常！！！");
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetTeachingProgram", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 查找教学方案（不分页，查询所有）
        /// </summary>
        /// <param name="TeacherID">教师ID</param>
        /// <param name="SchoolID">学校ID</param>
        /// <param name="SubjectID">学科ID</param>
        /// <param name="Term">学期ID</param>
        /// <param name="ClassID">班级ID（可选）</param>
        /// <returns>教学方案列表</returns>
        public TeachProgramM[] GetTeachingProgram(string TeacherID, string SchoolID, string SubjectID, string Term, string ClassID)
        {
            try
            {
                if (mInit == false) return null;
                string strTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mTeachCenterApiUrl + "/CouldPreparation/TeachingProgram/queryAll");
                sbUrl.Append("?TeacherID=" + TeacherID);
                sbUrl.Append("&SubjectID=" + SubjectID);
                sbUrl.Append("&Term=" + Term);
                sbUrl.Append("&ClassID=" + ClassID);
                sbUrl.Append("&SchoolID=" + SchoolID);
                sbUrl.Append("&pageNo=0");
                sbUrl.Append("&pageSize=0");
                sbUrl.Append("&KeyWord=");
                sbUrl.Append("&TimeStamp=" + strTimeStamp);
                sbUrl.Append("&Key=" + CP_MD5Helper.GetMd5Hash(strTimeStamp + TeacherID));

                WriteTrackLog("GetTeachingProgram，不分页", "sbUrl=" + sbUrl.ToString());
                string strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
                WriteTrackLog("GetTeachingProgram，不分页", "strResult:" + strResult);
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteErrorMessage("GetTeachingProgram，不分页", "返回值为空。查询条件为：" + sbUrl.ToString());

                    return null;
                }
                WriteTrackLog("GetTeachingProgram", "1");
                CLP_ApiResultM<TeachProgramSetM> result = JsonFormatter.JsonDeserialize<CLP_ApiResultM<TeachProgramSetM>>(strResult);
                if (result.ErrorFlag != 0)
                {
                    WriteErrorMessage("GetTeachingProgram", "查询失败，ErrorFlag=" + result.ErrorFlag + "，Message=" + result.Message);

                    return null;
                }
                WriteTrackLog("GetTeachingProgram", "2");
                if (result.Data != null)
                {
                    if (result.Data.TeachProgram == null)
                    {
                        WriteErrorMessage("GetTeachingProgram，不分页", "result.Data.TeachProgram为空！");
                        return null;
                    }
                    else
                    {
                        WriteTrackLog("GetTeachingProgram", "3");
                        return result.Data.TeachProgram.ToArray();
                    }
                }
                WriteErrorMessage("GetTeachingProgram，不分页", "异常！！！");
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetTeachingProgram", e.Message);
            }
            return null;
        }

        #endregion 查询课堂教学方案

        #region 根据课堂教案id查找课堂教案更多信息

        /// <summary>
        /// 根据课堂教案id查找课堂教案信息
        /// </summary>
        /// <param name="sTeachClassProgramID">课堂教案ID</param>
        /// <returns></returns>
        public TeachClassProgramM GetTeachClassProgramByID(string sTeachClassProgramID)
        {
            // int SumCount = 0;
            try
            {
                if (mInit == false) return null;
                string strTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mTeachCenterApiUrl + "/CouldPreparation/LessonPlan/queryByTeachClassProgramID");
                sbUrl.Append("?TeachClassProgramID=" + sTeachClassProgramID);
                sbUrl.Append("&TimeStamp=" + strTimeStamp);
                sbUrl.Append("&Key=" + CP_MD5Helper.GetMd5Hash(strTimeStamp + sTeachClassProgramID));

                string strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteDebugInfo("GetTeachingProgramByID", "返回值为空。查询条件为：" + sbUrl.ToString());

                    return null;
                }

                CLP_ApiResultM<TeachClassProgramM> result = JsonFormatter.JsonDeserialize<CLP_ApiResultM<TeachClassProgramM>>(strResult);
                if (result.ErrorFlag != 0)
                {
                    WriteErrorMessage("GetTeachingProgramByID", "查询失败，ErrorFlag=" + result.ErrorFlag + "，Message=" + result.Message);

                    return null;
                }

                if (result.Data != null)
                {
                    //SumCount = result.Data.Count;
                    if (result.Data == null)
                    {
                        return null;
                    }
                    else
                    {
                        return result.Data;
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetTeachingProgramByID", e.Message);
            }
            return null;
        }

        #endregion 根据课堂教案id查找课堂教案更多信息

        #region 根据用户ID、学校ID、学科ID、学期、班级ID查询课前、课堂、课后相关

        /// <summary>
        /// 查询课前预习方案,带Token
        /// </summary>
        /// <param name="TeacherID">教师ID</param>
        /// <param name="SchoolID">学校ID</param>
        /// <param name="SubjectID">学科ID</param>
        /// <param name="Term">学期ID</param>
        /// <param name="ClassID">班级ID（可选）</param>
        /// <param name="Token">token</param>
        /// <returns>课前预习方案列表</returns>
        public PreClassProgramM[] GetPreview(string TeacherID, string SchoolID, string SubjectID, string Term, string ClassID, string Token)
        {
            try
            {
                if (mInit == false) return null;
                string strTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mTeachCenterApiUrl + "/CouldPreparation/PreviewCourse/queryAll");
                sbUrl.Append("?TeacherID=" + TeacherID);
                sbUrl.Append("&SubjectID=" + SubjectID);
                sbUrl.Append("&Term=" + Term);
                sbUrl.Append("&ClassID=" + ClassID);
                sbUrl.Append("&SchoolID=" + SchoolID);
                sbUrl.Append("&Token=" + Token);
                sbUrl.Append("&TimeStamp=" + strTimeStamp);
                //sbUrl.Append("&Key=" + CP_EncryptHelper.EncryptCode("X888", strTimeStamp + TeacherID));
                sbUrl.Append("&Key=" + CP_MD5Helper.GetMd5Hash(strTimeStamp + TeacherID));

                WriteTrackLog("GetPreview", "sbUrl=" + sbUrl.ToString());
                string strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteDebugInfo("GetPreview", "返回值为空。查询条件为：" + sbUrl.ToString());

                    return null;
                }

                CLP_ApiResultM<PreClassProgramM[]> result = JsonFormatter.JsonDeserialize<CLP_ApiResultM<PreClassProgramM[]>>(strResult);
                if (result.ErrorFlag != 0)
                {
                    WriteErrorMessage("GetPreview", "查询失败，ErrorFlag=" + result.ErrorFlag + "，Message=" + result.Message);

                    return null;
                }

                if (result.Data != null)
                {
                    return result.Data;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetPreview", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 查询课前预习方案
        /// </summary>
        /// <param name="TeacherID">教师ID</param>
        /// <param name="SchoolID">学校ID</param>
        /// <param name="SubjectID">学科ID</param>
        /// <param name="Term">学期ID</param>
        /// <param name="ClassID">班级ID（可选）</param>
        /// <returns>课前预习方案列表</returns>
        public PreClassProgramM[] GetPreview(string TeacherID, string SchoolID, string SubjectID, string Term, string ClassID)
        {
            try
            {
                if (mInit == false) return null;
                string strTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mTeachCenterApiUrl + "/CouldPreparation/PreviewCourse/queryAll");
                sbUrl.Append("?TeacherID=" + TeacherID);
                sbUrl.Append("&SubjectID=" + SubjectID);
                sbUrl.Append("&Term=" + Term);
                sbUrl.Append("&ClassID=" + ClassID);
                sbUrl.Append("&SchoolID=" + SchoolID);
                sbUrl.Append("&TimeStamp=" + strTimeStamp);
                //sbUrl.Append("&Key=" + CP_EncryptHelper.EncryptCode("X888", strTimeStamp + TeacherID));
                sbUrl.Append("&Key=" + CP_MD5Helper.GetMd5Hash(strTimeStamp + TeacherID));

                WriteTrackLog("GetPreview", "sbUrl=" + sbUrl.ToString());
                string strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteDebugInfo("GetPreview", "返回值为空。查询条件为：" + sbUrl.ToString());

                    return null;
                }

                CLP_ApiResultM<PreClassProgramM[]> result = JsonFormatter.JsonDeserialize<CLP_ApiResultM<PreClassProgramM[]>>(strResult);
                if (result.ErrorFlag != 0)
                {
                    WriteErrorMessage("GetPreview", "查询失败，ErrorFlag=" + result.ErrorFlag + "，Message=" + result.Message);

                    return null;
                }

                if (result.Data != null)
                {
                    return result.Data;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetPreview", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 查找课堂教案，带Token
        /// </summary>
        /// <param name="TeacherID">教师ID</param>
        /// <param name="SchoolID">学校ID</param>
        /// <param name="SubjectID">学科ID</param>
        /// <param name="Term">学期</param>
        /// <param name="ClassID">班级ID（可空）</param>
        /// <param name="Token">token</param>
        /// <returns></returns>
        public TeachClassProgramM[] GetTeachClassProgram(string TeacherID, string SchoolID, string SubjectID, string Term, string ClassID, string Token)
        {
            try
            {
                if (mInit == false) return null;
                string strTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mTeachCenterApiUrl + "/CouldPreparation/LessonPlan/queryAll");
                sbUrl.Append("?TeacherID=" + TeacherID);
                sbUrl.Append("&SubjectID=" + SubjectID);
                sbUrl.Append("&Term=" + Term);
                sbUrl.Append("&ClassID=" + ClassID);
                sbUrl.Append("&SchoolID=" + SchoolID);
                sbUrl.Append("&Token=" + Token);
                sbUrl.Append("&TimeStamp=" + strTimeStamp);
                sbUrl.Append("&Key=" + CP_MD5Helper.GetMd5Hash(strTimeStamp + TeacherID));

                WriteTrackLog("GetTeachClassProgram", "sbUrl=" + sbUrl.ToString());
                string strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteDebugInfo("GetTeachClassProgram", "返回值为空。查询条件为：" + sbUrl.ToString());

                    return null;
                }

                CLP_ApiResultM<TeachClassProgramM[]> result = JsonFormatter.JsonDeserialize<CLP_ApiResultM<TeachClassProgramM[]>>(strResult);
                if (result.ErrorFlag != 0)
                {
                    WriteErrorMessage("GetTeachClassProgram", "查询失败，ErrorFlag=" + result.ErrorFlag + "，Message=" + result.Message);

                    return null;
                }

                if (result.Data != null)
                {
                    return result.Data;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetTeachClassProgram", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 查找课堂教案
        /// </summary>
        /// <param name="TeacherID">教师ID</param>
        /// <param name="SchoolID">学校ID</param>
        /// <param name="SubjectID">学科ID</param>
        /// <param name="Term">学期</param>
        /// <param name="ClassID">班级ID（可空）</param>
        /// <returns></returns>
        public TeachClassProgramM[] GetTeachClassProgram(string TeacherID, string SchoolID, string SubjectID, string Term, string ClassID)
        {
            try
            {
                if (mInit == false) return null;
                string strTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mTeachCenterApiUrl + "/CouldPreparation/LessonPlan/queryAll");
                sbUrl.Append("?TeacherID=" + TeacherID);
                sbUrl.Append("&SubjectID=" + SubjectID);
                sbUrl.Append("&Term=" + Term);
                sbUrl.Append("&ClassID=" + ClassID);
                sbUrl.Append("&SchoolID=" + SchoolID);
                sbUrl.Append("&TimeStamp=" + strTimeStamp);
                sbUrl.Append("&Key=" + CP_MD5Helper.GetMd5Hash(strTimeStamp + TeacherID));

                WriteTrackLog("GetTeachClassProgram", "sbUrl=" + sbUrl.ToString());
                string strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteDebugInfo("GetTeachClassProgram", "返回值为空。查询条件为：" + sbUrl.ToString());

                    return null;
                }

                CLP_ApiResultM<TeachClassProgramM[]> result = JsonFormatter.JsonDeserialize<CLP_ApiResultM<TeachClassProgramM[]>>(strResult);
                if (result.ErrorFlag != 0)
                {
                    WriteErrorMessage("GetTeachClassProgram", "查询失败，ErrorFlag=" + result.ErrorFlag + "，Message=" + result.Message);

                    return null;
                }

                if (result.Data != null)
                {
                    return result.Data;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetTeachClassProgram", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 查询课后测试（作业），带Token
        /// </summary>
        /// <param name="TeacherID">教师ID</param>
        /// <param name="SchoolID">学校ID</param>
        /// <param name="SubjectID">学科ID</param>
        /// <param name="Term">学期ID</param>
        /// <param name="ClassID">班级ID（可选）</param>
        /// <param name="Token">token</param>
        /// <returns>课后测试（作业）列表</returns>
        public AfterClassProgramM[] GetHomework(string TeacherID, string SchoolID, string SubjectID, string Term, string ClassID, string Token)
        {
            try
            {
                if (mInit == false) return null;
                string strTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mTeachCenterApiUrl + "/CouldPreparation/AfterClassTest/queryAll");
                sbUrl.Append("?TeacherID=" + TeacherID);
                sbUrl.Append("&SubjectID=" + SubjectID);
                sbUrl.Append("&Term=" + Term);
                sbUrl.Append("&ClassID=" + ClassID);
                sbUrl.Append("&SchoolID=" + SchoolID);
                sbUrl.Append("&Token=" + Token);
                sbUrl.Append("&TimeStamp=" + strTimeStamp);
                //sbUrl.Append("&Key=" + CP_EncryptHelper.EncryptCode("X888", strTimeStamp + TeacherID));
                sbUrl.Append("&Key=" + CP_MD5Helper.GetMd5Hash(strTimeStamp + TeacherID));

                string strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteDebugInfo("GetHomework", "返回值为空。查询条件为：" + sbUrl.ToString());

                    return null;
                }

                CLP_ApiResultM<AfterClassProgramM[]> result = JsonFormatter.JsonDeserialize<CLP_ApiResultM<AfterClassProgramM[]>>(strResult);
                if (result.ErrorFlag != 0)
                {
                    WriteErrorMessage("GetHomework", "查询失败，ErrorFlag=" + result.ErrorFlag + "，Message=" + result.Message);

                    return null;
                }

                if (result.Data != null)
                {
                    return result.Data;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetHomework", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 查询课后测试（作业）
        /// </summary>
        /// <param name="TeacherID">教师ID</param>
        /// <param name="SchoolID">学校ID</param>
        /// <param name="SubjectID">学科ID</param>
        /// <param name="Term">学期ID</param>
        /// <param name="ClassID">班级ID（可选）</param>
        /// <returns>课后测试（作业）列表</returns>
        public AfterClassProgramM[] GetHomework(string TeacherID, string SchoolID, string SubjectID, string Term, string ClassID)
        {
            try
            {
                if (mInit == false) return null;
                string strTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mTeachCenterApiUrl + "/CouldPreparation/AfterClassTest/queryAll");
                sbUrl.Append("?TeacherID=" + TeacherID);
                sbUrl.Append("&SubjectID=" + SubjectID);
                sbUrl.Append("&Term=" + Term);
                sbUrl.Append("&ClassID=" + ClassID);
                sbUrl.Append("&SchoolID=" + SchoolID);
                sbUrl.Append("&TimeStamp=" + strTimeStamp);
                //sbUrl.Append("&Key=" + CP_EncryptHelper.EncryptCode("X888", strTimeStamp + TeacherID));
                sbUrl.Append("&Key=" + CP_MD5Helper.GetMd5Hash(strTimeStamp + TeacherID));

                string strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteDebugInfo("GetHomework", "返回值为空。查询条件为：" + sbUrl.ToString());

                    return null;
                }

                CLP_ApiResultM<AfterClassProgramM[]> result = JsonFormatter.JsonDeserialize<CLP_ApiResultM<AfterClassProgramM[]>>(strResult);
                if (result.ErrorFlag != 0)
                {
                    WriteErrorMessage("GetHomework", "查询失败，ErrorFlag=" + result.ErrorFlag + "，Message=" + result.Message);

                    return null;
                }

                if (result.Data != null)
                {
                    return result.Data;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetHomework", e.Message);
            }
            return null;
        }

        #endregion 根据用户ID、学校ID、学科ID、学期、班级ID查询课前、课堂、课后相关

        #region 根据教学方案ID,查询课堂、课前、课后、相关教辅

        /// <summary>
        /// 根据教学方案ID查询该教学方案下的课堂教案
        /// </summary>
        /// <param name="TeachProgramID">教学方案ID</param>
        /// <returns>教学方案下的课堂教案</returns>
        public TeachClassProgramM GetTeachClassProgramByTPID(string TeachProgramID)
        {
            try
            {
                if (mInit == false) return null;
                string strTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mTeachCenterApiUrl + "/CouldPreparation/LessonPlan/queryByTeachingProgramID");
                sbUrl.Append("?TeachProgramID=" + TeachProgramID);
                sbUrl.Append("&TimeStamp=" + strTimeStamp);
                sbUrl.Append("&Key=" + CP_MD5Helper.GetMd5Hash(strTimeStamp + TeachProgramID));

                string strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteDebugInfo("GetTeachClassProgramByTPID", "返回值为空。查询条件为：" + sbUrl.ToString());

                    return null;
                }

                CLP_ApiResultM<TeachClassProgramM> result = JsonFormatter.JsonDeserialize<CLP_ApiResultM<TeachClassProgramM>>(strResult);
                if (result.ErrorFlag != 0)
                {
                    WriteErrorMessage("GetTeachClassProgramByTPID", "查询失败，ErrorFlag=" + result.ErrorFlag + "，Message=" + result.Message);

                    return null;
                }

                if (result.Data != null)
                {
                    return result.Data;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetTeachClassProgramByTPID", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 根据教学方案ID查询该教学方案下的课前预习
        /// </summary>
        /// <param name="TeachProgramID"></param>
        /// <returns></returns>
        public PreClassProgramM GetPreviewByTPID(string TeachProgramID)
        {
            try
            {
                if (mInit == false) return null;
                string strTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mTeachCenterApiUrl + "/CouldPreparation/PreviewCourse/queryByTeachingProgramID");
                sbUrl.Append("?TeachProgramID=" + TeachProgramID);
                sbUrl.Append("&TimeStamp=" + strTimeStamp);
                sbUrl.Append("&Key=" + CP_MD5Helper.GetMd5Hash(strTimeStamp + TeachProgramID));

                string strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteDebugInfo("GetPreviewByTPID", "返回值为空。查询条件为：" + sbUrl.ToString());

                    return null;
                }

                CLP_ApiResultM<PreClassProgramM> result = JsonFormatter.JsonDeserialize<CLP_ApiResultM<PreClassProgramM>>(strResult);
                if (result.ErrorFlag != 0)
                {
                    WriteErrorMessage("GetPreviewByTPID", "查询失败，ErrorFlag=" + result.ErrorFlag + "，Message=" + result.Message);

                    return null;
                }

                if (result.Data != null)
                {
                    return result.Data;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetPreviewByTPID", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 根据教学方案ID查询该教学方案下的课后预习
        /// </summary>
        /// <param name="TeachProgramID"></param>
        /// <returns></returns>
        public AfterClassProgramM GetHomeworkByTPID(string TeachProgramID)
        {
            try
            {
                if (mInit == false) return null;
                string strTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mTeachCenterApiUrl + "/CouldPreparation/AfterClassTest/queryByTeachingProgramID");
                sbUrl.Append("?TeachProgramID=" + TeachProgramID);
                sbUrl.Append("&TimeStamp=" + strTimeStamp);
                sbUrl.Append("&Key=" + CP_MD5Helper.GetMd5Hash(strTimeStamp + TeachProgramID));

                string strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteDebugInfo("GetGetHomeworkByTPID", "返回值为空。查询条件为：" + sbUrl.ToString());

                    return null;
                }

                CLP_ApiResultM<AfterClassProgramM> result = JsonFormatter.JsonDeserialize<CLP_ApiResultM<AfterClassProgramM>>(strResult);
                if (result.ErrorFlag != 0)
                {
                    WriteErrorMessage("GetGetHomeworkByTPID", "查询失败，ErrorFlag=" + result.ErrorFlag + "，Message=" + result.Message);

                    return null;
                }

                if (result.Data != null)
                {
                    return result.Data;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetGetHomeworkByTPID", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 根据教学方案ID查询该教学方案下的相关教辅
        /// </summary>
        /// <param name="TeachProgramID">教学方案ID</param>
        /// <returns>教学方案下的相关教辅</returns>
        public AttachResourceProgramM GetAttachResourceProgramByTPID(string TeachProgramID)
        {
            try
            {
                if (mInit == false) return null;
                string strTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mTeachCenterApiUrl + "/CouldPreparation/AttachResourceProgram/queryByTeachingProgramID");
                sbUrl.Append("?TeachProgramID=" + TeachProgramID);
                sbUrl.Append("&TimeStamp=" + strTimeStamp);
                sbUrl.Append("&Key=" + CP_MD5Helper.GetMd5Hash(strTimeStamp + TeachProgramID));

                string strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteDebugInfo("GetAttachResourceProgramByTPID", "返回值为空。查询条件为：" + sbUrl.ToString());

                    return null;
                }

                CLP_ApiResultM<AttachResourceProgramM> result = JsonFormatter.JsonDeserialize<CLP_ApiResultM<AttachResourceProgramM>>(strResult);
                if (result.ErrorFlag != 0)
                {
                    WriteErrorMessage("GetAttachResourceProgramByTPID", "查询失败，ErrorFlag=" + result.ErrorFlag + "，Message=" + result.Message);

                    return null;
                }

                if (result.Data != null)
                {
                    return result.Data;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetAttachResourceProgramByTPID", e.Message);
            }
            return null;
        }

        #endregion 根据教学方案ID,查询课堂、课前、课后、相关教辅

        #region 查询任务类

        /// <summary>
        /// 查询课前预习任务信息
        /// </summary>
        /// <param name="TeacherID">教师ID</param>
        /// <param name="SchoolID">学校ID</param>
        /// <param name="SubjectID">学科ID</param>
        /// <param name="Term">学期ID</param>
        /// <param name="ClassID">班级ID</param>
        /// <returns>课前预习任务列表</returns>
        public PreClassAssignmentM[] GetPreviewTask(string TeacherID, string SchoolID, string SubjectID, string Term, string ClassID)
        {
            try
            {
                if (mInit == false) return null;
                string strTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mTeachCenterApiUrl + "/CouldPreparation/PreviewAssignment/queryAll");
                sbUrl.Append("?TeacherID=" + TeacherID);
                sbUrl.Append("&SubjectID=" + SubjectID);
                sbUrl.Append("&Term=" + Term);
                sbUrl.Append("&ClassID=" + ClassID);
                sbUrl.Append("&SchoolID=" + SchoolID);
                sbUrl.Append("&TimeStamp=" + strTimeStamp);
                //sbUrl.Append("&Key=" + CP_EncryptHelper.EncryptCode("X888", strTimeStamp + TeacherID));
                sbUrl.Append("&Key=" + CP_MD5Helper.GetMd5Hash(strTimeStamp + TeacherID));

                string strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteDebugInfo("GetPreviewTask", "返回值为空。查询条件为：" + sbUrl.ToString());

                    return null;
                }

                CLP_ApiResultM<PreClassAssignmentM[]> result = JsonFormatter.JsonDeserialize<CLP_ApiResultM<PreClassAssignmentM[]>>(strResult);
                if (result.ErrorFlag != 0)
                {
                    WriteErrorMessage("GetPreviewTask", "查询失败，ErrorFlag=" + result.ErrorFlag + "，Message=" + result.Message);

                    return null;
                }

                if (result.Data != null)
                {
                    return result.Data;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetPreviewTask", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 查询课后测试任务
        /// </summary>
        /// <param name="TeacherID">教师ID</param>
        /// <param name="SchoolID">学校ID</param>
        /// <param name="SubjectID">学科ID</param>
        /// <param name="Term">学期ID</param>
        /// <param name="ClassID">班级ID</param>
        /// <returns>课后测试任务列表</returns>
        public AfterClassAssignmentM[] GetAfterClassAssignment(string TeacherID, string SchoolID, string SubjectID, string Term, string ClassID)
        {
            try
            {
                if (mInit == false) return null;
                string strTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mTeachCenterApiUrl + "/CouldPreparation/AfterClassAssignment/queryAll");
                sbUrl.Append("?TeacherID=" + TeacherID);
                sbUrl.Append("&SubjectID=" + SubjectID);
                sbUrl.Append("&Term=" + Term);
                sbUrl.Append("&ClassID=" + ClassID);
                sbUrl.Append("&SchoolID=" + SchoolID);
                sbUrl.Append("&TimeStamp=" + strTimeStamp);
                sbUrl.Append("&Key=" + CP_MD5Helper.GetMd5Hash(strTimeStamp + TeacherID));

                string strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteDebugInfo("GetAfterClassAssignment", "返回值为空。查询条件为：" + sbUrl.ToString());

                    return null;
                }

                CLP_ApiResultM<AfterClassAssignmentM[]> result = JsonFormatter.JsonDeserialize<CLP_ApiResultM<AfterClassAssignmentM[]>>(strResult);
                if (result.ErrorFlag != 0)
                {
                    WriteErrorMessage("GetAfterClassAssignment", "查询失败，ErrorFlag=" + result.ErrorFlag + "，Message=" + result.Message);

                    return null;
                }

                if (result.Data != null)
                {
                    return result.Data;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetAfterClassAssignment", e.Message);
            }
            return null;
        }

        //设置某个设置项的值返回值为ID(学科教学云调用)
        public long SetItemValueSingle(LBD_WebApiInterface.Api.TeachSetI.E_SetItem eSetItem, byte bSubjectID, string strTeacherID, string strValue, string strCourseClassID, short sClassroomIndex)
        {
            try
            {
                short setItemID = TeachSetI.GetSetItemID(eSetItem);
                string param = JsonFormatter.JsonSerialize(new
                {
                    SetItemID = setItemID,
                    SetItemValue = strValue,
                    SubjectID = bSubjectID,
                    TeachID = strTeacherID,
                    CoursePlanID = strCourseClassID,
                    ClassIndex = sClassroomIndex
                });
                string strReturn = CallApiHelper.CallMethodPost(mTeachCenterApiUrl + "/CouldPreparation/TeacherSetInfo/insertTeacherSetInfo", param);
                if (string.IsNullOrEmpty(strReturn))
                    return 0;
                else
                {
                    CLP_ApiResultM<string> result = JsonFormatter.JsonDeserialize<CLP_ApiResultM<string>>(strReturn);
                    if (result.ErrorFlag == 0)
                        return long.Parse(result.Data);
                    else
                    {
                        WriteErrorMessage("SetItemValueSingle", strReturn);
                        return 0;
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("SetItemValueSingle", e.Message);
            }

            return 0;
        }

        //获取某个设置项的值(学科教学云调用)
        public string GetItemValueSingle(LBD_WebApiInterface.Api.TeachSetI.E_SetItem eSetItem, byte bSubjectID, string strTeacherID, string strCourseClassID, short sClassroomIndex)
        {
            try
            {
                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mTeachCenterApiUrl + "/CouldPreparation/TeacherSetInfo/getTeacherSetInfo");
                sbUrl.Append("?SetItemID=" + TeachSetI.GetSetItemID(eSetItem).ToString());
                sbUrl.Append("&SubjectID=" + bSubjectID.ToString());
                sbUrl.Append("&TeachID=" + strTeacherID);
                sbUrl.Append("&CoursePlanID=" + strCourseClassID);
                sbUrl.Append("&ClassIndex=" + sClassroomIndex.ToString());

                string strReturn = CallApiHelper.CallMethodGet(sbUrl.ToString());
                if (string.IsNullOrEmpty(strReturn))
                    return null;
                else
                {
                    CLP_ApiResultM<JObject> result = JsonFormatter.JsonDeserialize<CLP_ApiResultM<JObject>>(strReturn);
                    if (result.ErrorFlag == 0)
                    {
                        JObject Data = result.Data;
                        if (Data == null)
                            return null;
                        string setValue = Data["SetItemValue"].ToString();
                        return setValue;
                    }
                    else
                    {
                        WriteErrorMessage("GetItemValueSingle", strReturn);
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetItemValueSingle", e.Message);
                return null;
            }
        }

        #endregion 查询任务类

        #region 课后离线作业上传、下载相关

        /// <summary>
        ///  制作离线作业
        /// </summary>
        /// <param name="sHomeworkApiUrl">课后作业wsUrl，如：http://192.168.3.61:8020/ ,以"/"结尾</param>
        /// <param name="UserID">用户ID</param>
        /// <param name="AssignmentID">任务ID</param>
        /// <returns></returns>
        public OfflineHomeworkFtpInfo MakeOfflineHomeWork(string sHomeworkApiUrl, string UserID, string AssignmentID)
        {
            try
            {
                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(sHomeworkApiUrl + "api/TaskAnswer/GetUnpackTaskFtpPath");
                sbUrl.Append(string.Format("?userID={0}&assignmentID={1}", UserID, AssignmentID));
                WriteTrackLog("MakeOfflineHomeWork", "sbUrl = " + sbUrl);
                string strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
                WriteTrackLog("MakeOfflineHomeWork", "strResult = " + strResult);
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteErrorMessage("MakeOfflineHomeWork", "返回值为空。查询条件为：" + sbUrl.ToString());
                    return null;
                }
                //XmlDocument doc=new XmlDocument();
                //doc.LoadXml(strResult);
                //XmlNodeList Data_nodes = doc.GetElementsByTagName("Data");
                OHW_ApiResultM<OfflineHomeworkFtpInfo> result = JsonFormatter.JsonDeserialize<OHW_ApiResultM<OfflineHomeworkFtpInfo>>(strResult);
                result.Data.FtpIP = lancoo.cp.basic.sysbaseclass.CP_EncryptHelper.DecryptCode("unpack", result.Data.FtpIP);
                result.Data.Ftppassword = lancoo.cp.basic.sysbaseclass.CP_EncryptHelper.DecryptCode("unpack", result.Data.Ftppassword);
                result.Data.Ftppath = lancoo.cp.basic.sysbaseclass.CP_EncryptHelper.DecryptCode("unpack", result.Data.Ftppath);
                result.Data.Ftpport = lancoo.cp.basic.sysbaseclass.CP_EncryptHelper.DecryptCode("unpack", result.Data.Ftpport);
                result.Data.FtpUserName = lancoo.cp.basic.sysbaseclass.CP_EncryptHelper.DecryptCode("unpack", result.Data.FtpUserName);
                return result.Data;
            }
            catch (Exception e)
            {
                WriteErrorMessage("MakeOfflineHomeWork", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 上传离线作业(作业ftp上传后调用此接口）
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="FtpIP">ftp的ip地址</param>
        /// <param name="FtpPort">ftp的端口</param>
        /// <param name="FtpPath">上传的离线作业ftp相对地址</param>
        /// <param name="FtpUserName">ftp的用户名</param>
        /// <param name="FtpPassword">ftp的密码</param>
        /// <returns>False-表示失败，True-表示成功</returns>
        public bool PutMyOfflineHomeWork(string UserID, string FtpIP, string FtpPort, string FtpPath, string FtpUserName, string FtpPassword, string AssignmentID)
        {
            try
            {
                UserID = lancoo.cp.basic.sysbaseclass.CP_EncryptHelper.EncryptCode("unpack", UserID);
                FtpIP = lancoo.cp.basic.sysbaseclass.CP_EncryptHelper.EncryptCode("unpack", FtpIP);
                FtpPort = lancoo.cp.basic.sysbaseclass.CP_EncryptHelper.EncryptCode("unpack", FtpPort);
                FtpPath = lancoo.cp.basic.sysbaseclass.CP_EncryptHelper.EncryptCode("unpack", FtpPath);
                FtpUserName = lancoo.cp.basic.sysbaseclass.CP_EncryptHelper.EncryptCode("unpack", FtpUserName);
                FtpPassword = lancoo.cp.basic.sysbaseclass.CP_EncryptHelper.EncryptCode("unpack", FtpPassword);
                AssignmentID = lancoo.cp.basic.sysbaseclass.CP_EncryptHelper.EncryptCode("unpack", AssignmentID);

                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mHomeworkApiUrl + "/api/TaskAnswer/PutUnpackTaskSuccess");
                //sbUrl.Append(string.Format("?userID={0}&ftpIP={1}&ftpPort={2}&ftpPath={3}&ftpUserName={4}&ftpPassword={5}", UserID, FtpIP, FtpPort, FtpPath, FtpUserName, FtpPassword));

                UnpackTaskSuccessMode utsmObj = new UnpackTaskSuccessMode();
                utsmObj.userID = UserID;
                utsmObj.assignmentID = AssignmentID;
                utsmObj.ftpIP = FtpIP;
                utsmObj.ftpPort = FtpPort;
                utsmObj.ftpPath = FtpPath;
                utsmObj.ftpUserID = FtpUserName;
                utsmObj.ftpPassword = FtpPassword;

                string strParam = JsonFormatter.JsonSerialize(utsmObj);
                WriteTrackLog("PutMyOfflineHomeWork", "strParam = " + strParam);
                string strResult = CallApiHelper.CallMethodPost(sbUrl.ToString(), strParam);
                WriteTrackLog("PutMyOfflineHomeWork", "strResult = " + strResult);
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteErrorMessage("PutMyOfflineHomeWork", "返回值为空。查询条件为：" + sbUrl.ToString());
                    return false;
                }
                //XmlDocument doc=new XmlDocument();
                //doc.LoadXml(strResult);
                //XmlNodeList Data_nodes = doc.GetElementsByTagName("Data");
                OHW_ApiResultM<string> result = JsonFormatter.JsonDeserialize<OHW_ApiResultM<string>>(strResult);
                if (result.Code == "00")
                    return true;
                else
                {
                    WriteErrorMessage("PutMyOfflineHomeWork", "返回值：" + result.Code + " " + result.Data + " " + result.Msg);
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("PutMyOfflineHomeWork", e.Message);
            }
            return false;
        }

        /// <summary>
        /// 上传离线作业(作业ftp上传后调用此接口）
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="FtpIP">ftp的ip地址</param>
        /// <param name="FtpPort">ftp的端口</param>
        /// <param name="FtpPath">上传的离线作业ftp相对地址</param>
        /// <param name="FtpUserName">ftp的用户名</param>
        /// <param name="FtpPassword">ftp的密码</param>
        /// <returns>False-表示失败，True-表示成功</returns>
        public bool PutMyOfflineHomeWork(string UserID, string FtpIP, string FtpPort, string FtpPath, string FtpUserName, string FtpPassword, string AssignmentID, out string strRtnMsg)
        {
            strRtnMsg = "";
            try
            {
                UserID = lancoo.cp.basic.sysbaseclass.CP_EncryptHelper.EncryptCode("unpack", UserID);
                FtpIP = lancoo.cp.basic.sysbaseclass.CP_EncryptHelper.EncryptCode("unpack", FtpIP);
                FtpPort = lancoo.cp.basic.sysbaseclass.CP_EncryptHelper.EncryptCode("unpack", FtpPort);
                FtpPath = lancoo.cp.basic.sysbaseclass.CP_EncryptHelper.EncryptCode("unpack", FtpPath);
                FtpUserName = lancoo.cp.basic.sysbaseclass.CP_EncryptHelper.EncryptCode("unpack", FtpUserName);
                FtpPassword = lancoo.cp.basic.sysbaseclass.CP_EncryptHelper.EncryptCode("unpack", FtpPassword);
                AssignmentID = lancoo.cp.basic.sysbaseclass.CP_EncryptHelper.EncryptCode("unpack", AssignmentID);

                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mHomeworkApiUrl + "/api/TaskAnswer/PutUnpackTaskSuccess");
                //sbUrl.Append(string.Format("?userID={0}&ftpIP={1}&ftpPort={2}&ftpPath={3}&ftpUserName={4}&ftpPassword={5}", UserID, FtpIP, FtpPort, FtpPath, FtpUserName, FtpPassword));

                UnpackTaskSuccessMode utsmObj = new UnpackTaskSuccessMode();
                utsmObj.userID = UserID;
                utsmObj.assignmentID = AssignmentID;
                utsmObj.ftpIP = FtpIP;
                utsmObj.ftpPort = FtpPort;
                utsmObj.ftpPath = FtpPath;
                utsmObj.ftpUserID = FtpUserName;
                utsmObj.ftpPassword = FtpPassword;

                string strParam = JsonFormatter.JsonSerialize(utsmObj);
                WriteTrackLog("PutMyOfflineHomeWork2", "strParam = " + strParam);
                string strResult = CallApiHelper.CallMethodPost(sbUrl.ToString(), strParam);
                WriteTrackLog("PutMyOfflineHomeWork2", "strResult = " + strResult);
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteErrorMessage("PutMyOfflineHomeWork", "返回值为空。查询条件为：" + sbUrl.ToString());
                    return false;
                }
                //XmlDocument doc=new XmlDocument();
                //doc.LoadXml(strResult);
                //XmlNodeList Data_nodes = doc.GetElementsByTagName("Data");
                OHW_ApiResultM<string> result = JsonFormatter.JsonDeserialize<OHW_ApiResultM<string>>(strResult);
                if (result.Code == "00")
                {
                    return true;
                }
                else
                {
                    WriteErrorMessage("PutMyOfflineHomeWork", "返回值：" + result.Code + " " + result.Data + " " + result.Msg);
                    strRtnMsg = result.Msg;
                    return false;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("PutMyOfflineHomeWork", e.Message);
            }
            return false;
        }

        //public UnpackTaskStuInfoMode[] GetUnpackTaskStuInfo(UnpackTaskStuInfoMode[] unpackTaskStuInfoMode, string assignmentID)
        //{
        //    try
        //    {
        //        StringBuilder sbUrl = new StringBuilder();
        //        sbUrl.Append(mHomeworkApiUrl + "/api/TaskAnswer/GetUnpackTaskStuInfo");
        //        sbUrl.Append(string.Format("?assignmentID={0}", assignmentID));

        //        UnpackTaskStuInfo utsiObj = new UnpackTaskStuInfo();
        //        //UnpackTaskStuInfo utsiObj1 = new UnpackTaskStuInfo();
        //        utsiObj.assignmentID = assignmentID;
        //        if(unpackTaskStuInfoMode!=null)
        //            utsiObj.stuInfos = new List<UnpackTaskStuInfoMode>(unpackTaskStuInfoMode);

        //        string strParam = JsonFormatter.JsonSerializeWithNull(utsiObj);
        //        //utsiObj1 = (UnpackTaskStuInfo)JsonFormatter.JsonDeserialize < UnpackTaskStuInfo > (strParam);
        //        string strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
        //        if (string.IsNullOrEmpty(strResult))
        //        {
        //            WriteErrorMessage("GetUnpackTaskStuInfo", "返回值为空。查询条件为：" + sbUrl.ToString());
        //            return null;
        //        }
        //        //XmlDocument doc=new XmlDocument();
        //        //doc.LoadXml(strResult);
        //        //XmlNodeList Data_nodes = doc.GetElementsByTagName("Data");
        //        UnpackTaskStuInfo result = JsonFormatter.JsonDeserialize<UnpackTaskStuInfo>(strResult);
        //        return result.stuInfos.ToArray();
        //    }
        //    catch(Exception e)
        //    {
        //        WriteErrorMessage("GetUnpackTaskStuInfo", e.Message);
        //    }
        //    return null;
        //}

        /// <summary>
        /// 获取某课后作业任务所有对象作答方式
        /// </summary>
        /// <param name="assignmentID"></param>
        /// <returns>返回UnpackTaskStuInfoMode对象，Dotype:0-未选择；1-在线；2-离线</returns>
        public UnpackTaskStuInfoMode[] GetUnpackTaskStuInfo(string assignmentID)
        {
            try
            {
                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mHomeworkApiUrl + "/api/TaskAnswer/GetUnpackTaskStuInfo");
                sbUrl.Append(string.Format("?assignmentID={0}", assignmentID));

                string strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteErrorMessage("GetUnpackTaskStuInfo", "返回值为空。查询条件为：" + sbUrl.ToString());
                    return null;
                }
                //XmlDocument doc=new XmlDocument();
                //doc.LoadXml(strResult);
                //XmlNodeList Data_nodes = doc.GetElementsByTagName("Data");
                OHW_ApiResultM<UnpackTaskStuInfo> result = JsonFormatter.JsonDeserialize<OHW_ApiResultM<UnpackTaskStuInfo>>(strResult);
                return result.Data.stuInfos.ToArray();
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetUnpackTaskStuInfo", e.Message);
            }
            return null;
        }

        #endregion 课后离线作业上传、下载相关

        #region 私有方法，调试使用

        private void WriteDebugInfo(string strMethodName, string strInfo)
        {
#if RELEASE
            return;
#endif
            try
            {
                DirectoryInfo clsPath = new DirectoryInfo(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory));
                DirectoryInfo clsSubPath = clsPath.CreateSubdirectory("RunningLog\\LBD_WebApiInterface\\Api");

                if (clsSubPath.Exists)
                {
                    DateTime clsDate = DateTime.Now;
                    string strPath = clsSubPath.FullName + "\\CloudPreparationI(" + String.Format("{0:yyyy-MM-dd}", clsDate) + ").log";
                    StreamWriter clsWriter = new StreamWriter(strPath, true);
                    clsWriter.WriteLine(String.Format("{0:HH:mm:ss}", clsDate) + " " + strMethodName + ": " + strInfo);
                    clsWriter.Flush();
                    clsWriter.Close();
                }
            }
            catch { }
        }

        private void WriteErrorMessage(string strMethodName, string strErrorMessage)
        {
            try
            {
                DirectoryInfo clsPath = new DirectoryInfo(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory));
                DirectoryInfo clsSubPath = clsPath.CreateSubdirectory("Errlog\\LBD_WebApiInterface\\Api");

                if (clsSubPath.Exists)
                {
                    DateTime clsDate = DateTime.Now;
                    string strPath = clsSubPath.FullName + "\\CloudPreparationI(" + String.Format("{0:yyyy-MM-dd}", clsDate) + ").log";
                    StreamWriter clsWriter = new StreamWriter(strPath, true);
                    clsWriter.WriteLine(String.Format("{0:HH:mm:ss}", clsDate) + " " + strMethodName + ": " + strErrorMessage);
                    clsWriter.Flush();
                    clsWriter.Close();
                }
            }
            catch { }
        }

        private void WriteTrackLog(string strMethodName, string strErrorMessage)
        {
            try
            {
                DirectoryInfo clsPath = new DirectoryInfo(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory));
                DirectoryInfo clsSubPath = clsPath.CreateSubdirectory("Tracklog\\LBD_WebApiInterface");

                if (clsSubPath.Exists)
                {
                    DateTime clsDate = DateTime.Now;
                    string strPath = clsSubPath.FullName + "\\TeachInfoI(" + String.Format("{0:yyyy-MM-dd}", clsDate) + ").log";
                    StreamWriter clsWriter = new StreamWriter(strPath, true);
                    clsWriter.WriteLine("【" + String.Format("{0:HH:mm:ss}", clsDate) + "】 " + strMethodName + ": " + strErrorMessage);
                    clsWriter.Flush();
                    clsWriter.Close();
                }
            }
            catch { }
        }

        /// <summary>
        /// 判断是否需要打印跟踪日志
        /// </summary>
        /// <returns></returns>
        private bool IsShowDebugInfo()
        {
            try
            {
                bool bValue = false;
                string sValue = GetRegistryValue(Microsoft.Win32.Registry.LocalMachine, @"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\L5800TC", "ShowDebugInfo");
                if (sValue == "1") bValue = true;
                return bValue;
            }
            catch (Exception e)
            {
                WriteErrorMessage("IsShowDebugInfo", e.ToString());
                return false;
            }
        }

        /// <summary>
        /// 获取注册表值
        /// </summary>
        /// <param name="root">注册表基项,如 HKEY_LOCAL_MACHINE</param>
        /// <param name="path">键值路径</param>
        /// <param name="paramName">键值数据</param>
        /// <returns></returns>
        private string GetRegistryValue(Microsoft.Win32.RegistryKey root, string path, string paramName)
        {
            try
            {
                string value = string.Empty;
                Microsoft.Win32.RegistryKey rk = root.OpenSubKey(path);
                if (rk != null)
                {
                    value = (string)rk.GetValue(paramName, null);
                }
                return value;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetRegistryValue", e.ToString());
                return null;
            }
        }

        #endregion 私有方法，调试使用
    }
}