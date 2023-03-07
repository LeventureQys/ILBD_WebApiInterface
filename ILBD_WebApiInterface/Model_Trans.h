#pragma once
//Author:Leventure
//data:2023.3.7
//Info:这个类在cpp文件中引用，用于提供一些转换函数，将.net中的类转换成cpp能读得懂的类，通常以拷贝构造函数或者转换函数进行，具体策略在更新的过程中

#include "UserInfoM.h"
#include "Trans.h"
using namespace LBD_WebApiInterface::Models::CloudPlatform;
using namespace WebApi_Model;





//算是个拷贝构造函数
LoginUserInfo_cpp to_LoginUserInfo(LoginUserInfo^ Info) {
	LoginUserInfo_cpp result;

	TRANCPP_S(result, Info, UserID);
	TRANCPP_S(result, Info, UserName);
	TRANCPP_S(result, Info, Gender);
	TRANCPP_S(result, Info, GradeID);
	TRANCPP_S(result, Info, GradeName);
	TRANCPP_S(result, Info, GlobalGrade);
	TRANCPP_S(result, Info, GroupID);
	TRANCPP_S(result, Info, GroupName);
	TRANCPP_S(result, Info, SubjectIDs);
	TRANCPP_S(result, Info, SubjectNames);
	TRANCPP(result, Info, UserType);
	TRANCPP(result, Info, UserClass);
	TRANCPP_S(result, Info, PhotoPath);
	TRANCPP_S(result, Info, PreLoginTime);
	TRANCPP_S(result, Info, PreLoginIP);
	TRANCPP_S(result, Info, ShortName);
	TRANCPP_S(result, Info, Sign);
	TRANCPP_S(result, Info, SchoolID);
	TRANCPP_S(result, Info, SchoolName);
	TRANCPP_S(result, Info, UpdateTime);
	TRANCPP_S(result, Info, Token);
	TRANCPP_S(result, Info, LoginInfo);

	return result;

}

