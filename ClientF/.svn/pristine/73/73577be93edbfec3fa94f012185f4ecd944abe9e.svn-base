using System;
using System.Collections.Generic;
using UnityEngine ;

namespace PlatformCore
{
    public class AssetResource : ITimeStamp
    {

        /// <summary>
        /// 资源类型
        /// </summary>
        public enum DataTypeE
        {
            BYTES = 1,
            CONFIG,
            EDITORRESOURCE,
            RESOURCE,
            TEXTURE,
            MANIFEST,
            PREFAB,
            ASSETBUNDLE,
            POST,
            GET,
        }

        /// <summary>
        /// 数据的来源类型
        /// </summary>
        public enum FromTypeE
        {
            StreamingAssets,
            PersistentLocal,
            Remote,
        }

        public enum AssetStateE
        {
            NONE,// 未加载
            PARSING,//解析状态
            PARSED,//解析状态

            LOADING,// 加载中
            READY,// 已加载
            FAILD,// 加载失败
            DISPOSE//不具体使用,用于调试
        }

        protected string mUrl;
        protected Loader mLoader;
        protected AssetStateE mStatus = AssetStateE.NONE;
        protected AssetResource.DataTypeE mDataType;
        public bool IsForceRemote = false;

        public string PostData;
        public int Timeout = -1;

        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get { return mUrl; } }

        /// <summary>
        /// 加载器
        /// </summary>
        public Loader Loader { get { return mLoader; } }

        /// <summary>
        /// 加载到的数据
        /// </summary>
        public virtual object Data
        {
            get
            {
                if (Loader == null)
                        return null;
                return Loader.Data;
            } 
        }

        /// <summary>
        /// 当前状态
        /// </summary>
        public AssetStateE Status { get { return mStatus; } }

        /// <summary>
        /// 数据类型
        /// </summary>
        public AssetResource.DataTypeE DataType { get { return mDataType; } set { mDataType = value; }}

        /// <summary>
        /// 数据来源
        /// </summary>
        public AssetResource.FromTypeE FromType { get { return mLoader.FromType; } }

        /// <summary>
        /// 是否已经加载结束
        /// </summary>
        public bool IsLoaded{get{return (Status == AssetStateE.READY || Status == AssetStateE.FAILD);}}

        /// <summary>
        /// 是否加载成功
        /// </summary>
        public bool IsReady{get{return Status == AssetStateE.READY;}}

        public AssetResource(string url_)
        {
            mUrl = url_;
        }

        ~AssetResource()
        {
            
        }

        public virtual void Load(uint retryCount = 0, bool progress = false, int priority = 0)
        {
            //已经在加载直接返回
            if (Status == AssetStateE.LOADING)
                return;

            if (IsLoaded && IsForceRemote == false)
            {//已经加载过
                if (Status == AssetStateE.FAILD)
                {
                    if (Core.LoaderManager.BUrl404(mUrl) == true)
                    {//资源已经出问题了
                        DispatchEvent(Event.FAILED, "404");
                    }
                    else
                    {//重新加载
                        loadImp(priority, progress, retryCount);
                    }
                }
                else
                {
                    //直接发送资源加载成功通知
                    DispatchEvent(Event.COMPLETE, Data);

                    //放入缓存池
                    Core.AssetResourceRefPool.Add(this);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(mUrl))
                {
                    mStatus = AssetStateE.FAILD;
                    Debug.Log("assetLoading:" + mUrl);
                    DispatchEvent(Event.FAILED, "url is empty");
                }
                else
                {// 开始加载
                    loadImp(priority, progress, retryCount);
                }
            }
        }
        
        protected virtual void loadImp(int priority = 0, bool progress = false, uint retryCount = 0)
        {
            //设置状态
            mStatus = AssetStateE.LOADING;

            //重新获取加载器
            if (mLoader == null || IsForceRemote == true)
                mLoader = Loader.New(this);

            mLoader.CheckProgress = progress;
            mLoader.RetryCount = retryCount;

            Event.AddCompleteAndFail(mLoader, loadComplete);
            if (progress)
                mLoader.AddListener(Event.PROGRESS, progressHandle , 0);
            mLoader.AddListener(Event.CANCEL, loadCancelHandle, 1);

            //添加到下载队列
            Core.LoaderManager.Add(mLoader);
        }

