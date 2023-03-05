using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using LBD_WebApiInterface.Utility;
using System.Xml;
using LBD_WebApiInterface.ClassTeach;
using LgSoft.LgMgrCenterDOTDLL;
using LBD_WebApiInterface.Models.CloudPlatform;
using AccessWebApi_Model.SchoolLesson;
using LBD_WebApiInterface.Models;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using lancoo.cp.basic.sysbaseclass;
using System.Diagnostics;


namespace LBD_WebApiInterface.Api
{
    public class TeachInfoI
    {

        #region 改写部分
        /// <summary>
        /// 教学模块
        /// </summary>
        public enum E_Teach_Module
        {
            课堂讲解 = 1,
            课堂辅导 = 2,
            随堂综合测试 = 3,
            口语测试 = 4,
            作业 = 5,
            管理平台 = 6,
            多媒体教学=7
        }

        //现在多学科，教学模式有变更。但其它学科的又不能直接追加在后面，因为有相同名字的模式
        /// <summary>
        /// 教学模式
        /// </summary>
        public enum E_TeachClass_Mode
        {
            课文讲解 = 1,
            听力讲解 = 2,
            口语教学 = 3,
            阅读训练 = 4,
            随堂听力测试 = 5,
            情景会话 = 6,
            协作写作 = 7,
            课堂辅导 = 8,
            随堂测试 = 9,
            复习辅导 = 10,
            协作设计制作 = 11,
            知识竞答 = 12,
            随堂综合测试 = 13
        }

        /// <summary>
        /// 操作类型
        /// </summary>
        public enum E_Operation
        {
            随堂提问 = 1,
            听写 = 2,
            其他 = 3,
            资料搜索 = 4,
            手动加分 = 5,
            测试加分 = 6,
            跟读 = 7,
            听力测试 = 8,
            配音 = 9,
            小组讨论 = 10,

            朗读 = 11,
            口头表达 = 12,
            写作 = 13,
            影子训练 = 14,
            短期记忆 = 15,
            自主领读 = 16,
            自主复听 = 17      //自主复听也是操作类型
        }

        /// <summary>
        /// 学生获取分数类型
        /// </summary>
        public enum E_DataType
        {
            手动课堂加分 = 1,
            课堂测试加分 = 2,
            课堂训练 = 3,
            其他类型 = 4
        }

        /// <summary>
        /// 教师设置项
        /// </summary>
        public enum E_TeachSetItem
        {
            常用教材设置 = 1,
            更新体验 = 2,
            主界面定制提示 = 3,//'用于控制主界面定制提示是否显示
            本地收藏地址 = 4,//'教师设置本地收藏地址
            资料讲解全屏 = 5//xiezongwu20130711增加资料讲解是否全屏设置，默认不全屏
        }

        /// <summary>
        /// 资料来源类别
        /// </summary>
        public enum E_OriResourType
        {
            图文教材库 = 1,      //大学才会使用，因为兼容原lgzx_dir
            多媒体教程库 = 2,
            公共媒体库 = 3,
            作业库 = 4,
            情景会话库 = 5,
            水平试题库 = 6,
            知识点课件库 = 7,
            主题背景库 = 8,
            本地电脑 = 9,    //v5.0新增
            翻译库 = 10,     //新增翻译库
            电子资源库=11,
            数字化资源库=12,
            网络化课件库=13,
            U盘=14,
            专用教材=15,
            课前预习=16,
            课堂教案=17,
            课后练习=18,
            智能化课件=19,
            智能组卷试卷 = 20,
            随堂测试卷 = 21,
            教学方案 = 22,
            课前预习方案 = 23,
            课堂教案V2 = 24,
            课后作业方案 = 25,
            课文讲解课件 = 26,
            重难点讲解课件 = 27,
            本地资料课件集 = 28,  
            智能化课件组成 = 29  //智能化课件的子模块类型例如主体原文、课前热身xiezongwu201710-13
           
        }

        /// <summary>
        /// 资料的结果类型
        /// </summary>
        public enum E_ResourceResultType
        {
            错误率 = 1,
            平均分 = 2
        }

        #endregion


        #region 事件
        public delegate void EventInvalidTokenHandler();
        public event EventInvalidTokenHandler EventInvalidToken; 

        #endregion



        #region  属性

        // 学科
        public enum E_Subject
        {
            None = 0,
            语文 = 1,
            数学 = 2,
            英语 = 3,
            物理 = 4,
            化学 = 5,
            生物 = 6,
            政治 = 7,
            历史 = 8,
            地理 = 9,
            科学=10,
            美术=11
        }

        //其它系统名称（从本系统接口获取，区别于云平台）
        public enum E_OtherSysName
        {
            None = 0,            //表示不指定，则应该返回所有系统的值
            基础 = 1,
            CSetup = 2,
            作文评分 = 3,
            自由学习 = 4,
            口语测试 = 5,
            资源库 = 6,
            技能训练 = 7,
            主题背景库 = 8,
            VOD = 9,
            知识点 = 10
        }

        /// <summary>
        /// 教学系统产品名称
        /// </summary>
        public enum E_TeachProductName
        {
            Default=0,//默认为英语课堂教学系统5.2
            EnglishClassRoomTeachSystemV52=1,
            GeneralClassRoomTeachSystemV20=2,
            InternetStudySystemV10=3
        }

        //本系统的学科ID
        public byte MySubjectID
        {
            get
            {
                return mMySubjectID;
            }
        }
        //当前系统Id
        public string CurSysID {
            get {
                return m_curSysID;
            }
        }
        //本系统的系统ID
        public string MySysID
        {
            get
            {
                return C_MySystemID;
            }
        }
        //学生成绩总评
        public string StudentResultSysID
        {
            get
            {
                return C_StudentResultID;
            }
        }

        // 课堂信息管理API访问地址
        public string ApiBaseUrl
        {
            get
            {
                return mApiBaseUrl;
            }
        }

        //云平台基础平台地址
        public string CloudPlatformBFIP
        {
            get
            {
                return mCloudPlatformBFIP;
            }
        }
        public string CloudPlatformBFPort
        {
            get
            {
                return mCloudPlatformBFPort;
            }
        }
        public string CloudPlatformBFPhyPath
        {
            get
            {
                return mCloudPlatformBFPhyPath;
            }
        }
        //云平台基础平台学科ID
        public string CloudPlatformSubjectID
        {
            get
            {
                return mCloudPlatformSubjectID;
            }
        }
        //云平台基础平台学科名称
        public string CloudPlatformSubjectName
        {
            get
            {
                return mCloudPlatformSubjectName;
            }
        }

        //智能化语言实验室网站地址
        public string TeachWebServerICIP
        {
            get
            {
                return m_strWebServerICIP;
            }
        }
        public long TeachWebServerICPort
        {
            get
            {
                return m_lngWebServerICPort;
            }
        }
        public string TeachWebServerICPhyPath
        {
            get
            {
                return m_strWebServerICPhyPath;
            }
        }

        //电子阅览室（李琳璐，又名学科资源库）网站服务器地址（IP:Port）
        public string MultipleSubjectWebIPAndPort
        {
            get
            {
                return mMultipleSubjectWebIPAndPort;
            }
        }
        //电子阅览室（李琳璐，又名学科资源库）WS服务和API地址（IP:Port）
        public string MultipleSubjectAPIIPAndPort
        {
            get
            {
                return mMultipleSubjectAPIIPAndPort;
            }
        }

        //当前学校ID
        public string SchoolID
        {
            get
            {
                return mSchoolID;
            }
        }
        //学校名称
        public string SchoolName
        {
            get
            {
                return mSchoolName;
            }
        }
        //学校级别（ 1：大学、 2：中小学）
        public string SchoolLevel
        {
            get
            {
                return mSchoolLevel;
            }
        }
        public string SchoolType
        {
            get
            {
                return mSchoolType;
            }
        }
        //本系统的概念，区别于云平台
        public int MySchoolType
        {
            get
            {
                return mMySchoolType;
            }
        }


        public string TermYear
        {
            get
            {
                return mTermYear;
            }
        }

        public string ZYK_DB_IP
        {
            get
            {
                return m_strResourceDBIP;
            }
        }
        public string ZYK_DB_Name
        {
            get
            {
                return m_strResourceDBName;
            }
        }
        public string ZYK_DB_UserName
        {
            get
            {
                return m_strResourceDBUserName;
            }
        }
        public string ZYK_DB_UserPwd
        {
            get
            {
                return m_strResourceDBUserPwd;
            }
        }

        public string ZYK_FTP_IP
        {
            get
            {
                return m_strFtpIPAddress;
            }
        }
        public long ZYK_FTP_Port
        {
            get
            {
                return m_lngFtpPort;
            }
        }
        public string ZYK_FTP_Name
        {
            get
            {
                return m_strFtpName;
            }
        }
        public string ZYK_FTP_UserName
        {
            get
            {
                return m_strFtpUserName;
            }
        }
        public string ZYK_FTP_UserPwd
        {
            get
            {
                return m_strFtpUserPwd;
            }
        }
        public string ZYK_FTP_PhyPath
        {
            get
            {
                return m_strFtpPhyPath;
            }
        }

        public string ZYK_HTTP_IP
        {
            get
            {
                return m_strHttpIPAddress;
            }
        }
        public int ZYK_HTTP_Port
        {
            get
            {
                return m_intHttpPort;
            }
        }
        public string ZYK_HTTP_Name
        {
            get
            {
                return m_strHttpName;
            }
        }

        //作文评分
        public string EssayWSIP
        {
            get
            {
                return m_strEssayWSIP;
            }
        }
        public long EssayWSPort
        {
            get
            {
                return m_lngEssayWSPort;
            }
        }

        //作文评分
        public string EssayWSIP_IR
        {
            get
            {
                return m_strEssayWSIP_IR;
            }
        }
        public long EssayWSPort_IR
        {
            get
            {
                return m_lngEssayWSPort_IR;
            }
        }
        public string EssayVirDir_IR
        {
            get
            {
                return m_strEssayVirDir_IR;
            }
        }
        //口语考试 
        public string OTServiceIP
        {
            get
            {
                return m_strOTServiceIP;
            }
        }
        public long OTServicePort
        {
            get
            {
                return m_lngOTServicePort;
            }
        }

        //主题背景库
        public string ThemeVideoWSIP
        {
            get
            {
                return mThemeVideoWSIP;
            }
            set
            {
                mThemeVideoWSIP = value;
            }
        }
        public string ThemeVideoPort
        {
            get
            {
                return mThemeVideoPort;
            }
            set
            {
                mThemeVideoPort = value;
            }
        }
        /// <summary>
        /// 主题背景库的聚合应用程序名
        /// </summary>
        public string ThemeVideoVirDir { get { return mThemeVideoVirDir; } }

        //知识点
        public string KnowledgeWSIP
        {
            get
            {
                return m_strKnowledgeWSIP;
            }
        }
        public long KnowledgeWSPort
        {
            get
            {
                return m_lngKnowledgeWSPort;
            }
        }
        public string KnowledgeDBIP
        {
            get
            {
                return m_strKnowledgeDBIP;
            }
        }
        public string KnowledgeDBName
        {
            get
            {
                return m_strKnowledgeDBName;
            }
        }
        public string KnowledgeDBUserName
        {
            get
            {
                return m_strKnowledgeDBUserName;
            }
        }
        public string KnowledgeDBUserPwd
        {
            get
            {
                return m_strKnowledgeDBUserPwd;
            }
        }

        /// <summary>
        /// 技能训练服务器IP
        /// </summary>
        public string SkillTrainIP
        {
            get
            {
                return mSkillTrainIP;
            }
        }
        /// <summary>
        /// 技能训练服务器Port
        /// </summary>
        public long SkillTrainPort
        {
            get
            {
                return mSkillTrainPort;
            }
        }
        /// <summary>
        /// 技能训练服务器的聚合应用程序名
        /// </summary>
        public string SkillTrainVirDir {get { return mSkillTrainVirDir; }}

        //WebSocket
        public string WebSocketIP
        {
            get
            {
                return mWebSocketIP;
            }
            set
            {
                mWebSocketIP = value;
            }
        }
        public string WebSocketPort
        {
            get
            {
                return mWebSocketPort;
            }
            set
            {
                mWebSocketPort = value;
            }
        }

        //大数据中心
        public string BigDataWebIPAndPort
        {
            get
            {
                return mBigDataWebAddr;
            }
            set
            {
                mBigDataWebAddr = value;
            }
        }
        public string BigDataAPIIPAndPort
        {
            get
            {
                return mBigDataAPIAddr;
            }
            set
            {
                mBigDataAPIAddr= value;
            }
        }

        //个人网盘接口的地址
        public string PersonalDiskAPIIPAndPort
        {
            get
            {
                return mPersonalDiskAPIIPAndPort;
            }
        }

        //资源库管理
        public string ZYKGL_WS_IP
        {
            get
            {
                return mZYKGL_WS_IP;
            }
        }
        public string ZYKGL_WS_Port
        {
            get
            {
                return mZYKGL_WS_Port;
            }
        }
        /// <summary>
        /// 资源库管理（A00）的聚合应用程序名
        /// </summary>
        public string ZYKGL_WS_VirDir { get { return mZYKGL_WS_VirDir; } }
        //知识点识别服务器WS
        public string ZSDSB_WS_IP
        {
            get
            {
                return mZSDSB_WS_IP;
            }
        }
        public string ZSDSB_WS_Port
        {
            get
            {
                return mZSDSB_WS_Port;
            }
        }
        /// <summary>
        /// 知识点识别服务器的聚合应用程序名
        /// </summary>
        public string ZSDSB_WS_VirDir { get { return mZSDSB_WS_VirDir; } }
        //知识点库服务器WS
        public string ZSDK_WS_IP
        {
            get
            {
                return mZSDK_WS_IP;
            }
        }
        public string ZSDK_WS_Port
        {
            get
            {
                return mZSDK_WS_Port;
            }
        }

        /// <summary>
        /// 知识点库FTP，sIP
        /// </summary>
        public string ZSDK_FTP_IP
        {
            get
            {
                return mZSDK_FTP_IP;
            }
        }
        /// <summary>
        /// 知识点库FTP，sPort
        /// </summary>
        public string ZSDK_FTP_Port
        {
            get
            {
                return mZSDK_FTP_Port;
            }
        }
        /// <summary>
        /// 知识点库FTP，sVirtualPath
        /// </summary>
        public string ZSDK_FTP_VirDir
        {
            get
            {
                return mZSDK_FTP_VirDir;
            }
        }
        /// <summary>
        /// 知识点库FTP，sUserName
        /// </summary>
        public string ZSDK_FTP_UserName
        {
            get
            {
                return mZSDK_FTP_UserName;
            }
        }
        /// <summary>
        /// 知识点库FTP，sPWD
        /// </summary>
        public string ZSDK_FTP_Pwd
        {
            get
            {
                return mZSDK_FTP_Pwd;
            }
        }
        /// <summary>
        /// 知识点库服务器的聚合应用程序名
        /// </summary>
        public string ZSDK_WS_VirDir { get { return mZSDK_WS_VirDir; } }
        //知识点课件WS以及资源库WS
        public string ZSDKJ_WS_IP
        {
            get
            {
                return mZSDKJ_WS_IP;
            }
        }
        public string ZSDKJ_WS_Port
        {
            get
            {
                return mZSDKJ_WS_Port;
            }
        }
        /// <summary>
        /// 知识点课件WS以及资源库WS服务器的聚合应用程序名
        /// </summary>
        public string ZSDKJ_WS_VirDir { get { return mZSDKJ_WS_VirDir; } }
        //资源库使用信息收集（与资源库web站点在一起）
        public string ZYKXXSJ_WS_IP
        {
            get
            {
                return mZYKXXSJ_WS_IP;
            }
        }
        public string ZYKXXSJ_WS_Port
        {
            get
            {
                return mZYKXXSJ_WS_Port;
            }
        }
        /// <summary>
        /// 资源管理平台Web服务器的聚合应用程序名
        /// </summary>
        public string ZYKXXSJ_WS_VirDir { get { return mZYKXXSJ_WS_VirDir; } }

        //知识点资源库HTTP信息
        public string ZSDZYK_HTTP_IP
        {
            get
            {
                return mZSDZYK_HTTP_IP;
            }
        }
        public string ZSDZYK_HTTP_Port
        {
            get
            {
                return mZSDZYK_HTTP_Port;
            }
        }
        /// <summary>
        ///Http资源服务器的聚合应用程序名
        /// </summary>
        public string ZSDZYK_HTTP_VirDir { get { return mZSDZYK_HTTP_VirDir; } }

        //知识点资源库FTP信息 
        public string ZSDZYK_FTP_IP
        {
            get
            {
                return mZSDZYK_FTP_IP;
            }
        }
        public string ZSDZYK_FTP_Port
        {
            get
            {
                return mZSDZYK_FTP_Port;
            }
        }
        public string ZSDZYK_FTP_UserName
        {
            get
            {
                return mZSDZYK_FTP_UserName;
            }
        }
        public string ZSDZYK_FTP_Pwd
        {
            get
            {
                return mZSDZYK_FTP_Pwd;
            }
        }
        /// <summary>
        ///Ftp资源服务器的聚合应用程序名
        /// </summary>
        public string ZSDZYK_FTP_VirDir { get { return mZSDZYK_FTP_VirDir; } }


        //以新的方式获取的作文评分WS地址
        public string EssayWSIP2
        {
            get
            {
                return m_strEssayWSIP2;
            }
        }
        public long EssayWSPort2
        {
            get
            {
                return m_lngEssayWSPort2;
            }
        }
        /// <summary>
        /// 作文评分的聚合应用程序名
        /// </summary>
        public string EssayVirDir { get { return m_strEssayVirDir; } }

        #endregion



        #region 成员变量
        //基础平台基本系统信息
        private CLPSysConfigInfo clpSysConfigInfo = null;
        //解密使用
        private const string C_SecCode = "11122156";
        private string m_curSysID="B10";
        //英语课堂教学系统系统ID
        private const string C_MySystemID = "B10";
        //通用课堂教学系统系统ID
        private const string C_GeneralSystemID = "B11";
        //电子阅览室系统ID（管理端）
        private const string C_InternetStudySysID = "B30";
        private const string C_DigitalLibarySysID = "C11";
        //大数据中心之大数据信息收集系统ID
        private const string C_BigDataSysID = "890";
        //学生成绩总评系统ID（等于Web端“学生课堂表现”）
        //该系统URL可在更多应用系统中获取
        private const string C_StudentResultID = "810";
        //大数据分析中心（教师端）（等同于Web端“学科大数据”）
        //该系统URL可在更多应用系统中获取
        private const string C_BigDataAnalysisSysID = "841";
        //个人网盘系统ID
        private const string C_PersonalDiskSysID = "360";
        //多教室统一在线管理系统ID
        private const string C_ClassroomManagerSysID = "E00";
        //资源库管理系统ID
        private const string C_ZYKGLSysID = "A00";
        //作文评分系统ID
        private const string C_EssaySysID = "J11";
        //主题背景库系统ID
        //private const string C_ThemeVideo = "K20";
        //网络化资源库
        private const string C_NETZYKSysID = "B90";

        private string m_strOTServiceIP;      //口语考试ServiceIP  OralTest
        private long m_lngOTServicePort;      //口语考试ServicePort

        private string m_strZYWSIP;          //作业的WSIP
        private long m_lngZYWSPort;          //作业的WSPort
        private string m_strZYKWSVirDir;
        //xiezongwu20151126增加作文评分接口
        private string m_strEssayWSIP;  //作文评分WSip
        private long m_lngEssayWSPort;    //作文评分WSport

        private string m_strEssayWSIP_IR;  //作文评分WSip_IR
        private long m_lngEssayWSPort_IR;    //作文评分WSport_IR
        private string m_strEssayVirDir_IR;//qinkun2022/2/18增加，由于m_strEssayVirDir已被使用，增加"_IR"区别

        private string m_strKnowledgeWSIP;          //知识点的WSIP
        private long m_lngKnowledgeWSPort;          //知识点的WSPort
        private string m_strKnowledgeDBIP;          //知识点数据库信息
        private string m_strKnowledgeDBName;        //知识点数据库名称
        private string m_strKnowledgeDBUserName;    //数据库用户名
        private string m_strKnowledgeDBUserPwd;     //知识点数据库密码

        //技能训练服务器信息
        private string mSkillTrainIP;
        private long mSkillTrainPort;
        private string mSkillTrainVirDir;

        //xiezongwu20130401增加FTP资源库独立时获取FTP数据库信息,目前只有资料搜索时才会使用数据库查找方式即访问独立的资源库数据库
        private string m_strResourceDBIP;    //含Port因为该Port与IP是用","隔开
        private string m_strResourceDBName;
        private string m_strResourceDBUserName;
        private string m_strResourceDBUserPwd;

        //全局变量通过属性访问
        private string m_strFtpName;          //Ftp发布路径的ID
        private string m_strFtpUserName;      //当前FTP的用户名
        private string m_strFtpUserPwd;       //当前FTP的密码
        private string m_strFtpIPAddress;     //IP地址
        private long m_lngFtpPort;            //端口号  //xiezongwu2011-04-18改为Long类型
        private string m_strFtpPhyPath;       //物理路径
        private string m_strHttpIPAddress;    //IP地址
        private int m_intHttpPort;        //端口
        private string m_strHttpName;         //Http对应的ServerName如Lgftp，默认为lgftp

        private string m_strWebServerICIP;      //智能化语言实验室发布网站的地址及虚拟目录
        private long m_lngWebServerICPort;
        private string m_strWebServerICPhyPath;

        //多教室统一在线管理的WS的IP和Port
        private string mCMWSIPandPort;

        //本系统API和WS服务的地址
        private string mNetTeachApiIP;
        private string mNetTeachApiPort;
        private string mNetTeachApiVirDir;//add by qinkun @20191012,修改支持兼容版本

        //云平台基础平台服务地址
        private string mCloudPlatformBFUrl;//不带http
        private string mCloudPlatformBFIP;
        private string mCloudPlatformBFPort;
        private string mCloudPlatformBFPhyPath;

        //访问Web API
        private string mApiBaseUrl;
        private CommandApi mCommandApi;

        //访问WS服务
        private CommandWS mCommandWS;

        //初始化状态
        private bool mInitStatus;

        //学科平台下的各个系统（从云平台获取）
        private SubjectPlatformSysInfoM[] mSubjectPlatformSysInfo;

        //电子阅览室（李琳璐，曾名学科资源库）网站服务器地址
        private string mMultipleSubjectWebIPAndPort;
        //电子阅览室（李琳璐，曾名学科资源库）WS服务和API地址
        private string mMultipleSubjectAPIIPAndPort;

        //个人网盘系统接口的IP和Port
        private string mPersonalDiskAPIIPAndPort;

/*
        //云平台基础平台Ftp的IP
        private string mCloudPlatformFtpIP;
        //云平台基础平台Ftp的端口
        private string mCloudPlatformFtpPort;
        //云平台基础平台Ftp的虚拟目录
        private string mCloudPlatformFtpVirPath;
        //云平台基础平台Ftp的用户名
        private string mCloudPlatformFtpUserName;
        //云平台基础平台Ftp的密码
        private string mCloudPlatformFtpPassword;
        //云平台基础平台Ftp的物理路径
        private string mCloudPlatformFtpPhyPath;
*/
        //学校ID
        private string mSchoolID;
        //学校名称
        private string mSchoolName;
        //学校级别（ 1：大学、 2：中小学）
        private string mSchoolLevel;
        //学校类型
        /*（当 SchoolLevel 为 1 时， 2:2 年制、 3:3 年制、 4:4 年制、
5:5 年制、 6:6 年制、 7:7 年制；当 SchoolLevel 为 2 时， 1：小学、 2：初中、 3：小
学+初中、 4：高中、 5：小学+高中、 6：初中+高中、 7：小学+初中+高中）
         */
        private string mSchoolType;
        //1-小学，2-初中，3-高中，4-大学，5-小学+初中，6-初中+高中，7-小学+高中（一般不会有此类别），8-小学+初中+高中
        private int mMySchoolType;

        //学期
        private string mTermYear;

        //保存Token
        private string mToken;

        //保存当前学科
        private E_Subject mSubject;
        //本系统学科ID
        private byte mMySubjectID;
        //云平台学科ID
        private string mCloudPlatformSubjectID;
        //云平台学科名
        private string mCloudPlatformSubjectName;
         
        private string curClassSubjectId;//当前用于上课的学科id
        //教师ID
        private string mTeacherID;

        //WebSocket
        private string mWebSocketIP;
        private string mWebSocketPort;

        //大数据中心地址
        private string mBigDataWebAddr;
        private string mBigDataAPIAddr;

        //主题背景库
        private string mThemeVideoWSIP;  //主题背景库WSip
        private string mThemeVideoPort;    //主题背景库WSport
        private string mThemeVideoVirDir;//主题背景库应用程序名

        //资源库管理
        private string mZYKGL_WS_IP;
        private string mZYKGL_WS_Port;
        private string mZYKGL_WS_VirDir;
        //知识点识别服务器WS
        private string mZSDSB_WS_IP;
        private string mZSDSB_WS_Port;
        private string mZSDSB_WS_VirDir;
        //知识点库服务器WS
        private string mZSDK_WS_IP;
        private string mZSDK_WS_Port;
        private string mZSDK_WS_VirDir;
        //2020.4.30增加by qinkun 新版拼音表使用
        private string mZSDK_FTP_IP;
        private string mZSDK_FTP_Port;
        private string mZSDK_FTP_VirDir;
        private string mZSDK_FTP_UserName;
        private string mZSDK_FTP_Pwd;
        //知识点课件WS以及资源库WS
        private string mZSDKJ_WS_IP;
        private string mZSDKJ_WS_Port;
        private string mZSDKJ_WS_VirDir;
        //资源库使用信息收集（与资源库web站点在一起）
        private string mZYKXXSJ_WS_IP;
        private string mZYKXXSJ_WS_Port;
        private string mZYKXXSJ_WS_VirDir;
        //知识点资源库HTTP的IP和Port
        private string mZSDZYK_HTTP_IP;
        private string mZSDZYK_HTTP_Port;
        private string mZSDZYK_HTTP_VirDir;
        //知识点资源库FTP信息
        private string mZSDZYK_FTP_IP;
        private string mZSDZYK_FTP_Port;
        private string mZSDZYK_FTP_VirDir;
        private string mZSDZYK_FTP_UserName;
        private string mZSDZYK_FTP_Pwd;

        //以新的方式获取作文评分的WS地址
        private string m_strEssayWSIP2;
        private long m_lngEssayWSPort2;
        private string m_strEssayVirDir;

        //在线讨论系统id,系统api地址
        private string m_tutorSysId="629";
        private string m_tutorSysApiUrl;

        //教学云备课管理端地址
        private string m_bkSysId = "S10";
        private string m_bkSysWebUrl;
        #endregion



        #region 公有方法

        #region 初始化相关

        public TeachInfoI()
        {
            mCommandApi = new CommandApi();
            mCommandWS = new CommandWS();

            mInitStatus = false;
        }
        
