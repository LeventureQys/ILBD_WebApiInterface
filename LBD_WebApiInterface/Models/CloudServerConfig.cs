using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LBD_WebApiInterface.Models
{
   public  class CloudServerConfig
    {
        public List<CloudServerConfigM> cloudServerConfigs;
    }
    //备课服务器配置信息类
    public class CloudServerConfigM
    {
        public string ServerCode { get; set; }
        public string ServerDescription { get; set; }
        public string ServerIP { get; set; }
        public string ServerPort { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public string VirtualPath { get; set; }
        public string PhyPath { get; set; }

    }
    //新版数字化资源子库类
    public class LgBasicLibrary
    {
        public List<BLibraryM> LgBasicLibrarys;
    }
    public class BLibraryM
    {
        public string LibrarySequence { get; set; }
        public string LibraryCode { get; set; }
        public string LibraryName { get; set; }
        public string CourseCode { get; set; }
    }
    //获取数字化资源库子库的资料详情
    public class LgdigitalRes
    {
        public  string TotalNum { get; set; }//总页码数
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

        }
    }
    //获取服务器地址信息
    public class ServerConf
    {
        public List<Server> server;

       
    }
    public class Server
    {
        public string sModID { get; set; }
        public string sServerType { get; set; }
        public string sServerName { get; set; }
        public string sIP { get; set; }
        public string sPort { get; set; }
        public string sUserName { get; set; }
        public string sPWD { get; set; }
    }
    //随堂测试paper获取信息
    public class PaperResultDataGetParams
    {
        public string BigItemQuantity { get; set; }
        public string FullScore { get; set; }
        public string IsHaveAudio { get; set; }
        public string ItemQuantity { get; set; }
        public string PaperID { get; set; }
        public string PaperName { get; set; }
        public string PaperSource { get; set; }
        public string TestTimeLong { get; set; }
        public string UpTime { get; set; }
        public string UpTimeStr { get; set; }
        public string UseCount { get; set; }
    }
    //
    public class SubjectNetworkM
    {
        public string ID { get; set; }// 主键
        public string TeacherID { get; set; }
        public string SchoolID { get; set; }// 学校id
        public string Term { get; set; }// 学期
        public string SubjectID { get; set; }// 学科id
        public string Icon { get; set; }//学科网图标
        public string ResAddress { get; set; }//地址（网站地址，ftp地址，其他地址）
        public string AddressName { get; set; }//地址名称
        public string AddressUsername { get; set; }//地址的用户名
        public string AddressPassword { get; set; }//地址密码
    }
    public class SubjectNetwork
    {
        List<SubjectNetworkM> SubjectNetworkM { get; set; }
    }
    //课时信息
    public class CourseTimeM
    {
        public string ID { get; set; }// 主键
        public string CourseTime { get; set; }
        public string CourseName { get; set; }
        public string TeachProgramID { get; set; }


    }
    //知识点信息
    public class KnowledgeM
    {
        public string ID { get; set; }// 主键
        public string KnowledgeCode { get; set; }
        public string KnowledgeContent { get; set; }
        public string BelongCoursewareID { get; set; }
        public string TeachProgramID { get; set; }
    }

    public class MaterialLibraryM
    {
        public string ID { get; set; }// 主键
        public string ClassPeriodID { get; set; }
        public string TeachingMaterialID { get; set; }
        public string TeachProgramID { get; set; }
    }
}
