#include "pch.h"
#define _IWEB

#include "ITeachInfo.h"
#include "Trans.h"

using namespace std;

bool WEB_API WebApi_Api::TeachInfo::Initialize(string strValue) {

	return CLR::Instance()->Initialize(s2s(strValue));
}

bool WEB_API WebApi_Api::TeachInfo::Initialize(string strServerIP, string strPort){
	return CLR::Instance()->Initialize(s2s(strServerIP), s2s(strPort));
}

bool WEB_API WebApi_Api::TeachInfo::ReGetZSDBasicLibInfo(){
	return CLR::Instance()->ReGetZSDBasicLibInfo();
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