        protected virtual void progressHandle(Event e)
        {
            this.DispatchEvent(e);
        }

        protected virtual void loadCancelHandle(Event e)
        {
            mLoader.RemoveListener(Event.PROGRESS, progressHandle);
            mLoader.RemoveListener(Event.CANCEL, loadCancelHandle);
            Event.RemoveCompleteAndFail(mLoader, loadComplete);

            if (Status == AssetStateE.LOADING)
                mStatus = AssetStateE.NONE;

            DispatchEvent(Event.FAILED);
        }
        protected virtual void loadComplete(Event e)
        {
            mLoader = e.Target as Loader;

            mLoader.RemoveListener(Event.PROGRESS, progressHandle);
            mLoader.RemoveListener(Event.CANCEL, loadCancelHandle);
            Event.RemoveCompleteAndFail(mLoader, loadComplete);

            if (e.Type == Event.FAILED)
            {
                resourceComplete(Event.FAILED, (string)e.Data);
                return;
            }

            if (Data == null)
            {
                resourceComplete(Event.FAILED, "加载资源为null");
                return;
            }

            resourceComplete(Event.COMPLETE);
        }
        protected virtual void resourceComplete(string eventType, string errorMessage = "")
        {
            if (eventType == Event.COMPLETE)
            {
                mStatus = AssetStateE.READY;
                DispatchEvent(eventType, Data);
            }
            else
            {
                mStatus = AssetStateE.FAILD;
                DispatchEvent(eventType, errorMessage);
            }
        }

        //-----------------------------------------------------------------------------------------------------
        protected AssetBundleManifest mManifest;
        protected UnityEngine.Object mMainAsset;
        protected Stack<PrefabPoolItem> mPrefabPool;
        protected Sprite mSprite;
        protected string[] mAllScenePaths;
        protected string[] mAllAssetNames;

        public string[] AllScenePaths
        {
            get
            {
                if (mAllScenePaths == null && Data is AssetBundleItem == true)
                    mAllScenePaths = (Data as AssetBundleItem).GetAllScenePaths();
                return mAllScenePaths;
            }
        }

        public string[] AllAssetNames
        {
            get
            {
                if (mAllAssetNames == null && Data is AssetBundleItem == true)
                    mAllAssetNames = (Data as AssetBundleItem).GetAllAssetNames();
                return mAllAssetNames;
            }
        }

        public Stack<PrefabPoolItem> PrefabPool
        {
            get
            {
                if(mPrefabPool == null)
                    mPrefabPool = new Stack<PrefabPoolItem>();
                return mPrefabPool;
            }
        }

        /// <summary>
        /// 过去依赖关系信息
        /// </summary>
        public AssetBundleManifest Manifest
        {
            get
            {
                if(mManifest == null && Data != null && Data is AssetBundleItem == true)
                    mManifest = (Data as AssetBundleItem).LoadAsset<AssetBundleManifest>("AssetBundleManifest");
                return mManifest;
            }
        }


        /// <summary>
        /// 获取主资源
        /// </summary>
        /// <returns></returns>
        public object MainAsset
        {
            get
            {
                if (mMainAsset == null)
                {
                    if (Data is AssetBundleItem)
                    {
                        string name = FileFunc.GetNameInPathname(mUrl);
                        mMainAsset = (Data as AssetBundleItem).LoadAsset(name);
                    }
                }
                return mMainAsset;
            }
        }


        public Sprite Sprite
        {
            get
            {
                if (mSprite != null)
                    return mSprite;

                Texture2D texture = Data as Texture2D;
                if (texture != null)
                {
                    Rect rect = new Rect(0, 0, texture.width, texture.height);
                    Vector2 p = new Vector2();
                    mSprite = Sprite.Create(texture, rect, p, 100, 0, SpriteMeshType.FullRect);
                }

                return mSprite;
            }
        }


