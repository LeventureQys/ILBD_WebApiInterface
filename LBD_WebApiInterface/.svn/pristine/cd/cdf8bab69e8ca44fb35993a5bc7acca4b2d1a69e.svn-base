using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LBD_WebApiInterface.Utility;
using LBD_WebApiInterface.Models;
using LBD_WebApiInterface.MultipleSubjectM;
using System.IO;

namespace LBD_WebApiInterface.Api
{
    public class MultipleSubjectI
    {
        /// <summary>
        /// 学科资源库Api的访问地址
        /// </summary>
        public string ApiBaseUrl
        {
            get
            {
                return mApiBaseUrl;
            }          
        }

        private string mApiBaseUrl;
        private string mServiceUrl;
        private CommandApi mCommandApi;

        private bool mInitStatus;



        public MultipleSubjectI()
        {
            mInitStatus = false;
        }

        /*
        public bool Initialize(string strNetTeachIP, string strNetTeachPort)
        {
            try
            {
                if (mInitStatus == true)
                {
                    return false;
                }

                //需要访问云平台获得
                string strMultiSubjectIP = "183.63.90.221";
                string strMultiSubjectPort = "9000";

                mApiBaseUrl = string.Format(Properties.Resources.MultipleSubjectUrl, strMultiSubjectIP, strMultiSubjectPort);
                mServiceUrl = string.Format(Properties.Resources.MultipleSubjectServiceUrl, strMultiSubjectIP, strMultiSubjectPort);

                int iErrorFlag = 8;

                CommandApi c = new CommandApi();
                c.BaseUrl = mServiceUrl;
                //c.ControllerName = "Service";
                string strData = c.CallMethodGet_DXK("CheckConnection", null, out iErrorFlag);

                if (iErrorFlag == 1)
                {
                    mCommandApi = new CommandApi();
                    mCommandApi.BaseUrl = mApiBaseUrl;
                    //mCommandApi.ControllerName = "Subject_Resource";

                    mInitStatus = true;
                }
                else
                {
                    mInitStatus = false;
                }

                return mInitStatus;
            }
            catch (Exception e)
            {
                WriteErrorMessage("Initialize", e.Message);
            }

            return false;
        }
        */

        /// <summary>
        /// 参数为学科资源库的地址
        /// </summary>
        public bool Initialize(string strMultipleSubjectAPIIPAndPort)
        {
            try
            {
                if (mInitStatus == true)
                {
                    return false;
                }

                if (string.IsNullOrEmpty(strMultipleSubjectAPIIPAndPort))
                {
                    return false;
                }

                mApiBaseUrl ="http://" + strMultipleSubjectAPIIPAndPort + "/ExternalInterface/Subject_Resource.ashx";
                mServiceUrl = "http://" + strMultipleSubjectAPIIPAndPort + "/Service.ashx";

                int iErrorFlag = 8;

                CommandApi c = new CommandApi();
                c.BaseUrl = mServiceUrl;
                string strData = c.CallMethodGet_DXK("CheckConnection", null, out iErrorFlag);

                if (iErrorFlag == 1)
                {
                    mCommandApi = new CommandApi();
                    mCommandApi.BaseUrl = mApiBaseUrl;

                    mInitStatus = true;
                }
                else
                {
                    mInitStatus = false;
                }

                return mInitStatus;
            }
            catch (Exception e)
            {
                WriteErrorMessage("Initialize", e.Message);
            }

            return false;
        }

