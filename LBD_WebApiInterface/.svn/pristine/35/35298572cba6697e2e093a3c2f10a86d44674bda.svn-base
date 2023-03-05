using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LBD_WebApiInterface.Models
{
    //与数据库保持一致
    /// <summary>
    /// 设置项的值
    /// </summary>
    public class TeacherSetInfoM
    {
        /// <summary>
        /// ID（自增长）
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 设置项ID
        /// </summary>
        public short SetItemID { get; set; }
        /// <summary>
        /// 设置内容
        /// </summary>
        public string SetItemValue { get; set; }
        /// <summary>
        /// 学科ID
        /// </summary>
        public byte SubjectID { get; set; }
        /// <summary>
        /// 教师ID
        /// </summary>
        public string TeachID { get; set; }
        /// <summary>
        /// 课程班ID
        /// </summary>
        public string CoursePlanID { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        public string Creator { get; set; }
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
        /// <summary>
        /// 课室号
        /// </summary>
        public short ClassIndex { get; set; }
    }

    //与数据库保持一致
    /// <summary>
    /// 设置项
    /// </summary>
    public class TeachSetItemM
    {
        /// <summary>
        /// 设置项ID
        /// </summary>
        public short SetItemID { get; set; }
        /// <summary>
        /// 设置项名称
        /// </summary>
        public string SetItemName { get; set; }
        /// <summary>
        /// 设置项描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 用途
        /// </summary>
        public string Purpose { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        public string Creator { get; set; }
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

    //根据业务逻辑衍生
    /// <summary>
    /// 数字化资源库（目前使用电子资源阅览室）
    /// </summary>
    public class DigitizedResourceItemM
    {
        /// <summary>
        /// 子库ID
        /// </summary>
        public string ItemID { get; set; }
        /// <summary>
        /// 子库库名称
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 缩略图路径
        /// </summary>
        public string PhotoPath { get; set; }
    }

    /// <summary>
    /// 外部应用系统
    /// </summary>
    public class OuterSystemM
    {
        /// <summary>
        /// ID（自增长）
        /// </summary>
        public short ID { get; set; }
        /// <summary>
        /// 系统ID
        /// </summary>
        public string SystemID { get; set; }
        /// <summary>
        /// 系统名称
        /// </summary>
        public string SystemName { get; set; }
        /// <summary>
        /// 适用的学科
        /// </summary>
        public string ApplySubjects { get; set; }
        /// <summary>
        /// 缩略图路径
        /// </summary>
        public string PhotoPath { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        public string Creator { get; set; }
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
    }

    /// <summary>
    /// 教材库目录
    /// </summary>
    public class LGZXDirM
    {
        /// <summary>
        /// ID（自增长）
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 目录ID
        /// </summary>
        public string DirID { get; set; }
        /// <summary>
        /// 目录名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 父目录ID
        /// </summary>
        public string PID { get; set; }
        /// <summary>
        /// Ftp路径
        /// </summary>
        public string FtpPath { get; set; }
        /// <summary>
        /// 缩略图路径
        /// </summary>
        public string JiaoCaiPicPath { get; set; }
        /// <summary>
        /// 排列顺序
        /// </summary>
        public int OrderNo { get; set; }
        /// <summary>
        /// 适用年级
        /// </summary>
        public short LevelID { get; set; }
        /// <summary>
        /// 是否是教材
        /// </summary>
        public short IsJiaoCai { get; set; }
        /// <summary>
        /// 是否有分类
        /// </summary>
        public int HaveSorts { get; set; }
        /// <summary>
        /// 是否含有课件
        /// </summary>
        public short HaveCourseWare { get; set; }
        /// <summary>
        /// 是否含有子目录
        /// </summary>
        public int HaveSubDir { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifiedTime { get; set; }
    }
}
