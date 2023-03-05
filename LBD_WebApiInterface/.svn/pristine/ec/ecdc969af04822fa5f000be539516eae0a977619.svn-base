using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LBD_WebApiInterface.Utility;
using LBD_WebApiInterface.ClassTeach;
using System.IO;
using LBD_WebApiInterface.Models.CloudPlatform;
using LBD_WebApiInterface.Models.BigData;
using System.Xml;
using System.Net;

namespace LBD_WebApiInterface.Api
{
    public class BigDataInfoI
    {
        private CommandApi mCommandApi;
        private CommandWS mCommandWS;
        private string mCloudPlatformIPandPort;
        private string mToken;

        public bool Initialize(string NetTeachIPandPort, string CloudPlatformIPandPort, string Token)
        {
            try
            {
                if (string.IsNullOrEmpty(NetTeachIPandPort) || string.IsNullOrEmpty(CloudPlatformIPandPort))
                {
                    return false;
                }
                mCloudPlatformIPandPort = CloudPlatformIPandPort;
                mToken = Token;

                mCommandApi = new CommandApi();
                mCommandApi.BaseUrl = "http://" + NetTeachIPandPort + "/NetTeachApiF/API/BigDataApi.ashx";

                mCommandWS = new CommandWS();

                string strResult = mCommandApi.CallMethodGet("TestConnection", null);
                if (strResult == "OK")
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
                WriteErrorMessage("Initialize", e.Message);
            }
            return false;
        }

