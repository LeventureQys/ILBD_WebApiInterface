using System;

namespace LBD_WebApiInterface.ClassTeach
{
    public class LastClassInfoV51M
    {
        public int LastLoginID { get; set; }
        public string LastClassModelName { get; set; }
        public int LastClassModelID { get; set; }
        public string LastUseingTeachModeName { get; set; }
        public int LastUseingTeachModeID { get; set; }
        public string LastUseingNetCourseWareID { get; set; }
        public string LastUseingResourceID { get; set; }
        public string[] LastNetCoursewareID { get; set; }
        public string[] LastNetCoursewareName { get; set; }
        public string[] OrigResourceID { get; set; }
        public string[] ResourceContent { get; set; }
        public int[] OrigTypeID { get; set; }
        public string[] OriTypeName { get; set; }
    }

    public class LastLoginInfoV51M
    {
        public string TeacherID { get; set; }
        public string CourseID { get; set; }
        public string CoursePlanIDs { get; set; }
        public DateTime ClassStartTime { get; set; }
        public DateTime ClassEndTime { get; set; }
        public string LoginIP { get; set; }
        public string TermYear { get; set; }
        public int ClassroomID { get; set; }
        public int LoginClassID { get; set; }
    }

    /// <summary>
    /// 所有操作
    /// </summary>
    public class AllOperationM
    {
        /// <summary>
        /// 自增长ID
        /// </summary>
        public short ID { get; set; }

        /// <summary>
        /// 操作代码（唯一）
        /// </summary>
        public string OperationCode { get; set; }

        /// <summary>
        /// 操作说明
        /// </summary>
        public string OperationDesciption { get; set; }

        /// <summary>
        ///
        /// </summary>
        public short OperationParent { get; set; }

        /// <summary>
        ///
        /// </summary>
        public byte OperationType { get; set; }

        /// <summary>
        ///
        /// </summary>
        public byte Operator { get; set; }
    }

    /// <summary>
    /// 附加资料
    /// </summary>
    public class AttachResourceM
    {
        /// <summary>
        /// 自增长ID
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 进入教学模式的ID
        /// </summary>
        public long LCModeID { get; set; }

        /// <summary>
        /// 关联的主资料ID
        /// </summary>
        public string MainResourceID { get; set; }

        /// <summary>
        /// 附属资料类型
        /// </summary>
        public byte AttachResourceType { get; set; }

        /// <summary>
        /// 附属资料内容或附属资料路径
        /// </summary>
        public string AttachResourceContent { get; set; }

        /// <summary>
        /// 附属资料存储位置（1-存储在数据库，2-存储在FTP，此时AttachResourceContent保存的就是FTP路径）
        /// </summary>
        public byte StoreLocationType { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 预留字段
        /// </summary>
        public string ReserveField { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }
    }

    /// <summary>
    /// 学生每一堂课的学习信息（出勤、课堂加分、测试加分、聊天测试等）信息
    /// </summary>
    public class ClassStuStudyInfoM
    {
        /// <summary>
        /// 学生ID
        /// </summary>
        public string StudentID { get; set; }

        /// <summary>
        /// 出勤得分
        /// </summary>
        public float AttendScore { get; set; }

        /// <summary>
        /// 课堂手动加分
        /// </summary>
        public float HandAddScore { get; set; }

        /// <summary>
        /// 课堂测试加分
        /// </summary>
        public float TestAddScore { get; set; }

        /// <summary>
        /// 课堂发言次数
        /// </summary>
        public int SpeakTimes { get; set; }
    }

    /// <summary>
    /// 学生出勤信息
    /// </summary>
    public class AttendanceDetailM
    {
        /// <summary>
        /// 自增长ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 学生ID
        /// </summary>
        public string StudentID { get; set; }

        /// <summary>
        /// 座位ID
        /// </summary>
        public string SeatID { get; set; }

        /// <summary>
        /// 课堂编号
        /// </summary>
        public int LoginID { get; set; }

