using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LBD_WebApiInterface.Models.CloudPlatform
{
    public class ScheduleInfoM
    {
        public string CalssDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string TeacherID { get; set; }
        public string TeacherName { get; set; }
        public string TeacherPhoto { get; set; }
        public string CourseClassID { get; set; }
        public string CourseClassName { get; set; }
        public string CourseNo { get; set; }
        public string CourseName { get; set; }
        public string SubjectID { get; set; }
        public string SubjectName { get; set; }
        public string ScheduleID { get; set; }
    }
}
