using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LBD_WebApiInterface.Models.CloudPlatform
{
    //管理员信息
    public class ManagerInfoM
    {
        public string UserID { get; set; }
        public string ShorName { get; set; }
        public string UserName { get; set; }
        public string UserType { get; set; }
        public string UserClass { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string PhotoPath { get; set; }
        public string Sign { get; set; }
        public string QQ { get; set; }
        public string Weibo { get; set; }
        public string Weixin { get; set; }
        public string Renren { get; set; }
        public string SchoolID { get; set; }
        public string UpdateTime { get; set; }
    }

    public class GradeInfoM
    {
        public string GradeID { get; set; }
        public string GradeName { get; set; }
        public string GlobalGrade { get; set; }
        public string OrderNo { get; set; }
        public string SchoolID { get; set; }
    }

    public class ClassInfoM
    {
        public string ClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassNameQM { get; set; }
        public string GradeID { get; set; }
        public string OrderNo { get; set; }
        public string UpdateTime { get; set; }
    }

    public class StudentInfoM
    {
        public string UserID { get; set; }
        public string ShorName { get; set; }
        public string UserName { get; set; }
        public string ClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassNameQM { get; set; }
        public string UserClass { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string PhotoPath { get; set; }
        public string Sign { get; set; }
        public string QQ { get; set; }
        public string Weibo { get; set; }
        public string Weixin { get; set; }
        public string Renren { get; set; }
        public string SchoolID { get; set; }
        public string UpdateTime { get; set; }
    }

    public class StudentInfoSimpleM
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public string ClassID { get; set; }
        public string PhotoPath { get; set; }
    }

    public class CourseClassInfoM
    {
        public string CourseClassID { get; set; }
        public string CourseClassName { get; set; }
        public string CourseNo { get; set; }
        public string CourseName { get; set; }
        public string TeacherID { get; set; }
        public string TeacherName { get; set; }
        public string UpdateTime { get; set; }
    }
    //add by qinkun20171205
    public class CourseClassInfoExM
    {
        public string CourseClassID { get; set; }
        public string CourseClassName { get; set; }
        public string CourseNo { get; set; }
        public string CourseName { get; set; }
        public string TeacherID { get; set; }
        public string TeacherName { get; set; }
        public string UpdateTime { get; set; }
        public string SubjectID { get; set; }//学科ID
        public string StudyLevelID { get; set; }//学习阶段ID
        public string StudyLevelName { get; set; }//学习阶段名称
        public string GlobalGrade { get; set; }//年级标识，K1~K12（一年级至高中三年级），U1~U5(大一至大五)，M1~M3（中职一至中职三），H1~H5（高职一至高职五）
    }

    public class CourseStudentInfoM
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhotoPath { get; set; }
        public string ClassIDQM { get; set; }
        public string ClassNameQM { get; set; }
        public string CourseClassID { get; set; }
        public string CourseClassName { get; set; }
        public string UpdateTime { get; set; }
    }

    public class LoginUserInfo
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public string GradeID { get; set; }
        public string GradeName { get; set; }
        public string GlobalGrade { get; set; }
        public string GroupID { get; set; }
        public string GroupName { get; set; }
        public string SubjectIDs { get; set; }
        public string SubjectNames { get; set; }
        public int UserType { get; set; }
        public int UserClass { get; set; }
        public string PhotoPath { get; set; }
        public string PreLoginTime { get; set; }
        public string PreLoginIP { get; set; }
        public string ShortName { get; set; }
        public string Sign { get; set; }
        public string SchoolID { get; set; }
        public string SchoolName { get; set; }
        public string UpdateTime { get; set; }
        public string Token { get; set; }
        public string LoginInfo { get; set; }
    }

    /// <summary>
    /// 教师信息（简略版）
    /// </summary>
    public class TeacherInfoSimpleM
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public string GroupID { get; set; }
        public string PhotoPath { get; set; }
    }

    /// <summary>
    /// 教师信息（详细版）
    /// </summary>
    public class TeacherInfoDetailM
    {
        public string UserID { get; set; }
        public string ShortName { get; set; }
        public string UserName { get; set; }
        public string GroupID { get; set; }
        public string GroupName { get; set; }
        public string TitileName { get; set; }
        public string RoleNames { get; set; }
        public string SchoolID { get; set; }
        public string UserClass { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string PhotoPath { get; set; }
        public string Sign { get; set; }
        public string QQ { get; set; }
        public string Weibo { get; set; }
        public string Weixin { get; set; }
        public string Renren { get; set; }
        public string UpdateTime { get; set; }
    }

}
