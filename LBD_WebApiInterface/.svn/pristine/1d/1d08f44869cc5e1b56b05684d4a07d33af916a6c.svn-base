using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LBD_WebApiInterface.Models.CourseCenter
{
    /// <summary>
    /// 课程中心接口返回结果形式
    /// </summary>
    /// <typeparam name="T">实际数据类型</typeparam>
    public class CCApiResultM<T>
    {
        /// <summary>
        /// 错误码（0-正常，1-参数有误，2-函数不存在）
        /// </summary>
        public int error { get; set; }
        /// <summary>
        /// 总资源数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 实际数据
        /// </summary>
        public T Data { get; set; }        
    }

    public class SubjectM {
        public string Subject { get; set; }//学科名称
    }

    public class TeacherM
    {
        public string TeacherID { get; set; }//教师id
        public string TeacherName { get; set; }//教师名称
    }
    /// <summary>
    /// 点播资源
    /// </summary>
    public class RequestResourceM
    {
        /// <summary>
        /// 资料ID
        /// </summary>
        public string ResID { get; set; }
        /// <summary>
        /// 资料名称
        /// </summary>
        public string ResName { get; set; }
        /// <summary>
        /// 父目录ID
        /// </summary>
        public string ParentID { get; set; }
        /// <summary>
        /// HTTP地址
        /// </summary>
        public string HttpUrl { get; set; }
        /// <summary>
        /// FTP地址
        /// </summary>
        public string FtpUrl { get; set; }
        /// <summary>
        /// 上传者ID
        /// </summary>
        public string UploaderID { get; set; }
        /// <summary>
        /// 上传者名称
        /// </summary>
        public string UploaderName { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        public string Theme { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword { get; set; }
        /// <summary>
        /// 资源类型
        /// </summary>
        public string ResType { get; set; }
        /// <summary>
        /// 共享状态
        /// </summary>
        public string ShareFlag { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
        /// <summary>
        /// 浏览数
        /// </summary>
        public int ScanNum { get; set; }
        /// <summary>
        /// 下载数
        /// </summary>
        public int DownloadNum { get; set; }
        /// <summary>
        /// 推荐数
        /// </summary>
        public int RecommedNum { get; set; }
    }
    /// <summary>
    /// 资源目录
    /// </summary>
    public class ResourceDir {
        /// <summary>
        /// 目录id
        /// </summary>
        public string DirID { get; set; }
        /// <summary>
        /// 目录名称
        /// </summary>
        public string DirName { get; set; }
       /// <summary>
       /// 主讲人id
       /// </summary>
        public string LecturerID { get; set; }
        /// <summary>
        /// 主讲人姓名
        /// </summary>
        public string LecturerName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
        /// <summary>
        /// 浏览数
        /// </summary>
        public int ScanNum { get; set; }
        /// <summary>
        /// 下载数
        /// </summary>
        public int DownloadNum { get; set; }
        /// <summary>
        /// 目录的排序字符串
        /// </summary>
        public string OrderNo { get; set; }
    }
    /// <summary>
    /// 广播资源
    /// </summary>
    public class BroadcastResourceM
    {
        /// <summary>
        /// 资源ID
        /// </summary>
        public string ResID { get; set; }
        /// <summary>
        /// 资源名称
        /// </summary>
        public string ResName { get; set; }
        /// <summary>
        /// 父目录ID
        /// </summary>
        public string ParentID { get; set; }
        /// <summary>
        /// 教师ID
        /// </summary>
        public string TeacherID { get; set; }
        /// <summary>
        /// 教师名
        /// </summary>
        public string TeacherName { get; set; }
        /// <summary>
        /// HTTP地址
        /// </summary>
        public string HttpUrl { get; set; }
        /// <summary>
        /// FTP地址
        /// </summary>
        public string FtpUrl { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        public string Theme { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 广播类型
        /// </summary>
        public string BroadcastType { get; set; }
        /// <summary>
        /// 浏览次数
        /// </summary>
        public int ScanNum { get; set; }
        /// <summary>
        /// 下载次数
        /// </summary>
        public int DownloadNum { get; set; }
        /// <summary>
        /// 推荐数
        /// </summary>
        public int RecommedNum { get; set; }
    }

    /// <summary>
    /// 直播资源
    /// </summary>
    public class LiveResourceM
    {
        /// <summary>
        /// 资料ID
        /// </summary>
        public string ResID { get; set; }
        /// <summary>
        /// 资料名称
        /// </summary>
        public string ResName { get; set; }
        /// <summary>
        /// 父目录ID
        /// </summary>
        public string ParentID { get; set; }
        /// <summary>
        /// 教师ID
        /// </summary>
        public string TeacherID { get; set; }
        /// <summary>
        /// 教师名称
        /// </summary>
        public string TeacherName { get; set; }
        /// <summary>
        /// HTTP地址
        /// </summary>
        public string HttpUrl { get; set; }
        /// <summary>
        /// FTP地址
        /// </summary>
        public string FtpUrl { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        public string Theme { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 直播类型
        /// </summary>
        public string LiveType { get; set; }
        /// <summary>
        /// 浏览次数
        /// </summary>
        public int ScanNum { get; set; }
        /// <summary>
        /// 下载次数
        /// </summary>
        public int DownloadNum { get; set; }
        /// <summary>
        /// 推荐数
        /// </summary>
        public int RecommedNum { get; set; }
    }
}
