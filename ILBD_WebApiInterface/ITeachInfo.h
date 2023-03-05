﻿#pragma once
#ifndef _IWEB_H
#define _IWEB_H

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

	public:
		bool Initialize(string sCloudBasicPlatformBFUrl);
		bool Initialize(string strServerIP, string strPort);
		bool ReGetZSDBasicLibInfo();

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