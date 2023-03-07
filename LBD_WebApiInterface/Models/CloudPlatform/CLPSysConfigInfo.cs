namespace LBD_WebApiInterface.Models.CloudPlatform
{
    public class CLPSysConfigInfo
    {
        /// <summary>
        /// 云平台产品使用范围。（1：单个专业英语院校使用，2：
        //单个普通大学使用，3：单个中小学校使用，4：多学校（县/
        //区）范围）使用，5：中职学校使用，6：高职学校使用。）
        /// </summary>
        public string useRange { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string productName { get; set; }
    }
}