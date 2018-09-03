using System.Text;
using UnityEngine ;

namespace PlatformCore
{
    public class CorePathDefine : EventDispatcherMono , ICore
    {
        protected static string PRE_HASH = "";
        private static int PRE_HASH_LEN = 0;
        private StringBuilder mSBuilder = new StringBuilder();

        /// <summary>
        /// 打包资源统一扩展名
        /// </summary>
        public string U3DExt{get { return ".unity3d"; }}

        /// <summary>
        /// 持久化路径
        /// </summary>
        public string PersistentDataPath { get { return Application.persistentDataPath; } }

        /// <summary>
        /// 保内资源路径
        /// </summary>
        public string StreamingAssetsPath { get { return Application.streamingAssetsPath; } }

        /// <summary>
        /// 版本hash文件统一名称
        /// </summary>
        public string VHashFileName { get { return "v.txt"; } }

        //平台名
        protected string mPlatformName = null;
        public string PlatformName
        {
            get
            {
                if (string.IsNullOrEmpty(mPlatformName) == false)
                {
                    return mPlatformName;
                }
                mPlatformName = "Android";
#if UNITY_EDITOR
                switch (UnityEditor.EditorUserBuildSettings.activeBuildTarget)
                {
                    case UnityEditor.BuildTarget.iOS:
                        mPlatformName = "iOS";
                        break;
                    case UnityEditor.BuildTarget.Android:
                        mPlatformName = "Android";
                        break;
                }
#endif
                switch (Application.platform)
                {
                    case RuntimePlatform.Android:
                        mPlatformName = "Android";
                        break;
                    case RuntimePlatform.IPhonePlayer:
                        mPlatformName = "iOS";
                        break;
                }

                return mPlatformName;
            }
        }
        
        public CorePathDefine()
        {
            
        }
        public static string GetLocalHttpPrefix()
        {
            string prefix = "file://";
            if (Application.isEditor)
            {
                prefix = "file:///";
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
                prefix = "file://";
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                prefix = "file://";
            }
            return prefix;
        }

        public string GetRemotePath(string url_ = "")
        {
            mSBuilder.Length = 0;
            mSBuilder.Append(Core.ShellConfig.UpdateServerUrl);
            if(string.IsNullOrEmpty(url_) == false && url_[0] != '/')
                mSBuilder.Append("/");
            mSBuilder.Append(url_);

            return mSBuilder.ToString();
        }

        public string GetPersistentPath(string url_ = "", bool isWWW_ = false)
        {
            string prefix = "";
            if (isWWW_ == true)
            {
                prefix = GetLocalHttpPrefix();
            }
            mSBuilder.Length = 0;
            mSBuilder.Append(prefix);
            mSBuilder.Append(PersistentDataPath);
            //mSBuilder.Append("/");
            mSBuilder.Append(url_);

            return mSBuilder.ToString();
        }
        public string GetStreamingAssetsPath(string url_ = "", bool isWWW_ = false)
        {
            string prefix = "";
            if (isWWW_ == true)
            {
                prefix = "file://";
                if (Application.isEditor)
                {
                    prefix = "file://";
                }
                else if (Application.platform == RuntimePlatform.Android)
                {
                    prefix = "";
                }
                else if (Application.platform == RuntimePlatform.IPhonePlayer)
                {
                    prefix = "file://";
                }
            }

            mSBuilder.Length = 0;
            mSBuilder.Append(prefix);
            mSBuilder.Append(StreamingAssetsPath);
            mSBuilder.Append("/");
            mSBuilder.Append(url_);

            return mSBuilder.ToString();
        }
        /// <summary>
        /// 取得远程文件在本地的映射路径
        /// </summary>
        /// <param name="url">远程文件路径</param>
        /// <param name="isSubfix">是否只含后缀(不是完整路径)</param>
        /// <returns></returns>
        public string GetLocalPathByURL(string url_, bool isSubfix_ = false)
        {
            string localPath = "";
            int index = url_.IndexOf(PRE_HASH);
            if (index != -1)
            {
                localPath = url_.Substring(index + PRE_HASH_LEN);
                localPath = Core.PathDefine.formatedLocalURL(localPath);

                if (isSubfix_ == false)
                {
                    localPath = Core.PathDefine.GetPersistentPath(localPath);
                }
            }
            return localPath;
        }
        /// <summary>
        /// 本地路径 格式化(主要去掉服务器?后参数)
        /// </summary>
        /// <param name="localPath"></param>
        /// <returns></returns>
        protected virtual string formatedLocalURL(string localPath_)
        {
            int index = localPath_.IndexOf('?');
            if (index == -1)
            {
                return localPath_;
            }
            return localPath_.Substring(0, index);
        }
    }
}
