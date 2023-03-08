#pragma once
//Author:Leventure
//Data:2023.3.7
//Info:UserInfoM��Ķ���ӿ�
#include <string>
using namespace std;
namespace WebApi_Model {
    public class LoginUserInfo_cpp
    {
    public:
        string UserID;
        string UserName;
        string Gender;
        string GradeID;
        string GradeName;
        string GlobalGrade;
        string GroupID;
        string GroupName;
        string SubjectIDs;
        string SubjectNames;
        int UserType;
        int UserClass;
        string PhotoPath;
        string PreLoginTime;
        string PreLoginIP;
        string ShortName;
        string Sign;
        string SchoolID;
        string SchoolName;
        string UpdateTime;
        string Token;
        string LoginInfo;

    };
}
