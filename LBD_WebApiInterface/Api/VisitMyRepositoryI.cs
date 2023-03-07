using LBD_WebApiInterface.Models;
using LBD_WebApiInterface.Models.E_ResourceLibrary;
using LBD_WebApiInterface.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace LBD_WebApiInterface.Api
{
    public class VisitMyRepositoryI
    {
        private string mNetTeachIP;
        private string mNetTeachPort;
        private string mNetTeachVirDir;

        private bool mInitStatus;

        public VisitMyRepositoryI()
        {
            mInitStatus = false;
        }

        /// <summary>
        /// 参数为网络化课堂教学系统整体服务的IP和Port
        /// </summary>
        public bool Initialize(string strNetTeachIP, string strNetTeachPort, string strNetTeachVirDir)
        {
            try
            {
                if (mInitStatus == true)
                {
                    return false;
                }

                if (string.IsNullOrEmpty(strNetTeachIP))
                {
                    return false;
                }
                else
                {
                    mNetTeachIP = strNetTeachIP;
                }

                if (string.IsNullOrEmpty(strNetTeachPort))
                {
                    return false;
                }
                else
                {
                    mNetTeachPort = strNetTeachPort;
                }
                mNetTeachVirDir = strNetTeachVirDir;
                mInitStatus = true;

                return mInitStatus;
            }
            catch (Exception e)
            {
                WriteErrorMessage("Initialize", e.Message);
            }

            return false;
        }

        #region 获取“我的资源库”

        /// <summary>
        /// 获取“我的资料库”的内容
        /// </summary>
        public MyRepositoryM GetMyRepository(byte bSubjectID, string strTeacherID, string strMultipleSubjectAPIIPAndPort, string strCourseClassID)
        {
            try
            {
                DateTime dt1 = DateTime.Now;

                MyRepositoryM myRepository = new MyRepositoryM();

                R_MyDigitizedResourceM myDR = GetMyDigitizedResourceV3(bSubjectID, strTeacherID, strMultipleSubjectAPIIPAndPort, strCourseClassID);
                R_MyFavoriteM myF = GetMyFavorite();
                R_MyNoteM myNote = GetMyNote();
                R_MyNetDiskM myND = GetMyNetDisk();
                R_MyNetCoursewareM myNCW = GetMyNetCourseware(bSubjectID, strTeacherID);

                if (myDR == null)
                {
                    myDR = new R_MyDigitizedResourceM();
                    myDR.IsShow = true;
                    myDR.HasValue = false;
                    myDR.SourceItems = null;
                }
                if (myF == null)
                {
                    myF = new R_MyFavoriteM();
                    myF.IsShow = false;
                    myF.HasValue = false;
                }
                if (myNote == null)
                {
                    myNote = new R_MyNoteM();
                    myNote.IsShow = false;
                    myNote.HasValue = false;
                }
                if (myND == null)
                {
                    myND = new R_MyNetDiskM();
                    myND.IsShow = false;
                    myND.HasValue = false;
                }
                if (myNCW == null)
                {
                    myNCW = new R_MyNetCoursewareM();
                    myNCW.IsShow = true;
                    myNCW.HasValue = false;
                    myNCW.NetCoursewares = null;
                }
                myRepository.MyDigitizedResource = myDR;
                myRepository.MyFavorite = myF;
                myRepository.MyNote = myNote;
                myRepository.MyNetDisk = myND;
                myRepository.MyNetCourseware = myNCW;

                DateTime dt2 = DateTime.Now;
                TimeSpan ts = dt2.Subtract(dt1);

                return myRepository;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetMyRepository", e.Message);
            }

            return null;
        }

        //获取电子资源库
        private R_MyDigitizedResourceM GetMyDigitizedResource(byte bSubjectID, string strTeacherID, string strMultipleSubjectAPIIPAndPort, string strCourseClassID)
        {
            try
            {
                List<DigitizedResourceItemM> listSelected = new List<DigitizedResourceItemM>();

                TeachSetI ts = new TeachSetI();
                ts.Initialize(mNetTeachIP, mNetTeachPort, mNetTeachVirDir);

                WriteDebugInfo("GetMyDigitizedResource: 标志1");
                DigitizedResourceItemM[] allSourceItem = ts.GetAllDigitizedResourceItem(bSubjectID, strTeacherID, strMultipleSubjectAPIIPAndPort, strCourseClassID);
                WriteDebugInfo("GetMyDigitizedResource: 标志2");
                TeachSetItemM[] allSetItem = ts.GetAllSetItem();
                string strSetValue = ts.GetItemValueSingle(TeachSetI.E_SetItem.我的资料库, bSubjectID, strTeacherID, strCourseClassID, 0);
                if (string.IsNullOrEmpty(strSetValue) == false)
                {
                    //教师定制的资料库ID（多个，数组）
                    string[] arrSelectedItemID = strSetValue.Split('|');
                    WriteDebugInfo("GetMyDigitizedResource: 标志3");
                    for (int i = 0; i < arrSelectedItemID.Length; i++)
                    {
                        for (int j = 0; j < allSourceItem.Length; j++)
                        {
                            WriteDebugInfo("GetMyDigitizedResource: 标志4");
                            //当教师定制的资料库ID在所有的资料库ID中含有，则获取
                            if (arrSelectedItemID[i] == allSourceItem[j].ItemID.ToString())
                            {
                                WriteDebugInfo("GetMyDigitizedResource: 标志5");
                                listSelected.Add(allSourceItem[j]);
                                WriteDebugInfo("GetMyDigitizedResource: 标志6");
                            }
                        }
                    }
                }

                R_MyDigitizedResourceM myDR = new R_MyDigitizedResourceM();
                myDR.IsShow = true;
                if (listSelected.Count > 0)
                {
                    myDR.HasValue = true;
                    myDR.SourceItems = listSelected;
                }
                else
                {
                    myDR.HasValue = false;
                    myDR.SourceItems = null;
                }

                return myDR;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetMyDigitizedResource", e.Message);
            }
            return null;
        }

        //获取电子资源库V3.0
        private R_MyDigitizedResourceM GetMyDigitizedResourceV3(byte bSubjectID, string strTeacherID, string strE_ResourceLibraryIPandPort, string strCourseClassID)
        {
            try
            {
                //教师定制的子库
                List<DigitizedResourceItemM> listSelected = new List<DigitizedResourceItemM>();

                TeachSetI ts = new TeachSetI();
                ts.Initialize(mNetTeachIP, mNetTeachPort, mNetTeachVirDir);
                string strSetValue = ts.GetItemValueSingle2(TeachSetI.E_SetItem.数字化资源库重命名, bSubjectID, strTeacherID, strCourseClassID, 0);
                //若教师定制为空，则默认
                if (strSetValue == null)
                {
                    ResourceClassLevelTwoForSearchM[] arrItem = E_ResourceLibrary3I.Static_GetResourceClassTwo(strE_ResourceLibraryIPandPort);
                    if (arrItem != null)
                    {
                        for (int i = 0; i < arrItem.Length; i++)
                        {
                            DigitizedResourceItemM item = new DigitizedResourceItemM()
                            {
                                ItemID = arrItem[i].ID,
                                ItemName = arrItem[i].Name
                            };

                            listSelected.Add(item);
                        }
                    }
                }
                //若strSetValue=string.Empty，则表示老师定制的项为空
                else
                {
                    string[] arrItem = strSetValue.Split((char)1);
                    for (int i = 0; i < arrItem.Length; i++)
                    {
                        if (string.IsNullOrEmpty(arrItem[i]) == false)
                        {
                            string[] arrIDandName = arrItem[i].Split((char)2);
                            if (arrIDandName.Length == 2 && string.IsNullOrEmpty(arrIDandName[0]) == false)
                            {
                                DigitizedResourceItemM item = new DigitizedResourceItemM();
                                item.ItemID = arrIDandName[0];
                                item.ItemName = arrIDandName[1];
                                listSelected.Add(item);
                            }
                        }
                    }
                }

                R_MyDigitizedResourceM myDR = new R_MyDigitizedResourceM();
                myDR.IsShow = true;
                if (listSelected.Count > 0)
                {
                    myDR.HasValue = true;
                    myDR.SourceItems = listSelected;
                }
                else
                {
                    myDR.HasValue = false;
                    myDR.SourceItems = null;
                }

                return myDR;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetMyDigitizedResource", e.Message);
            }
            return null;
        }

        //获取“我的收藏”
        private R_MyFavoriteM GetMyFavorite()
        {
            try
            {
                R_MyFavoriteM myF = new R_MyFavoriteM();
                myF.IsShow = false;
                myF.HasValue = false;

                return myF;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetMyFavorite", e.Message);
            }

            return null;
        }

        //获取“我的笔记”
        private R_MyNoteM GetMyNote()
        {
            try
            {
                R_MyNoteM myNote = new R_MyNoteM();
                myNote.IsShow = false;
                myNote.HasValue = false;

                return myNote;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetMyNote", e.Message);
            }

            return null;
        }

        //获取“我的网盘”
        private R_MyNetDiskM GetMyNetDisk()
        {
            try
            {
                R_MyNetDiskM myND = new R_MyNetDiskM();
                myND.IsShow = true;
                myND.HasValue = false;

                return myND;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetMyNetDisk", e.Message);
            }
            return null;
        }

        //获取“我的网络化课件”
        private R_MyNetCoursewareM GetMyNetCourseware(byte bSubjectID, string strTeacherID)
        {
            try
            {
                WriteErrorMessage("GetMyNetCourseware", "被调用了。SubjectID=" + bSubjectID + ",strTeacherID=" + strTeacherID);

                List<NetCoursewareM> listCourseware = new List<NetCoursewareM>();

                NetCoursewareI cm = new NetCoursewareI();
                cm.Initialize(mNetTeachIP, mNetTeachPort);
                NetCoursewareM[] netCourseware = cm.GetNetCourseware(bSubjectID, strTeacherID);
                if (netCourseware != null)
                {
                    listCourseware.AddRange(netCourseware);
                }

                R_MyNetCoursewareM myNCW = new R_MyNetCoursewareM();
                myNCW.IsShow = true;
                if (listCourseware.Count > 0)
                {
                    myNCW.HasValue = true;
                    myNCW.NetCoursewares = listCourseware;
                }
                else
                {
                    myNCW.HasValue = false;
                    myNCW.NetCoursewares = null;
                }

                string strJson = JsonFormatter.JsonSerialize(myNCW.NetCoursewares);
                WriteErrorMessage("GetMyNetCourseware", strJson);

                return myNCW;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetMyNetCourseware", e.Message);
            }

            return null;
        }

        #endregion 获取“我的资源库”

        /// <summary>
        /// 根据课件ID获取网络化课件详细信息（strTeacherID为null，则教学计划为null）
        /// </summary>
        public NetCoursewareDetailM GetNetCoursewareDetailByID(string strCoursewareID, string strTeacherID)
        {
            try
            {
                if (string.IsNullOrEmpty(strCoursewareID))
                {
                    return null;
                }

                NetCoursewareI cm = new NetCoursewareI();
                cm.Initialize(mNetTeachIP, mNetTeachPort);

                NetCoursewareDetailM ncwd = cm.GetNetCoursewareDetailByID(strCoursewareID, strTeacherID);

                return ncwd;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetNetCoursewareDetailByID", e.Message);
            }

            return null;
        }

        //此接口
        //更新网络化课件的状态（使用网络化课件上课后就调用此接口将网络化课件状态更新为true）
        public int UpdateNetCoursewareStatus(string strCoursewareID, bool bStatus, string strLastEditor)
        {
            try
            {
                if (string.IsNullOrEmpty(strCoursewareID))
                {
                    return 0;
                }

                string strApiBaseUrl = string.Format(Properties.Resources.NetCoursewareUrl, mNetTeachIP, mNetTeachPort, "");
                CommandApi CommandApi = new CommandApi();
                CommandApi.BaseUrl = strApiBaseUrl;

                string[] arrParam = new string[9];
                arrParam[0] = strCoursewareID;
                arrParam[1] = null;
                arrParam[2] = null;
                arrParam[3] = null;
                arrParam[4] = null;
                arrParam[5] = null;
                arrParam[6] = bStatus.ToString();
                arrParam[7] = null;
                arrParam[8] = strLastEditor;

                string strReturn = CommandApi.CallMethodPost("UpdateNetCourseware", arrParam);
                if (string.IsNullOrEmpty(strReturn))
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(strReturn);
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("UpdateNetCoursewareStatus", e.Message);
            }
            return 0;
        }

        /// <summary>
        /// 获取“我的课本”
        /// </summary>
        public string GetMyTextbook(byte bSubjectID, string strTeacherID, string strCourseClassID)
        {
            try
            {
                TeachSetI ts = new TeachSetI();
                ts.Initialize(mNetTeachIP, mNetTeachPort, mNetTeachVirDir);
                TeachSetItemM[] allSetItem = ts.GetAllSetItem();
                short resultID = -1;
                if (allSetItem != null)
                {
                    foreach (TeachSetItemM setItem in allSetItem)
                    {
                        if (setItem.SetItemName == "我的课本")
                        {
                            resultID = setItem.SetItemID;
                            break;
                        }
                    }
                }

                if (resultID > 0)
                {
                    string strValue = ts.GetItemValueSingle(TeachSetI.E_SetItem.我的课本, bSubjectID, strTeacherID, strCourseClassID, 0);
                    return strValue;
                    //TeacherSetInfoM[] tsInfo = ts.GetTeacherSetValue(bSubjectID, strTeacherID, resultID);
                    //if (tsInfo != null && tsInfo.Length == 1)
                    //{
                    //    string strDirID = tsInfo[0].SetItemValue;
                    //    return strDirID;
                    //}
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetMyTextbook", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 获取本地电脑常用路径
        /// </summary>
        public string GetLocalComputerPath(byte bSubjectID, string strTeacherID, short sClassroomIndex)
        {
            try
            {
                // TeacherSetInfoM ts=GetSetInfo(bSubjectID,strTeacherID,"本地电脑常用教学资源目录定制");
                //                 if(ts!=null)
                //                 {
                //                     string strPath=ts.SetItemValue;
                //                     return strPath;
                //                 }

                TeachSetI ts = new TeachSetI();
                ts.Initialize(mNetTeachIP, mNetTeachPort, mNetTeachVirDir);
                string strValue = ts.GetItemValueSingle(TeachSetI.E_SetItem.本地电脑常用目录, bSubjectID, strTeacherID, null, sClassroomIndex);

                return strValue;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetLocalComputerPath", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 获取U盘常用路径
        /// </summary>
        public string GetUpanPath(byte bSubjectID, string strTeacherID, short sClassroomIndex)
        {
            try
            {
                //TeacherSetInfoM ts = GetSetInfo(bSubjectID, strTeacherID, "U盘常用教学资源目录定制");
                //if (ts != null)
                //{
                //    string strPath = ts.SetItemValue;
                //    return strPath;
                //}

                TeachSetI ts = new TeachSetI();
                ts.Initialize(mNetTeachIP, mNetTeachPort, mNetTeachVirDir);
                string strValue = ts.GetItemValueSingle(TeachSetI.E_SetItem.U盘常用目录, bSubjectID, strTeacherID, null, sClassroomIndex);
                return strValue;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetUpanPath", e.Message);
            }
            return null;
        }

        #region 资料选择相关

        //获取“我的收藏”的目录
        public FavoriteFolderM[] GetMyFavoriteFolder(string strToken, string strSysID, string strCloudPlatformIPAndPort)
        {
            try
            {
                CommandApi mCommandApi = new CommandApi();
                string strWholeUrl = "";
                string strUrl = "";
                string strMethod = "";
                string[] arrParam = null;
                string strReturn = "";

                strWholeUrl = "http://{0}/SysMgr/Favorite/Api/Service_Favorite.ashx?token={1}&method={2}{3}";
                strUrl = strCloudPlatformIPAndPort;
                strMethod = "GetFolderInfo";
                arrParam = new string[4];
                arrParam[0] = strUrl;
                arrParam[1] = strToken;
                arrParam[2] = strMethod;
                if (string.IsNullOrEmpty(strSysID))
                {
                    arrParam[3] = "&params=";
                }
                else
                {
                    arrParam[3] = "&params=" + strSysID;
                }
                strWholeUrl = string.Format(strWholeUrl, arrParam);
                strReturn = mCommandApi.CallMethodGet(strWholeUrl);
                UtilityClass.AnalyseCloudJson(ref strReturn);

                FavoriteFolderM[] folder = JsonFormatter.JsonDeserialize<FavoriteFolderM[]>(strReturn);

                return folder;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetMyFavoriteFolder", e.Message);
            }

            return null;
        }

        //获取“我的收藏”的资源
        public FavoriteResInfoM[] GetMyFavoriteResInfo(string strToken, string strCloudPlatformIPAndPort, string strFolderID, int iPageIndex, int iPageSize)
        {
            try
            {
                CommandApi mCommandApi = new CommandApi();

                string strWholeUrl = "";
                string strUrl = "";
                string strMethod = "";
                string strParam = "";
                string[] arrParam = null;
                string strReturn = "";

                strWholeUrl = "http://{0}/SysMgr/Favorite/Api/Service_Favorite.ashx?token={1}&method={2}{3}";
                strUrl = strCloudPlatformIPAndPort;
                strMethod = "GetCollectResInfo";
                strParam = "&params={0}|{1}|{2}";
                strParam = string.Format(strParam, strFolderID, iPageIndex, iPageSize);
                arrParam = new string[4];
                arrParam[0] = strUrl;
                arrParam[1] = strToken;
                arrParam[2] = strMethod;
                arrParam[3] = strParam;
                strWholeUrl = string.Format(strWholeUrl, arrParam);
                strReturn = mCommandApi.CallMethodGet(strWholeUrl);
                UtilityClass.AnalyseCloudJson(ref strReturn);

                FavoriteResInfoM[] res = JsonFormatter.JsonDeserialize<FavoriteResInfoM[]>(strReturn);

                return res;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetMyFavoriteFolder", e.Message);
            }

            return null;
        }

        //个人网盘的目录和文件（strFolderID为空时查询根目录）
        public PersonDiskFolderOrFileM[] GetMyNetDiskFolderAndFile(string strToken, string strCloudPlatformIPAndPort, string strFolderID)
        {
            try
            {
                CommandApi mCommandApi = new CommandApi();

                string strWholeUrl = "";
                string strUrl = "";
                string strMethod = "";
                string strParam = "";
                string[] arrParam = null;
                string strReturn = "";

                //type和range应该不需要外部指定
                int iType = 3;
                int iRange = 1;

                strWholeUrl = "http://{0}/SysMgr/PersonDisk/Api/Service_PersonDisk.ashx?token={1}&method={2}{3}";
                strUrl = strCloudPlatformIPAndPort;
                strMethod = "GetAllFileByFolderID";
                strParam = "&params={0}|{1}|{2}";
                strParam = string.Format(strParam, strFolderID, iType, iRange);
                arrParam = new string[4];
                arrParam[0] = strUrl;
                arrParam[1] = strToken;
                arrParam[2] = strMethod;
                arrParam[3] = strParam;
                strWholeUrl = string.Format(strWholeUrl, arrParam);
                strReturn = mCommandApi.CallMethodGet(strWholeUrl);
                UtilityClass.AnalyseCloudJson(ref strReturn);

                PersonDiskFolderOrFileM[] folder = JsonFormatter.JsonDeserialize<PersonDiskFolderOrFileM[]>(strReturn);

                return folder;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetMyNetDiskFolder", e.Message);
            }
            return null;
        }

        //个人网盘的文件
        public PersonDiskFileM GetMyNetDiskFile(string strToken, string strCloudPlatformIPAndPort, string strFileID)
        {
            try
            {
                CommandApi mCommandApi = new CommandApi();

                string strWholeUrl = "";
                string strUrl = "";
                string strMethod = "";
                string strParam = "";
                string[] arrParam = null;
                string strReturn = "";

                strWholeUrl = "http://{0}/SysMgr/PersonDisk/Api/Service_PersonDisk.ashx?token={1}&method={2}{3}";
                strUrl = strCloudPlatformIPAndPort;
                strMethod = "GetFileInfo";
                strParam = "&params=" + strFileID;
                arrParam = new string[4];
                arrParam[0] = strUrl;
                arrParam[1] = strToken;
                arrParam[2] = strMethod;
                arrParam[3] = strParam;
                strWholeUrl = string.Format(strWholeUrl, arrParam);
                strReturn = mCommandApi.CallMethodGet(strWholeUrl);
                UtilityClass.AnalyseCloudJson(ref strReturn);

                PersonDiskFileM file = JsonFormatter.JsonDeserialize<PersonDiskFileM>(strReturn);

                return file;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetMyNetDiskFile", e.Message);
            }
            return null;
        }

        //（WS）个人网盘的目录和文件（strFolderID为空时查询根目录）
        public PersonDiskFolderOrFileM[] GetMyNetDiskFolderAndFile_WS(string strToken, string strCloudPlatformIPAndPort, string strFolderID)
        {
            try
            {
                CommandWS CommandWS = new CommandWS();

                string strWebServiceURL = "";
                string strParam = "";
                string strReturn = "";
                XmlDocument xmlDoc;

                //type和range应该不需要外部指定
                int iType = 1;//获取文件
                int iRange = 1;

                if (strFolderID == null)
                {
                    strFolderID = "";
                }

                strWebServiceURL = "http://{0}/SysMgr/PersonDisk/WS/Service_PersonDisk.asmx/WS_SysMgr_G_GetAllFileByFolderID";
                //云平台的strSubjectID跟课堂教学系统的不一样
                strParam = "token=" + strToken + "&folderID=" + strFolderID + "&type=" + iType + "&range=" + iRange;
                strWebServiceURL = string.Format(strWebServiceURL, strCloudPlatformIPAndPort);
                strReturn = CommandWS.CallMethodPost(strWebServiceURL, strParam);

                if (string.IsNullOrEmpty(strReturn))
                {
                    return null;
                }

                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);
                XmlNodeList list = xmlDoc.GetElementsByTagName("anyType");
                int iCount = 0;
                if (list != null)
                    iCount = list.Count;
                List<PersonDiskFolderOrFileM> listDir = new List<PersonDiskFolderOrFileM>();
                if (iCount > 0)
                {
                    for (int i = 0; i < iCount; i++)
                    {
                        XmlNode node = list[i];
                        PersonDiskFolderOrFileM dir = new PersonDiskFolderOrFileM();
                        dir.FolderOrFileID = node.ChildNodes[0].InnerText;
                        dir.FolderOrFileName = node.ChildNodes[1].InnerText;
                        dir.UpdateTime = node.ChildNodes[2].InnerText;
                        dir.PID = node.ChildNodes[3].InnerText;
                        dir.IsFolder = false;
                        listDir.Add(dir);
                    }
                }
                iType = 2;//获取目录
                strParam = "token=" + strToken + "&folderID=" + strFolderID + "&type=" + iType + "&range=" + iRange;
                strReturn = CommandWS.CallMethodPost(strWebServiceURL, strParam);
                if (string.IsNullOrEmpty(strReturn))
                    return null;
                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);
                list = xmlDoc.GetElementsByTagName("anyType");
                if (list != null && list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        XmlNode node = list[i];
                        PersonDiskFolderOrFileM dir = new PersonDiskFolderOrFileM();
                        dir.FolderOrFileID = node.ChildNodes[0].InnerText;
                        dir.FolderOrFileName = node.ChildNodes[1].InnerText;
                        dir.UpdateTime = node.ChildNodes[2].InnerText;
                        dir.PID = node.ChildNodes[3].InnerText;
                        dir.IsFolder = true;
                        listDir.Add(dir);
                    }
                }
                if (listDir.Count > 0)
                    return listDir.ToArray();
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetMyNetDiskFolderAndFile_WS", e.Message);
            }
            return null;
        }

        //（WS）个人网盘的文件
        public PersonDiskFileM GetMyNetDiskFile_WS(string strToken, string strCloudPlatformIPAndPort, string strFileID)
        {
            try
            {
                CommandWS CommandWS = new CommandWS();

                string strWebServiceURL = "";
                string strParam = "";
                string strReturn = "";
                XmlDocument xmlDoc;

                strWebServiceURL = "http://{0}/SysMgr/PersonDisk/WS/Service_PersonDisk.asmx/WS_SysMgr_G_GetFileInfo";
                //云平台的strSubjectID跟课堂教学系统的不一样
                strParam = "token=" + strToken + "&resID=" + strFileID;
                strWebServiceURL = string.Format(strWebServiceURL, strCloudPlatformIPAndPort);
                strReturn = CommandWS.CallMethodPost(strWebServiceURL, strParam);

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
                if (iCount == 1)
                {
                    PersonDiskFileM file = new PersonDiskFileM();

                    XmlNode node = list[0];
                    file.FileID = node.ChildNodes[0].InnerText;
                    file.FileName = node.ChildNodes[1].InnerText;
                    file.FileType = node.ChildNodes[2].InnerText;
                    file.FileSize = node.ChildNodes[3].InnerText;
                    file.FilePath = node.ChildNodes[4].InnerText;
                    file.FolderID = node.ChildNodes[5].InnerText;

                    return file;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetMyNetDiskFile_WS", e.Message);
            }

            return null;
        }

        //////////////////////////
        //（WS）获取个人网盘的所有文件
        public PersonDiskFolderOrFileM[] GetAllMyNetDiskFile_WS(string strToken, string strCloudPlatformIPAndPort)
        {
            try
            {
                CommandWS CommandWS = new CommandWS();

                string strWebServiceURL = "";
                string strParam = "";
                string strReturn = "";
                XmlDocument xmlDoc;
                //type和range应该不需要外部指定
                int iType = 1;//获取文件
                int iRange = 2;//获取本目录及子目录下所有文件
                strWebServiceURL = "http://{0}/SysMgr/PersonDisk/WS/Service_PersonDisk.asmx/WS_SysMgr_G_GetAllFileByFolderID";
                //云平台的strSubjectID跟课堂教学系统的不一样
                strParam = "token=" + strToken + "&folderID=&type=" + iType + "&range=" + iRange;
                strWebServiceURL = string.Format(strWebServiceURL, strCloudPlatformIPAndPort);
                strReturn = CommandWS.CallMethodPost(strWebServiceURL, strParam);
                if (string.IsNullOrEmpty(strReturn))
                    return null;
                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);
                XmlNodeList list = xmlDoc.GetElementsByTagName("anyType");
                int iCount = 0;
                if (list != null)
                    iCount = list.Count;
                List<PersonDiskFolderOrFileM> listDir = new List<PersonDiskFolderOrFileM>();
                if (iCount > 0)
                {
                    for (int i = 0; i < iCount; i++)
                    {
                        XmlNode node = list[i];
                        PersonDiskFolderOrFileM dir = new PersonDiskFolderOrFileM();
                        dir.FolderOrFileID = node.ChildNodes[0].InnerText;
                        dir.FolderOrFileName = node.ChildNodes[1].InnerText;
                        dir.UpdateTime = node.ChildNodes[2].InnerText;
                        dir.PID = node.ChildNodes[3].InnerText;
                        dir.IsFolder = false;
                        listDir.Add(dir);
                    }
                }
                if (listDir.Count > 0)
                    return listDir.ToArray();
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetAllMyNetDiskFile_WS", e.Message);
            }
            return null;
        }

        #endregion 资料选择相关

        //private TeacherSetInfoM GetSetInfo(byte bSubjectID,string strTeacherID,string strName)
        //{
        //    try
        //    {
        //        TeachSetI ts = new TeachSetI();
        //        ts.Initialize(mNetTeachIP, mNetTeachPort);
        //        TeachSetItemM[] allSetItem = ts.GetAllSetItem();
        //        short resultID = -1;
        //        if (allSetItem != null)
        //        {
        //            foreach (TeachSetItemM setItem in allSetItem)
        //            {
        //                if (setItem.SetItemName == strName)
        //                {
        //                    resultID = setItem.SetItemID;
        //                    break;
        //                }
        //            }
        //        }

        //        if (resultID > 0)
        //        {
        //            TeacherSetInfoM[] tsInfo = ts.GetTeacherSetValue(bSubjectID, strTeacherID, resultID);
        //            if (tsInfo != null && tsInfo.Length == 1)
        //            {
        //                return tsInfo[0];
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        WriteErrorMessage("GetSetInfo", e.Message);
        //    }

        //    return null;
        //}

        private void WriteDebugInfo(string strInfo)
        {
            try
            {
                DirectoryInfo clsPath = new DirectoryInfo(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory));
                DirectoryInfo clsSubPath = clsPath.CreateSubdirectory("RunningLog\\LBD_WebApiInterface\\Api");

                if (clsSubPath.Exists)
                {
                    DateTime clsDate = DateTime.Now;
                    string strPath = clsSubPath.FullName + "\\VisitMyRepositoryI(" + String.Format("{0:yyyy-MM-dd}", clsDate) + ").log";
                    StreamWriter clsWriter = new StreamWriter(strPath, true);
                    clsWriter.WriteLine(String.Format("{0:HH:mm:ss}", clsDate) + " " + strInfo);
                    clsWriter.Close();
                }
            }
            catch { }
        }

        private void WriteErrorMessage(string strMethodName, string sErrorMessage)
        {
            try
            {
                DirectoryInfo clsPath = new DirectoryInfo(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory));
                DirectoryInfo clsSubPath = clsPath.CreateSubdirectory("Errlog\\LBD_WebApiInterface\\Api");

                if (clsSubPath.Exists)
                {
                    DateTime clsDate = DateTime.Now;
                    string strPath = clsSubPath.FullName + "\\VisitMyRepositoryI(" + String.Format("{0:yyyy-MM-dd}", clsDate) + ").log";
                    StreamWriter clsWriter = new StreamWriter(strPath, true);
                    clsWriter.WriteLine(String.Format("{0:HH:mm:ss}", clsDate) + " " + strMethodName + ": " + sErrorMessage);
                    clsWriter.Close();
                }
            }
            catch { }
        }
    }
}