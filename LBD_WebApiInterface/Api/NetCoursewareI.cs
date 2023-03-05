using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LBD_WebApiInterface.Models;
using LBD_WebApiInterface.Utility;
using System.IO;

namespace LBD_WebApiInterface.Api
{
    public class NetCoursewareI
    {
        /// <summary>
        /// 网络化课件相关接口的访问地址
        /// </summary>
        public string ApiBaseUrl
        {
            get
            {
                return mApiBaseUrl;
            }
        }

        /// <summary>
        /// 数据库字段
        /// </summary>
        public enum NcwDataField
        {
            None = 0,
            CoursewareID = 1,
            SubjectID = 2,
            CreatorID = 3,
            CreateTime = 4,
            CoursewareName = 5,
            LevelID = 7,
            /// <summary>
            /// 课件上课时长
            /// </summary>
            CoursewareTime = 8,
            CoursewareStatus = 9,
            LastEditor = 11,
            LastEditTime = 12,

            /// <summary>
            /// 授课时间
            /// </summary>
            TeachTime = 25,
            /// <summary>
            /// 授课教室
            /// </summary>
            Classroom = 26,
            /// <summary>
            /// 授课对象（课程班）
            /// </summary>
            TeachClass = 27
        }

        /// <summary>
        /// 排序方式
        /// </summary>
        public enum NcwOrderType
        {
            ASC = 1,
            DESC = 2
        }

        /// <summary>
        /// 搜索方式
        /// </summary>
        public enum SearchKind
        {
            /// <summary>
            /// 根据条件搜索
            /// </summary>
            ByCondition = 1,
            /// <summary>
            /// 根据关键词搜索
            /// </summary>
            ByKeyword = 2
        }

        public enum TableFlagName
        {
            /// <summary>
            /// 图文教材库
            /// </summary>        
            图文教材库=1,
            /// <summary>
            /// 多媒体教程库
            /// </summary>
            多媒体教程库 = 2,
            /// <summary>
            /// 公共媒体库
            /// </summary>
            公共媒体库 = 3,
            /// <summary>
            /// 作业库
            /// </summary>
            作业库 = 4,
            /// <summary>
            /// 情景会话库
            /// </summary>
            情景会话库 = 5,
            /// <summary>
            /// 水平试题库
            /// </summary>
            水平试题库 = 6,
            /// <summary>
            /// 知识点课件库
            /// </summary>
            知识点课件库 = 7,
            /// <summary>
            /// 主题背景库
            /// </summary>
            主题背景库 = 8,
            /// <summary>
            /// 本地电脑
            /// </summary>
            本地电脑 = 9,
            /// <summary>
            /// 翻译库
            /// </summary>
            翻译库 = 10,
            /// <summary>
            /// 电子资源库
            /// </summary>
            电子资源库 = 11,
            /// <summary>
            /// 数字化资源库
            /// </summary>
            数字化资源库 = 12,
            网络化课件库=13,
            U盘=14
        }

        private string mApiBaseUrl;
        private CommandApi mCommandApi;

        private bool mInitStatus;

        public NetCoursewareI()
        {
            mInitStatus = false;
        }



        /// <summary>
        /// 初始化连接
        /// </summary>
        public bool Initialize(string strNetTeachIP,string strNetTeachPort)
        {
            try
            {
                if (mInitStatus == true)
                {
                    return false;
                }
                mApiBaseUrl = string.Format("http://{0}:{1}NetTeachApiF/API/NetCoursewareApi.ashx", strNetTeachIP, strNetTeachPort);
                //mApiBaseUrl = string.Format("http://{0}:{1}/NetTeachF/API/", strNetTeachIP, strNetTeachPort);

                mCommandApi = new CommandApi();
                mCommandApi.BaseUrl = mApiBaseUrl;
                //mCommandApi.ControllerName = "NetCoursewareApi";
                
                mInitStatus = true;

                return mInitStatus;
            }
            catch (Exception e)
            {
                WriteErrorMessage("Initialize", e.Message);
            }

            return false;
        }

