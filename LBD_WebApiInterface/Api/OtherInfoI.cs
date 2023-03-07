using lancoo.cp.basic.sysbaseclass;
using LBD_WebApiInterface.Models;
using LBD_WebApiInterface.Models.CloudPlatform;
using LBD_WebApiInterface.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace LBD_WebApiInterface.Api
{
    public class OtherInfoI
    {
        // 学科
        public enum E_Subject
        {
            None = 0,
            语文 = 1,
            数学 = 2,
            英语 = 3
        }

        private CommandWS mCommandWS;
        private CommandApi mCommandApi;

        private string mNetTeachApiIP;
        private string mNetTeachApiPort;
        private string mNetTeachApiVirDir;//Added by Qinkun
        private string mCloudPlatformIPAndPort;

        private bool mInitStatus;

        public OtherInfoI()
        {
            mInitStatus = false;
        }

        //初始化
        public bool Initialize(string strNetTeachIPAndPort, string strCloudPlatformIPAndPort)
        {
            try
            {
                if (mInitStatus == true)
                {
                    return false;
                }

                mCloudPlatformIPAndPort = strCloudPlatformIPAndPort;

                string strNetTeachIP = "";
                string strNetTeachPort = "";
                long lPort = -1;
                string strNetTeachVirDir = "";
                if (string.IsNullOrEmpty(strNetTeachIPAndPort) == false)
                {
                    string[] arrTemp = strNetTeachIPAndPort.Split(':');
                    strNetTeachIP = arrTemp[0];
                    //strNetTeachPort = arrTemp[1];
                    FormatPortAndVirdir(arrTemp[1], out lPort, out strNetTeachVirDir);
                    strNetTeachPort = lPort.ToString();

                    mNetTeachApiIP = strNetTeachIP;
                    mNetTeachApiPort = strNetTeachPort;
                    mNetTeachApiVirDir = strNetTeachVirDir;
                }

                string strUrl = string.Format(Properties.Resources.TeachInfoUrl, strNetTeachIP, strNetTeachPort, mNetTeachApiVirDir);
                mCommandApi = new CommandApi();
                mCommandApi.BaseUrl = strUrl;

                mCommandWS = new CommandWS();

                mInitStatus = true;
                return mInitStatus;
            }
            catch (Exception e)
            {
                WriteErrorMessage("Initialize", e.Message);
            }
            return false;
        }

        public SubjectPlatformSysInfoM[] GetSubjectPlatformSysInfo(string strCloudSubjectID, string strCloudSubjectName, string strSysID, int iUserType, byte bSubjectID)
        {
            try
            {
                //为空则不查询。一定要确定学科后才查询更多应用系统
                if (string.IsNullOrEmpty(strCloudSubjectID))
                {
                    return null;
                }

                //初始化更多应用系统，一开始从本地数据库读取，主要信息只有系统ID和系统名称
                //然后从学科平台获取，将各系统的其它信息添加进来
                string strWholeUrl = string.Format(Properties.Resources.TeachSetUrl, mNetTeachApiIP, mNetTeachApiPort, mNetTeachApiVirDir);
                strWholeUrl = strWholeUrl + "?action=SelectAllOuterSystemBySubject&params=[\"" + bSubjectID.ToString() + "\"]";
                string strData = mCommandApi.CallMethodGet(strWholeUrl);
                if (string.IsNullOrEmpty(strData))
                {
                    return null;
                }
                OuterSystemM[] arrOuterSys = JsonFormatter.JsonDeserialize<OuterSystemM[]>(strData);
                if (arrOuterSys == null || arrOuterSys.Length == 0)
                {
                    return null;
                }

                List<SubjectPlatformSysInfoM> listSystem = new List<SubjectPlatformSysInfoM>(arrOuterSys.Length);

                //表示当前有用户登录
                if (iUserType > -1)
                {
                    //说明该用户是新用户，则返回默认的系统
                    if (strSysID == null)
                    {
                        strSysID = "630|510|640|810|330|430";
                    }
                    //说明该用户定制的内容就是没有任何系统，则返回0个系统
                    else if (strSysID == "")
                    {
                        strSysID = "330|430";
                    }
                    else
                    {
                        strSysID = strSysID + "|330|430";
                    }

                    for (int i = 0; i < arrOuterSys.Length; i++)
                    {
                        if (strSysID.Contains(arrOuterSys[i].SystemID))
                        {
                            SubjectPlatformSysInfoM sys = new SubjectPlatformSysInfoM();
                            sys.SysID = arrOuterSys[i].SystemID;
                            sys.SysName = arrOuterSys[i].SystemName;
                            sys.SysImage = arrOuterSys[i].PhotoPath;
                            sys.SubjectID = strCloudSubjectID;
                            sys.SubjectName = strCloudSubjectName;
                            sys.IsSetup = false;

                            //为资料收藏夹和公共论坛指定默认访问路径，若之后能从云平台读取这两个系统信息，则覆盖
                            if (string.IsNullOrEmpty(mCloudPlatformIPAndPort) == false)
                            {
                                if (sys.SysID == "330")
                                {
                                    sys.IsSetup = true;
                                    sys.AccessAddr = "http://" + mCloudPlatformIPAndPort + "/SysMgr/Favorite/Default.aspx";
                                }
                                else if (sys.SysID == "430")
                                {
                                    sys.IsSetup = true;
                                    sys.AccessAddr = "http://" + mCloudPlatformIPAndPort + "/Community/Forum/WebPage/ForumMain.aspx";
                                }
                            }

                            listSystem.Add(sys);
                        }
                    }
                }
                //表示当前没有用户登录
                else if (iUserType == -1)
                {
                    iUserType = 1;//当做老师用户来处理
                    for (int i = 0; i < arrOuterSys.Length; i++)
                    {
                        SubjectPlatformSysInfoM sys = new SubjectPlatformSysInfoM();
                        sys.SysID = arrOuterSys[i].SystemID;
                        sys.SysName = arrOuterSys[i].SystemName;
                        sys.SysImage = arrOuterSys[i].PhotoPath;
                        sys.SubjectID = strCloudSubjectID;
                        sys.SubjectName = strCloudSubjectName;
                        sys.IsSetup = false;

                        //为资料收藏夹和公共论坛指定默认访问路径，若之后能从云平台读取这两个系统信息，则覆盖
                        if (string.IsNullOrEmpty(mCloudPlatformIPAndPort) == false)
                        {
                            if (sys.SysID == "330")
                            {
                                sys.IsSetup = true;
                                sys.AccessAddr = "http://" + mCloudPlatformIPAndPort + "/SysMgr/Favorite/Default.aspx";
                            }
                            else if (sys.SysID == "430")
                            {
                                sys.IsSetup = true;
                                sys.AccessAddr = "http://" + mCloudPlatformIPAndPort + "/Community/Forum/WebPage/ForumMain.aspx";
                            }
                        }

                        listSystem.Add(sys);
                    }
                }

                //从学科平台读取配置的应用系统信息
                string strWebServiceURL = "";
                string strParam = "";
                string strReturn = "";
                XmlDocument xmlDoc;
                strWebServiceURL = "http://{0}/Base/WS/Service_Basic.asmx/WS_G_GetSystemAccessInfoForSP";
                //云平台的strSubjectID跟课堂教学系统的不一样
                strParam = "subjectID=" + strCloudSubjectID;
                strWebServiceURL = string.Format(strWebServiceURL, mCloudPlatformIPAndPort);
                strReturn = mCommandWS.CallMethodPost(strWebServiceURL, strParam);

                if (string.IsNullOrEmpty(strReturn))
                {
                    return null;
                }

                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);
                XmlNodeList list = xmlDoc.GetElementsByTagName("anyType");
                int iCount = 0;
                if (list != null)
                {
                    iCount = list.Count;
                }

                if (iCount > 0)
                {
                    for (int i = 0; i < iCount; i++)
                    {
                        XmlNode node = list[i];
                        SubjectPlatformSysInfoM o = new SubjectPlatformSysInfoM();
                        o.SubjectID = node.ChildNodes[0].InnerText;
                        o.SubjectName = node.ChildNodes[1].InnerText;
                        o.SysID = node.ChildNodes[2].InnerText;
                        o.SysName = node.ChildNodes[3].InnerText;
                        o.SysImage = node.ChildNodes[4].InnerText;
                        o.IsEXE = Convert.ToBoolean(node.ChildNodes[5].InnerText);
                        o.IsSetup = Convert.ToBoolean(node.ChildNodes[6].InnerText);
                        o.AccessAddr = node.ChildNodes[7].InnerText;
                        o.WebSvrAddr = node.ChildNodes[8].InnerText;
                        o.WsSvrAddr = node.ChildNodes[9].InnerText;

                        for (int j = 0; j < listSystem.Count; j++)
                        {
                            if (listSystem[j].SysID == o.SysID)
                            {
                                //匹配学科
                                if (string.IsNullOrEmpty(o.SubjectID))//如果查询到的系统的学科ID为空，则认为适合所有学科
                                {
                                    listSystem[j] = o;

                                    string strSysName = listSystem[j].SysName;
                                    DealWithString(ref strSysName, iUserType);
                                    listSystem[j].SysName = strSysName;

                                    string strAccessAddr = listSystem[j].AccessAddr;
                                    DealWithString(ref strAccessAddr, iUserType);
                                    listSystem[j].AccessAddr = strAccessAddr;
                                }
                                else
                                {
                                    if (o.SubjectID == strCloudSubjectID)
                                    {
                                        listSystem[j] = o;

                                        string strSysName = listSystem[j].SysName;
                                        DealWithString(ref strSysName, iUserType);
                                        listSystem[j].SysName = strSysName;

                                        string strAccessAddr = listSystem[j].AccessAddr;
                                        DealWithString(ref strAccessAddr, iUserType);
                                        listSystem[j].AccessAddr = strAccessAddr;
                                    }
                                }
                            }
                        }
                    }
                }

                if (listSystem != null && listSystem.Count > 0)
                {
                    return listSystem.ToArray();
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetOuterSystemInfo", e.Message);
            }

            return null;
        }

        /// <summary>
        /// 获取更多应用系统（对接云平台不再维护各应用系统相对路径的版本）
        /// </summary>
        /// <param name="strSysID">用户定制的系统</param>
        /// <param name="iUserType">用户类型，-1代表没有用户登录，则默认返回所有系统</param>
        /// <param name="strCloudSubjectID">云平台学科ID</param>
        /// <param param name="strCloudSubjectName">云平台学科名称</param>
        /// <param name="bSubjectID">本系统的学科ID</param>
        /// <param name="strToken">用户登录产生的Token，若没有登录可以为空</param>
        /// <returns>学科平台子系统信息</returns>
        public SubjectPlatformSysInfoM[] GetSubjectPlatformSysInfoV2(string strCloudSubjectID, string strCloudSubjectName, string strSysID, int iUserType, byte bSubjectID, string strToken)
        {
            try
            {
                //为空则不查询。一定要确定学科后才查询更多应用系统
                if (string.IsNullOrEmpty(strCloudSubjectID))
                {
                    return null;
                }

                //初始化更多应用系统，确定当前学科最多可以显示哪些系统（从本地数据库读取，主要信息只有系统ID和系统名称）
                string strWholeUrl = string.Format(Properties.Resources.TeachSetUrl, mNetTeachApiIP, mNetTeachApiPort, mNetTeachApiVirDir);
                strWholeUrl = strWholeUrl + "?action=SelectAllOuterSystemBySubject&params=[\"" + bSubjectID.ToString() + "\"]";
                string strData = mCommandApi.CallMethodGet(strWholeUrl);
                if (string.IsNullOrEmpty(strData))
                {
                    return null;
                }

                //更多应用系统（本系统实体）
                OuterSystemM[] arrOuterSys = JsonFormatter.JsonDeserialize<OuterSystemM[]>(strData);
                if (arrOuterSys == null || arrOuterSys.Length == 0)
                {
                    return null;
                }

                //更多应用系统（学科平台的实体）
                List<SubjectPlatformSysInfoM> listSystem = new List<SubjectPlatformSysInfoM>(arrOuterSys.Length);

                //表示有用户登录
                if (iUserType > -1)
                {
                    //说明该用户是新用户，则默认显示如下几个系统
                    if (strSysID == null)
                    {
                        strSysID = "810|430";
                    }
                    //说明该用户定制的内容就是没有任何系统，但必须要显示SysID=430
                    else if (strSysID == "")
                    {
                        strSysID = "430";
                    }
                    //一般情况（有定制内容），添加显示SysID=430
                    else
                    {
                        strSysID = strSysID + "|430";
                    }

                    //有用户登录，则需要判断用户定制了哪些系统
                    for (int i = 0; i < arrOuterSys.Length; i++)
                    {
                        //保留用户定制的系统
                        if (strSysID.Contains(arrOuterSys[i].SystemID))
                        {
                            SubjectPlatformSysInfoM sys = P_GetSPSysModel(arrOuterSys[i], iUserType, strCloudSubjectID, strCloudSubjectName, strToken);
                            listSystem.Add(sys);
                        }
                    }
                }
                //表示当前没有用户登录
                else if (iUserType == -1)
                {
                    //没有用户登录则不用判断用户定制了哪些系统，直接返回所有可显示的
                    for (int i = 0; i < arrOuterSys.Length; i++)
                    {
                        SubjectPlatformSysInfoM sys = P_GetSPSysModel(arrOuterSys[i], iUserType, strCloudSubjectID, strCloudSubjectName, strToken);
                        listSystem.Add(sys);
                    }
                }

                if (listSystem != null && listSystem.Count > 0)
                {
                    return listSystem.ToArray();
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetSubjectPlatformSysInfoV2", e.Message);
            }
            return null;
        }

        //将本系统实体转换成学科平台实体
        private SubjectPlatformSysInfoM P_GetSPSysModel(OuterSystemM outerSys, int iUserType, string strCloudSubjectID, string strCloudSubjectName, string strToken)
        {
            if (outerSys == null)
            {
                return null;
            }

            SubjectPlatformSysInfoM sys = new SubjectPlatformSysInfoM();
            sys.SysID = outerSys.SystemID;
            sys.SysName = outerSys.SystemName;
            sys.SysImage = outerSys.PhotoPath;
            sys.SubjectID = strCloudSubjectID;
            sys.SubjectName = strCloudSubjectName;
            sys.IsSetup = false;
            //访问地址中的IP和端口从云平台接口获取
            string strWebIPandPort = "";
            //430的IP和端口就是云平台的，无法从云平台接口获取
            if (sys.SysID == "430")
            {
                strWebIPandPort = mCloudPlatformIPAndPort;
            }
            else
            {
                strWebIPandPort = GetSubSysWebIPandPort(sys.SysID);
            }
            if (string.IsNullOrEmpty(strWebIPandPort))
            {
                sys.IsSetup = false;
                sys.AccessAddr = "";
            }
            else
            {
                sys.IsSetup = true;
                //若无当前无用户登录，则当做教师用户来获取相对路径
                if (iUserType == -1)
                {
                    iUserType = 1;
                }
                //访问地址中的相对路径和参数在代码里写死
                sys.AccessAddr = "http://" + strWebIPandPort + P_GetSubjectPlatformSysPartAddr(sys.SysID, iUserType, strCloudSubjectID, strToken);
            }

            return sys;
        }

        //根据系统ID获取学科平台子系统的相对路径
        private string P_GetSubjectPlatformSysPartAddr(string SysID, int UserType, string strCloudSubjectID, string strToken)
        {
            string strPartAddr = "";

            if (SysID == null)
            {
                return strPartAddr;
            }

            if (strToken == null)
            {
                strToken = "";
            }

            string strSysID = SysID.ToUpper();
            switch (SysID)
            {
                case "S11":
                    strPartAddr = "/ClassPreview.aspx?lg_tk=" + strToken + "&SubjectID=" + strCloudSubjectID;
                    break;

                case "S13":
                    strPartAddr = "/AfterClassPractice.aspx?lg_tk=" + strToken + "&SubjectID=" + strCloudSubjectID;
                    break;

                case "810":
                    strPartAddr = "/index.aspx?lg_tk=" + strToken + "&SubjectID=" + strCloudSubjectID;
                    break;

                case "830":
                    strPartAddr = "/index.aspx?lg_tk=" + strToken + "&SubjectID=" + strCloudSubjectID;
                    break;

                case "821":
                    strPartAddr = "/index.aspx?lg_tk=" + strToken + "&SubjectID=" + strCloudSubjectID;
                    break;

                case "851":
                    strPartAddr = "/View/TeachStudy.aspx?lg_tk=" + strToken + "&typeid=1";
                    break;

                case "852":
                    strPartAddr = "/View/TeachBehaviour.aspx?lg_tk=" + strToken + "&typeid=2";
                    break;

                case "S20":
                    strPartAddr = "/FreeStudy/index.aspx?lg_tk=" + strToken;
                    break;

                case "S30":
                    strPartAddr = "/Mainpage.aspx?lg_tk=" + strToken + "&subID=" + strCloudSubjectID;
                    break;

                case "430":
                    strPartAddr = "/Community/Forum/WebPage/ForumMain.aspx";
                    break;
            }

            return strPartAddr;
        }

        /// <summary>
        /// 获取云平台下子系统的地址（可选择性使用云平台学科ID获取）
        /// </summary>
        /// <param name="strSysID">系统ID</param>
        /// <param name="strSubjectID">云平台学科ID</param>
        /// <returns>返回值中的地址已删掉头部的“http://”和尾部的“/”</returns>
        private CloudPlatformSubsystemM GetSubSysAddr(string strSysID, string strSubjectID)
        {
            try
            {
                if (string.IsNullOrEmpty(strSysID) || string.IsNullOrEmpty(mCloudPlatformIPAndPort))
                {
                    return null;
                }

                string strWholeUrl = "";
                string strParam = "";
                string strReturn = "";
                XmlDocument xmlDoc = null;
                CloudPlatformSubsystemM sysAddr = new CloudPlatformSubsystemM();

                //若是以下系统，则强制不根据学科来获取
                switch (strSysID)
                {
                    case "S10":
                    case "S20":
                    case "S30":
                    case "S40":
                    case "S50":
                    case "E00":
                        strSubjectID = "";
                        break;
                }
                //学科ID为空则使用全学科的接口获取，然后取第一个
                if (string.IsNullOrEmpty(strSubjectID))
                {
                    strWholeUrl = "http://" + mCloudPlatformIPAndPort + "/Base/WS/Service_Basic.asmx/WS_G_GetSubSystemServerInfoForAllSubject";
                    strParam = "sysID={0}";
                    strParam = string.Format(strParam, strSysID);
                    strReturn = mCommandWS.CallMethodPost(strWholeUrl, strParam);
                    if (string.IsNullOrEmpty(strReturn))
                    {
                        return null;
                    }
                    xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(strReturn);
                    XmlNodeList list = xmlDoc.GetElementsByTagName("anyType");
                    if (list == null || list.Count == 0)
                    {
                        return null;
                    }

                    sysAddr.SysID = list[0].ChildNodes[0].InnerText;
                    sysAddr.SubjectID = list[0].ChildNodes[1].InnerText;
                    sysAddr.WebSvrAddr = list[0].ChildNodes[2].InnerText;
                    sysAddr.WsSvrAddr = list[0].ChildNodes[3].InnerText;
                }
                //学科ID不为空，则使用学科相关的接口获取
                else
                {
                    strWholeUrl = "http://" + mCloudPlatformIPAndPort + "/Base/WS/Service_Basic.asmx/WS_G_GetSubSystemServerInfo";
                    strParam = "sysID={0}&subjectID={1}";
                    strParam = string.Format(strParam, strSysID, strSubjectID);
                    strReturn = mCommandWS.CallMethodPost(strWholeUrl, strParam);
                    if (string.IsNullOrEmpty(strReturn))
                    {
                        return null;
                    }
                    xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(strReturn);
                    XmlNodeList list = xmlDoc.GetElementsByTagName("string");
                    if (list == null || list.Count == 0)
                    {
                        return null;
                    }

                    sysAddr.SysID = list[0].InnerText;
                    sysAddr.SubjectID = list[1].InnerText;
                    sysAddr.WebSvrAddr = list[2].InnerText;
                    sysAddr.WsSvrAddr = list[3].InnerText;
                }

                if (string.IsNullOrEmpty(sysAddr.SysID))
                {
                    return null;
                }

                if (string.IsNullOrEmpty(sysAddr.WebSvrAddr) == false)
                {
                    sysAddr.WebSvrAddr = sysAddr.WebSvrAddr.Replace("http://", "").TrimEnd('/');
                }
                if (string.IsNullOrEmpty(sysAddr.WsSvrAddr) == false)
                {
                    sysAddr.WsSvrAddr = sysAddr.WsSvrAddr.Replace("http://", "").TrimEnd('/');
                }

                return sysAddr;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetSubSysAddr", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 根据基础平台版本判断是否有“个人网盘”模块
        /// </summary>
        /// <returns></returns>
        public bool IsHaveNetDisk()
        {
            try
            {
                string requestTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string secCode = CP_MD5Helper.GetMd5Hash("000" + requestTime);
                string strParam = string.Format("RequestTime={0}&SecCode={1}", requestTime, secCode);
                string strReturn = mCommandWS.CallMethodPost("http://" + mCloudPlatformIPAndPort + "/base/WS/Service_Basic.asmx/WS_G_GetBaseLockerInfo", strParam);
                if (string.IsNullOrEmpty(strReturn))
                    return false;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);
                XmlNodeList list = xmlDoc.GetElementsByTagName("string");
                if (list == null || list.Count == 0)
                    return false;
                string cloudPlatformVer = CP_EncryptHelper.DecryptCode("000", list[1].InnerText);
                if (cloudPlatformVer == "1")//基础版没有个人网盘
                    return false;
                else
                    return true;
            }
            catch (Exception e)
            {
                WriteErrorMessage("IsHaveNetDisk", e.Message);
                return false;
            }
        }

        /// <summary>
        /// 获取云平台下子系统Web访问地址（不使用云平台学科ID获取，即按通用学科方式获取）
        /// </summary>
        /// <param name="strSysID">系统ID</param>
        /// <returns>返回值中的地址已删掉头部的“http://”和尾部的“/”</returns>
        private string GetSubSysWebIPandPort(string strSysID)
        {
            try
            {
                CloudPlatformSubsystemM sysAddr = GetSubSysAddr(strSysID, null);
                if (sysAddr != null)
                {
                    return sysAddr.WebSvrAddr;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetSubSysWebIPandPort", e.Message);
            }
            return "";
        }

        private void DealWithString(ref string str, int iUserType)
        {
            try
            {
                if (string.IsNullOrEmpty(str) == false)
                {
                    bool bIsFind = false;
                    string[] arrTemp = str.Split('|');
                    foreach (string s in arrTemp)
                    {
                        string[] arr = s.Split(new string[1] { "@@" }, StringSplitOptions.None);
                        if (arr[0] == iUserType.ToString())
                        {
                            str = arr[1];
                            bIsFind = true;
                            break;
                        }
                    }

                    if (bIsFind == false)
                    {
                        str = arrTemp[0];
                        int index = str.IndexOf("@@");
                        if (index > -1)
                        {
                            str = str.Substring(index + 2);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("DealWithString", e.Message);
            }
        }

        private string GetSubjectName(E_Subject eSubject)
        {
            try
            {
                string strSubjectName = "";
                switch ((int)eSubject)
                {
                    case 0:
                        strSubjectName = "";
                        break;

                    case 1:
                        strSubjectName = "语文";
                        break;

                    case 2:
                        strSubjectName = "数学";
                        break;

                    case 3:
                        strSubjectName = "英语";
                        break;
                }
                return strSubjectName;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetSubjectName", e.Message);
            }
            return "";
        }

        private bool FormatPortAndVirdir(string sFormatedString, out long lPort, out string sVirDir)
        {
            lPort = -100;
            sVirDir = "-1";
            try
            {
                string[] infoArr = sFormatedString.Split('/');
                lPort = long.Parse(infoArr[0]);
                if (infoArr.Length > 2)
                    sVirDir = infoArr[1] + "/";
                else if ((infoArr.Length == 2) && (string.IsNullOrEmpty(infoArr[1]) == true))
                    sVirDir = "";
                else if ((infoArr.Length == 2) && (string.IsNullOrEmpty(infoArr[1]) == false))
                    sVirDir = infoArr[1] + "/";
                else
                    sVirDir = "";
                return true;
            }
            catch (Exception e)
            {
                WriteErrorMessage("FormatPortAndVirdir", e.ToString());
                return false;
                //throw;
            }
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
                    string strPath = clsSubPath.FullName + "\\OtherInfoI(" + String.Format("{0:yyyy-MM-dd}", clsDate) + ").log";
                    StreamWriter clsWriter = new StreamWriter(strPath, true);
                    clsWriter.WriteLine(String.Format("{0:HH:mm:ss}", clsDate) + " " + strMethodName + ": " + strErrorMessage);
                    clsWriter.Close();
                }
            }
            catch { }
        }
    }
}