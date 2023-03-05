#pragma once
#ifndef _IWEB_H
#define _IWEB_H
//获得转换后的字符串类型数据，仅转换字符串
//我知道这会降低可读性，但是爷乐意


//Author:Leventure
//Date:2023.3.1
//Info:教室相关的信息

#ifdef _IWEB
#define WEB_API __declspec(dllexport)
#else
#define WEB_API __declspec(dllimport)
#endif

#include <string>
using namespace std;
namespace WebApi_Api {
	class WEB_API TeachInfo {
#pragma region 枚举类型
		// 学科
		enum E_Subject
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
			科学 = 10,
			美术 = 11
		};

		//其它系统名称（从本系统接口获取，区别于云平台）
		enum E_OtherSysName
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
		};

		/// <summary>
		/// 教学系统产品名称
		/// </summary>
		enum E_TeachProductName
		{
			Default = 0,//默认为英语课堂教学系统5.2
			EnglishClassRoomTeachSystemV52 = 1,
			GeneralClassRoomTeachSystemV20 = 2,
			InternetStudySystemV10 = 3
		};
		/// <summary>
		/// 教学模块
		/// </summary>
		enum E_Teach_Module
		{
			课堂讲解 = 1,
			课堂辅导 = 2,
			随堂综合测试 = 3,
			口语测试 = 4,
			作业 = 5,
			管理平台 = 6,
			多媒体教学 = 7
		};

		//现在多学科，教学模式有变更。但其它学科的又不能直接追加在后面，因为有相同名字的模式
		/// <summary>
		/// 教学模式
		/// </summary>
		enum E_TeachClass_Mode
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
		};

		/// <summary>
		/// 操作类型
		/// </summary>
		enum E_Operation
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
		};

		/// <summary>
		/// 学生获取分数类型
		/// </summary>
		enum E_DataType
		{
			手动课堂加分 = 1,
			课堂测试加分 = 2,
			课堂训练 = 3,
			其他类型 = 4
		};

		/// <summary>
		/// 教师设置项
		/// </summary>
		enum E_TeachSetItem
		{
			常用教材设置 = 1,
			更新体验 = 2,
			主界面定制提示 = 3,//'用于控制主界面定制提示是否显示
			本地收藏地址 = 4,//'教师设置本地收藏地址
			资料讲解全屏 = 5//xiezongwu20130711增加资料讲解是否全屏设置，默认不全屏
		};

		/// <summary>
		/// 资料来源类别
		/// </summary>
		enum E_OriResourType
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
			电子资源库 = 11,
			数字化资源库 = 12,
			网络化课件库 = 13,
			U盘 = 14,
			专用教材 = 15,
			课前预习 = 16,
			课堂教案 = 17,
			课后练习 = 18,
			智能化课件 = 19,
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
		};

		/// <summary>
		/// 资料的结果类型
		/// </summary>
		enum E_ResourceResultType
		{
			错误率 = 1,
			平均分 = 2
		};
