using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine ;

namespace PlatformCore
{
    public class Loader : EventDispatcher,IDisposable
    {
        /// <summary>
        /// 当前的加载状态
        /// </summary>
        public enum LoadStateE
        {
            NONE,
            LOADING,
            COMPLETE,
            ERROR,
            UNLOAD
        }

        protected string mUrl = "";
        protected AssetResource.DataTypeE mDataType = AssetResource.DataTypeE.ASSETBUNDLE;
        protected LoadStateE mLoadState = LoadStateE.NONE ;

        /// <summary>
        /// WebRequest使用的数据
        /// </summary>
        public string PostData { get; set; }
        public int Timeout = -1;

        public bool CheckProgress = false;
        public uint RetryCount = 0;

        /// <summary>
        /// 数据来源
        /// </summary>
        public AssetResource.FromTypeE FromType = AssetResource.FromTypeE.PersistentLocal;

        /// <summary>
        /// 加载到的数据
        /// </summary>
        protected object mData = null;
        public object Data { get { return mData; } }

        /// <summary>
        /// 资源地址
        /// </summary>
        public string Url { get { return mUrl; } }

        /// <summary>
        /// 数据类型
        /// </summary>
        public AssetResource.DataTypeE DataType { get { return mDataType; } }

        /// <summary>
        /// 当前加载携程的句柄
        /// </summary>
        protected Coroutine mCoroutine = null;

        /// <summary>
        /// 当前的状态
        /// </summary>
        public LoadStateE LoadState { get { return mLoadState; } }

        //---------------------------------------------------------------------

        public static Loader New(AssetResource ar_)
        {
            if (ar_ == null)
                return null;

            Loader loader = Core.LoaderManager.GetLoader(ar_);
            loader.AddRef();
            return loader;
        }

        public static void Delete(ref Loader loader_)
        {
            if (loader_ == null)
                return;

            loader_.DelRef();
            loader_ = null;
        }

        protected int mRefCount = 0;

        public int RefCount { get { return mRefCount; } }


        public void AddRef()
        {
            mRefCount++;
        }

        public void DelRef()
        {
            mRefCount--;
            if (mRefCount <= 0)
                Dispose();
        }

        //----------------------------------------------------------------------

        public Loader(string url_, AssetResource.DataTypeE dataType_)
        {
            mUrl = url_;
            mDataType = dataType_;
        }

        public virtual void Load()
        {
            
        }

        /// <summary>
        /// 开启携程
        /// </summary>
        /// <param name="routine"></param>
        /// <returns></returns>
        public Coroutine StartCoroutine(IEnumerator routine)
        {
            mCoroutine = Core.Instance.StartCoroutine(routine);
            return mCoroutine;
        }

        protected virtual void onAssetBundleHandle(AssetBundle assetBoundle_)
        {
            if (assetBoundle_ != null)
            {
                mLoadState = LoadStateE.COMPLETE;

                //生成ab的引用计数
                mData = AssetBundleItem.New(mUrl , assetBoundle_);
                DispatchEvent(Event.COMPLETE, mData);
            }
            else
            {
                mLoadState = LoadStateE.ERROR;
                mData = null;

                string message = string.Format("加载:{0},未获取到assetBundle", mUrl);
                Debug.LogWarning(message);
                DispatchEvent(Event.FAILED, message);
            }
        }

        public virtual void Update(float progress)
        {

        }

        //------------------------------------------------------

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用
        public bool BDisposed { get { return disposedValue; } }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                Debug.Log("Destroy Loader:" + this.Url);

                disposedValue = true;

                Core.LoaderManager.RemoveLoader(Url);

                if (mLoadState == LoadStateE.COMPLETE)
                {
                    //是ab包
                    AssetBundleItem.Delete(ref mData);
                
                    //是图片资源
                    if (mData != null && mData is Texture2D == true)
                    {
                        //DeletePoolGameObjectDestroy.New(mData as UnityEngine.Object);
                        mData = null;
                    }
                }
                
                if (mLoadState == LoadStateE.LOADING)
                {//如果还在加载过程中则停掉携程
                    if(mCoroutine != null)
                        Core.Instance.StopCoroutine(mCoroutine);
                    mCoroutine = null;
                }
                
                mData = null;
                
                mLoadState = LoadStateE.NONE;
                
                DispatchEvent(Event.DISPOSE);
                
                //清除消息
                ClearEvent();
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
         ~Loader() {
           // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
           Dispose(false);
         }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
