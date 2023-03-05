using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LBD_WebApiInterface.Models
{
    public class NetCoursewareWithCountM
    {
        public long SumCount { get; set; }
        public List<NetCoursewareM> NetCourseware { get; set; }

        public NetCoursewareWithCountM()
        {
            NetCourseware = new List<NetCoursewareM>();
        }
    }

    // 与数据库保持一致
    /// <summary>
    /// 网络化课件
    /// </summary>
    public class NetCoursewareM
    {
        /// <summary>
        /// 课件ID
        /// </summary>
        public string CoursewareID { get; set; }
        /// <summary>
        /// 学科ID
        /// </summary>
        public byte SubjectID { get; set; }
        /// <summary>
        /// 创建者ID
        /// </summary>
        public string CreatorID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 课件名称
        /// </summary>
        public string CoursewareName { get; set; }
        /// <summary>
        /// 课件路径
        /// </summary>
        public string CoursewarePath { get; set; }
        /// <summary>
        /// 课件缩略图路径
        /// </summary>
        public string CoursewarePicPath { get; set; }
        /// <summary>
        /// 课件适用年级ID
        /// </summary>
        public short LevelID { get; set; }
        /// <summary>
        /// 课件上课时长，单位秒
        /// </summary>
        public int CoursewareTime { get; set; }//单位：秒
        /// <summary>
        /// 课件授课状态
        /// </summary>
        public bool CoursewareStatus { get; set; }
        /// <summary>
        /// 是否共享
        /// </summary>
        public bool IsShare{get;set;}
        /// <summary>
        /// 修改者
        /// </summary>
        public string LastEditor { get; set; }
        public DateTime LastEditTime { get; set; }
        /// <summary>
        /// KWJJ代表课文讲解，ZNDJJ代表重难点讲解
        /// </summary>
        public string CoursewareType { get; set; }
        /// <summary>
        /// 使用次数
        /// </summary>
        public int UseTimes { get; set; }
    }

    //与数据库保持一致
    /// <summary>
    /// 网络化课件所需资料
    /// </summary>
    public class NCWResourceM
    {
        /// <summary>
        /// ID（自增长）
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 课件ID
        /// </summary>
        public string CoursewareID { get; set; }
        /// <summary>
        /// 资源ID
        /// </summary>
        public string ResourceID { get; set; }
        /// <summary>
        /// 资料来源标识
        /// </summary>
        public short TableFlag { get; set; }
        /// <summary>
        /// 教学模式ID
        /// </summary>
        public short TeachModeID { get; set; }
        /// <summary>
        /// 资料上课时长，单位秒
        /// </summary>
        public int TeachDurationTime { get; set; }
        /// <summary>
        /// 资料信息记录文件的路径
        /// </summary>
        public string SimulationInfoPath { get; set; }
        /// <summary>
        /// 资料排列顺序
        /// </summary>
        public byte OrderNo { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        public string CreatorID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改者
        /// </summary>
        public string LastEditor { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime LastEditTime { get; set; }
        //public bool DataFlag { get; set; }
        //public string Remark { get; set; }
    }

    //与数据库保持一致
    /// <summary>
    /// 网络化课件辅助资源库
    /// </summary>
    public class NCWAssistResourceM
    {
        /// <summary>
        /// ID（自增长）
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 资源ID
        /// </summary>
        public string ResourceID { get; set; }
        /// <summary>
        /// 资源原始ID
        /// </summary>
        public string OriResourceID { get; set; }
        /// <summary>
        /// 资源原始来源标识
        /// </summary>
        public short OriTableFlag { get; set; }
        /// <summary>
        /// 资源名称
        /// </summary>
        public string ResourceName { get; set; }
        /// <summary>
        /// 资源路径
        /// </summary>
        public string FtpPath { get; set; }
        /// <summary>
        /// 资源缩略图路径
        /// </summary>
        public string PhotoPath { get; set; }
        /// <summary>
        /// 资源扩展名
        /// </summary>
        public string ExtensionName { get; set; }
        /// <summary>
        /// 资源使用次数
        /// </summary>
        public int UsedCount { get; set; }
        /// <summary>
        /// 创建者ID
        /// </summary>
        public string CreatorID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改者
        /// </summary>
        public string LastEditor { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime LastEditTime { get; set; }
        //public bool DataFlag { get; set; }
        //public string Remark { get; set; }
    }

    //与数据库保持一致
    /// <summary>
    /// 网络化课件对应的教学计划
    /// </summary>
    public class NCWTeachPlanM
    {
        /// <summary>
        /// ID（自增长）
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 课件ID
        /// </summary>
        public string CoursewareID { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        public string CreatorID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 授课时间
        /// </summary>
        public DateTime TeachTime { get; set; }
        /// <summary>
        /// 授课教室
        /// </summary>
        public string Classroom { get; set; }//实际的教室
        /// <summary>
        /// 授课班级/对象
        /// </summary>
        public string TeachClass { get; set; }//课程班
        //public byte PlanStatus { get; set; }
        /// <summary>
        /// 修改者
        /// </summary>
        public string LastEditor { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime LastEditTime { get; set; }
        //public bool DataFlag { get; set; }
        //public string Remark { get; set; }
    }

    public class CurrentTeachPlanM
    {
        public int ID { get; set; }
        /// <summary>
        /// 学科ID
        /// </summary>
        public byte SubjectID { get; set; }
        public string TeachPlanName { get; set; }
        /// <summary>
        /// 课件ID
        /// </summary>
        public string CoursewareID { get; set; }
        public string CoursewareName { get; set; }
        /// <summary>
        /// 课件路径
        /// </summary>
        public string CoursewarePath { get; set; }
        /// <summary>
        /// 课件上课时长，单位秒
        /// </summary>
        public int CoursewareTime { get; set; }//单位：秒
        /// <summary>
        /// 创建者
        /// </summary>
        public string CreatorID { get; set; }
        /// <summary>
        /// 授课时间
        /// </summary>
        public DateTime TeachTime { get; set; }
        /// <summary>
        /// 授课教室
        /// </summary>
        public string Classroom { get; set; }//实际的教室
        /// <summary>
        /// 授课班级/对象
        /// </summary>
        public string TeachClass { get; set; }//课程班
    }

    // 根据业务逻辑衍生
    /// <summary>
    /// 网络化课件所需资源（因NCWResourceM不满足需求所以衍生出此实体）
    /// </summary>
    public class ResourceM
    {
        public int ID { get; set; }
        public string CoursewareID { get; set; }
        public string ResourceID { get; set; }
        public short TableFlag { get; set; }
        public string ResourceName { get; set; }
        public short TeachModeID { get; set; }
        public int TeachDurationTime { get; set; }
        public string SimulationInfoPath { get; set; }
        public byte OrderNo { get; set; }
        /// <summary>
        /// 资源路径
        /// </summary>
        public string FtpPath { get; set; }
        /// <summary>
        /// 资源缩略图路径
        /// </summary>
        public string PhotoPath { get; set; }
        /// <summary>
        /// 资源文件扩展名
        /// </summary>
        public string ExtensionName { get; set; }
        public string CreatorID { get; set; }
        public DateTime CreateTime { get; set; }
        public string LastEditor { get; set; }
        public DateTime LastEditTime { get; set; }
    }

    // 根据业务逻辑衍生
    /// <summary>
    /// 网络化课件详细信息
    /// </summary>
    public class NetCoursewareDetailM
    {
        /// <summary>
        /// 课件信息
        /// </summary>
        public NetCoursewareM NetCourseware { get; set; }
        /// <summary>
        /// 教学计划（多个）
        /// </summary>
        public List<NCWTeachPlanM> NCWTeachPlan { get; set; }
        /// <summary>
        /// 所需资源（多个）
        /// </summary>
        public List<ResourceM> Resource { get; set; }
    }

    public class SearchConditionM
    {
        public List<string> TeachClass;
        public List<string> Classroom;
        public List<string> TeachTime;
    }
}
