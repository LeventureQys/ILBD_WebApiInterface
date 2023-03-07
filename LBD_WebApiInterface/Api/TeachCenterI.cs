using LBD_WebApiInterface.Models.TeachCenter;
using LBD_WebApiInterface.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

//using LBD_WebApiInterface.Other;
//using LG.IntelligentCourse.WebService.Model;

namespace LBD_WebApiInterface.Api
{
    public class TeachCenterI
    {
        private const string C_PreviewApiAddr = "/API/Res/ResPreviewApi.ashx";
        private const string C_LessonPlanApiAddr = "/API/Res/ResLessonPlanApi.ashx";
        private const string C_PracticeApiAddr = "/API/Res/ResPracticeApi.ashx";
        private const string C_BaseInfoApiAddr = "/API/BaseInfo/BaseInfoApi.ashx";

        private string mTeachCenterApiIP;
        private string mTeachCenterApiPort;

        private string mPreviewApiUrl;
        private string mLessonPlanApiUrl;
        private string mPracticeApiUrl;

        private CommandApi mCommandApi;
        //private IntelCoursewareI mIntelCoursewareI;

        public bool Initialize(string strTeachCenterIP, string strTeachCenterPort)
        {
            try
            {
                if (string.IsNullOrEmpty(strTeachCenterIP) || string.IsNullOrEmpty(strTeachCenterPort))
                {
                    return false;
                }

                mTeachCenterApiIP = strTeachCenterIP;
                mTeachCenterApiPort = strTeachCenterPort;

                mPreviewApiUrl = "http://" + mTeachCenterApiIP + ":" + mTeachCenterApiPort + C_PreviewApiAddr;
                mLessonPlanApiUrl = "http://" + mTeachCenterApiIP + ":" + mTeachCenterApiPort + C_LessonPlanApiAddr;
                mPracticeApiUrl = "http://" + mTeachCenterApiIP + ":" + mTeachCenterApiPort + C_PracticeApiAddr;

                mCommandApi = new CommandApi();
                //mIntelCoursewareI = new IntelCoursewareI();
                return true;
            }
            catch (Exception e)
            {
                WriteErrorMessage("Initialize", e.Message);
            }
            return false;
        }

        #region 公共接口

        /// <summary>
        /// 获取教学中心WebSocket地址
        /// </summary>
        public string GetTeachCenterWebSocket()
        {
            try
            {
                //组织接口的访问路径
                string strBaseUrl = "http://" + mTeachCenterApiIP + ":" + mTeachCenterApiPort + C_BaseInfoApiAddr;
                mCommandApi.BaseUrl = strBaseUrl;
                string info = mCommandApi.CallMethodGet("GetTeachCenterWebSocketIPandPort", null);
                if (string.IsNullOrEmpty(info))
                {
                    return "";
                }
                string strUrl = UtilityClass.AnalyseTeachCenterJson<string>(info);
                if (strUrl == null)
                {
                    return "";
                }
                return strUrl;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetTeachCenterWebSocket", e.Message);
            }
            return "";
        }

        /// <summary>
        /// 获取智能化产品配备信息
        /// </summary>
        /// <returns></returns>
        public bool GetIntelligentSysConfigInfo()
        {
            try
            {
                //组织接口的访问路径
                string strBaseUrl = "http://" + mTeachCenterApiIP + ":" + mTeachCenterApiPort + C_BaseInfoApiAddr;
                mCommandApi.BaseUrl = strBaseUrl;
                string strResult = mCommandApi.CallMethodGet("GetBDandInelProductConfigInfo", null);
                if (string.IsNullOrEmpty(strResult))
                {
                    return false;
                }
                LBD_TeachCenterModel.CloudPlatform.BDandInelProductConfigInfoM info = UtilityClass.AnalyseTeachCenterJson<LBD_TeachCenterModel.CloudPlatform.BDandInelProductConfigInfoM>(strResult);
                if (info == null)
                {
                    return false;
                }
                if (info.HasIntelligentSys == 1)
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
                WriteErrorMessage("GetIntelligentSysConfigInfo", e.Message);
                return false;
            }
        }

        #endregion 公共接口

        #region 课前预习相关

