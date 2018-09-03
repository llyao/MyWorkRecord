using System;
using System.Collections.Generic;
using UnityEngine ;

namespace PlatformCore
{
    public class AssetBundleResource:AssetResource
    {
        protected string manifestKey = "";
        protected string dependKey = "";
        protected bool isProgress = false;
        protected AssetResource assetBundleManifest;

        protected List<AssetResource> dependenciesResource = new List<AssetResource>();
        protected AssetResource manifestResource;

        protected float currentProgress = 0;

        public AssetBundleResource(string url_) : base(url_)
        {
            //取得对应的manifes
            manifestKey = Core.AssetManager.GetManifestKeyInUrl(url_);

            string manifesURI = "/" + manifestKey + "/";
            int index = url_.IndexOf(manifesURI);
            int len = manifesURI.Length;
            dependKey = url_.Substring(index + len).ToLower();
        }

        public override void MakeTimeStamp()
        {
            base.MakeTimeStamp();
            foreach (AssetResource item in dependenciesResource)
                item.MakeTimeStamp();
        }

        protected override void loadImp(int priority = 0, bool progress = false, uint retryCount = 0)
        {
            //设置加载中状态
            mStatus = AssetStateE.LOADING;

            isProgress = progress;

            //首先加载manifest
            manifestResource = Core.AssetManager.GetResource(true , string.Format("/{0}/{1}" , manifestKey , manifestKey), AssetResource.DataTypeE.MANIFEST);
            manifestResource.IsForceRemote = IsForceRemote;
            Event.AddCompleteAndFail(manifestResource , manifestHandleComplete);
            manifestResource.Load();
        }
        protected override void progressHandle(Event e)
        {
            DispatchEvent(e.Type, (float)e.Data * (1f - currentProgress) + currentProgress);
        }

        private int mNeedLoadedDependCount = 0;
        private int mTotalDependCount = 0;

        private void manifestHandleComplete(Event e)
        {
            Event.RemoveCompleteAndFail(manifestResource , manifestHandleComplete);

            currentProgress = 0.1f;
            DispatchEvent(Event.PROGRESS, currentProgress);

            assetBundleManifest = e.Target as AssetResource;
            if (e.Type != Event.COMPLETE)
            {//manifest加载失败直接返回,并且释放加载器
                Loader.Delete(ref mLoader);
                resourceComplete(Event.FAILED);
                return;
            }

            //取得依赖资源
            string[] dependencies = assetBundleManifest.Manifest.GetDirectDependencies(dependKey);
            AssetResource tempResource;
            mNeedLoadedDependCount = dependencies.Length;

            //首先需要增加自身
            mNeedLoadedDependCount += 1;

            dependenciesResource.Clear();
            for (int i = 0; i < dependencies.Length; i++)
            {
                string dependency = dependencies[i];
                if (dependency == dependKey)
                {//如果是自身，则直接返回
                    mNeedLoadedDependCount--;
                    continue;
                }
                string url = "/" + Core.PathDefine.PlatformName + "/" + manifestKey + "/" + dependency;
                tempResource = Core.AssetManager.GetResource(true , "/" + manifestKey + "/" + dependency, AssetResource.DataTypeE.ASSETBUNDLE);
                if (dependenciesResource.Contains(tempResource))
                {//已经在依赖列表中
                    mNeedLoadedDependCount--;
                    continue;
                }
                Event.AddCompleteAndFail(tempResource, dependsHandle);
                dependenciesResource.Add(tempResource);
            }

            mTotalDependCount = mNeedLoadedDependCount;
            for (int i = 0; i < dependenciesResource.Count; i++)
            {
                dependenciesResource[i].Load();
            }

            base.loadImp(0, isProgress);
        }

        private void dependsHandle(Event e)
        {
            currentProgress = 0.8f;
            float progress = (mTotalDependCount - mNeedLoadedDependCount) / (float)mTotalDependCount;
            DispatchEvent(Event.PROGRESS, 0.7f * progress + 0.1f);

            AssetResource tempResource = e.Target as AssetResource;
            Event.RemoveCompleteAndFail(tempResource, dependsHandle);

            mNeedLoadedDependCount--;
            CheckAllComplete();
        }

        protected override void loadComplete(Event e)
        {
            //加载引用计数减1
            mNeedLoadedDependCount--;

            mLoader = e.Target as Loader;

            mLoader.RemoveListener(Event.PROGRESS, progressHandle);
            mLoader.RemoveListener(Event.CANCEL, loadCancelHandle);
            Event.RemoveCompleteAndFail(mLoader, loadComplete);

            string eventType = e.Type;
            if (eventType != Event.COMPLETE)
            {//加载失败
                resourceComplete(Event.FAILED, (string)e.Data);
                return;
            }

            AssetBundleItem abItem = e.Data as AssetBundleItem;
            if (abItem != null)
            {//当前资源包加载成功
                //检查是否全部加载成功
                CheckAllComplete();
                return;
            }
            else
            {//资源包加载失败
                resourceComplete(Event.FAILED, "assetBundle is null");
            }
        }
        
        private void CheckAllComplete()
        {
            if (mNeedLoadedDependCount != 0)
            {
                if (mNeedLoadedDependCount < 0)
                {
                    Debug.LogError("AssetBundle lenError:" + mNeedLoadedDependCount + " url:" + mUrl);
                    resourceComplete(Event.COMPLETE);
                }
                return;
            }
            if (Data is AssetBundle == false)
            {
                resourceComplete(Event.COMPLETE); 
                return;
            }
            AssetBundle assetBundle = (AssetBundle)Data;
            if (assetBundle.isStreamedSceneAssetBundle)
            {
                resourceComplete(Event.COMPLETE);
                return;
            }

            string name = FileFunc.GetNameInPathname(dependKey);
            resourceComplete(Event.COMPLETE);
        }
        
        protected override void Dispose(bool disposing_)
        {
            //不做除引用操作;
            if (manifestResource != null)
                manifestResource.ClearEvent();
            manifestResource = null;

            dependenciesResource.Clear();

            base.Dispose(disposing_);
        }

    }
}
 