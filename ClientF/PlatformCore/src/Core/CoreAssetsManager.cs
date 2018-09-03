using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine ;

namespace PlatformCore
{
    public class CoreAssetsManager : EventDispatcherMono , ICore
    {
        /// <summary>
        /// 依赖文件名称索引
        /// </summary>
        protected Dictionary<string, string> mManifestMap = new Dictionary<string, string>();

        /// <summary>
        /// 资源缓存池使用弱引用方式，配合清理池使用
        /// </summary>
        private Dictionary<string, WeakReferenceItem<AssetResource>> mResourceMap = new Dictionary<string, WeakReferenceItem<AssetResource>>();

        /// <summary>
        /// 资源类型列表
        /// </summary>
        private Dictionary<AssetResource.DataTypeE, Type> mResourceTypeMapping = new Dictionary<AssetResource.DataTypeE, Type>();
        
        /// <summary>
        /// 获取url地址中包含的依赖关系名称
        /// </summary>
        /// <param name="url_"></param>
        /// <returns></returns>
        public string GetManifestKeyInUrl(string url_)
        {
            foreach (string key in mManifestMap.Keys)
            {
                if (url_.IndexOf(key) != -1)
                {
                    return Core.AssetManager.mManifestMap[key];
                }
            }
            return "";
        }

        public void RegistManifest(string pathKey, string manifesKey)
        {
            if (mManifestMap.ContainsKey(pathKey))
            {
                mManifestMap.Remove(pathKey);
            }
            mManifestMap.Add(pathKey, manifesKey);
        }

        public CoreAssetsManager()
        {
            RegistManifest("/all/", "all");
            RegistManifest("/ui/", "ui");

            regist<AssetResource>(AssetResource.DataTypeE.MANIFEST);
            regist<AssetBundleResource>(AssetResource.DataTypeE.PREFAB);
            regist<AssetBundleResource>(AssetResource.DataTypeE.ASSETBUNDLE);
            regist<InnerAssetResource>(AssetResource.DataTypeE.RESOURCE);
            regist<EditorAssetResource>(AssetResource.DataTypeE.EDITORRESOURCE);
        }
        protected void regist<T>(AssetResource.DataTypeE type) where T : AssetResource
        {
            Type clz = typeof(T);
            if (mResourceTypeMapping.ContainsKey(type))
                mResourceTypeMapping.Remove(type);

            mResourceTypeMapping.Add(type, clz);
        }

        /// <summary>
        /// 寻找已有的res
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        protected AssetResource FindResource(string url)
        {
            if (url == null)
                return null;

            WeakReferenceItem<AssetResource> res = null;
            string key = url.ToLower();
            if (mResourceMap.TryGetValue(key, out res) == true)
            {
                return res.Target;
            }

            return null;
        }
        
        /// <summary>
        /// 取得一个资源
        /// </summary>
        /// <param name="url"></param>
        /// <param name="autoCreateType"></param>
        /// <returns></returns>
        public AssetResource GetResource(bool bAssetBundle_ , string url, AssetResource.DataTypeE autoCreateType = AssetResource.DataTypeE.BYTES)
        {
            if(autoCreateType == AssetResource.DataTypeE.MANIFEST)
                url = string.Format("/{0}{1}", Core.PathDefine.PlatformName, url);
            else if (bAssetBundle_ == true)
            {
                url = string.Format("/{0}{1}", Core.PathDefine.PlatformName, url);
                if (url.EndsWith(Core.PathDefine.U3DExt) == false)
                    url = string.Format("{0}{1}", url, Core.PathDefine.U3DExt);
            }

            AssetResource res = FindResource(url);
            if (res == null)
            {//没有找到则创建新的
                Type cls = null;
                if (mResourceTypeMapping.TryGetValue(autoCreateType, out cls) == false)
                {//不是已注册类型则使用默认类型
                    res = new AssetResource(url);
                }
                else
                {
                    res = (AssetResource)Activator.CreateInstance(cls, url);
                }

                res.DataType = autoCreateType;

                //注册释放回调
                res.AddListener(Event.DISPOSE, resourceDisposeHandle, 1);

                //放入引用池
                string key = url.ToLower();
                mResourceMap[key] = WeakReferenceItem<AssetResource>.New(res);
            }

            //放入缓存池
            Core.AssetResourceRefPool.Add(res);

            return res;
        }

        /// <summary>
        /// 释放资源，同时从两个池中释放资源
        /// </summary>
        /// <param name="url_"></param>
        public void RemoveResource(string url_)
        {
            string url = url_.ToLower();

            WeakReferenceItem<AssetResource> w = null;
            if (mResourceMap.TryGetValue(url , out w) == true)
            {
                //取消弱引用
                mResourceMap.Remove(url);
                
                //取消强引用
                Core.AssetResourceRefPool.Remove(w.Target);

                //回收
                WeakReferenceItem<AssetResource>.Delete(ref w);
            }
        }

        private void resourceDisposeHandle(Event e)
        {
            AssetResource res = e.Target as AssetResource;
            res.RemoveListener(Event.DISPOSE, resourceDisposeHandle);

            //释放资源
            RemoveResource(res.Url);
        }

    }
}
