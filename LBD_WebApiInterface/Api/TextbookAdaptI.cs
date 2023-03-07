using LBD_WebApiInterface.Models;
using LBD_WebApiInterface.Utility;
using System;
using System.IO;

namespace LBD_WebApiInterface.Api
{
    public class TextbookAdaptI
    {
        //专用教材表的字段
        public enum BookDataField
        {
            ID = 1,
            BookName = 2,
            BookSize = 5,
            FileNum = 6,
            LastEditTime = 11
        }

        //专用教材排序方式
        public enum BookOrderType
        {
            ASC = 1,
            DESC = 2
        }

        //专用教材适配状态（整形值与API接口所需值相匹配，直接转换成整形值使用即可）
        public enum BookAdaptStatus
        {
            All = -1,//查询所有，不在乎有没有适配
            None = 0,//完全没有适配的
            Some = 1,//部分适配的
            Whole = 2//全部适配的
        }

        private string mApiBaseUrl;
        private CommandApi mCommandApi;

        private bool mInitStatus;

        public TextbookAdaptI()
        {
            mInitStatus = false;
        }

        /// <summary>
        /// 初始化连接
        /// </summary>
        public bool Initialize(string strNetTeachIP, string strNetTeachPort)
        {
            try
            {
                if (mInitStatus == true)
                {
                    return false;
                }

                mApiBaseUrl = string.Format(Properties.Resources.TextbookAdaptUrl, strNetTeachIP, strNetTeachPort);

                //mApiBaseUrl = string.Format("http://{0}:{1}/API/", strNetTeachIP, strNetTeachPort);

                mCommandApi = new CommandApi();
                mCommandApi.BaseUrl = mApiBaseUrl;
                //mCommandApi.ControllerName = "TextbookAdaptApi";

                mInitStatus = true;

                return mInitStatus;
            }
            catch (Exception e)
            {
                WriteErrorMessage("Initialize", e.Message);
            }

            return false;
        }

