using System;
using System.Collections.Generic;

namespace LBD_WebApiInterface.ClassTeach
{
    //上一堂课基本的登录信息
    public class LastClassLoginInfoM
    {
        //教师ID
        public string TeacherID { get; set; }
        //课程ID
        public string CourseID { get; set; }
        //课程班ID
        public string CoursePlanIDs { get; set; }
        //课堂开始时间
        public DateTime ClassStartTime { get; set; }
        //课堂结束时间
        public DateTime ClassEndTime { get; set; }
        //登录的IP
        public string LoginIP { get; set; }
        //学年
        public string TermYear { get; set; }
        //教室ID
        public int ClassroomID { get; set; }
        //课堂编号
        public int LoginClassID { get; set; }
    }

    /// <summary>
    /// 上一堂课总体信息
    /// </summary>
    public class LastClassSumInfoM
    {
        /// <summary>
        /// 上一堂课编号
        /// </summary>
        public int LastLoginClassID { get; set; }
        /// <summary>
        /// 上一堂课最后进入的模块
        /// </summary>
        public int LastLoginModuleID { get; set; }
        /// <summary>
        /// 上一堂课最后进入的模块的ID
        /// </summary>
        public short LastModuleID { get; set; }
        /// <summary>
        /// 上一堂课最后进入的模块的名称
        /// </summary>
        public string LastModuleName { get; set; }
        /// <summary>
        /// 上一堂课最后进入的模块里选取的资料（包含每篇资料最后用的教学模式）
        /// </summary>
        public List<LCResourceTreeNodeM> LastClassResource { get; set; }
        /// <summary>
        /// 上一堂课最后使用的资料记录时产生的ID
        /// </summary>
        public int LastUsingInserResID { get; set; }
        //（由于不一定唯一，暂不使用）上一堂课最后使用的资料的ID
        private int LastUsingResourceID { get; set; }
    }

    //上一堂课使用的资料信息
    public class LastClassResourceInfoM
    {
        public int TrainResourceID { get; set; }
        public short OrigTypeID { get; set; }
        public string OrigTypeName { get; set; }
        public string ResourceID { get; set; }
        public string ResourceName { get; set; }
        public string ResourceContent { get; set; }
        public List<LastClassResourceFileInfoM> Files { get; set; }
    }

    //上一堂课使用的资料所包含的文件信息
    public class LastClassResourceFileInfoM
    {
        public long ID { get; set; }
        public int TrainResourceID { get; set; }
        public string FileID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileExtension { get; set; }
        //文件对应的教学模式
        public long LastLoginModeID { get; set; }
    }

    //上一堂课进入的教学模式信息
    public class LastClassModeInfoM
    {
        //该模式进入时产生的ID
        public long LoginModeID { get; set; }
        //教学模式的ID
        public short TeachModeID { get; set; }
        //教学模式的名称
        public string TeachModeName { get; set; }
        //最后一个子模式进入时产生的ID
        public long LoginSubModeID { get; set; }
        //最后一个子模式的代码
        public string SubModeCode { get; set; }
        //最后一个子模式内的操作
        public List<LCSubModeOperM> SubModeOper { get; set; }
        //教学模式的附属资料信息
        public List<AttachResourceM> AttchResource { get; set; }
    }

    /// <summary>
    /// 课堂信息
    /// </summary>
    public class LoginClassInfoM
    {
        /// <summary>
        /// 课堂编号
        /// </summary>
        public int LoginID { get; set; }
        /// <summary>
        /// 教师ID
        /// </summary>
        public string TeacherID { get; set; }
        /// <summary>
        /// 学科ID
        /// </summary>
        public byte SubjectID { get; set; }
        /// <summary>
        /// 课程ID
        /// </summary>
        public string CourseID { get; set; }
        /// <summary>
        /// 课程表对应的课程ID
        /// </summary>
        public string ScheduleCourseID { get; set;}
        /// <summary>
        /// 课程班ID
        /// </summary>
        public string CoursePlanIDs { get; set; }
        /// <summary>
        /// 教室ID
        /// </summary>
        public int ClassRoomID { get; set; }
        /// <summary>
        /// 班级总人数
        /// </summary>
        public int ClassStuCount { get; set; }
        /// <summary>
        /// 产品代码（网络化课堂教学系统=LBD；多媒体课堂教学系统=MMT）
        /// </summary>
        public string ProductCode { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime ClassStartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? ClassEndTime { get; set; }
        /// <summary>
        /// 登录IP
        /// </summary>
        public string LoginIP { get; set; }
        /// <summary>
        /// 学校ID
        /// </summary>
        public string SchoolID { get; set; }
        /// <summary>
        /// 学年学期
        /// </summary>
        public string TermYear { get; set; }
    }

