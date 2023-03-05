using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LBD_WebApiInterface.Models.CloudPreparation
{
    /// <summary>
    /// 云备课系统的WebApi返回值的统一规范
    /// </summary>
    public class CLP_ApiResultM<T>
    {
        /// <summary>
        /// 错误码
        /// <para>0-正常</para>
        /// <para>1-操作失败</para>
        /// <para>2-解密失败</para>
        /// <para>3-参数有误</para>
        /// <para>4-Token失效（需要Token的场合）</para>
        /// </summary>
        public int ErrorFlag { get; set; }
        /// <summary>
        /// 异常消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 实际数据
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// 服务器时间
        /// </summary>
        public DateTime ServerTime { get; set; }
    }

    /// <summary>
    /// 教学方案集合
    /// </summary>
    public class TeachProgramSetM
    {
        public List<TeachProgramM> TeachProgram { get; set; }
        public int Count { get; set; }
    }

    /// <summary>
    /// 教学方案
    /// </summary>
    public class TeachProgramM
    {
        /// <summary>
        /// 教学方案ID
        /// </summary>
        public string TeachProgramID { get; set; }
        /// <summary>
        /// 教学方案名称
        /// </summary>
        public string TeachProgramName { get; set; }
        /// <summary>
        /// 教学方案GUID
        /// </summary>
        public Guid TeachProgramGUID { get; set; }
        /// <summary>
        /// FTP路径
        /// </summary>
        public string FtpPathFile { get; set; }
        /// <summary>
        /// HTTP路径
        /// </summary>
        public string HttpPathFloder { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastEditTime { get; set; }
        /// <summary>
        /// 共享状态:0表示不共享，1表示共享
        /// </summary>
        public int ShareStatus { get; set; }
        public bool IsShare
        {
            get
            {
                if (ShareStatus == 0)
                    return false;
                else
                    return true;
            }
        }
        /// <summary>
        /// 教学方案的作者
        /// </summary>
        public string TeacherID { get; set; }
    }

    /// <summary>
    /// 课前预习
    /// </summary>
    public class PreClassProgramM
    {
        /// <summary>
        /// 课前预习方案ID
        /// </summary>
        public string PreClassProgramID { get; set; }
        /// <summary>
        /// 教学方案GUID
        /// </summary>
        public Guid TeachProgramGUID { get; set; }
        /// <summary>
        /// 教学方案ID
        /// </summary>
        public string TeachProgramID { get; set; }
        /// <summary>
        /// HTTP路径
        /// </summary>
        public string HttpPath { get; set; }
        /// <summary>
        /// 课前预习任务发布状态，0-未发布未保存；1-已保存未发布；2-已发布
        /// </summary>
        //public string sState { get; set; }
        /// <summary>
        /// FTP路径
        /// </summary>
        public string FtpPath { get; set; }
        /// <summary>
        /// 课前预习方案名称
        /// </summary>
        public string PreClassProgramName { get; set; }
        /// <summary>
        /// 0-未发布未保存;1-已保存;2-已发布
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 课前预习任务ID
        /// </summary>
        public string PreClassAssignmentID { get; set; }
        /// <summary>
        /// 共享状态:0表示不共享，1表示共享
        /// </summary>
        public int ShareStatus { get; set; }
        public bool IsShare
        {
            get
            {
                if (ShareStatus == 0)
                    return false;
                else
                    return true;
            }
        }
        /// <summary>
        /// 课前预习方案的作者
        /// </summary>
        public string TeacherID { get; set; }
    }

    /// <summary>
    /// 课堂教案
    /// </summary>
    public class TeachClassProgramM
    {
        /// <summary>
        /// 课堂教案ID
        /// </summary>
        public string TeachClassProgramID { get; set; }
        /// <summary>
        /// 教学方案ID
        /// </summary>
        public string TeachProgramGUID { get; set; }
        public string FtpPath { get; set; }
        public string HttpPath { get; set; }
        /// <summary>
        /// 0-未发布未保存;1-已保存;2-已发布
        /// </summary>
        public int State { get; set; }
        public string TeachClassProgramName { get; set; }
        /// <summary>
        /// 共享状态:0表示不共享，1表示共享
        /// </summary>
        public int ShareStatus { get; set; }
        public bool IsShare
        {
            get
            {
                if (ShareStatus == 0)
                    return false;
                else
                    return true;
            }
        }
        /// <summary>
        /// 课堂教案的作者
        /// </summary>
        public string TeacherID { get; set; }
    }

    /// <summary>
    /// 课后测试（作业）
    /// </summary>
    public class AfterClassProgramM
    {
        /// <summary>
        /// 课后测试ID
        /// </summary>
        public string AfterClassProgramID { get; set; }
        /// <summary>
        /// 教学方案GUID
        /// </summary>
        public Guid TeachProgramGUID { get; set; }
        /// <summary>
        /// 教学方案ID
        /// </summary>
        public string TeachProgramID { get; set; }
        /// <summary>
        /// HTTP路径
        /// </summary>
        public string HttpPath { get; set; }
        /// <summary>
        /// 课后测试任务发布状态，0-未发布未保存；1-已保存未发布；2-已发布
        /// </summary>
        //public string sState { get; set; }
        /// <summary>
        /// FTP路径
        /// </summary>
        public string FtpPath { get; set; }
        /// <summary>
        /// 课后测试名称
        /// </summary>
        public string AfterClassProgramName { get; set; }
        /// <summary>
        /// 0-未发布未保存;1-已保存;2-已发布
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 课后作业任务ID
        /// </summary>
        public string AfterClassAssignmentID { get; set; }
        /// <summary>
        /// 共享状态:0表示不共享，1表示共享
        /// </summary>
        public int ShareStatus { get; set; }
        public bool IsShare
        {
            get
            {
                if (ShareStatus == 0)
                    return false;
                else
                    return true;
            }
        }
        /// <summary>
        /// 课后作业的作者
        /// </summary>
        public string TeacherID { get; set; }
    }

    /// <summary>
    /// 相关教辅
    /// </summary>
    public class AttachResourceProgramM
    {
        public string AttachResourceProgramID { get; set; }
        public Guid TeachProgramGUID { get; set; }
        public string FtpPath { get; set; }
        public string HttpPath { get; set; }
        public string AttachResourceProgramName { get; set; }
    }

    ///// <summary>
    ///// 教学计划
    ///// </summary>
    //public class TeachPlanM
    //{
    //    public long ID { get; set; }
    //    public string TeachProgramID { get; set; }
    //    public string MyTeachingPlanName { get; set; }
    //    public DateTime TeachTime { get; set; }
    //    public int ClassID { get; set; }
    //    public string ClassName { get; set; }
    //    public string ClassroomID { get; set; }
    //    public string ClassroomName { get; set; }
    //    public string Creator { get; set; }
    //    public DateTime CreateTime { get; set; }
    //    public DateTime LastEditTime { get; set; }
    //}

    /// <summary>
    /// 课前预习、课堂教案、课后测试、相关教辅下包含的资料信息
    /// </summary>
    public class CoursewareResInfoM
    {
        /// <summary>
        /// 自增长ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 教学方案GUID
        /// </summary>
        public Guid TeachProgramGUID { get; set; }
        /// <summary>
        /// 资料来源ID
        /// </summary>
        public int ResOriginTypeID { get; set; }
        /// <summary>
        /// 资料类型（预习课文、讲解视频、重难点课件等...）
        /// </summary>
        public string ContentTypeID { get; set; }
        /// <summary>
        /// 资料ID
        /// </summary>
        public string ResID { get; set; }
        /// <summary>
        /// 资料名称
        /// </summary>
        public string ResName { get; set; }
        /// <summary>
        /// 资料后缀
        /// </summary>
        public string FileExtension { get; set; }
        /// <summary>
        /// 资料的排序
        /// </summary>
        public int OrderNo { get; set; }
        /// <summary>
        /// 资料设置信息
        /// </summary>
        public string ResSetInfo { get; set; }
        /// <summary>
        /// HTTP下载路径
        /// </summary>
        public string HttpPath { get; set; }
        /// <summary>
        /// FTP下载路径
        /// </summary>
        public string FtpPath { get; set; }
    }

    /// <summary>
    /// 课前预习任务
    /// </summary>
    public class PreClassAssignmentM
    {
        /// <summary>
        /// 课前预习任务ID
        /// </summary>
        public string PreClassAssignmentID { get; set; }
        /// <summary>
        /// 课前预习ID
        /// </summary>
        public string PreClassProgramID { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastEditTime { get; set; }
        /// <summary>
        /// 课前预习任务名称
        /// </summary>
        public string PreClassAssignmentName { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 学习方式
        /// </summary>
        public int StudyStyle { get; set; }
        /// <summary>
        /// 达标要求设置信息
        /// </summary>
        public string StandardRequirements { get; set; }
        /// <summary>
        /// 发布任务状态（1-未发布，2-发布，3-已完成）
        /// </summary>
        public int State { get; set; }
    }

    /// <summary>
    /// 课后测试任务
    /// </summary>
    public class AfterClassAssignmentM
    {
        /// <summary>
        /// 课后测试任务ID
        /// </summary>
        public string AfterClassAssignmentID { get; set; }
        /// <summary>
        /// 课后测试ID
        /// </summary>
        public string AfterClassProgramID { get; set; }
        /// <summary>
        /// 课后测试任务名称
        /// </summary>
        public string AfterClassAssignmentName { get; set; }
        /// <summary>
        /// 限时作答类型
        /// </summary>
        public int MarkType { get; set; }
        /// <summary>
        /// 限制时长
        /// </summary>
        public int LimitingTime { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 互评或者自动评阅（1互评，2自动评阅）
        /// </summary>
        public int ReviewStyle { get; set; }
        /// <summary>
        /// 发布任务状态（1-未发布，2-已发布,3-已完成）
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 最后编辑时间
        /// </summary>
        public DateTime LastEditTime { get; set; }
    }

    /// <summary>
    /// 离线课后作业生成离线作来返回值规范
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OHW_ApiResultM<T>
    {
        public string Code{get;set;}
        public string Msg { get; set; }
        /// <summary>
        /// 实际数据
        /// </summary>
        public T Data { get; set; }
    }
    /// <summary>
    /// 离线作业生成，保存的ftpinfo类
    /// </summary>
    public class OfflineHomeworkFtpInfo
    {
        public string FtpIP { get; set; }
        public string Ftpport { get; set; }
        public string Ftppath { get; set; }
        public string FtpUserName { get; set; }
        public string Ftppassword { get; set; }
    }
    /// <summary>
    /// 作业上传信息类
    /// </summary>
    public class UnpackTaskSuccessMode
    {
        public string userID { get; set; }//学生ID
        public string assignmentID { get; set; }//任务ID
        public string ftpIP { get; set; }//FTP  IP地址
        public string ftpPort { get; set; }//FTP 端口号
        public string ftpPath { get; set; }     // FTP 路径“/LGFTP/homeworkAll/unpackTaskdownload/3/1.exe”
        public string ftpUserID { get; set; }   // 用户ID
        public string ftpPassword { get; set; } // 用户密码
    }

    /// <summary>
    /// 学生课后作业作答方式集合
    /// </summary>
    public class UnpackTaskStuInfoMode
    {
        public string UserID { get; set; }
        public int DoType { get; set; }//作答方式：0-未选择；1-在线；2-离线
    }

    /// <summary>
    /// 学生课后作业作答方式集合（带任务ID）
    /// </summary>
    public class UnpackTaskStuInfo
    {
        public string assignmentID { get; set; }//作业作务ID
        public List<UnpackTaskStuInfoMode> stuInfos { get; set; }
    }
    
    /// <summary>
    /// 智能化课件信息
    /// </summary>
    public class ItelCoursewareM
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
        //public byte CoursewareType { get; set; }
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
        public ItelCoursewareSetInfoM CoursewareSetInfo { get; set; }
    }
    /// <summary>
    /// 智能化课件设置信息
    /// </summary>
    public class ItelCoursewareSetInfoM
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
    /// <summary>
    /// 云备课服务器信息
    /// </summary>
    public class CloudPreparationSrvInfoM
    {
        /// <summary>
        /// 服务器代码
        /// </summary>
        public string ServerCode { get; set; }
        /// <summary>
        /// 服务器描述
        /// </summary>
        public string ServerDescription { get; set; }
        /// <summary>
        /// 服务器ip地址
        /// </summary>
        public string ServerIP { get; set; }
        /// <summary>
        /// 服务器端口
        /// </summary>
        public string ServerPort{ get; set; }
        /// <summary>
        /// 服务器Ftp用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 服务器Ftp密码
        /// </summary>
        public string UserPwd{ get; set; }
        /// <summary>
        /// 服务器虚拟路径
        /// </summary>
        public string VirtualPath{ get; set; }
        /// <summary>
        /// 服务器物理路径
        /// </summary>
        public string PhyPath { get; set; }
    }
    
    //public class CloudPreparationSrvInfoList
    //{
    //    public List<CloudPreparationSrvInfo> ServerInfo { get; set; }
    //    public int Count { get; set; }
    //}

}
