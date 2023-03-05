using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LBD_WebApiInterface.Utility
{
    public static class LogHelper
    {
        public static void WriteDebugInfo(string strMethodName, string strInfo)
        {
            try
            {
                DirectoryInfo clsPath = new DirectoryInfo(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory));
                DirectoryInfo clsSubPath = clsPath.CreateSubdirectory("RunningLog\\LBD_WebApiInterface\\Api");

                if (clsSubPath.Exists)
                {
                    DateTime clsDate = DateTime.Now;
                    string strPath = clsSubPath.FullName + "\\TeachInfoI(" + String.Format("{0:yyyy-MM-dd}", clsDate) + ").log";
                    StreamWriter clsWriter = new StreamWriter(strPath, true);
                    clsWriter.WriteLine(String.Format("{0:HH:mm:ss}", clsDate) + " " + strMethodName + ": " + strInfo);
                    clsWriter.Close();
                }
            }
            catch { }
        }

        public static void BD_WriteDebugInfo(string strMethodName, string strInfo)
        {
            try
            {
                DirectoryInfo clsPath = new DirectoryInfo(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory));
                DirectoryInfo clsSubPath = clsPath.CreateSubdirectory("RunningLog");

                if (clsSubPath.Exists)
                {
                    DateTime clsDate = DateTime.Now;
                    string strPath = clsSubPath.FullName + "\\大数据测试(" + String.Format("{0:yyyy-MM-dd}", clsDate) + ").log";
                    StreamWriter clsWriter = new StreamWriter(strPath, true);
                    clsWriter.WriteLine("【" + String.Format("{0:HH:mm:ss}", clsDate) + "】 【" + strMethodName + "】 " + strInfo);
                    clsWriter.Close();
                }
            }
            catch { }
        }

        public static void WriteErrorMessage(string strMethodName, string strErrorMessage)
        {
            try
            {
                DirectoryInfo clsPath = new DirectoryInfo(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory));
                DirectoryInfo clsSubPath = clsPath.CreateSubdirectory("Errlog\\LBD_WebApiInterface\\Api");

                if (clsSubPath.Exists)
                {
                    DateTime clsDate = DateTime.Now;
                    string strPath = clsSubPath.FullName + "\\TeachInfoI(" + String.Format("{0:yyyy-MM-dd}", clsDate) + ").log";
                    StreamWriter clsWriter = new StreamWriter(strPath, true);
                    clsWriter.WriteLine(String.Format("{0:HH:mm:ss}", clsDate) + " " + strMethodName + ": " + strErrorMessage);
                    clsWriter.Flush();
                    clsWriter.Close();
                }
            }
            catch { }
        }
      public static void WriteTrackLog(string strMethodName, string strErrorMessage)
        {
            try
            {
                DirectoryInfo clsPath = new DirectoryInfo(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory));
                DirectoryInfo clsSubPath = clsPath.CreateSubdirectory("Tracklog\\LBD_WebApiInterface");

                if (clsSubPath.Exists)
                {
                    DateTime clsDate = DateTime.Now;
                    string strPath = clsSubPath.FullName + "\\TeachInfoI(" + String.Format("{0:yyyy-MM-dd}", clsDate) + ").log";
                    StreamWriter clsWriter = new StreamWriter(strPath, true);
                    clsWriter.WriteLine(String.Format("{0:HH:mm:ss}", clsDate) + " " + strMethodName + ": " + strErrorMessage);
                    clsWriter.Flush();
                    clsWriter.Close();
                }
            }
            catch { }
        }
    }
}