        private Sprite[] mSpriteList;

        public Sprite GetSpriteByName(string name)
        {
            object[] list = (object[])Data;
            if(list != null)
            {
                for (int i = 0; i < list.Length; i++)
                {
                    Sprite item = list[i] as Sprite;
                    if (item != null && item.name == name)
                    {
                        return item;
                    }
                }
            }

            if (mSpriteList == null)
                mSpriteList = ((AssetBundleItem)Data).LoadAllAssets<Sprite>();
            if (mSpriteList == null)
                return null;

            int len = mSpriteList.Length;
            if (len > 0)
            {
                for (int i = 0; i < len; i++)
                {
                    if (mSpriteList[i].name == name)
                    {
                        return mSpriteList[i];
                    }
                }
            }
            return null;
        }

        protected bool mIsShaderFinded = false;
        public object InstantiatePrefab()
        {
            if (MainAsset == null || MainAsset is GameObject == false)
                return null ;

            GameObject retGO = null;
            try
            {
                if (mIsShaderFinded == false)
                {
                    mIsShaderFinded = true;
                    RenderUtils.ShaderRebind((GameObject)MainAsset);
                }
                retGO = GameObject.Instantiate((GameObject)MainAsset) as GameObject;
                if(retGO.activeSelf == false)
                    retGO.SetActive(true) ;
            }
            catch (Exception e)
            {
            }

            return retGO;
        }

        public virtual void RecycleToPool(PrefabPoolItem poolItem)
        {
            if (poolItem == null)
                return;
            GameObject go = poolItem.gameObject;
            go.SetActive(false);
            PrefabPool.Push(poolItem);
        }

        public virtual void RecycleToPool(GameObject go_)
        {
            if (go_ == null)
                return;

            PrefabPoolItem pi = go_.GetComponent<PrefabPoolItem>();
            if (pi == null)
            {
                GameObject.DestroyImmediate(go_);
                return;
            }

            go_.SetActive(false);
            PrefabPool.Push(pi);
        }

        public virtual PrefabPoolItem GetPrefabFromPool()
        {
            PrefabPoolItem retP = null;
            GameObject go = null;

            //先看池里有没有资源
            while (PrefabPool.Count > 0)
            {
                retP = PrefabPool.Pop();
                if (retP != null)
                {
                    retP.isNew = false;
                    go = retP.gameObject;
                    go.SetActive(true);
                    return retP;
                }
            }

            //池里没有则创建新的
            go = InstantiatePrefab() as GameObject;
            if (go != null)
            {
                retP = go.AddComponent<PrefabPoolItem>();
                retP.mAssetResource = this;
            }
            return retP;
        }

        //----------------------------------------------------------------------------------------------

        /// <summary>
        /// 垃圾回收释放资源
        /// </summary>
        /// <param name="disposing_"></param>
        protected override void Dispose(bool disposing_)
        {
            if (!disposedValue)
            {
                Debug.Log("Destroy assetResource:"+this.Url);

                disposedValue = true;

                //释放对应的加载器
                Loader.Delete(ref mLoader);

                //释放加载进来的主资源
                if (mMainAsset != null)
                {
                    //DeletePoolGameObjectDestroy.New(mMainAsset);
                    mMainAsset = null;
                }

                if (mSprite != null)
                {
                    //DeletePoolGameObjectDestroy.New(mSprite);
                    mSprite = null;
                }

                //释放加载进来的sprite
                if (mSpriteList != null)
                    mSpriteList = null;

                if (mPrefabPool != null)
                {
                    foreach (PrefabPoolItem item in mPrefabPool)
                    {
                        item.Dispose();
                    }
                    mPrefabPool.Clear();
                    mPrefabPool = null;
                }

                mAllScenePaths = null;

                mStatus = AssetStateE.NONE;


                //向外发送已经被垃圾回收的事件
                DispatchEvent(Event.DISPOSE);

                //清理消息
                ClearEvent();
            }
        }
    }
}
