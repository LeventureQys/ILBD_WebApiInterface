using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LBD_WebApiInterface.Utility;
using System.Xml;
using LBD_WebApiInterface.Models;

namespace LBD_WebApiInterface.Api
{
    public class TeachmaterialI
    {
        private string webapiIp;//校本教材库webapi ip地址
        private string webapiPort;//校本教材库webapi端口

        public TeachmaterialI(string webapiIp,string webapiPort) {
            this.webapiIp = webapiIp;
            this.webapiPort = webapiPort;
        }
        /// <summary>
        /// 获取校本教材库里面的教材
        /// <param name="userID ">用户ID</param>
        /// </summary>
        /// <returns></returns>
        public List<TeachmaterialM> GetLessonTeachMaterialInfo(string userID, string beiKeMuBiaoCode)
        {
            try
            {
                if (string.IsNullOrEmpty(webapiIp) || string.IsNullOrEmpty(webapiPort))
                    return null;
                string webapiAddr = string.Format("http://{0}:{1}/WebService.asmx/WS_Get_LessonTeachMaterialInfo?userID="+ userID, webapiIp, webapiPort);
                webapiAddr += "&beiKeMuBiaoCode=" + beiKeMuBiaoCode;
                string result = CallApiHelper.CallMethod_Get(webapiAddr);
                if (string.IsNullOrEmpty(result))
                    return null;
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(result);
                XmlNodeList nodeList=xd.GetElementsByTagName("string");
                if (nodeList == null || nodeList.Count <= 0)
                    return null;
                string jsonResult = nodeList[0].InnerText;
                Teachmaterialheaddata teachmaterialheaddata = JsonFormatter.JsonDeserialize<Teachmaterialheaddata>(jsonResult);
                return teachmaterialheaddata.teachMaterialHeadDatas;
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorMessage("GetLessonTeachMaterialInfo", e.ToString());
                return null;
            }
        }
        /// <summary>
        /// 根据教材id获取教材里面的课程
        /// </summary>
        /// <param name="teachMaterialID">教材id</param>
        /// <returns></returns>
        public List<TeachmaterialCourseM> GetLessonOneTeachMaterialInfo(string teachMaterialID)
        {
            try
            {
                if (string.IsNullOrEmpty(teachMaterialID))
                    return null;
                if (string.IsNullOrEmpty(webapiIp) || string.IsNullOrEmpty(webapiPort))
                    return null;
                string webapiAddr = string.Format("http://{0}:{1}/WebService.asmx/WS_Get_LessonOneTeachMaterialInfo?teachMaterialID={2}", webapiIp, webapiPort,teachMaterialID);
                string result = CallApiHelper.CallMethod_Get(webapiAddr);
                if (string.IsNullOrEmpty(result))
                    return null;
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(result);
                XmlNodeList nodeList = xd.GetElementsByTagName("string");
                if (nodeList == null || nodeList.Count <= 0)
                    return null;
                string jsonResult = nodeList[0].InnerText;
                TeachmaterialCourseApiResult teachmaterialCourseApiResult = JsonFormatter.JsonDeserialize<TeachmaterialCourseApiResult>(jsonResult);
                return teachmaterialCourseApiResult.teachMaterialRowDatas;
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorMessage("GetLessonOneTeachMaterialInfo", e.ToString());
                return null;
            }
        }
        /// <summary>
        /// 获取课程内的资料
        /// </summary>
        /// <param name="teachMaterialID">教材id</param>
        /// <param name="materialID">课程id</param>
        /// <returns></returns>
        public TeachmaterialCourseRecsM GetLessonOneTeachMaterialCourse(string teachMaterialID, string materialID)
        {
            try
            {
                if (string.IsNullOrEmpty(teachMaterialID))
                    return null;
                if (string.IsNullOrEmpty(webapiIp) || string.IsNullOrEmpty(webapiPort))
                    return null;
                string webapiAddr = string.Format("http://{0}:{1}/WebService.asmx/WS_Get_LessonOneTeachMaterialCourse?teachMaterialID={2}&materialID={3}", webapiIp, webapiPort, teachMaterialID,materialID);
                string result = CallApiHelper.CallMethod_Get(webapiAddr);
                if (string.IsNullOrEmpty(result))
                    return null;
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(result);
                XmlNodeList nodeList = xd.GetElementsByTagName("string");
                if (nodeList == null || nodeList.Count <= 0)
                    return null;
                string jsonResult = nodeList[0].InnerText;
                TeachmaterialCourseRecsM teachmaterialCourseRecsM = JsonFormatter.JsonDeserialize<TeachmaterialCourseRecsM>(jsonResult);
                return teachmaterialCourseRecsM;
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorMessage("GetLessonOneTeachMaterialCourse", e.ToString());
                return null;
            }
        }
    }
}
