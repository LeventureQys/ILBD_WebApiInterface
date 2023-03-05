using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LBD_WebApiInterface.Models.E_ResourceLibrary
{
    public class ApiResultM<T>
    {
        public int error { get; set; }
        public string msg { get; set; }
        public T data { get; set; }
    }

    /// <summary>
    /// 资源库各种分类方式
    /// </summary>
    public class ResourceCategoryM
    {
        public List<ResourceClassLevelOneForSearchM> resourceClasslevelOne { get; set; }
        public List<ResourceClassLevelTwoForSearchM> resourceClassForlevelTwo { get; set; }
        public List<CategoryInfoForSearchM> categorylist { get; set; }
        public List<SubjectNameForIndexM> subjectList { get; set; }
        public List<GradeInfoForSearchM> gradeList { get; set; }
    }

    /// <summary>
    /// 多学科资源库or公共资源库
    /// </summary>
    public class ResourceClassLevelOneForSearchM
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }

    /// <summary>
    /// 多学科资源库子库
    /// </summary>
    public class ResourceClassLevelTwoForSearchM
    {
        /// <summary>
        /// 子库ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 子库名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 父目录ID
        /// </summary>
        public string FatherID { get; set; }
    }

    public class CategoryInfoForSearchM
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }

    public class SubjectNameForIndexM
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
    }

    /// <summary>
    /// 年级信息
    /// </summary>
    public class GradeInfoForSearchM
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }

    /// <summary>
    /// 目录及资源
    /// </summary>
    public class FolderAndResourceM
    {
        /// <summary>
        /// 目录信息
        /// </summary>
        public List<FolderShowM> FolderList { get; set; }
        /// <summary>
        /// 资源信息
        /// </summary>
        public List<ResourceDetailM> Resource { get; set; }
    }

    /// <summary>
    /// 目录信息
    /// </summary>
    public class FolderShowM
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 父目录ID
        /// </summary>
        public int FatherID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 创建者ID
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
        /// 创建者名称
        /// </summary>
        public string CreatorName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 所属级数
        /// </summary>
        public int Level { get; set; }
    }

    public class ResourceSimpleM
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Category { get; set; }
        public string CategoryName { get; set; }
        public string SubjectID { get; set; }
        public string SubjectName { get; set; }
        public string GradeID { get; set; }
        public string GradeName { get; set; }
        public string Creator { get; set; }
        public string CreatorName { get; set; }
        public string SchoolID { get; set; }
        public DateTime Time { get; set; }
    }

    /// <summary>
    /// 资源详细信息
    /// </summary>
    public class ResourceDetailM
    {
        /// <summary>
        /// 资源ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 资源名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 创建者ID
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
        /// 创建者名称
        /// </summary>
        public string CreatorName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 学科ID
        /// </summary>
        public string SubjectID { get; set; }
        /// <summary>
        /// 年级ID
        /// </summary>
        public string GradeID { get; set; }
        /// <summary>
        /// 模板分类ID
        /// </summary>
        public int Category { get; set; }
        /// <summary>
        /// 学科名称
        /// </summary>
        public string SubjectName { get; set; }
        /// <summary>
        /// 年级名称
        /// </summary>
        public string GradeName { get; set; }
        /// <summary>
        /// 模板名称
        /// </summary>
        public string CategoryName { get; set; }
        //还有一个Tags属性，暂不需要，就不写了
        /// <summary>
        /// 资源简介
        /// </summary>
        public string Desc { get; set; }
        /// <summary>
        /// 缩略图路径
        /// </summary>
        public string IconPath { get; set; }
    }

    public class ResourceAndFileM
    {
        /// <summary>
        /// 资源
        /// </summary>
        public ResourceDetailM RModel { get; set; }
        /// <summary>
        /// 文件
        /// </summary>
        public List<FilesInfoM> Filelist { get; set; }
    }
    /// <summary>
    /// 基础信息
    /// </summary>
    public class BasicInfo
    {
        public string ResourcesPath { get; set; }
        public string ResourcesUrl { get; set; }

        public string SystemVersion { get; set; }
        }

    public class FilesInfoM
    {
        /// <summary>
        /// 文件ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 资源ID
        /// </summary>
        public string ResourceID { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string Extension { get; set; }
        /// <summary>
        /// 文件下载路径（全路径）
        /// </summary>
        public string Path { get; set; }
        public string WebPath { get; set; }
        /// <summary>
        /// 关联文件（例如音频文件会关联歌词文件）
        /// </summary>
        public object AttachFile { get; set; }
        /// <summary>
        /// 文件大小（单位KB）
        /// </summary>
        public double Size { get; set; }
        /// <summary>
        /// 总大小（包括转码文件，单位KB）
        /// </summary>
        public double TotalSize { get; set; }
        /// <summary>
        /// 是否可播放 0：不展示，1：展示
        /// </summary>
        public int Show { get; set; }
        /// <summary>
        /// 文件长度（视频、音频长度，文档页数）
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// 资源文件类型
        /// </summary>
        public string FileType { get; set; }
    }

    public class ResourceUseLog 
    {
        public string ResourceID { get; set; }
        public string FileID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