    /// <summary>
    /// 进入的模块
    /// </summary>
    public class LoginClassModuleM
    {
        /// <summary>
        /// 进入模块产生的ID
        /// </summary>
        public int LoginModelID { get; set; }
        /// <summary>
        /// 模块ID
        /// </summary>
        public short ModelID { get; set; }
        /// <summary>
        /// 课堂编号
        /// </summary>
        public int LoginID { get; set; }
        /// <summary>
        /// 当前正在使用的模式的ID（动态ID）
        /// </summary>
        public long UsingLoginModeID { get; set; }
        public string UsingBookName { get; set; }
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
        /// 修改者
        /// </summary>
        public string lastEditor { get; set; }
    }

    /// <summary>
    /// 进入的教学模式
    /// </summary>
    public class LoginTeachModeM
    {
        /// <summary>
        /// 进入教学产生的ID
        /// </summary>
        public long lgnClassTeachModeID { get; set; }
        /// <summary>
        /// 教学模式ID
        /// </summary>
        public short TeachModeID { get; set; }
        /// <summary>
        /// 进入模块产生的ID
        /// </summary>
        public int LoginModelID { get; set; }
        /// <summary>
        /// 资源的ID
        /// </summary>
        public string ResourceID { get; set; }
        /// <summary>
        /// 资源下的文件的ID
        /// </summary>
        public int InsertResourceID { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 最后的修改者
        /// </summary>
        public string LastEditor { get; set; }
    }

    /// <summary>
    /// 教师操作（子模式之外的）
    /// </summary>
    public class LCModuleOperM
    {
        /// <summary>
        /// 插入操作记录产生的ID
        /// </summary>
        public int LoginModuleOperID { get; set; }
        /// <summary>
        /// 操作代码
        /// </summary>
        public string OperationCode { get; set; }
        /// <summary>
        /// 进入模块产生的ID
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
        /// 创建者
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
        /// 最后的修改者
        /// </summary>
        public string LastEditor { get; set; }
        /// <summary>
        /// 进入的教学模式ID
        /// </summary>
        public long LoginClassModeID { get; set; }
        /// <summary>
        /// 操作内容
        /// </summary>
        public string OperContent { get; set; }
        /// <summary>
        /// 操作的目标学生ID
        /// </summary>
        public string OperTarget { get; set; }
        /// <summary>
        /// 操作的类型
        /// </summary>
        public string OperationType { get; set; }
        /// <summary>
        /// 操作者（1-教师，2-学生）
        /// </summary>
        public byte Operator { get; set; }
    }

    /// <summary>
    /// 教师操作（子模式内的）
    /// </summary>
    public class LCSubModeOperM
    {
        public long ID { get; set; }
        public string OperationCode { get; set; }
        public long LCSubModeID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string OperContent { get; set; }
        public string OperTarget { get; set; }
        public string Creator { get; set; }
        public string LastEditor { get; set; }
    }

    /// <summary>
    /// 学生操作
    /// </summary>
    public class LCStudentOperM
    {
        /// <summary>
        /// 自增长ID
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 操作代码
        /// </summary>
        public string OperationCode { get; set; }
        /// <summary>
        /// 课堂编号
        /// </summary>
        public int LoginClassID { get; set; }
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
        /// 最后的修改者
        /// </summary>
        public string LastEditor { get; set; }
        /// <summary>
        /// 操作内容
        /// </summary>
        public string OperContent { get; set; }

    }
    /// <summary>
    /// 学生操作及操作的结果xiezongwu20171116
    /// </summary>
    public class LCStudentOperAndResultM
    {
        /// <summary>
        /// StudentID
        /// </summary>
        public string StudentID { get; set; }
        /// <summary>
        /// 操作代码
        /// </summary>
        public string OperationCode { get; set; }
        /// <summary>
        /// 操作内容结果详情
        /// </summary>
        public string OperResultDetail { get; set; }
        /// <summary>
        /// 操作内容结果值特指分值
        /// </summary>
        public float OperScore { get; set; }
        /// <summary>
        /// 课堂编号
        /// </summary>
        public int LoginClassID { get; set; }
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


    }
    public class LCTrainInfoM
    {
        public int LCTrainOperID { get; set; }
        public byte TrainSource { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public List<LCTrainQueInfoM> TrainQueInfo { get; set; }
        public string LastEditor { get; set; }
    }