#pragma endregion

	public:
		bool Initialize(string sCloudBasicPlatformBFUrl);
		bool Initialize(string strServerIP, string strPort);
		//TODO:ENUM之间如何转化？
		//bool Initialize(string sCloudBasicPlatformBFUrl, E_TeachProductName sProductName);
		bool InitializeXKJXY(string sCloudBasicPlatformBFUrl);
		
		bool InitInfoForUser(string strToken, string strTeacherID);
		bool ReGetZSDBasicLibInfo();
		string GetZYKServerVer();
		int L_HasLastClassInfo(string strProductCode, string strCourseClassID);
		//bool InitInfoForSubject(E_Subject eSubject);
		bool InitInfoForCustomSubject(string cusSubjectId, string cusSubjectName);
		//bool Initialize_BS(string strNetTeachIP, string strNetTeachPort, string strToken, string strTeacherID, E_Subject eSubject);
		//int UserLogin(string strUserAccount, string strUserPwd, string strMachineType, string strLoginIP, string strMacAddress, string& strLoginContent);
		bool CheckUserOnline();
		bool UserLogout();
		//LoginUserInfo GetOnlineUserInfo();
		//LoginUserInfo GetOnlineUserInfo(string token)
		//ScheduleInfoM[] GetScheduleByNetClassRoom(string strClassroomID, DateTime dtLessonTime);
		//ScheduleInfoM[] GetScheduleByNetClassRoom_WS(string strClassroomID, DateTime dtLessonTime);
		bool JudgeDeviceDetec();
		//SchoolBaseInfoM GetSchoolInfoByID(string strSchoolID)
		//bool GetCloudPlatformSubject(string& strSubjectID, string& strSubjectName);
		//TeacherInfoSimpleM GetTeacherInfoSimple();
		//TeacherInfoDetailM GetTeacherInfoDetail();
		//CloudPlatformSubsystemM GetSubSysAddr(string strSysID)
		//CloudPlatformSubsystemM GetSubSysAddr(string strSysID, string strSubjectID);
		string GetSubSysWebIPandPort(string strSysID);
		string GetSubSysApiIPandPort(string strSysID);
		//ClassroomInfoM[] GetClassroomInfo_Total()
		// 这种应该是vector<string> 或者是什么，不太懂，放到后面再去改写吧
		//bool GetClassroomInfo_Simple(out string[] arrID, out string[] arrName)
		//ClassroomInfoM[] GetClassroomInfo_Total2()
		//bool GetClassroomInfo_Simple2(out string[] arrID, out string[] arrName)
		//int GetGradeInfo(out string[] arrGradeID, out string[] arrGradeName)
		//int GetClassInfoByGradeID(string strGradeID, out string[] arrClassID, out string[] arrClassName, out int[] arrStudentNum)
		//bool GetClassInfoByClassID(string strClassID, out string strClassName, out string strGradeID, out string strGradeName)
		//int GetStudentInfoByClassID(string strClassID, out string[] arrXH, out string[] arrName, out string[] arrGender, out string[] arrPhotoPath)
		int GetStudentNumByClassID(string strClassID);
		//int GetCourseClassInfoByGH(out string[] arrCourseNo, out string[] arrCourseName, out string[] arrCourseClassID, out string[] arrCourseClassName, out string[] arrSubjectID, out string[] arrStudyLevelID, out string[] arrStudyLevelName, out string[] arrGloba
		//GetCourseClassInfoOfTeacher(out string[] arrCourseNo, out string[] arrCourseName, out string[] arrCourseClassID, out string[] arrCourseClassName, out string[] arrSubjectID, out string[] arrStudyLevelID, out string[] arrStudyLevelName)
		//int GetCourseClassInfoByUser(out string[] arrCourseNo, out string[] arrCourseName, out string[] arrCourseClassID, out string[] arrCourseClassName, out string[] arrSubjectID, out string[] arrStudyLevelID, out string[] arrStudyLevelName, out string[] a
		//int GetStudentInfoByCourseClassID(string strCourseClassID, out string[] arrXH, out string[] arrName, out string[] arrGender, out string[] arrPhotoPath, out string[] arrClassID, out string[] arrClassName)
		//SubjectPlatformSysInfoM[] GetSubjectPlatformSysInfo(string strSysID, int iUserType)
		//SubjectPlatformSysInfoM[] GetSubjectPlatformSysInfoV2(string strSysID, int iUserType)
		//SubjectPlatformSysInfoM[] GetSubjectPlatformSysInfoForStu()
		string GetDigitalLibraryUrl(int iUserType);
		//CLPSysConfigInfo GetCLPSysConfigInfo()
		//string[] GetSomeSystemWebUrl(int iUserType)
		//CloudPlatformSubjectM[] GetSubjectsByUserID(string strToken, string strUserID, out bool bValidToken)
		int WS_G_SetNewLockPoint(string sysId, string token);
		//int GetSubSystemLockerInfoByID(string slockerID, int& iProductPointCount, string& sProbationYear, string& sProbationMonth, string& sProbationDay);
		//TeachModuleM[] GetSubjectTeachModule()
		//LBD_WebApiInterface.ClassTeach.TeachModeM[] GetSubjectTeachMode()
		int UpdateNetCoursewareStatus(string strCoursewareID, bool bStatus, string strLastEditor);
		//LastLoginInfoV51M GetLastLoginInfoByTeacherID(string strCourseClassID)
		//LastLoginInfoV51M GetLastLoginInfoByLoginID(int intLastLoginID)
		//LastClassInfoV51M GetLastCourseInfoV51(int intLastLoginID)
		//bool GetResourceTypeIDandName(E_OriResourType eResourceType, int& iTypeID, string& strTypeName);
#pragma region 获得属性

		string ZYK_DB_IP();
		string ZYK_DB_Name();
		string ZYK_DB_UserName();
		string ZYK_DB_UserPwd();
		string CloudPlatformBFIP();
		string CloudPlatformBFPort();
		string KnowledgeWSIP();
		long long KnowledgeWSPort();
		string KnowledgeDBIP();
		string KnowledgeDBName();
		string KnowledgeDBUserName();
		string KnowledgeDBUserPwd();
		string ThemeVideoWSIP();
		string ThemeVideoPort();
		string EssayWSIP();
		long long EssayWSPort();
		string ZSDSB_WS_VirDir();
		string ZYK_FTP_IP();
		long long ZYK_FTP_Port();
		string ZYK_FTP_UserName();
		string ZYK_FTP_UserPwd();
		string ZYK_FTP_Name();
		string ZYK_FTP_PhyPath();
		string ZYK_HTTP_IP();
		int ZYK_HTTP_Port();
#pragma endregion
	};
}

#endif