        public LBD_WebApiInterface.Models.TeachCenter.PreviewM[] GetPreview(string strTeacherID, string strCloudSubjectID, string strTerm, string strCourseClassID)
        {
            try
            {
                string[] arrParam = new string[11];
                arrParam[0] = strTeacherID;
                arrParam[1] = strCloudSubjectID;
                arrParam[2] = strTerm;
                arrParam[3] = null;//课程班ID
                arrParam[4] = null;//开始时间
                arrParam[5] = null;//结束时间
                arrParam[6] = null;//关键词
                arrParam[7] = "0";//分页
                arrParam[8] = "0";//当前页码
                arrParam[9] = "3";//排序方式
                arrParam[10] = "false";//升序or降序
                mCommandApi.BaseUrl = mPreviewApiUrl;
                string strResult = mCommandApi.CallMethodGet("Res_GetPreviewComplex", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return null;
                }
                LBD_TeachCenterModel.Res.PreviewWithCountM preWithCountM = UtilityClass.AnalyseTeachCenterJson<LBD_TeachCenterModel.Res.PreviewWithCountM>(strResult);
                LBD_TeachCenterModel.Res.PreviewM[] arrPreview = null;
                if (preWithCountM != null && preWithCountM.Preview != null)
                {
                    arrPreview = preWithCountM.Preview;
                }
                if (arrPreview == null || arrPreview.Length == 0)
                {
                    return null;
                }

                //包含指定课程班的
                List<LBD_WebApiInterface.Models.TeachCenter.PreviewM> list1 = new List<LBD_WebApiInterface.Models.TeachCenter.PreviewM>();
                //不包含指定课程班的
                List<LBD_WebApiInterface.Models.TeachCenter.PreviewM> list2 = new List<LBD_WebApiInterface.Models.TeachCenter.PreviewM>();
                for (int i = 0; i < arrPreview.Length; i++)
                {
                    LBD_WebApiInterface.Models.TeachCenter.PreviewM preview = new LBD_WebApiInterface.Models.TeachCenter.PreviewM();
                    preview.ID = arrPreview[i].ID;
                    preview.Name = arrPreview[i].PreviewName;
                    preview.Status = arrPreview[i].Status;
                    preview.SchoolType = arrPreview[i].SchoolType;
                    preview.LevelCode = arrPreview[i].LevelCode;
                    preview.CreateTime = arrPreview[i].CreateTime;
                    if (arrPreview[i].PreCourseware != null)
                    {
                        for (int j = 0; j < arrPreview[i].PreCourseware.Count; j++)
                        {
                            switch (arrPreview[i].PreCourseware[j].CoursewareType)
                            {
                                case 1:
                                case 2:
                                    preview.MuKeID = arrPreview[i].PreCourseware[j].CoursewareID;
                                    preview.MuKeName = arrPreview[i].PreCourseware[j].CoursewareName;
                                    preview.MuKeType = arrPreview[i].PreCourseware[j].CoursewareType;
                                    break;

                                case 3:
                                case 4:
                                case 5:
                                case 6:
                                case 7:
                                case 8:
                                case 9:
                                case 10:
                                case 11:
                                case 12:
                                    preview.ZuoYeID = arrPreview[i].PreCourseware[j].CoursewareID;
                                    preview.ZuoYeName = arrPreview[i].PreCourseware[j].CoursewareName;
                                    preview.ZuoYeType = arrPreview[i].PreCourseware[j].CoursewareType;
                                    break;
                            }
                        }
                    }
                    if (arrPreview[i].PreTask != null)
                    {
                        preview.CourseClassID = arrPreview[i].PreTask.TaskObject;
                        preview.CourseClassName = arrPreview[i].PreTask.TaskObjectName;
                        preview.StartTime = arrPreview[i].PreTask.StartTime;
                        preview.EndTime = arrPreview[i].PreTask.EndTime;
                        if (arrPreview[i].PreTask.SumCount > 0)
                        {
                            preview.Progress = (arrPreview[i].PreTask.CurCount / arrPreview[i].PreTask.SumCount);
                        }
                        else
                        {
                            preview.Progress = 0;
                        }

                        //无课程班
                        if (arrPreview[i].PreTask.TaskObject == null)
                        {
                            list2.Add(preview);
                        }
                        else
                        {
                            //包含指定的课程班
                            if (arrPreview[i].PreTask.TaskObject.Contains<string>(strCourseClassID))
                            {
                                list1.Add(preview);
                            }
                            //不包含指定的课程班
                            else
                            {
                                list2.Add(preview);
                            }
                        }
                    }
                    else
                    {
                        list2.Add(preview);
                    }
                }

                list1.AddRange(list2);
                if (list1.Count > 0)
                {
                    return list1.ToArray();
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetPreview", e.Message);
            }
            return null;
        }

        public LBD_WebApiInterface.Models.TeachCenter.PreviewM GetPreviewByID(string strPreviewID)
        {
            try
            {
                string[] arrParam = new string[1];
                arrParam[0] = strPreviewID;
                mCommandApi.BaseUrl = mPreviewApiUrl;
                string strResult = mCommandApi.CallMethodGet("Res_GetPreviewByID", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return null;
                }
                LBD_TeachCenterModel.Res.PreviewM preview = UtilityClass.AnalyseTeachCenterJson<LBD_TeachCenterModel.Res.PreviewM>(strResult);
                if (preview == null)
                {
                    return null;
                }

                LBD_WebApiInterface.Models.TeachCenter.PreviewM preview2 = new LBD_WebApiInterface.Models.TeachCenter.PreviewM();
                preview2.ID = preview.ID;
                preview2.Name = preview.PreviewName;
                preview2.Status = preview.Status;
                preview2.SchoolType = preview.SchoolType;
                preview2.LevelCode = preview.LevelCode;
                preview2.CreateTime = preview.CreateTime;
                if (preview.PreCourseware != null)
                {
                    for (int j = 0; j < preview.PreCourseware.Count; j++)
                    {
                        switch (preview.PreCourseware[j].CoursewareType)
                        {
                            case 1:
                            case 2:
                                preview2.MuKeID = preview.PreCourseware[j].CoursewareID;
                                preview2.MuKeName = preview.PreCourseware[j].CoursewareName;
                                preview2.MuKeType = preview.PreCourseware[j].CoursewareType;
                                break;

                            case 3:
                            case 4:
                            case 5:
                            case 6:
                            case 7:
                            case 8:
                            case 9:
                            case 10:
                            case 11:
                            case 12:
                                preview2.ZuoYeID = preview.PreCourseware[j].CoursewareID;
                                preview2.ZuoYeName = preview.PreCourseware[j].CoursewareName;
                                preview2.ZuoYeType = preview.PreCourseware[j].CoursewareType;
                                break;
                        }
                    }
                }
                if (preview.PreTask != null)
                {
                    preview2.CourseClassID = preview.PreTask.TaskObject;
                    preview2.CourseClassName = preview.PreTask.TaskObjectName;
                    preview2.StartTime = preview.PreTask.StartTime;
                    preview2.EndTime = preview.PreTask.EndTime;
                }
                if (preview.PreTask.SumCount > 0)
                {
                    preview2.Progress = (preview.PreTask.CurCount / preview.PreTask.SumCount);
                }
                else
                {
                    preview2.Progress = 0;
                }

                return preview2;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetPreviewByID", e.Message);
                return null;
            }
        }

        #endregion 课前预习相关

        #region 课堂教案相关

        //返回的数据会把指定班级的放在前面，同时把非此班级的带在后面
        //教学计划从1个变成可以多个，此接口将只返回第一个教学计划，若想获取多个教学计划，用GetLessonPlan_MultTask
        public LBD_WebApiInterface.Models.TeachCenter.LessonPlanM[] GetLessonPlan(string strTeacherID, string strCloudSubjectID, string strTerm, string strCourseClassID)
        {
            try
            {
                string[] arrParam = new string[11];
                arrParam[0] = strTeacherID;
                arrParam[1] = strCloudSubjectID;
                arrParam[2] = strTerm;
                arrParam[3] = null;//课程班ID
                arrParam[4] = null;//开始时间
                arrParam[5] = null;//结束时间
                arrParam[6] = null;//关键词
                arrParam[7] = "0";//分页
                arrParam[8] = "0";//当前页码
                arrParam[9] = "3";//排序方式
                arrParam[10] = "false";//升序or降序
                mCommandApi.BaseUrl = mLessonPlanApiUrl;
                string strResult = mCommandApi.CallMethodGet("Res_GetLessonPlanComplex", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return null;
                }
                LBD_TeachCenterModel.Res.LessonPlanWithCount lpWithCount = UtilityClass.AnalyseTeachCenterJson<LBD_TeachCenterModel.Res.LessonPlanWithCount>(strResult);
                LBD_TeachCenterModel.Res.LessonPlanM[] arrLessonPlan = null;
                if (lpWithCount != null && lpWithCount.LessonPlan != null)
                {
                    arrLessonPlan = lpWithCount.LessonPlan;
                }
                if (arrLessonPlan == null || arrLessonPlan.Length == 0)
                {
                    return null;
                }

                List<LBD_WebApiInterface.Models.TeachCenter.LessonPlanM> list1 = new List<LBD_WebApiInterface.Models.TeachCenter.LessonPlanM>();
                List<LBD_WebApiInterface.Models.TeachCenter.LessonPlanM> list2 = new List<LBD_WebApiInterface.Models.TeachCenter.LessonPlanM>();
                for (int i = 0; i < arrLessonPlan.Length; i++)
                {
                    LBD_WebApiInterface.Models.TeachCenter.LessonPlanM lessonPlan = new LBD_WebApiInterface.Models.TeachCenter.LessonPlanM();
                    lessonPlan.ID = arrLessonPlan[i].ID;
                    lessonPlan.Name = arrLessonPlan[i].LessonPlanName;
                    lessonPlan.Status = arrLessonPlan[i].Status;
                    lessonPlan.SchoolType = arrLessonPlan[i].SchoolType;
                    lessonPlan.LevelCode = arrLessonPlan[i].LevelCode;
                    lessonPlan.CreateTime = arrLessonPlan[i].CreateTime;
                    if (arrLessonPlan[i].LPCourseware != null)
                    {
                        for (int j = 0; j < arrLessonPlan[i].LPCourseware.Count; j++)
                        {
                            switch (arrLessonPlan[i].LPCourseware[j].CoursewareType)
                            {
                                case 1:
                                case 2:
                                    lessonPlan.KeWenJiangJieID = arrLessonPlan[i].LPCourseware[j].CoursewareID;
                                    lessonPlan.KeWenJiangJieName = arrLessonPlan[i].LPCourseware[j].CoursewareName;
                                    break;

                                case 3:
                                case 4:
                                    lessonPlan.ZhongNanDianID = arrLessonPlan[i].LPCourseware[j].CoursewareID;
                                    lessonPlan.ZhongNanDianName = arrLessonPlan[i].LPCourseware[j].CoursewareName;
                                    break;

                                case 5:
                                case 6:
                                case 7:
                                case 8:
                                    lessonPlan.SuiTangCeShiID = arrLessonPlan[i].LPCourseware[j].CoursewareID;
                                    lessonPlan.SuiTangCeShiName = arrLessonPlan[i].LPCourseware[j].CoursewareName;
                                    break;
                            }
                        }
                    }
                    if (arrLessonPlan[i].LPTask != null && arrLessonPlan[i].LPTask.Count > 0)
                    {
                        lessonPlan.CourseClassID = arrLessonPlan[i].LPTask[0].TaskObject;
                        lessonPlan.CourseClassName = arrLessonPlan[i].LPTask[0].TaskObjectName;
                        lessonPlan.TeachTime = arrLessonPlan[i].LPTask[0].TeachTime;
                        lessonPlan.Classroom = arrLessonPlan[i].LPTask[0].Classroom;

                        if (arrLessonPlan[i].LPTask[0].TaskObject == null)
                        {
                            list2.Add(lessonPlan);
                        }
                        else
                        {
                            if (arrLessonPlan[i].LPTask[0].TaskObject.Contains<string>(strCourseClassID))
                            {
                                list1.Add(lessonPlan);
                            }
                            else
                            {
                                list2.Add(lessonPlan);
                            }
                        }
                    }
                    else
                    {
                        list2.Add(lessonPlan);
                    }
                }

                list1.AddRange(list2);
                if (list1.Count > 0)
                {
                    return list1.ToArray();
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetLessonPlan", e.Message);
            }
            return null;
        }

        public LBD_WebApiInterface.Models.TeachCenter.LessonPlanM GetLessonPlanByID(string strLessonPlanID)
        {
            try
            {
                string[] arrParam = new string[1];
                arrParam[0] = strLessonPlanID;
                mCommandApi.BaseUrl = mLessonPlanApiUrl;
                string strResult = mCommandApi.CallMethodGet("Res_GetLessonPlanByID", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return null;
                }
                LBD_TeachCenterModel.Res.LessonPlanM lessonPlan = UtilityClass.AnalyseTeachCenterJson<LBD_TeachCenterModel.Res.LessonPlanM>(strResult);
                if (lessonPlan == null)
                {
                    return null;
                }

                LBD_WebApiInterface.Models.TeachCenter.LessonPlanM lessonPlan2 = new LBD_WebApiInterface.Models.TeachCenter.LessonPlanM();
                lessonPlan2.ID = lessonPlan.ID;
                lessonPlan2.Name = lessonPlan.LessonPlanName;
                lessonPlan2.Status = lessonPlan.Status;
                lessonPlan2.SchoolType = lessonPlan.SchoolType;
                lessonPlan2.LevelCode = lessonPlan.LevelCode;
                lessonPlan2.CreateTime = lessonPlan.CreateTime;
                if (lessonPlan.LPCourseware != null)
                {
                    for (int j = 0; j < lessonPlan.LPCourseware.Count; j++)
                    {
                        switch (lessonPlan.LPCourseware[j].CoursewareType)
                        {
                            case 1:
                            case 2:
                                lessonPlan2.KeWenJiangJieID = lessonPlan.LPCourseware[j].CoursewareID;
                                lessonPlan2.KeWenJiangJieName = lessonPlan.LPCourseware[j].CoursewareName;
                                break;

                            case 3:
                            case 4:
                                lessonPlan2.ZhongNanDianID = lessonPlan.LPCourseware[j].CoursewareID;
                                lessonPlan2.ZhongNanDianName = lessonPlan.LPCourseware[j].CoursewareName;
                                break;

                            case 5:
                            case 6:
                            case 7:
                            case 8:
                                lessonPlan2.SuiTangCeShiID = lessonPlan.LPCourseware[j].CoursewareID;
                                lessonPlan2.SuiTangCeShiName = lessonPlan.LPCourseware[j].CoursewareName;
                                break;
                        }
                    }
                }
                if (lessonPlan.LPTask != null)
                {
                    lessonPlan2.CourseClassID = lessonPlan.LPTask[0].TaskObject;
                    lessonPlan2.CourseClassName = lessonPlan.LPTask[0].TaskObjectName;
                    lessonPlan2.TeachTime = lessonPlan.LPTask[0].TeachTime;
                    lessonPlan2.Classroom = lessonPlan.LPTask[0].Classroom;
                }

                return lessonPlan2;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetLessonPlanByID", e.Message);
            }
            return null;
        }

        public LBD_WebApiInterface.Models.TeachCenter.Get_LessonPlanM[] GetLessonPlan_MultTask(string strTeacherID, string strCloudSubjectID, string strTerm, string strCourseClassID)
        {
            try
            {
                string[] arrParam = new string[11];
                arrParam[0] = strTeacherID;
                arrParam[1] = strCloudSubjectID;
                arrParam[2] = strTerm;
                arrParam[3] = null;//课程班ID
                arrParam[4] = null;//开始时间
                arrParam[5] = null;//结束时间
                arrParam[6] = null;//关键词
                arrParam[7] = "0";//分页
                arrParam[8] = "0";//当前页码
                arrParam[9] = "3";//排序方式
                arrParam[10] = "false";//升序or降序
                mCommandApi.BaseUrl = mLessonPlanApiUrl;
                string strResult = mCommandApi.CallMethodGet("Res_GetLessonPlanComplex", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return null;
                }
                LBD_TeachCenterModel.Res.LessonPlanWithCount lpWithCount = UtilityClass.AnalyseTeachCenterJson<LBD_TeachCenterModel.Res.LessonPlanWithCount>(strResult);
                LBD_TeachCenterModel.Res.LessonPlanM[] arrLessonPlan = null;
                if (lpWithCount != null && lpWithCount.LessonPlan != null)
                {
                    arrLessonPlan = lpWithCount.LessonPlan;
                }
                if (arrLessonPlan == null || arrLessonPlan.Length == 0)
                {
                    return null;
                }

                List<LBD_WebApiInterface.Models.TeachCenter.Get_LessonPlanM> list1 = new List<LBD_WebApiInterface.Models.TeachCenter.Get_LessonPlanM>();
                List<LBD_WebApiInterface.Models.TeachCenter.Get_LessonPlanM> list2 = new List<LBD_WebApiInterface.Models.TeachCenter.Get_LessonPlanM>();
                for (int i = 0; i < arrLessonPlan.Length; i++)
                {
                    LBD_WebApiInterface.Models.TeachCenter.Get_LessonPlanM lessonPlan = new LBD_WebApiInterface.Models.TeachCenter.Get_LessonPlanM();
                    lessonPlan.ID = arrLessonPlan[i].ID;
                    lessonPlan.Name = arrLessonPlan[i].LessonPlanName;
                    lessonPlan.Status = arrLessonPlan[i].Status;
                    lessonPlan.SchoolType = arrLessonPlan[i].SchoolType;
                    lessonPlan.LevelCode = arrLessonPlan[i].LevelCode;
                    lessonPlan.CreateTime = arrLessonPlan[i].CreateTime;
                    if (arrLessonPlan[i].LPCourseware != null)
                    {
                        for (int j = 0; j < arrLessonPlan[i].LPCourseware.Count; j++)
                        {
                            Get_LPCoursewareM cour = new Get_LPCoursewareM();
                            cour.CoursewareID = arrLessonPlan[i].LPCourseware[j].CoursewareID;
                            cour.CoursewareName = arrLessonPlan[i].LPCourseware[j].CoursewareName;
                            cour.CoursewareType = arrLessonPlan[i].LPCourseware[j].CoursewareType;
                            cour.CoursewarePicPath = arrLessonPlan[i].LPCourseware[j].CoursewarePicPath;
                            lessonPlan.LPCourseware.Add(cour);
                        }
                    }

                    if (arrLessonPlan[i].LPTask != null && arrLessonPlan[i].LPTask.Count > 0)
                    {
                        Get_LPTaskM task = null;
                        bool bContainsClass = false;
                        for (int j = 0; j < arrLessonPlan[i].LPTask.Count; j++)
                        {
                            task = new Get_LPTaskM();
                            task.ID = arrLessonPlan[i].LPTask[j].ID;
                            task.CourseClassID = arrLessonPlan[i].LPTask[j].TaskObject;
                            task.CourseClassName = arrLessonPlan[i].LPTask[j].TaskObjectName;
                            task.TeachTime = arrLessonPlan[i].LPTask[j].TeachTime;
                            task.ClassroomID = arrLessonPlan[i].LPTask[j].ClassroomID;
                            task.Classroom = arrLessonPlan[i].LPTask[j].Classroom;
                            lessonPlan.LPTask.Add(task);

                            if (arrLessonPlan[i].LPTask[j].TaskObject != null)
                            {
                                if (arrLessonPlan[i].LPTask[j].TaskObject.Contains<string>(strCourseClassID))
                                {
                                    bContainsClass = true;
                                }
                            }
                        }

                        if (bContainsClass == true)
                        {
                            list1.Add(lessonPlan);
                        }
                        else
                        {
                            list2.Add(lessonPlan);
                        }
                    }
                    else
                    {
                        list2.Add(lessonPlan);
                    }
                }

                list1.AddRange(list2);
                if (list1.Count > 0)
                {
                    return list1.ToArray();
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetLessonPlan_MultTask", e.Message);
            }
            return null;
        }

        public LBD_WebApiInterface.Models.TeachCenter.Get_LessonPlanM GetLessonPlanByID_MultTask(string strLessonPlanID)
        {
            try
            {
                string[] arrParam = new string[1];
                arrParam[0] = strLessonPlanID;
                mCommandApi.BaseUrl = mLessonPlanApiUrl;
                string strResult = mCommandApi.CallMethodGet("Res_GetLessonPlanByID", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return null;
                }
                LBD_TeachCenterModel.Res.LessonPlanM lessonPlan = UtilityClass.AnalyseTeachCenterJson<LBD_TeachCenterModel.Res.LessonPlanM>(strResult);
                if (lessonPlan == null)
                {
                    return null;
                }

                LBD_WebApiInterface.Models.TeachCenter.Get_LessonPlanM lessonPlan2 = new LBD_WebApiInterface.Models.TeachCenter.Get_LessonPlanM();
                lessonPlan2.ID = lessonPlan.ID;
                lessonPlan2.Name = lessonPlan.LessonPlanName;
                lessonPlan2.Status = lessonPlan.Status;
                lessonPlan2.SchoolType = lessonPlan.SchoolType;
                lessonPlan2.LevelCode = lessonPlan.LevelCode;
                lessonPlan2.CreateTime = lessonPlan.CreateTime;
                if (lessonPlan.LPCourseware != null)
                {
                    for (int j = 0; j < lessonPlan.LPCourseware.Count; j++)
                    {
                        Get_LPCoursewareM cour = new Get_LPCoursewareM();
                        cour.CoursewareID = lessonPlan.LPCourseware[j].CoursewareID;
                        cour.CoursewareName = lessonPlan.LPCourseware[j].CoursewareName;
                        cour.CoursewareType = lessonPlan.LPCourseware[j].CoursewareType;
                        cour.CoursewarePicPath = lessonPlan.LPCourseware[j].CoursewarePicPath;
                        lessonPlan2.LPCourseware.Add(cour);
                    }
                }

                if (lessonPlan.LPTask != null && lessonPlan.LPTask.Count > 0)
                {
                    Get_LPTaskM task = null;
                    for (int j = 0; j < lessonPlan.LPTask.Count; j++)
                    {
                        task = new Get_LPTaskM();
                        task.ID = lessonPlan.LPTask[j].ID;
                        task.CourseClassID = lessonPlan.LPTask[j].TaskObject;
                        task.CourseClassName = lessonPlan.LPTask[j].TaskObjectName;
                        task.TeachTime = lessonPlan.LPTask[j].TeachTime;
                        task.ClassroomID = lessonPlan.LPTask[j].ClassroomID;
                        task.Classroom = lessonPlan.LPTask[j].Classroom;
                        lessonPlan2.LPTask.Add(task);
                    }
                }

                return lessonPlan2;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetLessonPlanByID_MultTask", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 获取课堂教案下各课件的设置信息
        /// </summary>
        public LBD_WebApiInterface.Models.TeachCenter.LP_CoursewareSetInfoM[] GetLPCoursewareSetInfo(string strLessonPlanID, List<Get_LPCoursewareM> courseware)
        {
            LBD_WebApiInterface.Models.TeachCenter.LP_CoursewareSetInfoM[] arrResult = null;
            try
            {
                if (string.IsNullOrEmpty(strLessonPlanID))
                {
                    return null;
                }

                if (courseware == null || courseware.Count == 0)
                {
                    return null;
                }

                string[] arrParam = new string[1];
                arrParam[0] = strLessonPlanID;
                mCommandApi.BaseUrl = mLessonPlanApiUrl;
                string strResult = mCommandApi.CallMethodGet("Res_GetLPCoursewareSetInfo", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return null;
                }
                List<LBD_TeachCenterModel.Res.LP_CoursewareSetInfoM_Whole> list = UtilityClass.AnalyseTeachCenterJson<List<LBD_TeachCenterModel.Res.LP_CoursewareSetInfoM_Whole>>(strResult);
                if (list == null || list.Count == 0)
                {
                    return null;
                }

                arrResult = new LP_CoursewareSetInfoM[courseware.Count];
                for (int i = 0; i < courseware.Count; i++)
                {
                    arrResult[i] = null;

                    for (int j = 0; j < list.Count; j++)
                    {
                        if (courseware[i].CoursewareID == list[j].CoursewareID)
                        {
                            arrResult[i] = new LP_CoursewareSetInfoM();
                            arrResult[i].HuPing = list[j].CoursewareSetInfo.HuPing;
                            arrResult[i].TeachDuration = list[j].CoursewareSetInfo.TeachDuration;
                            arrResult[i].DaXiaoTi = list[j].CoursewareSetInfo.DaXiaoTi;

                            courseware[i].CoursewareSetInfo = arrResult[i];
                            break;
                        }
                    }
                }

                return arrResult;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetLPCoursewareSetInfo", e.Message);
                return null;
            }
        }

        /// <summary>
        /// 获取课堂教案下课件的FTP路径
        /// </summary>
        //public string[] GetLPCoursewareFtpPath(List<Get_LPCoursewareM> courseware, string strNetTeachIPandPort,string strZhiNengBeiKeIPandPort, string strTeacherID)
        //{
        //    try
        //    {
        //        if (courseware == null || courseware.Count == 0)
        //        {
        //            return null;
        //        }

        //        string[] arrPath = new string[courseware.Count];
        //        for (int i = 0; i < courseware.Count; i++)
        //        {
        //            switch (courseware[i].CoursewareType)
        //            {
        //                case 1:
        //                case 3:
        //                    arrPath[i] = P_GetCoursewareFtpPath1(courseware[i].CoursewareID, strNetTeachIPandPort);
        //                    break;
        //                //与智能化备课
        //                case 2:
        //                case 4:
        //                    arrPath[i] = P_GetCoursewareFtpPath2(courseware[i].CoursewareID, strTeacherID, strZhiNengBeiKeIPandPort);
        //                    break;
        //                //试卷不需要FTP路径，不处理
        //                case 5:
        //                case 6:
        //                case 7:
        //                case 8:
        //                    arrPath[i] = "";
        //                    break;
        //            }
        //            courseware[i].FtpPath = arrPath[i];
        //        }
        //        return arrPath;
        //    }
        //    catch (Exception e)
        //    {
        //        WriteErrorMessage("GetLPCoursewareFtpPath", e.Message);
        //    }
        //    return null;
        //}

        //获取人工制作的课文讲解、重难点讲解课件的FTP
        private string P_GetCoursewareFtpPath1(string strCoursewareID, string strNetTeachIPandPort)
        {
            try
            {
                string[] arrParam = new string[1];
                arrParam[0] = strCoursewareID;
                string[] arr = strNetTeachIPandPort.Split(':');
                mCommandApi.BaseUrl = string.Format(Properties.Resources.NetCoursewareUrl, arr[0], arr[1], "");
                string strResult = mCommandApi.CallMethodGet("GetCoursewarePath", arrParam);
                return strResult;
            }
            catch (Exception e)
            {
                WriteErrorMessage("P_GetCoursewareFtpPath1", e.Message);
            }
            return "";
        }

        //获取智能课件的FTP路径
        //private string P_GetCoursewareFtpPath2(string strCoursewareID, string strTeacherID, string strZhiNengBeiKeIPandPort)
        //{
        //    try
        //    {
        //        bool bSetResult = mIntelCoursewareI.SetZhiNengBeiKeUrl(strZhiNengBeiKeIPandPort);
        //        if (bSetResult == false)
        //        {
        //            return "";
        //        }

        //        lgdt_courseware cour = mIntelCoursewareI.GetCoursewareByID(strTeacherID, strCoursewareID);

        //        if (cour != null)
        //        {
        //            return cour.FilePath;
        //        }
        //        else
        //        {
        //            return "";
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        WriteErrorMessage("P_GetCoursewareFtpPath2", e.Message);
        //        return "";
        //    }
        //}

        /// <summary>
        /// 获取智能化备课系统的FTP和HTTP信息
        /// </summary>
        //public IntelCoursewareSysConfig GetZhiNengBeiKeSysConfig(string strZhiNengBeiKeIPandPort)
        //{
        //    try
        //    {
        //        bool bSetResult = mIntelCoursewareI.SetZhiNengBeiKeUrl(strZhiNengBeiKeIPandPort);
        //        if (bSetResult == false)
        //        {
        //            return null;
        //        }
        //        IntelCoursewareSysConfig sysInfo = mIntelCoursewareI.GetSysConfig();
        //        return sysInfo;
        //    }
        //    catch (Exception e)
        //    {
        //        WriteErrorMessage("GetZhiNengBeiKeSysConfig", e.Message);
        //        return null;
        //    }
        //}

        /// <summary>
        /// 获取智能化课件的图片路径
        /// </summary>
        //public string GetIntelCoursewarePicPath(string strZhiNengBeiKeIPandPort, string strCoursewareID,string strTeacherID)
        //{
        //    try
        //    {
        //        bool bSetResult = mIntelCoursewareI.SetZhiNengBeiKeUrl(strZhiNengBeiKeIPandPort);
        //        if (bSetResult == false)
        //        {
        //            return "";
        //        }

        //        lgdt_courseware cour = mIntelCoursewareI.GetCoursewareByID(strTeacherID, strCoursewareID);
        //        if (cour != null)
        //        {
        //            return cour.CoverImage;
        //        }
        //        else
        //        {
        //            return "";
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        WriteErrorMessage("GetIntelCoursewarePicPath", e.Message);
        //        return "";
        //    }
        //}

        /// <summary>
        /// 保存课堂教案，返回教案ID，失败时返回null
        /// </summary>
        public string SaveLessonPlan(Save_LessonPlanM lessonPlan)
        {
            try
            {
                if (lessonPlan == null)
                {
                    return "";
                }
                if (lessonPlan.LPCourseware == null || lessonPlan.LPCourseware.Count == 0)
                {
                    return "";
                }

                string[] arrParam = new string[1];
                arrParam[0] = JsonFormatter.JsonSerialize(lessonPlan);
                mCommandApi.BaseUrl = mLessonPlanApiUrl;
                string strResult = mCommandApi.CallMethodPost_TC("Res_SaveLessonPlan", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return null;
                }
                Debug.WriteLine("strResult=" + strResult, "TeachCenterI-SaveLessonPlan");
                string strID = UtilityClass.AnalyseTeachCenterJson<string>(strResult);
                return strID;
            }
            catch (Exception e)
            {
                WriteErrorMessage("SaveLessonPlan", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 保存课堂教案的课件，返回插入课件生成的ID，失败时返回null
        /// </summary>
        public long[] SaveLPCourseware(string strLessonPlanID, string strCreator, List<Save_LPCoursewareM> LPCourseware)
        {
            try
            {
                if (string.IsNullOrEmpty(strLessonPlanID))
                {
                    return new long[0];
                }
                if (LPCourseware == null || LPCourseware.Count == 0)
                {
                    return new long[0];
                }

                string[] arrParam = new string[3];
                arrParam[0] = strLessonPlanID;
                arrParam[1] = strCreator;
                arrParam[2] = JsonFormatter.JsonSerialize(LPCourseware);
                mCommandApi.BaseUrl = mLessonPlanApiUrl;
                string strResult = mCommandApi.CallMethodPost_TC("Res_SaveLPCourseware", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return null;
                }
                long[] arrID = UtilityClass.AnalyseTeachCenterJson<long[]>(strResult);
                return arrID;
            }
            catch (Exception e)
            {
                WriteErrorMessage("SaveLPCourseware", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 保存课堂教案的教学计划，返回插入教学计划生成的ID，失败时返回null
        /// </summary>
        public long[] SaveLPTask(string strLessonPlanID, string strCreator, List<Save_LPTaskM> LPTask)
        {
            try
            {
                if (string.IsNullOrEmpty(strLessonPlanID))
                {
                    return null;
                }
                if (LPTask == null)
                {
                    return null;
                }

                string[] arrParam = new string[3];
                arrParam[0] = strLessonPlanID;
                arrParam[1] = strCreator;
                arrParam[2] = JsonFormatter.JsonSerialize(LPTask);
                mCommandApi.BaseUrl = mLessonPlanApiUrl;
                string strResult = mCommandApi.CallMethodPost_TC("Res_SaveLessonPlanTask", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return null;
                }
                long[] arrID = UtilityClass.AnalyseTeachCenterJson<long[]>(strResult);
                return arrID;
            }
            catch (Exception e)
            {
                WriteErrorMessage("SaveLPTask", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 获取可供选择的课件。
        /// strType="KWJJ"或者"ZNDJJ"分别代表课文讲解课件或重难点讲解课件；
        /// ManulOrIntel=1代表人工，=2代表智能；
        /// iOrderMode=0代表按最后修改时间排序，=1代表按创建时间排序；
        /// </summary>
        //public Get_LPCoursewareM[] GetOptionalCourseware(string strNetTeachIPandPort, string strZhiNengBeiKeIPandPort, string strType, int ManulOrIntel, string strTeacherID, string strSubjectID, string strTerm, string strKeyword,
        //    int iPageSize, int iPageIndex, int iOrderMode, bool bAscOrDesc, out long SumCount)
        //{
        //    SumCount = 0;
        //    Get_LPCoursewareM[] arrCour = null;
        //    try
        //    {
        //        if (ManulOrIntel == 1)
        //        {
        //            string[] arrParam = new string[8];
        //            arrParam[0] = strTeacherID;
        //            arrParam[1] = strSubjectID;
        //            arrParam[2] = strType;
        //            arrParam[3] = strKeyword;
        //            arrParam[4] = iPageSize.ToString();
        //            arrParam[5] = iPageIndex.ToString();
        //            arrParam[6] = iOrderMode.ToString();
        //            arrParam[7] = bAscOrDesc.ToString();
        //            string[] arr = strNetTeachIPandPort.Split(':');
        //            mCommandApi.BaseUrl = string.Format(Properties.Resources.NetCoursewareUrl, arr[0], arr[1]);
        //            string strResult = mCommandApi.CallMethodGet("GetCoursewareForTC", arrParam);
        //            if (string.IsNullOrEmpty(strResult))
        //            {
        //                return null;
        //            }
        //            NetCoursewareWithCountM ncwm = JsonFormatter.JsonDeserialize<NetCoursewareWithCountM>(strResult);
        //            SumCount = ncwm.SumCount;
        //            List<NetCoursewareM> list = ncwm.NetCourseware;
        //            if (list == null || list.Count == 0)
        //            {
        //                return null;
        //            }
        //            arrCour = new Get_LPCoursewareM[list.Count];
        //            for (int i = 0; i < list.Count; i++)
        //            {
        //                arrCour[i] = new Get_LPCoursewareM();
        //                arrCour[i].CoursewareID = list[i].CoursewareID;
        //                arrCour[i].CoursewareName = list[i].CoursewareName;
        //                if (list[i].CoursewareType == "KWJJ")
        //                {
        //                    arrCour[i].CoursewareType = 1;
        //                }
        //                else if (list[i].CoursewareType == "ZNDJJ")
        //                {
        //                    arrCour[i].CoursewareType = 3;
        //                }
        //                arrCour[i].FtpPath = list[i].CoursewarePath;
        //                arrCour[i].CoursewarePicPath = list[i].CoursewarePicPath;
        //                arrCour[i].Creator = list[i].CreatorID;
        //                arrCour[i].LastEditTime = list[i].LastEditTime;
        //                arrCour[i].UseTimes = list[i].UseTimes;
        //                arrCour[i].ShareStatus = list[i].IsShare ? (byte)1 : (byte)0;
        //                if (arrCour[i].CoursewareSetInfo == null)
        //                {
        //                    arrCour[i].CoursewareSetInfo = new LP_CoursewareSetInfoM();
        //                }
        //                arrCour[i].CoursewareSetInfo.TeachDuration = list[i].CoursewareTime;
        //            }
        //        }
        //        else if (ManulOrIntel == 2)
        //        {
        //            bool bSetResult = mIntelCoursewareI.SetZhiNengBeiKeUrl(strZhiNengBeiKeIPandPort);
        //            if (bSetResult == false)
        //            {
        //                return null;
        //            }
        //            //智能化课件只有英语学科
        //            if (strSubjectID.Contains("English"))
        //            {
        //                string strCoursewareType = "1";
        //                switch (strType)
        //                {
        //                    case "KWJJ":
        //                        strCoursewareType = "1";//课文讲解
        //                        break;
        //                    case "ZNDJJ":
        //                        strCoursewareType = "2";//重难点讲解
        //                        break;
        //                }
        //                string strSortField = "";
        //                switch (iOrderMode)
        //                {
        //                    case 0:
        //                        strSortField = "ModifiedTime";
        //                        break;
        //                    case 1:
        //                        strSortField = "CreateTime";
        //                        break;
        //                    default:
        //                        strSortField = "ModifiedTime";
        //                        break;
        //                }
        //                LG.IntelligentCourse.WebService.Common.Model.CommonEnum.SortType sortType = LG.IntelligentCourse.WebService.Common.Model.CommonEnum.SortType.Desc;
        //                if (bAscOrDesc)
        //                {
        //                    sortType = LG.IntelligentCourse.WebService.Common.Model.CommonEnum.SortType.Asc;
        //                }
        //                else
        //                {
        //                    sortType = LG.IntelligentCourse.WebService.Common.Model.CommonEnum.SortType.Desc;
        //                }
        //                int iSumCount = 0;
        //                lgdt_courseware[] arrIntel = mIntelCoursewareI.GetCourseware(strTeacherID, strTerm, strCoursewareType, strKeyword, iPageSize, iPageIndex, strSortField, sortType, out iSumCount);
        //                SumCount = iSumCount;
        //                if (arrIntel == null || arrIntel.Length == 0)
        //                {
        //                    return null;
        //                }
        //                arrCour = new Get_LPCoursewareM[arrIntel.Length];
        //                for (int i = 0; i < arrCour.Length; i++)
        //                {
        //                    arrCour[i] = new Get_LPCoursewareM();
        //                    arrCour[i].CoursewareID = arrIntel[i].Id.ToString();
        //                    arrCour[i].CoursewareName = arrIntel[i].Name;
        //                    arrCour[i].CoursewarePicPath = arrIntel[i].CoverImage;//相对路径
        //                    switch (strType)
        //                    {
        //                        case "KWJJ":
        //                            arrCour[i].CoursewareType = 2;//课文讲解
        //                            break;
        //                        case "ZNDJJ":
        //                            arrCour[i].CoursewareType = 4;//重难点讲解
        //                            break;
        //                    }
        //                    arrCour[i].Creator = arrIntel[i].TeacherId;
        //                    arrCour[i].LastEditTime = arrIntel[i].ModifiedTime;
        //                    arrCour[i].FtpPath = arrIntel[i].FilePath;//相对路径
        //                    arrCour[i].UseTimes = 0;
        //                    arrCour[i].ShareStatus = arrIntel[i].ShareFlag;
        //                    if (arrCour[i].CoursewareSetInfo == null)
        //                    {
        //                        arrCour[i].CoursewareSetInfo = new LP_CoursewareSetInfoM();
        //                    }
        //                    if (arrIntel[i].LessonTime == null)
        //                    {
        //                        arrCour[i].CoursewareSetInfo.TeachDuration = 0;
        //                    }
        //                    else
        //                    {
        //                        arrCour[i].CoursewareSetInfo.TeachDuration = Convert.ToInt32(arrIntel[i].LessonTime.TotalTime * 60);
        //                    }
        //                }
        //            }
        //        }

        //        return arrCour;
        //    }
        //    catch (Exception e)
        //    {
        //        WriteErrorMessage("GetOptionalCourseware", e.Message);
        //        return null;
        //    }
        //}

        public bool UpdateLessonPlan(string strLessonPlanID, string strTeacherID, string strLessonPlanName)
        {
            try
            {
                string[] arrParam = new string[4];
                arrParam[0] = strLessonPlanID;
                arrParam[1] = strTeacherID;
                arrParam[2] = strLessonPlanName;
                arrParam[3] = null;
                mCommandApi.BaseUrl = mLessonPlanApiUrl;
                string strResult = mCommandApi.CallMethodPost_TC("Res_UpdateLessonPlan", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return false;
                }
                bool bResult = UtilityClass.AnalyseTeachCenterJson<bool>(strResult);
                return bResult;
            }
            catch (Exception e)
            {
                WriteErrorMessage("UpdateLessonPlan", e.Message);
            }
            return false;
        }

        /// <summary>
        /// 当使用了某个课堂教案时调用此接口，从“未授课”变更为“已授课”
        /// </summary>
        public bool HasUsedLessonPlan(string strLessonPlanID, string strTeacherID)
        {
            try
            {
                string[] arrParam = new string[4];
                arrParam[0] = strLessonPlanID;
                arrParam[1] = strTeacherID;
                arrParam[2] = null;
                arrParam[3] = "1";
                mCommandApi.BaseUrl = mLessonPlanApiUrl;
                string strResult = mCommandApi.CallMethodPost_TC("Res_UpdateLessonPlan", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return false;
                }
                bool bResult = UtilityClass.AnalyseTeachCenterJson<bool>(strResult);

                if (bResult == false)
                {
                    return false;
                }

                string[] arrLessonPlanID = new string[1] { strLessonPlanID };
                string[] arrParam2 = new string[2];
                arrParam2[0] = JsonFormatter.JsonSerialize(arrLessonPlanID);
                arrParam2[1] = strTeacherID;

                strResult = mCommandApi.CallMethodPost_TC("Res_UpdateLessonPlanUseTimes", arrParam2);
                if (string.IsNullOrEmpty(strResult))
                {
                    return false;
                }
                int iRowCount = UtilityClass.AnalyseTeachCenterJson<int>(strResult);
                if (iRowCount == arrLessonPlanID.Length)
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
                WriteErrorMessage("HasUsedLessonPlan", e.Message);
            }
            return false;
        }

        /// <summary>
        /// 删除课堂教案下的课件。arrCoursewareType长度需与arrCoursewareID保持一致
        /// </summary>
        public bool DeleteLPCourseware(string strLessonPlanID, string[] arrCoursewareID, byte[] arrCoursewareType, string strLastEditor)
        {
            try
            {
                string[] arrParam = new string[4];
                arrParam[0] = strLessonPlanID;
                arrParam[1] = JsonFormatter.JsonSerialize(arrCoursewareID);
                arrParam[2] = JsonFormatter.JsonSerialize(arrCoursewareType);
                arrParam[3] = strLastEditor;
                mCommandApi.BaseUrl = mLessonPlanApiUrl;
                string strResult = mCommandApi.CallMethodPost_TC("Res_DeleteLPCourseware", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return false;
                }
                bool bResult = UtilityClass.AnalyseTeachCenterJson<bool>(strResult);
                return bResult;
            }
            catch (Exception e)
            {
                WriteErrorMessage("DeleteLPCourseware", e.Message);
            }
            return false;
        }

        public bool DeleteLessonPlanTask(long[] arrTaskID, string strTeacherID)
        {
            try
            {
                if (arrTaskID == null || arrTaskID.Length == 0)
                {
                    return false;
                }

                string[] arrParam = new string[2];
                arrParam[0] = JsonFormatter.JsonSerialize(arrTaskID);
                arrParam[1] = strTeacherID;
                mCommandApi.BaseUrl = mLessonPlanApiUrl;
                string strResult = mCommandApi.CallMethodPost_TC("Res_DeleteLessonPlanTask", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return false;
                }
                int iResult = UtilityClass.AnalyseTeachCenterJson<int>(strResult);
                if (iResult > 0)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("DeleteLessonPlanTask", e.Message);
            }
            return false;
        }

        public LBD_WebApiInterface.Models.TeachCenter.Get_LessonPlanM GetRecentPlan(string strTeacherID, string strCourseClassID, string strSubjectID, string strTerm)
        {
            try
            {
                string[] arrParam = new string[4];
                arrParam[0] = strTeacherID;
                arrParam[1] = strCourseClassID;
                arrParam[2] = strSubjectID;
                arrParam[3] = strTerm;

                mCommandApi.BaseUrl = mLessonPlanApiUrl;
                string strResult = mCommandApi.CallMethodGet("Res_GetRecentPlan", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return null;
                }

                LBD_TeachCenterModel.Res.LessonPlanM lessonPlan = UtilityClass.AnalyseTeachCenterJson<LBD_TeachCenterModel.Res.LessonPlanM>(strResult);
                if (lessonPlan == null)
                {
                    return null;
                }

                LBD_WebApiInterface.Models.TeachCenter.Get_LessonPlanM lessonPlan2 = new LBD_WebApiInterface.Models.TeachCenter.Get_LessonPlanM();
                lessonPlan2.ID = lessonPlan.ID;
                lessonPlan2.Name = lessonPlan.LessonPlanName;
                lessonPlan2.Status = lessonPlan.Status;
                lessonPlan2.SchoolType = lessonPlan.SchoolType;
                lessonPlan2.LevelCode = lessonPlan.LevelCode;
                lessonPlan2.CreateTime = lessonPlan.CreateTime;
                if (lessonPlan.LPTask != null && lessonPlan.LPTask.Count == 1)
                {
                    Get_LPTaskM task = new Get_LPTaskM();
                    task.ID = lessonPlan.LPTask[0].ID;
                    task.CourseClassID = lessonPlan.LPTask[0].TaskObject;
                    task.CourseClassName = lessonPlan.LPTask[0].TaskObjectName;
                    task.TeachTime = lessonPlan.LPTask[0].TeachTime;
                    task.ClassroomID = lessonPlan.LPTask[0].ClassroomID;
                    task.Classroom = lessonPlan.LPTask[0].Classroom;
                    lessonPlan2.LPTask.Add(task);
                }

                return lessonPlan2;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetRecentPlan", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 获取教案下是否有随堂测试，以此来判断是否开启“查看详情”
        /// </summary>
        public bool GetPopQuizStateByLessonPlanID(string strUserID, string strLessonPlanID)
        {
            try
            {
                if (string.IsNullOrEmpty(strUserID) || string.IsNullOrEmpty(strLessonPlanID))
                {
                    return false;
                }

                string[] arrParam = new string[2];
                arrParam[0] = strUserID;
                arrParam[1] = strLessonPlanID;
                mCommandApi.BaseUrl = mLessonPlanApiUrl;
                string strResult = mCommandApi.CallMethodGet("Res_GetPopQuizStateByLessonPlanID", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return false;
                }
                bool bResult = UtilityClass.AnalyseTeachCenterJson<bool>(strResult);
                return bResult;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetPopQuizStateByLessonPlanID", e.Message);
                return false;
            }
        }

        /// <summary>
        /// 更新课件的使用次数.
        /// ManulOrIntel=1代表人工制作，=2代表智能制作。strIPandPort为相关服务的地址
        /// </summary>
        public bool UpdateCoursewareUseTimes(string strCoursewareID, int ManulOrIntel, string strIPandPort)
        {
            bool bResult = false;
            try
            {
                if (string.IsNullOrEmpty(strIPandPort))
                {
                    return bResult;
                }

                //人工制作
                if (ManulOrIntel == 1)
                {
                    string[] arrIPandPort = strIPandPort.Split(':');

                    string[] arrParam = new string[1];
                    arrParam[0] = strCoursewareID;
                    mCommandApi.BaseUrl = string.Format(Properties.Resources.NetCoursewareUrl, arrIPandPort[0], arrIPandPort[1], "");
                    string strResult = mCommandApi.CallMethodPost("UpdateNetCoursewareUseTimes", arrParam);
                    if (string.IsNullOrEmpty(strResult))
                    {
                        bResult = false;
                    }
                    else
                    {
                        bResult = Convert.ToBoolean(strResult);
                    }
                }
                //智能制作
                else if (ManulOrIntel == 2)
                {
                }

                return bResult;
            }
            catch (Exception e)
            {
                WriteErrorMessage("UpdateCoursewareUseTimes", e.Message);
                return false;
            }
        }

        /// <summary>
        /// 更新课件共享状态，支持批量共享
        /// <para>ManualOrIntel=1代表网络化课件，=2代表智能化课件</para>
        /// </summary>
        //public bool UpdateCoursewareShareStatus(int ManualOrIntel, string[] arrCoursewareID, bool bShareStatus, string strTeacherID, string strIPandPort)
        //{
        //    bool bResult = false;
        //    try
        //    {
        //        if (string.IsNullOrEmpty(strIPandPort))
        //        {
        //            return bResult;
        //        }

        //        //人工制作
        //        if (ManualOrIntel == 1)
        //        {
        //            string[] arrIPandPort = strIPandPort.Split(':');

        //            string[] arrParam = new string[3];
        //            arrParam[0] = JsonFormatter.JsonSerialize(arrCoursewareID);
        //            arrParam[1] = bShareStatus.ToString();
        //            arrParam[2] = strTeacherID;
        //            mCommandApi.BaseUrl = string.Format(Properties.Resources.NetCoursewareUrl, arrIPandPort[0], arrIPandPort[1]);
        //            string strResult = mCommandApi.CallMethodGet("UpdateNetCoursewareShareStatus", arrParam);
        //            if (string.IsNullOrEmpty(strResult))
        //            {
        //                bResult = false;
        //            }
        //            else
        //            {
        //                bResult = Convert.ToBoolean(strResult);
        //            }
        //        }
        //        //智能制作
        //        else if (ManualOrIntel == 2)
        //        {
        //            bResult = mIntelCoursewareI.SetZhiNengBeiKeUrl(strIPandPort);
        //            if (bResult == false)
        //            {
        //                return bResult;
        //            }

        //            bResult = mIntelCoursewareI.ShareCourseware(arrCoursewareID, bShareStatus, strTeacherID);
        //        }

        //        return bResult;
        //    }
        //    catch (Exception e)
        //    {
        //        WriteErrorMessage("UpdateCoursewareShareStatus", e.Message);
        //        return false;
        //    }
        //}

        /// <summary>
        /// 批量删除课件
        /// </summary>
        //public bool DeleteCourseware(int ManualOrIntel, string[] arrCoursewareID, string strTeacherID, string strIPandPort)
        //{
        //    bool bResult = false;
        //    try
        //    {
        //        if (string.IsNullOrEmpty(strIPandPort))
        //        {
        //            return bResult;
        //        }

        //        //人工制作
        //        if (ManualOrIntel == 1)
        //        {
        //            string[] arrIPandPort = strIPandPort.Split(':');

        //            string[] arrParam = new string[2];
        //            arrParam[0] = JsonFormatter.JsonSerialize(arrCoursewareID);
        //            arrParam[1] = strTeacherID;
        //            mCommandApi.BaseUrl = string.Format(Properties.Resources.NetCoursewareUrl, arrIPandPort[0], arrIPandPort[1]);
        //            string strResult = mCommandApi.CallMethodPost("DelNetCoursewareMult", arrParam);
        //            if (string.IsNullOrEmpty(strResult))
        //            {
        //                bResult = false;
        //            }
        //            else
        //            {
        //                bResult = Convert.ToBoolean(strResult);
        //            }
        //        }
        //        //智能制作
        //        else if (ManualOrIntel == 2)
        //        {
        //            bResult = mIntelCoursewareI.SetZhiNengBeiKeUrl(strIPandPort);
        //            if (bResult == false)
        //            {
        //                return bResult;
        //            }

        //            bResult = mIntelCoursewareI.DeleteCourseware(arrCoursewareID, strTeacherID);
        //        }

        //        return bResult;
        //    }
        //    catch (Exception e)
        //    {
        //        WriteErrorMessage("DeleteCourseware", e.Message);
        //        return false;
        //    }
        //}

        #endregion 课堂教案相关

        #region 课后练习相关

        public LBD_WebApiInterface.Models.TeachCenter.PracticeM[] GetPractice(string strTeacherID, string strCloudSubjectID, string strTerm, string strCourseClassID)
        {
            try
            {
                string[] arrParam = new string[11];
                arrParam[0] = strTeacherID;
                arrParam[1] = strCloudSubjectID;
                arrParam[2] = strTerm;
                arrParam[3] = null;
                arrParam[4] = null;
                arrParam[5] = null;
                arrParam[6] = null;
                arrParam[7] = "0";
                arrParam[8] = "0";
                arrParam[9] = "3";
                arrParam[10] = "false";
                mCommandApi.BaseUrl = mPracticeApiUrl;
                string strResult = mCommandApi.CallMethodGet("Res_GetPracticeComplex", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return null;
                }
                LBD_TeachCenterModel.Res.PracticeWithCountM praWithCountM = UtilityClass.AnalyseTeachCenterJson<LBD_TeachCenterModel.Res.PracticeWithCountM>(strResult);
                LBD_TeachCenterModel.Res.PracticeM[] arrPractice = null;
                if (praWithCountM != null && praWithCountM.Practice != null)
                {
                    arrPractice = praWithCountM.Practice;
                }
                if (arrPractice == null || arrPractice.Length == 0)
                {
                    return null;
                }

                //包含指定课程班的
                List<LBD_WebApiInterface.Models.TeachCenter.PracticeM> list1 = new List<LBD_WebApiInterface.Models.TeachCenter.PracticeM>();
                //不包含指定课程班的
                List<LBD_WebApiInterface.Models.TeachCenter.PracticeM> list2 = new List<LBD_WebApiInterface.Models.TeachCenter.PracticeM>();
                for (int i = 0; i < arrPractice.Length; i++)
                {
                    LBD_WebApiInterface.Models.TeachCenter.PracticeM practice = new LBD_WebApiInterface.Models.TeachCenter.PracticeM();
                    practice.ID = arrPractice[i].ID;
                    practice.Name = arrPractice[i].PracticeName;
                    practice.Status = arrPractice[i].Status;
                    practice.SchoolType = arrPractice[i].SchoolType;
                    practice.LevelCode = arrPractice[i].LevelCode;
                    practice.CreateTime = arrPractice[i].CreateTime;
                    if (arrPractice[i].PraCourseware != null)
                    {
                        for (int j = 0; j < arrPractice[i].PraCourseware.Count; j++)
                        {
                            switch (arrPractice[i].PraCourseware[j].CoursewareType)
                            {
                                case 1:
                                case 2:
                                case 3:
                                case 4:
                                    practice.KeHouCeShiID = arrPractice[i].PraCourseware[j].CoursewareID;
                                    practice.KeHouCeShiName = arrPractice[i].PraCourseware[j].CoursewareName;
                                    practice.KeHouCeShiType = arrPractice[i].PraCourseware[j].CoursewareType;
                                    break;

                                case 5:
                                case 6:
                                case 7:
                                case 8:
                                case 9:
                                case 10:
                                case 11:
                                case 12:
                                case 13:
                                case 14:
                                    practice.KeHouZuoYeID = arrPractice[i].PraCourseware[j].CoursewareID;
                                    practice.KeHouZuoYeName = arrPractice[i].PraCourseware[j].CoursewareName;
                                    practice.KeHouZuoYeType = arrPractice[i].PraCourseware[j].CoursewareType;
                                    break;
                            }
                        }
                    }
                    if (arrPractice[i].PraTask != null)
                    {
                        practice.CourseClassID = arrPractice[i].PraTask.TaskObject;
                        practice.CourseClassName = arrPractice[i].PraTask.TaskObjectName;
                        practice.StartTime = arrPractice[i].PraTask.StartTime;
                        practice.EndTime = arrPractice[i].PraTask.EndTime;
                        if (arrPractice[i].PraTask.SumCount > 0)
                        {
                            practice.Progress = (arrPractice[i].PraTask.CurCount / arrPractice[i].PraTask.SumCount);
                        }
                        else
                        {
                            practice.Progress = 0;
                        }

                        if (arrPractice[i].PraTask.TaskObject == null)
                        {
                            list2.Add(practice);
                        }
                        else
                        {
                            if (arrPractice[i].PraTask.TaskObject.Contains<string>(strCourseClassID))
                            {
                                list1.Add(practice);
                            }
                            else
                            {
                                list2.Add(practice);
                            }
                        }
                    }
                    else
                    {
                        list2.Add(practice);
                    }
                }

                list1.AddRange(list2);
                if (list1.Count > 0)
                {
                    return list1.ToArray();
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetPractice", e.Message);
            }
            return null;
        }

        public LBD_WebApiInterface.Models.TeachCenter.PracticeM GetPracticeByID(string strPracticeID)
        {
            try
            {
                string[] arrParam = new string[1];
                arrParam[0] = strPracticeID;
                mCommandApi.BaseUrl = mPracticeApiUrl;
                string strResult = mCommandApi.CallMethodGet("Res_GetPracticeByID", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return null;
                }
                LBD_TeachCenterModel.Res.PracticeM practice = UtilityClass.AnalyseTeachCenterJson<LBD_TeachCenterModel.Res.PracticeM>(strResult);
                if (practice == null)
                {
                    return null;
                }

                LBD_WebApiInterface.Models.TeachCenter.PracticeM practice2 = new LBD_WebApiInterface.Models.TeachCenter.PracticeM();
                practice2.ID = practice.ID;
                practice2.Name = practice.PracticeName;
                practice2.Status = practice.Status;
                practice2.SchoolType = practice.SchoolType;
                practice2.LevelCode = practice.LevelCode;
                practice2.CreateTime = practice.CreateTime;
                if (practice.PraCourseware != null)
                {
                    for (int j = 0; j < practice.PraCourseware.Count; j++)
                    {
                        switch (practice.PraCourseware[j].CoursewareType)
                        {
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                                practice2.KeHouCeShiID = practice.PraCourseware[j].CoursewareID;
                                practice2.KeHouCeShiName = practice.PraCourseware[j].CoursewareName;
                                practice2.KeHouCeShiType = practice.PraCourseware[j].CoursewareType;
                                break;

                            case 5:
                            case 6:
                            case 7:
                            case 8:
                            case 9:
                            case 10:
                            case 11:
                            case 12:
                            case 13:
                            case 14:
                                practice2.KeHouZuoYeID = practice.PraCourseware[j].CoursewareID;
                                practice2.KeHouZuoYeName = practice.PraCourseware[j].CoursewareName;
                                practice2.KeHouZuoYeType = practice.PraCourseware[j].CoursewareType;
                                break;
                        }
                    }
                }
                if (practice.PraTask != null)
                {
                    practice2.CourseClassID = practice.PraTask.TaskObject;
                    practice2.CourseClassName = practice.PraTask.TaskObjectName;
                    practice2.StartTime = practice.PraTask.StartTime;
                    practice2.EndTime = practice.PraTask.EndTime;
                }
                if (practice.PraTask.SumCount > 0)
                {
                    practice2.Progress = (practice.PraTask.CurCount / practice.PraTask.SumCount);
                }
                else
                {
                    practice2.Progress = 0;
                }

                return practice2;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetPracticeID", e.Message);
                return null;
            }
        }

        #endregion 课后练习相关

        private void WriteErrorMessage(string strMethodName, string strErrorMessage)
        {
            try
            {
                DirectoryInfo clsPath = new DirectoryInfo(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory));
                DirectoryInfo clsSubPath = clsPath.CreateSubdirectory("Errlog\\LBD_WebApiInterface\\Api");

                if (clsSubPath.Exists)
                {
                    DateTime clsDate = DateTime.Now;
                    string strPath = clsSubPath.FullName + "\\TeachCenterI(" + String.Format("{0:yyyy-MM-dd}", clsDate) + ").log";
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