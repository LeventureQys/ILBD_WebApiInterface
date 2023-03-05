using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LBD_WebApiInterface.Models
{
    public class CloudSubjectM
    {
        public string SubjectID { get; set; }
        public string SubjectName { get; set; }
        public string Grades { get; set; }
    }

    /// <summary>
    /// 学科的购买信息
    /// </summary>
    public class SubjectPurchaseInfoM
    {
        /// <summary>
        /// 结果标识
        /// <para>1—正常获取</para>
        /// <para>-1—未检测到加密所</para>
        /// <para>-2—加密锁已过期</para>
        /// <para>-3—加密锁不能用于该产品</para>
        /// <para>-4—加密锁接口调用错误</para>
        /// <para>-5—加密锁时钟错误</para>
        /// </summary>
        public int ResultFlag { get; set; }
        /// <summary>
        /// 每种结果标识对应的文字说明
        /// </summary>
        public string ResultMsg { get; set; }
        /// <summary>
        /// 是否购买了语文。 0—否， 1—是。当ResultFlag!=1时，此值为-1
        /// </summary>
        public int HasChinese { get; set; }
        /// <summary>
        /// 是否购买了数学。 0—否， 1—是。当ResultFlag!=1时，此值为-1
        /// </summary>
        public int HasMaths { get; set; }
        /// <summary>
        /// 是否购买了英语。 0—否， 1—是。当ResultFlag!=1时，此值为-1
        /// </summary>
        public int HasEnglish { get; set; }
        /// <summary>
        /// 是否购买了物理。 0—否， 1—是。当ResultFlag!=1时，此值为-1
        /// </summary>
        public int HasPhysics { get; set; }
        /// <summary>
        /// 是否购买了化学。 0—否， 1—是。当ResultFlag!=1时，此值为-1
        /// </summary>
        public int HasChemistry { get; set; }
        /// <summary>
        /// 是否购买了生物。 0—否， 1—是。当ResultFlag!=1时，此值为-1
        /// </summary>
        public int HasBiology { get; set; }
        /// <summary>
        /// 是否购买了政治。 0—否， 1—是。当ResultFlag!=1时，此值为-1
        /// </summary>
        public int HasPolitics { get; set; }
        /// <summary>
        /// 是否购买了历史。 0—否， 1—是。当ResultFlag!=1时，此值为-1
        /// </summary>
        public int HasHistory { get; set; }
        /// <summary>
        /// 是否购买了地理。 0—否， 1—是。当ResultFlag!=1时，此值为-1
        /// </summary>
        public int HasGeography { get; set; }
    }
    public class SubjectHelper
    {
        public static string getSubNameById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;
            string subName = null;
            switch (id)
            {
                case "S1-English":
                case "S2-English":
                case "S3-English":
                case "S4-English": subName = "英语"; break;
                case "S1-Chinese":
                case "S2-Chinese": subName = "语文"; break;
                case "S1-Maths":
                case "S2-Maths": subName = "数学"; break;
                case "S2-Biology": subName = "生物"; break;
                case "S2-Chemistry": subName = "化学"; break;
                case "S2-Geography": subName = "地理"; break;
                case "S2-History": subName = "历史"; break;
                case "S2-Physics": subName = "物理"; break;
                case "S2-Politics": subName = "政治"; break;
                //case "S2-Science":
                //case "Science": subName = "科学"; break;
                //case "S2-Morality": subName = "道德与法制"; break;
                //case "S2-Calligraphy": subName = "书法"; break;
                //case "S2-YPA": subName = "少先队活动"; break;
                //case "S2-Write": subName = "写字"; break;
                //case "S2-Composition": subName = "作文"; break;
                //case "S2-Research": subName = "研学"; break;
                //case "S2-Labour": subName = "劳动"; break;
                //case "S2-Poetry": subName = "古诗词"; break;
                //case "S2-SelfStudy": subName = "自习"; break;
                //case "S2-PE": subName = "体育与健康"; break;
                //case "S2-Painting":
                //case "Art": subName = "美术"; break;
                //case "S2-Music": subName = "音乐"; break;
                //case "S2-Zither": subName = "古筝"; break;
                //case "S2-IT": subName = "信息技术"; break;
                //case "S2-Read": subName = "读书"; break;
                //case "S2-Assn": subName = "社团活动"; break;
                //case "S2-FreeTime": subName = "自主活动"; break;
                default: break;
            }
            return subName;
        }
    }
}