    /// <summary>
    /// 训练的题目信息
    /// </summary>
    public class LCTrainQueInfoM
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
        /// 题目ID
        /// </summary>
        public string QueID { get; set; }
        /// <summary>
        /// 题目类型
        /// </summary>
        public string QueType { get; set; }
        /// <summary>
        /// 题目分值（若没有则为0）
        /// </summary>
        public double QueScore { get; set; }
        /// <summary>
        /// 题目来源
        /// </summary>
        public string QueSource { get; set; }
        public string SpecialCode { get; set; }
        public string SpecialName { get; set; }
        public string ThemeCode { get; set; }
        public string ThemeName { get; set; }
        /// <summary>
        /// 描述信息
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// 与学生相关的训练结果
    /// </summary>
    public class LCTrainStuResultM
    {
        /// <summary>
        /// 自增长ID
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 插入训练操作产生的ID
        /// </summary>
        public long LCOperationID { get; set; }
        /// <summary>
        /// 学生ID
        /// </summary>
        public string StudentID { get; set; }
        /// <summary>
        /// 答对的题目数量
        /// </summary>
        public int RightNum { get; set; }
        /// <summary>
        /// 答错的题目数量
        /// </summary>
        public int WrongNum { get; set; }
        /// <summary>
        /// 未答的题目数量
        /// </summary>
        public int NotAnsweredNum { get; set; }
        /// <summary>
        /// 得分
        /// </summary>
        public double Score { get; set; }
        /// <summary>
        /// 做对的知识点
        /// </summary>
        public string[] RightZSD { get; set; }
        /// <summary>
        /// 做错的知识点
        /// </summary>
        public string[] WrongZSD { get; set; }
        /// <summary>
        /// 每道题的得分（若没有得分，则为每道题的对错，0-错误，1-正确）
        /// </summary>
        public float[] EachQueResult { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        public string Creator { get; set; }
    }

    /// <summary>
    /// 训练题目相关的结果
    /// </summary>
    public class LCTrainQueResultM
    {
        /// <summary>
        /// 自增长ID
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 插入操作记录产生的ID
        /// </summary>
        public long LCOperationID { get; set; }
        /// <summary>
        /// 题目ID
        /// </summary>
        public string QueID { get; set; }
        /// <summary>
        /// 错误率
        /// </summary>
        public float WrongRate { get; set; }
        /// <summary>
        /// 每个选项的选择人数
        /// </summary>
        public int[] OptionChooseNum { get; set; }
        /// <summary>
        /// 最后的修改者
        /// </summary>
        public string LastEditor { get; set; }
    }

    /// <summary>
    /// 训练的总体结果
    /// </summary>
    public class LCTrainResultM
    {
        /// <summary>
        /// 自增长ID
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 插入操作产生的ID
        /// </summary>
        public long LCOperationID { get; set; }
        /// <summary>
        /// 最高分
        /// </summary>
        public float HighestScore { get; set; }
        /// <summary>
        /// 最低分
        /// </summary>
        public float LowestScore { get; set; }
        /// <summary>
        /// 平均分
        /// </summary>
        public float AveragedScore { get; set; }
        /// <summary>
        /// 有效学生总人数
        /// </summary>
        public int ValidStuCount { get; set; }
        /// <summary>
        /// 最后的修改者
        /// </summary>
        public string LastEditor { get; set; }
    }