        /// <summary>
        /// 根据学科名字获取学科ID
        /// </summary>
        public int GetSubjectIDByName(string strSubjectName)
        {
            try
            {
                if (string.IsNullOrEmpty(strSubjectName))
                {
                    return -1;
                }

                int iErrorFlag = -1;
                string strData = mCommandApi.CallMethodGet_DXK("GetSubject", null, out iErrorFlag);
                if (string.IsNullOrEmpty(strData))
                {
                    return -1;
                }
                SubjectInfoM[] arrSubject = JsonFormatter.JsonDeserialize<SubjectInfoM[]>(strData);

                //var result = from subject in arrSubject where subject.DirectoryName == strSubjectName select subject;

                if (arrSubject != null)
                {
                    foreach(var subject in arrSubject)
                    {
                        if (subject.DirectoryName == strSubjectName)
                        {
                            return subject.DirectoryID;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetSubjectIDByName", e.Message);
            }
            return -1;
        }

        /// <summary>
        /// 获取年级目录
        /// </summary>
        public GradeInfoM[] GetGradeInfo()
        {
            try
            {
                int iErrorFlag = -1;
                string strData = mCommandApi.CallMethodGet_DXK("GetGradeInfo", null, out iErrorFlag);
                if (string.IsNullOrEmpty(strData))
                {
                    return null;
                }
                GradeInfoM[] arrGrade = JsonFormatter.JsonDeserialize<GradeInfoM[]>(strData);
                return arrGrade;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetGradeInfo", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 获取目录下的子目录或者Resource
        /// <para>(注意：学科ID也算是DirID)</para>
        /// </summary>
        public DirectoryAndResourceInfoM GetChildDirOrResource(int iDirID, int iGradeID)
        {
            try
            {
                if (iDirID < 0 || iGradeID < 0)
                {
                    return null;
                }
                
                int iErrorFlag = -1;
                string[] arrParam = new string[2];
                arrParam[0] = iDirID.ToString();
                arrParam[1] = iGradeID.ToString();
                string strData = mCommandApi.CallMethodGet_DXK("GetDirectoryAndResource", arrParam, out iErrorFlag);
                if (string.IsNullOrEmpty(strData))
                {
                    return null;
                }
                DirectoryAndResourceInfoM[] arrDir= JsonFormatter.JsonDeserialize<DirectoryAndResourceInfoM[]>(strData);
                if (arrDir != null && arrDir.Length == 1)
                {
                    return arrDir[0];
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetChildDirOrResource", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 获取资源详细信息（包括文件和附件）
        /// </summary>
        public ResourceDetailInfoM GetResourceDetail(int iResourceID)
        {
            try
            {
                if (iResourceID < 0)
                {
                    return null;
                }

                int iErrorFlag = -1;
                string[] arrParam = new string[1];
                arrParam[0] = iResourceID.ToString();
                string strData = mCommandApi.CallMethodGet_DXK("GetResourceDetail", arrParam, out iErrorFlag);
                if (string.IsNullOrEmpty(strData))
                {
                    return null;
                }
                ResourceDetailInfoM ResourceDetail = JsonFormatter.JsonDeserialize<ResourceDetailInfoM>(strData);
                if (ResourceDetail != null)
                {
                    return ResourceDetail;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetResourceDetail", e.Message);
            }

            return null;
        }

        /// <summary>
        /// 获取资源文件
        /// </summary>
        public ResourceFileM[] GetResourceFile(int iResourceID)
        {
            try
            {
                if (iResourceID < 0)
                {
                    return null;
                }

                ResourceFileM[] file = null;

                int iErrorFlag = -1;
                string[] arrParam = new string[1];
                arrParam[0] = iResourceID.ToString();
                string strData = mCommandApi.CallMethodGet_DXK("GetResourceDetail", arrParam, out iErrorFlag);
                if (string.IsNullOrEmpty(strData))
                {
                    return null;
                }
                ResourceDetailInfoM[] ResourceDetail = JsonFormatter.JsonDeserialize<ResourceDetailInfoM[]>(strData);
                if (ResourceDetail != null && ResourceDetail.Length==1)
                {
                    List<ResourceFileM> list = new List<ResourceFileM>();
                    if (ResourceDetail[0].Content != null)
                    {
                        foreach (var item in ResourceDetail[0].Content)
                        {
                            ResourceFileM r = new ResourceFileM();
                            r.FileID = item.ContentID;
                            r.ResourceID = item.ResourceID;
                            r.FileName = item.ContentName;
                            r.FilePath = item.FilePath;
                            r.Extension = item.Extension;
                            r.ContentOrAttachment = true;
                            list.Add(r);
                        }
                    }

                    if (ResourceDetail[0].Attachment != null)
                    {
                        foreach (var item in ResourceDetail[0].Attachment)
                        {
                            ResourceFileM r = new ResourceFileM();
                            r.FileID = item.AttachmentID;
                            r.ResourceID = item.ResourceID;
                            r.FileName = item.AttachmentName;
                            r.FilePath = item.FilePath;
                            r.Extension = item.Extension;
                            r.ContentOrAttachment = false;
                            list.Add(r);
                        }
                    }

                    file = list.ToArray();
                }

                return file;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetResourceFile", e.Message);
            }

            return null;
        }
     
        //根据ID获取文件
        public ResourceFileM GetContentFile(int iFileID)
        {
            try
            {
                if (iFileID < 0)
                {
                    return null;
                }

                int iErrorFlag = -1;
                string[] arrParam = new string[1];
                arrParam[0] = iFileID.ToString();
                string strData = mCommandApi.CallMethodGet_DXK("GetResourceContent", arrParam, out iErrorFlag);
                if (string.IsNullOrEmpty(strData))
                {
                    return null;
                }

                ResourceContentInfoM[] content = JsonFormatter.JsonDeserialize<ResourceContentInfoM[]>(strData);
                if (content != null && content.Length == 1)
                {
                    ResourceFileM file = new ResourceFileM();
                    if (content[0] != null)
                    {
                        file.FileID = content[0].ContentID;
                        file.ResourceID = content[0].ResourceID;
                        file.FileName = content[0].ContentName;
                        file.FilePath = content[0].FilePath;
                        file.Extension = content[0].Extension;
                        file.ContentOrAttachment = true;
                    }
                    return file;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetContentFile", e.Message);
            }

            return null;
        }

        //根据ID获取附件
        public ResourceFileM GetAttachmentFile(int iFileID)
        {
            try
            {
                if (iFileID < 0)
                {
                    return null;
                }

                int iErrorFlag = -1;
                string[] arrParam = new string[1];
                arrParam[0] = iFileID.ToString();
                string strData = mCommandApi.CallMethodGet_DXK("GetResourceAttachment", arrParam, out iErrorFlag);
                if (string.IsNullOrEmpty(strData))
                {
                    return null;
                }

                ResourceAttachmentInfoM[] attach = JsonFormatter.JsonDeserialize<ResourceAttachmentInfoM[]>(strData);
                if (attach != null && attach.Length == 1)
                {
                    ResourceFileM file = new ResourceFileM();
                    if (attach[0] != null)
                    {
                        file.FileID = attach[0].AttachmentID;
                        file.ResourceID = attach[0].ResourceID;
                        file.FileName = attach[0].AttachmentName;
                        file.FilePath = attach[0].FilePath;
                        file.Extension = attach[0].Extension;
                        file.ContentOrAttachment = false;
                    }
                    return file;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetAttachmentFile", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 获取资源主要信息
        /// </summary>
        public ResourceInfoM GetResourceMain(int iResourceID)
        {
            try
            {
                if (iResourceID < 0)
                {
                    return null;
                }

                int iErrorFlag = -1;
                string[] arrParam = new string[1];
                arrParam[0] = iResourceID.ToString();
                string strData = mCommandApi.CallMethodGet_DXK("GetResourceMain", arrParam, out iErrorFlag);
                if (string.IsNullOrEmpty(strData))
                {
                    return null;
                }

                ResourceInfoM[] resource = JsonFormatter.JsonDeserialize<ResourceInfoM[]>(strData);

                if (resource != null && resource.Length == 1)
                {
                    return resource[0];
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetResourceMain", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 搜索资源
        /// </summary>
        public ResourceInfoM[] SearchResource(int iGradeID, int iResourceType, string strKeyword)
        {
            try
            {
                int iErrorFlag = -1;
                string[] arrParam = new string[3];
                arrParam[0] = iGradeID.ToString();
                arrParam[1] = iResourceType.ToString();
                arrParam[2] = strKeyword;

                string strData = mCommandApi.CallMethodGet_DXK("SearchResource", arrParam, out iErrorFlag);
                if (string.IsNullOrEmpty(strData))
                {
                    return null;
                }

                ResourceInfoM[] resource = JsonFormatter.JsonDeserialize<ResourceInfoM[]>(strData);

                return resource;
            }
            catch (Exception e)
            {
                WriteErrorMessage("SearchResource", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 搜索资源文件和附件
        /// </summary>
        public ResourceDetailInfoM[] SearchContentAndAttachment(int iGradeID, int iResourceType, string strExtension, string strKeyword)
        {
            try
            {
                int iErrorFlag = -1;

                string[] arrParam = new string[4];
                arrParam[0] = iGradeID.ToString();
                arrParam[1] = iResourceType.ToString();
                arrParam[2] = strExtension;
                arrParam[3] = strKeyword;

                string strData = mCommandApi.CallMethodGet_DXK("SearchResource", arrParam, out iErrorFlag);
                if (string.IsNullOrEmpty(strData))
                {
                    return null;
                }

                ResourceDetailInfoM[] resource = JsonFormatter.JsonDeserialize<ResourceDetailInfoM[]>(strData);

                return resource;
            }
            catch (Exception e)
            {
                WriteErrorMessage("SearchContentAndAttachment", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 返回长度为2的数组，其中string[0]为资源站点本地路径，string[1]为HTTP下载地址
        /// </summary>
        public string[] GetBasicInfo()
        {
            try
            {
                int iErrorFlag = -1;
                BasicInfo[] basicInfo=null;
                string strData = mCommandApi.CallMethodGet_DXK("GetBasicInfo",null, out iErrorFlag);
                if (string.IsNullOrEmpty(strData) == false)
                {
                    basicInfo = JsonFormatter.JsonDeserialize<BasicInfo[]>(strData);
                }

                if (basicInfo != null && basicInfo.Length==1)
                {
                    string[] arrReturn = new string[2];

                    if (basicInfo[0].ResourcePath != null)
                    {
                        arrReturn[0] = basicInfo[0].ResourcePath;
                    }
                    else
                    {
                        arrReturn[0] = "";
                    }

                    if (basicInfo[0].ResourceUrl != null)
                    {
                        arrReturn[1] = basicInfo[0].ResourceUrl;
                    }
                    else
                    {
                        arrReturn[1] = "";
                    }

                    return arrReturn;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetBasicInfo", e.Message);
            }

            return null;
        }

        public DirInfoM[] GetAllDigitizedResourceItem(string strSubjectName)
        {
            try
            {
                //这里是学科资源库的学科ID，与本系统不同
                int iSubjectID = GetSubjectIDByName(strSubjectName);
                //任何一个年级下的第一层子目录应该是一致的
                DirectoryAndResourceInfoM info = GetChildDirOrResource(iSubjectID, 1);
                if (info != null)
                {
                    List<DirInfoM> listDir = info.Dir;
                    if (listDir != null && listDir.Count > 0)
                    {
                        return listDir.ToArray();
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetAllDigitizedResourceItem", e.Message);
            }
            return null;
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
                    string strPath = clsSubPath.FullName + "\\MultipleSubjectI(" + String.Format("{0:yyyy-MM-dd}", clsDate) + ").log";
                    StreamWriter clsWriter = new StreamWriter(strPath, true);
                    clsWriter.WriteLine(String.Format("{0:HH:mm:ss}", clsDate) + " " + strMethodName + ": " + sErrorMessage);
                    clsWriter.Close();
                }
            }
            catch { }
        }

    }
}
