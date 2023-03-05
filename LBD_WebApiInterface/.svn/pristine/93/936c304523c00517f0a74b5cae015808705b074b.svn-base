using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Diagnostics;
using LBD_WebApiInterface.Utility;
using LBD_TeachCenterModel;
using Newtonsoft.Json;

namespace LBD_WebApiInterface.Utility
{
    public class CommandApi
    {
        public enum ApiType
        {
            FalseApi = 1,
            WebService = 2
        }
        public string BaseUrl { get; set; }

        private const int C_TimeOut = 8000;

        public CommandApi()
        {
            //CommandApiType = ApiType.FalseApi;
        }


        /******************************************************
         * 调试时发现发送PUT和DELETE请求时返回405错误码，说明这两个动词不被支持。
         * 但具体是程序配置还是浏览器还是IIS配置导致的，原因不明。
         * 目前将PUT改成POST，DELETE改成GET
         ******************************************************/

        public string CallMethodGet(string strActionName, string[] arrParam)
        {
            try
            {
                string strUrl = BaseUrl;
                if (string.IsNullOrEmpty(strActionName) == false)
                {
                    strUrl = strUrl + "?action=" + strActionName;
                }

                if (arrParam != null)
                {
                    string strParam = JsonFormatter.JsonSerialize(arrParam);
                    if (strParam != null)
                    {
                        strUrl = strUrl + "&params=" + strParam;
                    }
                }
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUrl);
                request.Method = "GET";
                //request.Proxy = null;
                request.Timeout = C_TimeOut;
                request.ContentType = "application/json";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                string strResult = sr.ReadToEnd();
                sr.Close();
                responseStream.Close();

                return strResult;
            }
            catch (Exception e)
            {
                WriteErrorMessage("CallMethodGet", e.Message);
            }
            return null;
        }

