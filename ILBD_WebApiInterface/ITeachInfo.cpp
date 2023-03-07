#include "pch.h"
#define _IWEB

#include "ITeachInfo.h"
#include "Trans.h"
#include "Model_Trans.h"
//����ԭʼ���ݣ�����int longlongʲô�ģ�����û��ת����Ҫ��ת�ַ������ͣ���Ϊ.net���ַ������й���framework�ϵ�
//����ϵͳԭ��������
#define RAW(value){\
CLR::TeachInfo_Instance()->value\
}
//����string���ͣ�����ת��
#define INS(value){\
s2s(RAW(value))\
}


//��ת��ʾ��
ENUM_MATCH(WebApi_Api::E_TeachProductName_cpp, LBD_WebApiInterface::Api::TeachInfoI::E_TeachProductName, E_TeachProductName_cpp, E_TeachProductName)
ENUM_MATCH(WebApi_Api::E_Subject_cpp, LBD_WebApiInterface::Api::TeachInfoI::E_Subject, E_Subject_cpp, E_Subject)

using namespace std;



bool WEB_API WebApi_Api::TeachInfo::Initialize(string strValue) {
	return CLR::TeachInfo_Instance()->Initialize(s2s(strValue));
}

bool WEB_API WebApi_Api::TeachInfo::Initialize(string strServerIP, string strPort) {
	return RAW(Initialize(s2s(strServerIP), s2s(strPort)));
}

bool WebApi_Api::TeachInfo::Initialize(string sCloudBasicPlatformBFUrl, E_TeachProductName_cpp sProductName)
{
	return RAW(Initialize(s2s(sCloudBasicPlatformBFUrl), ToE_TeachProductName(sProductName)));
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

bool WebApi_Api::TeachInfo::InitInfoForSubject(E_Subject_cpp eSubject)
{
	return RAW(InitInfoForSubject(ToE_Subject(eSubject)));
}



bool WebApi_Api::TeachInfo::InitInfoForCustomSubject(string cusSubjectId, string cusSubjectName)
{
	return RAW(InitInfoForCustomSubject(s2s(cusSubjectId), s2s(cusSubjectName)));
}

bool WebApi_Api::TeachInfo::Initialize_BS(string strNetTeachIP, string strNetTeachPort, string strToken, string strTeacherID, E_Subject_cpp eSubject)
{
	return RAW(Initialize_BS(s2s(strNetTeachIP), s2s(strNetTeachPort), s2s(strToken), s2s(strTeacherID), ToE_Subject(eSubject)));
}

int WebApi_Api::TeachInfo::UserLogin(string strUserAccount, string strUserPwd, string strMachineType, string strLoginIP, string strMacAddress, string& strLoginContent)
{
	System::String^ cs_strLoginContent = s2s(strLoginContent);
	int result = 0;
	result = RAW(UserLogin(s2s(strUserAccount), s2s(strUserPwd), s2s(strMachineType), s2s(strLoginIP), s2s(strMacAddress), cs_strLoginContent));
	strLoginContent = s2s(cs_strLoginContent);
	return 0;
}



bool WebApi_Api::TeachInfo::CheckUserOnline()
{
	return RAW(CheckUserOnline());
}

bool WebApi_Api::TeachInfo::UserLogout()
{
	return RAW(UserLogout());
}

LoginUserInfo_cpp WebApi_Api::TeachInfo::GetOnlineUserInfo()
{
	return to_LoginUserInfo(RAW(GetOnlineUserInfo()));
}

LoginUserInfo_cpp WebApi_Api::TeachInfo::GetOnlineUserInfo(string token)
{
	return to_LoginUserInfo(RAW(GetOnlineUserInfo(s2s(token))));
}

bool WebApi_Api::TeachInfo::JudgeDeviceDetec()
{
	return RAW(JudgeDeviceDetec());
}

bool WebApi_Api::TeachInfo::GetCloudPlatformSubject(std::string& strSubjectID, std::string& strSubjectName)
{
	System::String^ cs_strSubjectID = s2s(strSubjectID);
	System::String^ cs_strSubjectName = s2s(strSubjectName);
	bool result = false;

	result = CLR::TeachInfo_Instance()->GetCloudPlatformSubject(cs_strSubjectID, cs_strSubjectName);

	strSubjectID = s2s(cs_strSubjectID);
	strSubjectName = s2s(cs_strSubjectName);

	return result;
}

string WEB_API WebApi_Api::TeachInfo::GetSubSysWebIPandPort(string strSysID)
{
	return INS(GetSubSysWebIPandPort(s2s(strSysID)));
}

string WEB_API WebApi_Api::TeachInfo::GetSubSysApiIPandPort(string strSysID)
{
	return INS(GetSubSysApiIPandPort(s2s(strSysID)));
}

int WEB_API WebApi_Api::TeachInfo::GetStudentNumByClassID(string strClassID)
{
	return RAW(GetStudentNumByClassID(s2s(strClassID)));
}

string WEB_API WebApi_Api::TeachInfo::GetDigitalLibraryUrl(int iUserType)
{
	return INS(GetDigitalLibraryUrl(iUserType));
}

int WEB_API WebApi_Api::TeachInfo::WS_G_SetNewLockPoint(string sysId, string token)
{
	return RAW(WS_G_SetNewLockPoint(s2s(sysId), s2s(token)));
}

int WebApi_Api::TeachInfo::GetSubSystemLockerInfoByID(string slockerID, int& iProductPointCount, string& sProbationYear, string& sProbationMonth, string& sProbationDay)
{
	System::String^ cs_sProbationYear = s2s(sProbationYear);
	System::String^ cs_sProbationMonth = s2s(sProbationMonth);
	System::String^ cs_sProbationDay = s2s(sProbationDay);

	int result = 0;
	result = RAW(GetSubSystemLockerInfoByID(s2s(slockerID), iProductPointCount, cs_sProbationYear, cs_sProbationMonth, cs_sProbationDay));

	sProbationYear = s2s(cs_sProbationYear);
	sProbationMonth = s2s(cs_sProbationMonth);
	sProbationDay = s2s(cs_sProbationDay);

	return result;
}

int WEB_API WebApi_Api::TeachInfo::UpdateNetCoursewareStatus(string strCoursewareID, bool bStatus, string strLastEditor)
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