        #region 云平台接口
        /// <summary>
        /// 获取学校信息
        /// </summary>
        /// <param name="SchoolID">学校ID</param>
        /// <returns></returns>
        public SchoolBaseInfoM  CF_GetSchoolInfo(string SchoolID)
        {
            try
            {
                if (string.IsNullOrEmpty(SchoolID))
                {
                    return null;
                }

                string strWebServiceURL = "http://" + mCloudPlatformIPandPort + "/SysMgr/SysSetting/WS/Service_SysSetting.asmx/WS_SysMgr_G_GetSchoolBaseInfo";
                string strParam = "schoolID=" + SchoolID;
                string strReturn = mCommandWS.CallMethodPost(strWebServiceURL, strParam);
                if (string.IsNullOrEmpty(strReturn))
                {
                    return null;
                }
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);
                XmlNodeList list = xmlDoc.GetElementsByTagName("anyType");
                if (list != null && list.Count == 1)
                {
                    SchoolBaseInfoM school = new SchoolBaseInfoM();
                    school.SchoolID = list[0].ChildNodes[0].InnerText;
                    school.SchoolName = list[0].ChildNodes[1].InnerText;
                    school.SchoolCode = list[0].ChildNodes[2].InnerText;
                    school.SchoolLevel = list[0].ChildNodes[3].InnerText;
                    school.SchoolType = list[0].ChildNodes[4].InnerText;
                    school.SchoolState = list[0].ChildNodes[5].InnerText; ;
                    school.CreateTime = list[0].ChildNodes[6].InnerText;
                    return school;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetSchoolInfo", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 获取课程班下所有学生信息
        /// </summary>
        /// <param name="strCourseClassID">课程班ID</param>
        /// <returns></returns>
        public CourseStudentInfoM[] CF_GetCourseClassStudent(string strCourseClassID)
        {
            try
            {
                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append("http://" + mCloudPlatformIPandPort + "/UserMgr/TeachInfoMgr/Api/Service_TeachInfo.ashx");
                sbUrl.Append("?token=" + mToken);
                sbUrl.Append("&method=GetCourseStudents");
                sbUrl.Append(string.Format("&params=|{0}||", strCourseClassID));
                string strReturn = mCommandApi.CallMethodGet(sbUrl.ToString());
                int iFlag = UtilityClass.AnalyseCloudJson(ref strReturn);
                if (iFlag == 3)
                {
                    WriteErrorMessage("GetCourseClassStudent", "Token已失效");
                }
                CourseStudentInfoM[] stuInfo = JsonFormatter.JsonDeserialize<CourseStudentInfoM[]>(strReturn);
                return stuInfo;

                //                 if (iCount > 0)
                //                 {
                //                     int index = -1;
                //                     for (int i = 0; i < iCount; i++)
                //                     {
                //                         arrClassID[i] = stuInfo[i].ClassIDQM;
                //                         arrClassName[i] = stuInfo[i].ClassNameQM;
                // 
                //                         index = arrClassID[i].IndexOf('>');
                //                         if (index > -1)
                //                         {
                //                             arrClassID[i] = arrClassID[i].Substring(index + 1);
                //                         }
                //                         index = arrClassName[i].IndexOf('>');
                //                         if (index > -1)
                //                         {
                //                             arrClassName[i] = arrClassName[i].Substring(index + 1);
                //                         }
                //                     }

            }
            catch (Exception e)
            {
                WriteErrorMessage("GetCourseClassStudent", e.Message);
                return null;
            }
        }
        #endregion

        /// <summary>
        /// 查找课堂基本信息
        /// </summary>
        /// <param name="iLoginClassID">课堂编号</param>
        public LoginClassInfoM GetClassBaseInfo(int iLoginClassID)
        {
            try
            {
                string[] arrParam = new string[1];
                arrParam[0] = iLoginClassID.ToString();
                string strReturn = mCommandApi.CallMethodGet("GetLoginClassInfo", arrParam);
                if (string.IsNullOrEmpty(strReturn))
                {
                    return null;
                }
                LoginClassInfoM classInfo = JsonFormatter.JsonDeserialize<LoginClassInfoM>(strReturn);
                return classInfo;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetClassBaseInfo", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 获取学生签到信息
        /// </summary>
        /// <param name="LoginClassID">课堂编号</param>
        public AttendanceDetailM[] GetAttendanceStudent(int LoginClassID)
        {
            try
            {
                string[] arrParam = new string[1];
                arrParam[0] = LoginClassID.ToString();
                string strReturn = mCommandApi.CallMethodGet("GetAttendStudent", arrParam);
                if (string.IsNullOrEmpty(strReturn))
                {
                    return null;
                }
                AttendanceDetailM[] attendance = JsonFormatter.JsonDeserialize<AttendanceDetailM[]>(strReturn);
                return attendance;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetAttendanceStudent", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 获取学生测试加分、手动课堂加分、出勤信息
        /// </summary>
        /// <param name="LoginClassID">课堂编号</param>
        public ClassStuStudyInfoM[] GetClassStuStudyInfo(int LoginClassID)
        {
            try
            {
                string[] arrParam = new string[1];
                arrParam[0] = LoginClassID.ToString();
                string strReturn = mCommandApi.CallMethodGet("GetClassStuStudyInfo", arrParam);
                if (string.IsNullOrEmpty(strReturn))
                {
                    return null;
                }
                ClassStuStudyInfoM[] attendance = JsonFormatter.JsonDeserialize<ClassStuStudyInfoM[]>(strReturn);
                return attendance;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetAttendanceStudent", e.Message);
            }
            return null;
        }
        /// <summary>
        /// 查找资料信息（一次性获取课堂下所有的，仅包含使用过的）
        /// </summary>
        /// <param name="LoginClassID">课堂编号</param>
        /// <returns></returns>
        public BD_ResourceInfoM[] GetResourceInfo(int LoginClassID)
        {
            BD_ResourceInfoM[] arrResource = null;
            try
            {
                string[] arrParam = new string[1];
                arrParam[0] = LoginClassID.ToString();
                string strReturn = mCommandApi.CallMethodGet("GetResourceInfo", arrParam);
                if (string.IsNullOrEmpty(strReturn))
                {
                    arrResource = new BD_ResourceInfoM[0];
                    return arrResource;
                }
                arrResource = JsonFormatter.JsonDeserialize<BD_ResourceInfoM[]>(strReturn);
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetResourceInfo", e.Message);
            }
            if (arrResource == null)
            {
                arrResource = new BD_ResourceInfoM[0];
            }
            return arrResource;
        }

        /// <summary>
        /// 获取一堂课内所有进入的教学模式信息（包含子模式信息）
        /// </summary>
        /// <param name="LoginClassID">课堂编号</param>
        /// <returns></returns>
        public BD_LCTeachModeInfoM[] GetLoginModeInfo(int LoginClassID)
        {
            BD_LCTeachModeInfoM[] arrMode = null;
            try
            {
                string[] arrParam = new string[1];
                arrParam[0] = LoginClassID.ToString();
                string strReturn = mCommandApi.CallMethodGet("GetLoginModeInfo", arrParam);
                if (string.IsNullOrEmpty(strReturn) ==false)
                {
                    arrMode = JsonFormatter.JsonDeserialize<BD_LCTeachModeInfoM[]>(strReturn);
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetLoginModuleInfo", e.Message);
            }
            if (arrMode == null)
            {
                arrMode = new BD_LCTeachModeInfoM[0];
            }
            return arrMode;
        }

        /// <summary>
        /// 获取教师使用工具的信息
        /// </summary>
        /// <param name="LoginClassID"></param>
        /// <param name="LoginTeachModeID"></param>
        /// <returns></returns>
        public LCModuleOperM[] GetTeacherToolInfo(int LoginClassID, long LoginTeachModeID)
        {
            LCModuleOperM[] arrOper = null;
            try
            {
                string[] arrParam = new string[1];
                arrParam[0] = LoginClassID.ToString();
                arrParam[1] = LoginTeachModeID.ToString();
                string strReturn = mCommandApi.CallMethodGet("GetToolInfo", arrParam);
                if (string.IsNullOrEmpty(strReturn) == false)
                {
                    arrOper = JsonFormatter.JsonDeserialize<LCModuleOperM[]>(strReturn);
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetTeacherToolInfo", e.Message);
            }
            if (arrOper == null)
            {
                arrOper = new LCModuleOperM[0];
            }
            return arrOper;
        }

        /// <summary>
        /// 获取一堂课内所有的训练信息（包括训练题目、学生答案、学生作答结果等信息）
        /// </summary>
        /// <param name="LoginClassID"></param>
        /// <returns></returns>
        public BD_TrainInfoM[] GetLCTrainInfo(int LoginClassID)
        {
            BD_TrainInfoM[] arrTrain = null;
            try
            {
                string[] arrParam = new string[1];
                arrParam[0] = LoginClassID.ToString();
                string strReturn = mCommandApi.CallMethodGet("GetLCTrainInfo", arrParam);
                if (string.IsNullOrEmpty(strReturn) == false)
                {
                    arrTrain = JsonFormatter.JsonDeserialize<BD_TrainInfoM[]>(strReturn);
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetTeacherToolInfo", e.Message);
            }
            if (arrTrain == null)
            {
                arrTrain = new BD_TrainInfoM[0];
            }
            return arrTrain;
        }

        //获取一堂课内教室控制下的操作学生结果,一次操作算一次结果集
        public BD_StuResultByTeachInfoM[] GetStuResultByTeachInfo(int LoginClassID, string OperCode)
            {
            BD_StuResultByTeachInfoM[] arrStuResult = null;
            try
            {
                string[] arrparam = new string[2];
                arrparam[0] = LoginClassID.ToString();
                arrparam[1] = OperCode;
                string strReturn = mCommandApi.CallMethodGet("GetStuOperResultByTeach", arrparam);
                if (string.IsNullOrEmpty(strReturn)==false)
                {
                    arrStuResult = JsonFormatter.JsonDeserialize<BD_StuResultByTeachInfoM[]>(strReturn);
                }
            }
            catch(Exception e)
            { WriteErrorMessage("GetStuResultByTeachInfo", e.Message); }
            //if (arrStuResult == null)
            //{
            //    arrStuResult = new BD_StuResultByTeachInfoM[0];
            //}
            return arrStuResult;
            }
        /// <summary>
        /// 获取一堂课中所有学生的笔记信息
        /// </summary>
        /// <param name="LoginClassID"></param>
        /// <returns></returns>
        public BD_UserNoteInfoM[] GetUserNodeInfo(int LoginClassID)
        {
            BD_UserNoteInfoM[] arrNote = null;
            try
            {
                string[] arrParam = new string[1];
                arrParam[0] = LoginClassID.ToString();
                string strReturn = mCommandApi.CallMethodGet("GetLCUserNote", arrParam);
                if (string.IsNullOrEmpty(strReturn) == false)
                {
                    arrNote = JsonFormatter.JsonDeserialize<BD_UserNoteInfoM[]>(strReturn);
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetUserNodeInfo", e.Message);
            }
            if (arrNote == null)
            {
                arrNote = new BD_UserNoteInfoM[0];
            }
            return arrNote;
        }

        /// <summary>
        /// 获取一堂课内重读、重写、重听的知识点
        /// </summary>
        /// <param name="LoginClassID">课堂编号</param>
        /// <returns></returns>
        public BD_ImportantZSDM[] GetRepetitionZSD(int LoginClassID)
        {
            BD_ImportantZSDM[] arrZSD = null;
            try
            {
                string[] arrParam = new string[1];
                arrParam[0] = LoginClassID.ToString();
                string strReturn = mCommandApi.CallMethodGet("GetRepetitionZSD", arrParam);
                if (string.IsNullOrEmpty(strReturn) == false)
                {
                    arrZSD = JsonFormatter.JsonDeserialize<BD_ImportantZSDM[]>(strReturn);
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetImportantZSD", e.Message);
            }
            if (arrZSD == null)
            {
                arrZSD = new BD_ImportantZSDM[0];
            }

            return arrZSD;
        }

        //获取资料学习认知数据（认知大数据）
        public string  GetResourceStudyPerceive(int iLoginClassID)
        {
            try
            {
                string[] arrParam = new string[1];
                arrParam[0] = iLoginClassID.ToString();
                string strReturn = mCommandApi.CallMethodGet("GetResourceStudyPerceive", arrParam);
                //if (string.IsNullOrEmpty(strReturn))
                //{
                //    return null;
                //}
                //LoginClassInfoM classInfo = JsonFormatter.JsonDeserialize<LoginClassInfoM>(strReturn);
                //return classInfo;
                return strReturn;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetResourceStudyPerceive", e.Message);
                return null;
            }
            //return null;
        }
        //获取学习成绩认知数据
        public string GetStudyResultPerceive(int iLoginClassID)
        {
            try
            {
                string[] arrParam = new string[1];
                arrParam[0] = iLoginClassID.ToString();
                string strReturn = mCommandApi.CallMethodGet("GetStudyResultPerceive", arrParam);
                //if (string.IsNullOrEmpty(strReturn))
                //{
                //    return null;
                //}
                //LoginClassInfoM classInfo = JsonFormatter.JsonDeserialize<LoginClassInfoM>(strReturn);
                //return classInfo;
                return strReturn;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetStudyResultPerceive", e.Message);
                return null;
            }
            //return null;
        }
        #region 通过用户ID获取用户名称
        public static  string GetUserNameWithUserID(string UserID, string basicPlatServerAddr)
        {
            try
            {
                if (string.IsNullOrEmpty(UserID))
                {
                    return "";
                }
                string s1 = GetMd5Hash(UserID);
                string url = "http://" + basicPlatServerAddr + "/UserMgr/UserGroup/WS/Service_UserGroupMgr.asmx/WS_UserMgr_G_SearchUserInfo?Token=" + s1 + "&UserIDs=" + UserID + "&UserName=";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.Timeout = 8000;
                request.ContentType = "text/xml";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                string resultStr = sr.ReadToEnd();
                sr.Close();
                responseStream.Close();
                if (resultStr == null) return null;
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(resultStr);
                if (doc.LastChild.ChildNodes == null && doc.LastChild.ChildNodes.Count <= 0) return "";
                string sName = "";
                foreach (XmlNode item in doc.LastChild.LastChild.ChildNodes)
                {
                    if (item.Name.Equals("UserName"))
                    {
                        sName = item.InnerText;
                        break;
                    }
                }
                return sName;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        #endregion
        #region 返回字符串的MD5值
        /// <summary>
        /// 返回字符串的MD5值
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns></returns>
        public static string GetMd5Hash(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] data = md5.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
        #endregion
        private void WriteErrorMessage(string strMethodName, string strErrorMessage)
        {
            try
            {
                DirectoryInfo clsPath = new DirectoryInfo(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory));
                DirectoryInfo clsSubPath = clsPath.CreateSubdirectory("Errlog\\LBD_WebApiInterface\\Api");

                if (clsSubPath.Exists)
                {
                    DateTime clsDate = DateTime.Now;
                    string strPath = clsSubPath.FullName + "\\BigDataInfoI(" + String.Format("{0:yyyy-MM-dd}", clsDate) + ").log";
                    StreamWriter clsWriter = new StreamWriter(strPath, true);
                    clsWriter.WriteLine(String.Format("{0:HH:mm:ss}", clsDate) + " " + strMethodName + ": " + strErrorMessage);
                    clsWriter.Flush();
                    clsWriter.Close();
                }
            }
            catch { }
        }
    }
}
