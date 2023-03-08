#pragma once
//Author:Leventure
//data:2023.3.7
//Info:�������cpp�ļ������ã������ṩһЩת����������.net�е���ת����cpp�ܶ��ö����࣬ͨ���Կ������캯������ת���������У���������ڸ��µĹ�����

#include "UserInfoM.h"
#include "BaseInfoM.h"
#include "Trans.h"
using namespace LBD_WebApiInterface::Models::CloudPlatform;
using namespace LBD_WebApiInterface::Models;
using namespace WebApi_Model;





//���Ǹ��������캯��
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

CloudPlatformSubsystem_cpp to_CloudPlatformSubsystem(CloudPlatformSubsystemM^ Info) {

	CloudPlatformSubsystem_cpp result;
	TRANCPP_S(result, Info, SysID);
	TRANCPP_S(result, Info, SubjectID);
	TRANCPP_S(result, Info, WebSvrAddr);
	TRANCPP_S(result, Info, WsSvrAddr);

	return result;
}
