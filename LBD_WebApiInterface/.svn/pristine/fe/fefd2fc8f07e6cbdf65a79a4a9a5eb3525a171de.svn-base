using System;
using System.Collections.Generic;
namespace LBD_WebApiInterface.Models.TeachCenter
{
    /// <summary>
    /// 课前预习
    /// </summary>
    public class PreviewM
    {
        /// <summary>
        /// 课前预习ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 课前预习名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 发布状态（0-未发布，1-已发布但未进行，2-已发布且进行中，3-已结束）
        /// </summary>
        public byte Status { get; set; }
        /// <summary>
        /// 学校类型
        /// <para>1-大学</para>
        /// <para>2-中小学</para>
        /// </summary>
        public byte SchoolType { get; set; }
        /// <summary>
        /// 适用年级
        /// </summary>
        public string LevelCode { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        //慕课、预习作业
        public string MuKeID { get; set; }
        public string MuKeName { get; set; }
        /// <summary>
        ///1-慕课（人工）； 2-慕课（智能）；3-笔译作业（人工）；4-笔译作业（智能）；
        /// 5-综合作业（人工）；6-综合作业（智能）；7-口语作业（人工）；8-口语作业（智能）；
        /// 9-写作作业（人工）；10-写作作业（智能）；11-听读作业（人工）；12-听读作业（智能）
        /// </summary>
        public byte MuKeType { get; set; }
        public string ZuoYeID { get; set; }
        public string ZuoYeName { get; set; }
        /// <summary>
        ///1-慕课（人工）； 2-慕课（智能）；3-笔译作业（人工）；4-笔译作业（智能）；
        /// 5-综合作业（人工）；6-综合作业（智能）；7-口语作业（人工）；8-口语作业（智能）；
        /// 9-写作作业（人工）；10-写作作业（智能）；11-听读作业（人工）；12-听读作业（智能）
        /// </summary>
        public byte ZuoYeType { get; set; }
        public string[] CourseClassID { get; set; }
        public string[] CourseClassName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Progress { get; set; }
    }

    /// <summary>
    /// 课堂教案
    /// </summary>
    public class LessonPlanM
    {
        /// <summary>
        /// 课堂教案ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 课堂教案名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 授课状态（1-已授课，0-未授课）
        /// </summary>
        public byte Status { get; set; }
        /// <summary>
        /// 学校类型
        /// <para>1-大学</para>
        /// <para>2-中小学</para>
        /// </summary>
        public byte SchoolType { get; set; }
        /// <summary>
        /// 适用年级
        /// </summary>
        public string LevelCode { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        // 课文讲解、重难点讲解、随堂测试
        public string KeWenJiangJieID { get; set; }
        public string KeWenJiangJieName { get; set; }
        public string ZhongNanDianID { get; set; }
        public string ZhongNanDianName { get; set; }
        public string SuiTangCeShiID { get; set; }
        public string SuiTangCeShiName { get; set; }
        public string[] CourseClassID { get; set; }
        public string[] CourseClassName { get; set; }
        public DateTime TeachTime { get; set; }
        public string Classroom { get; set; }
        public string ClassroomName { get; set; }
    }

    /// <summary>
    /// 课后练习
    /// </summary>
    public class PracticeM
    {
        /// <summary>
        /// 课前预习ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 课前预习名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 发布状态（0-未发布，1-已发布但未进行，2-已发布且进行中，3-已结束）
        /// </summary>
        public byte Status { get; set; }
        /// <summary>
        /// 学校类型
        /// <para>1-大学</para>
        /// <para>2-中小学</para>
        /// </summary>
        public byte SchoolType { get; set; }
        /// <summary>
        /// 适用年级
        /// </summary>
        public string LevelCode { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        //慕课、预习作业
        public string KeHouZuoYeID { get; set; }
        public string KeHouZuoYeName { get; set; }
        /// <summary>
        /// 1-综合测试（人工）； 2-综合测试（智能）； 3-口语测试（人工）； 4-口语测试（智能）；
        /// 5-笔译作业（人工）；6-笔译作业（智能）； 7-综合作业（人工）；8-综合作业（智能）；
        /// 9-口语作业（人工）；10-口语作业（智能）；11-写作作业（人工）；12-写作作业（智能）；
        /// 13-听读作业（人工）；14-听读作业（智能）
        /// </summary>
        public byte KeHouZuoYeType { get; set; }
        public string KeHouCeShiID { get; set; }
        public string KeHouCeShiName { get; set; }
        /// <summary>
        /// 1-综合测试（人工）； 2-综合测试（智能）； 3-口语测试（人工）； 4-口语测试（智能）；
        /// 5-笔译作业（人工）；6-笔译作业（智能）； 7-综合作业（人工）；8-综合作业（智能）；
        /// 9-口语作业（人工）；10-口语作业（智能）；11-写作作业（人工）；12-写作作业（智能）；
        /// 13-听读作业（人工）；14-听读作业（智能）
        /// </summary>
        public byte KeHouCeShiType { get; set; }
        public string[] CourseClassID { get; set; }
        public string[] CourseClassName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Progress { get; set; }
    }

