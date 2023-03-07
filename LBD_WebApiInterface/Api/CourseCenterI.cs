using LBD_WebApiInterface.Models.CourseCenter;
using LBD_WebApiInterface.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LBD_WebApiInterface.Api
{
    /// <summary>
    /// 多媒体云课堂（课程中心）的接口
    /// </summary>
    public class CourseCenterI
    {
        //课程中心接口的地址，形式http://[IP]:[Port]
        private string mCourseCenterApiUrl;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="CloudPreparationIP">云备课WebApi的IP</param>
        /// <param name="CloudPreparationPort">云备课WebApi的Port</param>
        /// <returns>true-初始化成功，false-初始化失败</returns>
        public bool Initialize(string CourseCenterIP, string CourseCenterPort)
        {
            try
            {
                if (string.IsNullOrEmpty(CourseCenterIP) || string.IsNullOrEmpty(CourseCenterPort))
                {
                    return false;
                }

                mCourseCenterApiUrl = "http://" + CourseCenterIP + ":" + CourseCenterPort;

                return true;
            }
            catch (Exception e)
            {
                WriteErrorMessage("Initialize", e.Message);
            }
            return false;
        }

        /// <summary>
        /// 获取学科
        /// </summary>
        /// <returns></returns>
        public SubjectM[] GetAllSubjectName()
        {
            try
            {
                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mCourseCenterApiUrl + "/CourseCenter/MCC/External.ashx?method=GetSubjectInfo&params=1");
                string strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteErrorMessage("GetAllSubjectName", "返回值为空。查询条件为：" + sbUrl.ToString());
                    return null;
                }
                strResult = Uri.UnescapeDataString(strResult);
                CCApiResultM<SubjectM[]> result = JsonFormatter.JsonDeserialize<CCApiResultM<SubjectM[]>>(strResult);
                if (result != null)
                {
                    if (result.error != 0)
                    {
                        WriteErrorMessage("GetAllSubjectName", strResult);
                        return null;
                    }
                    else
                    {
                        List<SubjectM> list = new List<SubjectM>();
                        foreach (SubjectM subject in result.Data)
                            if (string.IsNullOrEmpty(subject.Subject) == false)
                                list.Add(subject);
                        return list.ToArray();
                    }
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetAllSubjectName", e.Message);
                return null;
            }
        }

        /// <summary>
        /// 获取教师信息
        /// </summary>
        /// <param name="Subject">传学科名称，可以传空</param>
        /// <returns></returns>
        public TeacherM[] GetTeacherInfo(string Subject)
        {
            try
            {
                if (string.IsNullOrEmpty(Subject))
                    Subject = "%20";
                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mCourseCenterApiUrl + "/CourseCenter/MCC/External.ashx?method=GetTeacherInfo");
                sbUrl.Append(string.Format("&params={0}", Subject));
                string strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteErrorMessage("GetTeacherInfo", "返回值为空。查询条件为：" + sbUrl.ToString());
                    return null;
                }
                strResult = Uri.UnescapeDataString(strResult);
                CCApiResultM<TeacherM[]> result = JsonFormatter.JsonDeserialize<CCApiResultM<TeacherM[]>>(strResult);
                if (result != null)
                {
                    if (result.error != 0)
                    {
                        WriteErrorMessage("GetTeacherInfo", strResult);
                        return null;
                    }
                    else
                        return result.Data;
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetTeacherInfo", e.Message);
                return null;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="Subject">传学科名称，传空获取不属于任何学科的视频</param>
        /// <param name="TeacherID">传教师id,传空表示所有老师</param>
        /// <param name="SortType"></param>
        /// <param name="SearchStr"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="StartTime">可以传空</param>
        /// <param name="EndTime">可以传空</param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public RequestResourceM[] GetRecordRes(string Subject, string TeacherID, int SortType, string SearchStr, int PageIndex, int PageSize, DateTime? StartTime, DateTime? EndTime, out int TotalCount)
        {
            TotalCount = 0;
            try
            {
                if (string.IsNullOrEmpty(TeacherID))
                    TeacherID = "ALL_TEACHER";
                string sTime = null;
                if (StartTime != null)
                    sTime = string.Format("{0:yyyyMMdd}", StartTime);
                string eTime = null;
                if (EndTime != null)
                    eTime = string.Format("{0:yyyyMMdd}", EndTime);
                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mCourseCenterApiUrl + "/CourseCenter/MCC/External.ashx?method=GetRecordRes");
                sbUrl.Append(string.Format("&params={7}|{0}|{1}|{2}|{3}|{4}|{5}|{6}", TeacherID, SortType, SearchStr, PageIndex, PageSize, sTime, eTime, Subject));
                string strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteErrorMessage("GetRecordRes", "返回值为空。查询条件为：" + sbUrl.ToString());
                    return null;
                }

                strResult = Uri.UnescapeDataString(strResult);
                CCApiResultM<RequestResourceM[]> result = JsonFormatter.JsonDeserialize<CCApiResultM<RequestResourceM[]>>(strResult);
                if (result != null)
                {
                    if (result.error != 0)
                    {
                        WriteErrorMessage("GetRecordRes", strResult);
                        return null;
                    }
                    else
                    {
                        TotalCount = result.TotalCount;
                        return result.Data;
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetRecordRes", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 获取指定目录下的录播资源
        /// </summary>
        /// <param name="DirID">目录id</param>
        /// <param name="TotalCount">资源总数</param>
        /// <returns></returns>
        public RequestResourceM[] GetDirRecordRes(string DirID, out int TotalCount)
        {
            TotalCount = 0;
            try
            {
                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mCourseCenterApiUrl + "/CourseCenter/MCC/External.ashx?method=GetRecordRes");
                sbUrl.Append(string.Format("&params={0}", DirID));
                string strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteErrorMessage("GetDirRecordRes", "返回值为空。查询条件为：" + sbUrl.ToString());
                    return null;
                }

                strResult = Uri.UnescapeDataString(strResult);
                CCApiResultM<RequestResourceM[]> result = JsonFormatter.JsonDeserialize<CCApiResultM<RequestResourceM[]>>(strResult);
                if (result != null)
                {
                    if (result.error != 0)
                    {
                        WriteErrorMessage("GetDirRecordRes", strResult);
                        return null;
                    }
                    else
                    {
                        TotalCount = result.TotalCount;
                        return result.Data;
                    }
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetDirRecordRes", e.Message);
                return null;
            }
        }

        /// <summary>
        /// 获取录播资源目录
        /// </summary>
        /// <param name="Subject">学科名称</param>
        /// <param name="TeacherID">教师id</param>
        /// <param name="SortType">排序标识（0：升序，1：降序）</param>
        /// <param name="SearchStr">可以传空</param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="StartTime">可以传空</param>
        /// <param name="EndTime">可以传空</param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public ResourceDir[] GetRecordDir(string Subject, string TeacherID, int SortType, string SearchStr, int PageIndex, int PageSize, DateTime? StartTime, DateTime? EndTime, out int TotalCount)
        {
            TotalCount = 0;
            try
            {
                string sTime = null;
                if (StartTime != null)
                    sTime = string.Format("{0:yyyyMMdd}", StartTime);
                string eTime = null;
                if (EndTime != null)
                    eTime = string.Format("{0:yyyyMMdd}", EndTime);
                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mCourseCenterApiUrl + "/CourseCenter/MCC/External.ashx?method=GetRecordDir");
                sbUrl.Append(string.Format("&params={7}|{0}|{1}|{2}|{3}|{4}|{5}|{6}", TeacherID, SortType, SearchStr, PageIndex, PageSize, sTime, eTime, Subject));
                WriteErrorMessage("GetRecordDir", sbUrl.ToString());
                string strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteErrorMessage("GetRecordDir", "返回值为空。查询条件为：" + sbUrl.ToString());
                    return null;
                }

                strResult = Uri.UnescapeDataString(strResult);
                CCApiResultM<ResourceDir[]> result = JsonFormatter.JsonDeserialize<CCApiResultM<ResourceDir[]>>(strResult);
                if (result != null)
                {
                    if (result.error != 0)
                    {
                        WriteErrorMessage("GetRecordDir", strResult);
                        return null;
                    }
                    else
                    {
                        TotalCount = result.TotalCount;
                        return result.Data;
                    }
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetRecordDir", e.Message);
                return null;
            }
        }

        /// <summary>
        /// 查找点播资源
        /// </summary>
        /// <param name="TeacherID">教师ID，传"ALL_TEACHER"表示查所有老师</param>
        /// <param name="SortType">排序规则，0-升序，1-降序</param>
        /// <param name="SearchStr">模糊搜索的关键字</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">每页大小</param>
        /// <param name="StartTime">开始时间，可为null</param>
        /// <param name="EndTime">结束时间，可为null</param>
        /// <param name="TotalCount">数据总量（输出参数）</param>
        /// <returns>点播资源列表</returns>
        public RequestResourceM[] GetRequestResource(string TeacherID, int SortType, string SearchStr, int PageIndex, int PageSize, DateTime? StartTime, DateTime? EndTime, out int TotalCount)
        {
            TotalCount = 0;
            try
            {
                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mCourseCenterApiUrl + "/CourseCenter/MCC/External.ashx?method=GetBODRes");
                sbUrl.Append(string.Format("&params={0}|{1}|{2}|{3}|{4}|{5}|{6}", TeacherID, SortType, SearchStr, PageIndex, PageSize, StartTime, EndTime));
                //WriteDebugInfo("GetRequestResource获取录播视频地址", sbUrl.ToString());

                string strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteErrorMessage("GetRequestResource", "返回值为空。查询条件为：" + sbUrl.ToString());
                    return null;
                }

                strResult = Uri.UnescapeDataString(strResult);
                CCApiResultM<RequestResourceM[]> result = JsonFormatter.JsonDeserialize<CCApiResultM<RequestResourceM[]>>(strResult);
                if (result != null)
                {
                    if (result.error != 0)
                    {
                        WriteErrorMessage("GetRequestResource", strResult);
                        return null;
                    }
                    else
                    {
                        TotalCount = result.TotalCount;
                        return result.Data;
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetRequestResource", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 资源ID
        /// </summary>
        /// <param name="strResID"></param>
        /// <returns></returns>
        public RequestResourceM GetRequestResourceByID(string strResID)
        {
            int TotalCount = 0;
            try
            {
                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mCourseCenterApiUrl + "/CourseCenter/MCC/External.ashx?method=GetBODRes");
                sbUrl.Append(string.Format("&params={0}|{1}|{2}|{3}|{4}|{5}|{6}", "ALL_TEACHER", 0, "", 1, 1000, null, null));

                string strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteErrorMessage("GetRequestResourceByID", "返回值为空。查询条件为：" + sbUrl.ToString());
                    return null;
                }

                strResult = Uri.UnescapeDataString(strResult);
                CCApiResultM<RequestResourceM[]> result = JsonFormatter.JsonDeserialize<CCApiResultM<RequestResourceM[]>>(strResult);
                if (result != null)
                {
                    if (result.error != 0)
                    {
                        WriteErrorMessage("GetRequestResourceByID", strResult);
                        return null;
                    }
                    else
                    {
                        TotalCount = result.TotalCount;
                        if (TotalCount > 1000)
                        {
                            sbUrl = null;
                            sbUrl.Append(mCourseCenterApiUrl + "/CourseCenter/MCC/External.ashx?method=GetBODRes");
                            sbUrl.Append(string.Format("&params={0}|{1}|{2}|{3}|{4}|{5}|{6}", "ALL_TEACHER", 0, "", 1, 1000, null, null));

                            strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
                            if (string.IsNullOrEmpty(strResult))
                            {
                                WriteErrorMessage("GetRequestResourceByID", "返回值为空。查询条件为：" + sbUrl.ToString());
                                return null;
                            }

                            strResult = Uri.UnescapeDataString(strResult);
                            result = JsonFormatter.JsonDeserialize<CCApiResultM<RequestResourceM[]>>(strResult);
                            if (result != null)
                            {
                                if (result.error != 0)
                                {
                                    WriteErrorMessage("GetRequestResourceByID", strResult);
                                    return null;
                                }
                                else
                                {
                                    for (int i = 0; i < result.Data.Length; i++)
                                    {
                                        if (result.Data[i].ResID == strResID)
                                            return result.Data[i];
                                    }
                                    return null;
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < result.Data.Length; i++)
                            {
                                if (result.Data[i].ResID == strResID)
                                    return result.Data[i];
                            }
                            return null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetRequestResourceByID", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 查找广播资源
        /// </summary>
        /// <param name="TeacherID">教师ID，传"ALL_TEACHER"表示查所有老师</param>
        /// <param name="SortType">排序规则，0-升序，1-降序</param>
        /// <param name="SearchStr">模糊搜索的关键字</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">每页大小</param>
        /// <param name="StartTime">开始时间，可为null</param>
        /// <param name="EndTime">结束时间，可为null</param>
        /// <param name="TotalCount">数据总量（输出参数）</param>
        /// <returns>广播资源列表</returns>
        public BroadcastResourceM[] GetBroadcastResource(string TeacherID, int SortType, string SearchStr, int PageIndex, int PageSize, DateTime? StartTime, DateTime? EndTime, out int TotalCount)
        {
            TotalCount = 0;
            try
            {
                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mCourseCenterApiUrl + "/CourseCenter/MCC/External.ashx?method=GetBroadcastRes");
                sbUrl.Append(string.Format("&params={0}|{1}|{2}|{3}|{4}|{5}|{6}", TeacherID, SortType, SearchStr, PageIndex, PageSize, StartTime, EndTime));

                string strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteErrorMessage("GetBroadcastResource", "返回值为空。查询条件为：" + sbUrl.ToString());
                    return null;
                }

                strResult = Uri.UnescapeDataString(strResult);
                CCApiResultM<BroadcastResourceM[]> result = JsonFormatter.JsonDeserialize<CCApiResultM<BroadcastResourceM[]>>(strResult);
                if (result != null)
                {
                    if (result.error != 0)
                    {
                        WriteErrorMessage("GetBroadcastResource", strResult);
                        return null;
                    }
                    else
                    {
                        TotalCount = result.TotalCount;
                        return result.Data;
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetBroadcastResource", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 查找直播资源
        /// </summary>
        /// <param name="TeacherID">教师ID，传"ALL_TEACHER"表示查所有老师</param>
        /// <param name="SortType">排序规则，0-升序，1-降序</param>
        /// <param name="SearchStr">模糊搜索的关键字</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">每页大小</param>
        /// <param name="StartTime">开始时间，可为null</param>
        /// <param name="EndTime">结束时间，可为null</param>
        /// <param name="TotalCount">数据总量（输出参数）</param>
        /// <returns>直播资源列表</returns>
        public LiveResourceM[] GetLiveResourceM(string TeacherID, int SortType, string SearchStr, int PageIndex, int PageSize, DateTime? StartTime, DateTime? EndTime, out int TotalCount)
        {
            TotalCount = 0;
            try
            {
                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mCourseCenterApiUrl + "/CourseCenter/MCC/External.ashx?method=GetLiveRes");
                sbUrl.Append(string.Format("&params={0}|{1}|{2}|{3}|{4}|{5}|{6}", TeacherID, SortType, SearchStr, PageIndex, PageSize, StartTime, EndTime));

                string strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteErrorMessage("GetLiveResourceM", "返回值为空。查询条件为：" + sbUrl.ToString());
                    return null;
                }

                strResult = Uri.UnescapeDataString(strResult);
                CCApiResultM<LiveResourceM[]> result = JsonFormatter.JsonDeserialize<CCApiResultM<LiveResourceM[]>>(strResult);
                if (result != null)
                {
                    if (result.error != 0)
                    {
                        WriteErrorMessage("GetLiveResourceM", strResult);
                        return null;
                    }
                    else
                    {
                        TotalCount = result.TotalCount;
                        return result.Data;
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetLiveResourceM", e.Message);
            }
            return null;
        }

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
                    string strPath = clsSubPath.FullName + "\\CourseCenterI(" + String.Format("{0:yyyy-MM-dd}", clsDate) + ").log";
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
                    string strPath = clsSubPath.FullName + "\\CourseCenterI(" + String.Format("{0:yyyy-MM-dd}", clsDate) + ").log";
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