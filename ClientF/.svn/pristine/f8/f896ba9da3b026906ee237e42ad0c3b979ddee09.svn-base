using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PlatformCore
{
    public class CoreLoaderManager : EventDispatcherMono, ICore
    {
        protected Dictionary<string , bool> mUrl404 = new Dictionary<string, bool>();
        public void SetUrl404(string url_)
        {
            mUrl404[url_] = true;
        }

        //正在加载中的加载器
        private Dictionary<string, Loader> mLoadingPool = new Dictionary<string,Loader>();

        public int LoadRetryCount { get { return 5; } }

        public bool BUrl404(string url_)
        {
            bool retB = false;
            if (mUrl404.TryGetValue(url_, out retB) == true)
                return retB;
            return false;
        }

        public void RemoveLoader(string url_)
        {
            Loader loader = null;
            if (mLoadingPool.TryGetValue(url_, out loader) == true)
            {
                loader.Dispose();
                mLoadingPool.Remove(url_);
            }
        }

        public Loader GetLoader(AssetResource resource_)
        {
            Loader loader = null;
            string url = resource_.Url;
            AssetResource.DataTypeE parserType = resource_.DataType;
            if (mLoadingPool.TryGetValue(url, out loader) == true && resource_.IsForceRemote == false)
            {
                return loader;
            }

            if (resource_.IsForceRemote == false)
            {
                //先验证是否有热更新的资源
                string fullLocalPath = Core.PathDefine.GetPersistentPath(url);
                if (File.Exists(fullLocalPath) == true)
                {//加载热更资源
                    //loader = new LoaderFileStream(fullLocalPath, url, parserType);
                    fullLocalPath = Core.PathDefine.GetPersistentPath(url , true);
                    loader = new LoaderStreamAssets(fullLocalPath, url, parserType);
                    loader.FromType = AssetResource.FromTypeE.PersistentLocal;
                }
                else
                {//加载包内资源
                    fullLocalPath = Core.PathDefine.GetStreamingAssetsPath(url, true);
                    if (Application.platform == RuntimePlatform.IPhonePlayer)
                    {//ios强制使用WebRequest;
                        loader = new LoaderWebRequest(fullLocalPath, parserType);
                    }
                    else
                    {
                        loader = new LoaderStreamAssets(fullLocalPath, url, parserType);
                    }
                    loader.FromType = AssetResource.FromTypeE.StreamingAssets;
                }
                mLoadingPool[resource_.Url] = loader;
            }

            if (loader == null || resource_.IsForceRemote == true)
            {//从服务器上获取资源
                loader = new LoaderWebRequest(url, parserType);
                if (resource_.IsForceRemote)
                {
                    loader.PostData = resource_.PostData;
                    loader.Timeout = resource_.Timeout;
                }
                loader.FromType = AssetResource.FromTypeE.Remote;
            }

            return loader;
        }

        //----------------------------------------------------------
        public static bool DEBUG = false;
        public static int MAX = 300;
        /// <summary>
        /// 默认限制数量
        /// </summary>
        public static int CONCURRENCE = 4;

        protected Queue<Loader> queue = new Queue<Loader>();
        protected List<Loader> runingList = new List<Loader>();

        private int running = 0;
        public bool Add(Loader loader)
        {
            if (runingList.Contains(loader) || queue.Contains(loader))
            {
                return false;
            }
            queue.Enqueue(loader);
            return true;
        }
        void Update()
        {
            if (running < CONCURRENCE && queue.Count > 0)
            {
                Loader loader = queue.Dequeue();
                if(loader != null && loader.BDisposed == false)
                {//loader没有被释放
                    loader.AddListener(Event.COMPLETE, loadHandle, 1);
                    loader.AddListener(Event.FAILED, loadHandle, 1);
                    loader.AddListener(Event.DISPOSE, loadHandle, 1);

                    runingList.Add(loader);
                    running++;

                    loader.Load();
                }
            }
        }

        private void loadHandle(Event e)
        {
            Loader target = (Loader)e.Target;
            runingList.Remove(target);
            running--;
            
            target.RemoveListener(Event.COMPLETE, loadHandle);
            target.RemoveListener(Event.FAILED, loadHandle);
            target.RemoveListener(Event.DISPOSE, loadHandle);
        }
    }
}
