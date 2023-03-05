using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;

namespace LBD_WebApiInterface.Utility
{
    public class JsonFormatter
    {
        public static string JsonSerialize(object o)
        {
            try
            {
                return JsonConvert.SerializeObject(o, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception e)
            {
                WriteErrorMessage("JsonSerialize", e.Message);
            }
            return null;
        }

        public static string JsonSerializeWithNull(object o)
        {
            try
            {
                return JsonConvert.SerializeObject(o);
            }
            catch (Exception e)
            {
                WriteErrorMessage("JsonSerializeWithNull", e.Message);
            }

            return null;
        }

        public static T JsonDeserialize<T>(string strJson)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(strJson);
            }
            catch (Exception e)
            {
                WriteErrorMessage("JsonDeserialize", "Json=" + strJson + "。" + e.Message);
            }
            return default(T);
        }

        private static void WriteErrorMessage(string strMethodName, string sErrorMessage)
        {
            DirectoryInfo clsPath = new DirectoryInfo(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory));
            DirectoryInfo clsSubPath = clsPath.CreateSubdirectory("Errlog\\LBD_WebApiInterface\\Utility");

            if (clsSubPath.Exists)
            {
                DateTime clsDate = DateTime.Now;
                string strPath = clsSubPath.FullName + "\\JsonFormatter(" + String.Format("{0:yyyy-MM-dd}", clsDate) + ").log";
                StreamWriter clsWriter = new StreamWriter(strPath, true);
                clsWriter.WriteLine(String.Format("{0:HH:mm:ss}", clsDate) + " " + strMethodName + ": " + sErrorMessage);
                clsWriter.Close();
            }
        }

    }
}
