using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace LBD_WebApiInterface.Utility
{
    public class CDirNameHelper
    {
        /// <summary>
        /// 判断该目录为是否属于年级
        /// </summary>
        /// <param name="dirName">目录名称</param>
        /// <returns></returns>
        private bool IsClassDir(string dirName)
        {
            try
            {
                if (String.IsNullOrEmpty(dirName))
                {
                    return false;
                }

                if (dirName.Contains("年级") || dirName.ToUpper().Contains("GRADE"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception err)
            {
                WriteErrorMessage("IsClassDir:" + err.Message);
                return false;
            }
        }

        /// <summary>
        /// 判断该目录是否属于单元
        /// </summary>
        /// <param name="dirName">目录名称</param>
        /// <returns></returns>
        private bool IsUnitDir(string dirName)
        {
            try
            {
                if (String.IsNullOrEmpty(dirName))
                {
                    return false;
                }

                if (dirName.Contains("单元")
                    || dirName.ToUpper().Contains("章")
                    || dirName.ToUpper().Contains("UNIT")
                    || dirName.ToUpper().Contains("CHAPTER")
                    || dirName.ToUpper().Contains("SECTION")
                    || dirName.ToUpper().Contains("BOOK")
                    || dirName.ToUpper().Contains("SEAL")
                    || dirName.ToUpper().Contains("BADGE"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception err)
            {
                WriteErrorMessage("IsUnitDir:" + err.Message);
                return false;
            }
        }

        private bool IsTextDir(string dirName)
        {
            try
            {
                if (String.IsNullOrEmpty(dirName))
                {
                    return false;
                }

                if (dirName.Contains("课文")
                    || dirName.ToUpper().Contains("Lesson"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception err)
            {
                WriteErrorMessage("IsTextDir:" + err.Message);
                return false;
            }
        }

        /// <summary>
        /// 判断两个目录结构是否相同
        /// </summary>
        /// <param name="dirInfo1">第一个目录信息</param>
        /// <param name="firInfo2">第二个目录信息</param>
        /// <returns></returns>
        private bool IsDirInfoIdentical(DirectoryInfo dirInfo1, DirectoryInfo dirInfo2)
        {
            try
            {
                DirectoryInfo[] dirInfos1 = dirInfo1.GetDirectories();
                DirectoryInfo[] dirInfos2 = dirInfo2.GetDirectories();

                if (dirInfos1.Length != dirInfos2.Length)
                {
                    return false;
                }

                foreach (DirectoryInfo di1 in dirInfos1)
                {
                    bool hasSame = false;

                    foreach (DirectoryInfo di2 in dirInfos2)
                    {
                        if (di2.Name == di1.Name)
                        {
                            hasSame = true;
                            break;
                        }
                    }

                    if (!hasSame)
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception err)
            {
                WriteErrorMessage("IsDirInfoIdentical:" + err.Message);
                return false;
            }
        }

        /// <summary>
        /// 根据本地素材目录获取能与数据库匹配的目录结构
        /// </summary>
        /// <param name="localDir">本地素材目录相对路径路径（不包含根目录）</param>
        /// <returns>返回null则目录不符合规范，能与数据库匹配的对应目录结构</returns>
        public string GetMatchedDirByLocalDir(string localDir)
        {
            try
            {
                if (String.IsNullOrEmpty(localDir))
                {
                    return null;
                }

                string[] sArray = localDir.Split('\\');
                string matchDir = String.Empty;

                if (sArray.Length == 1)
                {
                    return localDir;
                }
                else
                {
                    if (IsClassDir(sArray[0]))
                    {
                        matchDir = matchDir + "年级";

                        if (sArray.Length == 2)
                        {
                            return matchDir + "\\" + sArray[1];
                        }
                        else
                        {
                            if (IsUnitDir(sArray[1]))
                            {
                                matchDir = matchDir + "\\单元";

                                if (sArray.Length == 3)
                                {
                                    return matchDir + "\\" + sArray[2];
                                }
                                else
                                {
                                    if (IsTextDir(sArray[2]))
                                    {
                                        matchDir = matchDir + "\\课文";

                                        if (sArray.Length == 4)
                                        {
                                            return matchDir + "\\" + sArray[3];
                                        }
                                        else
                                        {
                                            return localDir;
                                        }
                                    }
                                    else
                                    {
                                        return localDir;
                                    }
                                }
                            }
                            else
                            {
                                return localDir;
                            }
                        }
                    }
                    else
                    {
                        return localDir;
                    }
                }
            }
            catch (Exception err)
            {
                WriteErrorMessage("GetMatchedDirByLocalDir:" + err.Message);
                return null;
            }
        }

        #region Write log

        private void WriteErrorMessage(string sErrorMessage)
        {
            DirectoryInfo clsPath = new DirectoryInfo(Path.GetDirectoryName(Application.ExecutablePath));
            DirectoryInfo clsSubPath = clsPath.CreateSubdirectory("Errlog\\LBD_WebApiInterface");

            if (clsSubPath.Exists)
            {
                DateTime clsDate = DateTime.Now;
                string strPath = clsSubPath.FullName + "\\CDirNameHelper(" + String.Format("{0:yyyy-MM-dd}", clsDate) + ").log";
                StreamWriter clsWriter = new StreamWriter(strPath, true);
                clsWriter.WriteLine(String.Format("{0:HH:mm:ss}", clsDate) + " " + sErrorMessage);
                clsWriter.Close();
            }
        }


        #endregion
    }
}