        /// <summary>
        /// 出勤得分
        /// </summary>
        public float Score { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 创建时间，即签到时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastEditTime { get; set; }

        /// <summary>
        /// 最后修改者
        /// </summary>
        public string LastEditor { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }

    /// <summary>
    /// 模块
    /// </summary>
    public class TeachModuleM
    {
        /// <summary>
        /// 模块ID
        /// </summary>
        public short ModelID { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// 支持的学校类型
        /// </summary>
        public string SchoolTypeName { get; set; }

        /// <summary>
        /// 所属学科ID
        /// </summary>
        public byte SubjectID { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// 教学模式
    /// </summary>
    public class TeachModeM
    {
        /// <summary>
        /// 教学模式ID
        /// </summary>
        public short TeachModeID { get; set; }

        /// <summary>
        /// 教学模式名称
        /// </summary>
        public string ModeName { get; set; }

        /// <summary>
        /// 所支持的学校类型
        /// </summary>
        public string SchoolTypeName { get; set; }

        /// <summary>
        /// 教学模式英文ID
        /// </summary>
        public string EnglishID { get; set; }

        /// <summary>
        /// 教学模式描述信息
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 所属学科ID
        /// </summary>
        public byte SubjectID { get; set; }
    }

    /// <summary>
    /// 教学模式子模式
    /// </summary>
    public class SubModeM
    {
        /// <summary>
        /// 自增长ID
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 子模式代码
        /// </summary>
        public string SubModeCode { get; set; }

        /// <summary>
        /// 子模式名称
        /// </summary>
        public string SubModeName { get; set; }

        /// <summary>
        /// 子模式所属学科ID
        /// </summary>
        public byte SubjectID { get; set; }

        /// <summary>
        /// 子模式描述信息
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// 设置信息
    /// </summary>
    public class LCSetInfoM
    {
        /// <summary>
        /// 自增长ID
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 课堂编号
        /// </summary>
        public int LoginClassID { get; set; }

        /// <summary>
        /// 进入模块产生的ID
        /// </summary>
        public int LCModuleID { get; set; }

        /// <summary>
        /// 进入教学模式产生的ID
        /// </summary>
        public int LCModeID { get; set; }

        /// <summary>
        /// 设置项的代码（最大长度为10，仅英文）
        /// </summary>
        public string SetItemCode { get; set; }

        /// <summary>
        /// 设置项的值（最大长度为100，可中文）
        /// </summary>
        public string SetItemValue { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后的修改者
        /// </summary>
        public string LastEditor { get; set; }

        /// <summary>
        /// 最后的修改时间
        /// </summary>
        public DateTime LastEditTime { get; set; }
    }

    /// <summary>
    /// 进入的子模式
    /// </summary>
    public class LCSubModeM
    {
        /// <summary>
        /// 自增长ID
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 子模式代码
        /// </summary>
        public string SubModeCode { get; set; }

        /// <summary>
        /// 进入模式产生的ID
        /// </summary>
        public long LCModeID { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后的修改者
        /// </summary>
        public string LastEditor { get; set; }

        /// <summary>
        /// 最后的修改时间
        /// </summary>
        public DateTime LastEditTime { get; set; }
    }

    /// <summary>
    /// 操作的结果
    /// </summary>
    public class LCOperResultM
    {
        /// <summary>
        /// 自增长ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 插入操作记录产生的ID
        /// </summary>
        public int LoginClassModelOperationID { get; set; }

        /// <summary>
        /// 学生ID
        /// </summary>
        public string StudentID { get; set; }

        /// <summary>
        /// 操作结果
        /// </summary>
        public string ResultDetail { get; set; }

        /// <summary>
        /// 学生分数
        /// </summary>
        public float Score { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 最后的修改者
        /// </summary>
        public string LastEditor { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }

    /// <summary>
    /// 训练时学生的作答结果
    /// </summary>
    public class LCTrainStuAnswerM
    {
        /// <summary>
        /// 自增长ID
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 插入操作产生的ID
        /// </summary>
        public int LCOperationID { get; set; }

        /// <summary>
        /// 学生ID
        /// </summary>
        public string StudentID { get; set; }

        /// <summary>
        /// 学生答案
        /// </summary>
        public string StuAnswer { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string Creator { get; set; }
    }

    /// <summary>
    /// 学生在课堂上的加分信息
    /// </summary>
    public class LCStudentScoreM
    {
        /// <summary>
        /// 自增长ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 学生ID
        /// </summary>
        public string StudentID { get; set; }

        /// <summary>
        /// 插入操作产生的ID
        /// </summary>
        public int LoginClassModelOperationID { get; set; }

        /// <summary>
        /// 课堂编号
        /// </summary>
        public int LoginID { get; set; }

        /// <summary>
        /// 操作代码
        /// </summary>
        public string OperationCode { get; set; }

        /// <summary>
        /// 得分
        /// </summary>
        public float Score { get; set; }

        /// <summary>
        /// 加分原因
        /// </summary>
        public string ScoreReason { get; set; }

        /// <summary>
        /// 学科ID
        /// </summary>
        public byte SubjectID { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        public string LastEditor { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }

    /// <summary>
    /// 教师操作（子模式之外的）
    /// </summary>
    public class LoginClassModOperM
    {
        /// <summary>
        /// 插入操作记录产生ID
        /// </summary>
        public long Jx_LoginClassModelOperationID { get; set; }

        /// <summary>
        /// 操作ID
        /// </summary>
        public short OperationID { get; set; }

        /// <summary>
        /// 操作代码
        /// </summary>
        public string OperationCode { get; set; }

        /// <summary>
        /// 进入的模块ID
        /// </summary>
        public int LoginModelID { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 进入的教学模式ID
        /// </summary>
        public long LoginClassModeID { get; set; }
    }

    //访问自己的WS得到的（区别于云平台）
    public class OtherSysInfoM
    {
        public string SysID { get; set; }
        public string SysWSAddr { get; set; }
        public string SysWebAddr { get; set; }
    }
}