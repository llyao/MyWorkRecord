using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PlatformCore
{
    public class AssetBundleItem : ObjectPoolItem<AssetBundleItem>
    {
        protected string mUrl;
        protected AssetBundle mAB = null;

        public string Url { get { return mUrl; } }
        public AssetBundle AssetBundle { get { return mAB; } }

        public void Init(string url_, AssetBundle ab_)
        {
            mUrl = url_;
            mAB = ab_;
        }

        public static AssetBundleItem New(string url_, AssetBundle ab_)
        {
            if (ab_ == null)
                return null;

            AssetBundleItem item = Core.ABPool.AddAssetBoundle(url_, ab_);
            return item;
        }

        public static void Delete(ref object item_)
        {
            AssetBundleItem item = item_ as AssetBundleItem;
            if (item == null)
                return;

            Core.ABPool.RemoveAssetBoundle(item.Url);
            item_ = null;
        }
        internal override void poolInitial()
        {

        }

        internal override void poolRecycle()
        {
            Debug.Log("Destroy AB:" + this.Url);

            //释放ab包资源
            DeletePoolAssetBundleUnload.New(mAB);
            mAB = null;
        }

        //--------------------------------------------------------------------

        public UnityEngine.Object LoadAsset(string name_)
        {
            if (mAB == null)
                return null;
            return mAB.LoadAsset(name_);
        }

        public T LoadAsset<T>(string name_) where T : UnityEngine.Object
        {
            if (mAB == null)
                return null;

            return mAB.LoadAsset<T>(name_);
        }

        public T[] LoadAllAssets<T>() where T:UnityEngine.Object
        {
            if (mAB == null)
                return null;
            return mAB.LoadAllAssets<T>();
        }

        public string[] GetAllScenePaths()
        {
            if (mAB == null)
                return null;
            return mAB.GetAllScenePaths();
        }

        public string[] GetAllAssetNames()
        {
            if (mAB == null)
                return null;
            return mAB.GetAllAssetNames();
        }
    }

    public class CoreABPool : ICore
    {
        /// <summary>
        /// ab包缓存
        /// </summary>
        protected Dictionary<string, AssetBundleItem> mABMap = new Dictionary<string, AssetBundleItem>();

        protected List<AssetBundle> mUnloadABLst = new List<AssetBundle>();

        /// <summary>
        /// 添加ab包
        /// </summary>
        /// <param name="url_"></param>
        /// <param name="ab_"></param>
        public AssetBundleItem AddAssetBoundle(string url_, AssetBundle ab_)
        {
            if (ab_ == null)
            {
                Debug.LogWarning("assetBundle 不存在 :" + url_);
                return null;
            }

            string url = url_.ToLower();

            AssetBundleItem abItem = null;
            if (mABMap.TryGetValue(url, out abItem) == true)
            {//已经存在则增加引用计数
                abItem.AddRef();
            }
            else
            {//创建新的abItem
                abItem = ObjectPool<AssetBundleItem>.New();
                abItem.Init(url_ , ab_);
                mABMap[url] = abItem;
            }

            return abItem;
        }

        /// <summary>
        /// 释放一个ab包
        /// </summary>
        /// <param name="url_"></param>
        public void RemoveAssetBoundle(string url_)
        {
            string key = url_.ToLower();

            AssetBundleItem abItem = null;
            if (mABMap.TryGetValue(key, out abItem) == true)
            {
                //先减少已有ab的引用次数
                abItem.DelRef();

                //已经被释放则从map中删除
                if(abItem.RefCount <= 0)
                    mABMap.Remove(key);
            }
        }

        /// <summary>
        /// 取得一个ab包
        /// </summary>
        /// <param name="url_"></param>
        /// <returns></returns>
        public AssetBundleItem GetAssetBoundle(string url_)
        {
            string key = url_.ToLower();
            AssetBundleItem abItem = null;
            if (mABMap.TryGetValue(key, out abItem) == true)
            {
                return abItem;
            }
            return null;
        }

        /// <summary>
        /// 取得ab包的引用次数
        /// </summary>
        /// <param name="url_"></param>
        /// <returns></returns>
        public int GetAssetBoundleRefCount(string url_)
        {
            string key = url_.ToLower();
            AssetBundleItem abItem = null;
            if (mABMap.TryGetValue(key, out abItem) == true)
            {
                return abItem.RefCount;
            }
            return 0;
        }

        public void AddUnloadAB(AssetBundle ab_)
        {
            mUnloadABLst.Add(ab_);
        }

        public void Update()
        {
            for (int i = 0; i < mUnloadABLst.Count; i++)
            {
                mUnloadABLst[i].Unload(false);
            }
            mUnloadABLst.Clear();
        }
    }
}
