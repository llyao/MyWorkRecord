using System.Collections;
using System.Collections.Generic;
using UnityEngine ;

namespace PlatformCore
{
    public class LoaderStreamAssets:Loader
    {
        protected string mFullLocalUrl;
        public LoaderStreamAssets(string fullLocalUrl_ , string url_, AssetResource.DataTypeE dataType_) : base(url_, dataType_)
        {
            mFullLocalUrl = fullLocalUrl_;
        }

        public override void Load()
        {
            if (mData != null)
            {
                DispatchEvent(Event.COMPLETE, mData);
                return;
            }

            if (LoadState == LoadStateE.LOADING)
            {
                Debug.LogWarning("正在加载中:" + mFullLocalUrl);
                return;
            }
            
            mLoadState = LoadStateE.LOADING;
            StartCoroutine(doLoad(mFullLocalUrl));
        }

        private IEnumerator doLoad(string fullLocalURL_)
        {
            Debug.Log("LoaderStreamAsset.doLoad:" + fullLocalURL_);
            WWW www = new WWW(fullLocalURL_);
            while (!www.isDone)
            {
                Update(www.progress);
                yield return null;
            }

            string error = www.error;
            if (string.IsNullOrEmpty(error))
            {
                mLoadState = LoadStateE.COMPLETE;
                switch (DataType)
                {
                    case AssetResource.DataTypeE.BYTES:
                    case AssetResource.DataTypeE.CONFIG:
                        mData = www.bytes;
                        DispatchEvent(Event.COMPLETE, mData);
                        break;
                    case AssetResource.DataTypeE.MANIFEST:
                    case AssetResource.DataTypeE.ASSETBUNDLE:
                    case AssetResource.DataTypeE.PREFAB:
                        onAssetBundleHandle(www.assetBundle);
                        break;
                    case AssetResource.DataTypeE.TEXTURE:
                        Texture2D tex;
                        tex = new Texture2D(2, 2, TextureFormat.ARGB32, false, false);
                        www.LoadImageIntoTexture(tex);
                        mData = tex;
                        DispatchEvent(Event.COMPLETE, mData);
                        break;
                }
                www.Dispose();
                www = null;
            }
            else
            {
                string message = string.Format("加载文件失败:{0} error:{1}", mUrl, error);
                Debug.LogWarning(message);

                www.Dispose();
                www = null;
                DispatchEvent(Event.FAILED, message);
            }
        }
    }
}