        public string CallMethodGet(string strWholeUrl)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strWholeUrl);
                request.Method = "GET";
                //request.Proxy = null;
                request.Timeout = C_TimeOut;
                request.ContentType = "application/json";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                string strResult = sr.ReadToEnd();
                sr.Close();
                responseStream.Close();

                return strResult;
            }
            catch (Exception e)
            {
                WriteErrorMessage("CallMethodGet", e.Message);
            }
            return null;
        }

        CookieContainer cookie = new CookieContainer();
        public string CallMethodPost(string strActionName, string[] arrParam)
        {
            try
            {
                //构造请求的URL
                string strUrl = BaseUrl;
                if (string.IsNullOrEmpty(strActionName) == false)
                {
                    strUrl = strUrl + "?action=" + strActionName;
                }
                //构造HTTP请求对象
                string strParam = "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUrl);
                //转成网络流
                if (arrParam != null)
                {
                    strParam = JsonFormatter.JsonSerialize(arrParam);
                }
                byte[] byteParam = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(strParam);
                //设置
                request.Method = "POST";
                //request.Proxy = null;
                request.Timeout = C_TimeOut;
                request.ContentType = "application/json";
                request.ContentLength = byteParam.Length;
                //request.MaximumAutomaticRedirections = 1;
                //request.AllowAutoRedirect = true;
                request.CookieContainer = cookie;
                //发送请求
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(byteParam, 0, byteParam.Length);
                requestStream.Flush();
                requestStream.Close();
                //获得接口返回值
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                response.Cookies = cookie.GetCookies(response.ResponseUri);
                Stream responseStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                string strResult = sr.ReadToEnd();
                sr.Close();
                responseStream.Close();

                return strResult;
            }
            catch (Exception e)
            {
                WriteErrorMessage("CallMethodPost", e.Message);
            }
            return null;
        }

        public string CallMethodPost(string strWholeUrl, string strParam)
        {
            try
            {
                //构造HTTP请求对象
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strWholeUrl);
                //转成网络流
                string[] arr = new string[1];
                arr[0] = strParam;
                string strData = "";
                if (arr != null)
                {
                    strData = JsonFormatter.JsonSerializeWithNull(arr);
                }
                byte[] byteParam = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(strData);
                //设置
                request.Method = "POST";
                //request.Proxy = null;
                request.Timeout = C_TimeOut;
                request.ContentType = "application/json";
                request.ContentLength = byteParam.Length;
                //request.MaximumAutomaticRedirections = 1;
                //request.AllowAutoRedirect = true;
                request.CookieContainer = cookie;
                //发送请求
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(byteParam, 0, byteParam.Length);
                requestStream.Flush();
                requestStream.Close();
                //获得接口返回值
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                response.Cookies = cookie.GetCookies(response.ResponseUri);
                Stream responseStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                string strResult = sr.ReadToEnd();
                sr.Close();
                responseStream.Close();

                return strResult;
            }
            catch (Exception e)
            {
                WriteErrorMessage("CallMethodPost", e.Message);
            }
            return null;
        }

        public string CallMethodPost_TC(string strActionName, string[] arrParam)
        {
            try
            {
                //构造请求的URL
                string strUrl = BaseUrl;
                ApiPostParamM objParam = new ApiPostParamM();
                objParam.action = strActionName;
                if (arrParam != null)
                {
                    objParam.Params = JsonConvert.SerializeObject(arrParam);
                }
                //构造HTTP请求对象
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUrl);
                //转成网络流
                string strParam = JsonFormatter.JsonSerialize(objParam);
                byte[] byteParam = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(strParam);
                //设置
                request.Method = "POST";
                //request.Proxy = null;
                request.Timeout = C_TimeOut;
                request.ContentType = "application/json";
                request.ContentLength = byteParam.Length;
                //request.MaximumAutomaticRedirections = 1;
                //request.AllowAutoRedirect = true;
                request.CookieContainer = cookie;
                //发送请求
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(byteParam, 0, byteParam.Length);
                requestStream.Flush();
                requestStream.Close();
                //获得接口返回值
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                response.Cookies = cookie.GetCookies(response.ResponseUri);
                Stream responseStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                string strResult = sr.ReadToEnd();
                sr.Close();
                responseStream.Close();

                return strResult;
            }
            catch (Exception e)
            {
                WriteErrorMessage("CallMethodPost", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 注意：由于调试发现不支持delete动词，所以此方法实际上还是发送get请求
        /// </summary>
        public string CallMethodPut(string strActionName, string[] arrParam)
        {
            try
            {
                //构造请求的URL
//                 string strUrl = BaseUrl;
//                 if (string.IsNullOrEmpty(strActionName) == false)
//                 {
//                     strUrl = strUrl + "?action=" + strActionName;
//                 }
                //构造HTTP请求对象
//                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUrl);
                //转成网络流
                //string strParam = JsonFormatter.JsonSerialize(arrParam);
                string strResult = CallMethodPost(strActionName, arrParam);


//                 byte[] byteParam = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(strParam);
//                 //设置
//                 request.Method = "PUT";
//                 request.Proxy = null;
//                 request.ContentLength = byteParam.Length;
//                 request.ContentType = "application/json";
//                 //myRequest.MaximumAutomaticRedirections = 1;
//                 //myRequest.AllowAutoRedirect = true;
//                 //发送请求
//                 Stream requestStream = request.GetRequestStream();
//                 requestStream.Write(byteParam, 0, byteParam.Length);
//                 requestStream.Flush();
//                 requestStream.Close();
//                 //获得接口返回值
//                 HttpWebResponse response = (HttpWebResponse)request.GetResponse();
//                 response.Cookies = cookie.GetCookies(response.ResponseUri);
//                 Stream responseStream = response.GetResponseStream();
//                 StreamReader sr = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
//                 string strResult = sr.ReadToEnd();
//                 sr.Close();
//                 responseStream.Close();

                return strResult;
            }
            catch (Exception e)
            {
                WriteErrorMessage("CallMethodPut", e.Message);
            }
            return null;
        }

        /// <summary>
        /// 注意：由于调试发现不支持delete动词，所以此方法实际上还是发送get请求
        /// </summary>
        public string CallMethodDelete(string strActionName, string[] arrParam)
        {
            try
            {
                string strResult = CallMethodGet(strActionName, arrParam);
                return strResult;

//                 string strUrl = BaseUrl;
//                 if (string.IsNullOrEmpty(strActionName) == false)
//                 {
//                     strUrl = strUrl + "?action=" + strActionName;
//                 }
// 
//                 if (arrParam != null)
//                 {
//                     string strParam = JsonFormatter.JsonSerialize(arrParam);
//                     if (strParam != null)
//                     {
//                         strUrl = strUrl + "&params=" + strParam;
//                     }
//                 }
// 
//                 HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUrl);
//                 request.Method = "DELETE";
//                 request.Proxy = null;
//                 request.ContentType = "application/json";
// 
//                 HttpWebResponse response = (HttpWebResponse)request.GetResponse();
//                 Stream responseStream = response.GetResponseStream();
//                 StreamReader sr = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
//                 string strResult = sr.ReadToEnd();
//                 sr.Close();
//                 responseStream.Close();
// 
//                 return strResult;
            }
            catch (Exception e)
            {
                WriteErrorMessage("CallMethodDelete", e.Message);
            }
            return null;
        }

        public string CallMethodGet_DXK(string strActionName, string[] arrParam, out int iErrorFlag)
        {
            DateTime dt1 = DateTime.Now;
            iErrorFlag = 8;
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream responseStream = null;
            try
            {
                string strUrl = BaseUrl;
                if (string.IsNullOrEmpty(strActionName) == false)
                {
                    strUrl = strUrl + "?method=" + strActionName;
                }

                if (arrParam != null)
                {
                    strUrl = strUrl + "&params=" + string.Join("|", arrParam);
                }

                WriteErrorMessage("【测试】CallMethodGet_DXK被调用了，地址为", strUrl);

                request = (HttpWebRequest)WebRequest.Create(strUrl);
                request.Method = "GET";
                request.Timeout = C_TimeOut;
                request.KeepAlive = false;
                //request.Proxy = null;
                request.ContentType = "application/json";            
                WriteErrorMessage("【测试】KeepAlive值为", request.KeepAlive.ToString());

                response = (HttpWebResponse)request.GetResponse();
                responseStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                string strResult = sr.ReadToEnd();
                sr.Close();
                responseStream.Close();

                string strErrorFlag = "8";
                string strData = "";
                if (strResult.StartsWith("{"))
                {
                    strResult = strResult.Substring(1);
                    int index = strResult.IndexOf(',');
                    strErrorFlag = strResult.Substring(0, index);
                    strErrorFlag = strErrorFlag.Substring(strErrorFlag.IndexOf(":") + 1);
                    strErrorFlag = strErrorFlag.Trim('"');
                    strData = strResult.Substring(index + 1);
                    strData = strData.Substring(0, strData.LastIndexOf('}'));
                    strData = strData.Substring(strData.IndexOf(":") + 1);
                }
                iErrorFlag = Convert.ToInt32(strErrorFlag);
                return strData;
            }
            catch (Exception e)
            {
                WriteErrorMessage("CallMethodGet_DXK", e.Message);
            }
            finally
            {
                if (request != null)
                {
                    request.Abort();
                    request = null;
                }
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
                if (responseStream != null)
                {
                    responseStream.Close();
                    responseStream = null;
                }
            }
            DateTime dt2 = DateTime.Now;
            TimeSpan ts = dt2.Subtract(dt1);
            WriteErrorMessage("【测试】CallMethodGet_DXK执行时间", ts.TotalMilliseconds.ToString());
            return null;
        }

        /// <summary>
        /// 电子资源阅览室V3.0专用
        /// </summary>
        /// <param name="strWholeUrl"></param>
        /// <returns></returns>
        public static T CallMethodGet_ERL3<T>(string strWholeUrl)
        {
            int ErrorCode;
            return CallMethodGet_ERL3<T>(strWholeUrl, out ErrorCode);
        }

        /// <summary>
        /// 电子资源阅览室专用（会返回错误码）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strWholeUrl"></param>
        /// <param name="ErrorCode">输出参数，错误码</param>
        /// <para>0-正常</para>
        /// <para>1-无数据</para>
        /// <para>2-系统状态不可用</para>
        /// <para>3-非法安全码</para>
        /// <para>4-非法参数</para>
        /// <para>5-操作失败</para>
        /// <returns></returns>
        public static T CallMethodGet_ERL3<T>(string strWholeUrl, out int ErrorCode)
        {
            if (string.IsNullOrEmpty(strWholeUrl))
            {
                ErrorCode = 4;
                return default(T);
            }
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream responseStream = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(strWholeUrl);
                request.Method = "GET";
                request.Timeout = C_TimeOut;
                request.KeepAlive = false;
                //request.Proxy = null;
                request.ContentType = "application/json";
                response = (HttpWebResponse)request.GetResponse();
                responseStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                string strResult = sr.ReadToEnd();
                sr.Close();
                responseStream.Close();

                LBD_WebApiInterface.Models.E_ResourceLibrary.ApiResultM<T> apiResult =
                    JsonFormatter.JsonDeserialize<LBD_WebApiInterface.Models.E_ResourceLibrary.ApiResultM<T>>(strResult);
                if (apiResult != null)
                {
                    ErrorCode = apiResult.error;
                    return apiResult.data;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("CallMethodGet_ERL3", e.Message);
            }
            finally
            {
                if (request != null)
                {
                    request.Abort();
                    request = null;
                }
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
                if (responseStream != null)
                {
                    responseStream.Close();
                    responseStream = null;
                }
            }
            ErrorCode = 5;
            return default(T);
        }

        private static void WriteErrorMessage(string strMethodName, string sErrorMessage)
        {
            DirectoryInfo clsPath = new DirectoryInfo(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory));
            DirectoryInfo clsSubPath = clsPath.CreateSubdirectory("Errlog\\LBD_WebApiInterface\\Api");

            if (clsSubPath.Exists)
            {
                DateTime clsDate = DateTime.Now;
                string strPath = clsSubPath.FullName + "\\CommandApi(" + String.Format("{0:yyyy-MM-dd}", clsDate) + ").log";
                StreamWriter clsWriter = new StreamWriter(strPath, true);
                clsWriter.WriteLine(String.Format("{0:HH:mm:ss}", clsDate) + " " + strMethodName + ": " + sErrorMessage);
                clsWriter.Close();
            }
        }
    }
}
