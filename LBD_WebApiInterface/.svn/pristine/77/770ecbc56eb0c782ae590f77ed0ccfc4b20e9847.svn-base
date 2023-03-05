using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * 此命名空间下的实体为教学系统实体到大数据中心实体过渡的实体。
 * 因为大数据所需数据的结构与教学系统的结构很不相同，直接转换需要多次
 * 调用同一接口获取重复数据，故用这些实体一次性读取所需数据并在内存中缓存，
 * 组织大数据时可重复访问这些实体。另外也可以协调当大数据实体和本系统实体两方的修改
 */
namespace LBD_WebApiInterface.Models.BigData
{
    public class BD_ModuleInfoM
    {
        public int LoginClassID { get; set; }
        public int LoginModuleID { get; set; }
        public short ModuleID { get; set; }
        public List<BD_LCTeachModeInfoM> LoginModeInfo { get; set; }
        public List<BD_ResourceInfoM> Resource { get; set; }

        public BD_ModuleInfoM()
        {
            LoginModeInfo = new List<BD_LCTeachModeInfoM>();
            Resource = new List<BD_ResourceInfoM>();
        }
    }

    public class BD_LCTeachModeInfoM
    {
        public int LoginModuleID { get; set; }
        public long LoginModeID { get; set; }
        public short TeachModeID { get; set; }
        public int InsertResID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public List<BD_LCSubModeInfoM> LoginSubModeInfo { get; set; }

        public BD_LCTeachModeInfoM()
        {
            LoginSubModeInfo = new List<BD_LCSubModeInfoM>();
        }
    }

    public class BD_LCSubModeInfoM
    {
        public long LoginModeID { get; set; }
        public long LoginSubModeID { get; set; }
        public string SubModeCode { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }

    public class BD_ResourceInfoM
    {
        /// <summary>
        /// 插入资料时产生的ID（只有这个才能作为唯一标识）
        /// </summary>
        public int InsertResID { get; set; }
        public string ResourceID { get; set; }
        public string ResourceName { get; set; }
        /// <summary>
        /// 资料来源ID
        /// </summary>
        public short ResOriginTypeID { get; set; }
        public string FileExtension { get; set; }
        public string SpecialCode { get; set; }
        public string SpecialName { get; set; }
        public string ThemeCode { get; set; }
        public string ThemeName { get; set; }
        /// <summary>
        /// 从根节点到此叶节点的路径
        /// </summary>
        private string IDPath { get; set; }
    }

    public class BD_StudentInfoM
    {
        public string StudentID { get; set; }
        /// <summary>
        /// 出勤得分
        /// </summary>
        public float AttendanceScore { get; set; }       
    }
    public class BD_StuStudyInfoM
    {
        public string StudentID { get; set; }
        /// <summary>
        /// 出勤得分
        /// </summary>
        public float AttendanceScore { get; set; }
        /// <summary>
        /// 课堂手动加分
        /// </summary>
        public float KTHandAddScore { get; set; }
        /// <summary>
        /// 课堂测试加分
        /// </summary>
        public float KTTestAddScore { get; set; }
    }
    public class BD_TrainInfoM
    {
        //同时也是训练操作的ID
        public int TrainID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public List<BD_TrainQueInfoM> TrainQue { get; set; }
        public List<BD_TrainStuResultM> StuResult { get; set; }

        public BD_TrainInfoM()
        {
            TrainQue = new List<BD_TrainQueInfoM>();
            StuResult = new List<BD_TrainStuResultM>();
        }
    }
    //教师操作带来的学生结果xiezongwu20171124
    public class BD_StuResultByTeachInfoM
    {
        //同时也是训练操作的ID
        public long LoginTeachModOperID { get; set; }
        public string OperationCode { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string RelateResource { get; set; }

        public List<BD_StuOperResultByTeachM> StuResult { get; set; }

        public BD_StuResultByTeachInfoM()
        {
            StuResult = new List<BD_StuOperResultByTeachM>();
        }
    }

    //在教师控制下学生操作带来的结果信息 xiezongwu20171124
    public class BD_StuOperResultByTeachM
    {
        public int StuResultID { get; set; }

        public string StudentID { get; set;}

        public string StudentResultStr { get; set; }

        public float fstudentScore { get; set; }

    }
    public class BD_TrainQueInfoM
    {
        public string QueID { get; set; }
        /// <summary>
        /// 题目来源（Resource-资料原题，Edit-自编试题，Digital-数字化资源库）
        /// </summary>
        public string QueSource { get; set; }
    }

    /// <summary>
    /// 学生的结果
    /// </summary>
    public class BD_TrainStuResultM
    {
        public string StudentID { get; set; }
        /// <summary>
        /// 学生关于每道题的结果
        /// </summary>
        public List<BD_TrainQueStuResultM> EachQueResult { get; set; }

        public BD_TrainStuResultM()
        {
            EachQueResult = new List<BD_TrainQueStuResultM>();
        }
    }

    /// <summary>
    /// 学生关于每道题的结果
    /// </summary>
    public class BD_TrainQueStuResultM
    {
        public string QueID { get; set; }
        /// <summary>
        /// 学生得分
        /// </summary>
        public float StuScore { get; set; }
        /// <summary>
        /// 学生答对的知识点（多个值用“|”分隔）
        /// </summary>
        public string RightZSD { get; set; }
        /// <summary>
        /// 学生答错的知识点（多个值用“|”分隔）
        /// </summary>
        public string WrongZSD { get; set; }
        /// <summary>
        /// 学生回答该道题所用时间
        /// </summary>
        public float UseTime { get; set; }
    }

    public class BD_UserNoteInfoM
    {
        public long NoteID { get; set; }
        public string UserID { get; set; }
        public DateTime NoteTime { get; set; }
        public string NoteTypeCode { get; set; }
        //该行为发生时的试题或资料编号
        public string BehaviorSubject { get; set; }
    }

    public class BD_ImportantZSDM
    {
        public string StudentID { get; set; }
        public string TypeCode { get; set; }
        public DateTime StartTime { get; set; }
        public string BehaviorSubject { get; set; }
        public long LCSubModeID { get; set; }
    }


   //学生学习资料（认知大数据）
    public class ResourceStudyPerceiveM
    {
        public string EventID { get; set; }
        public string UserID { get; set; }
        public string ResourceID { get; set; }
        public string UseTime { get; set; }
        public string SubjectID { get; set; }
        public string ActID { get; set; }
        public string ActName { get; set; }
        public string SchoolID { get; set; }
        public string Term { get; set; }
        public string RzType { get; set; }
    }
    //学习成绩认知
    public class StudyResultPerceiveM
    {
        public string EventID { get; set; }
        public string XH { get; set; }
        public string SchoolID { get; set; }
        public string Term { get; set; }
        public string RzType { get; set; }
        public string ActID { get; set; }
        public string ActName { get; set; }
        public string PublishUserID { get; set; }
        public string PublishUserName { get; set; }
        public string SubjectID { get; set; }
        public string SysID { get; set; }
        public string FatherActID { get; set; }//本次上课id
        //public string Score { get; set; }
        //public string AddScore { get; set; }
        public string Attendance { get; set; }
        public string ReleaseTime { get; set; }
        public List<AddScoreM> ClassAddScores { get; set; }
        public List<AddScoreM> TestAddScores { get; set; }
    }

    public class AddScoreM {
        public float Score { get; set; }
        public string ScoreReason { get; set; } 
        public string CreatedTime{get;set;}
    }
}