        public bool Initialize(string strNetTeachIP, string strNetTeachPort,string strNetVirDir)
        {
            try
            {
                if (mInitStatus == true)
                {
                    return false;
                }
                mApiBaseUrl = string.Format(Properties.Resources.NetCoursewareUrl, strNetTeachIP, strNetTeachPort, strNetVirDir);
                //mApiBaseUrl = string.Format("http://{0}:{1}/NetTeachF/API/", strNetTeachIP, strNetTeachPort);

                mCommandApi = new CommandApi();
                mCommandApi.BaseUrl = mApiBaseUrl;
                //mCommandApi.ControllerName = "NetCoursewareApi";

                mInitStatus = true;

                return mInitStatus;
            }
            catch (Exception e)
            {
                WriteErrorMessage("Initialize", e.Message);
            }

            return false;
        }

        /// <summary>
        /// 获取网络化课件（特定条件）
        /// </summary>
        public NetCoursewareM[] GetNetCourseware(byte bSubjectID, string strTeacherID)
        {
            try
            {
                string[] arrParam = new string[6];
                arrParam[0] = bSubjectID.ToString();
                arrParam[1] = strTeacherID;
                arrParam[2] = "0";
                arrParam[3] = "0";
                arrParam[4] = "CreateTime";
                arrParam[5] = "DESC";
                string strData = mCommandApi.CallMethodGet("SelectNetCourseware", arrParam);
                if (string.IsNullOrEmpty(strData))
                {
                    return null;
                }

                NetCoursewareM[] arr= JsonFormatter.JsonDeserialize<NetCoursewareM[]>(strData);
                return arr;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetNetCourseware", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 获取网络化课件数量
        /// </summary>
        public int GetNetCoursewareCount(byte bSubjectID, string strTeacherID)
        {
            try
            {
                string[] arrParam = new string[2];
                arrParam[0] = bSubjectID.ToString();
                arrParam[1] = strTeacherID;
                string strData = mCommandApi.CallMethodGet("SelectNetCoursewareCount", arrParam);
                return Convert.ToInt32(strData);
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetNetCoursewareCount", e.Message);
            }

            return 0;
        }

        /// <summary>
        /// 查找课件，返回详细信息，包含课件信息、教学计划信息、课件所需资料的信息
        /// </summary>
        /// <param name="bSubjectID">学科ID</param>
        /// <param name="strTeacherID">教师ID</param>
        /// <param name="iPageSize">（分页）每一页的大小（取值为0时表示不分页）</param>
        /// <param name="iPageIndex">（分页）当前要获取的页码</param>
        /// <param name="orderField">（排序）要排序的字段</param>
        /// <param name="orderType">（排序）升序还是降序</param>
        /// <param name="iLineNum">（输出）总记录数</param>
        public NetCoursewareDetailM[] GetNetCoursewareDetail(byte bSubjectID, string strTeacherID, int iPageSize, int iPageIndex, NcwDataField orderField, NcwOrderType orderType)
        {
            try
            {
                string[] arrParam = new string[6];
                arrParam[0] = bSubjectID.ToString();
                arrParam[1] = strTeacherID;
                arrParam[2] = iPageSize.ToString();
                arrParam[3] = iPageIndex.ToString();
                switch ((int)orderField)
                {
                    case 1:
                        arrParam[4] = "CoursewareID";
                        break;
                    case 4:
                        arrParam[4] = "CreateTime";    
                        break;
                    case 12:
                        arrParam[4] = "LastEditTime";
                        break;
                    default:
                        arrParam[4] = "CoursewareID";
                        break;
                }
                switch ((int)orderType)
                {
                    case 1:
                        arrParam[5] = "ASC";
                        break;
                    case 2:
                        arrParam[5] = "DESC";
                        break;
                    default:
                        arrParam[5] = "ASC";
                        break;
                }

                string strData = mCommandApi.CallMethodGet("SelectNetCoursewareDetail", arrParam);
                if (string.IsNullOrEmpty(strData))
                {
                    return null;
                }

                NetCoursewareDetailM[] arrDetail = JsonFormatter.JsonDeserialize<NetCoursewareDetailM[]>(strData);
                return arrDetail;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetNetCoursewareDetail", e.Message);
            }

            return null;
        }

        //根据课件ID查找课件
        public NetCoursewareM GetNetCoursewareByID(string strCoursewareID)
        {
            try
            {
                if (string.IsNullOrEmpty(strCoursewareID))
                {
                    return null;
                }

                string[] arrParam = new string[1];
                arrParam[0] = strCoursewareID;

                string str = mCommandApi.CallMethodGet("SelectNetCoursewareByID", arrParam);
                if (string.IsNullOrEmpty(str))
                {
                    return null;
                }

                NetCoursewareM ncw = JsonFormatter.JsonDeserialize<NetCoursewareM>(str);
                return ncw;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetNetCoursewareByID", e.Message);
            }

            return null;
        }

        //获取当前教学计划
        public CurrentTeachPlanM[] GetCurrentTeachPlan(byte bSubjectID, string strTeacherID, DateTime startTimePoint, DateTime endTimePoint)
        {
            try
            {
                string[] arrParam = new string[4];
                arrParam[0] = strTeacherID;
                arrParam[1] = bSubjectID.ToString();
                arrParam[2] = startTimePoint.ToString();
                arrParam[3] = endTimePoint.ToString();

                string str = mCommandApi.CallMethodGet("GetCurrentTeachPlan", arrParam);
                if (string.IsNullOrEmpty(str))
                {
                    return null;
                }

                CurrentTeachPlanM[] arrCtp = JsonFormatter.JsonDeserialize<CurrentTeachPlanM[]>(str);
                if (arrCtp != null && arrCtp.Length > 0)
                {
                    //教学计划计数（课件变化时，此变量重置）
                    int iCtpNameCount = 0;
                    //课件ID，根据此值来判断课件是否有变化
                    string strTempCoursewareID = arrCtp[0].CoursewareID;
                    //遍历所有教学计划，每当课件ID变化时，教学计划的变量就要重置
                    for (int i = 0; i < arrCtp.Length; i++)
                    {
                        if (arrCtp[i].CoursewareID == strTempCoursewareID)
                        {
                            iCtpNameCount++;
                        }
                        else
                        {
                            iCtpNameCount = 1;
                            strTempCoursewareID = arrCtp[i].CoursewareID;
                        }
                        arrCtp[i].TeachPlanName = arrCtp[i].CoursewareName + "的教学计划" + iCtpNameCount;
                    }

                    return arrCtp;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetCurrentTeachPlan", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 查询单个课件（若teacherID为空，则教学计划为空）
        /// </summary>
        public NetCoursewareDetailM GetNetCoursewareDetailByID(string strCoursewareID,string strTeacherID)
        {
            try
            {
                if (string.IsNullOrEmpty(strCoursewareID))
                {
                    return null;
                }

                string[] arrParam = new string[2];
                arrParam[0] = strCoursewareID;
                arrParam[1] = strTeacherID;

                string str = mCommandApi.CallMethodGet("SelectNetCoursewareDetailByID", arrParam);
                if (string.IsNullOrEmpty(str))
                {
                    return null;
                }

                NetCoursewareDetailM ncwd = JsonFormatter.JsonDeserialize<NetCoursewareDetailM>(str);
                return ncwd;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetNetCoursewareDetailByID", e.Message);
            }

            return null;
        }

        /// <summary>
        /// 搜索网络化课件
        /// </summary>
        public NetCoursewareDetailM[] SearchNetCourseware(byte bSubjectID, string strTeacherID, int iPageSize, int iPageIndex, NcwDataField orderField, NcwOrderType orderType, string strKeyword, NcwDataField searchField, SearchKind searchKind, out int iRowCount)
        {
            iRowCount = 0;
            try
            {
                string[] arrParam = new string[9];
                arrParam[0] = bSubjectID.ToString();
                arrParam[1] = strTeacherID;
                arrParam[2] = iPageSize.ToString();
                arrParam[3] = iPageIndex.ToString();
                switch ((int)orderField)
                {
                    case 1:
                        arrParam[4] = "CoursewareID";
                        break;
                    case 4:
                        arrParam[4] = "CreateTime";
                        break;
                    case 12:
                        arrParam[4] = "LastEditTime";
                        break;
                    default:
                        arrParam[4] = "CoursewareID";
                        break;
                }
                switch ((int)orderType)
                {
                    case 1:
                        arrParam[5] = "ASC";
                        break;
                    case 2:
                        arrParam[5] = "DESC";
                        break;
                    default:
                        arrParam[5] = "ASC";
                        break;
                }
                arrParam[6] = strKeyword;
                switch ((int)searchField)
                {
                    case 0:
                        arrParam[7] = "";
                        break;
                    case 9:
                        arrParam[7] = "CoursewareStatus";
                        break;
                    case 25:
                        arrParam[7] = "TeachTime";
                        break;
                    case 26:
                        arrParam[7]="Classroom";
                        break;
                    case 27:
                        arrParam[7] = "TeachClass";
                        break;
                    default:
                        arrParam[7] = "";
                        break;
                }
                switch ((int)searchKind)
                {
                    case 1:
                        arrParam[8] = "1";
                        break;
                    case 2:
                        arrParam[8] = "2";
                        break;
                    default:
                        break;
                }

                string strData = mCommandApi.CallMethodGet("SearchNetCourseware", arrParam);
                if (string.IsNullOrEmpty(strData))
                {
                    return null;
                }
                else
                {
                    int iIndex = strData.IndexOf(',');
                    if (iIndex > -1)
                    {
                        iRowCount = Convert.ToInt32(strData.Substring(0, iIndex));
                        strData = strData.Substring(iIndex + 1);
                    }
                }
                NetCoursewareDetailM[] arrDetail = JsonFormatter.JsonDeserialize<NetCoursewareDetailM[]>(strData);
                return arrDetail;
            }
            catch (Exception e)
            {
                WriteErrorMessage("SearchNetCourseware", e.Message);
            }

            return null;
        }

        /// <summary>
        /// 添加网络化课件（单个），课件ID会写入实体对象中，且作为返回值返回
        /// </summary>
        public string AddNetCourseware(NetCoursewareM ncw)
        {
            try
            {
                if (ncw == null)
                {
                    return "";
                }
                string[] arrParam = new string[1];
                arrParam[0] = JsonFormatter.JsonSerialize(ncw);
                string strReturn = mCommandApi.CallMethodPost("InsertNetCourseware", arrParam);

                if (string.IsNullOrEmpty(strReturn) == false)
                {
                    ncw.CoursewareID = strReturn;
                }
                else
                {
                    ncw.CoursewareID = "";
                }

                return strReturn;
            }
            catch (Exception e)
            {
                WriteErrorMessage("AddNetCourseware", e.Message);
            }

            return null;
        }

        /// <summary>
        /// 一次性添加网络化课件（逻辑有问题，暂且搁置，转用其它接口）
        /// </summary>
        //需要把各个课件的ID写进实体类中
        //需要先添加网络化课件，获得生成的课件ID，然后才能添加教学计划等，所以不可能一次性添加
        public int AddNetCoursewareDetail(NetCoursewareDetailM[] ncwd)
        {
            try
            {
                if (ncwd == null)
                {
                    return 0;
                }
                string[] arrParam = new string[1];
                arrParam[0]= JsonFormatter.JsonSerialize(ncwd);
                string strReturn = mCommandApi.CallMethodPost("AddNetCoursewareDetail", arrParam);

                return Convert.ToInt32(strReturn);
            }
            catch (Exception e)
            {
                WriteErrorMessage("AddNetCoursewareDetail", e.Message);
            }

            return -1;
        }

        /// <summary>
        /// 添加教学计划，生成的ID已写入实体对象中，且作为返回值返回，多个值用 | 分隔
        /// </summary>
        public string AddNCWTeachPlan(NCWTeachPlanM[] teachPlan)
        {
            try
            {
                if (teachPlan == null)
                {
                    return "";
                }

                string[] arrParam = new string[1];
                arrParam[0] = JsonFormatter.JsonSerialize(teachPlan);
                string strReturn = mCommandApi.CallMethodPost("InsertNCWTeachPlan", arrParam);
                if (strReturn != null)
                {
                    string[] arrID = strReturn.Split('|');
                    if (arrID != null)
                    {
                        for (int i = 0; i < teachPlan.Length; i++)
                        {
                            if (string.IsNullOrEmpty(arrID[i]))
                            {
                                teachPlan[i].ID = -1;
                            }
                            else
                            {
                                teachPlan[i].ID = Convert.ToInt32(arrID[i]);
                            }
                        }
                    }

                    return strReturn;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("AddNCWTeachPlan", e.Message);
            }

            return null;
        }

        /// <summary>
        /// 添加网络化课件辅助资源
        /// </summary>
        public string AddNcwAssistResource(NCWAssistResourceM[] aResource)
        {
            try
            {
                if (aResource == null)
                {
                    return "";
                }

                string[] arrParam = new string[1];
                arrParam[0]= JsonFormatter.JsonSerialize(aResource);
                string strReturn = mCommandApi.CallMethodPost("InsertNCWAssistResource", arrParam);
                if (strReturn != null)
                {
                    string[] arrID = strReturn.Split('|');
                    if (arrID != null)
                    {
                        for (int i = 0; i < aResource.Length; i++)
                        {
                            if (string.IsNullOrEmpty(arrID[i]))
                            {
                                aResource[i].ID = -1;
                            }
                            else
                            {
                                aResource[i].ID = Convert.ToInt32(arrID[i]);
                            }
                        }
                    }

                    return strReturn;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("AddNcwAssistResource", e.Message);
            }

            return null;
        }
        
        /// <summary>
        /// 添加网络化课件所需资源
        /// </summary>
        public string AddNcwResource(NCWResourceM[] nResource)
        {
            try
            {
                if (nResource == null)
                {
                    return "";
                }
                string[] arrParam = new string[1];
                arrParam[0] = JsonFormatter.JsonSerialize(nResource);
                string strReturn = mCommandApi.CallMethodPost("InsertNCWResource", arrParam);
                if (strReturn != null)
                {
                    string[] arrID = strReturn.Split('|');
                    if (arrID != null)
                    {
                        for (int i = 0; i < nResource.Length; i++)
                        {
                            if (string.IsNullOrEmpty(arrID[i]))
                            {
                                nResource[i].ID = -1;
                            }
                            else
                            {
                                nResource[i].ID = Convert.ToInt32(arrID[i]);
                            }
                        }
                    }

                    return strReturn;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("AddNcwResource", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 更新网络化课件(不需要的字段赋值为null，bool和int类型也可以赋值为null)
        /// </summary>
        public int UpdateNetCourseware(string strCoursewareID, string strCoursewareName, string strCoursewarePath, string strCoursewarePicPath, short? sLevelID, int? iCoursewareTime, bool? bCoursewareStatus, bool? bIsShare, string strLastEditor)
        {
            try
            {
                if (string.IsNullOrEmpty(strCoursewareID))
                {
                    return 0;
                }

                string[] arrParam = new string[9];
                arrParam[0] = strCoursewareID;
                arrParam[1] = strCoursewareName;
                arrParam[2] = strCoursewarePath;
                arrParam[3] = strCoursewarePicPath;
                if (sLevelID != null)
                {
                    arrParam[4] = sLevelID.ToString();
                }
                else
                {
                    arrParam[4] = "";
                }
                if (iCoursewareTime != null)
                {
                    arrParam[5] = iCoursewareTime.ToString();
                }
                else
                {
                    arrParam[5] = "";
                }
                if (bCoursewareStatus != null)
                {
                    arrParam[6] = bCoursewareStatus.ToString();
                }
                else
                {
                    arrParam[6] = "";
                }
                if (bIsShare != null)
                {
                    arrParam[7] = bIsShare.ToString();
                }
                else
                {
                    arrParam[7] = "";
                }
                arrParam[8] = strLastEditor;

                string strReturn = mCommandApi.CallMethodPut("UpdateNetCourseware", arrParam);
                return Convert.ToInt32(strReturn);
            }
            catch (Exception e)
            {
                WriteErrorMessage("UpdateNetCourseware", e.Message);
            }

            return -1;
        }

        /// <summary>
        /// 更新教学计划
        /// </summary>
        public int UpdateTeachPlan(int ID,DateTime? dtTeachTime,string strClassroom,string strTeachClass,string strLastEditor)
        {
            try
            {
                if (ID < 0)
                {
                    return 0;
                }

                string[] arrParam = new string[5];
                arrParam[0] = ID.ToString();
                if (dtTeachTime.HasValue)
                {
                    arrParam[1] = string.Format("{0:yyyy/M/d HH:mm:ss}", dtTeachTime.Value);
                }
                else
                {
                    arrParam[1] = "";
                }
                arrParam[2] = strClassroom;
                arrParam[3] = strTeachClass;
                arrParam[4] = strLastEditor;

                string strReturn = mCommandApi.CallMethodPut("UpdateNcwTeachPlan", arrParam);
                return Convert.ToInt32(strReturn);
            }
            catch (Exception e)
            {
                WriteErrorMessage("UpdateTeachPlan", e.Message);
            }

            return -1;
        }

        /// <summary>
        /// 更新网络化课件所需资源
        /// </summary>
        public int UpdateNCWResource(int ID, short? sTeachModeID, int? iTeachDurationTime, string strSimulationInfoPath, byte? bOrderNo,string strLastEditor)
        {
            try
            {
                if (ID < 0)
                {
                    return 0;
                }

                string[] arrParam = new string[6];
                arrParam[0] = ID.ToString();
                if (sTeachModeID != null)
                {
                    arrParam[1] = sTeachModeID.ToString();

                }
                if (iTeachDurationTime != null)
                {
                    arrParam[2] = iTeachDurationTime.ToString();
                }
                else
                {
                    arrParam[2] = "";
                }
                arrParam[3] = strSimulationInfoPath;
                if (bOrderNo != null)
                {
                    arrParam[4] = bOrderNo.ToString();
                }
                else
                {
                    arrParam[4] = "";
                }
                arrParam[5] = strLastEditor;

                string strReturn = mCommandApi.CallMethodPut("UpdateNcwResource", arrParam);
                return Convert.ToInt32(strReturn);
            }
            catch (Exception e)
            {
                WriteErrorMessage("UpdateNCWResource", e.Message);
            }

            return -1;
        }

        /// <summary>
        /// 删除网络化课件
        /// </summary>
        public int DelNetCourseware(string strCoursewareID, string strTeacherID)
        {
            try
            {
                if (string.IsNullOrEmpty(strCoursewareID)||string.IsNullOrEmpty(strTeacherID))
                {
                    return 0;
                }

                string[] arrParam = new string[2];
                arrParam[0] = strCoursewareID;
                arrParam[1] = strTeacherID;
                string strReturn = mCommandApi.CallMethodDelete("DelNetCourseware", arrParam);
                return Convert.ToInt32(strReturn);
            }
            catch (Exception e)
            {
                WriteErrorMessage("DelNetCourseware", e.Message);
            }

            return -1;
        }

        /// <summary>
        /// 删除网络化课件所需资源
        /// </summary>
        public int DelNCWResource(string strCoursewareID)
        {
            try
            {
                if (string.IsNullOrEmpty(strCoursewareID))
                {
                    return 0;
                }

                string[] arrParam = new string[1];
                arrParam[0] = strCoursewareID;
                string strReturn = mCommandApi.CallMethodDelete("DelNcwResource", arrParam);
                return Convert.ToInt32(strReturn);
            }
            catch (Exception e)
            {
                WriteErrorMessage("DelNCWResource", e.Message);
            }

            return -1;
        }

        //根据资料ID来删除
        public int DelNCWResourceSingle(int ID)
        {
            try
            {
                string[] arrParam = new string[1];
                arrParam[0] = ID.ToString();
                string strReturn = mCommandApi.CallMethodDelete("DelNcwResourceSingle", arrParam);
                return Convert.ToInt32(strReturn);
            }
            catch (Exception e)
            {
                WriteErrorMessage("DelNCWResourceSingle", e.Message);
            }

            return -1;
        }

        /// <summary>
        /// 删除教学计划
        /// </summary>
        public int DelNCWTeachPlan(string strCoursewareID, string strTeacherID)
        {
            try
            {
                if (string.IsNullOrEmpty(strCoursewareID))
                {
                    return 0;
                }

                string[] arrParam = new string[2];
                arrParam[0] = strCoursewareID;
                arrParam[1] = strTeacherID;
                string strReturn = mCommandApi.CallMethodDelete("DelNcwTeachPlan", arrParam);
                return Convert.ToInt32(strReturn);
            }
            catch (Exception e)
            {
                WriteErrorMessage("DelNCWTeachPlan", e.Message);
            }

            return -1;
        }

        //根据教学计划ID来删除
        public int DelNCWTeachPlanSingle(int ID)
        {
            try
            {
                string[] arrParam = new string[1];
                arrParam[0] = ID.ToString();
                string strReturn = mCommandApi.CallMethodDelete("DelNcwTeachPlanSingle", arrParam);
                return Convert.ToInt32(strReturn);
            }
            catch (Exception e)
            {
                WriteErrorMessage("DelNCWTeachPlanSingle", e.Message);
            }

            return -1;
        }

        /// <summary>
        /// 获取TableFlagID
        /// </summary>
        public short GetTableFlag(TableFlagName name)
        {
            try
            {
                string strName = "";
                switch ((int)name)
                {
                    case 1:
                        strName = "图文教材库";
                        break;
                    case 2:
                        strName = "多媒体教程库";
                        break;
                    case 3:
                        strName = "公共媒体库";
                        break;
                    case 4:
                        strName = "作业库";
                        break;
                    case 5:
                        strName = "情景会话库";
                        break;
                    case 6:
                        strName = "水平试题库";
                        break;
                    case 7:
                        strName = "知识点课件库";
                        break;
                    case 8:
                        strName = "主题背景库";
                        break;
                    case 9:
                        strName = "本地电脑";
                        break;
                    case 10:
                        strName = "翻译库";
                        break;
                    case 11:
                        strName = "电子资源库";
                        break;
                    case 12:
                        strName = "数字化资源库";
                        break;
                    case 13:
                        strName = "网络化课件库";
                        break;
                    case 14:
                        strName = "U盘";
                        break;
                }

                string[] arrParam=new string[1];
                arrParam[0]=strName;

                string strReturn = mCommandApi.CallMethodGet("GetTableFlagIDByName", arrParam);

                return Convert.ToInt16(strReturn);
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetTableFlag", e.Message);
            }

            return -1;
        }

        /// <summary>
        /// 获取教学模式名称
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
                string strName = mCommandApi.CallMethodGet("GetModeName", arrParam);

                return strName;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetModeNameByEnglishID", e.Message);
            }

            return null;
        }

        /// <summary>
        /// 根据老师ID和教学对象搜索教学最近的一个教学计划
        /// </summary>
        public NCWTeachPlanM GetRecentTeachPlan(string strTeacherID, string strTeachClass,byte bSubjectID)
        {
            try
            {
                string[] arrParam = new string[3];
                arrParam[0] = strTeacherID;
                arrParam[1] = strTeachClass;
                arrParam[2] = bSubjectID.ToString();

                string strReturn = mCommandApi.CallMethodGet("GetRecentTeachPlan", arrParam);

                if (string.IsNullOrEmpty(strReturn) == false)
                {
                    NCWTeachPlanM[] ncwtp = JsonFormatter.JsonDeserialize <NCWTeachPlanM[]>(strReturn);
                    if (ncwtp != null && ncwtp.Length == 1)
                    {
                        return ncwtp[0];
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetRecentTeachPlan", e.Message);
            }

            return null;
        }
    
        public void GetSearchCondition(byte bSubjectID, string strTeacherID, out string[] arrTeachClass, out string[] arrClassroom, out string[] arrTeachTime)
        {
            arrTeachClass = null;
            arrClassroom = null;
            arrTeachTime = null;
            try
            {
                string[] arrParam = new string[2];
                arrParam[0] = bSubjectID.ToString();
                arrParam[1] = strTeacherID;

                string strReturn = mCommandApi.CallMethodGet("GetSearchCondition", arrParam);

                if (string.IsNullOrEmpty(strReturn) == false)
                {
                    SearchConditionM condition = JsonFormatter.JsonDeserialize<SearchConditionM>(strReturn);
                    if (condition != null)
                    {
                        if (condition.TeachClass != null)
                        {
                            arrTeachClass = condition.TeachClass.ToArray();
                        }
                        if (condition.Classroom != null)
                        {
                            arrClassroom = condition.Classroom.ToArray();
                        }
                        if (condition.TeachTime != null)
                        {
                            arrTeachTime = condition.TeachTime.ToArray();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetSearchCondition", e.Message);
            }
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
                    string strPath = clsSubPath.FullName + "\\NetCoursewareI(" + String.Format("{0:yyyy-MM-dd}", clsDate) + ").log";
                    StreamWriter clsWriter = new StreamWriter(strPath, true);
                    clsWriter.WriteLine(String.Format("{0:HH:mm:ss}", clsDate) + " " + strMethodName + ": " + sErrorMessage);
                    clsWriter.Close();
                }
            }
            catch { }
        }

    }
}
