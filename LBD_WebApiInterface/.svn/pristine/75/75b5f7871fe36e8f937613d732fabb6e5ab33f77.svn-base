using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LBD_WebApiInterface.Models.CloudPlatform
{
    //云平台下的子系统
    public class CloudPlatformSubsystemM
    {
        //系统ID
        public string SysID { get; set; }
        //学科ID
        public string SubjectID { get; set; }
        //系统web服务器地址
        public string WebSvrAddr { get; set; }
        //系统WS服务地址
        public string WsSvrAddr { get; set; }
    }

    //学科平台下的系统
    public class SubjectPlatformSysInfoM
    {
        //学科ID
        public string SubjectID { get; set; }
        //学科名称
        public string SubjectName { get; set; }
        //系统ID
        public string SysID { get; set; }
        //系统名称
        public string SysName { get; set; }
        //缩略图路径（用于显示）
        public string SysImage { get; set; }
        //是否是exe程序
        public bool IsEXE { get; set; }
        //系统是否已被安装
        public bool IsSetup { get; set; }
        //系统主界面访问地址
        public string AccessAddr { get; set; }
        //系统Web服务器地址
        public string WebSvrAddr { get; set; }
        //系统WS服务地址
        public string WsSvrAddr { get; set; }
    }

    //学校基本信息
    public class SchoolBaseInfoM
    {
        public string SchoolID { get; set; }
        public string SchoolName { get; set; }
        public string SchoolCode { get; set; }
        public string SchoolLevel { get; set; }
        public string SchoolType { get; set; }
        public string SchoolState { get; set; }
        public string CreateTime { get; set; }
    }

    //教室信息
    public class ClassroomInfoM
    {
        //教室ID
        public string ID { get; set; }
        //教室名称
        public string CRName { get; set; }
        //教师机IP地址
        public string TMIP { get; set; }
        //教师机Mac地址
        public string TMMac { get; set; }
        //机位行数
        public string SeatRowCount { get; set; }
        //机位列数
        public string SeatColCount { get; set; }
        //机位排列方向
        public string SeatArrangeDirect { get; set; }
        //机位起止IP地址
        public string IPRange { get; set; }
        //更新时间
        public DateTime UpdateTime { get; set; }
    }

    //云平台的学科
    public class CloudPlatformSubjectM
    {
        public string SubjectID { get; set; }
        public string SubjectName { get; set; }
        public string Grades { get; set; }
    }

    public class CloudApiResultM<T>
    {
        public int error { get; set; }
        public T data { get; set; }
    }
    
    public class LoginApiDataM
    {
        public string Result { get; set; }
        public string Token { get; set; }
    }

    public class LoginApiSuccessDataM
    {
        public string Result { get; set; }
        public string Token { get; set; }
    }
}
