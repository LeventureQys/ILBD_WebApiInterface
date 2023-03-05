using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using LBD_TeachCenterModel;
using System.Reflection;

namespace LBD_WebApiInterface.Utility
{
    public class UtilityClass
    {

        //写文件
        public static bool WriteStringToFile(string strFilePath, string strStringToWrite, Encoding encoding)
        {
            StreamWriter sw = null;
            try
            {
                if (string.IsNullOrEmpty(strFilePath))
                {
                    return false;
                }

                if (strStringToWrite == null)
                {
                    strStringToWrite = "";
                }

                int lastIndex = strFilePath.LastIndexOf('\\');
                if (lastIndex > -1)
                {
                    string directory = strFilePath.Substring(0, lastIndex + 1);

                    if (string.IsNullOrEmpty(directory) == false)
                    {
                        if (Directory.Exists(directory) == false)
                        {
                            Directory.CreateDirectory(directory);
                        }
                    }
                }

                if (encoding == null)
                {
                    encoding = Encoding.Default;
                }

                sw = new StreamWriter(strFilePath, false, encoding);
                sw.Write(strStringToWrite);
                sw.Flush();
                sw.Close();
                sw = null;

                return true;
            }
            catch (Exception e)
            {
                WriteErrorMessage("WriteStringToFile", e.Message);
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }

            return false;
        }

        public static int AnalyseCloudJson(ref string strJson)
        {
            try
            {
                string strErrorFlag = "";
                string strData = "";
                if (strJson.StartsWith("{"))
                {
                    strJson = strJson.Substring(1);
                    int index = strJson.IndexOf(',');
                    strErrorFlag = strJson.Substring(0, index);
                    strErrorFlag = strErrorFlag.Substring(strErrorFlag.IndexOf(":") + 1);
                    strErrorFlag = strErrorFlag.Trim('"');
                    strData = strJson.Substring(index + 1);
                    strData = strData.Substring(0, strData.LastIndexOf('}'));
                    strData = strData.Substring(strData.IndexOf(":") + 1);
                }
                strJson = strData;
                strJson = Uri.UnescapeDataString(strJson);
                if (string.IsNullOrEmpty(strErrorFlag) == false)
                {
                    int iFlag = Convert.ToInt32(strErrorFlag);
                    return iFlag;
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("AnalyseCloudJson", e.Message);
            }
            return -1;
        }
        public static string serialObject(object obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] propertys = type.GetProperties();
            string[] propertyArr = new string[propertys.Length];
            for (int i = 0; i < propertys.Length; i++)
            {
                string propertyName = propertys[i].Name;
                string propertyValue = string.Empty;
                if (propertys[i].GetValue(obj,null) != null)
                    propertyValue =Uri.EscapeUriString(propertys[i].GetValue(obj,null).ToString());
                propertyArr[i] = propertyName + "=" + propertyValue;
            }
            return string.Join("&", propertyArr);
        }
        public static T AnalyseTeachCenterJson<T>(string strJson)
        {
            try
            {
                //先解密
                //lgTest_Base64Zip.ClsMainClass cls = new lgTest_Base64Zip.ClsMainClass();
                //str = cls.UNZipAndBase64(str);

                ApiResultM<T> apiResult = JsonFormatter.JsonDeserialize<ApiResultM<T>>(strJson);
                if (apiResult != null)
                {
                    return apiResult.Data;
                }
                else
                {
                    return default(T);
                }
            }
            catch (Exception e)
            {
                WriteErrorMessage("AnalyseTeachCenterJson", e.Message);
                return default(T);
            }
        }

        public static T AnalyseTeachCenterJson<T>(string strJson, out int iErrorFlag,out string strErrorMsg)
        {
            iErrorFlag = 0;
            strErrorMsg = "";
            try
            {
                //先解密
                //lgTest_Base64Zip.ClsMainClass cls = new lgTest_Base64Zip.ClsMainClass();
                //str = cls.UNZipAndBase64(str);

                ApiResultM<T> apiResult = JsonFormatter.JsonDeserialize<ApiResultM<T>>(strJson);
                if (apiResult != null)
                {
                    iErrorFlag = apiResult.ErrorFlag;
                    strErrorMsg = apiResult.Message;
                    return apiResult.Data;
                }
                else
                {
                    iErrorFlag = 1;
                    return default(T);
                }
            }
            catch(Exception e)
            {
                WriteErrorMessage("AnalyseTeachCenterJson", e.Message);
                iErrorFlag = 1;
                return default(T);
            }
        }

        private static void WriteErrorMessage(string strMethodName, string sErrorMessage)
        {
            DirectoryInfo clsPath = new DirectoryInfo(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory));
            DirectoryInfo clsSubPath = clsPath.CreateSubdirectory("Errlog\\LBD_NetTeachApiF\\Utility");

            if (clsSubPath.Exists)
            {
                DateTime clsDate = DateTime.Now;
                string strPath = clsSubPath.FullName + "\\UtilityClass(" + String.Format("{0:yyyy-MM-dd}", clsDate) + ").log";
                StreamWriter clsWriter = new StreamWriter(strPath, true);
                clsWriter.WriteLine(String.Format("{0:HH:mm:ss}", clsDate) + " " + strMethodName + ": " + sErrorMessage);
                clsWriter.Close();
            }
        }

    }
}
