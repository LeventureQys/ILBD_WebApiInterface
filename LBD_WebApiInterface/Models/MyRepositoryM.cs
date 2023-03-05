using System.Collections.Generic;
using LBD_WebApiInterface.MultipleSubjectM;

namespace LBD_WebApiInterface.Models
{
    /// <summary>
    /// 我的资料库
    /// </summary>
    public class MyRepositoryM
    {
        /// <summary>
        /// 电子资源库
        /// </summary>
        public R_MyDigitizedResourceM MyDigitizedResource { get; set; }
        /// <summary>
        /// “我的收藏”
        /// </summary>
        public R_MyFavoriteM MyFavorite { get; set; }
        /// <summary>
        /// 我的笔记
        /// </summary>
        public R_MyNoteM MyNote { get; set; }
        /// <summary>
        /// 我的网盘
        /// </summary>
        public R_MyNetDiskM MyNetDisk { get; set; }
        /// <summary>
        /// 我的网络化课件
        /// </summary>
        public R_MyNetCoursewareM MyNetCourseware { get; set; }
    }

    public class R_MyDigitizedResourceM
    {
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }
        /// <summary>
        /// 是否有值
        /// </summary>
        public bool HasValue { get; set; }
        /// <summary>
        /// 各项资源库具体来源
        /// </summary>
        public List<DigitizedResourceItemM> SourceItems { get; set; }
    }

    public class R_MyFavoriteM
    {
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }
        /// <summary>
        /// 是否有值
        /// </summary>
        public bool HasValue { get; set; }
    }

    public class R_MyNoteM
    {
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }
        /// <summary>
        /// 是否有值
        /// </summary>
        public bool HasValue { get; set; }
    }

    public class R_MyNetDiskM
    {
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }
        /// <summary>
        /// 是否有值
        /// </summary>
        public bool HasValue { get; set; }
    }

    public class R_MyNetCoursewareM
    {
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }
        /// <summary>
        /// 是否有值
        /// </summary>
        public bool HasValue { get; set; }
        /// <summary>
        /// 所有网络化课件
        /// </summary>
        public List<NetCoursewareM> NetCoursewares { get; set; }
    }

    public class FavoriteFolderM
    {
        public string FolderID { get; set; }
        public string FolderName { get; set; }
        public string PID { get; set; }
    }

    public class FavoriteResInfoM
    {
        public string ResID { get; set; }
        public string ResName { get; set; }
        public string ResAbstract { get; set; }
        public string ResRemark { get; set; }
        public string CollectTime { get; set; }
        public string ResLinkForWeb { get; set; }
        public string ResLinkForAndroid { get; set; }
        public string ResLinkForIos { get; set; }
        public string RecordCount { get; set; }
    }

    public class PersonDiskFolderOrFileM
    {
        //目录或文件ID
        public string FolderOrFileID { get; set; }
        //目录或文件名称
        public string FolderOrFileName { get; set; }
        //更新时间
        public string UpdateTime { get; set; }
        //父目录ID
        public string PID { get; set; }
        //是否是目录
        public bool IsFolder { get; set; }
    }

    public class PersonDiskFileM
    {
        //文件ID
        public string FileID { get; set; }
        //文件名称
        public string FileName { get; set; }
        //文件类型
        public string FileType { get; set; }
        //所属文件夹ID
        public string FolderID { get; set; }
        //文件路径（包含HTTP信息的完整路径）
        public string FilePath { get; set; }
        //文件大小
        public string FileSize { get; set; }
        //更新时间
        public string UpdateTime { get; set; }
    }

}
