using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LBD_WebApiInterface.Models
{

    public class TutorMes
    {
        /// <summary>
        /// 消息id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 发送消息的用户id
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 发送消息的用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 任务id
        /// </summary>
        public string AssignmentID { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string AssignmentName { get; set; }
        /// <summary>
        /// 资料id
        /// </summary>
        public string ResID { get; set; }
        /// <summary>
        /// 资料名称
        /// </summary>
        public string ResName { get; set; }
        /// <summary>
        /// 教师id
        /// </summary>
        public string TeacherID { get; set; }
        /// <summary>
        /// 教师名称
        /// </summary>
        public string TeacherName { get; set; }
        /// <summary>
        /// 系统id
        /// </summary>
        public object SysID { get; set; }
        /// <summary>
        /// 学科id
        /// </summary>
        public string SubjectID { get; set; }
        /// <summary>
        /// 学科名称
        /// </summary>
        public string SubjectName { get; set; }
        /// <summary>
        /// 发送消息时间
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        /// 消息查看跳转地址
        /// </summary>
        public string Url { get; set; }
        public int FromTopicIndex { get; set; }
    }

}
