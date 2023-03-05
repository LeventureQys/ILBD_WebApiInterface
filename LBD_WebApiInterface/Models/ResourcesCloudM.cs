using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LBD_WebApiInterface.Models
{
    /// <summary>
    /// 云备课系统的WebApi返回值的统一规范
    /// </summary>
    public class CLP1_ApiResultM<T>
    {
        /// <summary>
        /// 错误码
        /// <para>0-正常</para>
        /// <para>1-操作失败</para>
        /// </summary>
        public int StatusCode { get; set; }
        public int ErrCode { get; set; }
        /// <summary>
        /// 异常消息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 实际数据
        /// </summary>
        public T Data { get; set; }
       
    }
    public class ResourcesCloudM
    {
    }
    //获取数字化资源库子库的资料详情
    public class LgdigitalResM
    {
        public string TotalNum { get; set; }//总页码数
        public string PageSize { get; set; }//
        public string PageIndex { get; set; }//

        public List<ListRes> listRes;

        public class ListRes
        {
            public string ResCode { get; set; }
            public string RowNumber { get; set; }
            public string ResName { get; set; }
            public string ResType { get; set; }
            public string StoreDate { get; set; }
            public string ThemeCode { get; set; }
            public string ThemeText { get; set; }
            public string ImporKnCode { get; set; }
            public string ImporKnText { get; set; }
            public string MainKnCode { get; set; }
            public string MainKnText { get; set; }
            public string UnitNum { get; set; }
            public string ResSize { get; set; }
            public string ResClass { get; set; }
            public string ResLevel { get; set; }
            public string MD5Code { get; set; }
            public string LibCode { get; set; }
            public string InstitutionalUnit { get; set; }
            public string ResFtpPath { get; set; }
            public string IsExsitMedia { get; set; }
            public string TextFileContent { get; set; }
            public string IsDownload { get; set; }
            public string DurationLength { get; set; }
            public string ResLength { get; set; }
            public string ApplyNum { get; set; }
            public string Creator { get; set; }
            public string CreatorId { get; set; }
            public string ThemeKeywordCode { get; set; }
            public string ThemeKeywordText { get; set; }
            public string UpperKnlgCode { get; set; }
            public string UpperKnlgText { get; set; }
            public string OtherKnlgCode { get; set; }
            public string OtherKnlgText { get; set; }
            public string HeatRate { get; set; }
            public string MaterialType { get; set; }

        }
    }
    //获取数字化资源库子库的资料详情
    public class VersionM
    {
        public string VersionCode { get; set; }//版本


        public List<Topic> topic;

        public class Topic
        {
            public string Flag { get; set; }
            public string Code { get; set; }
            public string Video { get; set; }
            public string Sound { get; set; }
            public string Image { get; set; }
            public string Name { get; set; }
            public string TopicChinese { get; set; }

            public List<Themes> theme;
            public class Themes
            {

                public string Flag { get; set; }
                public string Code { get; set; }
                public string Video { get; set; }
                public string Sound { get; set; }
                public string Image { get; set; }

                public string ThemeChinese { get; set; }
                public string Theme { get; set; }
            }


        }
    }
    //学科网信息，对接基础平台，陈云龙
    public class WebsiteCustomsizeM
    {
        public string Url { get; set; }
        public bool IsGroup { get; set; }
        public string WebsiteName { get; set; }
        public int OrderNo { get; set; }
        public string GroupName { get; set; }
      
        public List<Website> List;
        public class Website
        {
            public string Url { get; set; }
            public string WebsiteName { get; set; }
            public int OrderNo { get; set; }
        }

    }
}
