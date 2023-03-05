using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using LBD_WebApiInterface.Models.CloudPlatform;
using LBD_WebApiInterface.Utility;

namespace LBD_WebApiInterface.Utility
{
    /// <summary>
    /// 调用WebApi的辅助类
    /// </summary>
    public class CallApiHelper
    {
        private const int C_Timeout = 8000;

        /// <summary>
        /// 以Get方式调用WebApi，出错时会抛出异常（例如URL不规范、超时等）
        /// </summary>
        /// <param name="strWholeUrl">访问WebApi的完整路径</param>
        /// <returns>WebApi的返回值，未做处理的的字符串格式</returns>
        public static string CallMethodGet(string strWholeUrl)
        {
          
            
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(strWholeUrl);
                request.Method = "GET";
                //request.Proxy = null;
                request.Timeout = C_Timeout;
                request.ContentType = "application/json";

                response = (HttpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                string strResult = sr.ReadToEnd();
                sr.Close();
                responseStream.Close();

                return strResult;
            }
            catch (Exception e)
            {
                throw e;
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
            }
        }

        /// <summary>
        /// 以Get方式调用WebApi，出错时会抛出异常（例如URL不规范、超时等）
        /// </summary>
        /// <param name="strWholeUrl">访问WebApi的完整路径</param>
        /// <returns>WebApi的返回值，未做处理的的字符串格式</returns>
        public static string CallMethod_Get(string strWholeUrl)
        {


            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(strWholeUrl);
                request.Method = "GET";
                //request.Proxy = null;
                request.Timeout = 15000;
                request.ContentType = "application/xml";

                response = (HttpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                string strResult = sr.ReadToEnd();
                sr.Close();
                responseStream.Close();

                return strResult;
            }
            catch (Exception e)
            {
                throw e;
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
            }
        }
        /// <summary>
        /// 以Get方式调用WebApi，出错时会抛出异常（例如URL不规范、超时等）
        /// </summary>
        /// <param name="strWholeUrl">访问WebApi的完整路径</param>
        /// <returns>WebApi的返回值，未做处理的的字符串格式</returns>
        public static string CallMethod_Get1(string strWholeUrl)
        {


            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(strWholeUrl);
                request.Method = "GET";
                //request.Proxy = null;
                request.Timeout = 120000;
                request.ContentType = "application/xml";

                response = (HttpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                string strResult = sr.ReadToEnd();
                sr.Close();
                responseStream.Close();

                return strResult;
            }
            catch (Exception e)
            {
                throw e;
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
            }
        }

        /// <summary>
        /// 以Post方式调用WebApi，出错时会抛出异常（例如URL不规范、超时等）
        /// </summary>
        /// <param name="strWholeUrl">访问WebApi的完整路径</param>
        /// <param name="strParam">WebApi所需参数，会写到消息体中</param>
        /// <returns>WebApi的返回值，未做处理的的字符串格式</returns>
        public static string CallMethodPost(string strWholeUrl, string strParam)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            CookieContainer cookieCon = new CookieContainer();
            try
            {
                //构造请求的URL
                if (string.IsNullOrEmpty(strWholeUrl))
                {
                    return "";
                }
                //构造HTTP请求对象
                request = (HttpWebRequest)WebRequest.Create(strWholeUrl);
                //转成网络流
                if (strParam == null)
                {
                    strParam = "";
                }
                byte[] byteParam = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(strParam);
                //设置
                request.Method = "POST";
                //request.Proxy = null;
                request.Timeout = C_Timeout;
                request.ContentType = "application/json";
                request.ContentLength = byteParam.Length;
                request.CookieContainer = cookieCon;
                //发送请求
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(byteParam, 0, byteParam.Length);
                requestStream.Flush();
                requestStream.Close();
                //获得接口返回值
                response = (HttpWebResponse)request.GetResponse();
                response.Cookies = cookieCon.GetCookies(response.ResponseUri);
                Stream responseStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                string strResult = sr.ReadToEnd();
                sr.Close();
                responseStream.Close();

                return strResult;
            }
            catch (Exception e)
            {
                
                throw e;
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
                if (cookieCon == null)
                {
                    cookieCon = null;
                }
            }
        }

        public static string CallMethod_Post(string strWholeUrl, string strParam)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                //构造请求的URL
                if (string.IsNullOrEmpty(strWholeUrl))
                {
                    return "";
                }
                //构造HTTP请求对象
                request = (HttpWebRequest)WebRequest.Create(strWholeUrl);
                //转成网络流
                if (strParam == null)
                {
                    strParam = "";
                }
                byte[] byteParam = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(strParam);
                //设置
                request.Method = "POST";
                //request.Proxy = null;
                request.Timeout = C_Timeout;
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteParam.Length;
                //发送请求
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(byteParam, 0, byteParam.Length);
                requestStream.Flush();
                requestStream.Close();
                //获得接口返回值
                response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                string strResult = sr.ReadToEnd();
                sr.Close();
                responseStream.Close();
                return strResult;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        ///  以Get方式调用WebApi
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strWholeUrl"></param>
        /// <param name="iErrorFlag"></param>
        /// <returns></returns>
        public static T CallMethodGet_Cloud<T>(string strWholeUrl, out int iErrorFlag)
        {
            iErrorFlag = 0;
            T objReturn = default(T);
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(strWholeUrl);
                request.Method = "GET";
                //request.Proxy = null;
                request.Timeout = C_Timeout;
                request.ContentType = "application/json";

                response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                string strResult = sr.ReadToEnd();
                sr.Close();
                responseStream.Close();

                if (string.IsNullOrEmpty(strResult))
                {
                    iErrorFlag = 4;
                    objReturn = default(T);
                }
                else
                {
                    strResult = Uri.UnescapeDataString(strResult);
                    
                    CloudApiResultM<T> apiResult = JsonFormatter.JsonDeserialize<CloudApiResultM<T>>(strResult);
                    iErrorFlag = apiResult.error;
                    objReturn = apiResult.data;
                }

                return objReturn;
            }
            catch (Exception e)
            {
                iErrorFlag = 4;
                throw e;
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
            }
        }
    }
}