    public class Get_LessonPlanM
    {
        /// <summary>
        /// 课堂教案ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 课堂教案名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 授课状态（1-已授课，0-未授课）
        /// </summary>
        public byte Status { get; set; }
        /// <summary>
        /// 学校类型
        /// <para>1-大学</para>
        /// <para>2-中小学</para>
        /// </summary>
        public byte SchoolType { get; set; }
        /// <summary>
        /// 适用年级
        /// </summary>
        public string LevelCode { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 教案下的课件
        /// </summary>
        public List<Get_LPCoursewareM> LPCourseware { get; set; }
        /// <summary>
        /// 教案的教学计划
        /// </summary>
        public List<Get_LPTaskM> LPTask { get; set; }

        public Get_LessonPlanM()
        {
            LPCourseware=new List<Get_LPCoursewareM>();
            LPTask=new List<Get_LPTaskM>();
        }
    }

    public class Get_LPCoursewareM
    {
        /// <summary>
        /// 课件的ID
        /// </summary>
        public string CoursewareID { get; set; }
        /// <summary>
        /// 课件的Name
        /// </summary>
        public string CoursewareName { get; set; }
        /// <summary>
        /// 1-课文讲解（人工）；2-课文讲解（智能）； 3-重难点（人工）；4-重难点（智能）；
        /// 5-综合试卷（人工）； 6-综合试卷（智能）；7-口语试卷（人工）；8-口语试卷（智能）
        /// </summary>
        public byte CoursewareType { get; set; }
        /// <summary>
        /// 课件图片
        /// </summary>
        public string CoursewarePicPath { get; set; }
        /// <summary>
        /// 课件FTP路径
        /// </summary>
        public string FtpPath { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastEditTime { get; set; }
        /// <summary>
        /// 引用次数
        /// </summary>
        public int UseTimes { get; set; }
        /// <summary>
        /// 共享状态（0-不共享，1-无密码共享，2-有密码共享）
        /// </summary>
        public byte ShareStatus { get; set; }
        /// <summary>
        /// 课件的设置信息
        /// </summary>
        public LP_CoursewareSetInfoM CoursewareSetInfo { get; set; }
    }

    /// <summary>
    /// 课堂教案下的课件设置信息
    /// </summary>
    public class LP_CoursewareSetInfoM
    {
        /// <summary>
        /// 互评设置（1-一对一 ；2-一对二；3-一对三）
        /// </summary>
        public int HuPing { get; set; }
        /// <summary>
        /// 教学时长（单位：秒）
        /// </summary>
        public int TeachDuration { get; set; }
        /// <summary>
        /// 大小题数（[大题数];[小题数]）
        /// <para> 示例： 1;5  表示1个大题5个小题</para>
        /// </summary>
        public string DaXiaoTi { get; set; }
    }

    public class Get_LPTaskM
    {
        /// <summary>
        /// 任务ID
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 授课对象（即课程班，可多值）
        /// </summary>
        public string[] CourseClassID { get; set; }
        /// <summary>
        /// 授课对象名称（即课程班名称，可多值）
        /// </summary>
        public string[] CourseClassName { get; set; }
        /// <summary>
        /// 授课时间
        /// </summary>
        public DateTime TeachTime { get; set; }
        /// <summary>
        /// 教室ID
        /// </summary>
        public int ClassroomID { get; set; }
        /// <summary>
        /// 授课教室
        /// </summary>
        public string Classroom { get; set; }
    }

