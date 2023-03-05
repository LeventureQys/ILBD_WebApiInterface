using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LBD_WebApiInterface.MultipleSubjectM
{
    /// <summary>
    /// 学科信息
    /// </summary>
    public class SubjectInfoM
    {
        /// <summary>
        /// 目录ID，这里也代表学科ID
        /// </summary>
        public int DirectoryID { get; set; }
        /// <summary>
        /// 父目录ID
        /// </summary>
        public int FatherDirectoryID { get; set; }
        /// <summary>
        /// 目录名
        /// </summary>
        public string DirectoryName { get; set; }
        /// <summary>
        /// 缩略图路径
        /// </summary>
        public string IconPath { get; set; }
    }

    /// <summary>
    /// 年级信息
    /// </summary>
    public class GradeInfoM
    {
        /// <summary>
        /// 年级ID
        /// </summary>
        public int GradeID { get; set; }
        /// <summary>
        /// 年级名称
        /// </summary>
        public string GradeName { get; set; }
    }

    /// <summary>
    /// 目录信息
    /// </summary>
    public class DirInfoM
    {
        /// <summary>
        /// 目录ID
        /// </summary>
        public int DirectoryID { get; set; }
        /// <summary>
        /// 父目录ID
        /// </summary>
        public int FatherDirectoryID { get; set; }
        /// <summary>
        /// 目录名称
        /// </summary>
        public string DirectoryName { get; set; }
        /// <summary>
        /// 缩略图路径
        /// </summary>
        public string IconPath { get; set; }
    }

    /// <summary>
    /// 资源包信息
    /// </summary>
    public class ResourceInfoM
    {
        /// <summary>
        /// 资源ID
        /// </summary>
        public int ResourceID { get; set; }
        /// <summary>
        /// 资源名称
        /// </summary>
        public string ResourceName { get; set; }
        /// <summary>
        /// 缩略图路径
        /// </summary>
        public string IconPath { get; set; }
        /// <summary>
        /// 上传者ID
        /// </summary>
        public string UploadUserID { get; set; }
        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime UploadTime { get; set; }
        /// <summary>
        /// 资源种类
        /// </summary>
        public int ResourceType { get; set; }
        /// <summary>
        /// 资源所在文件夹路径
        /// </summary>
        public string FolderPath { get; set; }
        /// <summary>
        /// 资源内容文件个数
        /// </summary>
        public int ContentNum { get; set; }
        /// <summary>
        /// 资源附件文件个数
        /// </summary>
        public int AttachmentNum { get; set; }
    }

    /// <summary>
    /// 目录和资源信息
    /// </summary>
    public class DirectoryAndResourceInfoM
    {
        /// <summary>
        /// 目录信息
        /// </summary>
        public List<DirInfoM> Dir { get; set; }
        /// <summary>
        /// 资源信息
        /// </summary>
        public List<ResourceInfoM> Resource { get; set; }
    }

    /// <summary>
    /// 资源内容文件
    /// </summary>
    public class ResourceContentInfoM
    {
        /// <summary>
        /// 内容文件ID
        /// </summary>
        public int ContentID { get; set; }
        /// <summary>
        /// 资源ID
        /// </summary>
        public int ResourceID { get; set; }
        /// <summary>
        /// 内容文件名称
        /// </summary>
        public string ContentName { get; set; }
        /// <summary>
        /// 内容文件路径
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 内容文件扩展名
        /// </summary>
        public string Extension { get; set; }
    }

    /// <summary>
    /// 资源附件文件
    /// </summary>
    public class ResourceAttachmentInfoM
    {
        /// <summary>
        /// 附件ID
        /// </summary>
        public int AttachmentID { get; set; }
        /// <summary>
        /// 资源ID
        /// </summary>
        public int ResourceID { get; set; }
        /// <summary>
        /// 附件名称
        /// </summary>
        public string AttachmentName { get; set; }
        /// <summary>
        /// 附件路径
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 附件扩展名
        /// </summary>
        public string Extension { get; set; }
    }

    /// <summary>
    /// 资源详细信息
    /// </summary>
    public class ResourceContentAndAttachmentInfoM
    {
        /// <summary>
        /// 资源内容文件信息
        /// </summary>
        public List<ResourceContentInfoM> rcList { get; set; }
        /// <summary>
        /// 资源附件信息
        /// </summary>
        public List<ResourceAttachmentInfoM> raList { get; set; }
    }

    /// <summary>
    /// 资源详细信息
    /// </summary>
    public class ResourceDetailInfoM
    {
        /// <summary>
        /// 资源本身的信息
        /// </summary>
        public ResourceInfoM Resource { get; set; }
        /// <summary>
        /// 资源文件信息
        /// </summary>
        public List<ResourceContentInfoM> Content { get; set; }
        /// <summary>
        /// 资源附件信息
        /// </summary>
        public List<ResourceAttachmentInfoM> Attachment { get; set; }
    }

    //根据业务逻辑衍生
    /// <summary>
    /// 资源文件（将Content和Attachment整合）
    /// </summary>
    public class ResourceFileM
    {
        /// <summary>
        /// 文件ID
        /// </summary>
        public int FileID { get; set; }
        /// <summary>
        /// 资源ID
        /// </summary>
        public int ResourceID { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 扩展名
        /// </summary>
        public string Extension { get; set; }
        /// <summary>
        /// 文件还是附件，true-Content, false-Attachment
        /// </summary>
        public bool ContentOrAttachment { get; set; }
    }

    internal class BasicInfo
    {
        /// <summary>
        /// 资源站点本地路径
        /// </summary>
        public string ResourcePath { get; set; }
        /// <summary>
        /// 资源站点URL地址
        /// </summary>
        public string ResourceUrl { get; set; }
    }
}
