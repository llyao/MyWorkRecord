using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine ;

namespace PlatformCore
{
    public class FileFunc
    {
        public static byte[] GetBytes(string fullPath)
        {
            if (File.Exists(fullPath) == false)
            {
                return null;
            }
            FileStream fs = File.OpenRead(fullPath);
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            return bytes;
        }
        public static string GetUTF8Text(string fullPath)
        {
            if (File.Exists(fullPath) == false)
            {
                return "";
            }
            FileStream fs = File.OpenRead(fullPath);
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            string v = Encoding.UTF8.GetString(bytes);
            fs.Close();

            return v;
        }
        
        public static void AutoCreateDirectory(string fullPath)
        {
            FileInfo fi = new FileInfo(fullPath);
            DirectoryInfo di = fi.Directory;
            if (di.Exists == false)
            {
                Directory.CreateDirectory(di.FullName);
            }
        }
        public static List<string> FindFile(string dirPath, List<string> exNameArr)
        {
            return FindFile(dirPath, exNameArr.ToArray());
        }

        public static List<string> FindFile(string dirPath, string[] exNameArr, SearchOption option = SearchOption.AllDirectories)
        {
            List<string> list = new List<string>();
            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);

            if (dirInfo.Exists == false)
            {
                return list;
            }

            //FileInfo[] fileInfoArr = new FileInfo[] { };

            for (int i = 0; i < exNameArr.Length; i++)
            {
                FileInfo[] fileInfoArr2 = dirInfo.GetFiles(exNameArr[i], option);

                foreach (FileInfo filePath in fileInfoArr2)
                {
                    string fileFullPath = filePath.FullName.Replace("\\", "/");
                    list.Add(fileFullPath);
                }
            }

            return list;
        }

        public static DirectoryInfo[] FinDirectory(string dirPath)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
            if (dirInfo.Exists == false)
            {
                Debug.Log(dirPath + ":不存在");
                return new DirectoryInfo[0];
            }
            return dirInfo.GetDirectories();
        }
        public static void Copy(string sourceFileName, string destFileName, bool overrideIt = true)
        {
            if (overrideIt == false)
            {
                if (File.Exists(destFileName) == true)
                {
                    return;
                }
            }
            AutoCreateDirectory(destFileName);
            File.Copy(sourceFileName, destFileName, overrideIt);
        }
        public static void CopyDirectory(string sourcePath, string destPath)
        {
            if (!Directory.Exists(destPath))
            {
                Directory.CreateDirectory(destPath);
            }
            DirectoryInfo dir = new DirectoryInfo(sourcePath);
            //先复制文件
            CopyFile(dir, destPath);
            //再复制子文件夹
            DirectoryInfo[] dirs = dir.GetDirectories();
            if (dirs.Length > 0)
            {
                foreach (DirectoryInfo temDirectoryInfo in dirs)
                {
                    string sourceDirectoryFullName = temDirectoryInfo.FullName;
                    string destDirectoryFullName = sourceDirectoryFullName.Replace(sourcePath, destPath);
                    if (!Directory.Exists(destDirectoryFullName))
                    {
                        Directory.CreateDirectory(destDirectoryFullName);
                    }
                    CopyFile(temDirectoryInfo, destDirectoryFullName);
                    CopyDirectory(sourceDirectoryFullName, destDirectoryFullName);
                }
            }
        }

        /// <summary>
        /// 拷贝目录下的所有文件到目的目录。
        /// </summary>
        /// <param >源路径</param>
        /// <param >目的路径</param>
        private static void CopyFile(DirectoryInfo path, string desPath)
        {
            string sourcePath = path.FullName;
            FileInfo[] files = path.GetFiles();
            foreach (FileInfo file in files)
            {
                string sourceFileFullName = file.FullName;
                string destFileFullName = sourceFileFullName.Replace(sourcePath, desPath);
                file.CopyTo(destFileFullName, true);
            }
        }

        public static string GetDirectory(string path, bool isFull = true)
        {

            FileInfo fileInfo = new FileInfo(path);

            if (isFull)
            {
                return fileInfo.Directory.FullName;
            }

            return fileInfo.DirectoryName;
        }

        public static void SaveBytes(byte[] bytes, string pathName_)
        {
            AutoCreateDirectory(pathName_);
            try
            {
                File.WriteAllBytes(pathName_, bytes);
            }
            catch (Exception ex)
            {
                Debug.Log("saveBytesError:" + ex.Message);
            }
        }

        public static string FileNameFormat(string fileName)
        {
            fileName = fileName.Replace(" ", "");
            fileName = fileName.Replace('#', '_');
            fileName = fileName.Replace('@', '_');
            fileName = fileName.Replace('(', '_');
            fileName = fileName.Replace(')', '_');
            return fileName;
        }

        public static Texture2D LoadTexture2DFromFile(string pathName_)
        {
            byte[] bytes = File.ReadAllBytes(pathName_);
            Texture2D tex = new Texture2D(2, 2, TextureFormat.ARGB32, false, false);
            tex.LoadImage(bytes);
            return tex;
        }
        public static void SaveConfig(object vo, string fullPath, bool deflate = true)
        {
//            AutoCreateDirectory(fullPath);
//            ByteArray bytesArray = new ByteArray();
//            bytesArray.WriteObject(vo);
//            if (deflate)
//            {
//                bytesArray.Deflate();
//            }
//            byte[] bytes = bytesArray.ToArray();
//
//            File.WriteAllBytes(fullPath, bytes);
        }

        public static void SaveObject(object obj_, string pathName_)
        {
            string str = JsonUtility.ToJson(obj_);
            byte[] buf = Encoding.UTF8.GetBytes(str);
            SaveBytes(buf , pathName_);
        }

        public static T LoadObject<T>(string pathName_)
        {
            string str = GetUTF8Text(pathName_);
            T obj = JsonUtility.FromJson<T>(str);
            return obj;
        }

        public static string GetNameInPathname(string pathName_)
        {
            if (string.IsNullOrEmpty(pathName_))
            {
                return pathName_;
            }
            string[] names = pathName_.Split('/');
            string name = names[names.Length - 1];
            names = name.Split('.');
            return names[0];
        }
    }
}
