using System;
using LBD_WebApiInterface.Models;
using LBD_WebApiInterface.Models.CloudPreparation;
using LBD_WebApiInterface.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Net;

namespace LBD_WebApiInterface.Api
{
    /// <summary>
    /// 资源云最新对接接口
    /// 接口维护人：李萌芽
    /// 维护日期：2019-10-16
    /// 维护记录如下：
    /// 1.
    /// 2.
    /// </summary>
    public class ResourcesCloudl
    {
        #region 私有变量       
        private string mResourcesApiUrl;
        private string mAllSubjectKlgApiUrl;
        private string mBaseApiUrl;
        #endregion

        #region 初始化
        /// <summary>
        /// 初始化A00
        /// </summary>
        /// <param name="ResourcesIP">数字资源库WebApi的IP</param>
        /// <param name="ResourcesPort">数字资源库WebApi的Port</param>
        /// <returns>true-初始化成功，false-初始化失败</returns>
        #endregion
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
        /// 初始化A00
        /// </summary>
        /// <param name="AllSubjectKlgIP">数字资源库WS的IP</param>
        /// <param name="AllSubjectKlgPort">数字资源库WS的Port</param>
        /// <returns>true-初始化成功，false-初始化失败</returns>
        #endregion
        public bool InitializeAllSubjectKlg(string AllSubjectKlgIP, string AllSubjectKlgPort)
        {
            try
            {
                WriteTrackLog("Initialize【Start】", "AllSubjectKlgIP=" + AllSubjectKlgIP + ",AllSubjectKlgPort=" + AllSubjectKlgPort);
                if (string.IsNullOrEmpty(AllSubjectKlgIP) || string.IsNullOrEmpty(AllSubjectKlgPort))
                {
                    return false;
                }

                mAllSubjectKlgApiUrl = "http://" + AllSubjectKlgIP + ":" + AllSubjectKlgPort;
                WriteTrackLog("Initialize", "mAllSubjectKlgApiUrl=" + mAllSubjectKlgApiUrl);
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
        /// <param name="BaseIP">基础平台WS的IP</param>
        /// <param name="BasePort">基础平台WS的Port</param>
        /// <returns>true-初始化成功，false-初始化失败</returns>
        #endregion
        public bool InitializeBase(string BaseIP, string BasePort)
        {
            try
            {
                WriteTrackLog("Initialize【Start】", "BaseIP=" + BaseIP + ",BasePort=" + BasePort);
                if (string.IsNullOrEmpty(BaseIP) || string.IsNullOrEmpty(BasePort))
                {
                    return false;
                }

                mBaseApiUrl = "http://" + BaseIP + ":" + BasePort;
                WriteTrackLog("Initialize", "mBaseApiUrl=" + mBaseApiUrl);
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
        //
        #region 获取新版数字化资源库字库信息详情
        /// <summary>
        ///  获取新版数字化资源库字库信息
        /// <param name="OptionalParams">参数的格式:学科}学习阶段}水平级别}子库}资源格式}题型（以"}"隔开）  C}F}}I}1|2}</param>
        /// <param name="PageIndex">当前页：取值为大于等于1的整数，默认值1</param>
        /// <param name="PageSize ">每页返回的数量：取值为大于0的整数，默认值10</param>
        /// <param name="ItemText "></param>
        /// </summary>
        /// <returns></returns>
        /// 
        public LgdigitalResM GetBLibraryMCatalogSearch(string OptionalParams, string ItemText, string PageIndex, string PageSize)
        {
            try
            {

                string webapiAddr = null;

                webapiAddr = mResourcesApiUrl + "/SearchResource.asmx/WS_Search_CatalogSearch?OptionalParams=" + OptionalParams + "&ItemText=" + ItemText + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;


                string result = CallApiHelper.CallMethod_Get(webapiAddr);
                if (string.IsNullOrEmpty(result))
                    return null;
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(result);
                XmlNodeList nodeList = xd.GetElementsByTagName("LgdigitalRes");
                if (nodeList == null || nodeList.Count <= 0)
                    return null;
                LgdigitalResM lgdigitalRes = new LgdigitalResM();
                List<LgdigitalResM.ListRes> listRes = new List<LgdigitalResM.ListRes>();
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
                        LgdigitalResM.ListRes listRes001 = new LgdigitalResM.ListRes();
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
                        listRes001.OtherKnlgText = nodeList[0].ChildNodes[i].ChildNodes[31].InnerText;
                        listRes001.HeatRate = nodeList[0].ChildNodes[i].ChildNodes[32].InnerText;
                        listRes001.MaterialType = nodeList[0].ChildNodes[i].ChildNodes[33].InnerText;

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
        #endregion
        #region 获取新版数字化资源库主题名称搜索
        /// <summary>
        ///  获取新版数字化资源库主题名称搜索
        /// <param name="OptionalParams">参数的格式:学科}学习阶段}水平级别}子库}资源格式}题型（以"}"隔开）  C}F}}I}1|2}</param>
        /// <param name="PageIndex">当前页：取值为大于等于1的整数，默认值1</param>
        /// <param name="PageSize ">每页返回的数量：取值为大于0的整数，默认值10</param>
        /// <param name="FilterContext ">要查询的主题</param>
        /// </summary>
        /// <returns></returns>
        /// 
        public LgdigitalResM GetBLibraryMSearchNameSearch(string FilterContext, string OptionalParams, string PageIndex, string PageSize)
        {
            try
            {

                string webapiAddr = null;

                webapiAddr = mResourcesApiUrl + "/SearchResource.asmx/WS_Search_NameSearch?OptionalParams=" + OptionalParams + "&FilterContext=" + FilterContext + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;

                string result = CallApiHelper.CallMethod_Get(webapiAddr);
                if (string.IsNullOrEmpty(result))
                    return null;
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(result);
                XmlNodeList nodeList = xd.GetElementsByTagName("LgdigitalRes");
                if (nodeList == null || nodeList.Count <= 0)
                    return null;
                LgdigitalResM lgdigitalRes = new LgdigitalResM();
                List<LgdigitalResM.ListRes> listRes = new List<LgdigitalResM.ListRes>();
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
                        LgdigitalResM.ListRes listRes001 = new LgdigitalResM.ListRes();
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
                        listRes001.OtherKnlgText = nodeList[0].ChildNodes[i].ChildNodes[31].InnerText;
                        listRes001.HeatRate = nodeList[0].ChildNodes[i].ChildNodes[32].InnerText;
                        listRes001.MaterialType = nodeList[0].ChildNodes[i].ChildNodes[33].InnerText;

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
        #endregion
        #region 获取新版数字化资源库主题目录搜索
        /// <summary>
        ///  获取新版数字化资源库主题目录搜索
        /// <param name="OptionalParams">参数的格式:学科}学习阶段}水平级别}子库}资源格式}题型（以"}"隔开）  C}F}}I}1|2}</param>
        /// <param name="PageIndex">当前页：取值为大于等于1的整数，默认值1</param>
        /// <param name="PageSize ">每页返回的数量：取值为大于0的整数，默认值10</param>
        /// <param name="ItemText ">要查询的目录</param>
        /// </summary>
        /// <returns></returns>
        /// 
        public LgdigitalResM GetBLibraryMSearchCatalogSearch(string OptionalParams, string ItemText, string PageIndex, string PageSize)
        {
            try
            {

                string webapiAddr = null;

                webapiAddr = mResourcesApiUrl + "/SearchResource.asmx/WS_Search_CatalogSearch?OptionalParams=" + OptionalParams + "&ItemText=" + ItemText + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;

                string result = CallApiHelper.CallMethod_Get(webapiAddr);
                if (string.IsNullOrEmpty(result))
                    return null;
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(result);
                XmlNodeList nodeList = xd.GetElementsByTagName("LgdigitalRes");
                if (nodeList == null || nodeList.Count <= 0)
                    return null;
                LgdigitalResM lgdigitalRes = new LgdigitalResM();
                List<LgdigitalResM.ListRes> listRes = new List<LgdigitalResM.ListRes>();
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
                        LgdigitalResM.ListRes listRes001 = new LgdigitalResM.ListRes();
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
                        listRes001.OtherKnlgText = nodeList[0].ChildNodes[i].ChildNodes[31].InnerText;
                        listRes001.HeatRate = nodeList[0].ChildNodes[i].ChildNodes[32].InnerText;
                        listRes001.MaterialType = nodeList[0].ChildNodes[i].ChildNodes[33].InnerText;

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
        #endregion
        #region 获取新版数字化资源库主题内容关键词搜索
        /// <summary>
        ///  获取新版数字化资源库主题内容关键词搜索
        /// <param name="OptionalParams">参数的格式:学科}学习阶段}水平级别}子库}资源格式}题型（以"}"隔开）  C}F}}I}1|2}</param>
        /// <param name="PageIndex">当前页：取值为大于等于1的整数，默认值1</param>
        /// <param name="PageSize ">每页返回的数量：取值为大于0的整数，默认值10</param>
        /// <param name="FilterContext ">要查询的关键字</param>
        /// </summary>
        /// <returns></returns>
        /// 
        public LgdigitalResM GetBLibraryMSearchKeywordSearch(string OptionalParams, string FilterContext, string PageIndex, string PageSize)
        {
            try
            {

                string webapiAddr = null;

                webapiAddr = mResourcesApiUrl + "/SearchResource.asmx/WS_Search_KeywordSearch?OptionalParams=" + OptionalParams + "&FilterContext=" + FilterContext + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;

                string result = CallApiHelper.CallMethod_Get(webapiAddr);
                if (string.IsNullOrEmpty(result))
                    return null;
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(result);
                XmlNodeList nodeList = xd.GetElementsByTagName("LgdigitalRes");
                if (nodeList == null || nodeList.Count <= 0)
                    return null;
                LgdigitalResM lgdigitalRes = new LgdigitalResM();
                List<LgdigitalResM.ListRes> listRes = new List<LgdigitalResM.ListRes>();
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
                        LgdigitalResM.ListRes listRes001 = new LgdigitalResM.ListRes();
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
                        listRes001.OtherKnlgText = nodeList[0].ChildNodes[i].ChildNodes[31].InnerText;
                        listRes001.HeatRate = nodeList[0].ChildNodes[i].ChildNodes[32].InnerText;
                        listRes001.MaterialType = nodeList[0].ChildNodes[i].ChildNodes[33].InnerText;

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
        #endregion
        #region 获取新版数字化资源库主题内容精确搜索(根据知识点内容获取资源)
        /// <summary>
        ///  获取新版数字化资源库主题内容精确搜索(根据知识点内容获取资源)
        /// <param name="OptionalParams">参数的格式:学科}学习阶段}水平级别}子库}资源格式}题型（以"}"隔开）  C}F}}I}1|2}</param>
        /// <param name="PageIndex">当前页：取值为大于等于1的整数，默认值1</param>
        /// <param name="PageSize ">每页返回的数量：取值为大于0的整数，默认值10</param>
        /// <param name="FilterContext ">要查询的关键字</param>
        /// </summary>
        /// <returns></returns>
        /// 
        public LgdigitalResM GetBLibraryMSearchAccurateSearch(string OptionalParams, string FilterContext, string PageIndex, string PageSize)
        {
            try
            {

                string webapiAddr = null;

                webapiAddr = mResourcesApiUrl + "/SearchResource.asmx/WS_Search_AccurateSearch?OptionalParams=" + OptionalParams + "&FilterContext=" + FilterContext + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;

                string result = CallApiHelper.CallMethod_Get(webapiAddr);
                if (string.IsNullOrEmpty(result))
                    return null;
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(result);
                XmlNodeList nodeList = xd.GetElementsByTagName("LgdigitalRes");
                if (nodeList == null || nodeList.Count <= 0)
                    return null;
                LgdigitalResM lgdigitalRes = new LgdigitalResM();
                List<LgdigitalResM.ListRes> listRes = new List<LgdigitalResM.ListRes>();
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
                        LgdigitalResM.ListRes listRes001 = new LgdigitalResM.ListRes();
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
                        listRes001.OtherKnlgText = nodeList[0].ChildNodes[i].ChildNodes[31].InnerText;
                        listRes001.HeatRate = nodeList[0].ChildNodes[i].ChildNodes[32].InnerText;
                        listRes001.MaterialType = nodeList[0].ChildNodes[i].ChildNodes[33].InnerText;

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
        #endregion
        #region 获取新版数字化资源库主题内容精确搜索(根据知识点内容获取资源)
        /// <summary>
        ///  获取新版数字化资源库主题内容精确搜索(根据知识点内容获取资源)
        /// <param name="SubjectCode">参数的格式:学科}学习阶段}水平级别}子库}资源格式}题型（以"}"隔开）  C}F}}I}1|2}</param>
        /// <param name="VersionCode">当前页：取值为大于等于1的整数，默认值1</param>
        /// </summary>
        /// <returns></returns>
        /// 
        public VersionM GetTpThBySubjectVersion(string SubjectCode, string VersionCode)
        {
            try
            {

                string webapiAddr = null;

                webapiAddr = mAllSubjectKlgApiUrl + "/Content/GetKnowledgeContent.asmx/WS_Klg_Content_GetTpThBySubjectVersion?SubjectCode=" + SubjectCode + "&VersionCode=" + VersionCode;

                string result = CallApiHelper.CallMethod_Get(webapiAddr);
                if (string.IsNullOrEmpty(result))
                    return null;
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(result);
                XmlNodeList nodeList = xd.GetElementsByTagName("Version");
                if (nodeList == null || nodeList.Count <= 0)
                    return null;
                VersionM lgdigitalRes = new VersionM();
                List<VersionM.Topic> listRes = new List<VersionM.Topic>();

                //List<BLibraryM> list = new List<BLibraryM>();
                lgdigitalRes.VersionCode = nodeList[0].Attributes[0].InnerText;

                int count = nodeList[0].ChildNodes.Count - 1;
                if (count >= 1)
                {
                    for (int i = 0; i <= count; i++)
                    {
                        VersionM.Topic listRes001 = new VersionM.Topic();
                        listRes001.Flag = nodeList[0].ChildNodes[i].Attributes[0].ChildNodes[0].InnerText;
                        listRes001.Code = nodeList[0].ChildNodes[i].Attributes[1].ChildNodes[0].InnerText;
                        listRes001.Video = nodeList[0].ChildNodes[i].Attributes[2].ChildNodes[0].InnerText;
                        listRes001.Sound = nodeList[0].ChildNodes[i].Attributes[3].ChildNodes[0].InnerText;
                        listRes001.Image = nodeList[0].ChildNodes[i].Attributes[4].ChildNodes[0].InnerText;
                        listRes001.Name = nodeList[0].ChildNodes[i].Attributes[5].ChildNodes[0].InnerText;
                        listRes001.TopicChinese = nodeList[0].ChildNodes[i].Attributes[6].ChildNodes[0].InnerText;
                        List<VersionM.Topic.Themes> listRes1 = new List<VersionM.Topic.Themes>();
                        int count1 = nodeList[0].ChildNodes[i].ChildNodes.Count - 1;
                        if (count1 >= 0)
                        {
                            for (int j = 0; j <= count1; j++)
                            {
                                VersionM.Topic.Themes listRes002 = new VersionM.Topic.Themes();
                                listRes002.Flag = nodeList[0].ChildNodes[i].ChildNodes[j].Attributes[0].ChildNodes[0].InnerText;
                                listRes002.Code = nodeList[0].ChildNodes[i].ChildNodes[j].Attributes[1].ChildNodes[0].InnerText;
                                listRes002.Video = nodeList[0].ChildNodes[i].ChildNodes[j].Attributes[2].ChildNodes[0].InnerText;
                                listRes002.Sound = nodeList[0].ChildNodes[i].ChildNodes[j].Attributes[3].ChildNodes[0].InnerText;
                                listRes002.Image = nodeList[0].ChildNodes[i].ChildNodes[j].Attributes[4].ChildNodes[0].InnerText;
                                listRes002.ThemeChinese = nodeList[0].ChildNodes[i].ChildNodes[j].Attributes[5].ChildNodes[0].InnerText;
                                listRes002.Theme = nodeList[0].ChildNodes[i].ChildNodes[j].ChildNodes[0].InnerText;
                                listRes1.Add(listRes002);
                            }
                        }
                        listRes001.theme = listRes1;
                        listRes.Add(listRes001);
                    }
                    lgdigitalRes.topic = listRes;
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
        #endregion
        #region 查找学科网信息
        /// <summary>
        /// 查找学科网信息
        /// </summary>
        /// <param name="UserId">教师ID</param>
        /// <param name="SubjectId">学校ID</param>
        /// <param name="Token">学科ID</param>       
        /// <returns>教学方案列表</returns>
        public WebsiteCustomsizeM[] GetWebsiteCustomsize(string UserId, string SubjectId, string Token)
        {
            try
            {



                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append(mBaseApiUrl + "/Index/Api/Teacher/GetWebsiteCustomsize");
                sbUrl.Append("?UserId=" + UserId);
                sbUrl.Append("&SubjectId=" + SubjectId);

                string strResult = CallMethod_Get(sbUrl.ToString(), Token);
                WriteTrackLog("GetWebsiteCustomsize，不分页", "strResult:" + strResult);
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteErrorMessage("GetWebsiteCustomsize，不分页", "返回值为空。查询条件为：" + sbUrl.ToString());

                    return null;
                }
                CLP1_ApiResultM<WebsiteCustomsizeM[]> result = JsonFormatter.JsonDeserialize<CLP1_ApiResultM<WebsiteCustomsizeM[]>>(strResult);
                if (result.ErrCode != 0)
                {
                    WriteErrorMessage("GetTeachingProgram", "查询失败，ErrorFlag=" + result.ErrCode + "，Message=" + result.Msg);

                    return null;
                }

                if (result.Data != null)
                {
                    if (result.Data == null)
                    {
                        WriteErrorMessage("GetTeachingProgram，不分页", "result.Data.TeachProgram为空！");
                        return null;
                    }
                    else
                    {

                        return result.Data;
                    }
                }
                WriteErrorMessage("GetTeachingProgram，不分页", "异常！！！");
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetTeachingProgram", e.Message);
            }
            return null;
        }

        #endregion
        //
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
                    string strPath = clsSubPath.FullName + "\\TeachCenter2I(" + String.Format("{0:yyyy-MM-dd}", clsDate) + ").log";
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
                    string strPath = clsSubPath.FullName + "\\TeachCenter2I(" + String.Format("{0:yyyy-MM-dd}", clsDate) + ").log";
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


        #endregion
        /// <summary>
        /// 以Get方式调用WebApi，出错时会抛出异常（例如URL不规范、超时等）
        /// </summary>
        /// <param name="strWholeUrl">访问WebApi的完整路径</param>
        ///  /// <param name="Token">token</param>
        /// <returns>WebApi的返回值，未做处理的的字符串格式</returns>
        public static string CallMethod_Get(string strWholeUrl,string Token)
        {


            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(strWholeUrl);


                request.Headers.Add("Authorization:X-Token="+ Token);
                request.Method = "GET";
                //request.Proxy = null;
                request.Timeout = 15000;
                request.ContentType = "application/xml";

                response = (HttpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                string strResult = sr.ReadToEnd();
                sr.Close();
                responseStream.Close();

                return strResult;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (request != null)
                {
                    request.Abort();
                    request = null;
                }
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
            }
        }
    }




}