    //教案制作使用
    /// <summary>
    /// 保存教案
    /// </summary>
    public class Save_LessonPlanM
    {
        /// <summary>
        /// 课堂教案ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 课堂教案名称
        /// </summary>
        public string LessonPlanName { get; set; }
        /// <summary>
        /// 课堂教案缩略图路径（暂无）
        /// </summary>
        public string PicPath { get; set; }
        /// <summary>
        /// 课堂教案共享状态（0-不共享，1-无密码共享，2-有密码共享）
        /// </summary>
        public byte ShareStatus { get; set; }
        /// <summary>
        /// 共享密码
        /// </summary>
        public string SharePwd { get; set; }
        /// <summary>
        /// 授课状态（0-无教学计划，1-有教学计划但未执行，2-有教学计划且执行中，2-已结束）
        /// </summary>
        public byte Status { get; set; }
        /// <summary>
        /// 学科ID（同云平台学科ID）
        /// </summary>
        public string SubjectID { get; set; }
        /// <summary>
        /// 学校类型
        /// <para>1-大学</para>
        /// <para>2-中小学</para>
        /// </summary>
        public byte SchoolType { get; set; }
        /// <summary>
        /// 年级ID
        /// </summary>
        public string LevelCode { get; set; }
        /// <summary>
        /// 学期
        /// </summary>
        public string Term { get; set; }
        /// <summary>
        /// 作者（即教师ID）
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 用户自定义目录的叶子节点ID（为0则表示没有自定义目录）
        /// </summary>
        public long FolderID { get; set; }
        /// <summary>
        /// 课文讲解、重难点讲解、随堂测试
        /// </summary>
        public List<Save_LPCoursewareM> LPCourseware { get; set; }
        /// <summary>
        /// 课堂教案的教学计划（未制定计划时值为null）
        /// </summary>
        public List<Save_LPTaskM> LPTask { get; set; }

        public Save_LessonPlanM()
        {
            LPTask = new List<Save_LPTaskM>();
        }
    }

    //教案制作使用
    /// <summary>
    /// 课堂教案包含的课件
    /// </summary>
    public class Save_LPCoursewareM
    {
        /// <summary>
        /// 课件的ID
        /// </summary>
        public string CoursewareID { get; set; }
        /// <summary>
        /// 课件的Name
        /// </summary>
        public string CoursewareName { get; set; }
        /// <summary>
        /// 1-课文讲解（人工）；2-课文讲解（智能）； 3-重难点（人工）；4-重难点（智能）；
        /// 5-综合试卷（人工）； 5-综合试卷（智能）；7-口语试卷（人工）；8-口语试卷（智能）
        /// </summary>
        public byte CoursewareType { get; set; }
        /// <summary>
        /// 课件状态（0-无教学计划，1-有教学计划但未执行，2-有教学计划且执行中，3-已结束）
        /// </summary>
        public byte CoursewareStatus { get; set; }
        /// <summary>
        /// 课件图片路径
        /// </summary>
        public string CoursewarePicPath { get; set; }
        /// <summary>
        /// 课件的设置信息
        /// </summary>
        public LP_CoursewareSetInfoM CoursewareSetInfo { get; set; }
    }

    //教案制作使用
    /// <summary>
    /// 课堂教案的教学计划
    /// </summary>
    public class Save_LPTaskM
    {
        /// <summary>
        /// 授课对象（即课程班，可多值）
        /// </summary>
        public string[] TaskObject { get; set; }
        /// <summary>
        /// 授课对象名称（即课程班名称，可多值）
        /// </summary>
        public string[] TaskObjectName { get; set; }
        /// <summary>
        /// 授课时间
        /// </summary>
        public DateTime TeachTime { get; set; }
        /// <summary>
        /// 教室ID
        /// </summary>
        public int ClassroomID { get; set; }
        /// <summary>
        /// 授课教室
        /// </summary>
        public string Classroom { get; set; }
    }

    public class IntelCoursewareSysConfig
    {
        public string FtpPath { get; set; }
        public string FtpUserName { get; set; }
        public string FtpUserPwd { get; set; }
        public string HttpPath { get; set; }
    }
}
