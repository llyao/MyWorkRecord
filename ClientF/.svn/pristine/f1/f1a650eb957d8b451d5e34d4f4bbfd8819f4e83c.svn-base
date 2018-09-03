using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Networking;

namespace PlatformCore
{
    internal class SimpleLoader
    {
        public Action<byte[], string> callBackAction;
        public Action<float> progressAction;
        public int timeOut = -1;
        private string url;
        private UnityWebRequest request;

        public SimpleLoader(string url)
        {
            this.url = url;
        }

        public IEnumerator load()
        {
            float startTime = Time.realtimeSinceStartup;
            request = UnityWebRequest.Get(url);
            if (timeOut > 0)
            {
                request.timeout = timeOut;
            }

            request.Send();

            while (!request.isDone)
            {
                if (progressAction != null)
                {
                    float progress = request.downloadProgress;
                    progressAction(progress < 0 ? 0 : progress);
                }

                yield return null;
            }

            string error = request.error;

            if (string.IsNullOrEmpty(error))
            {
                Debug.Log(url + "\tcomplete");
                callBackAction(request.downloadHandler.data, null);
            }
            else
            {
                Debug.Log(url + "\terror:" + error);
                callBackAction(null, error);
            }

            request.Dispose();
            request = null;
        }
    }

    public class ShellApp:MonoBehaviour
    {
        protected string mHotDllName = "coreDll.dll";

        void Start()
        {
            loadWWW("", remoteDllCallback, 5);
        }

        /// <summary>
        /// 加载远程DLL
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="error"></param>
        private void remoteDllCallback(byte[] bytes, string error)
        {
            if (string.IsNullOrEmpty(error) == false)
            {
                Debug.Log("加载远程DLL错误:" + error);
                loadLocalDll();
                return;
            }

            try
            {
                string filePath = Application.persistentDataPath + "/" + mHotDllName + ".dll";
                if (File.Exists(filePath))
                    File.Delete(filePath);

                using (FileStream stream = new FileStream(filePath, FileMode.CreateNew))
                {//保存更新好的dll
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Flush();
                }

                string hash = MD5Byte9(bytes);
                Debug.Log("生成dll hash:" + hash);
                PlayerPrefs.SetString(mHotDllName + "_SHELL", hash);
            }
            catch (Exception e)
            {
                Debug.Log("远程DLL有问题，直接加载本地dll:" + e.Message);
                loadLocalDll();
                return;
            }

            startGameApp(bytes);
        }

        private void loadLocalDll()
        {
            ///加载本地(要转换为Http形式);
            string dllURL = Application.persistentDataPath + "/" + mHotDllName + ".dll";
            if (File.Exists(dllURL))
            {
                loadWWW(dllURL, localDllCallback);
                return;
            }
            //如果本地不存在,从安装包内加载;
            loadPackageDll();
        }

        /// <summary>
        /// 加载远程资源
        /// </summary>
        /// <param name="url"></param>
        /// <param name="remoteDllCallback"></param>
        /// <param name="timeOut"></param>
        /// <param name="progressHandle"></param>
        /// <returns></returns>
        private SimpleLoader loadWWW(string url, Action<byte[], string> remoteDllCallback, int timeOut = -1, Action<float> progressHandle = null)
        {
            timeOut = 60 * 10;
            SimpleLoader loader = new SimpleLoader(url);
            loader.callBackAction = remoteDllCallback;
            if (progressHandle == null)
            {
                progressHandle = progressAction;
            }
            if (timeOut != -1)
            {
                loader.timeOut = timeOut;
            }
            loader.progressAction = progressHandle;
            StartCoroutine(loader.load());
            return loader;
        }

        /// <summary>
        /// 处理进度条
        /// </summary>
        /// <param name="value"></param>
        private void progressAction(float value)
        {
        }

        /// <summary>
        /// 挂在游戏脚本
        /// </summary>
        /// <param name="bytes"></param>
        private void startGameApp(byte[] bytes)
        {
            Type type = null;
            Type baseType = null;
            try
            {
                Assembly assembly = Assembly.Load(bytes);
                type = assembly.GetType("GameApp");
                baseType = assembly.GetType("PlatformCore.CoreApp");
                if (type == null)
                {
                    Debug.Log("dll中没有找到GameApp");
                    type = Type.GetType("GameApp");
                }
                if (baseType == null)
                {
                    baseType = Type.GetType("PlatformCore.CoreApp");
                }
            }
            catch (Exception ex)
            {
                Debug.Log("无法加载远程DLL:" + ex.Message + "  开始加载包内的dll");
                loadPackageDll();
                return;
            }

            if (type != null)
            {
                Debug.Log("动态加载DLL成功，开始绑定GameApp");
                gameObject.AddComponent(type);
            }
            else
            {
                Debug.Log("找不到GameApp");
            }
        }

        /// <summary>
        /// 从包内加载dll
        /// </summary>
        private void loadPackageDll()
        {
            //            string dllURL = ShellHelper.getStreamingAssetsLocal(gameDllName + ".dll", true);
            //            loadWWW(dllURL, localDllCallback);
        }

        private void localDllCallback(byte[] bytes, string error)
        {
            if (string.IsNullOrEmpty(error) == false)
            {
                Debug.Log("找不到主程序!!!" + error);
                return;
            }

            startGameApp(bytes);
        }

        /// <summary>
        /// 生成md5
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string MD5Byte9(byte[] buffer)
        {
            string strResult = "";
            string strHashData = "";
            byte[] arrbytHashValue;
            MD5CryptoServiceProvider oMD5Hasher =
                new MD5CryptoServiceProvider();
            try
            {
                arrbytHashValue = oMD5Hasher.ComputeHash(buffer); //计算指定Stream 对象的哈希值  
                //由以连字符分隔的十六进制对构成的String，其中每一对表示value 中对应的元素；例如“F-2C-4A”  
                strHashData = BitConverter.ToString(arrbytHashValue);
                //替换-  
                strHashData = strHashData.Replace("-", "");
                strResult = strHashData.ToLower().Substring(0, 9);
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
            }

            return strResult;
        }
    }
}
