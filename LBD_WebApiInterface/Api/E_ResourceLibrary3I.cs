using LBD_WebApiInterface.Models.E_ResourceLibrary;
using LBD_WebApiInterface.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

//using LBD_WebApiInterface.Api;

namespace LBD_WebApiInterface.Api
{
    public class E_ResourceLibrary3I
    {
        private CommandApi mCommandApi;
        private string mApiIPandPort;

        //云平台学科ID
        private string mCloudSubjectID;

        private string mToken;

        //年级信息
        private List<GradeInfoForSearchM> mGradeInfo;

        //子库分类
        private List<ResourceClassLevelTwoForSearchM> mClassLevelTwo;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="strIPandPort">电子资源阅览室地址</param>
        /// <param name="strSubjectID">云平台学科ID</param>
        /// <param name="strToken"></param>
        /// <returns>初始化成功与否</returns>
        public bool Initialize(string strIPandPort, string strSubjectID, string strToken)
        {
            try
            {
                if (string.IsNullOrEmpty(strIPandPort))
                {
                    return false;
                }

                mApiIPandPort = strIPandPort;
                mCloudSubjectID = strSubjectID;
                mToken = strToken;

                mCommandApi = new CommandApi();
                ResourceCategoryM category = GetResourceClass();
                if (category == null || category.subjectList == null)
                {
                    WriteErrorMessage("Initialize", "获取电子资源阅览室资源分类失败，或学科列表为空");
                    return false;
                }

                List<ResourceClassLevelTwoForSearchM> classTwo = category.resourceClassForlevelTwo;
                if (classTwo != null)
                {
                    mClassLevelTwo = new List<ResourceClassLevelTwoForSearchM>();
                    for (int i = 0; i < classTwo.Count; i++)
                    {
                        //只需要多学科资源库
                        if (classTwo[i].FatherID == "1")
                        {
                            mClassLevelTwo.Add(classTwo[i]);
                        }
                    }
                }
                mGradeInfo = category.gradeList;

                if (string.IsNullOrEmpty(mCloudSubjectID))
                {
                    WriteErrorMessage("Initialize", "学科ID为空");
                    return false;
                }
                if (mGradeInfo == null || mClassLevelTwo == null)
                {
                    WriteErrorMessage("Initialize", "年级或分类信息为空");
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                WriteErrorMessage("Initialize", e.Message);
                return false;
            }
        }

        private ResourceCategoryM GetResourceClass()
        {
            try
            {
                string strWholeUrl = "http://" + mApiIPandPort + "/api/Public/GetResourceClass";
                ResourceCategoryM category = CommandApi.CallMethodGet_ERL3<ResourceCategoryM>(strWholeUrl);
                return category;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetResourceClass", e.Message);
                return null;
            }
        }

        /// <summary>
        /// 获取子库类别（例如精品课件、经典案例等）
        /// </summary>
        /// <returns></returns>
        public ResourceClassLevelTwoForSearchM[] GetResourceClassTwo()
        {
            if (mClassLevelTwo != null)
            {
                return mClassLevelTwo.ToArray();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取年级
        /// </summary>
        /// <returns></returns>
        public GradeInfoForSearchM[] GetGrade()
        {
            if (mGradeInfo != null)
            {
                return mGradeInfo.ToArray();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取某一目录下的子目录及资源
        /// </summary>
        /// <returns></returns>
        public FolderAndResourceM GetFolderAndResource(int FolderID)
        {
            try
            {
                string strWholeUrl = "http://" + mApiIPandPort + "/api/Public/GetResourceWithFolderList";
                StringBuilder sbParam = new StringBuilder();
                sbParam.Append("?CategoryID=-1");
                sbParam.Append("&FolderID=" + FolderID);
                if (mCloudSubjectID.StartsWith("S1"))
                {
                    sbParam.Append("&SubjectID=");//大学不能传学科，中小学才需要
                }
                else
                {
                    sbParam.Append("&SubjectID=" + mCloudSubjectID);
                }
                sbParam.Append("&GradeID=");
                sbParam.Append("&Token=" + mToken);
                strWholeUrl = strWholeUrl + sbParam.ToString();
                FolderAndResourceM data = CommandApi.CallMethodGet_ERL3<FolderAndResourceM>(strWholeUrl);
                if (data == null)
                {
                    return null;
                }
                if (data.Resource != null)
                {
                    for (int i = 0; i < data.Resource.Count; i++)
                    {
                        ResourceDetailM detail = GetResourceInfo(data.Resource[i].ID);
                        if (detail != null)
                        {
                            data.Resource[i].IconPath = detail.IconPath;
                        }
                    }
                }
                return data;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetFolderAndResource", e.Message);
                return null;
            }
        }

        /// <summary>
        /// 获取资源详细信息
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <returns></returns>
        public ResourceDetailM GetResourceInfo(string ResourceID)
        {
            try
            {
                string strWholeUrl = "http://" + mApiIPandPort + "/api/Public/GetResourceInfo";
                strWholeUrl = strWholeUrl + "?ResourceID=" + ResourceID;
                ResourceDetailM data = CommandApi.CallMethodGet_ERL3<ResourceDetailM>(strWholeUrl);
                return data;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetResourceInfo", e.Message);
                return null;
            }
        }

        /// <summary>
        /// 根据资源ID获取资源详细信息和所包含文件信息
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <returns></returns>
        public ResourceAndFileM GetResourcewithFilesInfo(string ResourceID)
        {
            try
            {
                string strWholeUrl = "http://" + mApiIPandPort + "/api/Public/GetResourcewithFilesInfo";
                strWholeUrl = strWholeUrl + "?ResourceID=" + ResourceID;
                ResourceAndFileM data = CommandApi.CallMethodGet_ERL3<ResourceAndFileM>(strWholeUrl);
                ModifyFilePath(data.Filelist.ToArray());
                return data;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetResourcewithFilesInfo", e.Message);
                return null;
            }
        }

        private string GetBasicInfo1()
        {
            try
            {
                string strWholeUrl = "http://" + mApiIPandPort + "/api/Public/GetBasicInfo";
                BasicInfo data = CommandApi.CallMethodGet_ERL3<BasicInfo>(strWholeUrl);
                return data.ResourcesUrl;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetBasicInfo1", e.Message);
                return null;
            }
        }

        /// <summary>
        /// 根据文件ID获取文件信息
        /// </summary>
        /// <param name="FileID"></param>
        /// <returns></returns>
        public FilesInfoM GetFileByID(string FileID)
        {
            try
            {
                string strWholeUrl = "http://" + mApiIPandPort + "/api/Public/GetFileByID";
                strWholeUrl = strWholeUrl + "?ID=" + FileID;
                FilesInfoM data = CommandApi.CallMethodGet_ERL3<FilesInfoM>(strWholeUrl);
                ModifyFilePath(data);
                return data;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetFileByID", e.Message);
                return null;
            }
        }

        //修改文件下载路径（电子资源阅览室接口原本提供的下载路径有问题）
        private void ModifyFilePath(params FilesInfoM[] arrFile)
        {
            try
            {
                if (arrFile == null)
                {
                    return;
                }

                //string strPartPath = "http://" + mApiIPandPort + "/api/Download/File?path=";
                string strPartPath = "http://" + mApiIPandPort + "/Download/NormalFile.ashx?Path=";
                for (int i = 0; i < arrFile.Length; i++)
                {
                    if (arrFile[i] == null)
                    {
                        continue;
                    }

                    string strTempPath = arrFile[i].Path;
                    if (string.IsNullOrEmpty(strTempPath))
                    {
                        continue;
                    }

                    string strbasicinfo = GetBasicInfo1();
                    strTempPath = strTempPath.Replace(strbasicinfo, "");
                    //strTempPath = strTempPath.Replace("http://", "");
                    //int index = strTempPath.IndexOf('/');
                    //if (index > 0)
                    //{
                    //    arrFile[i].Path = strPartPath + strTempPath.Substring(index + 1);
                    //}
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("ModifyFilePath", e.Message);
            }
        }

        /// <summary>
        /// （静态接口）获取电子资源阅览室子库分类
        /// </summary>
        public static ResourceClassLevelTwoForSearchM[] Static_GetResourceClassTwo(string strIPandPort)
        {
            try
            {
                string strWholeUrl = "http://" + strIPandPort + "/api/Public/GetResourceClass";
                ResourceCategoryM category = CommandApi.CallMethodGet_ERL3<ResourceCategoryM>(strWholeUrl);
                if (category == null)
                {
                    return null;
                }
                List<ResourceClassLevelTwoForSearchM> classTwo = category.resourceClassForlevelTwo;
                if (classTwo != null)
                {
                    List<ResourceClassLevelTwoForSearchM> classTwo2 = new List<ResourceClassLevelTwoForSearchM>();
                    for (int i = 0; i < classTwo.Count; i++)
                    {
                        //只需要多学科资源库
                        if (classTwo[i].FatherID == "1")
                        {
                            classTwo2.Add(classTwo[i]);
                        }
                    }
                    return classTwo2.ToArray();
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("Static_GetResourceClassTwo", e.Message);
            }
            return null;
        }

        /// <summary>
        /// （静态接口）获取电子资源阅览室子库分类
        /// </summary>
        /// <param name="ErrorCode">输出参数，错误码</param>
        /// <para>0-正常</para>
        /// <para>1-无数据</para>
        /// <para>2-系统状态不可用</para>
        /// <para>3-非法安全码</para>
        /// <para>4-非法参数</para>
        /// <para>5-操作失败</para>
        public static ResourceClassLevelTwoForSearchM[] Static_GetResourceClassTwo(string strIPandPort, out int ErrorCode)
        {
            try
            {
                string strWholeUrl = "http://" + strIPandPort + "/api/Public/GetResourceClass";
                ResourceCategoryM category = CommandApi.CallMethodGet_ERL3<ResourceCategoryM>(strWholeUrl, out ErrorCode);
                if (category == null)
                {
                    return null;
                }
                List<ResourceClassLevelTwoForSearchM> classTwo = category.resourceClassForlevelTwo;
                if (classTwo != null)
                {
                    List<ResourceClassLevelTwoForSearchM> classTwo2 = new List<ResourceClassLevelTwoForSearchM>();
                    for (int i = 0; i < classTwo.Count; i++)
                    {
                        //只需要多学科资源库
                        if (classTwo[i].FatherID == "1")
                        {
                            classTwo2.Add(classTwo[i]);
                        }
                    }
                    return classTwo2.ToArray();
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("Static_GetResourceClassTwo", e.Message);
            }
            ErrorCode = 5;
            return null;
        }

        /// <summary>
        /// 批量添加电子资源馆资料浏览记录
        /// </summary>
        /// <param name="userID">用户id</param>
        /// <param name="userType">用户类型</param>
        /// <param name="userName">用户名称</param>
        /// <param name="schoolID">学校id</param>
        /// <param name="resourceUseLogList"></param>
        /// <returns></returns>
        public bool ReadDetailLogAdd(string userID, int userType, string userName, string schoolID, List<ResourceUseLog> resourceUseLogList)
        {
            try
            {
                string param = JsonConvert.SerializeObject(new
                {
                    User = new { UserID = userID, UserType = userType, Token = mToken, SecureCode = "1", Context = "Web", SchoolID = schoolID, UserName = userName },
                    data = new { list = resourceUseLogList }
                });
                string strWholeUrl = "http://" + mApiIPandPort + "/api/Public/ReadDetailLogAdd";
                string result = CallApiHelper.CallMethodPost(strWholeUrl, param);
                JObject jobResult = JsonConvert.DeserializeObject(result) as JObject;
                string error = jobResult["error"].ToString();
                if (error == "0")
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                WriteErrorMessage("ReadDetailLogAdd", e.Message);
                return false;
            }
        }

        private static void WriteErrorMessage(string strMethodName, string sErrorMessage)
        {
            try
            {
                DirectoryInfo clsPath = new DirectoryInfo(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory));
                DirectoryInfo clsSubPath = clsPath.CreateSubdirectory("Errlog\\LBD_WebApiInterface\\Api");

                if (clsSubPath.Exists)
                {
                    DateTime clsDate = DateTime.Now;
                    string strPath = clsSubPath.FullName + "\\DigitalResource3I(" + String.Format("{0:yyyy-MM-dd}", clsDate) + ").log";
                    StreamWriter clsWriter = new StreamWriter(strPath, true);
                    clsWriter.WriteLine(String.Format("{0:HH:mm:ss}", clsDate) + " " + strMethodName + ": " + sErrorMessage);
                    clsWriter.Close();
                }
            }
            catch { }
        }
    }
}