        //获取教材总数
        public int GetTextbookCount(byte bSubjectID, BookAdaptStatus eAdaptStatus)
        {
            try
            {
                string[] arrParam = new string[2];
                arrParam[0] = bSubjectID.ToString();
                arrParam[1] = ((int)eAdaptStatus).ToString();
                string str = mCommandApi.CallMethodGet("SelectTextbookCount", arrParam);

                if (string.IsNullOrEmpty(str))
                {
                    return -1;
                }
                else
                {
                    int count = Convert.ToInt32(str);
                    return count;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetTextbookCount", e.Message);
            }
            return -1;
        }

        /// <summary>
        ///获取所有的专用教材（教材总数通过数组长度获得）
        /// </summary>
        public PublishingTextbookM[] GetAllTextbook(byte bSubjectID, BookAdaptStatus eAdaptStatus, int iPageSize, int iPageIndex, BookDataField orderField, BookOrderType orderType)
        {
            try
            {
                WriteDebugInfo("GetAllTextbook: " + "bSubjectID=" + bSubjectID + ",eAdaptStatus=" + eAdaptStatus.ToString() + ",iPageSize=" + iPageSize + ",iPageIndex=" + iPageIndex + ",orderField=" + orderField.ToString() + ",orderType=" + orderType.ToString());
                string[] arrParam = new string[6];
                arrParam[0] = bSubjectID.ToString();
                arrParam[1] = ((int)eAdaptStatus).ToString();
                arrParam[2] = iPageSize.ToString();
                arrParam[3] = iPageIndex.ToString();
                switch (orderField)
                {
                    case BookDataField.ID:
                        arrParam[4] = "ID";
                        break;

                    case BookDataField.BookName:
                        arrParam[4] = "TextbookName";
                        break;

                    case BookDataField.BookSize:
                        arrParam[4] = "TextbookSize";
                        break;

                    case BookDataField.FileNum:
                        arrParam[4] = "FileNum";
                        break;

                    case BookDataField.LastEditTime:
                        arrParam[4] = "LastEditTime";
                        break;
                }
                switch (orderType)
                {
                    case BookOrderType.ASC:
                        arrParam[5] = "ASC";
                        break;

                    case BookOrderType.DESC:
                        arrParam[5] = "DESC";
                        break;
                }
                string str = mCommandApi.CallMethodGet("SelectAllTextbook", arrParam);
                WriteDebugInfo("GetAllTextbook返回值=" + str);
                if (string.IsNullOrEmpty(str))
                {
                    return null;
                }

                PublishingTextbookM[] result = JsonFormatter.JsonDeserialize<PublishingTextbookM[]>(str);
                return result;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetAllTextbook", e.Message);
            }
            return null;
        }

        public PublishingTextbookM GetTextbookByID(short ID)
        {
            try
            {
                WriteDebugInfo("GetTextbookByID: ID=" + ID);
                string[] arrParam = new string[1];
                arrParam[0] = ID.ToString();

                string str = mCommandApi.CallMethodGet("SelectTextbookByID", arrParam);
                WriteDebugInfo("GetTextbookByID: 返回值=" + str);
                if (string.IsNullOrEmpty(str))
                {
                    return null;
                }
                PublishingTextbookM[] result = JsonFormatter.JsonDeserialize<PublishingTextbookM[]>(str);
                if (result != null && result.Length == 1)
                {
                    return result[0];
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetTextbookByID", e.Message);
            }

            return null;
        }

        /// <summary>
        /// 获取所有的应用价值
        /// </summary>
        public ValueNodeM[] GetApplyValue(byte bSujectID)
        {
            try
            {
                string[] arrParam = new string[1];
                arrParam[0] = bSujectID.ToString();

                string str = mCommandApi.CallMethodGet("SelectApplyValue", arrParam);
                if (string.IsNullOrEmpty(str))
                {
                    return null;
                }
                ValueNodeM[] result = JsonFormatter.JsonDeserialize<ValueNodeM[]>(str);
                return result;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetApplyValue", e.Message);
            }

            return null;
        }

        /// <summary>
        /// 获取单个应用价值（根据ID）
        /// </summary>
        public ApplyValueM GetSingleApplyValue(short ID)
        {
            try
            {
                if (ID < 0)
                {
                    return null;
                }

                string[] arrParam = new string[1];
                arrParam[0] = ID.ToString();
                string str = mCommandApi.CallMethodGet("SelectSingleValue", arrParam);
                if (string.IsNullOrEmpty(str))
                {
                    return null;
                }
                ApplyValueM result = JsonFormatter.JsonDeserialize<ApplyValueM>(str);
                return result;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetSingleApplyValue", e.Message);
            }

            return null;
        }

        /// <summary>
        /// 获取素材类别
        /// </summary>
        public MaterialCategoryM[] GetMaterialCategory(short sTextbookID)
        {
            try
            {
                string[] arrParam = new string[1];
                arrParam[0] = sTextbookID.ToString();

                string str = mCommandApi.CallMethodGet("SelectMaterialCategory", arrParam);
                if (string.IsNullOrEmpty(str))
                {
                    return null;
                }
                MaterialCategoryM[] result = JsonFormatter.JsonDeserialize<MaterialCategoryM[]>(str);

                return result;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetMaterialCategory", e.Message);
            }

            return null;
        }

        /// <summary>
        /// 根据EnglishID获取教学模式名称
        /// </summary>
        public string GetModeNameByEnglishID(string strTeachModeID)
        {
            try
            {
                if (string.IsNullOrEmpty(strTeachModeID))
                {
                    return null;
                }

                string[] arrParam = new string[1];
                arrParam[0] = strTeachModeID;
                string strName = mCommandApi.CallMethodGet("GetModeNameByEnglishID", arrParam);

                return strName;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetModeNameByEnglishID", e.Message);
            }

            return null;
        }

        /// <summary>
        /// 根据数字ID获取教学模式名称
        /// </summary>
        public string GetModeNameByID(short sTeachModeID)
        {
            try
            {
                if (sTeachModeID < 0)
                {
                    return null;
                }

                string[] arrParam = new string[1];
                arrParam[0] = sTeachModeID.ToString();
                string strName = mCommandApi.CallMethodGet("GetModeNameByID", arrParam);

                return strName;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetModeNameByID", e.Message);
            }

            return null;
        }

        public TeachModeM GetTeachModeByID(short sTeachModeID)
        {
            try
            {
                if (sTeachModeID < 0)
                {
                    return null;
                }

                string[] arrParam = new string[1];
                arrParam[0] = sTeachModeID.ToString();
                string strReturn = mCommandApi.CallMethodGet("GetTeachModeByID", arrParam);

                if (string.IsNullOrEmpty(strReturn))
                {
                    return null;
                }

                TeachModeM[] tm = JsonFormatter.JsonDeserialize<TeachModeM[]>(strReturn);
                if (tm != null && tm.Length == 1)
                {
                    return tm[0];
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetTeachModeByID", e.Message);
            }

            return null;
        }

        //根据素材类别的路径获取此素材类别对应的教学模式
        public TeachModeM GetTeachModeByMaterialPath(short sTextbookID, string strPath)
        {
            try
            {
                WriteDebugInfo("GetTeachModeByMaterialPath: sTextbookID=" + sTextbookID + ", strPath=" + strPath);

                CDirNameHelper dirHelper = new CDirNameHelper();
                string strRootPath = "";
                int index = strPath.IndexOf("\\");
                if (index > -1)
                {
                    strRootPath = strPath.Substring(0, index);
                    strPath = strPath.Substring(index + 1);
                }

                string strTargetPath = dirHelper.GetMatchedDirByLocalDir(strPath);
                if (strTargetPath == null)
                {
                    return null;
                }

                WriteDebugInfo("GetTeachModeByMaterialPath: strTargetPath=" + strTargetPath);

                strTargetPath = strRootPath + "\\" + strTargetPath;
                string[] arrParam = new string[2];
                arrParam[0] = sTextbookID.ToString();
                arrParam[1] = strTargetPath;
                string strReturn = mCommandApi.CallMethodGet("GetTeachModeByMaterialPath", arrParam);

                WriteDebugInfo("GetTeachModeByMaterialPath: strReturn=" + strReturn);

                if (string.IsNullOrEmpty(strReturn))
                {
                    return null;
                }

                TeachModeM[] tm = JsonFormatter.JsonDeserialize<TeachModeM[]>(strReturn);
                if (tm != null && tm.Length == 1)
                {
                    return tm[0];
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetTeachModeByMaterialPath", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 添加专用教材
        /// </summary>
        public short AddTextbook(PublishingTextbookM textbook)
        {
            try
            {
                if (textbook == null)
                {
                    return -1;
                }
                string[] arrParam = new string[1];
                arrParam[0] = JsonFormatter.JsonSerialize(textbook);
                string strReturn = mCommandApi.CallMethodPost("InsertTextbook", arrParam);

                if (string.IsNullOrEmpty(strReturn) == false)
                {
                    textbook.ID = Convert.ToInt16(strReturn);
                    return textbook.ID;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("AddTextbook", e.Message);
            }

            return -1;
        }

        /// <summary>
        /// 添加素材类别
        /// </summary>
        public short[] AddMaterialCategory(MaterialCategoryM[] material)
        {
            try
            {
                if (material == null)
                {
                    return null;
                }

                string[] arrParam = new string[1];
                arrParam[0] = JsonFormatter.JsonSerialize(material);
                string str = mCommandApi.CallMethodPost("InsertMaterialCategory", arrParam);

                if (string.IsNullOrEmpty(str) == false)
                {
                    string[] arrReturn = str.Split(',');
                    short[] arrID = new short[arrReturn.Length];
                    if (arrReturn != null)
                    {
                        for (int i = 0; i < arrReturn.Length; i++)
                        {
                            if (string.IsNullOrEmpty(arrReturn[i]) == false)
                            {
                                material[i].ID = Convert.ToInt16(arrReturn[i]);
                                arrID[i] = material[i].ID;
                            }
                        }
                    }

                    return arrID;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("AddMaterialCategory", e.Message);
            }

            return null;
        }

        /// <summary>
        /// 更新专用教材（不需要的字段赋值为null，ID和LastEditor为必须参数）
        /// </summary>
        public int UpdateTextbook(short ID, string strTextbookName, string strTextbookPath, string strPhotoPath, long? lTextbookSize, int? iFileNum, byte? bAdaptStatus, string strLastEditor)
        {
            try
            {
                if (ID <= 0)
                {
                    return 0;
                }

                string[] arrParam = new string[8];
                arrParam[0] = ID.ToString();
                arrParam[1] = strTextbookName;
                arrParam[2] = strTextbookPath;
                arrParam[3] = strPhotoPath;
                if (lTextbookSize.HasValue)
                {
                    arrParam[4] = lTextbookSize.ToString();
                }
                else
                {
                    arrParam[4] = "";
                }
                if (iFileNum.HasValue)
                {
                    arrParam[5] = iFileNum.ToString();
                }
                else
                {
                    arrParam[5] = "";
                }
                if (bAdaptStatus.HasValue)
                {
                    arrParam[6] = bAdaptStatus.ToString();
                }
                else
                {
                    arrParam[6] = "";
                }
                arrParam[7] = strLastEditor;

                string str = mCommandApi.CallMethodPut("UpdateTextbook", arrParam);
                if (string.IsNullOrEmpty(str) == false)
                {
                    return Convert.ToInt32(str);
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("UpdateTextbook", e.Message);
            }

            return -1;
        }

        /// <summary>
        /// 更新素材类别（不需要的字段赋值为null，ID和LastEditor为必须参数）
        /// </summary>
        public int UpdateMaterialCategory(short ID, short? sApplyValueID, short? sTeachModeID, byte? bLevelNo, string strDirPath, string strLastEditor)
        {
            try
            {
                if (ID <= 0)
                {
                    return 0;
                }

                string[] arrParam = new string[6];
                arrParam[0] = ID.ToString();
                if (sApplyValueID.HasValue)
                {
                    arrParam[1] = sApplyValueID.Value.ToString();
                }
                else
                {
                    arrParam[1] = "";
                }

                if (sTeachModeID.HasValue)
                {
                    arrParam[2] = sTeachModeID.Value.ToString();
                }
                else
                {
                    arrParam[2] = "";
                }

                if (bLevelNo.HasValue)
                {
                    arrParam[3] = bLevelNo.Value.ToString();
                }
                else
                {
                    arrParam[3] = "";
                }

                arrParam[4] = strDirPath;
                arrParam[5] = strLastEditor;

                string str = mCommandApi.CallMethodPut("UpdateMaterialCategory", arrParam);
                if (string.IsNullOrEmpty(str) == false)
                {
                    return Convert.ToInt32(str);
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("UpdateMaterialCategory", e.Message);
            }

            return -1;
        }

        /// <summary>
        /// 删除专用教材
        /// </summary>
        public int DeleteTextbook(short ID)
        {
            try
            {
                if (ID < 0)
                {
                    return 0;
                }

                string[] arrParam = new string[1];
                arrParam[0] = ID.ToString();
                string str = mCommandApi.CallMethodDelete("DelTextbook", arrParam);
                if (string.IsNullOrEmpty(str) == false)
                {
                    return Convert.ToInt32(str);
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("DeleteTextbook", e.Message);
            }

            return -1;
        }

        /// <summary>
        /// 删除素材类别
        /// </summary>
        public int DeleteMaterialCategory(short ID)
        {
            try
            {
                if (ID < 0)
                {
                    return 0;
                }

                string[] arrParam = new string[1];
                arrParam[0] = ID.ToString();
                string str = mCommandApi.CallMethodDelete("DelMaterialCategory", arrParam);
                if (string.IsNullOrEmpty(str) == false)
                {
                    return Convert.ToInt32(str);
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("DeleteMaterialCategory", e.Message);
            }

            return -1;
        }

        /// <summary>
        /// 删除指定专用教材下的所有素材类别
        /// </summary>
        public int DeleteMaterialCategoryByTextbookID(short sTextbookID)
        {
            try
            {
                if (sTextbookID <= 0)
                {
                    return 0;
                }

                string[] arrParam = new string[1];
                arrParam[0] = sTextbookID.ToString();
                string str = mCommandApi.CallMethodDelete("DelMaterialCategoryByTextbookID", arrParam);
                if (string.IsNullOrEmpty(str) == false)
                {
                    return Convert.ToInt32(str);
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("DeleteMaterialCategoryByTextbookID", e.Message);
            }

            return -1;
        }

        private void WriteDebugInfo(string strInfo)
        {
            try
            {
                DirectoryInfo clsPath = new DirectoryInfo(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory));
                DirectoryInfo clsSubPath = clsPath.CreateSubdirectory("Errlog\\测试信息\\LBD_WebApiInterface\\Api");

                if (clsSubPath.Exists)
                {
                    DateTime clsDate = DateTime.Now;
                    string strPath = clsSubPath.FullName + "\\TextbookAdaptI(" + String.Format("{0:yyyy-MM-dd}", clsDate) + ").log";
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
                    string strPath = clsSubPath.FullName + "\\TextbookAdaptI(" + String.Format("{0:yyyy-MM-dd}", clsDate) + ").log";
                    StreamWriter clsWriter = new StreamWriter(strPath, true);
                    clsWriter.WriteLine(String.Format("{0:HH:mm:ss}", clsDate) + " " + strMethodName + ": " + sErrorMessage);
                    clsWriter.Close();
                }
            }
            catch { }
        }
    }
}