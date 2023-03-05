using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LBD_WebApiInterface.Models
{
   
    //与数据库保持一致
    /// <summary>
    /// 专用教材
    /// </summary>
    public class PublishingTextbookM
    {
        /// <summary>
        /// ID（自增长）
        /// </summary>
        public short ID { get; set; }
        /// <summary>
        /// 教材名称
        /// </summary>
        public string TextbookName { get; set; }
        /// <summary>
        /// 教材路径
        /// </summary>
        public string TextbookPath { get; set; }
        /// <summary>
        /// 缩略图路径
        /// </summary>
        public string PhotoPath { get; set; }
        /// <summary>
        /// 教材目录下所有文件总大小
        /// </summary>
        public long TextbookSize { get; set; }
        /// <summary>
        /// 教材目录下所有文件个数
        /// </summary>
        public int FileNum { get; set; }
        /// <summary>
        /// 学科ID
        /// </summary>
        public byte SubjectID { get; set; }
        /// <summary>
        /// 适配状态（0-完全没有适配，1-部分适配，2-全部适配）
        /// </summary>
        public byte AdaptStatus { get; set; }
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
    /// 素材类别
    /// </summary>
    public class MaterialCategoryM
    {
        /// <summary>
        /// 素材类别ID
        /// </summary>
        public short ID { get; set; }
        /// <summary>
        /// 素材类别名称
        /// </summary>
        public string MaterialName { get; set; }
        /// <summary>
        /// 应用价值ID
        /// </summary>
        public short ApplyValueID { get; set; }
        /// <summary>
        /// 教学模式ID
        /// </summary>
        public short TeachModeID { get; set; }
        /// <summary>
        /// 专用教材ID
        /// </summary>
        public short TextbookID { get; set; }
        /// <summary>
        /// 目录层次
        /// </summary>
        public byte LevelNo { get; set; }
        /// <summary>
        /// 素材类别所在目录的路径
        /// </summary>
        public string DirPath { get; set; }
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
    /// 应用价值
    /// </summary>
    public class ApplyValueM
    {
        /// <summary>
        /// 应用价值ID
        /// </summary>
        public short ID { get; set; }
        /// <summary>
        /// 应用价值名称
        /// </summary>
        public string ValueName { get; set; }
        /// <summary>
        /// 应用价值层级
        /// </summary>
        public byte LevelNo { get; set; }
        /// <summary>
        /// 父级应用价值ID
        /// </summary>
        public short ParentID { get; set; }
        /// <summary>
        /// 是否关联教学模式
        /// </summary>
        public bool IsRelated { get; set; }
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

    //根据业务逻辑衍生（这里只取需要用到的，完整的应该放在课堂信息管理中）
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
        /// 英语ID
        /// </summary>
        public string EnglishID { get; set; }
    }

    //根据业务逻辑衍生
    /// <summary>
    /// 应用价值节点（为构造节点树）
    /// </summary>
    public class ValueNodeM
    {
        /// <summary>
        /// 应用价值信息
        /// </summary>
        public ApplyValueM Value { get; set; }
        /// <summary>
        /// 应用价值关联的教学模式信息
        /// </summary>
        public List<TeachModeM> TeachMode { get; set; }
        /// <summary>
        /// 应用价值孩子节点
        /// </summary>
        public List<ValueNodeM> ChildNodes { get; set; }

    }

}
