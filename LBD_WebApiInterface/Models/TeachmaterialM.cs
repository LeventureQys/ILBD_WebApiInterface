using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace LBD_WebApiInterface.Models
{
    public class Teachmaterialheaddata
    {
        public List<TeachmaterialM> teachMaterialHeadDatas;
    }

    public class TeachmaterialM
    {
        /// <summary>
        /// 教材ID
        /// </summary>
        public string TeachMaterialID { get; set; }
        /// <summary>
        /// 教材名称
        /// </summary>
        public string teachMaterialLibraryName { get; set; }
        /// <summary>
        /// 教材封面路径
        /// </summary>
        public string CoverImagePath { get; set; }
        /// <summary>
        /// 教材编辑Ftp用户名
        /// </summary>
        public string TextFtpUserName { get; set; }
        /// <summary>
        /// 教材编辑Ftp密码
        /// </summary>
        public string TextFtpPwd { get; set; }
        /// <summary>
        /// 教材教学大纲
        /// </summary>
        public string learningPare { get; set; }
        /// <summary>
        /// 教材课时总数
        /// </summary>
        public int FinisedCLassNum { get; set; }
        /// <summary>
        /// 教材话题
        /// </summary>
        public string SecTopicName { get; set; }
    }

    public class TeachmaterialCourseApiResult
    {
        public string TeachMaterialID { get; set; }
        public List<TeachmaterialCourseM> teachMaterialRowDatas { get; set; }
    }

    public class TeachmaterialCourseM 
    {
        /// <summary>
        /// 课时ID（素材ID）
        /// </summary>
        public string MaterialID { get; set; }
        /// <summary>
        /// 课程号
        /// </summary>
        public int classNo { get; set; }
        /// <summary>
        /// 课程名称
        /// </summary>
        public string MaterialName { get; set; }
        /// <summary>
        /// 话题
        /// </summary>
        public string SecTopicName { get; set; }
        /// <summary>
        /// 生词量
        /// </summary>
        public int NewWords { get; set; }
        /// <summary>
        /// 生词率
        /// </summary>
        public string NewWordsRate { get; set; }
        /// <summary>
        /// 超纲率
        /// </summary>
        public string SuperSteelRate { get; set; }
        /// <summary>
        /// 篇幅量
        /// </summary>
        public int TotalWords { get; set; }
        /// <summary>
        /// 教学时长
        /// </summary>
        public int TotalLessonTime { get; set; }
    }

    public class TeachmaterialCourseRecsM
    {
        /// <summary>
        /// 教材ID
        /// </summary>
        public string TeachMaterialID { get; set; }
        /// <summary>
        /// 教材大纲编码
        /// </summary>
        public string JiaoCaiDaGangCode { get; set; }
        /// <summary>
        /// 目标阶段编码
        /// </summary>
        public string MuBiaoJieDuanCode { get; set; }
        /// <summary>
        /// 课时ID(素材ID)
        /// </summary>
        public string MaterialID { get; set; }
        /// <summary>
        /// 课程号
        /// </summary>
        public int classNo { get; set; }
        /// <summary>
        /// 课程名称
        /// </summary>
        public string MaterialName { get; set; }
        /// <summary>
        /// 原文素材Ftp地址
        /// </summary>
        public string CoverMaterialPath { get; set; }
        /// <summary>
        /// 素材格式
        /// </summary>
        public string CoursewareFormat { get; set; }
        /// <summary>
        /// 数字化资源库Ftp用户名
        /// </summary>
        public string SourceFtpUserName { get; set; }
        /// <summary>
        /// 数字化资源库Ftp密码
        /// </summary>
        public string SourceFtpPwd { get; set; }
        /// <summary>
        /// 学习阶段
        /// </summary>
        public string LeaningPare { get; set; }
        /// <summary>
        /// 知识点
        /// </summary>
        public string Knowledge { get; set; }
        /// <summary>
        /// 知识点编码
        /// </summary>
        public string KnowledgeCode { get; set; }
        /// <summary>
        /// 课件包ftp地址
        /// </summary>
        public string CourseWarePath { get; set; }
        /// <summary>
        /// 试卷包ftp地址
        /// </summary>
        public string PaperPath { get; set; }
        /// <summary>
        /// 教材编辑Ftp用户名
        /// </summary>
        public string TextFtpUserName { get; set; }
        /// <summary>
        /// 教材编辑Ftp密码
        /// </summary>
        public string TextFtpPwd { get; set; }
    }
}