        /// <summary>
        /// 初始化,根据不同的产品，获取不同的接口的入口地址
        /// </summary>
        /// <param name="sCloudBasicPlatformBFUrl">基础云平台URL,如http://172.16.16.16:8888/</param>
        /// <param name="sProductName">产品名称，枚举变量</param>
        /// <returns>初始化结果，True-成功；False-失败</returns>
        public bool Initialize(string sCloudBasicPlatformBFUrl,E_TeachProductName sProductName)
        {
            XmlDocument xmlDoc = null;
            string strWholeUrl = sCloudBasicPlatformBFUrl + "Base/WS/Service_Basic.asmx/WS_G_GetSubSystemServerInfoForAllSubject";
            string strParam = "sysID={0}";
            if (sProductName==E_TeachProductName.EnglishClassRoomTeachSystemV52)
            {
                WriteDebugInfo("Initialize", "本系统是云网络智慧教室");
                strParam = string.Format(strParam, C_MySystemID);
                m_curSysID = C_MySystemID;
            }
            else if(sProductName==E_TeachProductName.GeneralClassRoomTeachSystemV20)
            {
                WriteDebugInfo("Initialize", "本系统是通用网络化课堂教学");
                strParam = string.Format(strParam, C_GeneralSystemID);
                m_curSysID = C_GeneralSystemID;
            }
            else if(sProductName==E_TeachProductName.InternetStudySystemV10)
	    {
                WriteDebugInfo("Initialize", "本系统是互联网学习系统");
                strParam = string.Format(strParam, C_InternetStudySysID);
	        m_curSysID = C_InternetStudySysID;
            }
            else if (sProductName == E_TeachProductName.Default)
            {
                WriteDebugInfo("Initialize", "本系统是英语课堂教学系统");
                strParam = string.Format(strParam, C_MySystemID);
                m_curSysID = C_MySystemID;
            }
            else
                WriteDebugInfo("Initialize", "未知系统");
            string strReturn = mCommandWS.CallMethodPost(strWholeUrl, strParam);
            if (string.IsNullOrEmpty(strReturn))
            {
                if (string.IsNullOrEmpty(strReturn))
                {
                    return false;
                }
            }
            xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(strReturn);
            XmlNodeList list = xmlDoc.GetElementsByTagName("anyType");
            if (list == null || list.Count == 0)
            {
                return false;
            }
            string strNetTeachIpAndPort = list[0].ChildNodes[3].InnerText;
            strNetTeachIpAndPort = strNetTeachIpAndPort.Replace("http://", null).TrimEnd('/');

            //mCloudPlatformBFUrl = sCloudBasicPlatformBFUrl.Replace("http://", null).TrimEnd('/');
            //string strNetTeachIpAndPort = GetSubSysApiIPandPort(C_MySystemID);


            if (string.IsNullOrEmpty(strNetTeachIpAndPort) == false)
            {
                string[] arrTemp = strNetTeachIpAndPort.Split(':');
                if (arrTemp != null && arrTemp.Length == 2)
                {
                    string strNetTeachIP = arrTemp[0];
                    //string strNetTeachPort = arrTemp[1];
                    string strNetTeachPort = "";
                    long lPort = 0;
                    FormatPortAndVirdir(arrTemp[1],out lPort,out mNetTeachApiVirDir);
                    strNetTeachPort = lPort.ToString();
                    return Initialize(strNetTeachIP, strNetTeachPort);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 初始化,默认以英语课堂教学系统V5.2接口地址启动
        /// </summary>
        /// <param name="sCloudBasicPlatformBFUrl">基础云平台URL,如http://172.16.16.16:8888/</param>
        /// <returns>初始化结果，True-成功；False-失败</returns>
        public bool Initialize(string sCloudBasicPlatformBFUrl)
        {
            //Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
            //Debug.AutoFlush = true;
            //Debug.Indent();
            //Debug.WriteLine("Initialize(string sCloudBasicPlatformBFUrl)被调用，参数：" + sCloudBasicPlatformBFUrl);
            XmlDocument xmlDoc = null;
            string strWholeUrl = sCloudBasicPlatformBFUrl + "Base/WS/Service_Basic.asmx/WS_G_GetSubSystemServerInfoForAllSubject";
            string strParam = "sysID={0}";
           // strParam = string.Format(strParam, C_MySystemID);
            strParam = string.Format(strParam, m_curSysID);//20180706 ldy修改，使用当前系统id查询
            string strReturn = mCommandWS.CallMethodPost(strWholeUrl, strParam);
            if (string.IsNullOrEmpty(strReturn))
            {
                //未安装课堂教学系统的情况下
                //mCloudPlatformBFUrl = sCloudBasicPlatformBFUrl;
                //mCloudPlatformBFUrl = mCloudPlatformBFUrl.Replace("http://", "").TrimEnd('/');
                //char[] cSplit = {':'};
                //string[] sValueSplit = mCloudPlatformBFUrl.Split(cSplit);
                //mCloudPlatformBFIP = sValueSplit[0];
                //mCloudPlatformBFPort = sValueSplit[1];
                //bool bValue=Initialize("", "", true);
                //return bValue;
                return false;
            }
            else
            {
                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);
                XmlNodeList list = xmlDoc.GetElementsByTagName("anyType");
                if (list == null || list.Count == 0)
                {
                    //未安装课堂教学系统的情况下
                    mCloudPlatformBFUrl = sCloudBasicPlatformBFUrl;
                    mCloudPlatformBFUrl = mCloudPlatformBFUrl.Replace("http://", "").TrimEnd('/');
                    char[] cSplit = { ':' };
                    string[] sValueSplit = mCloudPlatformBFUrl.Split(cSplit);
                    mCloudPlatformBFIP = sValueSplit[0];
                    mCloudPlatformBFPort = sValueSplit[1];
                    //WriteDebugInfo("Initialize1", " Initialize(\"\", \"\", true);");
                    //Debug.WriteLine("在Initialize(string sCloudBasicPlatformBFUrl)中Initialize(\"\", \"\", true)被调用了");
                    //Debug.Unindent();
                    bool bValue = Initialize("", "", true);
                    return bValue;
                }
                string strNetTeachIpAndPort = list[0].ChildNodes[3].InnerText;
                strNetTeachIpAndPort = strNetTeachIpAndPort.Replace("http://", null).TrimEnd('/');

                //mCloudPlatformBFUrl = sCloudBasicPlatformBFUrl.Replace("http://", null).TrimEnd('/');
                //string strNetTeachIpAndPort = GetSubSysApiIPandPort(C_MySystemID);


                if (string.IsNullOrEmpty(strNetTeachIpAndPort) == false)
                {
                    string[] arrTemp = strNetTeachIpAndPort.Split(':');
                    if (arrTemp != null && arrTemp.Length == 2)
                    {
                        string strNetTeachIP = arrTemp[0];
                        string strNetTeachPort = "";// arrTemp[1];
                        long lPort=0;
                        FormatPortAndVirdir(arrTemp[1],out lPort,out mNetTeachApiVirDir);
                        strNetTeachPort=lPort.ToString();
                        //WriteDebugInfo("Initialize2", " Initialize(strNetTeachIP, strNetTeachPort)");
                        //Debug.WriteLine("在Initialize(string sCloudBasicPlatformBFUrl)中Initialize(strNetTeachIP, strNetTeachPort)被调用了");
                        //Debug.Unindent();
                        return Initialize(strNetTeachIP, strNetTeachPort);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

        }
        /// <summary>
        /// 为学科教学云初始化专门定制的接口ldy20180530
        /// </summary>
        /// <param name="sCloudBasicPlatformBFUrl"></param>
        /// <returns></returns>
        public bool InitializeXKJXY(string sCloudBasicPlatformBFUrl)
        {
            try
            {
                mCloudPlatformBFUrl = sCloudBasicPlatformBFUrl;
                mCloudPlatformBFUrl = mCloudPlatformBFUrl.Replace("http://", "").TrimEnd('/');
                char[] cSplit = { ':' };
                string[] sValueSplit = mCloudPlatformBFUrl.Split(cSplit);
                mCloudPlatformBFIP = sValueSplit[0];
                mCloudPlatformBFPort = sValueSplit[1];
                bool bValue = Initialize("", "", true);
                return bValue;
            }
            catch (Exception exc)
            {
                WriteErrorMessage("InitializeXKJXY", exc.ToString());
                return false;
            }
        }
                
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="strNetTeachIP">课堂教学Ws接口地址IP</param>
        /// <param name="strNetTeachPort">课堂教学Ws接口地址Port，支持带应用程序名</param>
        /// <returns>初始化结果，True-成功；False-失败</returns>
        public bool Initialize(string strNetTeachIP, string strNetTeachPort)
        {
            try
            {
                //WriteDebugInfo("Initialize(string strNetTeachIP, string strNetTeachPort)被直接调用啦", strNetTeachIP + "：" + strNetTeachPort);
                //Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
                //Debug.AutoFlush = true;
                //Debug.Indent();
                //Debug.WriteLine("Initialize(string strNetTeachIP, string strNetTeachPort)被调用了，参数:strNetTeachIP=" + strNetTeachIP + ",strNetTeachPort=" + strNetTeachPort);
                //Debug.Unindent();
                if (mInitStatus == true)
                {
                    return false;
                }
                string tmpNetTeachApiVirDir = "-1";
                long ltmpPort=0;
                FormatPortAndVirdir(strNetTeachPort, out ltmpPort, out tmpNetTeachApiVirDir);
                if (string.IsNullOrEmpty(tmpNetTeachApiVirDir) == true)
                {
                    mNetTeachApiIP = strNetTeachIP;
                    mNetTeachApiPort = strNetTeachPort;
                }
                else//学生端调用按这种方式调用，不采用传基础平台的方式调用，strNetTeachPort实际为"端口/NetTeachApiVirDir/"
                {
                    mNetTeachApiIP = strNetTeachIP;
                    mNetTeachApiPort = ltmpPort.ToString();
                    mNetTeachApiVirDir = tmpNetTeachApiVirDir;
                }

                mApiBaseUrl = string.Format(Properties.Resources.TeachInfoUrl, strNetTeachIP, strNetTeachPort, mNetTeachApiVirDir);
                WriteTrackLog("初始化课堂教学webapi地址:", mApiBaseUrl);
                mCommandApi.BaseUrl = mApiBaseUrl;

                //初始化所有系统访问地址
                //数据库信息及FTP信息需要采用加密和解密方法获取，防止被破解
                #region 获取云平台和本系统网站端地址
                XmlDocument xmlDoc;
                string strWebServiceURL = "";
                XmlNodeList list;

                string strParam = "";
                string strReturn = "";

                int iCount;

                strWebServiceURL = "http://" + mNetTeachApiIP + ":" + mNetTeachApiPort + "/"+ mNetTeachApiVirDir+"jxWebService.asmx/WS_G_BasicPFWebServerInfo";
                strParam = "strServerID=" + "";
                strReturn = mCommandWS.CallMethodPost(strWebServiceURL, strParam);
                if (string.IsNullOrEmpty(strReturn))
                {
                    WriteErrorMessage("Initialize", "获取云平台地址、Web网站、WebSocket等信息失败");
                    return false;
                }
                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);
                list = xmlDoc.DocumentElement.GetElementsByTagName("anyType");
                iCount = 0;
                if (list != null)
                {
                    iCount = list.Count;
                }

                if (iCount > 0)
                {
                    for (int i = 0; i < iCount; i++)
                    {
                        XmlNode node = list[i];
                        string strID = node.ChildNodes[0].InnerText;//node.SelectSingleNode("ServerID").InnerText;
                        if (strID == "BasicPf")//云平台基础平台
                        {
                            mCloudPlatformBFIP = ClsParamsEncDec.LgMgr_ParamDecrypt(node.ChildNodes[2].InnerText);
                            mCloudPlatformBFPort = ClsParamsEncDec.LgMgr_ParamDecrypt(node.ChildNodes[7].InnerText);//node.SelectSingleNode("ServerPort").InnerText;
                            mCloudPlatformBFPhyPath = ClsParamsEncDec.LgMgr_ParamDecrypt(node.ChildNodes[6].InnerText);

                            mCloudPlatformBFUrl = mCloudPlatformBFIP;
                            if (string.IsNullOrEmpty(mCloudPlatformBFPort) == false)
                            {
                                mCloudPlatformBFUrl = mCloudPlatformBFUrl + ":" + mCloudPlatformBFPort;
                            }
                        }
                        else if (strID == "TeachWeb")//智能化语言实验室发布网站
                        {
                            m_strWebServerICIP = ClsParamsEncDec.LgMgr_ParamDecrypt(node.ChildNodes[2].InnerText);
                            string strPort = node.ChildNodes[7].InnerText;//node.SelectSingleNode("ServerPort").InnerText;
                            if (string.IsNullOrEmpty(strPort))
                            {
                                m_lngWebServerICPort = 0;
                            }
                            else
                            {
                                m_lngWebServerICPort = Convert.ToInt64(ClsParamsEncDec.LgMgr_ParamDecrypt(strPort));
                            }
                            m_strWebServerICPhyPath = ClsParamsEncDec.LgMgr_ParamDecrypt(node.ChildNodes[6].InnerText);
                        }
                        else if (strID == "WebSocket")
                        {
                            mWebSocketIP = ClsParamsEncDec.LgMgr_ParamDecrypt(node.ChildNodes[2].InnerText);
                            mWebSocketPort = ClsParamsEncDec.LgMgr_ParamDecrypt(node.ChildNodes[7].InnerText);
                        }
                    }
                }//end if(count>0)

                #endregion

                #region 获取各应用系统数据接口地址（从本系统的WS服务获取）
                P_GetOtherSysService();

                //修改资源库地址(B90)从基础平台获取 ModifiedByQinkun@20171030
                string strNetZYK_IPandPort = GetSubSysApiIPandPort(C_NETZYKSysID);
                if (string.IsNullOrEmpty(strNetZYK_IPandPort) == false)
                {
                    string[] arrTemp = strNetZYK_IPandPort.Split(':');
                    if (arrTemp != null && arrTemp.Length == 2)
                    {
                        m_strZYWSIP = arrTemp[0];
                        string[] infoArr = arrTemp[1].Split('/');
                        m_lngZYWSPort =long.Parse(infoArr[0]);
                        if (infoArr.Length >= 2)
                            m_strZYKWSVirDir = infoArr[1]+"/";
                        else
                            m_strZYKWSVirDir = "";
                    }
                }

                //获取资源库数据库、FTP、HTTP信息
                if (string.IsNullOrEmpty(m_strZYWSIP) == false)
                {
                    string strZYKAddr = "http://" + m_strZYWSIP + ":" + m_lngZYWSPort + "/"+ m_strZYKWSVirDir;
                    P_GetZYKinfo(strZYKAddr);
                }

                //（放弃这种方式）获取知识点数据库信息
                //                 if (string.IsNullOrEmpty(m_strKnowledgeWSIP) == false)
                //                 {
                //                     string strZSDAddr = "http://" + m_strKnowledgeWSIP + ":" + m_lngKnowledgeWSPort + "/";
                //                     P_GetZSDinfo(strZSDAddr);
                //                 }
                
                //以新的方式获取知识点相关的WS地址
                string strZYKGL_IPandPort = GetSubSysApiIPandPort(C_ZYKGLSysID);
                if (string.IsNullOrEmpty(strZYKGL_IPandPort) == false)
                {
                    string[] arrTemp = strZYKGL_IPandPort.Split(':');
                    if (arrTemp != null && arrTemp.Length == 2)
                    {
                        mZYKGL_WS_IP = arrTemp[0];
                        //mZYKGL_WS_Port = arrTemp[1];
                        long lZYKGL_WS_Port;
                        FormatPortAndVirdir(arrTemp[1], out lZYKGL_WS_Port, out mZYKGL_WS_VirDir);
                        mZYKGL_WS_Port = lZYKGL_WS_Port.ToString();
                    }
                }
                if (string.IsNullOrEmpty(mZYKGL_WS_IP) == false && string.IsNullOrEmpty(mZYKGL_WS_Port) == false)
                {
                    P_GetZYKGLInfo();
                    m_strEssayWSIP = mZSDSB_WS_IP;
                    m_lngEssayWSPort = Convert.ToInt64(mZSDSB_WS_Port);
                }

                //以新的方式获取作文评分的WS地址
                string strEssayIPandPort = GetSubSysApiIPandPort(C_EssaySysID);
                if (string.IsNullOrEmpty(strEssayIPandPort) == false)
                {
                    string[] arrTemp = strEssayIPandPort.Split(':');
                    if (arrTemp != null && arrTemp.Length == 2)
                    {
                        m_strEssayWSIP2 = arrTemp[0];
                        //m_lngEssayWSPort2 = Convert.ToInt64(arrTemp[1]);
                        if (FormatPortAndVirdir(arrTemp[1], out m_lngEssayWSPort2, out m_strEssayVirDir) == false)
                            WriteErrorMessage("Initialize", "获取作文评测数据返回False");
                    }
                }

                //以新的方式获取主题背景库地址
//                 if (string.IsNullOrEmpty(mThemeVideoWSIP))
//                 {
//                     string strEssayIPandPort = GetSubSysApiIPandPort(C_EssaySysID);
// 
//                 }
                #endregion

                #region 获取学校信息和学期
                //未有教师登录时获取一个默认学校信息
                SchoolBaseInfoM schoolInfo = P_GetSchoolInfo_WS();
                P_SetSchoolInfo(schoolInfo);

                //获取学期
                mTermYear = P_GetTermInfo_WS();
                #endregion

                #region 获取大数据中心地址
                CloudPlatformSubsystemM bigData = GetSubSysAddr(C_BigDataSysID, null);
                if (bigData != null)
                {
                    mBigDataWebAddr = bigData.WebSvrAddr;
                    mBigDataAPIAddr = bigData.WsSvrAddr;
                }
                #endregion

                #region 获取个人网盘地址
                mPersonalDiskAPIIPAndPort = GetSubSysApiIPandPort(C_PersonalDiskSysID);
                #endregion

                mInitStatus = true;

                return mInitStatus;
            }
            catch (Exception e)
            {
                WriteErrorMessage("Initialize", e.Message);
            }

            return false;
        }

        private bool Initialize(string strNetTeachIP, string strNetTeachPort,bool begnoreNetTeach)
        {
            try
            {
                if (begnoreNetTeach == false)
                {
                    if (mInitStatus == true)
                    {
                        return false;
                    }

                    mNetTeachApiIP = strNetTeachIP;
                    mNetTeachApiPort = strNetTeachPort;

                    mApiBaseUrl = string.Format(Properties.Resources.TeachInfoUrl, strNetTeachIP, strNetTeachPort,mNetTeachApiVirDir);
                    WriteTrackLog("初始化课堂教学webapi地址:" , mApiBaseUrl);
                    mCommandApi.BaseUrl = mApiBaseUrl;

                    //初始化所有系统访问地址
                    //数据库信息及FTP信息需要采用加密和解密方法获取，防止被破解
                    #region 获取云平台和本系统网站端地址
                    XmlDocument xmlDoc;
                    string strWebServiceURL = "";
                    XmlNodeList list;

                    string strParam = "";
                    string strReturn = "";

                    int iCount;

                    strWebServiceURL = "http://" + mNetTeachApiIP + ":" + mNetTeachApiPort + "/"+ mNetTeachApiVirDir+"jxWebService.asmx/WS_G_BasicPFWebServerInfo";
                    strParam = "strServerID=" + "";
                    strReturn = mCommandWS.CallMethodPost(strWebServiceURL, strParam);
                    if (string.IsNullOrEmpty(strReturn))
                    {
                        WriteErrorMessage("Initialize", "获取云平台地址、Web网站、WebSocket等信息失败");
                        return false;
                    }
                    xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(strReturn);
                    list = xmlDoc.DocumentElement.GetElementsByTagName("anyType");
                    iCount = 0;
                    if (list != null)
                    {
                        iCount = list.Count;
                    }

                    if (iCount > 0)
                    {
                        for (int i = 0; i < iCount; i++)
                        {
                            XmlNode node = list[i];
                            string strID = node.ChildNodes[0].InnerText;//node.SelectSingleNode("ServerID").InnerText;
                            if (strID == "BasicPf")//云平台基础平台
                            {
                                mCloudPlatformBFIP = ClsParamsEncDec.LgMgr_ParamDecrypt(node.ChildNodes[2].InnerText);
                                mCloudPlatformBFPort = ClsParamsEncDec.LgMgr_ParamDecrypt(node.ChildNodes[7].InnerText);//node.SelectSingleNode("ServerPort").InnerText;
                                mCloudPlatformBFPhyPath = ClsParamsEncDec.LgMgr_ParamDecrypt(node.ChildNodes[6].InnerText);

                                mCloudPlatformBFUrl = mCloudPlatformBFIP;
                                if (string.IsNullOrEmpty(mCloudPlatformBFPort) == false)
                                {
                                    mCloudPlatformBFUrl = mCloudPlatformBFUrl + ":" + mCloudPlatformBFPort;
                                }
                            }
                            else if (strID == "TeachWeb")//智能化语言实验室发布网站
                            {
                                m_strWebServerICIP = ClsParamsEncDec.LgMgr_ParamDecrypt(node.ChildNodes[2].InnerText);
                                string strPort = node.ChildNodes[7].InnerText;//node.SelectSingleNode("ServerPort").InnerText;
                                if (string.IsNullOrEmpty(strPort))
                                {
                                    m_lngWebServerICPort = 0;
                                }
                                else
                                {
                                    m_lngWebServerICPort = Convert.ToInt64(ClsParamsEncDec.LgMgr_ParamDecrypt(strPort));
                                }
                                m_strWebServerICPhyPath = ClsParamsEncDec.LgMgr_ParamDecrypt(node.ChildNodes[6].InnerText);
                            }
                            else if (strID == "WebSocket")
                            {
                                mWebSocketIP = ClsParamsEncDec.LgMgr_ParamDecrypt(node.ChildNodes[2].InnerText);
                                mWebSocketPort = ClsParamsEncDec.LgMgr_ParamDecrypt(node.ChildNodes[7].InnerText);
                            }
                        }
                    }//end if(count>0)

                    #endregion
                }
                #region 获取各应用系统数据接口地址（从本系统的WS服务获取）
                if (begnoreNetTeach == false)  P_GetOtherSysService();

                //修改资源库地址从基础平台获取 ModifiedByQinkun@20171030
                string strNetZYK_IPandPort = GetSubSysApiIPandPort(C_NETZYKSysID);
                if (string.IsNullOrEmpty(strNetZYK_IPandPort) == false)
                {
                    string[] arrTemp = strNetZYK_IPandPort.Split(':');
                    if (arrTemp != null && arrTemp.Length == 2)
                    {
                        m_strZYWSIP = arrTemp[0];

                        string[] infoArr = arrTemp[1].Split('/');
                        m_lngZYWSPort = long.Parse(infoArr[0]);
                        if (infoArr.Length >= 2)
                            m_strZYKWSVirDir = infoArr[1] + "/";
                        else
                            m_strZYKWSVirDir = "";
                    }
                }

                //获取资源库数据库、FTP、HTTP信息
                if (string.IsNullOrEmpty(m_strZYWSIP) == false)
                {
                    string strZYKAddr = "http://" + m_strZYWSIP + ":" + m_lngZYWSPort + "/"+ m_strZYKWSVirDir;
                    P_GetZYKinfo(strZYKAddr);
                }

                //（放弃这种方式）获取知识点数据库信息
                //                 if (string.IsNullOrEmpty(m_strKnowledgeWSIP) == false)
                //                 {
                //                     string strZSDAddr = "http://" + m_strKnowledgeWSIP + ":" + m_lngKnowledgeWSPort + "/";
                //                     P_GetZSDinfo(strZSDAddr);
                //                 }

                //以新的方式获取知识点相关的WS地址
                string strZYKGL_IPandPort = GetSubSysApiIPandPort(C_ZYKGLSysID);
                if (string.IsNullOrEmpty(strZYKGL_IPandPort) == false)
                {
                    string[] arrTemp = strZYKGL_IPandPort.Split(':');
                    if (arrTemp != null && arrTemp.Length == 2)
                    {
                        mZYKGL_WS_IP = arrTemp[0];
                        mZYKGL_WS_Port = arrTemp[1];
                    }
                }
                if (string.IsNullOrEmpty(mZYKGL_WS_IP) == false && string.IsNullOrEmpty(mZYKGL_WS_Port) == false)
                {
                    P_GetZYKGLInfo();
                    m_strEssayWSIP = mZSDSB_WS_IP;
                    m_lngEssayWSPort = Convert.ToInt64(mZSDSB_WS_Port);
                }

                //以新的方式获取作文评分的WS地址
                string strEssayIPandPort = GetSubSysApiIPandPort(C_EssaySysID);
                if (string.IsNullOrEmpty(strEssayIPandPort) == false)
                {
                    string[] arrTemp = strEssayIPandPort.Split(':');
                    if (arrTemp != null && arrTemp.Length == 2)
                    {
                        m_strEssayWSIP2 = arrTemp[0];
                        //m_lngEssayWSPort2 = Convert.ToInt64(arrTemp[1]);
                        if (FormatPortAndVirdir(arrTemp[1], out m_lngEssayWSPort2, out m_strEssayVirDir) == false)
                            WriteErrorMessage("Initialize", "获取作文评测数据返回False");
                    }
                }

                //以新的方式获取主题背景库地址
                //                 if (string.IsNullOrEmpty(mThemeVideoWSIP))
                //                 {
                //                     string strEssayIPandPort = GetSubSysApiIPandPort(C_EssaySysID);
                // 
                //                 }
                #endregion

                #region 获取学校信息和学期
                //未有教师登录时获取一个默认学校信息
                SchoolBaseInfoM schoolInfo = P_GetSchoolInfo_WS();
                P_SetSchoolInfo(schoolInfo);

                //获取学期
                mTermYear = P_GetTermInfo_WS();
                #endregion

                #region 获取大数据中心地址
                CloudPlatformSubsystemM bigData = GetSubSysAddr(C_BigDataSysID, null);
                if (bigData != null)
                {
                    mBigDataWebAddr = bigData.WebSvrAddr;
                    mBigDataAPIAddr = bigData.WsSvrAddr;
                }
                #endregion

                #region 获取个人网盘地址
                mPersonalDiskAPIIPAndPort = GetSubSysApiIPandPort(C_PersonalDiskSysID);
                #endregion

                mInitStatus = true;

                return mInitStatus;
            }
            catch (Exception e)
            {
                WriteErrorMessage("Initialize", e.Message);
            }

            return false;
        }

        //初始化用户相关的信息（用户改变时重新调用）
        public bool InitInfoForUser(string strToken, string strTeacherID)
        {
            try
            {
                //if (string.IsNullOrEmpty(strToken) || string.IsNullOrEmpty(strTeacherID))
                //{
                //    return false;
                //}
                if ( string.IsNullOrEmpty(strTeacherID))
                {
                    return false;
                }
                mToken = strToken;
                mTeacherID = strTeacherID;
                //根据当前教师所属学校ID更新学校信息和云平台学科ID
                P_UpdateSchoolInfoByTeacher();

                return true;
            }
            catch (Exception e)
            {
                WriteErrorMessage("InitInfoForUser", e.Message);
            }

            return false;
        }

        //初始化学科相关的信息（学科改变时重新调用）
        public bool InitInfoForSubject(E_Subject eSubject)
        {
            try
            {
                WriteDebugInfo("InitInfoForSubject", "eSubject=" + eSubject.ToString());
                mSubject = eSubject;
                mMySubjectID = GetMySubjectID(mSubject);
                //设置云平台学科信息
                P_SetCloudPlatformSubject();

                #region 获取电子资源阅览室地址（原本与学科相关）
                CloudPlatformSubsystemM sysAddr = GetSubSysAddr(C_DigitalLibarySysID, null);
                if (sysAddr != null)
                {
                    mMultipleSubjectWebIPAndPort = sysAddr.WebSvrAddr;
                    mMultipleSubjectAPIIPAndPort = sysAddr.WsSvrAddr;
                }
                #endregion

                return true;
            }
            catch (Exception e)
            {
                WriteErrorMessage("InitInfoForSubject", e.Message);
            }

            return false;
        }
        //
        /// <summary>
        /// 初始化自定义学科
        /// </summary>
        /// <param name="cusSubjectId"></param>
        /// <param name="cusSubjectName"></param>
        /// <returns></returns>

        public bool InitInfoForCustomSubject(string cusSubjectId,string cusSubjectName)
        {
            try
            {
                WriteDebugInfo("InitInfoForCustomSubject", string.Format("cusSubjectId={0},cusSubjectName={1}", cusSubjectId, cusSubjectName));
                mCloudPlatformSubjectID = cusSubjectId;
                mCloudPlatformSubjectName = cusSubjectName;

                #region 获取电子资源阅览室地址（原本与学科相关）
                CloudPlatformSubsystemM sysAddr = GetSubSysAddr(C_DigitalLibarySysID, null);
                if (sysAddr != null)
                {
                    mMultipleSubjectWebIPAndPort = sysAddr.WebSvrAddr;
                    mMultipleSubjectAPIIPAndPort = sysAddr.WsSvrAddr;
                }
                #endregion

                return true;
            }
            catch (Exception e)
            {
                WriteErrorMessage("InitInfoForCustomSubject", e.Message);
            }

            return false;
        }
        //
        //为Web端提供的初始化接口
        public bool Initialize_BS(string strNetTeachIP, string strNetTeachPort, string strToken, string strTeacherID, E_Subject eSubject)
        {
            try
            {
                mNetTeachApiIP = strNetTeachIP;
                mNetTeachApiPort = strNetTeachPort;

                mApiBaseUrl = string.Format(Properties.Resources.TeachInfoUrl, strNetTeachIP, strNetTeachPort,mNetTeachApiVirDir);
                mCommandApi.BaseUrl = mApiBaseUrl;

                #region 初始化用户信息
                if (string.IsNullOrEmpty(strToken) || string.IsNullOrEmpty(strTeacherID))
                {
                    return false;
                }

                mToken = strToken;
                mTeacherID = strTeacherID;
                #endregion

                #region 初始化学科信息
                mSubject = eSubject;
                mMySubjectID = GetMySubjectID(mSubject);
                #endregion

                XmlDocument xmlDoc;
                string strWebServiceURL = "";
                XmlNodeList list;
                string strParam = "";
                string strReturn = "";
                int iCount;

                #region 获取云平台和本系统网站端地址
                strWebServiceURL = "http://" + mNetTeachApiIP + ":" + mNetTeachApiPort + "/"+ mNetTeachApiVirDir + "jxWebService.asmx/WS_G_BasicPFWebServerInfo";
                strParam = "strServerID=" + "";
                strReturn = mCommandWS.CallMethodPost(strWebServiceURL, strParam);

                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);
                list = xmlDoc.DocumentElement.GetElementsByTagName("anyType");
                iCount = 0;
                if (list != null)
                {
                    iCount = list.Count;
                }

                if (iCount > 0)
                {
                    for (int i = 0; i < iCount; i++)
                    {
                        XmlNode node = list[i];
                        string strID = node.ChildNodes[0].InnerText;//node.SelectSingleNode("ServerID").InnerText;
                        if (strID == "BasicPf")//云平台基础平台
                        {
                            mCloudPlatformBFIP = ClsParamsEncDec.LgMgr_ParamDecrypt(node.ChildNodes[2].InnerText);
                            mCloudPlatformBFPort = ClsParamsEncDec.LgMgr_ParamDecrypt(node.ChildNodes[7].InnerText);//node.SelectSingleNode("ServerPort").InnerText;
                            mCloudPlatformBFPhyPath = ClsParamsEncDec.LgMgr_ParamDecrypt(node.ChildNodes[6].InnerText);

                            mCloudPlatformBFUrl = mCloudPlatformBFIP;
                            if (string.IsNullOrEmpty(mCloudPlatformBFPort) == false)
                            {
                                mCloudPlatformBFUrl = mCloudPlatformBFUrl + ":" + mCloudPlatformBFPort;
                            }
                        }
                        else if (strID == "TeachWeb")//智能化语言实验室发布网站
                        {
                            m_strWebServerICIP = ClsParamsEncDec.LgMgr_ParamDecrypt(node.ChildNodes[2].InnerText);
                            string strPort = node.ChildNodes[7].InnerText;//node.SelectSingleNode("ServerPort").InnerText;
                            if (string.IsNullOrEmpty(strPort))
                            {
                                m_lngWebServerICPort = 0;
                            }
                            else
                            {
                                m_lngWebServerICPort = Convert.ToInt64(ClsParamsEncDec.LgMgr_ParamDecrypt(strPort));
                            }
                            m_strWebServerICPhyPath = ClsParamsEncDec.LgMgr_ParamDecrypt(node.ChildNodes[6].InnerText);
                        }
                        else if (strID == "WebSocket")
                        {
                            mWebSocketIP = ClsParamsEncDec.LgMgr_ParamDecrypt(node.ChildNodes[2].InnerText);
                            mWebSocketPort = ClsParamsEncDec.LgMgr_ParamDecrypt(node.ChildNodes[7].InnerText);
                        }
                    }
                }//end if(count>0)
                #endregion

                #region 获取学校信息和学期
                //根据当前教师所属学校ID更新学校信息和云平台学科ID
                P_UpdateSchoolInfoByTeacher();

                //获取学期
                mTermYear = P_GetTermInfo_WS();
                #endregion

                #region 获取资源库HTTP信息
                //获取资源库WS服务地址
                strWebServiceURL = "http://" + mNetTeachApiIP + ":" + mNetTeachApiPort + "/" + mNetTeachApiVirDir + "jxWebService.asmx/WS_G_OtherSysInfo ";
                strParam = "SysID=PubResource&SecCode=" + C_SecCode;
                strReturn = mCommandWS.CallMethodPost(strWebServiceURL, strParam);

                if (string.IsNullOrEmpty(strReturn))
                {
                    WriteErrorMessage("Initialize_BS", "获取资源库地址为空");
                }
                else
                {
                    xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(strReturn);
                    list = xmlDoc.DocumentElement.GetElementsByTagName("anyType");
                    iCount = 0;
                    if (list != null)
                    {
                        iCount = list.Count;
                    }
                    if (iCount == 1)
                    {
                        XmlNode node = list[0];
                        string strTempWSAddr = ClsParamsEncDec.LgMgr_ParamDecrypt(node.ChildNodes[1].InnerText);
                        if (string.IsNullOrEmpty(strTempWSAddr) == false)
                        {
                            strTempWSAddr = strTempWSAddr.Replace("http://", "").TrimEnd('/');
                            string[] arrTemp = strTempWSAddr.Split(':');

                            m_strZYWSIP = arrTemp[0];
                            if (string.IsNullOrEmpty(arrTemp[1]))
                            {
                                m_lngZYWSPort = 0;
                            }
                            else
                            {
                                //m_lngZYWSPort = Convert.ToInt64(arrTemp[1]);
                                string[] infoArr = arrTemp[1].Split('/');
                                m_lngZYWSPort = long.Parse(infoArr[0]);
                                if (infoArr.Length >= 2)
                                    m_strZYKWSVirDir = "/"+infoArr[1] ;
                                else
                                    m_strZYKWSVirDir = "";
                            }
                        }
                    }

                    //获取资源库HTTP信息
                    if (string.IsNullOrEmpty(m_strZYWSIP) == false)
                    {
                        strWebServiceURL = "http://" + m_strZYWSIP + ":" + m_lngZYWSPort + m_strZYKWSVirDir+ "/ZYK/Server.asmx/WS_G_ZYK_GetResHttpServer";
                        strParam = "";
                        strReturn = mCommandWS.CallMethodPost(strWebServiceURL, strParam);

                        xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(strReturn);
                        strReturn = xmlDoc.GetElementsByTagName("string")[0].InnerText;
                        strReturn = ClsParamsEncDec.LgMgr_ParamDecrypt(strReturn);

                        string[] arrReturn = strReturn.Split('|');
                        if (arrReturn.Length == 3)
                        {
                            m_strHttpIPAddress = arrReturn[0];
                            m_intHttpPort = string.IsNullOrEmpty(arrReturn[1]) ? 0 : Convert.ToInt32(arrReturn[1]);
                            m_strHttpName = arrReturn[2];
                        }
                        else
                        {
                            WriteErrorMessage("获取资源库HTTP信息", "HTTP信息返回值数组长度不为3");
                        }
                    }
                }
                #endregion

                #region 获取电子资源阅览室地址
                CloudPlatformSubsystemM sysAddr = GetSubSysAddr(C_DigitalLibarySysID, null);
                if (sysAddr != null)
                {
                    mMultipleSubjectWebIPAndPort = sysAddr.WebSvrAddr;
                    mMultipleSubjectAPIIPAndPort = sysAddr.WsSvrAddr;
                }
                #endregion

                return true;
            }
            catch (Exception e)
            {
                WriteErrorMessage("Initialize_BS", e.Message);
            }
            return false;
        }

        #endregion

        #region“按课表上课”相关

        //strLoginInfo登录返回的信息，成功则返回Token，失败则返回错误提示
        public int UserLogin(string strUserAccount, string strUserPwd, string strMachineType, string strLoginIP, string strMacAddress, out string strLoginContent)
        {
            WriteDebugInfo("UserLogin", strUserAccount + ", " + strUserPwd + ", " + strMachineType + ", " + strLoginIP + ", " + strMacAddress);
            strLoginContent = "";
            try
            {
                int iResult = 0;

                string strBrowserInfo = "";

                if (string.IsNullOrEmpty(strUserPwd))
                {
                    iResult = 3;
                    return iResult;
                }

                //MD5加密并反序
                strUserPwd = GetMd5Hash(strUserPwd);
                strUserPwd = Revert(strUserPwd);

                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append("http://" + mCloudPlatformBFUrl + "/UserMgr/Login/Api/Login.ashx");
                sbUrl.Append("?method=Login");
                //sbUrl.Append(string.Format("&params={0}|{1}|{2}|{3}|{4}|{5}|{6}", strUserAccount, strUserPwd, C_MySystemID, strMachineType, strLoginIP, strMacAddress, strBrowserInfo));
                sbUrl.Append(string.Format("&params={0}|{1}|{2}|{3}|{4}|{5}|{6}", strUserAccount, strUserPwd, m_curSysID, strMachineType, strLoginIP, strMacAddress, strBrowserInfo));//20180706 ldy
                string strReturn = mCommandApi.CallMethodGet(sbUrl.ToString());
                WriteDebugInfo("UserLogin", strReturn);
                int iFlag = UtilityClass.AnalyseCloudJson(ref strReturn);
                //此接口不需要token，按道理永远不会触发此事件
                if (iFlag == 3)
                {
                    if (EventInvalidToken != null)
                    {
                        EventInvalidToken();
                        iResult = 0;
                        return iResult;
                    }
                }
                LoginApiDataM data = JsonFormatter.JsonDeserialize<LoginApiDataM>(strReturn);
                strLoginContent = data.Token;
                WriteDebugInfo("UserLogin", strLoginContent);
                if (string.IsNullOrEmpty(data.Result) == false)
                {
                    iResult = Convert.ToInt32(data.Result);

                    if (iResult == 1)//登录成功时需要记录Token
                    {
                        mToken = data.Token;
                    }

                    WriteDebugInfo("UserLogin", iResult + ", Token=" + mToken);

                    return iResult;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("UserLogin", e.Message);
            }
            return 0;
        }

        //检查当前是否有用户在线
        public bool CheckUserOnline()
        {
            try
            {
                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append("http://" + mCloudPlatformBFUrl + "/UserMgr/Login/Api/Login.ashx");
                sbUrl.Append("?token=" + mToken);
                sbUrl.Append("&method=TokenCheck");
                //sbUrl.Append("&params=" + C_MySystemID);
                sbUrl.Append("&params=" + m_curSysID);//20180706 ldy
                string strReturn = mCommandApi.CallMethodGet(sbUrl.ToString());
                int iFlag = UtilityClass.AnalyseCloudJson(ref strReturn);
                if (iFlag == 3)
                {
                    if (EventInvalidToken != null)
                    {
                        EventInvalidToken();
                        return false;
                    }
                }
                LoginApiDataM data = JsonFormatter.JsonDeserialize<LoginApiDataM>(strReturn);
                if (data!=null&&string.IsNullOrEmpty(data.Result) == false)
                {
                    if (data.Result != null)
                    {
                        bool bReturn = Convert.ToBoolean(data.Result);
                        return bReturn;
                    }
                    else
                    {
                        WriteErrorMessage("CheckUserOnline", "data.Result为空");                        
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("CheckUserOnline", e.Message);
            }
            return false;
        }

        //退出登录
        public bool UserLogout()
        {
            try
            {
                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append("http://" + mCloudPlatformBFUrl + "/UserMgr/Login/Api/Login.ashx");
                sbUrl.Append("?token=" + mToken);
                sbUrl.Append("&method=Logout");
                //sbUrl.Append("&params=" + C_MySystemID);
                sbUrl.Append("&params=" + m_curSysID);//20180706 ldy
                WriteErrorMessage("UserLogout->", "1");
                string strReturn = mCommandApi.CallMethodGet(sbUrl.ToString());
                WriteErrorMessage("UserLogout->", "2");
                int iFlag = UtilityClass.AnalyseCloudJson(ref strReturn);
                WriteErrorMessage("UserLogout->", "3");
                if (iFlag == 3)
                {
                    if (EventInvalidToken != null)
                    {
                        EventInvalidToken();
                        return false;
                    }
                }
                WriteErrorMessage("UserLogout->", "4,sbUrl =" + sbUrl);
                LoginApiDataM data = JsonFormatter.JsonDeserialize<LoginApiDataM>(strReturn);
                WriteErrorMessage("UserLogout->", "5,strReturn = " + strReturn);
                if (data!=null&&string.IsNullOrEmpty(data.Result) == false)
                {
                    if (data.Result != null)
                    {
                        bool bReturn = Convert.ToBoolean(data.Result);
                        return bReturn;
                    }
                    else
                    {
                        WriteErrorMessage("UserLogout", "data.Result为空");
                    }
                }
                WriteErrorMessage("UserLogout->", "6");
            }
            catch (Exception e)
            {
                WriteErrorMessage("UserLogout", e.Message);
            }
            return false;
        }

        //获取当前登录用户的信息
        public LoginUserInfo GetOnlineUserInfo()
        {
            try
            {
                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append("http://" + mCloudPlatformBFUrl + "/UserMgr/Login/Api/Login.ashx");
                sbUrl.Append("?token=" + mToken);
                sbUrl.Append("&method=GetUserInfo");
                //sbUrl.Append("&params=" + C_MySystemID);
                sbUrl.Append("&params=" + m_curSysID);//20180706 ldy
                string strReturn = mCommandApi.CallMethodGet(sbUrl.ToString());
                int iFlag = UtilityClass.AnalyseCloudJson(ref strReturn);
                if (iFlag == 3)
                {
                    if (EventInvalidToken != null)
                    {
                        EventInvalidToken();
                        return null;
                    }
                }
                LoginUserInfo data = JsonFormatter.JsonDeserialize<LoginUserInfo>(strReturn);
                if (data != null)
                {
                    mTeacherID = data.UserID;
                }
                return data;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetUserInfo", e.Message);
            }
            return null;
        }

        //根据token获取在线用户的信息
        public LoginUserInfo GetOnlineUserInfo(string token)
        {
            try
            {
                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append("http://" + mCloudPlatformBFUrl + "/UserMgr/Login/Api/Login.ashx");
                sbUrl.Append("?token=" + token);
                sbUrl.Append("&method=GetUserInfo");
                //sbUrl.Append("&params=" + C_MySystemID);
                sbUrl.Append("&params=" + m_curSysID);//20180706 ldy
                string strReturn = mCommandApi.CallMethodGet(sbUrl.ToString());
                int iFlag = UtilityClass.AnalyseCloudJson(ref strReturn);
                if (iFlag == 3)
                {
                    if (EventInvalidToken != null)
                    {
                        EventInvalidToken();
                        return null;
                    }
                }
                LoginUserInfo data = JsonFormatter.JsonDeserialize<LoginUserInfo>(strReturn);
                if (data != null)
                {
                    mTeacherID = data.UserID;
                }
                return data;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetUserInfo", e.Message);
            }
            return null;
        }


        //根据输入的字符串参数，返回MD5加密后的字符串
        private string GetMd5Hash(string input)
        {
            try
            {
                System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
                byte[] data = md5.ComputeHash(Encoding.Default.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetMd5Hash", e.Message);
            }
            return "";
        }

        // 反序字符串
        private string Revert(string input)
        {
            try
            {
                if (string.IsNullOrEmpty(input))
                    return "";
                string md5Hash = input;
                StringBuilder sb = new StringBuilder();
                for (int i = md5Hash.Length - 1; i >= 0; i--)
                {
                    char ch = md5Hash[i];
                    sb.Append(ch);
                }
                return sb.ToString();
            }
            catch (Exception e)
            {
                WriteErrorMessage("Revert", e.Message);
            }
            return "";
        }



        //查询课表信息
        public ScheduleInfoM[] GetScheduleByNetClassRoom(string strClassroomID, DateTime dtLessonTime)
        {
            try
            {
                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append("http://" + mCloudPlatformBFUrl + "/UserMgr/TeachInfoMgr/Api/Service_TeachInfo.ashx");
                sbUrl.Append("?method=GetScheduleByNetClassRoom");
                string strLessonTime = dtLessonTime.ToString("yyyy-MM-dd HH:mm:ss");
                sbUrl.Append(string.Format("&params={0}|{1}", strClassroomID, strLessonTime));
                string strReturn = mCommandApi.CallMethodGet(sbUrl.ToString());
                int iFlag = UtilityClass.AnalyseCloudJson(ref strReturn);
                //此接口不需要token，按道理永远不会触发此事件
                if (iFlag == 3)
                {
                    if (EventInvalidToken != null)
                    {
                        EventInvalidToken();
                    }
                }
                if (string.IsNullOrEmpty(strReturn))
                {
                    return null;
                }

                ScheduleInfoM[] scheduleInfo = JsonFormatter.JsonDeserialize<ScheduleInfoM[]>(strReturn);
                return scheduleInfo;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetScheduleByNetClassRoom", e.Message);
            }
            return null;
        }

        //查询课表信息（WS接口）
        public ScheduleInfoM[] GetScheduleByNetClassRoom_WS(string strClassroomID, DateTime dtLessonTime)
        {
            try
            {
                string strWebServiceURL = "";
                string strParam = "";
                string strReturn = "";

                XmlDocument xmlDoc;

                strWebServiceURL = "http://{0}/UserMgr/TeachInfoMgr/WS/Service_TeachInfo.asmx/WS_UserMgr_G_GetScheduleByNetClassRoom";
                strParam = "ClassRoomID=" + strClassroomID + "&ClassDate=" + dtLessonTime.ToString("yyyy-MM-dd HH:mm:ss");
                strWebServiceURL = string.Format(strWebServiceURL, mCloudPlatformBFUrl);
                strReturn = mCommandWS.CallMethodPost(strWebServiceURL, strParam);

                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);
                XmlNodeList list = xmlDoc.DocumentElement.GetElementsByTagName("anyType");

                int iCount = 0;
                if (list != null)
                {
                    iCount = list.Count;
                }
                if (iCount > 0)
                {
                    ScheduleInfoM[] arrSchedule = new ScheduleInfoM[iCount];
                    for (int i = 0; i < iCount; i++)
                    {
                        XmlNode node = list[i];
                        ScheduleInfoM schedule = new ScheduleInfoM();
                        schedule.CalssDate = node.ChildNodes[0].InnerText;
                        schedule.StartTime = node.ChildNodes[1].InnerText;
                        schedule.EndTime = node.ChildNodes[2].InnerText;
                        schedule.TeacherID = node.ChildNodes[3].InnerText;
                        schedule.TeacherName = node.ChildNodes[4].InnerText;
                        schedule.TeacherPhoto = node.ChildNodes[5].InnerText;
                        schedule.CourseClassID = node.ChildNodes[6].InnerText;
                        schedule.CourseClassName = node.ChildNodes[7].InnerText;
                        schedule.CourseNo = node.ChildNodes[8].InnerText;
                        schedule.CourseName = node.ChildNodes[9].InnerText;
                        schedule.SubjectID = node.ChildNodes[10].InnerText;
                        schedule.SubjectName = node.ChildNodes[11].InnerText;
                        schedule.ScheduleID = node.ChildNodes[12].InnerText;
                        arrSchedule[i] = schedule;
                    }

                    return arrSchedule;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetScheduleByNetClassRoomWS", e.Message);
            }
            return null;
        }

        //判断课堂教学软件在启动时是否需要进行相关设备的检测
        public bool JudgeDeviceDetec()
        {
            try
            {
                string strWebServiceURL = "";
                string strParam = "";
                string strReturn = "";

                XmlDocument xmlDoc;

                strWebServiceURL = "http://{0}/ClassRoom/Service_ClassRoom.asmx/WS_G_CRMgr_JudgeDeviceDetectOnSystemStart";
                strParam = "schoolID=" + mSchoolID;
                strWebServiceURL = string.Format(strWebServiceURL, mCloudPlatformBFUrl);
                strReturn = mCommandWS.CallMethodPost(strWebServiceURL, strParam);

                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);
                XmlNodeList list = xmlDoc.DocumentElement.GetElementsByTagName("int");
                if (list != null && list.Count > 0)
                {
                    string strResult = list[0].InnerText;
                    if (strResult == "1")
                    {
                        return true;
                    }
                    else if (strResult == "0")
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("JudgeDeviceDetec", e.Message);
            }

            return false;
        }

        #endregion

        #region 基础信息相关
        /// <summary>
        /// 根据学校ID获取学校信息
        /// </summary>
        /// <param name="strSchoolID">学校ID</param>
        /// <returns></returns>
        public SchoolBaseInfoM GetSchoolInfoByID(string strSchoolID)
        {
            try
            {
                if (string.IsNullOrEmpty(strSchoolID))
                {
                    return null;
                }

                string strWebServiceURL = "";
                string strParam = "";
                string strReturn = "";
                XmlDocument xmlDoc;

                strWebServiceURL = "http://{0}/SysMgr/SysSetting/WS/Service_SysSetting.asmx/WS_SysMgr_G_GetSchoolBaseInfo";
                strParam = "schoolID=" + strSchoolID;
                strWebServiceURL = string.Format(strWebServiceURL, mCloudPlatformBFUrl);
                strReturn = mCommandWS.CallMethodPost(strWebServiceURL, strParam);

                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);
                XmlNodeList list = xmlDoc.GetElementsByTagName("anyType");

                int iCount = 0;
                if (list != null)
                {
                    iCount = list.Count;
                }

                SchoolBaseInfoM school = null;
                if (iCount == 1)
                {
                    school = new SchoolBaseInfoM();
                    school.SchoolID = list[0].ChildNodes[0].InnerText;
                    school.SchoolName = list[0].ChildNodes[1].InnerText;
                    school.SchoolCode = list[0].ChildNodes[2].InnerText;
                    school.SchoolLevel = list[0].ChildNodes[3].InnerText;
                    school.SchoolType = list[0].ChildNodes[4].InnerText;
                    school.SchoolState = list[0].ChildNodes[5].InnerText;
                    school.CreateTime = list[0].ChildNodes[6].InnerText;
                }

                return school;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetSchoolInfoByID", e.Message);
            }
            return null;
        }

        public bool GetCloudPlatformSubject(out string strSubjectID, out string strSubjectName)
        {
            strSubjectID = "";
            strSubjectName = "";
            try
            {
                GetSubjectIDFromCloudPlatform(mSubject);
                strSubjectID = mCloudPlatformSubjectID;
                strSubjectName = mCloudPlatformSubjectName;
                return true;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetCloudPlatformSubject", e.Message);
            }
            return false;
        }

        /// <summary>
        /// 获取当前教师的信息（简略版）
        /// </summary>
        /// <returns></returns>
        public TeacherInfoSimpleM GetTeacherInfoSimple()
        {
            try
            {
                string strTeacherInfo = P_GetTeacherInfo(1);

                if (string.IsNullOrEmpty(strTeacherInfo))
                {
                    return null;
                }

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strTeacherInfo);
                XmlNodeList list = xmlDoc.GetElementsByTagName("anyType");

                if (list == null || list.Count == 0)
                {
                    return null;
                }

                TeacherInfoSimpleM teacherInfo = new TeacherInfoSimpleM();
                teacherInfo.UserID = list[0].ChildNodes[0].InnerText;
                teacherInfo.UserName = list[0].ChildNodes[1].InnerText;
                teacherInfo.Gender = list[0].ChildNodes[2].InnerText;
                teacherInfo.GroupID = list[0].ChildNodes[3].InnerText;
                teacherInfo.PhotoPath = list[0].ChildNodes[4].InnerText;

                return teacherInfo;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetTeacherInfoSimple", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 获取当前教师的信息（详细版）
        /// </summary>
        /// <returns></returns>
        public TeacherInfoDetailM GetTeacherInfoDetail()
        {
            try
            {
                string strTeacherInfo = P_GetTeacherInfo(2);

                if (string.IsNullOrEmpty(strTeacherInfo))
                {
                    return null;
                }

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strTeacherInfo);
                XmlNodeList list = xmlDoc.GetElementsByTagName("anyType");

                if (list == null || list.Count == 0)
                {
                    return null;
                }

                TeacherInfoDetailM teacherInfo = new TeacherInfoDetailM();
                teacherInfo.UserID = list[0].ChildNodes[0].InnerText;
                teacherInfo.ShortName = list[0].ChildNodes[1].InnerText;
                teacherInfo.UserName = list[0].ChildNodes[2].InnerText;
                teacherInfo.GroupID = list[0].ChildNodes[3].InnerText;
                teacherInfo.GroupName = list[0].ChildNodes[4].InnerText;
                teacherInfo.TitileName = list[0].ChildNodes[5].InnerText;
                teacherInfo.RoleNames = list[0].ChildNodes[6].InnerText;
                teacherInfo.SchoolID = list[0].ChildNodes[7].InnerText;
                teacherInfo.UserClass = list[0].ChildNodes[8].InnerText;
                teacherInfo.Gender = list[0].ChildNodes[9].InnerText;
                teacherInfo.Email = list[0].ChildNodes[10].InnerText;
                teacherInfo.Telephone = list[0].ChildNodes[11].InnerText;
                teacherInfo.PhotoPath = list[0].ChildNodes[12].InnerText;
                teacherInfo.Sign = list[0].ChildNodes[13].InnerText;
                teacherInfo.QQ = list[0].ChildNodes[14].InnerText;
                teacherInfo.Weibo = list[0].ChildNodes[15].InnerText;
                teacherInfo.Weixin = list[0].ChildNodes[16].InnerText;
                teacherInfo.Renren = list[0].ChildNodes[17].InnerText;
                teacherInfo.UpdateTime = list[0].ChildNodes[18].InnerText;

                return teacherInfo;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetTeacherInfoDetail", e.Message);
            }
            return null;
        }

        //DataModel（返回的数据模型）：1-简略版，2-详细版
        private string P_GetTeacherInfo(int DataModel)
        {
            try
            {
                string strWebServiceURL = "";
                string strParam = "";
                string strReturn = "";

                strWebServiceURL = "http://{0}/UserMgr/UserInfoMgr/WS/Service_UserInfo.asmx/WS_UserMgr_G_GetTeacher";
                strParam = "Token=" + mToken + "&UserID=" + mTeacherID + "&UserName=&GroupID="
                    + "&UserClass=&SchoolID=&UpdateTime=&DataModel=";
                switch (DataModel)
                {
                    case 1:
                        strParam = strParam + "base";
                        break;
                    case 2:
                        strParam = strParam + "all";
                        break;
                    default:
                        strParam = strParam + "base";
                        break;
                }
                strWebServiceURL = string.Format(strWebServiceURL, mCloudPlatformBFUrl);
                strReturn = mCommandWS.CallMethodPost(strWebServiceURL, strParam);
                return strReturn;
            }
            catch (Exception e)
            {
                WriteErrorMessage("P_GetTeacherInfo", e.Message);
            }

            return "";
        }

        /// <summary>
        /// 获取云平台下子系统的地址（内部自动使用云平台学科ID获取）
        /// </summary>
        /// <param name="strSysID">系统ID</param>
        /// <returns>返回值中的地址已删掉头部的“http://”和尾部的“/”</returns>
        public CloudPlatformSubsystemM GetSubSysAddr(string strSysID)
        {
            string strSubjectID = mCloudPlatformSubjectID;
            //若是以下系统，则强制不根据学科来获取
            switch (strSysID)
            {
                case "S10":
                case "S14":
                case "S20":
                case "S30":
                case "S40":
                case "S50":
                case "E00":
                case "D00":
                case "630":
                case "510":
                case "910":
                    strSubjectID = "";
                    break;
            }
            CloudPlatformSubsystemM subAddr = GetSubSysAddr(strSysID, strSubjectID);
            return subAddr;
        }

        /// <summary>
        /// 获取云平台下子系统的地址（可选择性使用云平台学科ID获取）
        /// </summary>
        /// <param name="strSysID">系统ID</param>
        /// <param name="strSubjectID">云平台学科ID</param>
        /// <returns>返回值中的地址已删掉头部的“http://”和尾部的“/”</returns>
        public CloudPlatformSubsystemM GetSubSysAddr(string strSysID, string strSubjectID)
        {
            try
            {
                WriteTrackLog("GetSubSysAddr", String.Format("strSysID={0},strSubjectID={1},mCloudPlatformBFUrl={2}", strSysID, strSubjectID, mCloudPlatformBFUrl));
                if (string.IsNullOrEmpty(strSysID) || string.IsNullOrEmpty(mCloudPlatformBFUrl))
                {
                    return null;
                }

                string strWholeUrl = "";
                string strParam = "";
                string strReturn = "";
                XmlDocument xmlDoc = null;
                CloudPlatformSubsystemM sysAddr = new CloudPlatformSubsystemM();

                //若是以下系统，则强制不根据学科来获取
                switch (strSysID)
                {
                    case "S10":
                    case "S20":
                    case "S30":
                    case "S40":
                    case "S50":
                    case "E00":
                        strSubjectID = "";
                        break;
                }
                //学科ID为空则使用全学科的接口获取，然后取第一个
                if (string.IsNullOrEmpty(strSubjectID))
                {
                    strWholeUrl = "http://" + mCloudPlatformBFUrl + "/Base/WS/Service_Basic.asmx/WS_G_GetSubSystemServerInfoForAllSubject";
                    strParam = "sysID={0}";
                    strParam = string.Format(strParam, strSysID);
                    strReturn = mCommandWS.CallMethodPost(strWholeUrl, strParam);
                    if (string.IsNullOrEmpty(strReturn))
                    {
                        return null;
                    }
                    xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(strReturn);
                    XmlNodeList list = xmlDoc.GetElementsByTagName("anyType");
                    if (list == null || list.Count == 0)
                    {
                        return null;
                    }

                    sysAddr.SysID = list[0].ChildNodes[0].InnerText;
                    sysAddr.SubjectID = list[0].ChildNodes[1].InnerText;
                    sysAddr.WebSvrAddr = list[0].ChildNodes[2].InnerText;
                    sysAddr.WsSvrAddr = list[0].ChildNodes[3].InnerText;
                }
                //学科ID不为空，则使用学科相关的接口获取
                else
                {
                    strWholeUrl = "http://" + mCloudPlatformBFUrl + "/Base/WS/Service_Basic.asmx/WS_G_GetSubSystemServerInfo";
                    strParam = "sysID={0}&subjectID={1}";
                    strParam = string.Format(strParam, strSysID, strSubjectID);
                    strReturn = mCommandWS.CallMethodPost(strWholeUrl, strParam);
                    if (string.IsNullOrEmpty(strReturn))
                    {
                        return null;
                    }
                    xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(strReturn);
                    XmlNodeList list = xmlDoc.GetElementsByTagName("string");
                    if (list == null || list.Count == 0)
                    {
                        return null;
                    }

                    sysAddr.SysID = list[0].InnerText;
                    sysAddr.SubjectID = list[1].InnerText;
                    sysAddr.WebSvrAddr = list[2].InnerText;
                    sysAddr.WsSvrAddr = list[3].InnerText;
                }

                if (string.IsNullOrEmpty(sysAddr.SysID))
                {
                    return null;
                }

                if (string.IsNullOrEmpty(sysAddr.WebSvrAddr) == false)
                {
                    sysAddr.WebSvrAddr = sysAddr.WebSvrAddr.Replace("http://", "").TrimEnd('/');
                }
                if (string.IsNullOrEmpty(sysAddr.WsSvrAddr) == false)
                {
                    sysAddr.WsSvrAddr = sysAddr.WsSvrAddr.Replace("http://", "").TrimEnd('/');
                }

                return sysAddr;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetSubSysAddr", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 获取云平台下子系统Web访问地址（不使用云平台学科ID获取，即按通用学科方式获取）
        /// </summary>
        /// <param name="strSysID">系统ID</param>
        /// <returns>返回值中的地址已删掉头部的“http://”和尾部的“/”</returns>
        public string GetSubSysWebIPandPort(string strSysID)
        {
            try
            {
                CloudPlatformSubsystemM sysAddr = GetSubSysAddr(strSysID, null);
                if (sysAddr != null)
                {
                    return sysAddr.WebSvrAddr;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetSubSysWebIPandPort", e.Message);
            }
            return "";
        }

        /// <summary>
        /// 获取云平台下子系统接口的地址（不使用云平台学科ID获取）
        /// </summary>
        /// <param name="strSysID">系统ID</param>
        /// <returns>返回值中的地址已删掉头部的“http://”和尾部的“/”</returns>
        public string GetSubSysApiIPandPort(string strSysID)
        {
            try
            {
                CloudPlatformSubsystemM sysAddr = GetSubSysAddr(strSysID, null);
                if (sysAddr != null)
                {
                    return sysAddr.WsSvrAddr;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetSubSysApiIPandPort", e.Message);
            }
            return "";
        }

        //public string GetA00HttpServerInfo(string sInteligentWsIp,string sInteligentWsPort)
        //{
        //    try
        //    {
        //        strWholeUrl = "http://" + sInteligentWsIp + ":" + sInteligentWsPort + "/SearchStatisticalInfo.asmx//WS_Search_GetServerAddressConf";
        //        strParam = "sysID={0}";
        //        strParam = string.Format(strParam, strSysID);
        //        strReturn = mCommandWS.CallMethodPost(strWholeUrl, strParam);
        //        if (string.IsNullOrEmpty(strReturn))
        //        {
        //            return null;
        //        }
        //        xmlDoc = new XmlDocument();
        //        xmlDoc.LoadXml(strReturn);
        //        XmlNodeList list = xmlDoc.GetElementsByTagName("Server");
        //        if (list == null || list.Count == 0)
        //        {
        //            return null;
        //        }
        //    }
        //    catch(Exception e)
        //    {
        //        WriteErrorMessage("GetA00HttpServerInfo", e.Message);
        //    }
        //}

        //获取当前学校有哪些教室，复杂类型
        public ClassroomInfoM[] GetClassroomInfo_Total()
        {
            try
            {
                string strWebServiceURL = "";
                string strParam = "";
                string strReturn = "";

                XmlDocument xmlDoc;

                strWebServiceURL = "http://{0}/ClassRoom/Service_ClassRoom.asmx/WS_Basic_CR_GetClassRoomInfoByID";
                strParam = "schoolID=" + mSchoolID;
                strWebServiceURL = string.Format(strWebServiceURL, mCloudPlatformBFUrl);
                strReturn = mCommandWS.CallMethodPost(strWebServiceURL, strParam);

                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);
                XmlNodeList list = xmlDoc.DocumentElement.GetElementsByTagName("anyType");
                
                int iCount = 0;
                if (list != null)
                {
                    iCount = list.Count;
                }
                if (iCount > 0)
                {
                    ClassroomInfoM[] info = new ClassroomInfoM[iCount];
                    for (int i = 0; i < iCount; i++)
                    {
                        ClassroomInfoM c = new ClassroomInfoM();
                        XmlNode node = list[i];

                        c.ID = node.ChildNodes[0].InnerText;
                        c.CRName = node.ChildNodes[1].InnerText;
                        c.TMIP = node.ChildNodes[2].InnerText;
                        c.TMMac = node.ChildNodes[3].InnerText;
                        c.SeatRowCount = node.ChildNodes[4].InnerText;
                        c.SeatColCount = node.ChildNodes[5].InnerText;
                        c.SeatArrangeDirect = node.ChildNodes[6].InnerText;
                        c.IPRange = node.ChildNodes[7].InnerText;
                        DateTime dt;
                        if (DateTime.TryParse(node.ChildNodes[8].InnerText, out dt))
                        {
                            c.UpdateTime = dt;
                        }
                        info[i] = c;
                    }

                    return info;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetClassroomInfo_Total", e.Message);
            }

            return null;
        }

        //获取当前学校有哪些教室，简单类型
        public bool GetClassroomInfo_Simple(out string[] arrID, out string[] arrName)
        {
            arrID = null;
            arrName = null;
            try
            {
                string strWebServiceURL = "";
                string strParam = "";
                string strReturn = "";

                XmlDocument xmlDoc;

                strWebServiceURL = "http://{0}/ClassRoom/Service_ClassRoom.asmx/WS_Basic_CR_GetClassRoomInfoByID";
                strParam = "schoolID=" + mSchoolID;
                strWebServiceURL = string.Format(strWebServiceURL, mCloudPlatformBFUrl);
                strReturn = mCommandWS.CallMethodPost(strWebServiceURL, strParam);

                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);
                XmlNodeList list = xmlDoc.DocumentElement.GetElementsByTagName("anyType");

                int iCount = 0;
                if (list != null)
                {
                    iCount = list.Count;
                }
                if (iCount > 0)
                {
                    arrID = new string[iCount];
                    arrName = new string[iCount];

                    for (int i = 0; i < iCount; i++)
                    {
                        XmlNode node = list[i];

                        arrID[i] = node.ChildNodes[0].InnerText;
                        arrName[i] = node.ChildNodes[1].InnerText;
                    }

                    return true;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetClassroomInfo_Simple", e.Message);
            }

            return false;
        }

        //（新版）获取当前学校有哪些教室，复杂类型
        public ClassroomInfoM[] GetClassroomInfo_Total2()
        {
            try
            {
                string strWebServiceURL = "";
                string strParam = "";
                string strReturn = "";

                XmlDocument xmlDoc;

                if (string.IsNullOrEmpty(mCMWSIPandPort))
                {
                    CloudPlatformSubsystemM sys = GetSubSysAddr(C_ClassroomManagerSysID);
                    if (sys == null)
                    {
                        return null;
                    }
                    else
                    {
                        mCMWSIPandPort = sys.WsSvrAddr;
                    }
                }

                strWebServiceURL = "http://{0}/WebService.asmx/WS_G_CRMgr_GetClassroomInfoBySchoolID";
                strParam = "schoolID=" + mSchoolID;
                strWebServiceURL = string.Format(strWebServiceURL, mCMWSIPandPort);
                strReturn = mCommandWS.CallMethodPost(strWebServiceURL, strParam);

                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);
                XmlNodeList list = xmlDoc.DocumentElement.GetElementsByTagName("NetClassRoomInfo");

                int iCount = 0;
                if (list != null)
                {
                    iCount = list.Count;
                }
                if (iCount > 0)
                {
                    ClassroomInfoM[] info = new ClassroomInfoM[iCount];
                    for (int i = 0; i < iCount; i++)
                    {
                        ClassroomInfoM c = new ClassroomInfoM();
                        XmlNode node = list[i];

                        c.ID = node.ChildNodes[0].InnerText;
                        c.CRName = node.ChildNodes[1].InnerText;
                        c.TMIP = node.ChildNodes[2].InnerText;
                        c.TMMac = node.ChildNodes[3].InnerText;
                        c.SeatRowCount = node.ChildNodes[4].InnerText;
                        c.SeatColCount = node.ChildNodes[5].InnerText;
                        c.SeatArrangeDirect = node.ChildNodes[6].InnerText;
                        c.IPRange = node.ChildNodes[7].InnerText;
                        DateTime dt;
                        if (DateTime.TryParse(node.ChildNodes[8].InnerText, out dt))
                        {
                            c.UpdateTime = dt;
                        }
                        info[i] = c;
                    }

                    return info;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetClassroomInfo_Total2", e.Message);
            }

            return null;
        }

        //（新版）获取当前学校有哪些教室，简单类型
        public bool GetClassroomInfo_Simple2(out string[] arrID, out string[] arrName)
        {
            arrID = null;
            arrName = null;
            try
            {
                ClassroomInfoM[] list = GetClassroomInfo_Total2();
                int iCount = 0;
                if (list != null)
                {
                    iCount = list.Length;
                }
                if (iCount > 0)
                {
                    arrID = new string[iCount];
                    arrName = new string[iCount];

                    for (int i = 0; i < iCount; i++)
                    {
                        arrID[i] = list[i].ID;
                        arrName[i] = list[i].CRName;
                    }

                    return true;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetClassroomInfo_Simple2", e.Message);
            }

            return false;
        }

        //获取当前系统的年级信息
        public int GetGradeInfo(out string[] arrGradeID, out string[] arrGradeName)
        {
            arrGradeID = null;
            arrGradeName = null;
            try
            {
                string strWholeUrl = "";
                string strUrl = "";
                string strMethod = "";
                string strParam = "";
                string[] arrParam = null;
                string strReturn = "";

                strWholeUrl = "http://{0}/UserMgr/UserInfoMgr/API/Service_UserInfo.ashx?token={1}&method={2}{3}";
                strUrl = mCloudPlatformBFUrl;
                strMethod = "GetGrade";
                strParam = "&params=|{0}";//说明文档为3个参数，有误
                strParam = string.Format(strParam, mSchoolID);
                arrParam = new string[4];
                arrParam[0] = strUrl;
                arrParam[1] = mToken;
                arrParam[2] = strMethod;
                arrParam[3] = strParam;
                strWholeUrl = string.Format(strWholeUrl, arrParam);
                strReturn = mCommandApi.CallMethodGet(strWholeUrl);
                int iFlag = UtilityClass.AnalyseCloudJson(ref strReturn);
                if (iFlag == 3)
                {
                    if (EventInvalidToken != null)
                    {
                        EventInvalidToken();
                        return 0;
                    }
                }
                GradeInfoM[] grade = JsonFormatter.JsonDeserialize<GradeInfoM[]>(strReturn);

                if (grade != null)
                {
                    int iCount = grade.Length;
                    if (iCount > 0)
                    {
                        arrGradeID = new string[iCount];
                        arrGradeName = new string[iCount];

                        for (int i = 0; i < iCount; i++)
                        {
                            arrGradeID[i] = Uri.UnescapeDataString(grade[i].GradeID);
                            arrGradeName[i] = Uri.UnescapeDataString(grade[i].GradeName);
                        }
                    }

                    return iCount;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetGradeInfo", e.Message);
            }

            return 0;
        }

        //获取某一年级下有哪些班级（行政班）
        public int GetClassInfoByGradeID(string strGradeID, out string[] arrClassID, out string[] arrClassName, out int[] arrStudentNum)
        {
            arrClassID = null;
            arrClassName = null;
            arrStudentNum = null;
            try
            {
                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append("http://" + mCloudPlatformBFUrl + "/UserMgr/UserInfoMgr/API/Service_UserInfo.ashx");
                sbUrl.Append("?token=" + mToken);
                sbUrl.Append("&method=GetClass");
                sbUrl.Append(string.Format("&params=|{0}||", strGradeID));
                string strReturn = mCommandApi.CallMethodGet(sbUrl.ToString());
                int iFlag = UtilityClass.AnalyseCloudJson(ref strReturn);
                if (iFlag == 3)
                {
                    if (EventInvalidToken != null)
                    {
                        EventInvalidToken();
                        return 0;
                    }
                }
                ClassInfoM[] classInfo = JsonFormatter.JsonDeserialize<ClassInfoM[]>(strReturn);

                if (classInfo != null)
                {
                    int iCount = classInfo.Length;
                    if (iCount > 0)
                    {
                        arrClassID = new string[iCount];
                        arrClassName = new string[iCount];
                        arrStudentNum = new int[iCount];

                        for (int i = 0; i < iCount; i++)
                        {
                            arrClassID[i] = Uri.UnescapeDataString(classInfo[i].ClassID);
                            arrClassName[i] = Uri.UnescapeDataString(classInfo[i].ClassName);
                            StudentInfoSimpleM[] stu = GetStudentInfo_Simple("", arrClassID[i], "");
                            if (stu != null)
                            {
                                arrStudentNum[i] = stu.Length;
                            }
                            else
                            {
                                arrStudentNum[i] = 0;
                            }
                        }
                    }

                    return iCount;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetClassInfoByGradeID", e.Message);
            }

            return 0;
        }

        //根据班级ID，获取班级的属性信息（行政班）
        public bool GetClassInfoByClassID(string strClassID, out string strClassName, out string strGradeID, out string strGradeName)
        {
            strClassName = "";
            strGradeID = "";
            strGradeName = "";
            try
            {
                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append("http://" + mCloudPlatformBFUrl + "/UserMgr/UserInfoMgr/API/Service_UserInfo.ashx");
                sbUrl.Append("?token=" + mToken);
                sbUrl.Append("&method=GetClass");
                sbUrl.Append(string.Format("&params={0}|||", strClassID));
                string strReturn = mCommandApi.CallMethodGet(sbUrl.ToString());
                int iFlag = UtilityClass.AnalyseCloudJson(ref strReturn);
                if (iFlag == 3)
                {
                    if (EventInvalidToken != null)
                    {
                        EventInvalidToken();
                        return false;
                    }
                }
                ClassInfoM[] classInfo = JsonFormatter.JsonDeserialize<ClassInfoM[]>(strReturn);

                if (classInfo != null && classInfo.Length == 1)
                {
                    strClassName = classInfo[0].ClassName;
                    strGradeID = classInfo[0].GradeID;

                    GradeInfoM[] grade = P_GetGradeInfo(strGradeID);
                    if (grade != null && grade.Length == 1)
                    {
                        strGradeName = grade[0].GradeName;
                    }
                    else
                    {
                        strGradeName = "";
                    }

                    return true;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetClassInfoByClassID", e.Message);
            }

            return false;
        }

        //根据行政班级ID，获取班级下所有学生的详细信息
        public int GetStudentInfoByClassID(string strClassID, out string[] arrXH, out string[] arrName, out string[] arrGender, out string[] arrPhotoPath)
        {
            arrXH = null;
            arrName = null;
            arrGender = null;
            arrPhotoPath = null;
            try
            {
                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append("http://" + mCloudPlatformBFUrl + "/UserMgr/UserInfoMgr/API/Service_UserInfo.ashx");
                sbUrl.Append("?token=" + mToken);
                sbUrl.Append("&method=GetStudent");
                sbUrl.Append(string.Format("&params=||{0}||||||base", strClassID));
                string strReturn = mCommandApi.CallMethodGet(sbUrl.ToString());
                int iFlag = UtilityClass.AnalyseCloudJson(ref strReturn);
                if (iFlag == 3)
                {
                    if (EventInvalidToken != null)
                    {
                        EventInvalidToken();
                        return 0;
                    }
                }
                StudentInfoSimpleM[] stuInfo = JsonFormatter.JsonDeserialize<StudentInfoSimpleM[]>(strReturn);

                int iCount = 0;
                if (stuInfo != null)
                {
                    iCount = stuInfo.Length;
                }

                if (iCount > 0)
                {
                    arrXH = new string[iCount];
                    arrName = new string[iCount];
                    arrGender = new string[iCount];
                    arrPhotoPath = new string[iCount];

                    for (int i = 0; i < iCount; i++)
                    {
                        arrXH[i] = stuInfo[i].UserID;
                        arrName[i] = stuInfo[i].UserName;
                        arrGender[i] = stuInfo[i].Gender;
                        arrPhotoPath[i] = stuInfo[i].PhotoPath;
                    }

                    return iCount;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetStudentInfoByClassID", e.Message);
            }

            return 0;
        }

        //根据班级ID（行政班），获取该班的学生人数
        public int GetStudentNumByClassID(string strClassID)
        {
            try
            {
                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append("http://" + mCloudPlatformBFUrl + "/UserMgr/UserInfoMgr/API/Service_UserInfo.ashx");
                sbUrl.Append("?token=" + mToken);
                sbUrl.Append("&method=GetStudent");
                sbUrl.Append(string.Format("&params=||{0}||||||base", strClassID));
                string strReturn = mCommandApi.CallMethodGet(sbUrl.ToString());
                int iFlag = UtilityClass.AnalyseCloudJson(ref strReturn);
                if (iFlag == 3)
                {
                    if (EventInvalidToken != null)
                    {
                        EventInvalidToken();
                        return 0;
                    }
                }
                StudentInfoSimpleM[] stuInfo = JsonFormatter.JsonDeserialize<StudentInfoSimpleM[]>(strReturn);

                if (stuInfo != null)
                {
                    return stuInfo.Length;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("BF_StudentNumByClassID", e.Message);
            }

            return 0;
        }

        /// <summary>
        /// 根据教师TeacherID，获取该教师所带的课程班信息
        /// </summary>
        /// <param name="arrCourseNo">课程ID</param>
        /// <param name="arrCourseName">课堂名称</param>
        /// <param name="arrCourseClassID">课程班ID</param>
        /// <param name="arrCourseClassName">课程班名称</param>
        /// <param name="arrSubjectID">学科ID</param>
        /// <param name="arrStudyLevelID">学习阶段ID</param>
        /// <param name="arrStudyLevelName">学习阶段名称</param>
        /// <param name="arrGlobalGrade">年级标识，K1~K12（一年级至高中三年级），U1~U5(大一至大五)，M1~M3（中职一至中职三），H1~H5（高职一至高职五）</param>
        /// <returns>课程班个数</returns>
        public int GetCourseClassInfoByGH(out string[] arrCourseNo, out string[] arrCourseName, out string[] arrCourseClassID, out string[] arrCourseClassName,out string[] arrSubjectID,out string[] arrStudyLevelID,out string[] arrStudyLevelName,out string[] arrGlobalGrade)
        {
            arrCourseNo = null;
            arrCourseName = null;
            arrCourseClassID = null;
            arrCourseClassName = null;
            arrSubjectID = null;
            arrStudyLevelID = null;
            arrStudyLevelName = null;
            arrGlobalGrade = null;
            try
            {
                CourseClassInfoExM[] courseClass1 = P_GetCourseClassByUser();
                CourseClassInfoM[] courseClass2 = P_GetCourseClassBySubject();
                if (courseClass1 == null)
                    WriteErrorMessage("GetCourseClassInfoByGH", "用户所带的教学班为空");
                if (courseClass2 == null)
                    WriteErrorMessage("GetCourseClassInfoByGH", "学科下的教学班为空");
                if (courseClass1 == null || courseClass2 == null)
                {
                    return 0;
                }
                List<CourseClassInfoExM> list = new List<CourseClassInfoExM>();
                for (int i = 0; i < courseClass1.Length; i++)
                {
                    for (int j = 0; j < courseClass2.Length; j++)
                    {
                        if (courseClass1[i].CourseClassID == courseClass2[j].CourseClassID)
                        {
                            list.Add(courseClass1[i]);
                            break;
                        }
                    }
                }

                if (list != null)
                {
                    int iCount = list.Count;
                    if (iCount > 0)
                    {
                        arrCourseNo = new string[iCount];
                        arrCourseName = new string[iCount];
                        arrCourseClassID = new string[iCount];
                        arrCourseClassName = new string[iCount];
                        arrSubjectID = new string[iCount];
                        arrStudyLevelID = new string[iCount];
                        arrStudyLevelName = new string[iCount];
                        arrGlobalGrade = new string[iCount];

                        for (int i = 0; i < iCount; i++)
                        {
                            arrCourseNo[i] = list[i].CourseNo;
                            arrCourseName[i] = list[i].CourseName;
                            arrCourseClassID[i] = list[i].CourseClassID;
                            arrCourseClassName[i] = list[i].CourseClassName;
                            arrSubjectID[i] = list[i].SubjectID;
                            arrStudyLevelID[i] = list[i].StudyLevelID;
                            arrStudyLevelName[i] = list[i].StudyLevelName;
                            arrGlobalGrade[i] = list[i].GlobalGrade;
                        }
                    }
                    else
                    {
                        WriteTrackLog("GetCourseClassInfoByGH", "教学班数量为0");
                    }
                    return iCount;
                }
                else 
                {
                    WriteTrackLog("GetCourseClassInfoByGH",JsonFormatter.JsonSerialize(list));
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetCourseClassInfoByGH", e.Message);
            }

            return 0;
        }
        /// <summary>
        /// 根据教师TeacherID，获取该教师所带的课程班信息
        /// </summary>
        /// <param name="arrCourseNo">课程ID</param>
        /// <param name="arrCourseName">课堂名称</param>
        /// <param name="arrCourseClassID">课程班ID</param>
        /// <param name="arrCourseClassName">课程班名称</param>
        /// <param name="arrSubjectID">学科ID</param>
        /// <param name="arrStudyLevelID">学习阶段ID</param>
        /// <param name="arrStudyLevelName">学习阶段名称</param>
        /// <returns>课程班个数</returns>
        public int GetCourseClassInfoOfTeacher(out string[] arrCourseNo, out string[] arrCourseName, out string[] arrCourseClassID, out string[] arrCourseClassName, out string[] arrSubjectID, out string[] arrStudyLevelID, out string[] arrStudyLevelName)
        {
            arrCourseNo = null;
            arrCourseName = null;
            arrCourseClassID = null;
            arrCourseClassName = null;
            arrSubjectID = null;
            arrStudyLevelID = null;
            arrStudyLevelName = null;
            try
            {
                CourseClassInfoExM[] courseClass1 = P_GetCourseClassByUser();
              
                if (courseClass1 == null)
                    WriteErrorMessage("GetCourseClassInfoByGH", "用户所带的教学班为空");
               
                if (courseClass1 == null )
                {
                    return 0;
                }
                List<CourseClassInfoExM> list = new List<CourseClassInfoExM>(courseClass1);
                if (list != null)
                {
                    int iCount = list.Count;
                    if (iCount > 0)
                    {
                        arrCourseNo = new string[iCount];
                        arrCourseName = new string[iCount];
                        arrCourseClassID = new string[iCount];
                        arrCourseClassName = new string[iCount];
                        arrSubjectID = new string[iCount];
                        arrStudyLevelID = new string[iCount];
                        arrStudyLevelName = new string[iCount];

                        for (int i = 0; i < iCount; i++)
                        {
                            arrCourseNo[i] = list[i].CourseNo;
                            arrCourseName[i] = list[i].CourseName;
                            arrCourseClassID[i] = list[i].CourseClassID;
                            arrCourseClassName[i] = list[i].CourseClassName;
                            arrSubjectID[i] = list[i].SubjectID;
                            arrStudyLevelID[i] = list[i].StudyLevelID;
                            arrStudyLevelName[i] = list[i].StudyLevelName;
                        }
                    }
                    else
                    {
                        WriteTrackLog("GetCourseClassInfoByGH", "教学班数量为0");
                    }
                    return iCount;
                }
                else
                {
                    WriteTrackLog("GetCourseClassInfoByGH", JsonFormatter.JsonSerialize(list));
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetCourseClassInfoByGH", e.Message);
            }

            return 0;
        }
        ////
        /// <summary>
        /// 根据教师TeacherID，获取该教师所带的课程班信息
        /// </summary>
        /// <param name="arrCourseNo">课程ID</param>
        /// <param name="arrCourseName">课堂名称</param>
        /// <param name="arrCourseClassID">课程班ID</param>
        /// <param name="arrCourseClassName">课程班名称</param>
        /// <param name="arrSubjectID">学科ID</param>
        /// <param name="arrStudyLevelID">学习阶段ID</param>
        /// <param name="arrStudyLevelName">学习阶段名称</param>
        /// <param name="arrGlobalGrade">年级标识，K1~K12（一年级至高中三年级），U1~U5(大一至大五)，M1~M3（中职一至中职三），H1~H5（高职一至高职五）</param>
        /// <returns>课程班个数</returns>
        public int GetCourseClassInfoByUser(out string[] arrCourseNo, out string[] arrCourseName, out string[] arrCourseClassID, out string[] arrCourseClassName, out string[] arrSubjectID, out string[] arrStudyLevelID, out string[] arrStudyLevelName,out string[] arrGlobalGrade)
        {
            arrCourseNo = null;
            arrCourseName = null;
            arrCourseClassID = null;
            arrCourseClassName = null;
            arrSubjectID = null;
            arrStudyLevelID = null;
            arrStudyLevelName = null;
            arrGlobalGrade = null;
            try
            {
                CourseClassInfoExM[] courseClass1 = P_GetCourseClassByUser();
                //CourseClassInfoM[] courseClass2 = P_GetCourseClassBySubject();
                if (courseClass1 == null)
                {
                    WriteErrorMessage("GetCourseClassInfoByUser", "用户所带的教学班为空");
                    return 0;
                }
                List<CourseClassInfoExM> list = new List<CourseClassInfoExM>(courseClass1);
                //for (int i = 0; i < courseClass1.Length; i++)
                //{
                //    for (int j = 0; j < courseClass2.Length; j++)
                //    {
                //        if (courseClass1[i].CourseClassID == courseClass2[j].CourseClassID)
                //        {
                //            list.Add(courseClass1[i]);
                //            break;
                //        }
                //    }
                //}

                if (list != null)
                {
                    int iCount = list.Count;
                    if (iCount > 0)
                    {
                        arrCourseNo = new string[iCount];
                        arrCourseName = new string[iCount];
                        arrCourseClassID = new string[iCount];
                        arrCourseClassName = new string[iCount];
                        arrSubjectID = new string[iCount];
                        arrStudyLevelID = new string[iCount];
                        arrStudyLevelName = new string[iCount];
                        arrGlobalGrade = new string[iCount];

                        for (int i = 0; i < iCount; i++)
                        {
                            arrCourseNo[i] = list[i].CourseNo;
                            arrCourseName[i] = list[i].CourseName;
                            arrCourseClassID[i] = list[i].CourseClassID;
                            arrCourseClassName[i] = list[i].CourseClassName;
                            arrSubjectID[i] = list[i].SubjectID;
                            arrStudyLevelID[i] = list[i].StudyLevelID;
                            arrStudyLevelName[i] = list[i].StudyLevelName;
                            arrGlobalGrade[i] = list[i].GlobalGrade;
                        }
                    }
                    else
                    {
                        WriteTrackLog("GetCourseClassInfoByUser", "课程班数量为0");
                    }
                    return iCount;
                }
                else
                {
                    WriteTrackLog("GetCourseClassInfoByUser", JsonFormatter.JsonSerialize(list));
                }

            }
            catch (Exception e)
            {
                WriteErrorMessage("GetCourseClassInfoByUser", e.Message);
            }

            return 0;
        }
        ////
        //根据课程班级ID，获取班级下所有学生的详细信息
        public int GetStudentInfoByCourseClassID(string strCourseClassID, out string[] arrXH, out string[] arrName, out string[] arrGender, out string[] arrPhotoPath, out string[] arrClassID, out string[] arrClassName)
        {
            arrXH = null;
            arrName = null;
            arrGender = null;
            arrPhotoPath = null;
            arrClassID = null;
            arrClassName = null;
            try
            {
                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append("http://" + mCloudPlatformBFUrl + "/UserMgr/TeachInfoMgr/Api/Service_TeachInfo.ashx");
                sbUrl.Append("?token=" + mToken);
                sbUrl.Append("&method=GetCourseStudents");
                sbUrl.Append(string.Format("&params=|{0}||", strCourseClassID));
                string strReturn = mCommandApi.CallMethodGet(sbUrl.ToString());
                int iFlag = UtilityClass.AnalyseCloudJson(ref strReturn);
                if (iFlag == 3)
                {
                    if (EventInvalidToken != null)
                    {
                        EventInvalidToken();
                        return 0;
                    }
                }
                CourseStudentInfoM[] stuInfo = JsonFormatter.JsonDeserialize<CourseStudentInfoM[]>(strReturn);

                int iCount = 0;
                if (stuInfo != null)
                {
                    iCount = stuInfo.Length;
                }

                if (iCount > 0)
                {
                    arrXH = new string[iCount];
                    arrName = new string[iCount];
                    arrGender = new string[iCount];
                    arrPhotoPath = new string[iCount];
                    arrClassID = new string[iCount];
                    arrClassName = new string[iCount];

                    int index = -1;
                    for (int i = 0; i < iCount; i++)
                    {
                        arrXH[i] = stuInfo[i].UserID;
                        arrName[i] = stuInfo[i].UserName;
                        arrGender[i] = stuInfo[i].Gender;
                        arrPhotoPath[i] = stuInfo[i].PhotoPath;
                        arrClassID[i] = stuInfo[i].ClassIDQM;
                        arrClassName[i] = stuInfo[i].ClassNameQM;

                        index = arrClassID[i].IndexOf('>');
                        if (index > -1)
                        {
                            arrClassID[i] = arrClassID[i].Substring(index + 1);
                        }
                        index = arrClassName[i].IndexOf('>');
                        if (index > -1)
                        {
                            arrClassName[i] = arrClassName[i].Substring(index + 1);
                        }
                    }

                    return iCount;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetStudentInfoByCourseClassID", e.Message);
            }

            return 0;
        }

        //（弃用）根据学号，获取学生的相关信息
        private bool GetStudentInfoByXH(string strXH, ref string strName, ref string strGender, ref string strPhotoPath, ref string strClassID, ref string strClassName)
        {
            try
            {
                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append("http://" + mCloudPlatformBFUrl + "/UserMgr/UserInfoMgr/API/Service_UserInfo.ashx");
                sbUrl.Append("?token=" + mToken);
                sbUrl.Append("&method=GetStudent");
                sbUrl.Append(string.Format("&params={0}||||||||", strXH));
                string strReturn = mCommandApi.CallMethodGet(sbUrl.ToString());
                int iFlag = UtilityClass.AnalyseCloudJson(ref strReturn);
                if (iFlag == 3)
                {
                    if (EventInvalidToken != null)
                    {
                        EventInvalidToken();
                        return false;
                    }
                }
                StudentInfoM[] stuInfo = JsonFormatter.JsonDeserialize<StudentInfoM[]>(strReturn);

                if (stuInfo != null && stuInfo.Length == 1)
                {
                    strName = stuInfo[0].UserName;
                    strGender = stuInfo[0].Gender;
                    strPhotoPath = stuInfo[0].PhotoPath;
                    strClassID = stuInfo[0].ClassID;
                    strClassName = stuInfo[0].ClassName;

                    return true;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetStudentInfoByXH", e.Message);
            }

            return false;
        }

        //（更多应用系统）获取学科平台下各应用系统信息，如果strSysID为空，则返回所有应用系统
        //iUserType=-1代表未登录，将默认返回所有系统，
        //若iUsertType>=0，则代表登录，将根据用户定制的信息来返回
        public SubjectPlatformSysInfoM[] GetSubjectPlatformSysInfo(string strSysID, int iUserType)
        {
            try
            {
                //为空则不查询。一定要确定学科后才查询更多应用系统
                if (string.IsNullOrEmpty(mCloudPlatformSubjectID))
                {
                    return null;
                }

                //初始化更多应用系统，一开始从本地数据库读取，主要信息只有系统ID和系统名称
                //然后从学科平台获取，将各系统的其它信息添加进来
                string strWholeUrl = string.Format(Properties.Resources.TeachSetUrl, mNetTeachApiIP, mNetTeachApiPort,mNetTeachApiVirDir);
                strWholeUrl = strWholeUrl + "?action=SelectAllOuterSystemBySubject&params=[\"" + mMySubjectID.ToString() + "\"]";
                string strData = mCommandApi.CallMethodGet(strWholeUrl);
                if (string.IsNullOrEmpty(strData))
                {
                    return null;
                }
                OuterSystemM[] arrOuterSys = JsonFormatter.JsonDeserialize<OuterSystemM[]>(strData);
                if (arrOuterSys == null || arrOuterSys.Length == 0)
                {
                    return null;
                }

                List<SubjectPlatformSysInfoM> listSystem = new List<SubjectPlatformSysInfoM>(arrOuterSys.Length);

                //表示有用户登录
                if (iUserType > -1)
                {
                    //说明该用户是新用户
                    if (strSysID == null)
                    {
                        strSysID = "630|510|640|810|330|430";
                    }
                    //说明该用户定制的内容就是没有任何系统
                    else if (strSysID == "")
                    {
                        strSysID = "330|430";
                    }
                    //一般情况（有定制内容）
                    else
                    {
                        strSysID = strSysID + "|330|430";
                    }

                    for (int i = 0; i < arrOuterSys.Length; i++)
                    {
                        if (strSysID.Contains(arrOuterSys[i].SystemID))
                        {
                            SubjectPlatformSysInfoM sys = new SubjectPlatformSysInfoM();
                            sys.SysID = arrOuterSys[i].SystemID;
                            sys.SysName = arrOuterSys[i].SystemName;
                            sys.SysImage = arrOuterSys[i].PhotoPath;
                            sys.SubjectID = mCloudPlatformSubjectID;
                            sys.SubjectName = mCloudPlatformSubjectName;
                            sys.IsSetup = false;

                            //为资料收藏夹和公共论坛指定默认访问路径，若之后能从云平台读取这两个系统信息，则覆盖
                            if (string.IsNullOrEmpty(mCloudPlatformBFUrl) == false)
                            {
                                if (sys.SysID == "330")
                                {
                                    sys.IsSetup = true;
                                    sys.AccessAddr = "http://" + mCloudPlatformBFUrl + "/SysMgr/Favorite/Default.aspx";
                                }
                                else if (sys.SysID == "430")
                                {
                                    sys.IsSetup = true;
                                    sys.AccessAddr = "http://" + mCloudPlatformBFUrl + "/Community/Forum/WebPage/ForumMain.aspx";
                                }
                            }

                            listSystem.Add(sys);
                        }
                    }
                }
                //表示当前没有用户登录
                else if (iUserType == -1)
                {
                    iUserType = 1;//当做老师用户来处理
                    for (int i = 0; i < arrOuterSys.Length; i++)
                    {
                        SubjectPlatformSysInfoM sys = new SubjectPlatformSysInfoM();
                        sys.SysID = arrOuterSys[i].SystemID;
                        sys.SysName = arrOuterSys[i].SystemName;
                        sys.SysImage = arrOuterSys[i].PhotoPath;
                        sys.SubjectID = mCloudPlatformSubjectID;
                        sys.SubjectName = mCloudPlatformSubjectName;
                        sys.IsSetup = false;

                        //为资料收藏夹和公共论坛指定默认访问路径，若之后能从云平台读取这两个系统信息，则覆盖
                        if (string.IsNullOrEmpty(mCloudPlatformBFUrl) == false)
                        {
                            if (sys.SysID == "330")
                            {
                                sys.IsSetup = true;
                                sys.AccessAddr = "http://" + mCloudPlatformBFUrl + "/SysMgr/Favorite/Default.aspx";
                            }
                            else if (sys.SysID == "430")
                            {
                                sys.IsSetup = true;
                                sys.AccessAddr = "http://" + mCloudPlatformBFUrl + "/Community/Forum/WebPage/ForumMain.aspx";
                            }
                        }

                        listSystem.Add(sys);
                    }
                }

                //从学科平台读取配置的应用系统信息
                string strWebServiceURL = "";
                string strParam = "";
                string strReturn = "";
                XmlDocument xmlDoc;
                strWebServiceURL = "http://{0}/Base/WS/Service_Basic.asmx/WS_G_GetSystemAccessInfoForSP";
                //云平台的strSubjectID跟课堂教学系统的不一样
                strParam = "subjectID=" + mCloudPlatformSubjectID;
                strWebServiceURL = string.Format(strWebServiceURL, mCloudPlatformBFUrl);
                strReturn = mCommandWS.CallMethodPost(strWebServiceURL, strParam);

                if (string.IsNullOrEmpty(strReturn))
                {
                    return null;
                }

                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);
                XmlNodeList list = xmlDoc.GetElementsByTagName("anyType");
                int iCount = 0;
                if (list != null)
                {
                    iCount = list.Count;
                }

                if (iCount > 0)
                {
                    for (int i = 0; i < iCount; i++)
                    {
                        XmlNode node = list[i];
                        SubjectPlatformSysInfoM o = new SubjectPlatformSysInfoM();
                        o.SubjectID = node.ChildNodes[0].InnerText;
                        o.SubjectName = node.ChildNodes[1].InnerText;
                        o.SysID = node.ChildNodes[2].InnerText;
                        o.SysName = node.ChildNodes[3].InnerText;
                        o.SysImage = node.ChildNodes[4].InnerText;
                        o.IsEXE = Convert.ToBoolean(node.ChildNodes[5].InnerText);
                        o.IsSetup = Convert.ToBoolean(node.ChildNodes[6].InnerText);
                        o.AccessAddr = node.ChildNodes[7].InnerText;
                        o.WebSvrAddr = node.ChildNodes[8].InnerText;
                        o.WsSvrAddr = node.ChildNodes[9].InnerText;

                        for (int j = 0; j < listSystem.Count; j++)
                        {
                            if (listSystem[j].SysID == o.SysID)
                            {
                                //匹配学科
                                if (string.IsNullOrEmpty(o.SubjectID))//如果查询到的系统的学科ID为空，则认为适合所有学科
                                {
                                    listSystem[j] = o;

                                    string strSysName = listSystem[j].SysName;
                                    DealWithString(ref strSysName, iUserType);
                                    listSystem[j].SysName = strSysName;

                                    string strAccessAddr = listSystem[j].AccessAddr;
                                    DealWithString(ref strAccessAddr, iUserType);
                                    listSystem[j].AccessAddr = strAccessAddr;
                                }
                                //若不为空则匹配当前学科
                                else
                                {
                                    if (o.SubjectID == mCloudPlatformSubjectID)
                                    {
                                        listSystem[j] = o;

                                        string strSysName = listSystem[j].SysName;
                                        DealWithString(ref strSysName, iUserType);
                                        listSystem[j].SysName = strSysName;

                                        string strAccessAddr = listSystem[j].AccessAddr;
                                        DealWithString(ref strAccessAddr, iUserType);
                                        listSystem[j].AccessAddr = strAccessAddr;
                                    }
                                }
                            }
                        }
                    }
                }

                if (listSystem != null && listSystem.Count > 0)
                {
                    return listSystem.ToArray();
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetOuterSystemInfo", e.Message);
            }

            return null;
        }

        /// <summary>
        /// 获取更多应用系统（对接云平台不再维护各应用系统相对路径的版本）
        /// </summary>
        /// <param name="strSysID">用户定制的系统</param>
        /// <param name="iUserType">用户类型，-1代表没有用户登录，则默认返回所有系统</param>
        /// <returns>学科平台子系统信息</returns>
        public SubjectPlatformSysInfoM[] GetSubjectPlatformSysInfoV2(string strSysID, int iUserType)
        {
            try
            {
                //为空则不查询。一定要确定学科后才查询更多应用系统
                if (string.IsNullOrEmpty(mCloudPlatformSubjectID))
                {
                    return null;
                }

                //初始化更多应用系统，确定当前学科最多可以显示哪些系统（从本地数据库读取，主要信息只有系统ID和系统名称）
                string strWholeUrl = string.Format(Properties.Resources.TeachSetUrl, mNetTeachApiIP, mNetTeachApiPort,mNetTeachApiVirDir);
                strWholeUrl = strWholeUrl + "?action=SelectAllOuterSystemBySubject&params=[\"" + mMySubjectID.ToString() + "\"]";
                string strData = mCommandApi.CallMethodGet(strWholeUrl);
                if (string.IsNullOrEmpty(strData))
                {
                    return null;
                }

                //更多应用系统（本系统实体）
                OuterSystemM[] arrOuterSys = JsonFormatter.JsonDeserialize<OuterSystemM[]>(strData);
                if (arrOuterSys == null || arrOuterSys.Length == 0)
                {
                    return null;
                }

                //更多应用系统（学科平台的实体）
                List<SubjectPlatformSysInfoM> listSystem = new List<SubjectPlatformSysInfoM>(arrOuterSys.Length);

                //表示有用户登录
                if (iUserType > -1)
                {
                    //说明该用户是新用户，则默认显示如下几个系统
                    if (strSysID == null)
                    {
                        strSysID = "810|430";
                    }
                    //说明该用户定制的内容就是没有任何系统，但必须要显示SysID=430
                    else if (strSysID == "")
                    {
                        strSysID = "430";
                    }
                    //一般情况（有定制内容），添加显示SysID=430
                    else
                    {
                        strSysID = strSysID + "|430";
                    }

                    //有用户登录，则需要判断用户定制了哪些系统
                    for (int i = 0; i < arrOuterSys.Length; i++)
                    {
                        //保留用户定制的系统
                        if (strSysID.Contains(arrOuterSys[i].SystemID))
                        {
                            SubjectPlatformSysInfoM sys = P_GetSPSysModel(arrOuterSys[i], iUserType);
                            listSystem.Add(sys);
                        }
                    }
                }
                //表示当前没有用户登录
                else if (iUserType == -1)
                {
                    //没有用户登录则不用判断用户定制了哪些系统，直接返回所有可显示的
                    for (int i = 0; i < arrOuterSys.Length; i++)
                    {
                        SubjectPlatformSysInfoM sys = P_GetSPSysModel(arrOuterSys[i], iUserType);
                        listSystem.Add(sys);
                    }
                }

                if (listSystem != null && listSystem.Count > 0)
                {
                    return listSystem.ToArray();
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetSubjectPlatformSysInfoV2", e.Message);
            }
            return null;
        }

        /// <summary>
        /// （学生端）获取学科平台子系统的信息（访问路径已经是完整路径）
        /// </summary>
        /// <returns>返回所有需要显示的系统，未安装的系统访问路径为空，IsSetup=false</returns>
        public SubjectPlatformSysInfoM[] GetSubjectPlatformSysInfoForStu()
        {
            try
            {
                //为空则不查询。一定要确定学科后才查询更多应用系统
                if (string.IsNullOrEmpty(mCloudPlatformSubjectID))
                {
                    return null;
                }
                List<SubjectPlatformSysInfoM> listSys = new List<SubjectPlatformSysInfoM>();
                listSys.Add(P_GetSubjectPlatformSysInfoForStu("C11"));
                listSys.Add(P_GetSubjectPlatformSysInfoForStu("630"));
                listSys.Add(P_GetSubjectPlatformSysInfoForStu("510"));
                listSys.Add(P_GetSubjectPlatformSysInfoForStu("810"));
                listSys.Add(P_GetSubjectPlatformSysInfoForStu("821"));
                return listSys.ToArray();
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetSubjectPlatformSysInfoForStu", e.Message);
            }
            return null;
        }

        //获取学生端需要的系统，并拼接完整的访问路径
        private SubjectPlatformSysInfoM P_GetSubjectPlatformSysInfoForStu(string SysID)
        {
            SubjectPlatformSysInfoM sys = new SubjectPlatformSysInfoM()
            {
                SysID = SysID,
                SubjectID = mCloudPlatformSubjectID,
                SubjectName = mCloudPlatformSubjectName,
                IsSetup = false
            };

            try
            {
                string strWebIPandPort = GetSubSysWebIPandPort(SysID);
                if (string.IsNullOrEmpty(strWebIPandPort))
                {
                    sys.AccessAddr = "";
                }
                else
                {
                    sys.IsSetup = true;
                    sys.WebSvrAddr = sys.AccessAddr = "http://" + strWebIPandPort + "/";
                }

                switch (SysID)
                {
                    case "C11":
                        sys.SysName = "自由学习";
                        if (sys.IsSetup == true)
                        {
                            sys.AccessAddr = sys.AccessAddr + "View/Index.aspx";
                        }
                        break;
                    case "630":
                        sys.SysName = "课前预习";
                        if (sys.IsSetup == true)
                        {
                            sys.AccessAddr = sys.AccessAddr + "Student/Index.aspx";
                        }
                        break;
                    case "510":
                        sys.SysName = "课后练习";
                        if (sys.IsSetup == true)
                        {
                            sys.AccessAddr = sys.AccessAddr + "Task/Student/StuIndex.aspx";
                        }
                        break;
                    case "810":
                        sys.SysName = "我的成绩";
                        if (sys.IsSetup == true)
                        {
                            sys.AccessAddr = sys.AccessAddr + "index.aspx";
                        }
                        break;
                    case "821":
                        sys.SysName = "我的知识谱";
                        if (sys.IsSetup == true)
                        {
                            //sys.AccessAddr = sys.AccessAddr + "index.aspx";
                            string RZWebApiAddr=GetSubSysApiIPandPort("821");
                            RZWebApiAddr = "http://"+RZWebApiAddr + "/api/ZSP/GetZSP_StudentMainPage";
                            RZWebApiAddr = string.Format("{0}?Token={1}&SubjectID={2}&XH={3}", RZWebApiAddr, mToken, mCloudPlatformSubjectID, mTeacherID);
                            WriteDebugInfo("GetSubjectPlatformSysInfoForStu访问知识谱webapi地址", RZWebApiAddr);
                            string AccessAddr = mCommandApi.CallMethodGet(RZWebApiAddr);
                            WriteDebugInfo("GetSubjectPlatformSysInfoForStu访问知识谱webapi结果", AccessAddr);
                            sys.AccessAddr = ((JObject)JsonConvert.DeserializeObject(AccessAddr))["url"].ToString();
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetSubjectPlatformSysInfoForStu", e.Message);
            }
            return sys;
        }

        //将本系统实体转换成学科平台实体
        private SubjectPlatformSysInfoM P_GetSPSysModel(OuterSystemM outerSys, int iUserType)
        {
            if (outerSys == null)
            {
                return null;
            }

            SubjectPlatformSysInfoM sys = new SubjectPlatformSysInfoM();
            sys.SysID = outerSys.SystemID;
            sys.SysName = outerSys.SystemName;
            sys.SysImage = outerSys.PhotoPath;
            sys.SubjectID = mCloudPlatformSubjectID;
            sys.SubjectName = mCloudPlatformSubjectName;
            sys.IsSetup = false;
            //访问地址中的IP和端口从云平台接口获取
            string strWebIPandPort = "";
            //430的IP和端口就是云平台的，无法从云平台接口获取
            if (sys.SysID == "430")
            {
                strWebIPandPort = mCloudPlatformBFUrl;
            }
            else
            {
                strWebIPandPort = GetSubSysWebIPandPort(sys.SysID);
            }
            if (string.IsNullOrEmpty(strWebIPandPort))
            {
                sys.IsSetup = false;
                sys.AccessAddr = "";
            }
            else
            {
                sys.IsSetup = true;
                //若无当前无用户登录，则当做教师用户来获取相对路径
                if (iUserType == -1)
                {
                    iUserType = 1;
                }
                //访问地址中的相对路径和参数在代码里写死
                sys.AccessAddr = "http://" + strWebIPandPort + P_GetSubjectPlatformSysPartAddr(sys.SysID, iUserType);
            }

            return sys;
        }

        //根据系统ID获取学科平台子系统的相对路径
        private string P_GetSubjectPlatformSysPartAddr(string SysID, int UserType)
        {
            string strPartAddr = "";

            if (SysID == null)
            {
                return strPartAddr;
            }

            string strSysID = SysID.ToUpper();
            switch (SysID)
            {
                case "S11":
                    strPartAddr = "/ClassPreview.aspx?lg_tk=" + mToken + "&SubjectID=" + mCloudPlatformSubjectID;
                    break;
                case "S13":
                    strPartAddr = "/AfterClassPractice.aspx?lg_tk=" + mToken + "&SubjectID=" + mCloudPlatformSubjectID;
                    break;
                case "810":
                    strPartAddr = "/index.aspx?lg_tk=" + mToken + "&SubjectID=" + mCloudPlatformSubjectID;
                    break;
                case "830":
                    strPartAddr = "/index.aspx?lg_tk=" + mToken + "&SubjectID=" + mCloudPlatformSubjectID;
                    break;
                case "821":
                    strPartAddr = "/index.aspx?lg_tk=" + mToken + "&SubjectID=" + mCloudPlatformSubjectID;
                    break;
                case "851":
                    strPartAddr = "/View/TeachStudy.aspx?lg_tk=" + mToken + "&typeid=1";
                    break;
                case "852":
                    strPartAddr = "/View/TeachBehaviour.aspx?lg_tk=" + mToken + "&typeid=2";
                    break;
                case "S20":
                    strPartAddr = "/FreeStudy/index.aspx?lg_tk=" + mToken;
                    break;
                case "S30":
                    strPartAddr = "/Mainpage.aspx?lg_tk=" + mToken + "&subID=" + mCloudPlatformSubjectID;
                    break;
                case "430":
                    strPartAddr = "/Community/Forum/WebPage/ForumMain.aspx";
                    break;
            }

            return strPartAddr;
        }

        private void DealWithString(ref string str, int iUserType)
        {
            try
            {
                if (string.IsNullOrEmpty(str) == false)
                {
                    bool bIsFind = false;
                    string[] arrTemp = str.Split('|');
                    foreach (string s in arrTemp)
                    {
                        string[] arr = s.Split(new string[1] { "@@" }, StringSplitOptions.None);
                        if (arr[0] == iUserType.ToString())
                        {
                            str = arr[1];
                            bIsFind = true;
                            break;
                        }
                    }

                    if (bIsFind == false)
                    {
                        str = arrTemp[0];
                        int index = str.IndexOf("@@");
                        if (index > -1)
                        {
                            str = str.Substring(index + 2);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("DealWithString", e.Message);
            }
        }

        //初始化部分系统的URL（这个是不是没用了？）
        public string GetDigitalLibraryUrl(int iUserType)
        {
            try
            {
                //从学科平台读取配置的应用系统信息
                string strWebServiceURL = "";
                string strParam = "";
                string strReturn = "";
                XmlDocument xmlDoc;
                strWebServiceURL = "http://{0}/Base/WS/Service_Basic.asmx/WS_G_GetSystemAccessInfoForSP";
                //云平台的strSubjectID跟课堂教学系统的不一样
                strParam = "subjectID=" + mCloudPlatformSubjectID;
                strWebServiceURL = string.Format(strWebServiceURL, mCloudPlatformBFUrl);
                strReturn = mCommandWS.CallMethodPost(strWebServiceURL, strParam);

                if (string.IsNullOrEmpty(strReturn))
                {
                    return null;
                }

                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);
                XmlNodeList list = xmlDoc.GetElementsByTagName("anyType");
                int iCount = 0;
                if (list != null)
                {
                    iCount = list.Count;
                }

                if (iCount > 0)
                {
                    string strDigitalUrl = "";
                    for (int i = 0; i < iCount; i++)
                    {
                        XmlNode node = list[i];
                        if (node.ChildNodes[2].InnerText == C_DigitalLibarySysID)
                        {
                            strDigitalUrl = node.ChildNodes[7].InnerText;
                            DealWithString(ref strDigitalUrl, iUserType);
                            return strDigitalUrl;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("InitSomeSystemUrl", e.Message);
            }

            return "";
        }
        /// <summary>
        /// 获取基础平台系统信息
        /// CLPSysConfigInfo.useRange 云平台产品使用范围。（1：单个专业英语院校使用，2：单个普通大学使用，3：单个中小学校使用，4：多学校（县/区）范围）使用，5：中职学校使用，6：高职学校使用。）
        ///CLPSysConfigInfo.useRange  产品名称
        /// </summary>
        /// <returns></returns>
        public CLPSysConfigInfo GetCLPSysConfigInfo()
        {
            try
            {
                string strWebServiceURL = "http://{0}/base/WS/Service_Basic.asmx/WS_G_GetSysConfigInfo";
                strWebServiceURL = string.Format(strWebServiceURL, mCloudPlatformBFUrl);
                string strReturn = mCommandWS.CallMethodPost(strWebServiceURL, "");
                if (string.IsNullOrEmpty(strReturn))
                    return null;
                CLPSysConfigInfo cLPSysConfigInfo = new CLPSysConfigInfo();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);
                XmlNodeList list = xmlDoc.GetElementsByTagName("ArrayOfString");
                if (list != null && list[0].ChildNodes.Count > 0)
                {
                    cLPSysConfigInfo.useRange = list[0].ChildNodes[0].InnerText;
                    cLPSysConfigInfo.productName = list[0].ChildNodes[1].InnerText;
                    return cLPSysConfigInfo;
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetCLPSysConfigInfo", e.Message);
                return null;
            }
        }
        //依次返回 学科大数据、学科课堂表现、电子资源阅览室 三个系统的访问地址
        public string[] GetSomeSystemWebUrl(int iUserType)
        {
            string[] arrSysUrl = new string[3];
            try
            {
                //学科大数据
                string strBigDataAnalysisUrl = "";
                //学生课堂表现
                string strStudentResultUrl = "";
                //电子资源阅览室
                string strDigitalLibarySysUrl = "";

                //从学科平台读取配置的应用系统信息
                string strWebServiceURL = "";
                string strParam = "";
                string strReturn = "";
                XmlDocument xmlDoc;
                strWebServiceURL = "http://{0}/Base/WS/Service_Basic.asmx/WS_G_GetSubSystemAccessAddrByUserType";
                strWebServiceURL = string.Format(strWebServiceURL, mCloudPlatformBFUrl);
                strParam = "sysID={0}&subjectID=" + mCloudPlatformSubjectID + "&userType=" + iUserType;

                string strParam1 = string.Format(strParam, C_BigDataAnalysisSysID);
                strReturn = mCommandWS.CallMethodPost(strWebServiceURL, strParam1);
                if (string.IsNullOrEmpty(strReturn) == false)
                {
                    xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(strReturn);
                    XmlNodeList list = xmlDoc.GetElementsByTagName("ArrayOfString");
                    if (list != null && list.Count == 1 && list[0].ChildNodes.Count > 0)
                    {
                        strBigDataAnalysisUrl = list[0].ChildNodes[3].InnerText;
                    }
                }

                string strParam2 = string.Format(strParam, C_StudentResultID);
                strReturn = mCommandWS.CallMethodPost(strWebServiceURL, strParam2);
                if (string.IsNullOrEmpty(strReturn) == false)
                {
                    xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(strReturn);
                    XmlNodeList list = xmlDoc.GetElementsByTagName("ArrayOfString");
                    if (list != null && list.Count == 1 && list[0].ChildNodes.Count > 0)
                    {
                        strStudentResultUrl = list[0].ChildNodes[3].InnerText;
                    }
                }

                string strParam3 = string.Format(strParam, C_DigitalLibarySysID);
                strReturn = mCommandWS.CallMethodPost(strWebServiceURL, strParam3);
                if (string.IsNullOrEmpty(strReturn) == false)
                {
                    xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(strReturn);
                    XmlNodeList list = xmlDoc.GetElementsByTagName("ArrayOfString");
                    if (list != null && list.Count == 1 && list[0].ChildNodes.Count > 0)
                    {
                        strDigitalLibarySysUrl = list[0].ChildNodes[3].InnerText;
                    }
                }

                arrSysUrl[0] = strBigDataAnalysisUrl;
                arrSysUrl[1] = strStudentResultUrl;
                arrSysUrl[2] = strDigitalLibarySysUrl;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetSomeSystemUrl", e.Message);
            }

            return arrSysUrl;
        }

        /// <summary>
        /// 获取教师所带学科（教师端）
        /// </summary>
        public CloudPlatformSubjectM[] GetSubjectsByUserID(string strToken, string strUserID, out bool bValidToken)
        {
            bValidToken = true;
            try
            {
                int iErrorFlag = 0;

                //这里可能需要Uri编码和解码
                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append("http://" + mCloudPlatformBFUrl + "/UserMgr/TeachInfoMgr/Api/Service_TeachInfo.ashx");
                sbUrl.Append("?token=" + strToken);
                sbUrl.Append("&method=GetSubjectsByUser");
                sbUrl.Append("&params=" + strUserID);
                WriteTrackLog("GetSubjectsByUserID", "sbUrl = " + sbUrl);
                CloudPlatformSubjectM[] subject = CallApiHelper.CallMethodGet_Cloud<CloudPlatformSubjectM[]>(sbUrl.ToString(), out iErrorFlag);
                WriteTrackLog("GetSubjectsByUserID", "subject.count = " + subject.GetLength(0).ToString());
                if (iErrorFlag == 3)
                {
                    bValidToken = false;
                }
                WriteErrorMessage("GetSubjectsByUserID", "");
                if (subject == null || subject.Length == 0)
                {
                    return null;
                }

                //CloudPlatformSubjectM[] arrSubject = P_MatchSubject(subject);
                return subject;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetSubjectsByUserID", e.Message);
            }
            return null;
        }
        /// <summary>
        ///   检测当前点控是否可用
        /// </summary>
        /// <returns>结果标识。 1：点控可用，0：超过点数上限，-1：未检测到加密锁，-2：加密锁已过试用期，-3：没有购买该产品，-4：加密锁接口调用错误，-5：加密锁时钟错误 -6:基础平台接口异常 -7:基础平台接口返回值为空 -8：LBD_Webapiinterface接口异常</returns>
        public int WS_G_SetNewLockPoint(string sysId,string token)
        {
            try
            {
                int iResult = 0;
                string strWholeUrl = "http://" + mCloudPlatformBFUrl + "/LockerMgr/WS/Service_LockerMgr.asmx/WS_G_SetNewLockPoint";
                if (string.IsNullOrEmpty(mTeacherID))
                    mTeacherID = GetOnlineUserInfo(token).UserID;
                string secCode = CP_MD5Helper.GetMd5Hash(sysId + mTeacherID);
                string strParam = string.Format("SysID={0}&PointID={1}&SecCode={2}", sysId, mTeacherID, secCode);
                string strReturn = mCommandWS.CallMethodPost(strWholeUrl, strParam);
                if (string.IsNullOrEmpty(strReturn))
                {
                    WriteErrorMessage("WS_G_SetNewLockPoint", "接口返回空");
                    return -6;
                }
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);
                XmlNodeList list = xmlDoc.GetElementsByTagName("int");
                if (list == null || list.Count == 0)
                {
                    return -7;
                }
                iResult = Convert.ToInt32(list[0].InnerText);
                return iResult;
            }
            catch (Exception exc)
            {
                WriteErrorMessage("WS_G_SetNewLockPoint", exc.ToString());
                return -8;
            }
        }

        /// <summary>
        /// 获取云平台子系统的加密锁配置信息
        /// </summary>
        /// <param name="slockerID">子系统锁编号</param>
        /// <param name="sProductPointCount">产品点数</param>
        /// <param name="sProbationYear">产品试用期的年份</param>
        /// <param name="sProbationMonth">产品试用期的月份</param>
        /// <param name="sProbationDay">产品试用期的日</param>
        /// <returns>结果标识，1-获取成功，返回其他值为失败</returns>
        public int GetSubSystemLockerInfoByID(string slockerID, out int iProductPointCount, out string sProbationYear, out string sProbationMonth, out string sProbationDay)
        {
            iProductPointCount = 0;
            sProbationYear = "";
            sProbationMonth = "";
            sProbationDay = "";
            try
            {
                XmlDocument xmlDoc = null;
               // string strWholeUrl = "http://" + mCloudPlatformBFUrl + "/Base/WS/Service_Basic.asmx/WS_G_GetSubSystemLockerInfoByID";
                string strWholeUrl = "http://" + mCloudPlatformBFUrl + "/LockerMgr/WS/Service_LockerMgr.asmx/WS_G_GetSubSystemLockerInfoByID";
                string requestTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string secCode = CP_MD5Helper.GetMd5Hash(slockerID + requestTime);
                string strParam = string.Format("lockerID={0}&requestTime={1}&secCode={2}", slockerID, requestTime, secCode);
                //WriteDebugInfo("GetSubSystemLockerInfoByID", string.Format("接口地址：{0},访问参数：{1}", strWholeUrl, strParam));
                string strReturn = mCommandWS.CallMethodPost(strWholeUrl, strParam);
                if (string.IsNullOrEmpty(strReturn))
                {
                    WriteErrorMessage("GetSubSystemLockerInfoByID", "接口返回空");
                    return -7;
                }
                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);
                XmlNodeList list = xmlDoc.GetElementsByTagName("string");
                if (list == null || list.Count == 0)
                {
                    return -6;
                }
                int iRtnValue =Convert.ToInt32(list[0].InnerText);
                if (iRtnValue == 1)
                {
                    iProductPointCount = Convert.ToInt32(CP_EncryptHelper.DecryptCode(slockerID,list[1].InnerText));
                    sProbationYear = CP_EncryptHelper.DecryptCode(slockerID,list[2].InnerText);
                    sProbationMonth = CP_EncryptHelper.DecryptCode(slockerID,list[3].InnerText);
                    sProbationDay = CP_EncryptHelper.DecryptCode(slockerID,list[4].InnerText);
                    return 1;//获取成功
                }
                else
                {
                    switch (iRtnValue)
                    {
                        case -1:
                            WriteTrackLog("GetSubSystemLockerInfoByID", "未检测到加密锁");
                            break;
                        case -2:
                            WriteTrackLog("GetSubSystemLockerInfoByID", "加密锁已过试用期");
                            break;
                        case -3:
                            WriteTrackLog("GetSubSystemLockerInfoByID", "没有购买该产品");
                            break;
                        case -4:
                            WriteTrackLog("GetSubSystemLockerInfoByID", "加密锁接口调用错误");
                            break;
                        case -5:
                            WriteTrackLog("GetSubSystemLockerInfoByID", "加密锁时钟错误");
                            break;
                        default:
                            WriteTrackLog("GetSubSystemLockerInfoByID", "未知错误！");
                            break;
                    }
                    return iRtnValue;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetSubSystemLockerInfoByID", e.Message);
            }
            return -7;
        }
        //（弃用）
        private bool P_InitSubjectPlatformSysInfo()
        {
            try
            {
                //为空则不查询。一定要确定学科后才查询更多应用系统
                if (string.IsNullOrEmpty(mCloudPlatformSubjectID))
                {
                    return false;
                }

                //初始化更多应用系统，一开始从本地数据库读取，主要信息只有系统ID和系统名称
                //然后从学科平台获取，将各系统的其它信息添加进来
                string strWholeUrl = string.Format(Properties.Resources.TeachSetUrl, mNetTeachApiIP, mNetTeachApiPort, mNetTeachApiVirDir);
                strWholeUrl = strWholeUrl + "?action=SelectAllOuterSystem";
                string strData = mCommandApi.CallMethodGet(strWholeUrl);
                if (string.IsNullOrEmpty(strData))
                {
                    return false;
                }
                OuterSystemM[] arrOuterSys = JsonFormatter.JsonDeserialize<OuterSystemM[]>(strData);
                if (arrOuterSys == null || arrOuterSys.Length == 0)
                {
                    return false;
                }

                List<SubjectPlatformSysInfoM> listSystem = new List<SubjectPlatformSysInfoM>(arrOuterSys.Length);
                for (int i = 0; i < arrOuterSys.Length; i++)
                {
                    SubjectPlatformSysInfoM sys = new SubjectPlatformSysInfoM();
                    sys.SysID = arrOuterSys[i].SystemID;
                    sys.SysName = arrOuterSys[i].SystemName;
                    sys.SysImage = arrOuterSys[i].PhotoPath;
                    sys.SubjectID = mCloudPlatformSubjectID;
                    sys.SubjectName = mCloudPlatformSubjectName;
                    sys.IsSetup = false;

                    listSystem.Add(sys);
                }

                //从学科平台读取配置的应用系统信息
                string strWebServiceURL = "";
                string strParam = "";
                string strReturn = "";
                XmlDocument xmlDoc;
                strWebServiceURL = "http://{0}/Base/WS/Service_Basic.asmx/WS_G_GetSystemAccessInfoForSP";
                //云平台的strSubjectID跟课堂教学系统的不一样
                strParam = "subjectID=" + mCloudPlatformSubjectID;
                strWebServiceURL = string.Format(strWebServiceURL, mCloudPlatformBFUrl);
                strReturn = mCommandWS.CallMethodPost(strWebServiceURL, strParam);

                if (string.IsNullOrEmpty(strReturn))
                {
                    mSubjectPlatformSysInfo = null;
                    return false;
                }

                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);
                XmlNodeList list = xmlDoc.GetElementsByTagName("anyType");
                int iCount = 0;
                if (list != null)
                {
                    iCount = list.Count;
                }
                if (iCount > 0)
                {
                    //如果当前云平台学科ID为空，则不匹配学科
//                     if (string.IsNullOrEmpty(mCloudPlatformSubjectID))
//                     {
//                         for (int i = 0; i < iCount; i++)
//                         {
//                             XmlNode node = list[i];
//                             SubjectPlatformSysInfoM o = new SubjectPlatformSysInfoM();
//                             o.SubjectID = node.ChildNodes[0].InnerText;
//                             o.SubjectName = node.ChildNodes[1].InnerText;
//                             o.SysID = node.ChildNodes[2].InnerText;
//                             o.SysName = node.ChildNodes[3].InnerText;
//                             o.SysImage = node.ChildNodes[4].InnerText;
//                             o.IsEXE = Convert.ToBoolean(node.ChildNodes[5].InnerText);
//                             o.IsSetup = Convert.ToBoolean(node.ChildNodes[6].InnerText);
//                             o.AccessAddr = node.ChildNodes[7].InnerText;
//                             o.WebSvrAddr = node.ChildNodes[8].InnerText;
//                             o.WsSvrAddr = node.ChildNodes[9].InnerText;
// 
//                             for (int j = 0; j < listSystem.Count; j++)
//                             {
//                                 if (listSystem[j].SysID == o.SysID)
//                                 {
//                                     listSystem[j] = o;
//                                 }
//                             }
//                         }
//                     }
                    //如果当前云平台学科ID不为空，则需要匹配学科
//                    else
//                   {
                        for (int i = 0; i < iCount; i++)
                        {
                            XmlNode node = list[i];
                            SubjectPlatformSysInfoM o = new SubjectPlatformSysInfoM();
                            o.SubjectID = node.ChildNodes[0].InnerText;
                            o.SubjectName = node.ChildNodes[1].InnerText;
                            o.SysID = node.ChildNodes[2].InnerText;
                            o.SysName = node.ChildNodes[3].InnerText;
                            o.SysImage = node.ChildNodes[4].InnerText;
                            o.IsEXE = Convert.ToBoolean(node.ChildNodes[5].InnerText);
                            o.IsSetup = Convert.ToBoolean(node.ChildNodes[6].InnerText);
                            o.AccessAddr = node.ChildNodes[7].InnerText;
                            o.WebSvrAddr = node.ChildNodes[8].InnerText;
                            o.WsSvrAddr = node.ChildNodes[9].InnerText;

                            for (int j = 0; j < listSystem.Count; j++)
                            {
                                if (listSystem[j].SysID == o.SysID)
                                {
                                    //匹配学科
                                    if (string.IsNullOrEmpty(o.SubjectID))//如果查询到的系统的学科ID为空，则认为适合所有学科
                                    {
                                        listSystem[j] = o;
                                    }
                                    else
                                    {
                                        if (o.SubjectID == mCloudPlatformSubjectID)
                                        {
                                            listSystem[j] = o;
                                        }
                                    }
                                }
                            }
                        }
//                    }
                }

                if (listSystem != null && listSystem.Count > 0)
                {
                    mSubjectPlatformSysInfo = listSystem.ToArray();
                    return true;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("P_InitSubjectPlatformSysInfo", e.Message);
            }
            return false;
        }

        #endregion

        #region 课堂信息相关

        public TeachModuleM[] GetSubjectTeachModule()
        {
            try
            {
                string[] arrParam = new string[1];
                arrParam[0] = mMySubjectID.ToString();
                string strReturn = mCommandApi.CallMethodGet("SelectTeachModule", arrParam);
                if (string.IsNullOrEmpty(strReturn))
                {
                    return null;
                }
                TeachModuleM[] tmm = JsonFormatter.JsonDeserialize<TeachModuleM[]>(strReturn);
                if (tmm != null)
                {
                    return tmm;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetSubjectTeachModule", e.Message);
            }
            return null;
        }

        //获取学科下的所有教学模式
        public LBD_WebApiInterface.ClassTeach.TeachModeM[] GetSubjectTeachMode()
        {
            try
            {
                string[] arrParam = new string[1];
                arrParam[0] = mMySubjectID.ToString();
                string strReturn = mCommandApi.CallMethodGet("SelectTeachMode", arrParam);
                if (string.IsNullOrEmpty(strReturn))
                {
                    return null;
                }
                LBD_WebApiInterface.ClassTeach.TeachModeM[] tmm = JsonFormatter.JsonDeserialize<LBD_WebApiInterface.ClassTeach.TeachModeM[]>(strReturn);
                if (tmm != null)
                {
                    return tmm;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetSubjectTeachMode", e.Message);
            }

            return null;
        }

        //此接口本应该放在NetCoursewareI中，因框架调用方便而放置于此
        //更新网络化课件的状态（使用网络化课件上课后就调用此接口将网络化课件状态更新为true）
        public int UpdateNetCoursewareStatus(string strCoursewareID, bool bStatus, string strLastEditor)
        {
            try
            {
                if (string.IsNullOrEmpty(strCoursewareID))
                {
                    return 0;
                }

                string strApiBaseUrl = string.Format(Properties.Resources.NetCoursewareUrl, mNetTeachApiIP, mNetTeachApiPort,mNetTeachApiVirDir);
                CommandApi CommandApi = new CommandApi();
                CommandApi.BaseUrl = strApiBaseUrl;

                string[] arrParam = new string[9];
                arrParam[0] = strCoursewareID;
                arrParam[1] = null;
                arrParam[2] = null;
                arrParam[3] = null;
                arrParam[4] = null;
                arrParam[5] = null;
                arrParam[6] = bStatus.ToString();
                arrParam[7] = null;
                arrParam[8] = strLastEditor;

                string strReturn = CommandApi.CallMethodPost("UpdateNetCourseware", arrParam);
                if (string.IsNullOrEmpty(strReturn))
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(strReturn);
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("UpdateNetCoursewareStatus", e.Message);
            }
            return 0;
        }

        //获取上一堂课登录信息
        public LastLoginInfoV51M GetLastLoginInfoByTeacherID(string strCourseClassID)
        {
            try
            {
                XmlDocument xmlDoc;
                string strWebServiceURL = "";
                string strParam = "";
                string strReturn = "";

                LastLoginInfoV51M lastInfo = new LastLoginInfoV51M();

                //根据CoursePlianID/TeacherID/Subject获取教师上一堂课的登录信息！
                strWebServiceURL = "http://" + mNetTeachApiIP + ":" + mNetTeachApiPort + "/" + mNetTeachApiVirDir + "jxWebService.asmx/WS_G_LastLoginInfoByTeacherID";
                strParam = "strTeacherID=" + mTeacherID + "&strCoursePlanID=" + strCourseClassID + "&strTermYear=" + mTermYear + "&m_intSubjectID=" + mMySubjectID;
                //WriteErrorMessage("GetLastLoginInfoByTeacherID参数", strWebServiceURL+strParam);
                strReturn = mCommandWS.CallMethodPost(strWebServiceURL, strParam);
               // WriteErrorMessage("GetLastLoginInfoByTeacherID结果", strReturn);
                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);

                XmlNodeList list = xmlDoc.GetElementsByTagName("anyType");
                if (list != null && list.Count > 0)
                {
                    XmlNode node = list[0];
                    lastInfo.TeacherID = node.ChildNodes[0].InnerXml;
                    lastInfo.CourseID = node.ChildNodes[1].InnerXml;
                    lastInfo.CoursePlanIDs = node.ChildNodes[2].InnerXml;
                    DateTime dt = DateTime.Now;
                    if (DateTime.TryParse(node.ChildNodes[3].InnerXml, out dt) == true)
                    {
                        lastInfo.ClassStartTime = dt;
                    }
                    if (DateTime.TryParse(node.ChildNodes[4].InnerXml, out dt) == true)
                    {
                        lastInfo.ClassEndTime = dt;
                    }
                    lastInfo.LoginIP = node.ChildNodes[5].InnerXml;
                    lastInfo.TermYear = node.ChildNodes[6].InnerXml;

                    if (string.IsNullOrEmpty(node.ChildNodes[7].InnerXml) == false)
                    {
                        lastInfo.ClassroomID = Convert.ToInt32(node.ChildNodes[7].InnerXml);
                    }
                    else
                    {
                        lastInfo.ClassroomID = 0;
                    }
                    if (string.IsNullOrEmpty(node.ChildNodes[8].InnerXml) == false)
                    {
                        lastInfo.LoginClassID = Convert.ToInt32(node.ChildNodes[8].InnerXml);
                    }
                    else
                    {
                        lastInfo.LoginClassID = 0;
                    }
                }

                return lastInfo;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetLastLoginInfoByTeacherID", e.Message);
            }

            return null;
        }

        //获取上一堂课登录信息
        public LastLoginInfoV51M GetLastLoginInfoByLoginID(int intLastLoginID)
        {
            try
            {
                XmlDocument xmlDoc;
                string strWebServiceURL = "";
                string strParam = "";
                string strReturn = "";

                LastLoginInfoV51M lastInfo = new LastLoginInfoV51M();

                //根据CoursePlianID/TeacherID/Subject获取教师上一堂课的登录信息！
                strWebServiceURL = "http://" + mNetTeachApiIP + ":" + mNetTeachApiPort + "/" + mNetTeachApiVirDir + "jxWebService.asmx/WS_G_LastLoginInfoByLoginID";
                strParam = "iLoginID=" + intLastLoginID.ToString();
                //WriteErrorMessage("GetLastLoginInfoByTeacherID参数", strWebServiceURL+strParam);
                strReturn = mCommandWS.CallMethodPost(strWebServiceURL, strParam);
                // WriteErrorMessage("GetLastLoginInfoByTeacherID结果", strReturn);
                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);

                XmlNodeList list = xmlDoc.GetElementsByTagName("anyType");
                if (list != null && list.Count > 0)
                {
                    XmlNode node = list[0];
                    lastInfo.TeacherID = node.ChildNodes[0].InnerXml;
                    lastInfo.CourseID = node.ChildNodes[1].InnerXml;
                    lastInfo.CoursePlanIDs = node.ChildNodes[2].InnerXml;
                    DateTime dt = DateTime.Now;
                    if (DateTime.TryParse(node.ChildNodes[3].InnerXml, out dt) == true)
                    {
                        lastInfo.ClassStartTime = dt;
                    }
                    if (DateTime.TryParse(node.ChildNodes[4].InnerXml, out dt) == true)
                    {
                        lastInfo.ClassEndTime = dt;
                    }
                    lastInfo.LoginIP = node.ChildNodes[5].InnerXml;
                    lastInfo.TermYear = node.ChildNodes[6].InnerXml;

                    if (string.IsNullOrEmpty(node.ChildNodes[7].InnerXml) == false)
                    {
                        lastInfo.ClassroomID = Convert.ToInt32(node.ChildNodes[7].InnerXml);
                    }
                    else
                    {
                        lastInfo.ClassroomID = 0;
                    }
                    if (string.IsNullOrEmpty(node.ChildNodes[8].InnerXml) == false)
                    {
                        lastInfo.LoginClassID = Convert.ToInt32(node.ChildNodes[8].InnerXml);
                    }
                    else
                    {
                        lastInfo.LoginClassID = 0;
                    }
                }

                return lastInfo;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetLastLoginInfoByTeacherID", e.Message);
            }

            return null;
        }

        //获取上一堂课信息
        public LastClassInfoV51M GetLastCourseInfoV51(int intLastLoginID)
        {
            try
            {
                XmlDocument xmlDoc;
                string strWebServiceURL = "";
                string strParam = "";
                string strReturn = "";

                LastClassInfoV51M lastInfo = new LastClassInfoV51M();

                //查找教师上一堂课的教学资料、教学模块信息，返回值用@#隔开，如使用网络化课件上课可以返回课件ID！
                strWebServiceURL = "http://" + mNetTeachApiIP + ":" + mNetTeachApiPort + "/" + mNetTeachApiVirDir + "jxWebService.asmx/WS_G_LastCourseModInfoV51 ";
                strParam = "intLoginClassID=" + intLastLoginID;
                strReturn = mCommandWS.CallMethodPost(strWebServiceURL, strParam);

                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);

                XmlNodeList nodeList = xmlDoc.GetElementsByTagName("string");
                if (nodeList != null && nodeList.Count > 0)
                {
                    string strNodeInnerXml = nodeList[0].InnerXml;
                    if (string.IsNullOrEmpty(strNodeInnerXml) == false)
                    {
                        string[] arrInnerXml = strNodeInnerXml.Split(new string[1] { "@#" }, StringSplitOptions.None);
                        lastInfo.LastLoginID = intLastLoginID;
                        lastInfo.LastClassModelName = arrInnerXml[4];
                        lastInfo.LastClassModelID = string.IsNullOrEmpty(arrInnerXml[5]) ? 0 : Convert.ToInt32(arrInnerXml[5]);
                        lastInfo.LastUseingTeachModeName = arrInnerXml[3];
                        lastInfo.LastUseingTeachModeID = string.IsNullOrEmpty(arrInnerXml[2]) ? 0 : Convert.ToInt32(arrInnerXml[2]);
                        lastInfo.LastUseingNetCourseWareID = arrInnerXml[1];
                        lastInfo.LastUseingResourceID = arrInnerXml[0];
                    }
                }

                //根据条件获取教师上一堂最后一次资料搜索所使用资料V51
                strWebServiceURL = "http://" + mNetTeachApiIP + ":" + mNetTeachApiPort + "/" + mNetTeachApiVirDir + "jxWebService.asmx/WS_G_LastCourseResInfoV51 ";
                strParam = "intLoginClassID=" + intLastLoginID;
                strReturn = mCommandWS.CallMethodPost(strWebServiceURL, strParam);

                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);

                XmlNodeList list = xmlDoc.GetElementsByTagName("anyType");
                if (list != null && list.Count > 0)
                {
                    int iCount = list.Count;
                    if (iCount > 0)
                    {
                        string[] arrOrigResourceID = new string[iCount];
                        int[] arrOrigTypeID = new int[iCount];
                        string[] arrOriTypeName = new string[iCount];
                        string[] arResourceContent = new string[iCount];
                        string[] arrLastNetCoursewareID = new string[iCount];
                        string[] arrLastNetCoursewareName = new string[iCount];
                        for (int i = 0; i < iCount; i++)
                        {
                            XmlNode node = list[i];
                            //这里由于selectNodes用不了，所以只能通过下标访问，但这样不安全
                            arrOrigResourceID[i] = node.ChildNodes[0].InnerXml;
                            if (string.IsNullOrEmpty(node.ChildNodes[1].InnerXml) == false)
                            {
                                arrOrigTypeID[i] = Convert.ToInt32(node.ChildNodes[1].InnerXml);
                            }
                            else
                            {
                                arrOrigTypeID[i] = 0;
                            }
                            arrOriTypeName[i] = node.ChildNodes[2].InnerXml;
                            arResourceContent[i] = node.ChildNodes[3].InnerXml;
                            arrLastNetCoursewareID[i] = node.ChildNodes[4].InnerXml;
                            arrLastNetCoursewareName[i] = node.ChildNodes[5].InnerXml;
                        }

                        lastInfo.OrigResourceID = arrOrigResourceID;
                        lastInfo.OrigTypeID = arrOrigTypeID;
                        lastInfo.OriTypeName = arrOriTypeName;
                        lastInfo.LastNetCoursewareID = arrLastNetCoursewareID;
                        lastInfo.LastNetCoursewareName = arrLastNetCoursewareName;
                    }
                }

                return lastInfo;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetLastCourseInfoV51", e.Message);
            }

            return null;
        }

        //获取资料类型的ID和名称
        public bool GetResourceTypeIDandName(E_OriResourType eResourceType, out int iTypeID, out string strTypeName)
        {
            iTypeID = 0;
            strTypeName = "";
            try
            {
                WriteDebugInfo("GetResourceTypeIDandName","进入。eResourceType=" + eResourceType.ToString());

                switch ((int)eResourceType)
                {
                    case 1:
                        iTypeID = 1;
                        strTypeName = "图文教材库";
                        break;
                    case 2:
                        iTypeID = 2;
                        strTypeName = "多媒体教程库";
                        break;
                    case 3:
                        iTypeID = 3;
                        strTypeName = "公共媒体库";
                        break;
                    case 4:
                        iTypeID = 4;
                        strTypeName = "作业库";
                        break;
                    case 5:
                        iTypeID = 5;
                        strTypeName = "情景会话库";
                        break;
                    case 6:
                        iTypeID = 6;
                        strTypeName = "水平试题库";
                        break;
                    case 7:
                        iTypeID = 7;
                        strTypeName = "知识点课件库";
                        break;
                    case 8:
                        iTypeID = 8;
                        strTypeName = "主题背景库";
                        break;
                    case 9:
                        iTypeID = 9;
                        strTypeName = "本地电脑";
                        break;
                    case 10:
                        iTypeID = 10;
                        strTypeName = "翻译库";
                        break;
                    case 11:
                        iTypeID = 11;
                        strTypeName = "电子资源库";
                        break;
                    case 12:
                        iTypeID = 12;
                        strTypeName = "数字化资源库";
                        break;
                    case 13:
                        iTypeID = 13;
                        strTypeName = "网络化课件库";
                        break;
                    case 14:
                        iTypeID = 14;
                        strTypeName = "U盘";
                        break;
                    case 15:
                        iTypeID = 15;
                        strTypeName = "专用教材";
                        break;
                    case 16:
                        iTypeID = 16;
                        strTypeName = "课前预习";
                        break;
                    case 17:
                        iTypeID = 17;
                        strTypeName = "课堂教案";
                        break;
                    case 18:
                        iTypeID = 18;
                        strTypeName = "课后练习";
                        break;
                    case 19:
                        iTypeID = 19;
                        strTypeName = "智能化课件";
                        break;
                    case 20:
                        iTypeID = 20;
                        strTypeName = "智能组卷试卷";
                        break;
                    case 21:
                        iTypeID = 21;
                        strTypeName = "随堂测试卷";
                        break;
                    case 22:
                        iTypeID = 22;
                        strTypeName = "教学方案";
                        break;
                    case 23:
                        iTypeID = 23;
                        strTypeName = "课前预习方案";
                        break;
                    case 24:
                        iTypeID = 24;
                        strTypeName = "课堂教案V2";
                        break;
                    case 25:
                        iTypeID = 25;
                        strTypeName = "课后作业方案";
                        break;
                    case 26:
                        iTypeID = 26;
                        strTypeName = "课文讲解课件";
                        break;
                    case 27:
                        iTypeID = 27;
                        strTypeName = "重难点讲解课件";
                        break;
                    case 28:
                        iTypeID = 28;
                        strTypeName = "本地资料课件集";
                        break;
                    case 29:
                        iTypeID = 29;
                        strTypeName = "智能化课件组成";
                        break;
                }

                return true;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetResourceTypeIDandName", e.Message);
            }
            return false;
        }

        //10:根据Jx_loginClassId+StudentID 查找出学生已加多少分。
        public int GetStudentAddScoreByCourseID(string StudentId)
        {
            try
            {
                XmlDocument xmlDoc;
                string strWebServiceURL = "";
                string strParam = "";
                string strReturn = "";

                strWebServiceURL = "http://" + mNetTeachApiIP + ":" + mNetTeachApiPort + "/" + mNetTeachApiVirDir + "jxWebService.asmx/WS_G_StudentCourseTotalAddScore";
                //当教师手动加分是m_strLoginModelOperId为空
                strParam = "strXH=" + StudentId + "&strCourseID=" + mCourseID + "&strCoursePlanID=" + mCourseClassID + "&m_intSubjectID=" + mMySubjectID;
                WriteDebugInfo("GetStudentAddScoreByCourseID", strWebServiceURL + " " + strParam);
                strReturn = mCommandWS.CallMethodPost(strWebServiceURL, strParam);

                if (string.IsNullOrEmpty(strReturn))
                {
                    return -1;
                }

                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);
                string strResult = xmlDoc.GetElementsByTagName("double").Item(0).InnerText;

                if (string.IsNullOrEmpty(strResult) == false)
                {
                    return Convert.ToInt32(strResult);
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetStudentAddScoreByCourseID", e.Message);
            }
            return -1;
        }
        /// <summary>
        /// 获取课堂内总加分值（学生端使用）
        /// </summary>
        /// <param name="StudentId">学号</param>
        /// <param name="CourseID">课程ID</param>
        /// <param name="CourseClassID">课程班ID</param>
        /// <param name="SubjectID">学科ID,语文-1，数学-2；英语-3</param>
        /// <returns>课堂加分值</returns>
        public int GetStudentAddScore(string StudentId,string CourseID,string CourseClassID,string SubjectID)
        {
            try
            {
                XmlDocument xmlDoc;
                string strWebServiceURL = "";
                string strParam = "";
                string strReturn = "";

                strWebServiceURL = "http://" + mNetTeachApiIP + ":" + mNetTeachApiPort + "/" + mNetTeachApiVirDir + "jxWebService.asmx/WS_G_StudentCourseTotalAddScore";
                //当教师手动加分是m_strLoginModelOperId为空
                strParam = "strXH=" + StudentId + "&strCourseID=" + CourseID + "&strCoursePlanID=" + CourseClassID + "&m_intSubjectID=" + SubjectID;
                WriteDebugInfo("GetStudentAddScore", strWebServiceURL + " " + strParam);
                strReturn = mCommandWS.CallMethodPost(strWebServiceURL, strParam);

                if (string.IsNullOrEmpty(strReturn))
                {
                    return -1;
                }

                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);
                string strResult = xmlDoc.GetElementsByTagName("double").Item(0).InnerText;

                if (string.IsNullOrEmpty(strResult) == false)
                {
                    return Convert.ToInt32(strResult);
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetStudentAddScore", e.Message);
            }
            return -1;
        }
        //根据首字母获取对应的单词（多个值之间用@@分隔）
        public string GetWordInfoByKeyword(string strKeyWord)
        {
            try
            {
                XmlDocument xmlDoc;

                string strParam = "";
                strParam = "word={0}&iFlag={1}";
                strParam = string.Format(strParam, strKeyWord, C_SecCode);

                string strURL = "http://{0}:{1}/WS_Knowledge/Service_Konwledge.asmx/Get_WordBySearchkey";
                strURL = string.Format(strURL, m_strKnowledgeWSIP, m_lngKnowledgeWSPort);

                string strReturn = mCommandWS.CallMethodPost(strURL, strParam);

                if (string.IsNullOrEmpty(strReturn))
                {
                    WriteErrorMessage("GetWordInfoByKeyword", "strReturn为空");
                    return "";
                }

                xmlDoc = new System.Xml.XmlDocument();
                xmlDoc.LoadXml(strReturn);
                string strResult = xmlDoc.GetElementsByTagName("string").Item(0).InnerText;

                return strResult;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetWordInfoByKeyword", e.Message);
            }
            return "";
        }

        //New 1 新增xiezongwu20150527根据教材名称，单元名称获取该单元包含的生词
        //参数strBookName=教材名称，strUnitName=单元或者model名称
        //返回值，返回单词的内容，以@#分隔
        public string GetWordByUnit(string strBookName, string strUnitName)
        {
            try
            {
                XmlDocument xmlDoc;
                string strWebServiceURL = "http://" + mNetTeachApiIP + ":" + mNetTeachApiPort + "/" + mNetTeachApiVirDir + "jxWebService.asmx/WS_G_WordByBookUnit";
                string strParam = "strBookName=" + strBookName + "&strUnitName=" + strUnitName;
                string strReturn = mCommandWS.CallMethodPost(strWebServiceURL, strParam);
                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);
                string strResult = xmlDoc.GetElementsByTagName("string")[0].InnerText;

                return strResult;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetWordByUnit", e.Message);
            }

            return "";
        }

        //New 2 新增xiezongwu20150530根据主题词搜索主题背景视频
        //参数：strTheme（主题名），strThemeWords（主题关键字）
        //返回值：该主题关键或主题关键字对应的主题XMLDocument
        public bool GetTVideoByThemeOrWords(string strTheme, string strThemeWords, out string strReturnXml)
        {
            strReturnXml = "";
            try
            {
                string strWebServiceURL = "";
                string strParam = "";

                if (string.IsNullOrEmpty(strTheme) == false && string.IsNullOrEmpty(strThemeWords) == true)
                {
                    if (string.IsNullOrEmpty(mThemeVideoVirDir) == true)
                        strWebServiceURL = "http://" + mThemeVideoWSIP + ":" + mThemeVideoPort + "/Service1.asmx/VideosFromTheme";
                    else
                        strWebServiceURL = "http://" + mThemeVideoWSIP + ":" + mThemeVideoPort +"/"+ mThemeVideoVirDir +"Service1.asmx/VideosFromTheme";
                    strParam = "themeName=" + strTheme;
                }
                else if (string.IsNullOrEmpty(strTheme) == false && string.IsNullOrEmpty(strThemeWords) == false)
                {
                    if (string.IsNullOrEmpty(mThemeVideoVirDir) == true)
                        strWebServiceURL = "http://" + mThemeVideoWSIP + ":" + mThemeVideoPort + "/Service1.asmx/VideosFromThemeAndThemeWords";
                    else
                        strWebServiceURL = "http://" + mThemeVideoWSIP + ":" + mThemeVideoPort + "/" + mThemeVideoVirDir + "Service1.asmx/VideosFromThemeAndThemeWords";
                    strParam = "themeName=" + strTheme + "&words=" + strThemeWords;
                }
                else
                {
                    if (string.IsNullOrEmpty(mThemeVideoVirDir) == true)
                        strWebServiceURL = "http://" + mThemeVideoWSIP + ":" + mThemeVideoPort + "/Service1.asmx/VideosFromThemeWords";
                    else
                        strWebServiceURL = "http://" + mThemeVideoWSIP + ":" + mThemeVideoPort +"/"+ mThemeVideoVirDir+ "Service1.asmx/VideosFromThemeWords";
                    strParam = "words=" + strThemeWords;
                }

                string strReturn = mCommandWS.CallMethodPost(strWebServiceURL, strParam);
                if (string.IsNullOrEmpty(strReturn) == false)
                {
                    strReturnXml = strReturn;
                }
                
                return true;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetTVideoByThemeOrWords", e.Message);
            }

            return false;
        }

        //重载
        public bool GetTVideoByThemeOrWords(string strTheme, out string strReturnXml)
        {
            strReturnXml = "";
            try
            {
                return GetTVideoByThemeOrWords(strTheme, "", out strReturnXml);
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetTVideoByThemeOrWords", e.Message);
            }

            return false;
        }
        #endregion

        #endregion



        #region 私有方法

        //获取学科ID（本系统）
        private byte GetMySubjectID(E_Subject eSubject)
        {
            try
            {
                byte bSubjectID = 0;
                string strSubjectName = "";
                switch ((int)eSubject)
                {
                    case 0:
                        return bSubjectID;
                    case 1:
                        strSubjectName = "语文";
                        break;
                    case 2:
                        strSubjectName = "数学";
                        break;
                    case 3:
                        strSubjectName = "英语";
                        break;
                    case 4:
                        strSubjectName = "物理";
                        break;
                    case 5:
                        strSubjectName = "化学";
                        break;
                    case 6:
                        strSubjectName = "生物";
                        break;
                    case 7:
                        strSubjectName = "政治";
                        break;
                    case 8:
                        strSubjectName = "历史";
                        break;
                    case 9:
                        strSubjectName = "地理";
                        break;
                    case 10:
                        strSubjectName = "科学";
                        break;
                    case 11:
                        strSubjectName = "美术";
                        break;
                }
                
                string[] arrParam = new string[1];
                arrParam[0] = strSubjectName;

                string strReturn = mCommandApi.CallMethodGet("SelectSubjectIDByName", arrParam);
                if (string.IsNullOrEmpty(strReturn) == false)
                {
                    bSubjectID = Convert.ToByte(strReturn);
                    return bSubjectID;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetSubjectID", e.Message);
            }

            return 0;
        }

        //根据枚举从云平台获取学科ID（云平台与本系统学科ID有所不同）
        private string GetSubjectIDFromCloudPlatform(E_Subject eSubject)
        {
            try
            {
                string strPartialSubjectID = "";
                //以下为基础平台定义的两个特殊学科
                string sciSubId = "Science";//科学
                string artSubId = "Art";//美术
                string strSubjectName="";

                switch(mSchoolLevel)
                {
                    case "":
                        break;
                    case "1"://大学
                        strPartialSubjectID="S1";
                        break;
                    case "2"://中小学
                        strPartialSubjectID="S2";
                        break;
                }
                try
                {
                    if (clpSysConfigInfo == null)
                        clpSysConfigInfo = GetCLPSysConfigInfo();
                    if (clpSysConfigInfo != null)
                    {
                        if (clpSysConfigInfo.useRange == "5")
                            strPartialSubjectID = "S3";
                        else if (clpSysConfigInfo.useRange == "6")
                            strPartialSubjectID = "S4";
                    }
                }
                catch (Exception e)
                {
                    WriteErrorMessage("GetSubjectIDFromCloudPlatform", e.ToString());
                }
                string strWholeUrl = "";
                string strMethod = "";
                string strParam = "";
                string[] arrParam = null;
                string strReturn = "";

                strSubjectName = GetSubjectName(eSubject);

                strWholeUrl = "http://{0}/UserMgr/TeachInfoMgr/Api/Service_TeachInfo.ashx?token={1}&method={2}{3}";
                strMethod = "GetSubjects";
                strParam = "&params=" + mSchoolID;
                arrParam = new string[4];
                arrParam[0] = mCloudPlatformBFUrl;
                arrParam[1] = mToken;
                arrParam[2] = strMethod;
                arrParam[3] = strParam;
                strWholeUrl = string.Format(strWholeUrl, arrParam);
                strReturn = mCommandApi.CallMethodGet(strWholeUrl);

                if (string.IsNullOrEmpty(strReturn) == false)
                {
                    int iFlag = UtilityClass.AnalyseCloudJson(ref strReturn);
                    if (iFlag == 3)
                    {
                        if (EventInvalidToken != null)
                        {
                            EventInvalidToken();
                            return null;
                        }
                    }

                    CloudPlatformSubjectM[] subjects = JsonFormatter.JsonDeserialize<CloudPlatformSubjectM[]>(strReturn);

                    if (subjects != null)
                    {
                        for (int i = 0; i < subjects.Length; i++)
                        {
                            if (subjects[i].SubjectName == strSubjectName)
                            {
                                if (subjects[i].SubjectID.Contains(strPartialSubjectID) || subjects[i].SubjectID.Contains(sciSubId) || subjects[i].SubjectID.Contains(artSubId))
                                {
                                    mCloudPlatformSubjectID = subjects[i].SubjectID;
                                    mCloudPlatformSubjectName = subjects[i].SubjectName;
                                }
                            }
                        }
                    }
                }

                if (mCloudPlatformSubjectID == null)
                {
                    mCloudPlatformSubjectID = "";
                }

                return mCloudPlatformSubjectID;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetSubjectIDFromCloudPlatform", e.Message);
            }
            return "";
        }

        //根据当前教师所属学校ID更新学校信息
        private void P_UpdateSchoolInfoByTeacher()
        {
            try
            {
                TeacherInfoDetailM teacher = GetTeacherInfoDetail();
                string strSchoolID = "";
                if (teacher != null)
                {
                    strSchoolID = teacher.SchoolID;
                }

                //如果教师所属学校ID与当前学校ID不同，则更新学校信息
                if (string.IsNullOrEmpty(strSchoolID) == false && strSchoolID != mSchoolID)
                {
                    //根据教师所属学校ID，修改当前的学校信息
                    SchoolBaseInfoM school = GetSchoolInfoByID(strSchoolID);

                    if (school != null)
                    {
                        //更新学校信息
                        P_SetSchoolInfo(school);

                        //根据修改后的学校信息，修改云平台学科信息（因为云平台学科ID与学校类型相关）
                        P_SetCloudPlatformSubject();
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("P_UpdateSchoolInfoByTeacher", e.Message);
            }
        }

        //赋值学校信息相关的成员变量
        private void P_SetSchoolInfo(SchoolBaseInfoM SchoolInfo)
        {
            if (SchoolInfo == null)
            {
                return;
            }

            mSchoolID = SchoolInfo.SchoolID;
            mSchoolName = SchoolInfo.SchoolName;
            mSchoolLevel = SchoolInfo.SchoolLevel;
            mSchoolType = SchoolInfo.SchoolType;

            if (mSchoolLevel == "1")
            {
                mMySchoolType = 4;
            }
            else if (mSchoolLevel == "2")
            {
                switch (mSchoolType)
                {
                    case "1":
                        mMySchoolType = 1;
                        break;
                    case "2":
                        mMySchoolType = 2;
                        break;
                    case "3":
                        mMySchoolType = 5;
                        break;
                    case "4":
                        mMySchoolType = 3;
                        break;
                    case "5":
                        mMySchoolType = 7;
                        break;
                    case "6":
                        mMySchoolType = 6;
                        break;
                    case "7":
                        mMySchoolType = 8;
                        break;
                }
            }
        }

        //赋值云平台学科相关成员变量（根据当前学校类型和枚举型学科）
        private void P_SetCloudPlatformSubject()
        {
            try
            {
                if (clpSysConfigInfo == null)
                    clpSysConfigInfo = GetCLPSysConfigInfo();
                if (clpSysConfigInfo != null)
                {
                    //5：中职，6：高职(中、高职只有英语学科)
                    if (clpSysConfigInfo.useRange == "5")
                    {
                        if (mSubject == E_Subject.英语)
                        {
                            mCloudPlatformSubjectID = "S3-English";
                            mCloudPlatformSubjectName = "英语";
                            return;
                        }
                    }
                    else if (clpSysConfigInfo.useRange == "6")
                    {
                        if (mSubject == E_Subject.英语)
                        {
                            mCloudPlatformSubjectID = "S4-English";
                            mCloudPlatformSubjectName = "英语";
                            return;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("P_SetCloudPlatformSubject", e.ToString());
            }
            switch (mSubject)
            {
                case E_Subject.语文:
                    switch (mSchoolLevel)
                    {
                        case "1"://大学
                            mCloudPlatformSubjectID = "S1-Chinese";
                            mCloudPlatformSubjectName = "语文";
                            break;
                        case "2"://中小学
                            mCloudPlatformSubjectID = "S2-Chinese";
                            mCloudPlatformSubjectName = "语文";
                            break;
                    }
                    break;
                case E_Subject.数学:
                    switch (mSchoolLevel)
                    {
                        case "1"://大学
                            mCloudPlatformSubjectID = "S1-Maths";
                            mCloudPlatformSubjectName = "数学";
                            break;
                        case "2"://中小学
                            mCloudPlatformSubjectID = "S2-Maths";
                            mCloudPlatformSubjectName = "数学";
                            break;
                    }
                    break;
                case E_Subject.英语:
                    switch (mSchoolLevel)
                    {
                        case "1"://大学
                            mCloudPlatformSubjectID = "S1-English";
                            mCloudPlatformSubjectName = "英语";
                            break;
                        case "2"://中小学
                            mCloudPlatformSubjectID = "S2-English";
                            mCloudPlatformSubjectName = "英语";
                            break;
                    }
                    break;
                case E_Subject.物理:
                    switch (mSchoolLevel)
                    {
                        case "1"://大学
                            mCloudPlatformSubjectID = "S1-Physics";
                            mCloudPlatformSubjectName = "物理";
                            break;
                        case "2"://中小学
                            mCloudPlatformSubjectID = "S2-Physics";
                            mCloudPlatformSubjectName = "物理";
                            break;
                    }
                    break;
                case E_Subject.化学:
                    switch (mSchoolLevel)
                    {
                        case "1"://大学
                            mCloudPlatformSubjectID = "S1-Chemistry";
                            mCloudPlatformSubjectName = "化学";
                            break;
                        case "2"://中小学
                            mCloudPlatformSubjectID = "S2-Chemistry";
                            mCloudPlatformSubjectName = "化学";
                            break;
                    }
                    break;
                case E_Subject.生物:
                    switch (mSchoolLevel)
                    {
                        case "1"://大学
                            mCloudPlatformSubjectID = "S1-Biology";
                            mCloudPlatformSubjectName = "生物";
                            break;
                        case "2"://中小学
                            mCloudPlatformSubjectID = "S2-Biology";
                            mCloudPlatformSubjectName = "生物";
                            break;
                    }
                    break;
                case E_Subject.政治:
                    switch (mSchoolLevel)
                    {
                        case "1"://大学
                            mCloudPlatformSubjectID = "S1-Politics";
                            mCloudPlatformSubjectName = "政治";
                            break;
                        case "2"://中小学
                            mCloudPlatformSubjectID = "S2-Politics";
                            mCloudPlatformSubjectName = "政治";
                            break;
                    }
                    break;
                case E_Subject.历史:
                    switch (mSchoolLevel)
                    {
                        case "1"://大学
                            mCloudPlatformSubjectID = "S1-History";
                            mCloudPlatformSubjectName = "历史";
                            break;
                        case "2"://中小学
                            mCloudPlatformSubjectID = "S2-History";
                            mCloudPlatformSubjectName = "历史";
                            break;
                    }
                    break;
                case E_Subject.地理:
                    switch (mSchoolLevel)
                    {
                        case "1"://大学
                            mCloudPlatformSubjectID = "S1-Geography";
                            mCloudPlatformSubjectName = "地理";
                            break;
                        case "2"://中小学
                            mCloudPlatformSubjectID = "S2-Geography";
                            mCloudPlatformSubjectName = "地理";
                            break;
                    }
                    break;
                case E_Subject.美术:
                    mCloudPlatformSubjectID = "Art";
                    mCloudPlatformSubjectName = "美术";
                    break;
                case E_Subject.科学:
                    mCloudPlatformSubjectID = "Science";
                    mCloudPlatformSubjectName = "科学";
                    break;
                default:
                    mCloudPlatformSubjectID = "";
                    mCloudPlatformSubjectName = "";
                    break;
            }
        }

        //根据学科和教学模式枚举来唯一确定教学模式ID
        //学科根据成员变量mSubject
        private short GetTeachModeIDWithSubject(E_TeachClass_Mode eTeachMode)
        {
            try
            {
                short sTeachModeID = 0;
                switch (eTeachMode)
                {
                    case E_TeachClass_Mode.课文讲解:
                        if (mSubject == E_Subject.英语)
                        {
                            sTeachModeID = 1;
                        }
                        else if (mSubject == E_Subject.语文)
                        {
                            sTeachModeID = 9;
                        }
                        break;
                    case E_TeachClass_Mode.听力讲解:
                        sTeachModeID = 2;
                        break;
                    case E_TeachClass_Mode.口语教学:
                        sTeachModeID = 3;
                        break;
                    case E_TeachClass_Mode.阅读训练:
                        if (mSubject == E_Subject.语文)
                        {
                            sTeachModeID = 13;
                        }
                        else if (mSubject == E_Subject.英语)
                        {
                            sTeachModeID = 4;
                        }
                        break;
                    case E_TeachClass_Mode.随堂听力测试:
                        sTeachModeID = 5;
                        break;
                    case E_TeachClass_Mode.情景会话:
                        sTeachModeID = 6;
                        break;
                    case E_TeachClass_Mode.协作写作:
                        sTeachModeID = 7;
                        break;
                    case E_TeachClass_Mode.课堂辅导:
                        sTeachModeID = 8;
                        break;
                    case E_TeachClass_Mode.随堂测试:
                        sTeachModeID = 10;
                        break;
                    case E_TeachClass_Mode.复习辅导:
                        sTeachModeID = 11;
                        break;
                    case E_TeachClass_Mode.协作设计制作:
                        sTeachModeID = 12;
                        break;
                    case E_TeachClass_Mode.知识竞答:
                        sTeachModeID = 14; 
                        break;
                    case E_TeachClass_Mode.随堂综合测试:
                        sTeachModeID = 15;
                        break;
                }
                return sTeachModeID;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetTeachModeIDWithSubject", e.Message);
            }
            return 0;
        }

        //（从本系统WS服务获取）获取各应用系统数据接口地址（主题背景库（ThemeVideo）、作文评分（eassy）、知识点识别（ zsd）、资源库（PubResource）、根据知识库获取知识点数据信息（DBIP NAME PWD）、通过资源库WS获取资源库DB、Ftp、Http信息）
        private bool P_GetOtherSysService()
        {
            try
            {
                string strWebServiceURL = "";
                string strParam = "";
                string strReturn = "";

                XmlDocument xmlDoc;

                string strSysID = "";
                strWebServiceURL = "http://" + mNetTeachApiIP + ":" + mNetTeachApiPort + "/" + mNetTeachApiVirDir + "jxWebService.asmx/WS_G_OtherSysInfo ";
                strParam = "SysID=" + strSysID + "&SecCode=" + C_SecCode;
                strReturn = mCommandWS.CallMethodPost(strWebServiceURL, strParam);

                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);
                XmlNodeList list = xmlDoc.GetElementsByTagName("anyType");

                int iCount = 0;
                if (list != null)
                {
                    iCount = list.Count;
                }

                if (iCount > 0)
                {
                    string strTempWSAddr = "";
                    string strTempWebAddr = "";
                    string[] arrTemp;
                    for (int i = 0; i < iCount; i++)
                    {
                        XmlNode node = list[i];

                        strTempWSAddr = ClsParamsEncDec.LgMgr_ParamDecrypt(node.ChildNodes[1].InnerText);
                        strTempWebAddr = ClsParamsEncDec.LgMgr_ParamDecrypt(node.ChildNodes[2].InnerText);
                        switch (node.ChildNodes[0].InnerText)
                        {
                            case "Basic":
                                break;
                            case "CSetup":
                                break;
                            case "Essay"://作文评分
                                if (string.IsNullOrEmpty(strTempWSAddr) == false)
                                {
                                    //P_FormatIPandPort(strTempWSAddr, out m_strEssayWSIP, out m_lngEssayWSPort);
                                    P_FormatIPandPort(strTempWSAddr, out m_strEssayWSIP_IR, out m_lngEssayWSPort_IR, out m_strEssayVirDir_IR);
                                }
                                break;
                            case "FreeStudy":
                                break;
                            case "OralTest"://口语考试
                                if (string.IsNullOrEmpty(strTempWSAddr) == false)
                                {
                                    P_FormatIPandPort(strTempWSAddr, out m_strOTServiceIP, out m_lngOTServicePort);
                                }
                                break;
                            case "PubResource"://资源库
                                //修改资源库地址从基础平台获取 ModifiedByQinkun@20171030
                                //if (string.IsNullOrEmpty(strTempWSAddr) == false)
                                //{
                                //    P_FormatIPandPort(strTempWSAddr, out m_strZYWSIP, out m_lngZYWSPort);
                                //}
                                break;
                            case "SkillTrain"://技能训练
                                if (string.IsNullOrEmpty(strTempWSAddr) == false)
                                {
                                    P_FormatIPandPort(strTempWSAddr, out mSkillTrainIP, out mSkillTrainPort,out mSkillTrainVirDir);
                                }
                                break;
                            case "ThemeVideo"://主题背景库
                                if (string.IsNullOrEmpty(strTempWSAddr) == false)
                                {
                                    long lThemeVideoPort =0;
                                    P_FormatIPandPort(strTempWSAddr, out mThemeVideoWSIP, out lThemeVideoPort, out mThemeVideoVirDir);
                                    mThemeVideoPort = lThemeVideoPort.ToString();
                                }
                                break;
                            case "VOD":
                                break;
                            case "ZSD"://知识点识别
                                if (mSubject == E_Subject.英语)
                                {
                                    if (string.IsNullOrEmpty(strTempWSAddr) == false)
                                    {
                                        P_FormatIPandPort(strTempWSAddr, out m_strKnowledgeWSIP, out m_lngKnowledgeWSPort);
                                    }
                                }
                                break;
                            case "CHZSD":
                                //非英语学科知识点服务器的获取使用此字段
                                if (mSubject != E_Subject.英语)
                                {
                                    if (string.IsNullOrEmpty(strTempWSAddr) == false)
                                    {
                                        P_FormatIPandPort(strTempWSAddr, out m_strKnowledgeWSIP, out m_lngKnowledgeWSPort);
                                    }
                                }
                                break;
                        }
                    }

                    return true;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("P_GetOtherSysService", e.Message);
            }

            return false;
        }

        private void P_FormatIPandPort(string strAddr, out string IP, out long Port)
        {
            IP = "";
            Port = 0;

            string strIP = "";
            string strPort = "";
            P_FormatIPandPort(strAddr, out strIP, out strPort);
            IP = strIP;
            if (string.IsNullOrEmpty(strPort) == false)
            {
                long.TryParse(strPort, out Port);
            }
        }
        //为增加获取聚合的应用程序名修改，非聚合版仍是调用无VirDir参数的
        private void P_FormatIPandPort(string strAddr, out string IP, out long Port,out string VirDir)
        {
            IP = "";
            Port = 0;
            VirDir = "";

            string strIP = "";
            string strPort = "";
            P_FormatIPandPort(strAddr, out strIP, out strPort);
            FormatPortAndVirdir(strPort,out Port,out VirDir);
            IP = strIP;
            //if (string.IsNullOrEmpty(strPort) == false)
            //{
            //    long.TryParse(strPort, out Port);
            //}
        }

        private void P_FormatIPandPort(string strAddr, out string IP, out string Port)
        {
            IP = "";
            Port = "";
            if (string.IsNullOrEmpty(strAddr))
            {
                return;
            }
            strAddr = strAddr.Replace("http://", "").TrimEnd('/');
            string[] arrTemp = strAddr.Split(':');
            if (arrTemp == null)
            {
                return;
            }
            if (arrTemp.Length > 0)
            {
                IP = arrTemp[0];
            }
            if (arrTemp.Length > 1)
            {
                Port = arrTemp[1];
            }
        }

        //根据枚举值获取系统ID（用于调用WS服务）
        private string GetOtherSysIDByName(E_OtherSysName eName)
        {
            try
            {
                string strSysID = "";
                switch ((int)eName)
                {
                    case 1:
                        strSysID = "Basic";
                        break;
                    case 2:
                        strSysID = "CSetUp";
                        break;
                    case 3:
                        strSysID = "Essay";
                        break;
                    case 4:
                        strSysID = "FreeStudy";
                        break;
                    case 5:
                        strSysID = "OralTest";
                        break;
                    case 6:
                        strSysID = "PubResource";
                        break;
                    case 7:
                        strSysID = "SkillTrain";
                        break;
                    case 8:
                        strSysID = "ThemeVideo";
                        break;
                    case 9:
                        strSysID = "VOD";
                        break;
                    case 10:
                        strSysID = "ZSD";
                        break;
                    default:
                        break;
                }
                return strSysID;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetOtherSysIDByName", e.Message);
            }

            return null;
        }
        
        //获取学校基本信息（通过WS）
        private SchoolBaseInfoM P_GetSchoolInfo_WS()
        {
            try
            {
                string strWebServiceURL = "";
                string strParam = "";
                string strReturn = "";

                XmlDocument xmlDoc;

                strWebServiceURL = "http://{0}/SysMgr/SysSetting/WS/Service_SysSetting.asmx/WS_SysMgr_G_GetSchoolBaseInfo";

                //云平台的strSubjectID跟课堂教学系统的不一样
                strParam = "schoolID=";
                strWebServiceURL = string.Format(strWebServiceURL, mCloudPlatformBFUrl);
                strReturn = mCommandWS.CallMethodPost(strWebServiceURL, strParam);

                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);
                XmlNodeList list = xmlDoc.GetElementsByTagName("anyType");

                int iCount = 0;
                if (list != null)
                {
                    iCount = list.Count;
                }
                if (iCount > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        string strState = list[i].ChildNodes[5].InnerText;
                        if (strState == "1")
                        {
                            SchoolBaseInfoM school = new SchoolBaseInfoM();
                            school.SchoolID = list[i].ChildNodes[0].InnerText;
                            school.SchoolName = list[i].ChildNodes[1].InnerText;
                            school.SchoolCode = list[i].ChildNodes[2].InnerText;
                            school.SchoolLevel = list[i].ChildNodes[3].InnerText;
                            school.SchoolType = list[i].ChildNodes[4].InnerText;
                            school.SchoolState = strState;
                            school.CreateTime = list[i].ChildNodes[6].InnerText;

                            return school;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("P_GetSchoolInfo_WS", e.Message);
            }
            return null;
        }

        //获取学校基本信息
        private SchoolBaseInfoM P_GetSchoolInfo()
        {
            try
            {
                string strWholeUrl = "";
                string strUrl = "";
                string strMethod = "";
                string strParam = "";
                string[] arrParam = null;
                string strReturn = "";

                //获取学校类型
                strWholeUrl = "http://{0}/SysMgr/SysSetting/Api/Service_SysSetting.ashx?token={1}&method={2}{3}";
                strUrl = mCloudPlatformBFUrl;
                strMethod = "GetSchoolBaseInfo";
                strParam = "&params=";
                arrParam = new string[4];
                arrParam[0] = strUrl;
                arrParam[1] = mToken;
                arrParam[2] = strMethod;
                arrParam[3] = strParam;
                strWholeUrl = string.Format(strWholeUrl, arrParam);
                strReturn = mCommandApi.CallMethodGet(strWholeUrl);

                if (string.IsNullOrEmpty(strReturn) == false)
                {
                    int iFlag = UtilityClass.AnalyseCloudJson(ref strReturn);
                    if (iFlag == 3)
                    {
                        if (EventInvalidToken != null)
                        {
                            EventInvalidToken();
                            return null;
                        }
                    }
                    SchoolBaseInfoM[] schoolInfo = JsonFormatter.JsonDeserialize<SchoolBaseInfoM[]>(strReturn);
                    int iCount = 0;
                    if (schoolInfo != null)
                    {
                        iCount = schoolInfo.Length;
                    }
                    if (iCount > 0)
                    {
                        for (int i = 0; i < iCount; i++)
                        {
                            if (schoolInfo[i].SchoolState == "1")
                            {
                                return schoolInfo[i];
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetSchoolInfo", e.Message);
            }

            return null;
        }

        //获取学期（通过WS）
        private string P_GetTermInfo_WS()
        {
            try
            {
                string strWebServiceURL = "";
                string strParam = "";
                string strReturn = "";

                XmlDocument xmlDoc;

                strWebServiceURL = "http://{0}/SysMgr/SysSetting/WS/Service_SysSetting.asmx/WS_SysMgr_G_GetTermInfo";

                //云平台的strSubjectID跟课堂教学系统的不一样
                strParam = "";
                strWebServiceURL = string.Format(strWebServiceURL, mCloudPlatformBFUrl);
                strReturn = mCommandWS.CallMethodPost(strWebServiceURL, strParam);

                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);
                strReturn = xmlDoc.GetElementsByTagName("string").Item(0).InnerText;
                return strReturn;
            }
            catch (Exception e)
            {
                WriteErrorMessage("P_GetTermInfo_WS", e.Message);
            }
            return "";
        }

        //获取学期
        private string P_GetTermInfo()
        {
            try
            {
                string strWholeUrl = "";
                string strUrl = "";
                string strMethod = "";
                string[] arrParam = null;
                string strReturn = "";

                strWholeUrl = "http://{0}/SysMgr/SysSetting/Api/Service_SysSetting.ashx?token={1}&method={2}&params={3}";
                strUrl = mCloudPlatformBFUrl;
                strMethod = "GetTermInfo";

                arrParam = new string[4];
                arrParam[0] = strUrl;
                arrParam[1] = mToken;
                arrParam[2] = strMethod;
                arrParam[3] = "";

                strWholeUrl = string.Format(strWholeUrl, arrParam);
                strReturn = mCommandApi.CallMethodGet(strWholeUrl);

                if (string.IsNullOrEmpty(strReturn) == false)
                {
                    int iFlag = UtilityClass.AnalyseCloudJson(ref strReturn);
                    if (iFlag == 3)
                    {
                        if (EventInvalidToken != null)
                        {
                            EventInvalidToken();
                            return "";
                        }
                    }
                    int index = strReturn.IndexOf(":");
                    if (index > -1)
                    {
                        strReturn = strReturn.Substring(index + 1);
                        strReturn = strReturn.Trim(']', '}', '"');
                    }
                }
                return strReturn;
            }
            catch (Exception e)
            {
                WriteErrorMessage("P_GetTermInfo", e.Message);
            }

            return "";
        }

        //资源库信息（数据库、FTP、HTTP）
        private bool P_GetZYKinfo(string strZYKAddr)
        {
            try
            {
                string strWebServiceURL = "";

                XmlDocument xmlDoc;

                string strParam = "";
                string strReturn = "";
                string[] arrReturn;

                //数据库信息
                strWebServiceURL = strZYKAddr + "ZYK/Server.asmx/WS_G_ZYK_GetDBServer";
                strParam = "";
                strReturn = mCommandWS.CallMethodPost(strWebServiceURL, strParam);

                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);
                strReturn = xmlDoc.GetElementsByTagName("string")[0].InnerText;
                strReturn = ClsParamsEncDec.LgMgr_ParamDecrypt(strReturn);

                arrReturn = strReturn.Split('|');
                if (arrReturn.Length == 4)
                {
                    m_strResourceDBIP = arrReturn[0];
                    m_strResourceDBName = arrReturn[1];
                    m_strResourceDBUserName = arrReturn[2];
                    m_strResourceDBUserPwd = arrReturn[3];
                }
                else
                {
                    WriteErrorMessage("P_GetZYKinfo", "数据库信息返回值数组长度不为4");
                }

                //FTP
                strWebServiceURL = strZYKAddr + "ZYK/Server.asmx/WS_G_ZYK_GetResFtpServer";
                strParam = "";
                strReturn = mCommandWS.CallMethodPost(strWebServiceURL, strParam);

                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);
                strReturn = xmlDoc.GetElementsByTagName("string")[0].InnerText;
                strReturn = ClsParamsEncDec.LgMgr_ParamDecrypt(strReturn);

                arrReturn = strReturn.Split('|');
                if (arrReturn.Length == 6)
                {
                    m_strFtpIPAddress = arrReturn[0];
                    m_lngFtpPort = string.IsNullOrEmpty(arrReturn[1]) ? 0 : Convert.ToInt64(arrReturn[1]);
                    m_strFtpName = arrReturn[2];
                    m_strFtpUserName = arrReturn[3];
                    m_strFtpUserPwd = arrReturn[4];
                    m_strFtpPhyPath = arrReturn[5];
                }
                else
                {
                    WriteErrorMessage("P_GetZYKinfo", "FTP信息返回值数组长度不为6");
                }

                //HTTP
                strWebServiceURL = strZYKAddr + "ZYK/Server.asmx/WS_G_ZYK_GetResHttpServer";
                strParam = "";
                strReturn = mCommandWS.CallMethodPost(strWebServiceURL, strParam);

                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);
                strReturn = xmlDoc.GetElementsByTagName("string")[0].InnerText;
                strReturn = ClsParamsEncDec.LgMgr_ParamDecrypt(strReturn);

                arrReturn = strReturn.Split('|');
                if (arrReturn.Length == 3)
                {
                    m_strHttpIPAddress = arrReturn[0];
                    m_intHttpPort = string.IsNullOrEmpty(arrReturn[1]) ? 0 : Convert.ToInt32(arrReturn[1]);
                    m_strHttpName = arrReturn[2];
                }
                else
                {
                    WriteErrorMessage("P_GetZYKinfo", "HTTP信息返回值数组长度不为3");
                }

                return true;
            }
            catch (Exception e)
            {
                WriteErrorMessage("P_GetZYKinfo", e.Message);
            }

            return false;
        }

        //知识点库信息（数据库）
        private bool P_GetZSDinfo(string strZSD_WSAddr)
        {
            try
            {
                string strWebServiceURL = "";

                XmlDocument xmlDoc;

                string strParam = "";
                string strReturn = "";
                string[] arrReturn;

                //数据库信息
                strWebServiceURL = strZSD_WSAddr + "LgMgrCenter/Service_SCORE.asmx/WS_G_GetKnowleageDBInfo_ByWebIP";
                strParam = "WebServerIP=";
                strReturn = mCommandWS.CallMethodPost(strWebServiceURL, strParam);

                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);
                strReturn = xmlDoc.GetElementsByTagName("string")[0].InnerText;
                strReturn = ClsParamsEncDec.LgMgr_ParamDecrypt(strReturn);

                arrReturn = strReturn.Split(':');
                if (arrReturn.Length == 4)
                {
                    m_strKnowledgeDBIP = arrReturn[0];                //知识点数据库IP
                    m_strKnowledgeDBName = arrReturn[1];          //知识点数据库名称
                    m_strKnowledgeDBUserName = arrReturn[2];   //数据库用户名
                    m_strKnowledgeDBUserPwd = arrReturn[3];     //知识点数据库密码
                }
                else
                {
                    WriteErrorMessage("P_GetZSDinfo", "数据库信息返回值数组长度不为4");
                }

                return true;
            }
            catch (Exception e)
            {
                WriteErrorMessage("P_GetZSDinfo", e.Message);
            }

            return false;
        }

        //strGradeID不需要时为string.Empty
        private GradeInfoM[] P_GetGradeInfo(string strGradeID)
        {
            try
            {
                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append("http://"+mCloudPlatformBFUrl + "/UserMgr/UserInfoMgr/API/Service_UserInfo.ashx");
                sbUrl.Append("?token=" + mToken);
                sbUrl.Append("&method=GetGrade");
                sbUrl.Append(string.Format("&params={0}|", strGradeID));
                string strReturn = mCommandApi.CallMethodGet(sbUrl.ToString());
                int iFlag = UtilityClass.AnalyseCloudJson(ref strReturn);
                if (iFlag == 3)
                {
                    if (EventInvalidToken != null)
                    {
                        EventInvalidToken();
                        return null;
                    }
                }
                GradeInfoM[] grade = JsonFormatter.JsonDeserialize<GradeInfoM[]>(strReturn);

                if (grade != null)
                {
                    return grade;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("P_GetGradeInfo", e.Message);
            }

            return null;
        }

        //strStudentID,strClassID,strCourseClassID三选一，确定一个之后，另外两个一定要置为string.Empty
        //这个是精简版，返回值需要确认
        private StudentInfoSimpleM[] GetStudentInfo_Simple(string strStudentID, string strClassID, string strCourseClassID)
        {
            try
            {
                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append("http://" + mCloudPlatformBFUrl + "/UserMgr/UserInfoMgr/API/Service_UserInfo.ashx");
                sbUrl.Append("?token=" + mToken);
                sbUrl.Append("&method=GetStudent");
                sbUrl.Append(string.Format("&params={0}||{1}||||||", strStudentID, strClassID));
                string strReturn = mCommandApi.CallMethodGet(sbUrl.ToString());
                int iFlag = UtilityClass.AnalyseCloudJson(ref strReturn);
                if (iFlag == 3)
                {
                    if (EventInvalidToken != null)
                    {
                        EventInvalidToken();
                        return null;
                    }
                }
                StudentInfoSimpleM[] stuSimple = JsonFormatter.JsonDeserialize<StudentInfoSimpleM[]>(strReturn);

                if (stuSimple != null)
                {
                    return stuSimple;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetStudentInfo", e.Message);
            }

            return null;
        }

        private CourseStudentInfoM[] GetStudentInfoByCourseClassID(string strCourseClassID)
        {
            try
            {
                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append("http://"+mCloudPlatformBFUrl + "/UserMgr/TeachInfoMgr/Api/Service_TeachInfo.ashx");
                sbUrl.Append("?token=" + mToken);
                sbUrl.Append("&method=GetCourseStudents");
                sbUrl.Append(string.Format("&params=|{0}|", strCourseClassID));
                string strReturn = mCommandApi.CallMethodGet(sbUrl.ToString());
                int iFlag = UtilityClass.AnalyseCloudJson(ref strReturn);
                if (iFlag == 3)
                {
                    if (EventInvalidToken != null)
                    {
                        EventInvalidToken();
                        return null;
                    }
                }
                CourseStudentInfoM[] courseStu = JsonFormatter.JsonDeserialize<CourseStudentInfoM[]>(strReturn);

                if (courseStu != null)
                {
                    return courseStu;
                }

            }
            catch (Exception e)
            {
                WriteErrorMessage("GetStudentInfoByCourseClassID", e.Message);
            }
            return null;
        }

        private string GetSubjectName(E_Subject eSubject)
        {
            try
            {
                string strSubjectName = "";
                switch ((int)eSubject)
                {
                    case 0:
                        strSubjectName = "";
                        break;
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
                WriteErrorMessage("GetSubjectName", e.Message);
            }
            return "";
        }

        private bool P_GetZYKGLInfo()
        {
            try
            {
                if (string.IsNullOrEmpty(mZYKGL_WS_IP) || string.IsNullOrEmpty(mZYKGL_WS_Port))
                {
                    return false;
                }

                string strWholeUrl = "";
                string strParam = "";
                string strReturn = "";
                XmlDocument xmlDoc = null;
                string sResLibVer = GetZYKServerVer();
                bool bDecrypt = false;
                if (string.Compare(sResLibVer, "\"v5.6\"",true)== 0) bDecrypt = true;                
                strWholeUrl = "http://" + mZYKGL_WS_IP + ":" + mZYKGL_WS_Port+"/"+ mZYKGL_WS_VirDir + "SearchStatisticalInfo.asmx/WS_Search_GetServerAddressConf";
                strParam = "serverID=";
                strReturn = mCommandWS.CallMethodPost(strWholeUrl, strParam);
                if (string.IsNullOrEmpty(strReturn))
                {
                    return false;
                }
                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);
                XmlNodeList list = xmlDoc.GetElementsByTagName("Server");
                if (list == null || list.Count == 0)
                {
                    return false;
                }
                string strModID = "";
                string strServerType = "";
                string strServerName = "";
                for (int i = 0; i < list.Count; i++)
                {
                    strModID = list[i].ChildNodes[0].InnerText;
                    strServerType = list[i].ChildNodes[1].InnerText;
                    strServerName = list[i].ChildNodes[2].InnerText;
                    if (strModID == "A10" && strServerType == "1")
                    {
                        if(bDecrypt)
                        {
                            mZSDK_WS_IP = AITeachCloud.Helper.EncryptHelper.DESDecrypt(list[i].ChildNodes[3].InnerText);
                            mZSDK_WS_Port = AITeachCloud.Helper.EncryptHelper.DESDecrypt(list[i].ChildNodes[4].InnerText);
                        }
                        else
                        {
                            mZSDK_WS_IP = list[i].ChildNodes[3].InnerText;
                            mZSDK_WS_Port = list[i].ChildNodes[4].InnerText;
                        }
                        if (list[i].ChildNodes.Count>7) mZSDK_WS_VirDir = list[i].ChildNodes[7].InnerText;
                    }
                    else if (strModID == "A20" && strServerType == "1")
                    {
                        if (bDecrypt)
                        {
                            mZSDSB_WS_IP = AITeachCloud.Helper.EncryptHelper.DESDecrypt(list[i].ChildNodes[3].InnerText);
                            mZSDSB_WS_Port = AITeachCloud.Helper.EncryptHelper.DESDecrypt(list[i].ChildNodes[4].InnerText);
                        }
                        else
                        {
                            mZSDSB_WS_IP = list[i].ChildNodes[3].InnerText;
                            mZSDSB_WS_Port = list[i].ChildNodes[4].InnerText;
                        }

                        if (list[i].ChildNodes.Count > 7) mZSDSB_WS_VirDir = list[i].ChildNodes[7].InnerText;
                    }
                    else if (strModID == "A00" && strServerType == "1")
                    {
                        if (bDecrypt)
                        {
                            mZSDKJ_WS_IP = AITeachCloud.Helper.EncryptHelper.DESDecrypt(list[i].ChildNodes[3].InnerText);
                            mZSDKJ_WS_Port = AITeachCloud.Helper.EncryptHelper.DESDecrypt(list[i].ChildNodes[4].InnerText);
                        }
                        else
                        {
                            mZSDKJ_WS_IP = list[i].ChildNodes[3].InnerText;
                            mZSDKJ_WS_Port = list[i].ChildNodes[4].InnerText;
                        }

                        if (list[i].ChildNodes.Count > 7) mZSDKJ_WS_VirDir = list[i].ChildNodes[7].InnerText;
                    }
                    else if (strModID == "A00" && strServerType == "2")
                    {
                        if (bDecrypt)
                        {
                            mZSDZYK_FTP_IP = AITeachCloud.Helper.EncryptHelper.DESDecrypt(list[i].ChildNodes[3].InnerText);
                            mZSDZYK_FTP_Port = AITeachCloud.Helper.EncryptHelper.DESDecrypt(list[i].ChildNodes[4].InnerText);
                            mZSDZYK_FTP_UserName = AITeachCloud.Helper.EncryptHelper.DESDecrypt(list[i].ChildNodes[5].InnerText);
                            mZSDZYK_FTP_Pwd = AITeachCloud.Helper.EncryptHelper.DESDecrypt(list[i].ChildNodes[6].InnerText);
                        }
                        else
                        {
                            mZSDZYK_FTP_IP = list[i].ChildNodes[3].InnerText;
                            mZSDZYK_FTP_Port = list[i].ChildNodes[4].InnerText;
                            mZSDZYK_FTP_UserName = list[i].ChildNodes[5].InnerText;
                            mZSDZYK_FTP_Pwd = list[i].ChildNodes[6].InnerText;
                        }

                        if (list[i].ChildNodes.Count > 7) mZSDZYK_FTP_VirDir = list[i].ChildNodes[7].InnerText;
                    }
                    else if (strModID == "A00" && strServerType == "3")
                    {
                        if (bDecrypt)
                        {
                            mZSDZYK_HTTP_IP = AITeachCloud.Helper.EncryptHelper.DESDecrypt(list[i].ChildNodes[3].InnerText);
                            mZSDZYK_HTTP_Port = AITeachCloud.Helper.EncryptHelper.DESDecrypt(list[i].ChildNodes[4].InnerText);
                        }
                        else
                        {
                            mZSDZYK_HTTP_IP = list[i].ChildNodes[3].InnerText;
                            mZSDZYK_HTTP_Port = list[i].ChildNodes[4].InnerText;
                        }

                        if (list[i].ChildNodes.Count > 7) mZSDZYK_HTTP_VirDir = list[i].ChildNodes[7].InnerText;
                    }
                    else if (strModID == "A00" && strServerType == "4" && strServerName== "资源管理平台Web站点")
                    {
                        if (bDecrypt)
                        {
                            mZYKXXSJ_WS_IP = AITeachCloud.Helper.EncryptHelper.DESDecrypt(list[i].ChildNodes[3].InnerText);
                            mZYKXXSJ_WS_Port = AITeachCloud.Helper.EncryptHelper.DESDecrypt(list[i].ChildNodes[4].InnerText);
                        }
                        else
                        {
                            mZYKXXSJ_WS_IP = list[i].ChildNodes[3].InnerText;
                            mZYKXXSJ_WS_Port = list[i].ChildNodes[4].InnerText;
                        }

                        if (list[i].ChildNodes.Count > 7) mZYKXXSJ_WS_VirDir = list[i].ChildNodes[7].InnerText;
                    }                    
                }

                return true;
            }
            catch (Exception e)
            {
                WriteErrorMessage("P_GetZYKGLInfo", e.Message);
                return false;
            }
        }

        // 获取老师某一课程下的所有课程班（课程目前为空）
        //20191224 ldy修改 将private改为public 满足黄兴全获取老师所有学科下的班级要求
        public CourseClassInfoExM[] P_GetCourseClassByUser()
        {
            try
            {
                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append("http://" + mCloudPlatformBFUrl + "/UserMgr/TeachInfoMgr/Api/Service_TeachInfo.ashx");
                sbUrl.Append("?token=" + mToken);
                sbUrl.Append("&method=GetCourseClassByUser");
                sbUrl.Append(string.Format("&params=|{0}", mTeacherID));
                WriteTrackLog("P_GetCourseClassByUser", "sbUrl = " + sbUrl);
                string strReturn = mCommandApi.CallMethodGet(sbUrl.ToString());
                WriteTrackLog("P_GetCourseClassByUser", "strReturn = " + strReturn);
                int iFlag = UtilityClass.AnalyseCloudJson(ref strReturn);
                if (iFlag == 3)
                {
                    if (EventInvalidToken != null)
                    {
                        EventInvalidToken();
                        return null;
                    }
                }
                if (string.IsNullOrEmpty(strReturn))
                {
                    return null;
                }
                strReturn = Uri.UnescapeDataString(strReturn);
                CourseClassInfoExM[] courseClassInfo = JsonFormatter.JsonDeserialize<CourseClassInfoExM[]>(strReturn);
                return courseClassInfo;
            }
            catch (Exception e)
            {
                //WriteErrorMessage("P_GetCourseClassByUser", e.Message);
                File.WriteAllText("log.txt", e.Message);
            }
            return null;
        }

        //获取学校下某一学科下所有课程班
        private CourseClassInfoM[] P_GetCourseClassBySubject()
        {
            try
            {
                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append("http://" + mCloudPlatformBFUrl + "/UserMgr/TeachInfoMgr/Api/Service_TeachInfo.ashx");
                sbUrl.Append("?token=" + mToken);
                sbUrl.Append("&method=GetCourseClassBySubject");
                sbUrl.Append(string.Format("&params={0}|{1}", mCloudPlatformSubjectID, mSchoolID));
                WriteTrackLog("P_GetCourseClassBySubject", "sbUrl = " + sbUrl);
                string strReturn = mCommandApi.CallMethodGet(sbUrl.ToString());
                WriteTrackLog("P_GetCourseClassBySubject", "strReturn = " + strReturn);
                int iFlag = UtilityClass.AnalyseCloudJson(ref strReturn);
                if (iFlag == 3)
                {
                    if (EventInvalidToken != null)
                    {
                        EventInvalidToken();
                        return null;
                    }
                }
                if (string.IsNullOrEmpty(strReturn))
                {
                    return null;
                }
                strReturn = Uri.UnescapeDataString(strReturn);
                CourseClassInfoM[] courseClassInfo = JsonFormatter.JsonDeserialize<CourseClassInfoM[]>(strReturn);
                return courseClassInfo;
            }
            catch (Exception e)
            {
                //WriteErrorMessage("P_GetCourseClassBySubject", e.Message);
                File.WriteAllText("log.txt", e.Message);
            }
            return null;
        }
        #endregion



        #region 继续上一堂课，待与大数据中心合并
        /// <summary>
        /// 课堂编号
        /// </summary>
        public int LoginClassID
        {
            get
            {
                return mLoginClassID;
            }
            set
            {
                mLoginClassID = value;
            }
        }
        /// <summary>
        /// 进入当前模块时产生的ID
        /// </summary>
        public int CurLoginModuleID
        {
            get
            {
                return mCurLoginModuleID;
            }
            set
            {
                mCurLoginModuleID = value;
            }
        }
        /// <summary>
        /// 进入当前的教学模式时产生的ID
        /// </summary>
        public long CurLoginTeachModeID
        {
            get
            {
                return mCurLoginTeachModeID;
            }
            set
            {
                mCurLoginTeachModeID = value;
            }
        }

        /// <summary>
        /// 课堂编号
        /// </summary>
        private int mLoginClassID;
        private string mCourseID;           //课程ID（可以不需要吧）
        private string mCourseClassID;   //课程班ID（可以不需要包）
        /// <summary>
        /// 插入当前模块时产生的ID
        /// </summary>
        private int mCurLoginModuleID;
        /// <summary>
        /// 当前的模块ID
        /// </summary>
        //private short mCurModuleID;
        /// <summary>
        /// 插入当前的教学模式时产生的ID
        /// </summary>
        private long mCurLoginTeachModeID;
        /// <summary>
        /// 当前的教学模式ID
        /// </summary>
        //private short mCurTeachModeID;

        private class L_ResourceInfoM
        {
            public int InsertResID { get; set; }
            public string ResourceID { get; set; }
            public string ParentResourceID { get; set; }
            public short TeachModeID { get; set; }
            public long LoginTeachModeID { get; set; }
        }



        /// <summary>
        /// 确定课程班时调用以下接口，记录一堂课的开始
        /// </summary>
        /// <param name="strProductCode">产品编号。网络化课堂教学系统=LBD；多媒体课堂教学系统=MMT</param>
        /// <param name="strCourseID"></param>
        /// <param name="strCourseClassID"></param>
        /// <param name="iClassRoomID"></param>
        /// <param name="iClassStuCount"></param>
        /// <param name="strLoginIP"></param>
        /// <returns>课堂编号</returns>
        public int L_StartClass(string strProductCode, string strCourseID, string strCourseClassID, int iClassRoomID, int iClassStuCount, string strLoginIP, string ScheduleCourseID)
        {
            try
            {
                BD_WriteDebugInfo("L_StartClass", "进入。strProductCode=" + strProductCode + ",strCourseClassID=" + strCourseClassID + ",iClassRoomID=" + iClassRoomID + ",iClassStuCount=" + iClassStuCount);
                if (mMySubjectID < 0 || mMySubjectID > 9)
                {
                    WriteErrorMessage("L_StartClass", "参数有误, mMySubjectID=" + mMySubjectID);
                    return 0;
                }
                curClassSubjectId = mCloudPlatformSubjectID;//记录当前上课使用的id
                //继续上一堂课和大数据中心都要求老师登录并确定课程班
                if (string.IsNullOrEmpty(mTeacherID) || string.IsNullOrEmpty(strCourseID) || string.IsNullOrEmpty(strCourseClassID))
                {
                    WriteErrorMessage("L_StartClass", "参数有误, mTeacherID=" + mTeacherID + ";strCourseID=" + strCourseID + ";strCourseClassID=" + strCourseClassID);
                    return 0;
                }

                mCourseID = strCourseID;
                mCourseClassID = strCourseClassID;

                LoginClassInfoM LoginInfo = new LoginClassInfoM();
                LoginInfo.TeacherID = mTeacherID;
                LoginInfo.CourseID = strCourseID;
                LoginInfo.SubjectID = mMySubjectID;
                LoginInfo.CoursePlanIDs = mCourseClassID;
                LoginInfo.ClassRoomID = iClassRoomID;
                LoginInfo.ClassStuCount = iClassStuCount;
                LoginInfo.ProductCode = strProductCode;
                LoginInfo.LoginIP = strLoginIP;
                LoginInfo.SchoolID = mSchoolID;
                LoginInfo.TermYear = mTermYear;
                LoginInfo.ScheduleCourseID = ScheduleCourseID;
                string[] arrParam = new string[1];
                arrParam[0] = JsonFormatter.JsonSerialize(LoginInfo);
                string strResult = mCommandApi.CallMethodPost("InsertLoginClassInfo", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return 0;
                }
                int iLoginID = Convert.ToInt32(strResult);
                mLoginClassID = iLoginID;
                return iLoginID;
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_StartClass", e.Message);
            }
            return 0;
        }

        /// <summary>
        /// 开始点名
        /// </summary>
        /// <returns>返回操作产生的ID，若操作不成功则返回0</returns>
        public int L_StartSignIn()
        {
            try
            {
                BD_WriteDebugInfo("L_StartSignIn", "进入");

                if (mLoginClassID <= 0)
                {
                    return 0;
                }

                int ModuleOperID = L_StartTeacherModuleOper("Sign001", null, null);
                return ModuleOperID;
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_StartSignIn", e.Message);
                return 0;
            }
        }

        //此接口需要具备插入和更新的功能
        /// <summary>
        /// 实时保存学生签到信息
        /// </summary>
        /// <param name="iType">为1表示学生签到，为2表示教师手动修改</param>
        /// <param name="strStudentID">学生ID</param>
        /// <param name="strSeatID">学生作为ID</param>
        /// <param name="fScore">得分</param>
        /// <param name="strDescription">说明信息</param>
        /// <param name="strRemark">备注</param>
        /// <returns>返回操作成功与否</returns>
        public bool L_StuSignIn(int iType, string strStudentID, string strSeatID, float fScore, string strDescription, string strRemark)
        {
            try
            {
                BD_WriteDebugInfo("L_StuSignIn", "进入。strStudentID=" + strStudentID + "，strSeatID=" + strSeatID);
                if (mLoginClassID <= 0)
                {
                    return false;
                }

                string[] arrParam = new string[1];
                AttendanceDetailM attendance = new AttendanceDetailM();
                attendance.StudentID = strStudentID;
                attendance.SeatID = strSeatID;
                attendance.LoginID = mLoginClassID;
                attendance.Score = fScore;
                attendance.Description = strDescription;
                if (iType == 1)
                {
                    attendance.Creator = strStudentID;//学生自主签到时为学生ID
                }
                else
                {
                    attendance.Creator = mTeacherID;//老师修改时为老师ID
                }
                attendance.LastEditor = attendance.Creator;
                attendance.Remark = strRemark;

                arrParam[0] = JsonFormatter.JsonSerialize(attendance);
                string strResult = mCommandApi.CallMethodPost("InsertAttendanceDetail", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return false;
                }
                int ID = Convert.ToInt32(strResult);
                if (ID > 0)
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
                WriteErrorMessage("L_StuSignIn", e.Message);
                return false;
            }
        }
        /// <summary>
        /// 删除学生的出勤记录（老师手动设置学生的出勤情况时调用）ldy20180403
        /// </summary>
        /// <param name="strStudentID">学生id</param>
        /// <returns></returns>
        public bool L_DelStuSignIn(string strStudentID)
        {
            try
            {
                BD_WriteDebugInfo("L_DelStuSignIn", "进入。strStudentID=" + strStudentID);
                if (string.IsNullOrEmpty(strStudentID))
                    return false;
                string[] arrParam = new string[1];
                arrParam[0] = JsonFormatter.JsonSerialize(new { StudentID = strStudentID, LoginID = mLoginClassID });
                string strResult = mCommandApi.CallMethodPost("DeleteAttendanceDetail", arrParam);
                if (string.IsNullOrEmpty(strResult))
                    return false;
                return true;
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_DelStuSignIn", e.Message);
                return false;
            }
        }
        /// <summary>
        /// 修改学生出勤记录
        /// </summary>
        /// <param name="strStudentID">学生id</param>
        /// <param name="iScore">分数</param>
        /// <returns></returns>
        public bool L_UpdateStuSignIn(string strStudentID,int iScore) {
            try
            {
                BD_WriteDebugInfo("L_UpdateStuSignIn", "进入。strStudentID=" + strStudentID);
                if (string.IsNullOrEmpty(strStudentID))
                    return false;
                string[] arrParam = new string[1];
                arrParam[0] = JsonFormatter.JsonSerialize(new { StudentID = strStudentID, Score=iScore,LoginID = mLoginClassID });
                string strResult = mCommandApi.CallMethodPost("UpdateAttendanceDetail", arrParam);
                if (string.IsNullOrEmpty(strResult))
                    return false;
                return true;
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_UpdateStuSignIn", e.Message);
                return false;
            }
        }
        //（弃用）目前采用实时保存的方式，此接口不用
        /// <summary>
        /// 批量保存学生签到信息（适合先缓存学生签到信息，最终一次性保存到数据库）
        /// </summary>
        /// <param name="arrStuAttendance">每个学生的签到信息</param>
        /// <returns>每条签到记录产生的ID，操作失败时返回null</returns>
        private bool L_StuSignIn_Mult(AttendanceDetailM[] arrStuAttendance)
        {
            try
            {
                if (mLoginClassID <= 0)
                {
                    return false;
                }
                if (arrStuAttendance == null || arrStuAttendance.Length == 0)
                {
                    return false;
                }

                for (int i = 0; i < arrStuAttendance.Length; i++)
                {
                    arrStuAttendance[i].LoginID = mLoginClassID;
                    arrStuAttendance[i].Creator = mTeacherID;//批量保存时为教师ID
                    arrStuAttendance[i].LastEditor = arrStuAttendance[i].Creator;
                    arrStuAttendance[i].LastEditTime = arrStuAttendance[i].CreateTime;//批量保存需要提供签到时间
                }
                string[] arrParam = new string[1];
                arrParam[0] = JsonFormatter.JsonSerialize(arrStuAttendance);
                string strResult = mCommandApi.CallMethodPost("InsertAttendanceDetail_Mult", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return false;
                }
                return Convert.ToBoolean(strResult);
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_StuSignIn_Mult", e.Message);
            }
            return false;
        }

        /// <summary>
        /// 结束点名
        /// </summary>
        /// <returns>返回操作产生的ID，若操作不成功则返回0</returns>
        public int L_EndSignIn()
        {
            try
            {
                BD_WriteDebugInfo("L_EndSignIn", "进入");
                if (mLoginClassID <= 0)
                {
                    return 0;
                }
                int ModuleOperID = L_StartTeacherModuleOper("Sign002", null, null);
                return ModuleOperID;
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_StartSignIn", e.Message);
                return 0;
            }
        }

        /// <summary>
        /// 取消点名
        /// </summary>
        /// <returns>返回操作产生的ID，若操作不成功则返回0</returns>
        public int L_CancelSignIn()
        {
            try
            {
                BD_WriteDebugInfo("L_CancelSignIn", "进入");

                if (mLoginClassID <= 0)
                {
                    return 0;
                }
                int ModuleOperID = L_StartTeacherModuleOper("Sign003", null, null);
                return ModuleOperID;
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_CancelSignIn", e.Message);
                return 0;
            }
        }

        /// <summary>
        /// 进入模块
        /// </summary>
        /// <param name="sModuleID">模块的ID</param>
        /// <returns>返回进入模块产生的ID，若操作失败则返回0</returns>
        public int L_EnterModule(short sModuleID)
        {
            try
            {
                BD_WriteDebugInfo("L_EnterModule", "进入");

                if (mLoginClassID <= 0)
                {
                    return 0;
                }

                string[] arrParam = new string[1];
                LoginClassModuleM LoginModule = new LoginClassModuleM();
                LoginModule.ModelID = sModuleID;
                LoginModule.LoginID = mLoginClassID;
                LoginModule.UsingBookName = "";//此参数已无用
                LoginModule.Creator = mTeacherID;
                arrParam[0] = JsonFormatter.JsonSerialize(LoginModule);
                string strResult = mCommandApi.CallMethodPost("InsertLoginClassModule", arrParam);
                if (string.IsNullOrEmpty(strResult) == false)
                {
                    mCurLoginModuleID = Convert.ToInt32(strResult);
                    return mCurLoginModuleID;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_EnterModule", e.Message);
            }
            mCurLoginModuleID = 0;
            return 0;
        }

        /// <summary>
        /// 退出模块
        /// </summary>
        /// <returns>返回操作成功与否</returns>
        public bool L_ExitModule()
        {
            try
            {
                BD_WriteDebugInfo("L_ExitModule", "进入");

                if (mCurLoginModuleID <= 0 || mCurLoginModuleID <= 0)
                {
                    return false;
                }

                string[] arrParam = new string[2];
                arrParam[0] = mCurLoginModuleID.ToString();
                arrParam[1] = mTeacherID;

                string strResult = mCommandApi.CallMethodPost("UpdateLoginModuleEndTime", arrParam);
                if (string.IsNullOrEmpty(strResult) == false)
                {
                    if (Convert.ToInt32(strResult) == 1)
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_ExitModule", e.Message);
            }
            return false;
        }

        /// <summary>
        /// 教师开始某项操作
        /// </summary>
        /// <param name="strOperCode">操作的代码</param>
        /// <param name="strOperDetail">操作的详细信息</param>
        /// <param name="strTargetStudentID">操作对象的学生ID（某些操作针对若干个学生）</param>
        /// <returns>操作产生的唯一ID</returns>
        public int L_StartTeacherModuleOper(string strOperCode, string strOperDetail, string strTargetStudentID)
        {
            try
            {
                BD_WriteDebugInfo("L_StartTeacherModuleOper", "进入。strOperCode=" + strOperCode + "，strOperDetail=" + strOperDetail + "，strTargetStudentID=" + strTargetStudentID);
                //BD_WriteDebugInfo("L_StartTeacherModuleOper", "当前使用模块id=" + mCurLoginModuleID.ToString());
                if (mCurLoginModuleID <= 0)
                {
                    return 0;
                }

                LCModuleOperM ModuleOper = new LCModuleOperM();
                ModuleOper.OperationCode = strOperCode;
                ModuleOper.LoginModelID = mCurLoginModuleID;
                ModuleOper.Creator = mTeacherID;
                ModuleOper.LoginClassModeID = mCurLoginTeachModeID;
                ModuleOper.OperContent = strOperDetail;
                ModuleOper.OperTarget = strTargetStudentID;

                string[] arrParam = new string[1];
                arrParam[0] = JsonFormatter.JsonSerialize(ModuleOper);
                string strResult = mCommandApi.CallMethodPost("StartTeacherModuleOper", arrParam);
                //BD_WriteDebugInfo("L_StartTeacherModuleOper", "strResult=" + strResult);
                if (string.IsNullOrEmpty(strResult))
                {
                    return 0;
                }

                int iModuleOperID = Convert.ToInt32(strResult);
                return iModuleOperID;
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_StartTeacherModuleOper", e.Message);
            }
            return 0;
        }

        //此接口根据OperationCode进行，则要求每个操作不能同时开启两个实例，否则无法区分彼此
        /// <summary>
        /// 教师结束某项操作
        /// </summary>
        /// <param name="strOperCode">操作的代码</param>
        /// <param name="arrOperResult">操作结果</param>
        /// <returns>操作成功与否</returns>
        public bool L_EndTeacherModuleOper(string strOperCode, LCOperResultM[] arrOperResult)
        {
            try
            {
                BD_WriteDebugInfo("L_EndTeacherModuleOper", "进入。strOperCode=" + strOperCode);

                if (mCurLoginModuleID <= 0)
                {
                    return false;
                }

                string[] arrParam = new string[5];
                arrParam[0] = mCurLoginModuleID.ToString();
                arrParam[1] = mCurLoginTeachModeID.ToString();
                arrParam[2] = strOperCode;
                arrParam[3] = (mTeacherID != null ? mTeacherID : "");
                arrParam[4] = JsonFormatter.JsonSerialize(arrOperResult);

                string strResult = mCommandApi.CallMethodPost("EndTeacherModuleOper", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return false;
                }

                if (Convert.ToInt32(strResult) == 1)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_EndTeacherModuleOper", e.Message);
            }
            return false;
        }

        //记录教师行为的结束时间（根据ModuleOperID进行）（返回受影响的行数）
        private int L_EndTeacherModuleOper(long lLoginModuleOperID, LCOperResultM[] arrOperResult)
        {
            try
            {
                string[] arrParam = new string[3];
                arrParam[0] = lLoginModuleOperID.ToString();
                arrParam[1] = (mTeacherID != null ? mTeacherID : "");
                arrParam[2] = JsonFormatter.JsonSerialize(arrOperResult);

                string strResult = mCommandApi.CallMethodPost("EndTeacherModuleOperByID", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_EndTeacherModuleOper", e.Message);
            }
            return 0;
        }

        //已选择的资料中的所有叶子节点
        private List<L_ResourceInfoM> mLeafResources;
        //当前正在使用的一批资料
        private L_ResourceInfoM mUsingResources;
        /// <summary>
        /// 开始上课前选择资料
        /// </summary>
        public bool L_ChooseResource(List<LCResourceTreeNodeM> Resource)
        {
            try
            {
                if (Resource == null || Resource.Count == 0)
                {
                    WriteErrorMessage("L_ChooseResource", "Resource为空");
                    return false;
                }

                string strParam = JsonFormatter.JsonSerialize(Resource);
                BD_WriteDebugInfo("L_ChooseResource", "进入。Resource=" + strParam);

                //插入选资料的操作
                int iOperID = L_StartTeacherModuleOper("Data001", null, null);

                //保存所选择的资料
                LCResourceElementM[] arrRes = L_GetResourceArray(Resource);
                string strFileName = null;
                int iExtensionIndex = -1;
                for (int i = 0; i < arrRes.Length; i++)
                {
                    arrRes[i].Data.LCModuleOperID = iOperID;
                    strFileName = arrRes[i].Data.ResourceName;
                    arrRes[i].Data.FileExtension = "";
                    if (string.IsNullOrEmpty(strFileName) == false)
                    {
                        iExtensionIndex = strFileName.LastIndexOf(".");
                        if (iExtensionIndex > -1)
                        {
                            arrRes[i].Data.FileExtension = strFileName.Substring(iExtensionIndex + 1, strFileName.Length - iExtensionIndex - 1);
                        }
                    }
                    arrRes[i].Data.LastEditor = mTeacherID;
                }
                string[] arrParam = new string[1];
                arrParam[0] = JsonFormatter.JsonSerialize(arrRes);
                BD_WriteDebugInfo("L_ChooseResource", "资料数组：" + arrParam[0]);

                string strResult = mCommandApi.CallMethodPost("InsertLCResource", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    WriteErrorMessage("L_ChooseResource", "保存资料时返回值有误，strResult为空");
                    return false;
                }
                int[] arrID = JsonFormatter.JsonDeserialize<int[]>(strResult);

                //结束选资料的操作
                L_EndTeacherModuleOper("Data001", null);

                if (arrID == null || arrID.Length != arrRes.Length)
                {
                    WriteErrorMessage("L_ChooseResource", "保存资料时返回值有误，strResult=" + strResult);
                    return false;
                }
                else
                {
                    //（这里应该剔除课前预习和课后练习）
                    mLeafResources = new List<L_ResourceInfoM>(arrRes.Length);
                    for (int i = 0; i < arrRes.Length; i++)
                    {
                        //说明是叶节点
                        if (arrRes[i].ChildElementIndex.Count == 0)
                        {
                            L_ResourceInfoM res = new L_ResourceInfoM()
                            {
                                InsertResID = arrID[i],
                                ResourceID = arrRes[i].Data.ResourceID,
                                LoginTeachModeID = 0,
                                ParentResourceID = arrRes[i].Data.ParentResourceID,
                                TeachModeID = 0
                            };
                            mLeafResources.Add(res);
                        }
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_ChooseResource", e.Message);
            }
            return false;
        }

        //将资料树转换成数组xiezongwu20171013修改
        private LCResourceElementM[] L_GetResourceArray(List<LCResourceTreeNodeM> tree)
        {
            List<LCResourceElementM> list = null;
            try
            {
                if (tree == null)
                {
                    return null;
                }

                list = new List<LCResourceElementM>();
                for (int i = 0; i < tree.Count; i++)
                {
                    L_TreeToList(tree[i], 1, -1, list);
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_GetResourceArray", e.Message);
            }
            if (list == null)
            {
                return null;
            }
            else
            {
                return list.ToArray();
            }
        }

        //树转换成数组时的迭代函数//将资料树转换成数组xiezongwu20171013修改
        private void L_TreeToList(LCResourceTreeNodeM treeNode, int depth, int parentIndex, List<LCResourceElementM> list)
        {
            try
            {
                int ParentNodeIndex = 0;
                //                 //默认为1
                //                 treeNode.NodeType = 1;
                //                 //没有子节点则认为是叶节点
                //                 if (treeNode.ChildNodes == null || treeNode.ChildNodes.Count == 0)
                //                 {
                //                     treeNode.NodeType = 3;
                //                 }
                //                 else
                //                 {
                //                     //若当前树的层次大于1，则修改为中间节点
                //                     if (depth > 1)
                //                     {
                //                         treeNode.NodeType = 2;
                //                     }
                //                 }

                LCResourceElementM element = new LCResourceElementM();
                element.Data = treeNode.Data;
                element.ParentIndex = parentIndex;
                list.Add(element);

                //if (parentIndex > -1)
                //{
                //    list[parentIndex].ChildElementIndex.Add(list.Count - 1);
                //}

                if (treeNode.ChildNodes != null)
                {
                    if (treeNode.ChildNodes.Count > 0)
                    {
                        ParentNodeIndex = list.Count - 1;
                        list[ParentNodeIndex].ChildElementIndex.Add(list.Count - 1);
                    }
                    for (int i = 0; i < treeNode.ChildNodes.Count; i++)
                    {
                        L_TreeToList(treeNode.ChildNodes[i], depth + 1, list.Count - 1, list);
                    }

                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_TreeToList", e.Message);
            }
        }      

        /// <summary>
        /// 初次进入教学模式时调用
        /// </summary>
        /// <param name="sTeachModeID"></param>
        /// <param name="strResourceID"></param>
        /// <param name="strParentResourceID">当前使用的资料的父级资料ID，没有可为空</param>
        /// <returns>进入教学模式产生的ID</returns>
        public long L_EnterTeachMode(short sTeachModeID, string strResourceID, string strParentResourceID)
        {
            try
            {
                BD_WriteDebugInfo("L_EnterTeachMode", "进入。mCurLoginModuleID=" + mCurLoginModuleID);
                if (mCurLoginModuleID <= 0)
                {
                    return 0;
                }
                if (mLeafResources == null || mLeafResources.Count == 0)
                {
                    WriteErrorMessage("L_EnterTeachMode", "mToBeUsedResources为空");
                    return 0;
                }
                BD_WriteDebugInfo("L_EnterTeachMode", "sTeachModeID=" + sTeachModeID + ",strResourceID=" + strResourceID + ",strParentResourceID=" + strParentResourceID);
                BD_WriteDebugInfo("L_EnterTeachMode", "mLeafResources=" + JsonFormatter.JsonSerialize(mLeafResources));

                for (int i = 0; i < mLeafResources.Count; i++)
                {
                    if (mLeafResources[i].ResourceID == strResourceID && mLeafResources[i].ParentResourceID == strParentResourceID)
                    {
                        mUsingResources = mLeafResources[i];
                    }
                }
                if (mUsingResources == null)
                {
                    WriteErrorMessage("L_EnterTeachMode", "无法确定当前正在使用的资料");
                    return 0;
                }

                LoginTeachModeM LoginMode = new LoginTeachModeM();
                LoginMode.TeachModeID = sTeachModeID;
                LoginMode.LoginModelID = mCurLoginModuleID;
                LoginMode.ResourceID = strResourceID;//可以不赋值
                LoginMode.LastEditor = mTeacherID;
                LoginMode.InsertResourceID = mUsingResources.InsertResID;

                string[] arrParam = new string[1];
                arrParam[0] = JsonFormatter.JsonSerialize(LoginMode);
                BD_WriteDebugInfo("L_EnterTeachMode", "参数为=" + arrParam[0]);
                string strResult = mCommandApi.CallMethodPost("InsertLoginTeachMode", arrParam);
                BD_WriteDebugInfo("L_EnterTeachMode", "返回值=" + strResult);
                if (string.IsNullOrEmpty(strResult))
                {
                    return 0;
                }
                mCurLoginTeachModeID = Convert.ToInt64(strResult);
                if (mCurLoginTeachModeID > 0)
                {
                    mUsingResources.LoginTeachModeID = mCurLoginTeachModeID;
                    mUsingResources.TeachModeID = LoginMode.TeachModeID;
                }
                return mCurLoginTeachModeID;
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_EnterTeachMode", e.Message);
                return 0;
            }
        }

        //切换资料时更新当前正在使用的资料
        /// <summary>
        /// 切换资料时调用
        /// </summary>
        /// <param name="sTeachModeID">切换后的教学模式ID</param>
        /// <param name="strResourceID">切换后的资料ID</param>
        /// <param name="strParentResourceID">切换后的资料的父级资料ID，若没有则为空</param>
        public bool L_DataChange(short sTeachModeID, string strResourceID, string strParentResourceID)
        {
            try
            {
                if (mCurLoginModuleID <= 0)
                {
                    return false;
                }
                if (mLeafResources == null || mLeafResources.Count == 0)
                {
                    WriteErrorMessage("L_EnterTeachMode", "mToBeUsedResources为空");
                    return false;
                }
                for (int i = 0; i < mLeafResources.Count; i++)
                {
                    if (mLeafResources[i].ResourceID == strResourceID && mLeafResources[i].ParentResourceID == strParentResourceID)
                    {
                        mUsingResources = mLeafResources[i];
                    }
                }
                //若该资料已打开过教学模式，则更新该教学模式的使用时间
                if (mUsingResources.LoginTeachModeID > 0)
                {
                    string[] arrParam = new string[2];
                    arrParam[0] = mUsingResources.LoginTeachModeID.ToString();
                    arrParam[1] = mTeacherID;
                    string strResult = mCommandApi.CallMethodPost("UpdateLoginModeUsingTime", arrParam);
                    if (string.IsNullOrEmpty(strResult))
                    {
                        return false;
                    }
                    if (Convert.ToInt32(strResult) == 1)
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_DataChange", e.Message);
            }
            return false;
        }

        //这个LogingModeID必须由外部指定，因为在退出模块时可能所有教学模式一起调用此接口
        /// <summary>
        /// 退出教学模式
        /// </summary>
        /// <param name="LoginModeID">进入教学模式产生的ID</param>
        /// <returns></returns>
        public bool L_ExitTeachMode(long LoginModeID)
        {
            try
            {
                BD_WriteDebugInfo("L_ExitTeachMode", "进入。LoginModeID=" + LoginModeID);

                if (LoginModeID <= 0)
                {
                    return false;
                }

                string[] arrParam = new string[2];
                arrParam[0] = LoginModeID.ToString();
                arrParam[1] = mTeacherID;
                string strResult = mCommandApi.CallMethodPost("UpdateLoginModeEndTime", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return false;
                }
                if(Convert.ToInt32(strResult)==1)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_ExitTeachMode", e.Message);
            }
            return false;
        }

        /// <summary>
        /// 进入子模式
        /// </summary>
        /// <param name="strSubModeCode">子模式代码</param>
        /// <returns>进入子模式产生的ID</returns>
        public long L_EnterSubMode(string strSubModeCode)
        {
            try
            {
                BD_WriteDebugInfo("L_EnterSubMode", "进入。strSubModeCode=" + strSubModeCode);

                if (mCurLoginTeachModeID <= 0)
                {
                    return 0;
                }

                string[] arrParam = new string[1];
                LCSubModeM LoginSubMode = new LCSubModeM();
                LoginSubMode.SubModeCode = strSubModeCode;
                LoginSubMode.LCModeID = mCurLoginTeachModeID;
                LoginSubMode.Creator = mTeacherID;
                arrParam[0] = JsonFormatter.JsonSerialize(LoginSubMode);
                string strResult = mCommandApi.CallMethodPost("InsertLoginSubMode", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return 0;
                }
                long lLoginSubModeID = Convert.ToInt64(strResult);
                return lLoginSubModeID;
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_EnterSubMode", e.Message);
            }
            return 0;
        }

        /// <summary>
        /// 退出子模式
        /// </summary>
        /// <param name="strSubModeCode"></param>
        /// <returns></returns>
        public bool L_ExitSubMode(string strSubModeCode)
        {
            try
            {
                BD_WriteDebugInfo("L_ExitSubMode", "进入。strSubModeCode=" + strSubModeCode);

                if (mCurLoginTeachModeID <= 0)
                {
                    return false;
                }

                string[] arrParam = new string[3];
                arrParam[0] = strSubModeCode;
                arrParam[1] = mCurLoginTeachModeID.ToString();
                arrParam[2] = mTeacherID;
                string strResult = mCommandApi.CallMethodPost("UpdateLoginSubModeEndTime", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return false;
                }
                if (Convert.ToInt32(strResult) == 1)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_ExitSubMode", e.Message);
            }
            return false;
        }

        /// <summary>
        /// 开始子模式内的操作
        /// </summary>
        /// <param name="strSubModeCode">子模式代码</param>
        /// <param name="strOperCode">操作代码</param>
        /// <param name="strOperDetail">操作细节</param>
        /// <param name="strTargetStudentID">目标学生ID，多个值以 | 分隔</param>
        /// <returns>操作产生的唯一ID</returns>
        public int L_StartTeacherSubModeOper(string strSubModeCode, string strOperCode, string strOperDetail, string strTargetStudentID)
        {
            try
            {
                BD_WriteDebugInfo("L_StartTeacherSubModeOper", "进入。strSubModeCode=" + strSubModeCode + "，strOperCode=" + strOperCode);

                LCSubModeOperM SubModeOper = new LCSubModeOperM();
                SubModeOper.OperationCode = strOperCode;
                SubModeOper.Creator = mTeacherID;
                SubModeOper.OperContent = strOperDetail;
                SubModeOper.OperTarget = strTargetStudentID;

                string[] arrParam = new string[6];
                arrParam[0] = mCurLoginTeachModeID.ToString();
                arrParam[1] = strSubModeCode;
                arrParam[2] = strOperCode;
                arrParam[3] = mTeacherID;
                arrParam[4] = strOperDetail;
                arrParam[5] = strTargetStudentID;

                string strResult = mCommandApi.CallMethodPost("InsertTeacherSubModeOper", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return 0;
                }
                int iSubModeOperID = Convert.ToInt32(strResult);

                BD_WriteDebugInfo("L_StartTeacherSubModeOper", "返回值=" + iSubModeOperID);

                return iSubModeOperID;
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_StartTeacherSubModeOper", e.Message);
            }
            return 0;
        }

        //测试通过
        //结束子模式内的操作
        public bool L_EndTeacherSubModeOper(string strSubModeCode, string strOperCode)
        {
            try
            {
                BD_WriteDebugInfo("L_EndTeacherSubModeOper", "进入。strSubModeCode=" + strSubModeCode + "，strOperCode=" + strOperCode);

                string[] arrParam = new string[4];
                arrParam[0] = mCurLoginTeachModeID.ToString();
                arrParam[1] = strSubModeCode;
                arrParam[2] = strOperCode.ToString();
                arrParam[3] = mTeacherID;
                string strResult = mCommandApi.CallMethodPost("UpdateTeacherSubModeOperEndTime", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return false;
                }

                if (Convert.ToInt32(strResult) == 1)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_EndTeacherSubModeOper", e.Message);
            }
            return false;
        }

        //（可以删除）瞬时操作（效果同连续调用L_StartTeacherSubModeOper和L_EndTeacherSubModeOper）
        private int L_TeacherSubModeOper(long lLoginModeID, string strSubModeCode, string strOperCode, string strOperDetail, string strTargetStudentID)
        {
            try
            {
                string[] arrParam = new string[4];
                LCSubModeOperM SubModeOper = new LCSubModeOperM();
                SubModeOper.OperationCode = strOperCode;
                SubModeOper.Creator = mTeacherID;
                SubModeOper.OperContent = strOperDetail;
                SubModeOper.OperTarget = strTargetStudentID;
                arrParam[0] = "1";//1 表示瞬时操作
                arrParam[1] = lLoginModeID.ToString();
                arrParam[2] = strSubModeCode;
                arrParam[3] = JsonFormatter.JsonSerialize(SubModeOper);
                string strResult = mCommandApi.CallMethodPost("InsertTeacherSubModeOper", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return 0;
                }

                int iSubModeOperID = Convert.ToInt32(strResult);
                return iSubModeOperID;
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_TeacherSubModeOper", e.Message);
            }
            return 0;
        }

        //保存训练的题目信息
        public int L_SaveTrainQue(LCTrainQueInfoM[] TrainQue)
        {
            try
            {
                BD_WriteDebugInfo("L_SaveTrainQue", "进入。");
                BD_WriteDebugInfo("L_SaveTrainQue", "TrainQue=" + JsonFormatter.JsonSerialize(TrainQue));

                if (TrainQue == null || TrainQue.Length == 0)
                {
                    return 0;
                }

                string[] arrParam = new string[2];
                arrParam[0] = mTeacherID;
                arrParam[1] = JsonFormatter.JsonSerialize(TrainQue);
                string strResult = mCommandApi.CallMethodPost("InsertLCTrainQueInfo", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return 0;
                }

                int iRowCount = Convert.ToInt32(strResult);
                return iRowCount;
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_StartTrain", e.Message);
            }
            return 0;
        }

        //保存学生答案
        public long L_SaveStuAnswer(int iLCOperID, string strStudentID, string strAnswer)
        {
            try
            {
                BD_WriteDebugInfo("L_SaveStuAnswer", "进入。iLCOperID=" + iLCOperID);

                if (string.IsNullOrEmpty(strStudentID))
                {
                    return 0;
                }
                if (strAnswer == null)
                {
                    return 0;
                }

                LCTrainStuAnswerM stuAnswer = new LCTrainStuAnswerM();
                stuAnswer.LCOperationID = iLCOperID;
                stuAnswer.StudentID = strStudentID;
                stuAnswer.StuAnswer = strAnswer;
                stuAnswer.Creator = (mTeacherID != null ? mTeacherID : "");

                string[] arrParam = new string[1];
                arrParam[0] = JsonFormatter.JsonSerialize(stuAnswer);
                string strResult = mCommandApi.CallMethodPost("InsertLCTrainStuAnswer", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return 0;
                }
                long lInsertID = Convert.ToInt64(strResult);
                return lInsertID;
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_TrainSaveStuAnswer", e.Message);
            }
            return 0;
        }

        //保存学生结果（需要一个重载，根据OperCode来保存）
        public long[] L_SaveStuResult(LCTrainStuResultM[] arrStuResult)
        {
            try
            {
                BD_WriteDebugInfo("L_SaveStuResult", "进入。arrStuResult=" + JsonFormatter.JsonSerialize(arrStuResult));

                if (arrStuResult == null)
                {
                    return null;
                }
                if (arrStuResult.Length == 0)
                {
                    return new long[0];
                }

                string[] arrParam = new string[1];
                arrParam[0] = JsonFormatter.JsonSerialize(arrStuResult);
                string strResult = mCommandApi.CallMethodPost("InsertLCTrainStuResult", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return null;
                }
                long[] arrInsertID = JsonFormatter.JsonDeserialize<long[]>(strResult);
                return arrInsertID;
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_SaveStuResult", e.Message);
            }
            return null;
        }

        //保存资料（试卷）结果（需要一个重载，根据OperCode来保存）
        public long[] L_SaveQueResult(LCTrainQueResultM[] arrQueResult)
        {
            try
            {
                BD_WriteDebugInfo("L_SaveQueResult", "进入。arrQueResult=" + JsonFormatter.JsonSerialize(arrQueResult));

                if (arrQueResult == null)
                {
                    return null;
                }
                if (arrQueResult.Length == 0)
                {
                    return new long[0];
                }

                string[] arrParam = new string[1];
                arrParam[0] = JsonFormatter.JsonSerialize(arrQueResult);
                string strResult = mCommandApi.CallMethodPost("InsertLCTrainQueResult", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return null;
                }
                long[] arrInsertID = JsonFormatter.JsonDeserialize<long[]>(strResult);
                return arrInsertID;
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_TrainSaveStuResult", e.Message);
            }
            return null;
        }

        //保存训练结果（需要一个重载，根据OperCode来保存）
        public long L_SaveTrainResult(LCTrainResultM trainResult)
        {
            try
            {
                BD_WriteDebugInfo("L_SaveTrainResult", "进入。trainResult=" + JsonFormatter.JsonSerialize(trainResult));

                if (trainResult == null)
                {
                    return 0;
                }

                if (trainResult.LastEditor == null)
                {
                    trainResult.LastEditor = (mTeacherID != null ? mTeacherID : "");
                }

                string[] arrParam = new string[1];
                arrParam[0] = JsonFormatter.JsonSerialize(trainResult);
                string strResult = mCommandApi.CallMethodPost("InsertLCTrainResult", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return 0;
                }
                long lInsertID = Convert.ToInt64(strResult);
                return lInsertID;
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_TrainSaveResult", e.Message);
            }
            return 0;
        }

        //课堂加分和测试加分
        public bool L_ClassBonus(int iClassBonusType, string[] arrStudentID, float[] arrScore, string[] arrReason, string[] arrRemark)
        {
            try
            {
                BD_WriteDebugInfo("L_ClassBonus", "进入。iClassBonusType=" + iClassBonusType);
                //BD_WriteDebugInfo("L_ClassBonus", "进入。学生id=" + arrStudentID[0]);
                //BD_WriteDebugInfo("L_ClassBonus", "进入。分数=" + arrScore[0].ToString());
                if (arrStudentID == null || arrStudentID.Length == 0)
                {
                    return false;
                }

                string strOperationCode = "";
                switch (iClassBonusType)
                {
                    case 1://课堂加分
                        strOperationCode = "ClassBonus001";
                        break;
                    case 2://测试加分
                        strOperationCode = "ClassBonus002";
                        break;
                }

                int iLCOperID = L_StartTeacherModuleOper(strOperationCode, null, null);
                BD_WriteDebugInfo("L_ClassBonus", "模块操作id=" + iLCOperID.ToString());
                if (iLCOperID == 0)
                {
                    return false;
                }

                LCStudentScoreM[] score = new LCStudentScoreM[arrStudentID.Length];
                for (int i = 0; i < arrStudentID.Length; i++)
                {
                    score[i] = new LCStudentScoreM();
                    score[i].StudentID = arrStudentID[i];
                    score[i].LoginClassModelOperationID = iLCOperID;
                    score[i].LoginID = mLoginClassID;
                    score[i].OperationCode = strOperationCode;
                    score[i].Score = arrScore[i];
                    score[i].ScoreReason = arrReason[i];
                    score[i].Creator = mTeacherID;
                    score[i].LastEditor = mTeacherID;
                    score[i].Remark = arrRemark[i];
                    score[i].SubjectID = mMySubjectID;
                }

                string[] arrParam = new string[1];
                arrParam[0] = JsonFormatter.JsonSerialize(score);
                mCommandApi.CallMethodPost("InsertStudentClassBonus", arrParam);

                //结束操作
                L_EndTeacherModuleOper(iLCOperID, null);
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_ClassBonus", e.Message);
            }
            return true;
        }


        //课堂加分和测试加分(多媒体使用)
        public bool L_ClassBonus_MT(int iClassBonusType, string[] arrStudentID, float[] arrScore, string[] arrReason, string[] arrRemark)
        {
            try
            {
                BD_WriteDebugInfo("L_ClassBonus_MT", "进入。iClassBonusType=" + iClassBonusType);
                if (arrStudentID == null || arrStudentID.Length == 0)
                {
                    return false;
                }

                string strOperationCode = "";
                switch (iClassBonusType)
                {
                    case 1://课堂加分
                        strOperationCode = "ClassBonus001";
                        break;
                    case 2://测试加分
                        strOperationCode = "ClassBonus002";
                        break;
                }

                //int iLCOperID = L_StartTeacherModuleOper(strOperationCode, null, null);
                //BD_WriteDebugInfo("L_ClassBonus", "模块操作id=" + iLCOperID.ToString());
                //if (iLCOperID == 0)
                //{
                //    return false;
                //}
                //BD_WriteDebugInfo("L_ClassBonus", "mTeacherID=" + mTeacherID);
                //BD_WriteDebugInfo("L_ClassBonus", "mMySubjectID=" + mMySubjectID);
                string LoginClassID = GetLastLoginIdByTeacher(mTeacherID);
                if (string.IsNullOrEmpty(LoginClassID))
                    return false;
                //BD_WriteDebugInfo("L_ClassBonus", "LoginClassID=" + LoginClassID);
                LCStudentScoreM[] score = new LCStudentScoreM[arrStudentID.Length];
                for (int i = 0; i < arrStudentID.Length; i++)
                {
                    score[i] = new LCStudentScoreM();
                    score[i].StudentID = arrStudentID[i];
                    score[i].LoginClassModelOperationID = 0;
                    score[i].LoginID = int.Parse(LoginClassID);
                    score[i].OperationCode = strOperationCode;
                    score[i].Score = arrScore[i];
                    score[i].ScoreReason = arrReason[i];
                    score[i].Creator = mTeacherID;
                    score[i].LastEditor = mTeacherID;
                    score[i].Remark = arrRemark[i];
                    score[i].SubjectID = mMySubjectID;
                }

                string[] arrParam = new string[1];
                arrParam[0] = JsonFormatter.JsonSerialize(score);
                //BD_WriteDebugInfo("L_ClassBonus_MT", "参数=" + arrParam[0]);
                mCommandApi.CallMethodPost("InsertStudentClassBonus", arrParam);

                //结束操作
                //L_EndTeacherModuleOper(iLCOperID, null);
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_ClassBonus_MT", e.Message);
            }
            return true;
        }
        //测试通过
        //保存学生操作及详情（含分值）
        public int L_StartStudentOperAndDetail(string strStudentID, string strOperCode, string strOperDetail,DateTime OperStartTime,DateTime OperEndTime,float OperScore)
        {
            try
            {
                BD_WriteDebugInfo("L_StartStudentOperAndDetail", "进入。strStudentID=" + strStudentID + "，strOperCode=" + strOperCode);

                LCStudentOperAndResultM StudentOper = new LCStudentOperAndResultM();
                StudentOper.OperationCode = strOperCode;
                StudentOper.LoginClassID = mLoginClassID;
                StudentOper.Creator = strStudentID;
                StudentOper.OperResultDetail = strOperDetail;
                StudentOper.StartTime = OperStartTime;
                StudentOper.EndTime = OperEndTime;
                StudentOper.OperScore = OperScore;
                StudentOper.StudentID = strStudentID;

                string[] arrParam = new string[1];
                arrParam[0] = JsonFormatter.JsonSerialize(StudentOper);
                string strResult = mCommandApi.CallMethodPost("InsertStudentOper", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return 0;
                }
                int StuOperID = Convert.ToInt32(strResult);
                return StuOperID;
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_StartStudentOperAndDetail", e.Message);
            }
            return 0;
        }
        //测试通过
        //开始学生操作
        public int L_StartStudentOper(string strStudentID, string strOperCode, string strOperDetail)
        {
            try
            {
                BD_WriteDebugInfo("L_StartStudentOper", "进入。strStudentID=" + strStudentID + "，strOperCode=" + strOperCode);

                LCStudentOperM StudentOper = new LCStudentOperM();
                StudentOper.OperationCode = strOperCode;
                StudentOper.LoginClassID = mLoginClassID;
                StudentOper.Creator = strStudentID;
                StudentOper.OperContent = strOperDetail;

                string[] arrParam = new string[1];
                arrParam[0] = JsonFormatter.JsonSerialize(StudentOper);
                string strResult = mCommandApi.CallMethodPost("InsertStudentOper", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return 0;
                }
                int StuOperID = Convert.ToInt32(strResult);
                return StuOperID;
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_StartStudentOper", e.Message);
            }
            return 0;
        }

        //测试通过
        //结束学生操作（根据OperationCode）
        public bool L_EndStudentOper(string strStudentID, string strOperCode)
        {
            try
            {
                BD_WriteDebugInfo("L_EndStudentOper", "进入。strStudentID=" + strStudentID + "，strOperCode=" + strOperCode);

                string[] arrParam = new string[3];
                arrParam[0] = mLoginClassID.ToString();
                arrParam[1] = strOperCode;
                arrParam[2] = strStudentID;
                string strResult = mCommandApi.CallMethodPost("UpdateStudentOperEndTime", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return false;
                }
                int iRowCount = Convert.ToInt32(strResult);
                if (Convert.ToInt32(strResult) == 1)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_EndStudentOper", e.Message);
            }
            return false;
        }

        //结束学生操作（根据OperationID）
        private int L_EndStudentOper(long lLCStuOperID)
        {
            return 0;
        }

        //学生记笔记的行为（可能还需要修改笔记和删除笔记）
        //strBehaviorSubject 该行为发生的某试题/资料编号
        //strTarget 当类型为“采集笔记”或“点赞笔记”时，该项表示目标笔记的所属人编号
        //string strStudentID, string strNoteName, string strNotePath, int iNoteType, string strBehaviorSubject, string strTarget, string strAttachName, string strAttachPath
        public long L_StudentNote(LCUserNoteM note)
        {
            try
            {
                BD_WriteDebugInfo("L_StudentNote", "进入。note=" + JsonFormatter.JsonSerialize(note));

                if (note == null)
                {
                    return 0;
                }

                //补充几个参数
                note.LoginID = mLoginClassID;
                note.CoursePlanID = mCourseClassID;
                note.SubjectID = mMySubjectID;

                string[] arrParam = new string[1];
                arrParam[0] = JsonFormatter.JsonSerialize(note);
                string strResult = mCommandApi.CallMethodPost("InsertStudentNode", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return 0;
                }
                long lNoteID = Convert.ToInt64(strResult);
                return lNoteID;
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_StudentNote", e.Message);
            }
            return 0;
        }

        //保存设置信息（插入前会先查一遍，所以此接口同时具备更新的功能）
        public long L_SaveSetInfo(int iLoginModeID, string strSetItemCode, string strSetItemValue)
        {
            try
            {
                BD_WriteDebugInfo("L_SaveSetInfo", "进入。iLoginModeID=" + iLoginModeID);

                LCSetInfoM SetInfo = new LCSetInfoM();
                SetInfo.LoginClassID = mLoginClassID;
                SetInfo.LCModuleID = mCurLoginModuleID;
                SetInfo.LCModeID = iLoginModeID;
                SetInfo.SetItemCode = strSetItemCode;
                SetInfo.SetItemValue = strSetItemValue;
                SetInfo.Creator = mTeacherID;

                string[] arrParam = new string[1];
                arrParam[0] = JsonFormatter.JsonSerialize(SetInfo);
                string strResult = mCommandApi.CallMethodPost("InsertLoginClassSetInfo", arrParam);
                if (string.IsNullOrEmpty(strResult))
                {
                    return 0;
                }
                long lSetInfoID = Convert.ToInt64(strResult);
                return lSetInfoID;
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_SaveSetInfo", e.Message);
            }
            return 0;
        }

        //结束课堂
        public bool L_EndClass()
        {
            bool bReturn = false;
            try
            {
                BD_WriteDebugInfo("L_EndClass", "进入。");

                string[] arrParam = new string[2];
                arrParam[0] = mLoginClassID.ToString();
                arrParam[1] = mTeacherID.ToString();

                string strResult = mCommandApi.CallMethodPost("UpdateLoginClassEndTime", arrParam);
                if (string.IsNullOrEmpty(strResult)==false)
                {
                    if (Convert.ToInt32(strResult) == 1)
                    {
                        bReturn = true;
                    }
                }

                //发送大数据的EXE路径
                string strExePath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
                strExePath = strExePath + "\\LBD_PostBigData.exe";
                BD_WriteDebugInfo("L_EndClass", "EXE路径=" + strExePath);
                if (File.Exists(strExePath) == false)
                {
                    return bReturn;
                }
                //参数
                string strParam = "{0} {1} {2} {3} {4} {5} {6}";
                string tmpNetTeachApiVirDi = "";
                if (string.IsNullOrEmpty(mNetTeachApiVirDir) == false)//不为空，即为聚合版本
                {
                    if (mNetTeachApiVirDir.EndsWith("/"))
                        tmpNetTeachApiVirDi = mNetTeachApiVirDir.TrimEnd('/');

                    else
                        tmpNetTeachApiVirDi = mNetTeachApiVirDir;
                    if (tmpNetTeachApiVirDi.StartsWith("/") == false)
                        tmpNetTeachApiVirDi = "/" + tmpNetTeachApiVirDi;
                }
                strParam = string.Format(strParam,
                    mLoginClassID, mBigDataAPIAddr, mNetTeachApiIP + ":" + mNetTeachApiPort+ tmpNetTeachApiVirDi, mCloudPlatformBFUrl, mToken, m_curSysID, curClassSubjectId);
                BD_WriteDebugInfo("L_EndClass", "参数=" + strParam);
                //打开发送大数据的EXE
                System.Diagnostics.Process.Start(strExePath, strParam).WaitForExit();

                return bReturn;
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_EndClass", e.Message);
            }
            return false;
        }
        //20190508加一个系统id
        public bool L_EndClass(string sysId)
        {
            bool bReturn = false;
            try
            {
                BD_WriteDebugInfo("L_EndClass", "进入。");

                string[] arrParam = new string[2];
                arrParam[0] = mLoginClassID.ToString();
                arrParam[1] = mTeacherID.ToString();

                string strResult = mCommandApi.CallMethodPost("UpdateLoginClassEndTime", arrParam);
                if (string.IsNullOrEmpty(strResult) == false)
                {
                    if (Convert.ToInt32(strResult) == 1)
                    {
                        bReturn = true;
                    }
                }

                //发送大数据的EXE路径
                string strExePath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
                strExePath = strExePath + "\\LBD_PostBigData.exe";
                BD_WriteDebugInfo("L_EndClass", "EXE路径=" + strExePath);
                if (File.Exists(strExePath) == false)
                {
                    return bReturn;
                }
                //参数
                string strParam = "{0} {1} {2} {3} {4} {5} {6}";
                string tmpNetTeachApiVirDi = "";
                if (string.IsNullOrEmpty(mNetTeachApiVirDir) == false)//不为空，即为聚合版本
                {
                    if (mNetTeachApiVirDir.EndsWith("/"))
                        tmpNetTeachApiVirDi = mNetTeachApiVirDir.TrimEnd('/');

                    else
                        tmpNetTeachApiVirDi = mNetTeachApiVirDir;
                    if (tmpNetTeachApiVirDi.StartsWith("/") == false)
                        tmpNetTeachApiVirDi = "/" + tmpNetTeachApiVirDi;
                }
                strParam = string.Format(strParam,
                    mLoginClassID, mBigDataAPIAddr, mNetTeachApiIP + ":" + mNetTeachApiPort+ tmpNetTeachApiVirDi, mCloudPlatformBFUrl, mToken, sysId, curClassSubjectId);
                BD_WriteDebugInfo("L_EndClass", "参数=" + strParam);
                //打开发送大数据的EXE
                System.Diagnostics.Process.Start(strExePath, strParam).WaitForExit();

                return bReturn;
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_EndClass", e.Message);
            }
            return false;
        }
        private string GetLastLoginIdByTeacher(string teacherId) {
            try 
            {
                string[] arrParam = new string[1];
                arrParam[0] = teacherId;
                string loginId = mCommandApi.CallMethodGet("GetLastLoginIdByTeacher", arrParam);
                return loginId;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetLastLoginIdByTeacher", e.Message);
                return null;
            }
        }

        #endregion

        #region 恢复上一堂课
        /// <summary>
        /// 判断是否有上一堂课信息，返回上一堂课编号，为0表示没有上一堂课
        /// </summary>
        /// <param name="strProductCode">产品代号</param>
        /// <param name="strCourseClassID">课程班ID</param>
        /// <returns>返回上一堂课编号，大于0表示有上一堂课，等于0表示没有上一堂课</returns>
        public int L_HasLastClassInfo(string strProductCode, string strCourseClassID)
        {
            try
            {
                BD_WriteDebugInfo("L_HasLastClassInfo", "进入。strProductCode=" + strProductCode + ",strCourseClassID=" + strCourseClassID);
                
                string[] arrParam = new string[4];
                arrParam[0] = strProductCode;
                arrParam[1] = mTeacherID;
                arrParam[2] = mMySubjectID.ToString();
                arrParam[3] = strCourseClassID;
                string str = mCommandApi.CallMethodGet("HasLastClassInfo", arrParam);
                if (string.IsNullOrEmpty(str))
                {
                    return 0;
                }
                return Convert.ToInt32(str);
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_HasLastClassInfo", e.Message);
                return 0;
            }
        }

        //查询上一堂课登录信息，根据课程班查询（课堂编号，教师ID，课程ID，课程班ID）
        private LastClassLoginInfoM L_GetLastLoginInfo(string strCourseClassID)
        {
            try
            {
                string[] arrParam = new string[1];
                arrParam[0] = strCourseClassID;
                string str = mCommandApi.CallMethodGet("SelectLastLoginInfo", arrParam);
                if (string.IsNullOrEmpty(str))
                {
                    return null;
                }
                LastClassLoginInfoM loginInfo = JsonFormatter.JsonDeserialize<LastClassLoginInfoM>(str);
                return loginInfo;
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_GetLastLoginInfo", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 查询上一堂课总体信息（主要包括最后进入的模块、所选择的资料、每篇资料最后打开的教学模式等信息）
        /// </summary>
        /// <param name="iLastLoginID">上一堂课编号</param>
        /// <returns></returns>
        public LastClassSumInfoM L_GetLastClassSumInfo(int iLastLoginID)
        {
            try
            {
                BD_WriteDebugInfo("L_GetLastClassSumInfo", "进入。iLastLoginID=" + iLastLoginID);

                string[] arrParam = new string[1];
                arrParam[0] = iLastLoginID.ToString();
                string strResult = mCommandApi.CallMethodGet("SelectLastClassSumInfo", arrParam);
                BD_WriteDebugInfo("L_GetLastClassSumInfo", "strResult=" + strResult);
                if (string.IsNullOrEmpty(strResult))
                {
                    return null;
                }
                LastClassSumInfoM classInfo = JsonFormatter.JsonDeserialize<LastClassSumInfoM>(strResult);
                return classInfo;
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_GetLastClassSumInfo", e.Message);
            }

            return null;
        }

        //返回某一教学模式的信息
        //根据上一接口返回的教学模式的LoginModeID，查找上一堂课这个教学模式做了哪些事情
        //返回的实体类是一个复杂的实体类，包含跟读等类型数据还包含测试等类型数据，
        public LastClassModeInfoM L_GetLastClassModeInfo(int iLastLoginModeID)
        {
            try
            {
                string[] arrParam = new string[1];
                arrParam[0] = iLastLoginModeID.ToString();
                string str = mCommandApi.CallMethodGet("SelectLastClassSumInfo", arrParam);
                if (string.IsNullOrEmpty(str))
                {
                    return null;
                }
                LastClassModeInfoM modeInfo = JsonFormatter.JsonDeserialize<LastClassModeInfoM>(str);
                return modeInfo;
            }
            catch (Exception e)
            {
                WriteErrorMessage("L_GetLastClassModeInfo", e.Message);
            }
            return null;
        }

        //返回模式中的设置信息
        public void L_GetLastClassModeSetInfo(int iLastLoginID, int iLastLoginModuleID, int iLastLoginModeID)
        {

        }

        //返回所有工具的信息（这里应该已经通过工具的操作轨迹计算出最终的工具状态）
        public void L_GetLastToolInfo(int iLastLoginID, int iLastLoginModuleID)
        {
            //一个工具的开始时间有了，结束时间也有，则说明是完整的操作，最终状态为弹起
            //一个工具的开始时间有了，结束时间没有，则最终状态为按下
            //还要返回工具操作产生的详细信息
        }

/// <summary>
/// 获取用户所带的而且已经购买的学科
/// </summary>
/// <param name="strToken">token</param>
/// <param name="strUserID">用户id</param>
/// <returns></returns>
        public CloudSubjectM[] GetUserSubjects(string strToken, string strUserID)
        {
            try
            {
                int iErrorFlag = 0;
                //这里可能需要Uri编码和解码
                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append("http://" + CloudPlatformBFIP + ":" + CloudPlatformBFPort + "/UserMgr/TeachInfoMgr/Api/Service_TeachInfo.ashx");
                sbUrl.Append("?token=" + strToken);
                sbUrl.Append("&method=GetSubjectsByUser");
                sbUrl.Append("&params=" + strUserID);
                CloudSubjectM[] subject = CallApiHelper.CallMethodGet_Cloud<CloudSubjectM[]>(sbUrl.ToString(), out iErrorFlag);
                if (subject == null || subject.Length == 0)
                    return null;
                CloudSubjectM[] arrSubject = P_MatchSubject(subject);
                return arrSubject;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetUserSubjects", e.ToString());
                return null;
            }
        }
        //获取并匹配学科的购买信息，将未购买的学科过滤掉
        private  CloudSubjectM[] P_MatchSubject(CloudSubjectM[] arrSubject)
        {
            try
            {
                if (arrSubject == null)
                    return null;
                //获取学科的购买信息
                SubjectPurchaseInfoM info = GetSubjectPurchaseInfo();
                //若未能获取到购买信息，则返回空
                if (info == null)
                    return null;
                if (info.ResultFlag != 1)
                    return null;
                //匹配每个学科的购买信息
                List<CloudSubjectM> buySubject = new List<CloudSubjectM>(arrSubject.Length);
                for (int i = 0; i < arrSubject.Length; i++)
                {
                    if (arrSubject[i].SubjectID.Contains("Chinese"))
                    {
                        if (info.HasChinese == 1)
                            buySubject.Add(arrSubject[i]);
                    }
                    else if (arrSubject[i].SubjectID.Contains("Maths"))
                    {
                        if (info.HasMaths == 1)
                            buySubject.Add(arrSubject[i]);
                    }
                    else if (arrSubject[i].SubjectID.Contains("English"))
                    {
                        if (info.HasEnglish == 1)
                            buySubject.Add(arrSubject[i]);
                    }
                    else if (arrSubject[i].SubjectID.Contains("Physics"))
                    {
                        if (info.HasPhysics == 1)
                            buySubject.Add(arrSubject[i]);
                    }
                    else if (arrSubject[i].SubjectID.Contains("Chemistry"))
                    {
                        if (info.HasChemistry == 1)
                            buySubject.Add(arrSubject[i]);
                    }
                    else if (arrSubject[i].SubjectID.Contains("Biology"))
                    {
                        if (info.HasBiology == 1)
                            buySubject.Add(arrSubject[i]);
                    }
                    else if (arrSubject[i].SubjectID.Contains("Politics"))
                    {
                        if (info.HasPolitics == 1)
                            buySubject.Add(arrSubject[i]);
                    }
                    else if (arrSubject[i].SubjectID.Contains("History"))
                    {
                        if (info.HasHistory == 1)
                            buySubject.Add(arrSubject[i]);
                    }
                    else if (arrSubject[i].SubjectID.Contains("Geography"))
                    {
                        if (info.HasGeography == 1)
                            buySubject.Add(arrSubject[i]);
                    }

                    if (string.IsNullOrEmpty(SubjectHelper.getSubNameById(arrSubject[i].SubjectID)))
                    {
                        buySubject.Add(arrSubject[i]);
                    }
                }
                if (buySubject.Count == 0)
                    return null;
                return buySubject.ToArray();
            }
            catch (Exception e)
            {
                WriteErrorMessage("P_MatchSubject", e.ToString());
                return null;
            }
        }
        /// <summary>
        /// 获取学科购买信息
        /// </summary>
        private  SubjectPurchaseInfoM GetSubjectPurchaseInfo()
        {
            try
            {
                string strWholeUrl = "";
                string strReturn = "";
                XmlDocument xmlDoc = null;
                strWholeUrl = "http://" + CloudPlatformBFIP + ":" + CloudPlatformBFPort + "/Base/WS/Service_Basic.asmx/WS_G_GetBuyedSubjectForSP";
                WriteErrorMessage("GetSubjectPurchaseInfo", strWholeUrl);
                string requestTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string secCode = CP_MD5Helper.GetMd5Hash("000" + requestTime);
                string strParam = string.Format("requestTime={0}&secCode={1}", requestTime, secCode);
                strReturn = CallApiHelper.CallMethod_Post(strWholeUrl, strParam);
                if (string.IsNullOrEmpty(strReturn))
                    return null;
                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);
                XmlNodeList list = xmlDoc.GetElementsByTagName("string");
                if (list == null || list.Count == 0)
                    return null;
                SubjectPurchaseInfoM info = new SubjectPurchaseInfoM();
                info.ResultFlag = Convert.ToInt32(list[0].InnerText);
                switch (info.ResultFlag)
                {
                    case 1:
                        info.ResultMsg = "正常获取";
                        break;
                    case -1:
                        info.ResultMsg = "未检测到加密锁";
                        break;
                    case -2:
                        info.ResultMsg = "加密锁已过期";
                        break;
                    case -3:
                        info.ResultMsg = "加密锁不能用于该产品";
                        break;
                    case -4:
                        info.ResultMsg = "加密锁接口调用错误";
                        break;
                    case -5:
                        info.ResultMsg = "加密锁时钟错误";
                        break;
                    default:
                        info.ResultMsg = "未知错误";
                        break;
                }
                if (info.ResultFlag == 1)
                {
                    info.HasChinese = Convert.ToInt32(CP_EncryptHelper.DecryptCode("000", list[1].InnerText));
                    info.HasMaths = Convert.ToInt32(CP_EncryptHelper.DecryptCode("000", list[2].InnerText));
                    info.HasEnglish = Convert.ToInt32(CP_EncryptHelper.DecryptCode("000", list[3].InnerText));
                    info.HasPhysics = Convert.ToInt32(CP_EncryptHelper.DecryptCode("000", list[4].InnerText));
                    info.HasChemistry = Convert.ToInt32(CP_EncryptHelper.DecryptCode("000", list[5].InnerText));
                    info.HasBiology = Convert.ToInt32(CP_EncryptHelper.DecryptCode("000", list[6].InnerText));
                    info.HasPolitics = Convert.ToInt32(CP_EncryptHelper.DecryptCode("000", list[7].InnerText));
                    info.HasHistory = Convert.ToInt32(CP_EncryptHelper.DecryptCode("000", list[8].InnerText));
                    info.HasGeography = Convert.ToInt32(CP_EncryptHelper.DecryptCode("000", list[9].InnerText));
                }
                else
                {
                    info.HasChinese = -1;
                    info.HasMaths = -1;
                    info.HasEnglish = -1;
                    info.HasPhysics = -1;
                    info.HasChemistry = -1;
                    info.HasBiology = -1;
                    info.HasPolitics = -1;
                    info.HasHistory = -1;
                    info.HasGeography = -1;
                }
                return info;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 获取在线讨论消息
        /// </summary>
        /// <param name="tchID">教师id</param>
        /// <param name="subjectID">学科id</param>
        /// <param name="sysID">系统id,课前：630，课后：510</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns></returns>
        public List<TutorMes> getTutorMes(string tchID, string subjectID, string sysID, int pageIndex, int pageSize)
        {
            string wholeApiUrl=null;
            try
            {
                List<TutorMes> tutorMesList;
                if (string.IsNullOrEmpty(m_tutorSysApiUrl))
                     m_tutorSysApiUrl = GetSubSysApiIPandPort(m_tutorSysId);
                if (string.IsNullOrEmpty(m_bkSysWebUrl))
                    m_bkSysWebUrl = GetSubSysWebIPandPort(m_bkSysId);
                wholeApiUrl = string.Format("http://{0}/api/Tutor/GetRecentlyQuesList?tchID={1}&subjectID={2}&sysID={3}&pageIndex={4}&pageSize={5}", m_tutorSysApiUrl, tchID, subjectID, sysID, pageIndex, pageSize);
                string result = CallApiHelper.CallMethodGet(wholeApiUrl);
                tutorMesList = JsonConvert.DeserializeObject<List<TutorMes>>(result);
                foreach (TutorMes tm in tutorMesList)
                    tm.Url = string.Format("http://{0}/tutor.aspx?"+ UtilityClass.serialObject(tm),m_bkSysWebUrl);
                return tutorMesList;
            }
            catch (Exception e)
            {
                WriteErrorMessage("getTutorMes", e.ToString()+" \n"+wholeApiUrl);
                return null;
            }
        }
     
        #endregion


        private void WriteDebugInfo(string strMethodName, string strInfo)
        {
            try
            {
                DirectoryInfo clsPath = new DirectoryInfo(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory));
                DirectoryInfo clsSubPath = clsPath.CreateSubdirectory("RunningLog\\LBD_WebApiInterface\\Api");

                if (clsSubPath.Exists)
                {
                    DateTime clsDate = DateTime.Now;
                    string strPath = clsSubPath.FullName + "\\TeachInfoI(" + String.Format("{0:yyyy-MM-dd}", clsDate) + ").log";
                    StreamWriter clsWriter = new StreamWriter(strPath, true);
                    clsWriter.WriteLine(String.Format("{0:HH:mm:ss}", clsDate) + " " + strMethodName + ": " + strInfo);
                    clsWriter.Close();
                }
            }
            catch { }
        }

        private void BD_WriteDebugInfo(string strMethodName, string strInfo)
        {
            try
            {
                DirectoryInfo clsPath = new DirectoryInfo(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory));
                DirectoryInfo clsSubPath = clsPath.CreateSubdirectory("RunningLog");

                if (clsSubPath.Exists)
                {
                    DateTime clsDate = DateTime.Now;
                    string strPath = clsSubPath.FullName + "\\大数据测试(" + String.Format("{0:yyyy-MM-dd}", clsDate) + ").log";
                    StreamWriter clsWriter = new StreamWriter(strPath, true);
                    clsWriter.WriteLine("【" + String.Format("{0:HH:mm:ss}", clsDate) + "】 【" + strMethodName + "】 " + strInfo);
                    clsWriter.Close();
                }
            }
            catch { }
        }

        private bool FormatPortAndVirdir(string sFormatedString, out long lPort, out string sVirDir)
        {
            lPort = -100;
            sVirDir = "-1";
            try
            {
                string[] infoArr = sFormatedString.Split('/');
                lPort = long.Parse(infoArr[0]);
                if (infoArr.Length > 2)
                    sVirDir = infoArr[1] + "/";
                else if ((infoArr.Length == 2) && (string.IsNullOrEmpty(infoArr[1]) == true))
                    sVirDir = "";
                else if ((infoArr.Length == 2) && (string.IsNullOrEmpty(infoArr[1]) == false))
                    sVirDir = infoArr[1] + "/";
                else
                    sVirDir = "";
                return true;
            }
            catch (Exception e)
            {
                WriteErrorMessage("FormatPortAndVirdir",e.ToString());
                return false;
                //throw;
            }
        }
        /// <summary>
        /// 重新获取知识点基础库信息。由于知识点打包与资源库的分离，导致原来接口获取的数据不正确。为了兼容云网络智慧教室5.0才有此接口
        /// </summary>
        /// <returns>返回结果，失败或成功</returns>
        public bool ReGetZSDBasicLibInfo()
        {
            string strWholeUrl = "";
            try
            {                
                XmlDocument xmlDoc = null;
                string strZYKGL_IPandPort = GetSubSysApiIPandPort("A10");
                strWholeUrl = "http://" + strZYKGL_IPandPort + "/" + "BaseData/GetBaseData.asmx/WS_Search_GetServerAddressConf";
                string strParam = "serverID=";
                string strReturn = mCommandWS.CallMethodPost(strWholeUrl, strParam);
                if (string.IsNullOrEmpty(strReturn))
                {
                    return false;                    
                }
                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strReturn);
                XmlNodeList list = xmlDoc.GetElementsByTagName("Server");
                if (list == null || list.Count == 0)
                {
                    return false;
                }
                string strModID = "";
                string strServerType = "";
                for (int i = 0; i < list.Count; i++)
                {
                    strModID = list[i].ChildNodes[0].InnerText;
                    strServerType = list[i].ChildNodes[1].InnerText;
                    if (strModID == "A10" && strServerType == "1")
                    {
                        mZSDK_WS_IP = AITeachCloud.Helper.EncryptHelper.DESDecrypt(list[i].ChildNodes[3].InnerText);
                        mZSDK_WS_Port = AITeachCloud.Helper.EncryptHelper.DESDecrypt(list[i].ChildNodes[4].InnerText);
                        if (list[i].ChildNodes.Count > 7) mZSDK_WS_VirDir = list[i].ChildNodes[7].InnerText;
                    }
                    else if (strModID == "A20" && strServerType == "1")
                    {
                        mZSDSB_WS_IP = AITeachCloud.Helper.EncryptHelper.DESDecrypt(list[i].ChildNodes[3].InnerText);
                        mZSDSB_WS_Port = AITeachCloud.Helper.EncryptHelper.DESDecrypt(list[i].ChildNodes[4].InnerText);
                        if (list[i].ChildNodes.Count > 7) mZSDSB_WS_VirDir = list[i].ChildNodes[7].InnerText;
                    }
                    else if (strModID == "A10" && strServerType == "2")
                    {
                        mZSDK_FTP_IP = AITeachCloud.Helper.EncryptHelper.DESDecrypt(list[i].ChildNodes[3].InnerText);
                        mZSDK_FTP_Port = AITeachCloud.Helper.EncryptHelper.DESDecrypt(list[i].ChildNodes[4].InnerText);
                        mZSDK_FTP_UserName = AITeachCloud.Helper.EncryptHelper.DESDecrypt(list[i].ChildNodes[4].InnerText);
                        mZSDK_FTP_Pwd = AITeachCloud.Helper.EncryptHelper.DESDecrypt(list[i].ChildNodes[4].InnerText);
                        if (list[i].ChildNodes.Count > 7) mZSDK_FTP_VirDir = list[i].ChildNodes[7].InnerText;
                    }
                }
                m_strEssayWSIP = mZSDSB_WS_IP;
                m_lngEssayWSPort = Convert.ToInt64(mZSDSB_WS_Port);
                return true;
            }
            catch (Exception e)
            {
                WriteErrorMessage("ReGetZSDBasicLibInfo", e.ToString()+"\n"+ strWholeUrl);
                return false;
            }
        }
        /// <summary>
        /// 获取数字化资源库（A00）版本
        /// </summary>
        /// <returns>字符串</returns>
        public string GetZYKServerVer()
        {
            try
            {
                StringBuilder sbUrl = new StringBuilder();
                string sResourceLibUrl = "";
                sResourceLibUrl = "http://"+ mZYKGL_WS_IP+":"+ mZYKGL_WS_Port + "/";
                if (string.IsNullOrEmpty(mZYKGL_WS_VirDir) == false)
                    sResourceLibUrl = sResourceLibUrl + mZYKGL_WS_VirDir;

                sbUrl.Append(sResourceLibUrl + "api/config/getversion");                
                WriteTrackLog("GetZYKServerVer", "sbUrl = " + sbUrl);
                string strResult = CallApiHelper.CallMethodGet(sbUrl.ToString());
                return strResult;
            }
            catch (Exception e)
            {
                WriteErrorMessage("GetZYKServerVer", e.ToString());
                return "error";
            }

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
                    string strPath = clsSubPath.FullName + "\\TeachInfoI(" + String.Format("{0:yyyy-MM-dd}", clsDate) + ").log";
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
                    clsWriter.WriteLine(String.Format("{0:HH:mm:ss}", clsDate) + " " + strMethodName + ": " + strErrorMessage);
                    clsWriter.Flush();
                    clsWriter.Close();
                }
            }
            catch { }
        }
    }
}
