#include "pch.h"
#define _IWEB

#include "ITeachInfo.h"
#include "Trans.h"
#define INS(value){\
s2s(CLR::TeachInfo_Instance()->value)\
}
//返回原始数据，比如int longlong什么的，这种没得转，主要是转字符串类型，因为.net的字符串是托管在framework上的
//不是系统原生的类型
#define RAW(value){\
CLR::TeachInfo_Instance()->value\
}
using namespace std;

bool WEB_API WebApi_Api::TeachInfo::Initialize(string strValue) {
	return CLR::TeachInfo_Instance()->Initialize(s2s(strValue));
}

bool WEB_API WebApi_Api::TeachInfo::Initialize(string strServerIP, string strPort) {
	return CLR::TeachInfo_Instance()->Initialize(s2s(strServerIP), s2s(strPort));
}

bool WebApi_Api::TeachInfo::InitializeXKJXY(string sCloudBasicPlatformBFUrl)
{
	return RAW(InitializeXKJXY(s2s(sCloudBasicPlatformBFUrl)));
}



bool WebApi_Api::TeachInfo::InitInfoForUser(string strToken, string strTeacherID)
{
	return RAW(InitInfoForUser(s2s(strToken), s2s(strTeacherID)));
}

bool WEB_API WebApi_Api::TeachInfo::ReGetZSDBasicLibInfo() {
	return CLR::TeachInfo_Instance()->ReGetZSDBasicLibInfo();
}

string WebApi_Api::TeachInfo::GetZYKServerVer()
{
	return INS(GetZYKServerVer());
}

int WebApi_Api::TeachInfo::L_HasLastClassInfo(string strProductCode, string strCourseClassID)
{
	return CLR::TeachInfo_Instance()->L_HasLastClassInfo(s2s(strProductCode), s2s(strCourseClassID));
}

bool WebApi_Api::TeachInfo::InitInfoForCustomSubject(string cusSubjectId, string cusSubjectName)
{
	return RAW(InitInfoForCustomSubject(s2s(cusSubjectId), s2s(cusSubjectName)));
}

bool WebApi_Api::TeachInfo::CheckUserOnline()
{
	return RAW(CheckUserOnline());
}

bool WebApi_Api::TeachInfo::UserLogout()
{
	return RAW(UserLogout());
}

bool WebApi_Api::TeachInfo::JudgeDeviceDetec()
{
	return RAW(JudgeDeviceDetec());
}

string WebApi_Api::TeachInfo::GetSubSysWebIPandPort(string strSysID)
{
	return INS(GetSubSysWebIPandPort(s2s(strSysID)));
}

string WebApi_Api::TeachInfo::GetSubSysApiIPandPort(string strSysID)
{
	return INS(GetSubSysApiIPandPort(s2s(strSysID)));
}

int WebApi_Api::TeachInfo::GetStudentNumByClassID(string strClassID)
{
	return RAW(GetStudentNumByClassID(s2s(strClassID)));
}

string WebApi_Api::TeachInfo::GetDigitalLibraryUrl(int iUserType)
{
	return INS(GetDigitalLibraryUrl(iUserType));
}

int WebApi_Api::TeachInfo::WS_G_SetNewLockPoint(string sysId, string token)
{
	return RAW(WS_G_SetNewLockPoint(s2s(sysId), s2s(token)));
}

int WebApi_Api::TeachInfo::UpdateNetCoursewareStatus(string strCoursewareID, bool bStatus, string strLastEditor)
{
	return CLR::TeachInfo_Instance()->UpdateNetCoursewareStatus(s2s(strCoursewareID), bStatus, s2s(strLastEditor));
}










string WEB_API WebApi_Api::TeachInfo::ZYK_DB_IP()
{
	return INS(ZYK_DB_IP);
}

string WEB_API WebApi_Api::TeachInfo::ZYK_DB_Name()
{
	return INS(ZYK_DB_Name);
}

string WEB_API WebApi_Api::TeachInfo::ZYK_DB_UserName()
{
	return INS(ZYK_DB_UserName);
}

string WEB_API WebApi_Api::TeachInfo::ZYK_DB_UserPwd()
{
	return INS(ZYK_DB_UserPwd);
}

string  WEB_API WebApi_Api::TeachInfo::CloudPlatformBFIP()
{
	return INS(CloudPlatformBFIP);
}

string WEB_API WebApi_Api::TeachInfo::CloudPlatformBFPort()
{
	return INS(CloudPlatformBFPort);
}

string WEB_API WebApi_Api::TeachInfo::KnowledgeWSIP()
{
	return INS(KnowledgeWSIP);
}

long long WEB_API WebApi_Api::TeachInfo::KnowledgeWSPort()
{
	return RAW(KnowledgeWSPort);
}

string WEB_API WebApi_Api::TeachInfo::KnowledgeDBIP()
{
	return INS(KnowledgeDBIP);
}

string WEB_API WebApi_Api::TeachInfo::KnowledgeDBName()
{
	return INS(KnowledgeDBName);
}

string WEB_API WebApi_Api::TeachInfo::KnowledgeDBUserName()
{
	return INS(KnowledgeDBUserName);
}

string WEB_API WebApi_Api::TeachInfo::KnowledgeDBUserPwd()
{
	return INS(KnowledgeDBUserPwd);
}

string WEB_API WebApi_Api::TeachInfo::ThemeVideoWSIP()
{
	return INS(ThemeVideoWSIP);
}

string WEB_API WebApi_Api::TeachInfo::ThemeVideoPort()
{
	return INS(ThemeVideoPort);
}

string WEB_API WebApi_Api::TeachInfo::EssayWSIP()
{
	return INS(EssayWSIP);
}

long long WEB_API WebApi_Api::TeachInfo::EssayWSPort()
{
	return RAW(EssayWSPort);
}

string WEB_API WebApi_Api::TeachInfo::ZSDSB_WS_VirDir()
{
	return INS(ZSDSB_WS_VirDir);
}

string WEB_API WebApi_Api::TeachInfo::ZYK_FTP_IP()
{
	return INS(ZYK_FTP_IP);
}

long long WEB_API WebApi_Api::TeachInfo::ZYK_FTP_Port()
{
	return RAW(ZYK_FTP_Port);
}

string WEB_API WebApi_Api::TeachInfo::ZYK_FTP_UserName()
{
	return INS(ZYK_FTP_UserName);
}

string WEB_API WebApi_Api::TeachInfo::ZYK_FTP_UserPwd()
{
	return INS(ZYK_FTP_UserPwd);
}

string WEB_API WebApi_Api::TeachInfo::ZYK_FTP_Name()
{
	return INS(ZYK_FTP_Name);
}

string WEB_API WebApi_Api::TeachInfo::ZYK_FTP_PhyPath()
{
	return INS(ZYK_FTP_PhyPath);
}

string WEB_API WebApi_Api::TeachInfo::ZYK_HTTP_IP()
{
	return INS(ZYK_HTTP_IP);
}

int WEB_API WebApi_Api::TeachInfo::ZYK_HTTP_Port()
{
	return RAW(ZYK_HTTP_Port);
}