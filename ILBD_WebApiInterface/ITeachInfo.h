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
#include "Api_Enums.h"
#include <string>
#include "UserInfoM.h"
using namespace std;
using namespace WebApi_Model;
namespace WebApi_Api {
	class WEB_API TeachInfo {
	public:


	public:
		bool Initialize(string sCloudBasicPlatformBFUrl);
		bool Initialize(string strServerIP, string strPort);
		bool Initialize(string sCloudBasicPlatformBFUrl, E_TeachProductName_cpp sProductName);
		bool InitializeXKJXY(string sCloudBasicPlatformBFUrl);
		
		bool InitInfoForUser(string strToken, string strTeacherID);
		bool ReGetZSDBasicLibInfo();
		string GetZYKServerVer();
		int L_HasLastClassInfo(string strProductCode, string strCourseClassID);
		bool InitInfoForSubject(E_Subject_cpp eSubject);
		bool InitInfoForCustomSubject(string cusSubjectId, string cusSubjectName);
		bool Initialize_BS(string strNetTeachIP, string strNetTeachPort, string strToken, string strTeacherID, E_Subject_cpp eSubject);
		int UserLogin(string strUserAccount, string strUserPwd, string strMachineType, string strLoginIP, string strMacAddress, string& strLoginContent);
		bool CheckUserOnline();
		bool UserLogout();
		LoginUserInfo_cpp GetOnlineUserInfo();
		LoginUserInfo_cpp GetOnlineUserInfo(string token);
		//ScheduleInfoM[] GetScheduleByNetClassRoom(string strClassroomID, DateTime dtLessonTime);
		//ScheduleInfoM[] GetScheduleByNetClassRoom_WS(string strClassroomID, DateTime dtLessonTime);
		bool JudgeDeviceDetec();
		//SchoolBaseInfoM GetSchoolInfoByID(string strSchoolID)
		bool GetCloudPlatformSubject(string& strSubjectID, string& strSubjectName);
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
		//这个功能获得系统的锁控信息
		int GetSubSystemLockerInfoByID(string slockerID, int& iProductPointCount, string& sProbationYear, string& sProbationMonth, string& sProbationDay);
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