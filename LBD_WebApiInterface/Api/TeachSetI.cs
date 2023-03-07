using lancoo.cp.basic.sysbaseclass;
using LBD_WebApiInterface.Models;
using LBD_WebApiInterface.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LBD_WebApiInterface.Api
{
    public class TeachSetI
    {
        /// <summary>
        /// 教学定制相关接口的访问地址
        /// </summary>
        public string ApiBaseUrl
        {
            get
            {
                return mApiBaseUrl;
            }
        }

        public enum E_SetItem
        {
            我的资料库 = 1,
            我的课本 = 2,
            更多系统 = 3,
            本地电脑常用目录 = 4,
            U盘常用目录 = 5,

            //个人网盘绑定=6,
            新建课件引导提示 = 7,

            数字化资源库重命名 = 8,
            专用教材引导提示 = 9,
            个人库定制提示 = 10,
            个人库定制功能引导提示 = 11,
            个人库的个人资料新增 = 12,
            个人库定制结果 = 13,
            教学应用库定制提示 = 14,
            教学应用库定制功能引导提示 = 15,
            教学应用库的电子书新增 = 16,
            教学应用库定制结果 = 17,
            学生重难点标注功能引导提示 = 18,
            智慧云网络室定制界面 = 19,
            锁控教室间数 = 20
        }

        private string mApiBaseUrl;
        private CommandApi mCommandApi;

        private bool mInitStatus;

        public TeachSetI()
        {
            mInitStatus = false;
        }

        /// <summary>
        /// 手动初始化教学设置类
        /// </summary>
        /// <param name="strNetTeachIP">课堂教学IP</param>
        /// <param name="strNetTeachPort">课堂教学端口</param>
        /// <param name="strNetTeachVirDir">应用程序名，非聚合版本传空</param>
        /// <returns></returns>
        public bool Initialize(string strNetTeachIP, string strNetTeachPort, string strNetTeachVirDir)
        {
            try
            {
                if (mInitStatus == true)
                {
                    return false;
                }
                if (string.IsNullOrEmpty(strNetTeachVirDir) == false)//strNetTeachVirDir非空
                    if (strNetTeachVirDir.EndsWith("/") == false) strNetTeachVirDir = strNetTeachVirDir + "/";
                mApiBaseUrl = string.Format(Properties.Resources.TeachSetUrl, strNetTeachIP, strNetTeachPort, strNetTeachVirDir);

                mCommandApi = new CommandApi();
                mCommandApi.BaseUrl = mApiBaseUrl;

                mInitStatus = true;

                return mInitStatus;
            }
            catch (Exception e)
            {
                WriteErrorMessage("Initialize", e.Message);
            }

            return false;
        }

        //获取所有设置项（仅针对需要在Web端显示的设置项）
        public TeachSetItemM[] GetAllSetItem()
        {
            try
            {
                string strData = mCommandApi.CallMethodGet("SelectAllSetItem", null);
                if (string.IsNullOrEmpty(strData))
                {
                    return null;
                }
                TeachSetItemM[] arrSetItem = JsonFormatter.JsonDeserialize<TeachSetItemM[]>(strData);
                return arrSetItem;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetAllSetItem", e.Message);
            }

            return null;
        }

        public TeacherSetInfoM[] GetTeacherSetValue(byte bSubjectID, string strTeacherID, short? sSetItemID, string strCourseClassID)
        {
            try
            {
                if (bSubjectID <= 0 || string.IsNullOrEmpty(strTeacherID))
                {
                    return null;
                }

                string[] arrParam = new string[5];
                arrParam[0] = bSubjectID.ToString();
                arrParam[1] = strTeacherID;
                if (sSetItemID.HasValue)
                {
                    arrParam[2] = sSetItemID.Value.ToString();
                }
                else
                {
                    arrParam[2] = "";
                }
                arrParam[3] = strCourseClassID;
                arrParam[4] = "0";

                string strData = mCommandApi.CallMethodGet("SelectItemValue", arrParam);
                if (string.IsNullOrEmpty(strData))
                {
                    return null;
                }

                TeacherSetInfoM[] arrSetValue = JsonFormatter.JsonDeserialize<TeacherSetInfoM[]>(strData);
                return arrSetValue;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetTeacherSetValue", e.Message);
            }

            return null;
        }

        // 添加设置内容(参数实体对象中要填写字段CourseClassID)
        public string AddTeacherSetValue(TeacherSetInfoM[] arrSetValue)
        {
            try
            {
                if (arrSetValue == null || arrSetValue.Length == 0)
                {
                    return "";
                }

                string[] arrParam = new string[1];
                arrParam[0] = JsonFormatter.JsonSerialize(arrSetValue);
                WriteErrorMessage("【测试】AddTeacherSetValue被调用了", "参数=" + arrParam[0]);

                string strReturn = mCommandApi.CallMethodPost("InsertItemValue", arrParam);

                if (strReturn != null)
                {
                    string[] arrID = strReturn.Split('|');
                    if (arrID != null)
                    {
                        for (int i = 0; i < arrSetValue.Length; i++)
                        {
                            if (string.IsNullOrEmpty(arrID[i]))
                            {
                                arrSetValue[i].ID = -1L;
                            }
                            else
                            {
                                arrSetValue[i].ID = Convert.ToInt64(arrID[i]);
                            }
                        }
                    }

                    return strReturn;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("AddTeacherSetValue", e.Message);
            }
            return null;
        }

        //
        public int UpdateTeacherSetValue(string strItemValue, long ID)
        {
            try
            {
                WriteErrorMessage("【测试】UpdateTeacherSetValue被调用了", "ID=" + ID + ",Value=" + strItemValue);
                string[] arrParam = new string[4];
                arrParam[0] = strItemValue;
                arrParam[1] = ID.ToString();

                string strReturn = mCommandApi.CallMethodPut("UpdateItemValue", arrParam);
                return Convert.ToInt32(strReturn);
            }
            catch (Exception e)
            {
                WriteErrorMessage("UpdateTeacherSetValue", e.Message);
            }
            return -1;
        }

        public int DeleteItemValue(byte bSubjectID, string strTeacherID)
        {
            try
            {
                string[] arrParam = new string[2];
                arrParam[0] = bSubjectID.ToString();
                arrParam[1] = strTeacherID.ToString();
                string strReturn = mCommandApi.CallMethodDelete("DelItemValue", arrParam);
                return Convert.ToInt32(strReturn);
            }
            catch (Exception e)
            {
                WriteErrorMessage("DeleteItemValue", e.Message);
            }

            return -1;
        }

        public int DeleteSingleItemValue(long ID)
        {
            try
            {
                string[] arrParam = new string[1];
                arrParam[0] = ID.ToString();
                string strReturn = mCommandApi.CallMethodDelete("DelSingleItemValue", arrParam);
                return Convert.ToInt32(strReturn);
            }
            catch (Exception e)
            {
                WriteErrorMessage("DeleteSingleItemValue", e.Message);
            }

            return -1;
        }

        #region 定制“我的资料库”相关

        //定制“我的资料库”，获取所有的子库
        public DigitizedResourceItemM[] GetAllDigitizedResourceItem(byte bMySubjectID, string strTeacherID, string strMultipleSubjectAPIIPAndPort, string strCourseClassID)
        {
            try
            {
                string strSubjectName = GetMySubjectName(bMySubjectID);
                MultipleSubjectI ms = new MultipleSubjectI();
                ms.Initialize(strMultipleSubjectAPIIPAndPort);
                //这里是学科资源库的学科ID，与本系统不同
                int iSubjectID = ms.GetSubjectIDByName(strSubjectName);
                //任何一个年级下的第一层子目录应该是一致的
                MultipleSubjectM.DirectoryAndResourceInfoM info = ms.GetChildDirOrResource(iSubjectID, 1);
                if (info != null)
                {
                    List<MultipleSubjectM.DirInfoM> listDir = info.Dir;
                    if (listDir != null && listDir.Count > 0)
                    {
                        //获取教师对资料库的重命名信息
                        Dictionary<string, string> dicResetName = new Dictionary<string, string>();
                        short sItemID = GetSetItemID(E_SetItem.数字化资源库重命名);
                        TeacherSetInfoM[] teacherSetInfo = GetTeacherSetValue(bMySubjectID, strTeacherID, sItemID, strCourseClassID);
                        if (teacherSetInfo != null && teacherSetInfo.Length == 1)
                        {
                            string strResetName = teacherSetInfo[0].SetItemValue;
                            if (string.IsNullOrEmpty(strResetName) == false)
                            {
                                string[] arrResetName = strResetName.Split((char)1);
                                for (int i = 0; i < arrResetName.Length; i++)
                                {
                                    string[] arrTemp = arrResetName[i].Split((char)2);
                                    dicResetName.Add(arrTemp[0], arrTemp[1]);
                                }
                            }
                        }

                        DigitizedResourceItemM[] arr = new DigitizedResourceItemM[listDir.Count];
                        //如果重命名列表不为空
                        if (dicResetName != null && dicResetName.Count > 0)
                        {
                            for (int i = 0; i < listDir.Count; i++)
                            {
                                DigitizedResourceItemM dr = new DigitizedResourceItemM();
                                dr.ItemID = listDir[i].DirectoryID.ToString();
                                dr.ItemName = listDir[i].DirectoryName;
                                dr.PhotoPath = listDir[i].IconPath;

                                //替换成老师重命名后的名称
                                if (dicResetName.ContainsKey(dr.ItemID))
                                {
                                    dr.ItemName = dicResetName[dr.ItemID];
                                }

                                arr[i] = dr;
                            }
                        }
                        //如果重命名列表为空
                        else
                        {
                            for (int i = 0; i < listDir.Count; i++)
                            {
                                DigitizedResourceItemM dr = new DigitizedResourceItemM();
                                dr.ItemID = listDir[i].DirectoryID.ToString();
                                dr.ItemName = listDir[i].DirectoryName;
                                dr.PhotoPath = listDir[i].IconPath;

                                arr[i] = dr;
                            }
                        }
                        string strArr = JsonFormatter.JsonSerialize(arr);
                        WriteDebugInfo("GetAllDigitizedResourceItem返回值： " + strArr);

                        return arr;
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetAllDigitizedResourceItem", e.Message);
            }
            return null;
        }

        //重设“我的资料库”中子库的名称
        public bool ResetDigitResLibraryName(byte bSubjectID, string strTeacherID, string[] arrLibraryID, string[] arrLibraryName, string strCourseClassID)
        {
            try
            {
                if (arrLibraryID == null || arrLibraryID.Length == 0)
                {
                    return false;
                }
                if (arrLibraryName == null || arrLibraryName.Length == 0)
                {
                    return false;
                }
                if (arrLibraryID.Length != arrLibraryName.Length)
                {
                    return false;
                }

                StringBuilder sbLibrary = new StringBuilder();
                for (int i = 0; i < arrLibraryID.Length; i++)
                {
                    sbLibrary.Append((char)1);
                    sbLibrary.Append(arrLibraryID[i] + (char)2 + arrLibraryName[i]);
                }
                if (sbLibrary.Length > 0)
                {
                    sbLibrary = sbLibrary.Remove(0, 1);
                }

                if (sbLibrary.Length == 0)
                {
                    return false;
                }

                SetItemValueSingle(E_SetItem.数字化资源库重命名, bSubjectID, strTeacherID, sbLibrary.ToString(), strCourseClassID, 0);

                return true;
            }
            catch (Exception e)
            {
                WriteErrorMessage("ResetDigitResLibraryName", e.Message);
            }

            return false;
        }

        private string GetMySubjectName(byte bMySubjectID)
        {
            try
            {
                string strSubjectName = "";
                switch (bMySubjectID)
                {
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
                WriteErrorMessage("GetMySubjectName", e.Message);
            }
            return "";
        }

        #endregion 定制“我的资料库”相关

        /// <summary>
        /// 查找父目录
        /// </summary>
        public LGZXDirM GetJiaoCaiParentDirectory(string strDirID)
        {
            try
            {
                if (string.IsNullOrEmpty(strDirID))
                {
                    return null;
                }

                string[] arrParam = new string[1];
                arrParam[0] = strDirID;

                string strData = mCommandApi.CallMethodGet("SelectLgzxParentDir", arrParam);
                if (string.IsNullOrEmpty(strData))
                {
                    return null;
                }
                LGZXDirM arrDir = JsonFormatter.JsonDeserialize<LGZXDirM>(strData);
                return arrDir;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetJiaoCaiParentDirectory", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 根据DirID查找当前目录详细信息
        /// </summary>
        public LGZXDirM GetJiaoCaiDirectory(string strDirID)
        {
            try
            {
                if (string.IsNullOrEmpty(strDirID))
                {
                    return null;
                }

                string[] arrParam = new string[1];
                arrParam[0] = strDirID;

                string strData = mCommandApi.CallMethodGet("SelectLgzxDir", arrParam);
                if (string.IsNullOrEmpty(strData))
                {
                    return null;
                }
                LGZXDirM arrDir = JsonFormatter.JsonDeserialize<LGZXDirM>(strData);
                return arrDir;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetJiaoCaiDirectory", e.Message);
            }
            return null;
        }

        //查找教材第一层目录
        //iSchoolType指定不同的学校类别不同的查询结果
        //arrIsDirCustomizable与返回值数组等长，指示每个教材目录是否可定制
        public LGZXDirM[] GetJiaoCaiFirstDirectory(int iSchoolType, out bool[] arrIsDirCustomizable)
        {
            arrIsDirCustomizable = null;
            try
            {
                if (iSchoolType <= 0)
                {
                    return null;
                }

                string[] arrParam = new string[1];
                arrParam[0] = iSchoolType.ToString();

                string strData = mCommandApi.CallMethodGet("SelectLgzxFirstDir", arrParam);
                if (string.IsNullOrEmpty(strData))
                {
                    return null;
                }

                string[] arrData = strData.Split((char)1);
                arrIsDirCustomizable = JsonFormatter.JsonDeserialize<bool[]>(arrData[0]);
                LGZXDirM[] arrDir = JsonFormatter.JsonDeserialize<LGZXDirM[]>(arrData[1]);

                return arrDir;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetJiaoCaiFirstDirectory", e.Message);
            }
            return null;
        }

        /*
                //判断一个目录是否能定制（能定制则不能进入子目录）
                public bool IsDirCustomizable(string strDirID, bool bIsJiaoCai, bool HasSubDir)
                {
                    try
                    {
                        bool bCanCustomizable = false;
                        if (bIsJiaoCai == true)
                        {
                            if (HasSubDir == true)
                            {
                                LGZXDirM[] lgzxDir = GetJiaoCaiChildDirectory(strDirID);
                                if (lgzxDir == null || lgzxDir.Length == 0)
                                {
                                    bCanCustomizable = true;
                                }
                                else
                                {
                                    //若子目录不是教材，则可定制
                                    if (lgzxDir[0].IsJiaoCai == 0)
                                    {
                                        bCanCustomizable = true;
                                    }
                                }
                            }
                            else
                            {
                                bCanCustomizable = true;
                            }
                        }
                        else
                        {
                            bCanCustomizable = false;
                        }

                        return bCanCustomizable;
                    }
                    catch (Exception e)
                    {
                        WriteErrorMessage("IsDirCustomizable", e.Message);
                    }
                    return false;
                }
        */

        /// <summary>
        /// 查找子目录
        /// </summary>
        public LGZXDirM[] GetJiaoCaiChildDirectory(string strDirID, out bool[] arrIsDirCustomizable)
        {
            arrIsDirCustomizable = null;
            try
            {
                if (string.IsNullOrEmpty(strDirID))
                {
                    return null;
                }

                string[] arrParam = new string[1];
                arrParam[0] = strDirID;

                string strData = mCommandApi.CallMethodGet("SelectLgzxChildDir", arrParam);
                if (string.IsNullOrEmpty(strData))
                {
                    return null;
                }
                string[] arrData = strData.Split((char)1);
                arrIsDirCustomizable = JsonFormatter.JsonDeserialize<bool[]>(arrData[0]);
                LGZXDirM[] arrDir = JsonFormatter.JsonDeserialize<LGZXDirM[]>(arrData[1]);
                return arrDir;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetJiaoCaiChildDirectory", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 查找父级所有目录
        /// </summary>
        public LGZXDirM[] GetJiaoCaiParentsDirectory(string strDirID, out bool[] arrIsDirCustomizable)
        {
            arrIsDirCustomizable = null;
            try
            {
                if (string.IsNullOrEmpty(strDirID))
                {
                    return null;
                }

                string[] arrParam = new string[1];
                arrParam[0] = strDirID;

                string strData = mCommandApi.CallMethodGet("SelectLgzxParentsDir", arrParam);
                if (string.IsNullOrEmpty(strData))
                {
                    return null;
                }
                string[] arrData = strData.Split((char)1);
                arrIsDirCustomizable = JsonFormatter.JsonDeserialize<bool[]>(arrData[0]);
                LGZXDirM[] arrDir = JsonFormatter.JsonDeserialize<LGZXDirM[]>(arrData[1]);

                return arrDir;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetJiaoCaiParentsDirectory", e.Message);
            }

            return null;
        }

        //获取老师默认推荐的教材
        public LGZXDirM[] GetJiaoCaiDefault(byte bSubjectID, int iSchoolType, out bool[] arrIsDirCustomizable)
        {
            arrIsDirCustomizable = null;
            try
            {
                if (iSchoolType <= 0)
                {
                    return null;
                }

                string[] arrParam = new string[1];
                //arrParam[0] = bSubjectID.ToString();
                arrParam[0] = iSchoolType.ToString();

                string strData = mCommandApi.CallMethodGet("GetLgzxDefaultValue", arrParam);
                if (string.IsNullOrEmpty(strData))
                {
                    return null;
                }
                string[] arrData = strData.Split((char)1);
                arrIsDirCustomizable = JsonFormatter.JsonDeserialize<bool[]>(arrData[0]);
                LGZXDirM[] arrDir = JsonFormatter.JsonDeserialize<LGZXDirM[]>(arrData[1]);
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetJiaoCaiDefault", e.Message);
            }

            return null;
        }

        //获取所有应用系统（不是老师定制的）
        public OuterSystemM[] GetAllApplySystem()
        {
            try
            {
                string strData = mCommandApi.CallMethodGet("SelectAllOuterSystem", null);
                if (string.IsNullOrEmpty(strData))
                {
                    return null;
                }
                OuterSystemM[] arrSys = JsonFormatter.JsonDeserialize<OuterSystemM[]>(strData);

                return arrSys;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetAllApplySystem", e.Message);
            }
            return null;
        }

        //根据学科获取所有应用系统
        public OuterSystemM[] GetAllApplySystemBySubject(byte bSubjectID)
        {
            try
            {
                string[] arrParam = new string[1];
                arrParam[0] = bSubjectID.ToString();

                string strData = mCommandApi.CallMethodGet("SelectAllOuterSystemBySubject", arrParam);
                if (string.IsNullOrEmpty(strData))
                {
                    return null;
                }
                OuterSystemM[] arrSys = JsonFormatter.JsonDeserialize<OuterSystemM[]>(strData);

                return arrSys;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetAllApplySystemBySubject", e.Message);
            }
            return null;
        }

        //获取老师是否点击了网络化课件的“我知道了”
        private bool GetUserHasKnown(byte bSubjectID, string strTeacherID)
        {
            try
            {
                bool bReturn = false;

                string[] arrParam = new string[3];
                arrParam[0] = bSubjectID.ToString();
                arrParam[1] = strTeacherID;
                short sID = GetItemIDByName("新建课件引导提示");
                if (sID > 0)
                {
                    arrParam[2] = sID.ToString();
                }
                else
                {
                    arrParam[2] = "";
                }
                string strData = mCommandApi.CallMethodGet("SelectItemValue", arrParam);
                if (string.IsNullOrEmpty(strData) == false)
                {
                    TeacherSetInfoM[] info = JsonFormatter.JsonDeserialize<TeacherSetInfoM[]>(strData);
                    if (info != null && info.Length > 0)
                    {
                        string strValue = info[0].SetItemValue;
                        if (string.IsNullOrEmpty(strValue) == false)
                        {
                            bReturn = Convert.ToBoolean(strValue);
                            return bReturn;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetUserHasKnown", e.Message);
            }

            return false;
        }

        //设置某个设置项的值（无论是新增还是更新），返回值为ID
        public long SetItemValueSingle(E_SetItem eSetItem, byte bSubjectID, string strTeacherID, string strValue, string strCourseClassID, short sClassroomIndex)
        {
            try
            {
                string[] arrParam = new string[1];

                TeacherSetInfoM[] tsm = new TeacherSetInfoM[1];

                TeacherSetInfoM tsim = new TeacherSetInfoM();
                short setItemID = GetSetItemID(eSetItem);
                tsim.SetItemID = setItemID;
                tsim.SetItemValue = strValue;
                tsim.SubjectID = bSubjectID;
                tsim.TeachID = strTeacherID;
                tsim.CoursePlanID = strCourseClassID;
                tsim.ClassIndex = sClassroomIndex;

                tsm[0] = tsim;
                arrParam[0] = JsonFormatter.JsonSerialize(tsm);

                //WriteDebugInfo("开始调用InsertItemValue Ws接口");
                string strReturn = mCommandApi.CallMethodPost("InsertItemValue", arrParam);
                //WriteDebugInfo("调用InsertItemValue Ws接口结束");
                if (string.IsNullOrEmpty(strReturn))
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt64(strReturn);
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("SetItemValueSingle", e.Message);
            }

            return 0;
        }

        //获取某个设置项的值
        public string GetItemValueSingle(E_SetItem eSetItem, byte bSubjectID, string strTeacherID, string strCourseClassID, short sClassroomIndex)
        {
            try
            {
                string[] arrParam = new string[5];
                arrParam[0] = bSubjectID.ToString();
                arrParam[1] = strTeacherID;
                arrParam[2] = GetSetItemID(eSetItem).ToString();
                arrParam[3] = strCourseClassID;
                arrParam[4] = sClassroomIndex.ToString();

                string strData = mCommandApi.CallMethodGet("SelectItemValue", arrParam);
                if (string.IsNullOrEmpty(strData) == false)
                {
                    TeacherSetInfoM[] info = JsonFormatter.JsonDeserialize<TeacherSetInfoM[]>(strData);
                    if (info != null && info.Length > 0)
                    {
                        string strValue = info[0].SetItemValue;
                        return strValue;
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetItemValueSingle", e.Message);
            }

            return "";
        }

        //返回值不同，若查询失败则返回null而不是string.Empty
        public string GetItemValueSingle2(E_SetItem eSetItem, byte bSubjectID, string strTeacherID, string strCourseClassID, short sClassroomIndex)
        {
            try
            {
                string[] arrParam = new string[5];
                arrParam[0] = bSubjectID.ToString();
                arrParam[1] = strTeacherID;
                arrParam[2] = GetSetItemID(eSetItem).ToString();
                arrParam[3] = strCourseClassID;
                arrParam[4] = sClassroomIndex.ToString();

                //WriteDebugInfo("开始调用SelectItemValue WS接口");
                string strData = mCommandApi.CallMethodGet("SelectItemValue", arrParam);
                //WriteDebugInfo("调用SelectItemValue WS接口结束");
                if (string.IsNullOrEmpty(strData) == false)
                {
                    TeacherSetInfoM[] info = JsonFormatter.JsonDeserialize<TeacherSetInfoM[]>(strData);
                    if (info != null && info.Length > 0)
                    {
                        string strValue = info[0].SetItemValue;
                        return strValue;
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetItemValueSingle2", e.Message);
            }

            return null;
        }

        /// <summary>
        /// 判断某教室是否正常授权
        /// </summary>
        /// <param name="sMacAddr">授权机器Mac地址，12位字符，4439C4333006</param>
        /// <param name="iAuthorizeTotal">额定授权总数</param>
        /// <param name="sErrTips">错误提示内容</param>
        /// <returns>授权成功或失败</returns>
        public bool IsAuthorizePass(string sMacAddr, int iAuthorizeTotal, out string sErrTips)
        {
            //TODO：双网卡传入的mac地址为xxx|yyy的格式，不能直接将这串字符串上传到服务器，需要先切分判断，再考虑上传！
            //这里情况比较复杂，详情见博客https://leventureqys.github.io//2022/10/27/Debug/
            sErrTips = "";
            try
            {
                List<System.String> strMacAddrs = new List<System.String>();

                if (sMacAddr.Contains('|'))
                {
                    strMacAddrs = sMacAddr.Split('|').ToList();
                }

                if (iAuthorizeTotal < 1)
                {
                    sErrTips = "加密锁授权点数不足！";
                    return false;
                }
                string sRtn = GetItemValueSingle2(E_SetItem.锁控教室间数, 0, "0", "0", 0);
                sRtn = CP_EncryptHelper.DecryptCode("20220311", sRtn);//解密
                if ((string.IsNullOrEmpty(sRtn) == true) && (iAuthorizeTotal >= 1))//还没有教室被授权使用
                {
                    //写保存sMacAddr代码
                    //TODO:第一次基础配置即初始化的时候需要工地保证仅有一个网卡，否则这里的设计是有问题的
                    SetItemValueSingle(E_SetItem.锁控教室间数, 0, "0", CP_EncryptHelper.EncryptCode("20220311", sMacAddr), "0", 0);
                    return true;
                }

                string[] arrSplitRtn = sRtn.Split('*');
                if (arrSplitRtn.Length > iAuthorizeTotal)//这是锁点数变少的情况（如原来学校是试用5个点，实际购买正式使用只习了2个点），重置
                {
                    //写保存sMacAddr代码
                    SetItemValueSingle(E_SetItem.锁控教室间数, 0, "0", CP_EncryptHelper.EncryptCode("20220311", sMacAddr), "0", 0);
                    return true;
                }
                bool bFind = false;
                if (arrSplitRtn != null)
                {
                    for (int i = 0; i < arrSplitRtn.Length; i++)
                    {
                        //单双网卡登记相等的情况下，则直接判对即可。
                        if (arrSplitRtn[i] == sMacAddr)
                        {
                            bFind = true;
                            break;
                        }

                        List<System.String> strReturns = new List<System.String>();
                        if (arrSplitRtn[i].Contains('|'))
                        {
                            strReturns = arrSplitRtn[i].Split('|').ToList();
                        }

                        //登记单网卡，实际双网卡的情况
                        if (strMacAddrs.Count > 0 && strReturns.Count == 0)
                        {
                            for (int j = 0; j < strMacAddrs.Count; ++j)
                            {
                                if (arrSplitRtn[i] == strMacAddrs[j])
                                {
                                    bFind = true;
                                    break;
                                }
                            }
                        }

                        //登记双网卡，实际单网卡的情况
                        if (strReturns.Count() > 0 && strMacAddrs.Count == 0)
                        {
                            //此时返回确定，同时从原字符串中擦除本条记录，并插入当前mac地址
                            for (int j = 0; j < strReturns.Count; ++j)
                            {
                                if (strReturns[j] == sMacAddr)
                                {
                                    bFind = true;
                                    sRtn = sRtn.Replace(arrSplitRtn[i], sMacAddr);
                                    SetItemValueSingle(E_SetItem.锁控教室间数, 0, "0", CP_EncryptHelper.EncryptCode("20220311", sRtn), "0", 0);
                                    break;
                                }
                            }
                        }

                        //登记双网卡，实际双网卡的情况，需要抹掉前一个替换网卡mac地址
                        if (strReturns.Count > 0 && strMacAddrs.Count > 0)
                        {
                            for (int j = 0; j < strMacAddrs.Count; ++j)
                            {
                                for (int m = 0; m < strReturns.Count; ++m)
                                {
                                    if (strMacAddrs[j] == strReturns[m])
                                    {
                                        bFind = true;
                                        sRtn = sRtn.Replace(arrSplitRtn[i], sMacAddr);
                                        SetItemValueSingle(E_SetItem.锁控教室间数, 0, "0", CP_EncryptHelper.EncryptCode("20220311", sRtn), "0", 0);
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    if (bFind)//已授权机器码
                    {
                        return true;
                    }
                    else
                    {
                        if (arrSplitRtn.Length < iAuthorizeTotal)//已授权数小于额定授权数
                        {
                            //写保存sMacAddr代码
                            //无论单双网卡，第一次上线都保存当前的数据情况
                            SetItemValueSingle(E_SetItem.锁控教室间数, 0, "0", CP_EncryptHelper.EncryptCode("20220311", sRtn + "*" + sMacAddr), "0", 0);
                            return true;
                        }
                        else
                        {
                            sErrTips = "加密锁授权点数不足！";
                            return false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("IsAuthorizePass", e.Message);
                sErrTips = "未知错误！" + e.Message;
            }
            return false;
        }

        //获取教学应用系统设置值
        private string GetOuterSystemSet(byte bSubjectID, string strTeacherID)
        {
            try
            {
                string[] arrParam = new string[3];
                arrParam[0] = bSubjectID.ToString();
                arrParam[1] = strTeacherID;
                short sID = GetItemIDByName("教学相关应用系统");
                if (sID > 0)
                {
                    arrParam[2] = sID.ToString();
                }
                else
                {
                    arrParam[2] = "";
                }
                string strData = mCommandApi.CallMethodGet("SelectItemValue", arrParam);

                string strValue = null;
                if (string.IsNullOrEmpty(strData) == false)
                {
                    TeacherSetInfoM[] info = JsonFormatter.JsonDeserialize<TeacherSetInfoM[]>(strData);
                    if (info != null && info.Length > 0)
                    {
                        strValue = info[0].SetItemValue;
                    }
                }
                else
                {
                    strValue = null;
                }

                return strValue;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetOuterSystemSet", e.Message);
            }

            return null;
        }

        private short GetItemIDByName(string strName)
        {
            try
            {
                string[] arrParam = new string[1];
                arrParam[0] = strName;
                string strData = mCommandApi.CallMethodGet("SelectItemID", arrParam);
                if (string.IsNullOrEmpty(strData) == false)
                {
                    short sID = Convert.ToInt16(strData);
                    return sID;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetItemIDByName", e.Message);
            }

            return -1;
        }

        //数据库更改时这里也要修改
        //无论怎样都要在代码里写死一部分内容（要么是SetItemName，要么是SetItemID）
        public static short GetSetItemID(E_SetItem eSetItem)
        {
            try
            {
                short sSetItemID = 0;
                switch ((int)eSetItem)
                {
                    case 1:
                        sSetItemID = 10;
                        break;

                    case 2:
                        sSetItemID = 11;
                        break;

                    case 3:
                        sSetItemID = 12;
                        break;

                    case 4:
                        sSetItemID = 13;
                        break;

                    case 5:
                        sSetItemID = 14;
                        break;

                    case 6:
                        sSetItemID = 15;
                        break;

                    case 7:
                        sSetItemID = 16;
                        break;

                    case 8:
                        sSetItemID = 17;
                        break;

                    case 9:
                        sSetItemID = 18;
                        break;

                    case 10:
                        sSetItemID = 19;
                        break;

                    case 11:
                        sSetItemID = 20;
                        break;

                    case 12:
                        sSetItemID = 21;
                        break;

                    case 13:
                        sSetItemID = 22;
                        break;

                    case 14:
                        sSetItemID = 23;
                        break;

                    case 15:
                        sSetItemID = 24;
                        break;

                    case 16:
                        sSetItemID = 25;
                        break;

                    case 17:
                        sSetItemID = 26;
                        break;

                    case 18:
                        sSetItemID = 27;
                        break;

                    case 19:
                        sSetItemID = 28;
                        break;

                    case 20:
                        sSetItemID = 29;
                        break;
                }
                return sSetItemID;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetItemID", e.Message);
            }
            return 0;
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
                    string strPath = clsSubPath.FullName + "\\TeachSetI(" + String.Format("{0:yyyy-MM-dd}", clsDate) + ").log";
                    StreamWriter clsWriter = new StreamWriter(strPath, true);
                    clsWriter.WriteLine(String.Format("{0:HH:mm:ss}", clsDate) + " " + strInfo);
                    clsWriter.Close();
                }
            }
            catch { }
        }

        private static void WriteErrorMessage(string strMethodName, string strErrorMessage)
        {
            try
            {
                DirectoryInfo clsPath = new DirectoryInfo(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory));
                DirectoryInfo clsSubPath = clsPath.CreateSubdirectory("Errlog\\LBD_WebApiInterface\\Api");

                if (clsSubPath.Exists)
                {
                    DateTime clsDate = DateTime.Now;
                    string strPath = clsSubPath.FullName + "\\TeachSetI(" + String.Format("{0:yyyy-MM-dd}", clsDate) + ").log";
                    StreamWriter clsWriter = new StreamWriter(strPath, true);
                    clsWriter.WriteLine(String.Format("{0:HH:mm:ss}", clsDate) + " " + strMethodName + ": " + strErrorMessage);
                    clsWriter.Close();
                }
            }
            catch { }
        }
    }
}