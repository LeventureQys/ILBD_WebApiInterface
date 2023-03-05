using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace LBD_WebApiInterface.Utility
{
    public class CommandWS
    {
        public string BaseUrl { get; set; }

        private const int C_TimeOut = 8000;

//         public string CallMethodPost(string strMethodName,string strParam)
//         {
//             try
//             {
//                 //构造请求的URL
//                 string strUrl = BaseUrl;
//                 if (string.IsNullOrEmpty(strMethodName) == false)
//                 {
//                     strUrl = strUrl.TrimEnd('/') + "/"+strMethodName;
//                 }
// 
//                 string strReturn = CallMethodPost(strUrl, strParam);
// 
//                 return strReturn;
//             }
//             catch (Exception e)
//             {
//                 WriteErrorMessage("CallMethodPost", e.Message);
//             }
// 
//             return "";
//         }

        public string CallMethodPost(string strWholeUrl, string strParam)
        {
            try
            {

                //构造请求的URL
                if (string.IsNullOrEmpty(strWholeUrl))
                {
                    return "";
                }
                //构造HTTP请求对象
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strWholeUrl);
                //转成网络流
                if (strParam == null)
                {
                    strParam = "";
                }
                byte[] byteParam = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(strParam);
                //设置
                request.Method = "POST";
                request.Proxy = null;
                request.Timeout = C_TimeOut;
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteParam.Length;
                //request.MaximumAutomaticRedirections = 1;
                //request.AllowAutoRedirect = true;
                //request.CookieContainer = cookie;
                //发送请求
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(byteParam, 0, byteParam.Length);
                requestStream.Flush();
                requestStream.Close();
                //获得接口返回值
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //response.Cookies = cookie.GetCookies(response.ResponseUri);
                Stream responseStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                string strResult = sr.ReadToEnd();
                sr.Close();
                responseStream.Close();

                return strResult;
            }
            catch (Exception e)
            {
                WriteErrorMessage("CallMethodPost异常记录", string.Format("访问地址：{0}，参数：{1}", strWholeUrl, strParam));
                WriteErrorMessage("CallMethodPost", e.Message);
            }

            return "";
        }

        private void WriteErrorMessage(string strMethodName, string sErrorMessage)
        {
            DirectoryInfo clsPath = new DirectoryInfo(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory));
            DirectoryInfo clsSubPath = clsPath.CreateSubdirectory("Errlog\\LBD_WebApiInterface\\Api");

            if (clsSubPath.Exists)
            {
                DateTime clsDate = DateTime.Now;
                string strPath = clsSubPath.FullName + "\\CommandWS(" + String.Format("{0:yyyy-MM-dd}", clsDate) + ").log";
                StreamWriter clsWriter = new StreamWriter(strPath, true);
                clsWriter.WriteLine(String.Format("{0:HH:mm:ss}", clsDate) + " " + strMethodName + ": " + sErrorMessage);
                clsWriter.Close();
            }
        }

    }
}
