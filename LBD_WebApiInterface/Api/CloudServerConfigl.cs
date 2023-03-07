using lancoo.cp.basic.sysbaseclass;
using LBD_WebApiInterface.Models;
using LBD_WebApiInterface.Models.CloudPreparation;
using LBD_WebApiInterface.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace LBD_WebApiInterface.Api
{
    /// <summary>
    ///  接口维护人：李萌芽
    /// 时间：2018-12-11
    /// </summary>
    /// <returns></returns>
    public class CloudServerConfigl
    {
        #region 私有变量

        private string mTeachCenterApiUrl;
        private string mResourcesApiUrl;
        private string mSuiTtestApiUrl;

        #endregion 私有变量

        #region 初始化

        /// <summary>
        /// 初始化S10
        /// </summary>
        /// <param name="CloudPreparationIP">云备课WebApi的IP</param>
        /// <param name="CloudPreparationPort">云备课WebApi的Port</param>
        /// <returns>true-初始化成功，false-初始化失败</returns>

        #endregion 初始化

        public bool Initialize(string CloudPreparationIP, string CloudPreparationPort)
        {
            try
            {
                WriteTrackLog("Initialize【Start】", "CloudPreparationIP=" + CloudPreparationIP + ",CloudPreparationPort=" + CloudPreparationPort);
                if (string.IsNullOrEmpty(CloudPreparationIP) || string.IsNullOrEmpty(CloudPreparationPort))
                {
                    return false;
                }

                mTeachCenterApiUrl = "http://" + CloudPreparationIP + ":" + CloudPreparationPort;
                WriteTrackLog("Initialize", "mTeachCenterApiUrl=" + mTeachCenterApiUrl);
                //mIntelCoursewareI = new IntelCoursewareI();
                // mCloudPreparationSrvInfo = GetCPSrvInfo();
                return true;
            }
            catch (Exception e)
            {
                WriteErrorMessage("Initialize", e.Message);
            }
            return false;
        }

        #region 初始化

        /// <summary>
        /// 初始化A00
        /// </summary>
        /// <param name="ResourcesIP">数字资源库WebApi的IP</param>
        /// <param name="ResourcesPort">数字资源库WebApi的Port</param>
        /// <returns>true-初始化成功，false-初始化失败</returns>

        #endregion 初始化

        public bool InitializeResources(string ResourcesIP, string ResourcesPort)
        {
            try
            {
                WriteTrackLog("Initialize【Start】", "ResourcesIP=" + ResourcesIP + ",ResourcesPort=" + ResourcesPort);
                if (string.IsNullOrEmpty(ResourcesIP) || string.IsNullOrEmpty(ResourcesPort))
                {
                    return false;
                }

                mResourcesApiUrl = "http://" + ResourcesIP + ":" + ResourcesPort;
                WriteTrackLog("Initialize", "mResourcesApiUrl=" + mResourcesApiUrl);
                //mIntelCoursewareI = new IntelCoursewareI();
                // mCloudPreparationSrvInfo = GetCPSrvInfo();
                return true;
            }
            catch (Exception e)
            {
                WriteErrorMessage("Initialize", e.Message);
            }
            return false;
        }

        #region 初始化

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="SuiTtestIP">随堂测试WebApi的IP</param>
        /// <param name="SuiTtestPort">随堂测试WebApi的Port</param>
        /// <returns>true-初始化成功，false-初始化失败</returns>

        #endregion 初始化

        public bool InitializeSuiTtest(string SuiTtestIP, string SuiTtestPort)
        {
            try
            {
                WriteTrackLog("Initialize【Start】", "SuiTtestIP=" + SuiTtestIP + ",SuiTtestPort=" + SuiTtestPort);
                if (string.IsNullOrEmpty(SuiTtestIP) || string.IsNullOrEmpty(SuiTtestPort))
                {
                    return false;
                }

                mSuiTtestApiUrl = "http://" + SuiTtestIP + ":" + SuiTtestPort;
                WriteTrackLog("Initialize", "mSuiTtestApiUrl=" + mSuiTtestApiUrl);
                //mIntelCoursewareI = new IntelCoursewareI();
                // mCloudPreparationSrvInfo = GetCPSrvInfo();
                return true;
            }
            catch (Exception e)
            {
                WriteErrorMessage("Initialize", e.Message);
            }
            return false;
        }

        #region 获取新版数字化资源库字库信息

        /// <summary>
        ///  获取新版数字化资源库字库信息
        /// </summary>
        /// <returns></returns>
        ///
        public List<BLibraryM> GetBLibraryM(string strLibCode, string strLibName, string strCourseCode)
        {
            try
            {
                string webapiAddr = mResourcesApiUrl + "/SearchBasicInfo.asmx/WS_Basic_GetLibraryInfo?strLibCode=" + strLibCode + "&strLibName=" + strLibName + "&strCourseCode=" + strCourseCode;

                string result = CallApiHelper.CallMethod_Get(webapiAddr);
                if (string.IsNullOrEmpty(result))
                    return null;
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(result);
                XmlNodeList nodeList = xd.GetElementsByTagName("BLibrary");
                if (nodeList == null || nodeList.Count <= 0)
                    return null;
                List<BLibraryM> list = new List<BLibraryM>();
                foreach (XmlNode node in nodeList)
                {
                    BLibraryM bm = new BLibraryM();
                    bm.LibrarySequence = node.ChildNodes[0].InnerText;
                    bm.LibraryCode = node.ChildNodes[1].InnerText;
                    bm.LibraryName = node.ChildNodes[2].InnerText;
                    bm.CourseCode = node.ChildNodes[3].InnerText;
                    list.Add(bm);
                }
                return list;
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorMessage("GetBLibraryM", e.ToString());
                return null;
            }
        }

        #endregion 获取新版数字化资源库字库信息

        #region 获取新版数字化资源库字库信息详情

        /// <summary>
        ///  获取新版数字化资源库字库信息
        /// <param name="OptionalParams">参数的格式:学科}学习阶段}水平级别}子库}资源格式}题型（以"}"隔开）  C}F}}I}1|2}</param>
        /// <param name="PageIndex">当前页：取值为大于等于1的整数，默认值1</param>
        /// <param name="PageSize ">每页返回的数量：取值为大于0的整数，默认值10</param>
        /// <param name="FilterContext ">要查询的关键字P</param>
        /// </summary>
        /// <returns></returns>
        ///
        public LgdigitalRes GetBLibraryMCatalogSearch(string OptionalParams, string ItemText, string PageIndex, string PageSize, string FilterContext)
        {
            try
            {
                string webapiAddr = null;
                if (FilterContext == null || FilterContext.Equals(""))
                {
                    webapiAddr = mResourcesApiUrl + "/SearchResource.asmx/WS_Search_CatalogSearch?OptionalParams=" + OptionalParams + "&ItemText=" + ItemText + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
                }
                else
                {
                    webapiAddr = mResourcesApiUrl + "/SearchResource.asmx/WS_Search_KeywordSearch?OptionalParams=" + OptionalParams + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize + "&FilterContext=" + FilterContext;
                }
                string result = CallApiHelper.CallMethod_Get(webapiAddr);
                if (string.IsNullOrEmpty(result))
                    return null;
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(result);
                XmlNodeList nodeList = xd.GetElementsByTagName("LgdigitalRes");
                if (nodeList == null || nodeList.Count <= 0)
                    return null;
                LgdigitalRes lgdigitalRes = new LgdigitalRes();
                List<LgdigitalRes.ListRes> listRes = new List<LgdigitalRes.ListRes>();
                //List<BLibraryM> list = new List<BLibraryM>();
                lgdigitalRes.TotalNum = nodeList[0].ChildNodes[0].InnerText;
                lgdigitalRes.PageSize = nodeList[0].ChildNodes[1].InnerText;
                lgdigitalRes.PageIndex = nodeList[0].ChildNodes[2].InnerText;
                // lgdigitalRes.listRes = nodeList[0].ChildNodes[3];
                int count = nodeList[0].ChildNodes.Count - 1;
                if (count >= 3)
                {
                    for (int i = 3; i <= count; i++)
                    {
                        LgdigitalRes.ListRes listRes001 = new LgdigitalRes.ListRes();
                        listRes001.ResCode = nodeList[0].ChildNodes[i].Attributes[0].ChildNodes[0].InnerText;
                        listRes001.RowNumber = nodeList[0].ChildNodes[i].ChildNodes[0].InnerText;
                        listRes001.ResName = nodeList[0].ChildNodes[i].ChildNodes[1].InnerText;
                        listRes001.ResType = nodeList[0].ChildNodes[i].ChildNodes[2].InnerText;
                        listRes001.StoreDate = nodeList[0].ChildNodes[i].ChildNodes[3].InnerText;
                        listRes001.ThemeCode = nodeList[0].ChildNodes[i].ChildNodes[4].InnerText;
                        listRes001.ThemeText = nodeList[0].ChildNodes[i].ChildNodes[5].InnerText;
                        listRes001.ImporKnCode = nodeList[0].ChildNodes[i].ChildNodes[6].InnerText;
                        listRes001.ImporKnText = nodeList[0].ChildNodes[i].ChildNodes[7].InnerText;
                        listRes001.MainKnCode = nodeList[0].ChildNodes[i].ChildNodes[8].InnerText;
                        listRes001.MainKnText = nodeList[0].ChildNodes[i].ChildNodes[9].InnerText;
                        listRes001.UnitNum = nodeList[0].ChildNodes[i].ChildNodes[10].InnerText;
                        listRes001.ResSize = nodeList[0].ChildNodes[i].ChildNodes[11].InnerText;
                        listRes001.ResClass = nodeList[0].ChildNodes[i].ChildNodes[12].InnerText;
                        listRes001.ResLevel = nodeList[0].ChildNodes[i].ChildNodes[13].InnerText;
                        listRes001.MD5Code = nodeList[0].ChildNodes[i].ChildNodes[14].InnerText;
                        listRes001.LibCode = nodeList[0].ChildNodes[i].ChildNodes[15].InnerText;
                        listRes001.InstitutionalUnit = nodeList[0].ChildNodes[i].ChildNodes[16].InnerText;
                        listRes001.ResFtpPath = nodeList[0].ChildNodes[i].ChildNodes[17].InnerText;
                        listRes001.IsExsitMedia = nodeList[0].ChildNodes[i].ChildNodes[18].InnerText;
                        listRes001.TextFileContent = nodeList[0].ChildNodes[i].ChildNodes[19].InnerText;
                        listRes001.IsDownload = nodeList[0].ChildNodes[i].ChildNodes[20].InnerText;
                        listRes001.DurationLength = nodeList[0].ChildNodes[i].ChildNodes[21].InnerText;
                        listRes001.ResLength = nodeList[0].ChildNodes[i].ChildNodes[22].InnerText;
                        listRes001.ApplyNum = nodeList[0].ChildNodes[i].ChildNodes[23].InnerText;
                        listRes001.Creator = nodeList[0].ChildNodes[i].ChildNodes[24].InnerText;
                        listRes001.CreatorId = nodeList[0].ChildNodes[i].ChildNodes[25].InnerText;
                        listRes001.ThemeKeywordCode = nodeList[0].ChildNodes[i].ChildNodes[26].InnerText;
                        listRes001.ThemeKeywordText = nodeList[0].ChildNodes[i].ChildNodes[27].InnerText;
                        listRes001.UpperKnlgCode = nodeList[0].ChildNodes[i].ChildNodes[28].InnerText;
                        listRes001.UpperKnlgText = nodeList[0].ChildNodes[i].ChildNodes[29].InnerText;
                        listRes001.OtherKnlgCode = nodeList[0].ChildNodes[i].ChildNodes[30].InnerText;

                        if (nodeList[0].ChildNodes[i].ChildNodes[34] != null)
                        {
                            if (string.IsNullOrEmpty(nodeList[0].ChildNodes[i].ChildNodes[34].InnerText) == false)
                                listRes001.ResCode = nodeList[0].ChildNodes[i].ChildNodes[34].InnerText;
                        }

                        listRes.Add(listRes001);
                    }
                    lgdigitalRes.listRes = listRes;
                }
                WriteTrackLog("GetBLibraryMCatalogSearch", "GetBLibraryMCatalogSearch=" + webapiAddr);
                string strlgdigitalRes = JsonFormatter.JsonSerialize(lgdigitalRes);
                WriteTrackLog("GetBLibraryMCatalogSearch", "GetBLibraryMCatalogSearch=" + strlgdigitalRes);
                return lgdigitalRes;
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorMessage("GetBLibraryMCatalogSearch", e.ToString());
                return null;
            }
        }

        #endregion 获取新版数字化资源库字库信息详情

        #region 获取新版数字化资源库服务器信息

        /// <summary>
        ///  获取新版数字化资源库服务器信息
        /// </summary>
        /// <returns></returns>
        ///
        public ServerConf GetServerAddressConf(string serverID)
        {
            try
            {
                string webapiAddr = mResourcesApiUrl + "/SearchStatisticalInfo.asmx/WS_Search_GetServerAddressConf?serverID=" + serverID;
                WriteTrackLog("webapiAddr", "webapiAddr=" + webapiAddr);
                string result = CallApiHelper.CallMethod_Get(webapiAddr);
                if (string.IsNullOrEmpty(result))
                    return null;
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(result);
                XmlNodeList nodeList = xd.GetElementsByTagName("Server");
                if (nodeList == null || nodeList.Count <= 0)
                    return null;
                ServerConf serverConf = new ServerConf();
                List<Server> list = new List<Server>();
                foreach (XmlNode node in nodeList)
                {
                    Server bm = new Server();
                    bm.sModID = node.ChildNodes[0].InnerText;
                    bm.sServerType = node.ChildNodes[1].InnerText;
                    bm.sServerName = node.ChildNodes[2].InnerText;
                    bm.sIP = node.ChildNodes[3].InnerText;
                    bm.sPort = node.ChildNodes[4].InnerText;
                    bm.sUserName = node.ChildNodes[5].InnerText;
                    bm.sPWD = node.ChildNodes[6].InnerText;

                    list.Add(bm);
                }
                serverConf.server = list;
                return serverConf;
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorMessage("GetServerAddressConf", e.ToString());
                return null;
            }
        }

        #endregion 获取新版数字化资源库服务器信息

        #region 关于数字化资源共享，需要调用共享的接口获取资源，通过调用数字化资源get方法，超时时间120s

        /// <summary>
        ///  调用数字化资源共享
        /// 20190423
        /// </summary>
        /// <returns></returns>
        ///
        public int GetSharedLibraryM(string resId)
        {
            try
            {
                string webapiAddr = mResourcesApiUrl + "/api/download?ResId=" + resId;
                WriteTrackLog("webapiAddr", "webapiAddr=" + webapiAddr);
                string result = CallApiHelper.CallMethod_Get1(webapiAddr);
                if (string.IsNullOrEmpty(result))
                    return 2;//调用出错

                bool s = JsonFormatter.JsonDeserialize<bool>(result);

                WriteTrackLog("GetSharedLibraryM", "GetSharedLibraryM=" + s);
                if (s)
                { return 1; }//下载成功
                else
                { return 0; }//下载失败
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorMessage("GetBLibraryM", e.ToString());
                return 2;//调用出错
            }
        }

        #endregion 关于数字化资源共享，需要调用共享的接口获取资源，通过调用数字化资源get方法，超时时间120s

        #region 获取随堂测试试卷信息

        /// <summary>
        ///  获取随堂测试试卷信息
        /// </summary>
        /// <returns></returns>
        ///
        public List<PaperResultDataGetParams> GetPaperResultData(string UserID, int PaperType, string BelongMonth, int QuestionType, string SearchContext, int PageIndex, int PageCount, string SubjectID)
        {
            try
            {
                string TimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mSuiTtestApiUrl + "/api/public/v1/ktcs/paper");
                sbUrl.Append("?UserID=" + UserID);
                sbUrl.Append("&PaperType=" + PaperType);
                sbUrl.Append("&BelongMonth=" + BelongMonth);
                sbUrl.Append("&QuestionType=" + QuestionType);
                sbUrl.Append("&SearchContext=" + SearchContext);
                sbUrl.Append("&PageIndex=" + PageIndex);
                sbUrl.Append("&PageCount=" + PageCount);
                sbUrl.Append("&SubjectID=" + SubjectID);
                sbUrl.Append("&TimeStamp=" + TimeStamp);
                sbUrl.Append("&Key=" + CP_MD5Helper.GetMd5Hash("TC_ktcs_" + UserID + TimeStamp));//需要添加前缀TC_ktcs_
                WriteTrackLog("mSuiTtestApiUrl", "mSuiTtestApiUrl = " + sbUrl);

                string result = CallApiHelper.CallMethod_Get(sbUrl.ToString());
                if (string.IsNullOrEmpty(result))
                    return null;
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(result);
                XmlNodeList nodeList = xd.GetElementsByTagName("templetParam3OfPaperResultGetParamsvPVjqod3");
                if (nodeList == null || nodeList.Count <= 0)
                    return null;
                if (nodeList[0].ChildNodes[0].InnerText != "0")
                {
                    WriteErrorMessage("GetPaperResultData", "查询失败，ErrorFlag=" + nodeList[0].ChildNodes[0].InnerText + "，Message=" + nodeList[0].ChildNodes[2].InnerText);

                    return null;
                }

                XmlNodeList nodeList1 = xd.GetElementsByTagName("d2p1:PaperResultDataGetParams");
                List<PaperResultDataGetParams> list = new List<PaperResultDataGetParams>();
                foreach (XmlNode node in nodeList1)
                {
                    PaperResultDataGetParams bm = new PaperResultDataGetParams();
                    bm.BigItemQuantity = node.ChildNodes[0].InnerText;
                    bm.FullScore = node.ChildNodes[1].InnerText;
                    bm.IsHaveAudio = node.ChildNodes[2].InnerText;
                    bm.ItemQuantity = node.ChildNodes[3].InnerText;
                    bm.PaperID = node.ChildNodes[4].InnerText;
                    bm.PaperName = node.ChildNodes[5].InnerText;
                    bm.PaperSource = node.ChildNodes[6].InnerText;
                    bm.TestTimeLong = node.ChildNodes[7].InnerText;
                    bm.UpTime = node.ChildNodes[8].InnerText;
                    bm.UpTimeStr = node.ChildNodes[9].InnerText;
                    bm.UseCount = node.ChildNodes[10].InnerText;

                    list.Add(bm);
                }
                return list;
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorMessage("GetPaperResultData", e.ToString());
                return null;
            }
        }

        #endregion 获取随堂测试试卷信息

        #region 教学云主页添加学科网信息

        /// <summary>
        /// 教学云主页添加学科网信息
        ///   <param name="TeacherID">教师ID</param>
        /// <param name="SchoolID">学校ID</param>
        /// <param name="SubjectID">学科ID</param>
        /// <param name="Term">学期ID</param>
        ///   <param name="Icons">图标路径</param>
        /// <param name="ResAddress">学科网网站</param>
        /// <param name="AddressName">学科网名称</param>
        /// <param name="AddressUsername">用户名</param>
        ///  <param name="AddressPassword">密码</param>
        /// </summary>
        /// <returns></returns>
        ///
        public string addSubjectNetwork(string bSubjectID, string strTeacherID, string strID, string Term, string SchoolID, string Icons, string ResAddress, string AddressName, string AddressUsername, string AddressPassword)
        {
            try
            {
                //short setItemID = TeachSetI.GetSetItemID(eSetItem);
                string strTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string Key = CP_MD5Helper.GetMd5Hash(strTimeStamp + strTeacherID);
                string param = JsonFormatter.JsonSerialize(new
                {
                    ID = strID,
                    Term = Term,
                    SubjectID = bSubjectID,
                    TeacherID = strTeacherID,
                    SchoolID = SchoolID,
                    //Icon = ImageConvertString(Icons),
                    Icon = Icons,
                    ResAddress = ResAddress,
                    AddressName = AddressName,
                    AddressUsername = AddressUsername,
                    AddressPassword = AddressPassword,
                    TimeStamp = strTimeStamp,
                    Key = Key
                });
                WriteTrackLog("addSubjectNetwork", "addSubjectNetwork = " + param);
                string strReturn = CallApiHelper.CallMethodPost(mTeachCenterApiUrl + "/CouldPreparation/SubjectNetwork/addSubjectNetwork", param);
                if (string.IsNullOrEmpty(strReturn))
                    return "0";
                else
                {
                    CLP_ApiResultM<string> result = JsonFormatter.JsonDeserialize<CLP_ApiResultM<string>>(strReturn);
                    if (result.ErrorFlag == 0)
                        return result.Data;
                    else
                    {
                        WriteErrorMessage("addSubjectNetwork", strReturn);
                        return "0";
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("addSubjectNetwork", e.Message);
            }

            return "0";
        }

        #endregion 教学云主页添加学科网信息

        #region 删除学科网信息

        /// <summary>
        /// 删除学科网信息
        ///  <param name="ID">学科网ID</param>
        /// </summary>
        /// <returns></returns>
        ///
        public string deleteSubjectNetwork(string ID)
        {
            try
            {
                //short setItemID = TeachSetI.GetSetItemID(eSetItem);
                string strTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string Key = CP_MD5Helper.GetMd5Hash(strTimeStamp + ID);
                string param = JsonFormatter.JsonSerialize(new
                {
                    ID = ID,
                    TimeStamp = strTimeStamp,
                    Key = Key
                });

                string strReturn = CallApiHelper.CallMethodPost(mTeachCenterApiUrl + "/CouldPreparation/SubjectNetwork/deleteSubjectNetwork", param);
                if (string.IsNullOrEmpty(strReturn))
                    return "0";
                else
                {
                    CLP_ApiResultM<string> result = JsonFormatter.JsonDeserialize<CLP_ApiResultM<string>>(strReturn);
                    if (result.ErrorFlag == 0)
                        return result.Data;
                    else
                    {
                        WriteErrorMessage("deleteSubjectNetwork", strReturn);
                        return "0";
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("deleteSubjectNetwork", e.Message);
            }

            return "0";
        }

        #endregion 删除学科网信息

        #region 根据教师id，学科，学期，学校id信息获取学科网信息列表

        /// <summary>
        /// 根据教师id，学科，学期，学校id信息获取学科网信息列表
        /// </summary>
        /// <param name="sTeachingProgramID"></param>
        /// <returns></returns>
        public SubjectNetworkM[] getSubjectNetworkByTeacherID(string TeacherID, string SchoolID, string SubjectID, string Term)
        {
            // int SumCount = 0;
            try
            {
                string strTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mTeachCenterApiUrl + "/CouldPreparation/SubjectNetwork/querySubjectNetworkByTeacherID");
                sbUrl.Append("?TeacherID=" + TeacherID);
                sbUrl.Append("&SchoolID=" + SchoolID);
                sbUrl.Append("&SubjectID=" + SubjectID);
                sbUrl.Append("&Term=" + Term);
                sbUrl.Append("&TimeStamp=" + strTimeStamp);
                sbUrl.Append("&Key=" + CP_MD5Helper.GetMd5Hash(strTimeStamp + TeacherID));

                WriteTrackLog("getSubjectNetworkByTeacherID", "sbUrl = " + sbUrl);
                string strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteErrorMessage("GetTeachingProgramByID", "返回值为空。查询条件为：" + sbUrl.ToString());

                    return null;
                }

                CLP_ApiResultM<SubjectNetworkM[]> result = JsonFormatter.JsonDeserialize<CLP_ApiResultM<SubjectNetworkM[]>>(strResult);
                if (result.ErrorFlag != 0)
                {
                    WriteErrorMessage("getSubjectNetworkByTeacherID", "查询失败，ErrorFlag=" + result.ErrorFlag + "，Message=" + result.Message);

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
                WriteErrorMessage("getSubjectNetworkByTeacherID", e.Message);
            }
            return null;
        }

        #endregion 根据教师id，学科，学期，学校id信息获取学科网信息列表

        #region 添加课时对应的教学方案信息

        /// <summary>
        /// 添加课时对应的教学方案信息
        ///  /// <param name="CourseTime">课时，格式2017-08-20 20:18:15</param>
        /// <param name="CourseName">课时名称</param>
        /// <param name="TeachProgramID">教学方案ID</param>
        /// </summary>
        /// <returns></returns>
        ///
        public string addCourseTime(string CourseTime, string CourseName, string strID, string TeachProgramID)
        {
            try
            {
                //short setItemID = TeachSetI.GetSetItemID(eSetItem);
                string strTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string Key = CP_MD5Helper.GetMd5Hash(strTimeStamp + TeachProgramID);
                string param = JsonFormatter.JsonSerialize(new
                {
                    ID = strID,
                    CourseTime = CourseTime,
                    CourseName = CourseName,
                    TeachProgramID = TeachProgramID,
                    TimeStamp = strTimeStamp,
                    Key = Key
                });
                WriteTrackLog("addCourseTime", "param = " + param);
                string strReturn = CallApiHelper.CallMethodPost(mTeachCenterApiUrl + "/CouldPreparation/CourseTime/addCourseTime", param);
                if (string.IsNullOrEmpty(strReturn))
                    return "0";
                else
                {
                    CLP_ApiResultM<string> result = JsonFormatter.JsonDeserialize<CLP_ApiResultM<string>>(strReturn);
                    if (result.ErrorFlag == 0)
                        return result.Data;
                    else
                    {
                        WriteErrorMessage("addCourseTime", strReturn);
                        return "0";
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("addCourseTime", e.Message);
            }

            return "0";
        }

        #endregion 添加课时对应的教学方案信息

        #region 根据教学方案id信息获取课时信息列表

        /// <summary>
        /// 根据教学方案id信息获取课时信息列表
        /// </summary>
        /// <param name="sTeachingProgramID"></param>
        /// <returns></returns>
        public CourseTimeM[] getCourseTimeByTeachProgramID(string TeachProgramID)
        {
            // int SumCount = 0;
            try
            {
                string strTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mTeachCenterApiUrl + "/CouldPreparation/CourseTime/queryCourseTimeByTeachProgramID");
                sbUrl.Append("?TeachProgramID=" + TeachProgramID);
                sbUrl.Append("&TimeStamp=" + strTimeStamp);
                sbUrl.Append("&Key=" + CP_MD5Helper.GetMd5Hash(strTimeStamp + TeachProgramID));

                WriteTrackLog("getCourseTimeByTeachProgramID", "sbUrl = " + sbUrl);
                string strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteErrorMessage("GetTeachingProgramByID", "返回值为空。查询条件为：" + sbUrl.ToString());

                    return null;
                }

                CLP_ApiResultM<CourseTimeM[]> result = JsonFormatter.JsonDeserialize<CLP_ApiResultM<CourseTimeM[]>>(strResult);
                if (result.ErrorFlag != 0)
                {
                    WriteErrorMessage("getCourseTimeByTeachProgramID", "查询失败，ErrorFlag=" + result.ErrorFlag + "，Message=" + result.Message);

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
                WriteErrorMessage("getCourseTimeByTeachProgramID", e.Message);
            }
            return null;
        }

        #endregion 根据教学方案id信息获取课时信息列表

        #region 添加或者更新知识点对应的教学方案信息

        /// <summary>
        /// 添加或者更新知识点对应的教学方案信息
        ///  /// <param name="KnowledgeCode">知识点唯一编码</param>
        /// <param name="KnowledgeContent">知识点内容</param>
        /// <param name="TeachProgramID">教学方案ID</param>
        /// <param name="BelongCoursewareID">所属的子方案id（如课前预习）</param>
        /// </summary>
        /// <returns></returns>
        ///
        public string addKnowledge(string KnowledgeCode, string KnowledgeContent, string strID, string TeachProgramID, string BelongCoursewareID)
        {
            try
            {
                //short setItemID = TeachSetI.GetSetItemID(eSetItem);
                string strTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string Key = CP_MD5Helper.GetMd5Hash(strTimeStamp + TeachProgramID);
                string param = JsonFormatter.JsonSerialize(new
                {
                    ID = strID,
                    KnowledgeCode = KnowledgeCode,
                    KnowledgeContent = KnowledgeContent,
                    TeachProgramID = TeachProgramID,
                    BelongCoursewareID = BelongCoursewareID,
                    TimeStamp = strTimeStamp,
                    Key = Key
                });
                WriteTrackLog("addKnowledge", "param = " + param);
                string strReturn = CallApiHelper.CallMethodPost(mTeachCenterApiUrl + "/CouldPreparation/Knowledge/addKnowledge", param);
                if (string.IsNullOrEmpty(strReturn))
                    return "0";
                else
                {
                    CLP_ApiResultM<string> result = JsonFormatter.JsonDeserialize<CLP_ApiResultM<string>>(strReturn);
                    if (result.ErrorFlag == 0)
                        return result.Data;
                    else
                    {
                        WriteErrorMessage("addKnowledge", strReturn);
                        return "0";
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("addKnowledge", e.Message);
            }

            return "0";
        }

        #endregion 添加或者更新知识点对应的教学方案信息

        #region 根据教学方案id、知识点唯一编码，所属子方案id信息获取课时信息列表

        /// <summary>
        /// 根据教学方案id、知识点唯一编码，所属子方案id信息获取课时信息列表
        /// </summary>
        /// <param name="sTeachingProgramID"></param>
        /// <returns></returns>
        public KnowledgeM[] getKnowledgeByTeachProgramID(string TeachProgramID, string KnowledgeCode, string BelongCoursewareID)
        {
            // int SumCount = 0;
            try
            {
                string strTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mTeachCenterApiUrl + "/CouldPreparation/Knowledge/queryKnowledgeByTeachProgramID");
                sbUrl.Append("?TeachProgramID=" + TeachProgramID);
                sbUrl.Append("&KnowledgeCode=" + KnowledgeCode);
                sbUrl.Append("&BelongCoursewareID=" + BelongCoursewareID);
                sbUrl.Append("&TimeStamp=" + strTimeStamp);
                sbUrl.Append("&Key=" + CP_MD5Helper.GetMd5Hash(strTimeStamp + TeachProgramID));

                WriteTrackLog("getKnowledgeByTeachProgramID", "sbUrl = " + sbUrl);
                string strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteErrorMessage("getKnowledgeByTeachProgramID", "返回值为空。查询条件为：" + sbUrl.ToString());

                    return null;
                }

                CLP_ApiResultM<KnowledgeM[]> result = JsonFormatter.JsonDeserialize<CLP_ApiResultM<KnowledgeM[]>>(strResult);
                if (result.ErrorFlag != 0)
                {
                    WriteErrorMessage("getKnowledgeByTeachProgramID", "查询失败，ErrorFlag=" + result.ErrorFlag + "，Message=" + result.Message);

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
                WriteErrorMessage("getKnowledgeByTeachProgramID", e.Message);
            }
            return null;
        }

        #endregion 根据教学方案id、知识点唯一编码，所属子方案id信息获取课时信息列表

        #region 添加或者更新教材库对应的教学方案信息

        /// <summary>
        /// 添加或者更新教材库对应的教学方案信息
        ///  /// <param name="ClassPeriodID">课时id</param>
        /// <param name="TeachingMaterialID">教材库id</param>
        /// <param name="TeachProgramID">教学方案ID</param>

        /// </summary>
        /// <returns></returns>
        ///
        public string addMaterialLibrary(string ClassPeriodID, string TeachingMaterialID, string strID, string TeachProgramID)
        {
            try
            {
                //short setItemID = TeachSetI.GetSetItemID(eSetItem);
                string strTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string Key = CP_MD5Helper.GetMd5Hash(strTimeStamp + TeachProgramID);
                string param = JsonFormatter.JsonSerialize(new
                {
                    ID = strID,
                    ClassPeriodID = ClassPeriodID,
                    TeachingMaterialID = TeachingMaterialID,
                    TeachProgramID = TeachProgramID,
                    TimeStamp = strTimeStamp,
                    Key = Key
                });
                WriteTrackLog("addMaterialLibrary", "param = " + param);
                string strReturn = CallApiHelper.CallMethodPost(mTeachCenterApiUrl + "/CouldPreparation/MaterialLibrary/addMaterialLibrary", param);
                if (string.IsNullOrEmpty(strReturn))
                    return "0";
                else
                {
                    CLP_ApiResultM<string> result = JsonFormatter.JsonDeserialize<CLP_ApiResultM<string>>(strReturn);
                    if (result.ErrorFlag == 0)
                        return result.Data;
                    else
                    {
                        WriteErrorMessage("addMaterialLibrary", strReturn);
                        return "0";
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("addMaterialLibrary", e.Message);
            }

            return "0";
        }

        #endregion 添加或者更新教材库对应的教学方案信息

        #region 根据教学方案id、课时id，教材库id信息获取教材库信息列表

        /// <summary>
        /// 根据教学方案id、知识点唯一编码，所属子方案id信息获取课时信息列表
        /// </summary>
        /// <param name="sTeachingProgramID"></param>
        /// <returns></returns>
        public MaterialLibraryM[] getMaterialLibraryByTeachProgramID(string TeachProgramID, string ClassPeriodID, string TeachingMaterialID)
        {
            // int SumCount = 0;
            try
            {
                string strTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mTeachCenterApiUrl + "/CouldPreparation/MaterialLibrary/queryMaterialLibraryByTeachProgramID");
                sbUrl.Append("?TeachProgramID=" + TeachProgramID);
                sbUrl.Append("&ClassPeriodID=" + ClassPeriodID);
                sbUrl.Append("&TeachingMaterialID=" + TeachingMaterialID);
                sbUrl.Append("&TimeStamp=" + strTimeStamp);
                sbUrl.Append("&Key=" + CP_MD5Helper.GetMd5Hash(strTimeStamp + TeachProgramID));

                WriteTrackLog("getMaterialLibraryByTeachProgramID", "sbUrl = " + sbUrl);
                string strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteErrorMessage("getMaterialLibraryByTeachProgramID", "返回值为空。查询条件为：" + sbUrl.ToString());

                    return null;
                }

                CLP_ApiResultM<MaterialLibraryM[]> result = JsonFormatter.JsonDeserialize<CLP_ApiResultM<MaterialLibraryM[]>>(strResult);
                if (result.ErrorFlag != 0)
                {
                    WriteErrorMessage("getMaterialLibraryByTeachProgramID", "查询失败，ErrorFlag=" + result.ErrorFlag + "，Message=" + result.Message);

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
                WriteErrorMessage("getMaterialLibraryByTeachProgramID", e.Message);
            }
            return null;
        }

        #endregion 根据教学方案id、课时id，教材库id信息获取教材库信息列表

        #region 获取云备课服务器信息

        /// <summary>
        ///  获取云备课服务器信息
        /// </summary>
        /// <returns></returns>
        ///
        public CloudServerConfigM[] GetCPSrvInfo()
        {
            // int SumCount = 0;
            try
            {
                //string strTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string strTimeStamp = "20181211";
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

                CLP_ApiResultM<CloudServerConfigM[]> result = JsonFormatter.JsonDeserialize<CLP_ApiResultM<CloudServerConfigM[]>>(strResult);
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

        #region 获取图片转换成二进制

        /// <summary>
        /// Base64String转本地图片
        /// </summary>
        /// <param name="buffer">Base64String</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>

        /// <summary>
        /// 本地图片文件转Base64字符串
        /// <param name="imagepath">本地文件路径</param>
        /// <returns>Base64String</returns>
        public string ImageConvertString(string imagepath)
        {
            using (FileStream fs = new FileStream(imagepath, FileMode.Open))
            {
                byte[] byData = new byte[fs.Length];
                fs.Read(byData, 0, byData.Length);
                fs.Close();
                return Convert.ToBase64String(byData);
            }
        }

        #endregion 获取图片转换成二进制

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
                    string strPath = clsSubPath.FullName + "\\CloudServerConfigl(" + String.Format("{0:yyyy-MM-dd}", clsDate) + ").log";
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
                    string strPath = clsSubPath.FullName + "\\CloudServerConfigl(" + String.Format("{0:yyyy-MM-dd}", clsDate) + ").log";
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

        #endregion 私有方法，调试使用
    }
}