    /// <summary>
    /// 课堂使用的资料
    /// </summary>
    public class LCResourceM
    {
        /// <summary>
        /// 记录资料时产生的自增长ID
        /// </summary>
        public int InsertResID { get; set; }
        /// <summary>
        /// 资料来源ID
        /// </summary>
        public short ResOriginTypeID { get; set; }
        /// <summary>
        /// 资料来源名称
        /// </summary>
        public string ResOriginTypeName { get; set; }
        /// <summary>
        /// 资料ID
        /// </summary>
        public string ResourceID { get; set; }
        /// <summary>
        /// 资料名称
        /// </summary>
        public string ResourceName { get; set; }
        /// <summary>
        /// 文件后缀
        /// </summary>
        public string FileExtension { get; set; }
        /// <summary>
        /// 资料专题编号
        /// </summary>
        public string SpecialCode { get; set; }
        /// <summary>
        /// 资料专题名称
        /// </summary>
        public string SpecialName { get; set; }
        /// <summary>
        /// 资料主题编号
        /// </summary>
        public string ThemeCode { get; set; }
        /// <summary>
        /// 资料主题名称
        /// </summary>
        public string ThemeName { get; set; }
        /// <summary>
        /// 选资料操作产生的ID
        /// </summary>
        public long LCModuleOperID { get; set; }
        /// <summary>
        /// 资料内容或资料的存储路径
        /// </summary>
        public string ResourceContent { get; set; }
        /// <summary>
        /// 记录父资料时产生的自增长ID
        /// </summary>
        public int ParentInsertResID { get; set; }
        /// <summary>
        /// 父节点的资料ID（若没有，则为空）
        /// </summary>
        public string ParentResourceID { get; set; }
        /// <summary>
        /// 修改者
        /// </summary>
        public string LastEditor { get; set; }
        /// <summary>
        /// 该资料最后进入教学模式产生的ID（继续上一堂课时，此值若为0，表示该资料从未打开过教学模式）
        /// </summary>
        public long LoginTeachModeID { get; set; }
        /// <summary>
        /// 该资料最后进入的教学模式ID
        /// </summary>
        public short TeachModeID { get; set; }
        /// <summary>
        /// 该资料最后进入的教学模式的名称
        /// </summary>
        public string TeachModeName { get; set; }
    }

    /// <summary>
    /// 构造资料树形结构的节点（把资料看做树形结构，例如从教案到底层的单个文件）
    /// </summary>
    public class LCResourceTreeNodeM
    {
        /// <summary>
        /// 数据
        /// </summary>
        public LCResourceM Data { get; set; }
        //（删除） 节点类型（1-根节点，2-中间节点，3-叶节点）
        private int NodeType { get; set; }
        /// <summary>
        /// 子节点
        /// </summary>
        public List<LCResourceTreeNodeM> ChildNodes { get; set; }

        public LCResourceTreeNodeM()
        {
            ChildNodes = new List<LCResourceTreeNodeM>();
        }
    }

    /// <summary>
    /// 构造资料数组型元素
    /// </summary>
    public class LCResourceElementM
    {
        /// <summary>
        /// 数据
        /// </summary>
        public LCResourceM Data { get; set; }
        /// <summary>
        /// 父级元素索引
        /// </summary>
        public int ParentIndex { get; set; }
        /// <summary>
        /// 子节点在链表中的Index
        /// </summary>
        public List<int> ChildElementIndex { get; set; }

        public LCResourceElementM()
        {
            ChildElementIndex = new List<int>();
        }
    }

    public class LCTrainResourceFileM
    {
        public int InsertFileID { get; set; }
        public int InsertResID { get; set; }
        public string FileID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileExtension { get; set; }
        public string IDPath { get; set; }
        public string NamePath { get; set; }
        public string LastEditor { get; set; }
    }

    /// <summary>
    /// 用户笔记
    /// </summary>
    public class LCUserNoteM
    {
        /// <summary>
        /// 笔记ID
        /// </summary>
        public long NoteID { get; set; }
        /// <summary>
        /// 笔记名称
        /// </summary>
        public string NoteName { get; set; }
        /// <summary>
        /// 笔记内容路径
        /// </summary>
        public string NoteContentPath { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 课堂编号
        /// </summary>
        public long LoginID { get; set; }
        /// <summary>
        /// 学科ID
        /// </summary>
        public byte SubjectID { get; set; }
        /// <summary>
        /// 课程班ID
        /// </summary>
        public string CoursePlanID { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
        /// 附件名
        /// </summary>
        public string AttachName { get; set; }
        /// <summary>
        /// 附件路径
        /// </summary>
        public string AttachPath { get; set; }
    }
}
