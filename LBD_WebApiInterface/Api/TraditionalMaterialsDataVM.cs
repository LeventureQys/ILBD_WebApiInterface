using LBD_WebApiInterface.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace LBD_WebApiInterface.Api
{
    #region 出版社
    /// <summary>
    /// Writed by lw in 2020-03-02
    /// 不同出版社的教材
    /// </summary>
    public class TraditionalMaterialsDataVM
    {
        public List<TraPressVM> Press { get; set; }//出版社

        private string ip;
        private string port;
        public TraditionalMaterialsDataVM(string ip,string port)
        {
            this.ip = ip;
            this.port = port;   
        }

        /// <summary>
        /// 获取全部教材ID
        /// </summary>
        /// <returns></returns>
        public List<TraPressVM> GetAllTraditionalMaterialsData()
        {
            try
            {
                if (!string.IsNullOrEmpty(this.ip) && !string.IsNullOrEmpty(this.port))
                {
                    string url = string.Format("http://{0}:{1}/WebService.asmx/GetTraditionalMaterialsData", ip, port);
                    string result = CallApiHelper.CallMethod_Get(url);
                    JObject joResult = Ultility.DeserializeToJObject(result);
                    string  pressString = joResult["Result"]["Press"].ToString();
                    this.Press = JsonFormatter.JsonDeserialize<List<TraPressVM>>(pressString);
                    return this.Press;
                }
                else
                {
                    //throw new Exception("IP或端口为空");
                    return null;
                }
            }catch(Exception ex)
            {
                LogHelper.WriteErrorMessage("GetTraditionalMaterialsDataVM", ex.ToString());
                return null;
            }
        }
        /// <summary>
        /// 根据ID找出版社
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public TraPressVM GetTraditionalMaterialsDataById(int Id)
        {
            if(this.Press != null && this.Press.Count != 0)
            {
                var traditionalMaterial = this.Press.Where(t => t.Id == Convert.ToInt32(Id));
                if(traditionalMaterial !=null && traditionalMaterial.Count() != 0)
                {
                    return traditionalMaterial.First();
                }
                else
                {
                    #region 没有找到就重新再找一遍
                    this.Press = GetAllTraditionalMaterialsData();
                    if (this.Press != null && this.Press.Count != 0)
                    {
                        var material = this.Press.Where(t => t.Id == Convert.ToInt32(Id));
                        if (material != null && material.Count() != 0)
                        {
                            return material.First();
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                    #endregion
                }
            }
            else
            {
                this.Press = GetAllTraditionalMaterialsData();
                if(this.Press != null && this.Press.Count != 0)
                {
                    var traditionalMaterial = this.Press.Where(t => t.Id == Convert.ToInt32(Id));
                    if (traditionalMaterial != null && traditionalMaterial.Count() != 0)
                    {
                        return traditionalMaterial.First();
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
                
            }
        }
        
    }
    public class TraPressVM
    {
        public int Id { get; set; }
        public string PressName { get; set; }//出版社名称
        //public List<Grade> Grades{ get; set; }//b版本
        public List<TraPressVersionList> TraPressVersionList { get; set; }//版本
    }
    public class TraPressVersionList
    {
        public string TraPressVersion { get; set; }//版本（系列）
        public List<Book> books { get; set; }
    }

    public class Book
    {
        /// <summary>
        /// 传统教材Id
        /// </summary>
        public string BookId { get; set; }
        /// <summary>
        /// 传统教材名称
        /// </summary>
        public string BookName { get; set; }
        /// <summary>
        /// 传统教材封面
        /// </summary>
        public string BookCover { get; set; }
        /// <summary>
        /// 出版社
        /// </summary>
        public string PressName { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public string PressVersionName { get; set; }
        /// <summary>
        /// 阶段
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 阶段编码
        /// </summary>
        public string VersionCode { get; set; }
        /// <summary>
        /// 章节单元数
        /// </summary>
        public string CharpterCount { get; set; }
        /// <summary>
        /// 传统教材所属年级
        /// </summary>
        public string Grade { get; set; }
        /// <summary>
        /// 第几册
        /// </summary>
        public string Volumn { get; set; }
        /// <summary>
        ///状态0 正常   1 已删除
        /// </summary>
        public int State { get; set; }
    }

    /// <summary>
    /// 传统教材提供移动端接口
    /// </summary>
    public class TrabookVM
    {
        public int Id { get; set; }
        /// <summary>
        /// 传统教材Id
        /// </summary>
        public string BookId { get; set; }
        /// <summary>
        /// 传统教材名称
        /// </summary>
        public string BookName { get; set; }
        /// <summary>
        /// 阶段
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 阶段编码
        /// </summary>
        public string VersionCode { get; set; }
        /// <summary>
        /// 传统教材封面
        /// </summary>
        public string BookCover { get; set; }
        /// <summary>
        /// 出版社Id
        /// </summary>
        public int PressId { get; set; }
        /// <summary>
        /// 出版社
        /// </summary>
        public string PressName { get; set; }
        /// <summary>
        /// 版本Id
        /// </summary>
        public int PressVersionId { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public string PressVersionName { get; set; }
        /// <summary>
        /// 章节单元数
        /// </summary>
        public int CharpterCount { get; set; }
        /// <summary>
        /// 课时数
        /// </summary>
        public int TextCount { get; set; }
        /// <summary>
        /// 传统教材所属年级
        /// </summary>
        public string Grade { get; set; }
        /// <summary>
        /// 第几册
        /// </summary>
        public string Volumn { get; set; }
        /// <summary>
        ///状态0 正常   1 已删除
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 创建人Id
        /// </summary>
        public string CreateId { get; set; }
    }


    #endregion 出版社


    #region 传统教材
    /// <summary>
    /// Writed by lw in 2020-03-02
    ///  获取传统教材单元课时数据
    /// </summary>
    public class TraBookChapterVM
    {
        /// <summary>
        /// 教材Id
        /// </summary>
        public string BookId { get; set; }
        /// <summary>
        /// 教材名称
        /// </summary>
        public string BookName { get; set; }
        /// <summary>
        /// 单元章节列表
        /// </summary>
        public List<Chapter> chapterList { get; set; }

        private string ip;
        private string port;
        public TraBookChapterVM(string ip, string port)
        {
            this.ip = ip;
            this.port = port;
        }
        /// <summary>
        /// 根据教材ID与教材名称获取课时数据
        /// </summary>
        /// <param name="bookId">教材ID</param>
        /// <param name="bookName">教材名称</param>
        /// <returns></returns>
        public List<Chapter> GetBookChapterByIdAndName(string bookId, string bookName)
        {
            try
            {
                if (!string.IsNullOrEmpty(ip) && !string.IsNullOrEmpty(port))
                {
                    string url = string.Format("http://{0}:{1}/WebService.asmx/GetBookChapter?bookId={2}&bookName={3}", ip, port, bookId, bookName);
                    string result = CallApiHelper.CallMethod_Get(url);

                    JObject joResult = Ultility.DeserializeToJObject(result);
                    string pressString = joResult["Result"]["chapterList"].ToString();
                    this.chapterList = JsonFormatter.JsonDeserialize<List<Chapter>>(pressString);

                    return this.chapterList;
                }
                else
                {
                    //throw new Exception("IP或端口为空");
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorMessage("GetTraditionalMaterialsDataVM", ex.ToString());
                return null;
            }
        }
    }
    public class Chapter
    {
        /// <summary>
        /// 单元章节Id
        /// </summary>
        public string ChapterId { get; set; }
        /// <summary>
        /// 单元章节名称
        /// </summary>
        public string ChapterName { get; set; }
        /// <summary>
        /// 课时列表
        /// </summary>
        public List<TextTitle> textTitle { get; set; }
    }
    /// <summary>
    /// 课时
    /// </summary>
    public class TextTitle
    {
        /// <summary>
        /// 课时Id
        /// </summary>
        public string TextId { get; set; }
        /// <summary>
        /// 查询生词使用 
        /// </summary>
        public string TextCode { get; set; }
        /// <summary>
        /// 0-文本资料  1-声文资料
        /// </summary>
        public int Type { get; set; } = 0;
        /// <summary>
        /// 课时名称
        /// </summary>
        public string TextTitleName { get; set; }
    } 
    #endregion 传统教材

    #region 课文
    public class TraTextInfoVM
    {
        /// <summary>
        /// 课文Id
        /// </summary>
        public string TextId { get; set; }
        /// <summary>
        /// 课文名称
        /// </summary>
        public string TextName { get; set; }
        /// <summary>
        /// 课文内容
        /// </summary>
        public string TextContent { get; set; }
        public string FtpUserName { get; set; }
        public string FtpPwd { get; set; }
        /// <summary>
        ///资源列表
        /// </summary>
        public List<TraResource> ResList { get; set; }

        public string NewWords { get; set; }

        /// <summary>
        /// 路径前缀
        /// </summary>
        public string AudioPath { get; set; }
        ///// <summary>
        ///// 生词列表
        ///// </summary>
        //public List<NewWords> NewWordsList { get; set; }    
        private String ip { get; set; }
        private String port { get; set; }

        public TraTextInfoVM(String ip, String port)
        {
            this.ip = ip;
            this.port = port;
        }
        public List<TraResource> GetTextInfoByIdAndName(String textId, String textName)
        {
            try
            {
                if (!string.IsNullOrEmpty(ip) && !string.IsNullOrEmpty(port))
                {
                    string url = string.Format("http://{0}:{1}/WebService.asmx/GetTextInfo?textId={2}&textName={3}", ip, port, textId, textName);
                    string result = CallApiHelper.CallMethod_Get(url);

                    JObject joResult = Ultility.DeserializeToJObject(result);
                    string pressString = joResult["Result"]["ResList"].ToString();
                    this.ResList = JsonFormatter.JsonDeserialize<List<TraResource>>(pressString);
                    return this.ResList;
                }
                else
                {
                    //throw new Exception("IP或端口为空");
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorMessage("GetTraditionalMaterialsDataVM", ex.ToString());
                return null;
            }
        }
    }
    public class NewWords
    {
        /// <summary>
        /// 生词编码
        /// </summary>
        public string NewWordCode { get; set; }
        /// <summary>
        /// 生词名
        /// </summary>
        public string NewWordText { get; set; }
    }
    #endregion

    /// <summary>
    /// 资源Mocel
    /// </summary>
    public class TraResource
    {
        public int Id { get; set; }
        /// <summary>
        /// 资源Id
        /// </summary>
        public string ResourceId { get; set; }
        /// <summary>
        /// 资源名称
        /// </summary>
        public string ResourceName { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string ResourcePath { get; set; }
        /// <summary>
        ///状态0 正常   1 已删除
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 创建人Id
        /// </summary>
        public string CreateId { get; set; }
        /// <summary>
        /// 上次修改时间
        /// </summary>
        public string LastModifyTime { get; set; }
        /// <summary>
        /// 上次修改人
        /// </summary>
        public string LastModifyId { get; set; }
    }
    public static class Ultility
    {
        /// <summary>
        /// 根据一个json字符串反序列化为一个json对象
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static JObject DeserializeToJObject(string jsonString)
        {
            try
            {
                return JObject.Parse(jsonString);
                // return (Newtonsoft.Json.Linq.JObject) JsonConvert.DeserializeObject(jsonString);
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorMessage("JsonHelper的DeserializeJObject", ex.Message);
                throw;
            }
        }
    }
}
