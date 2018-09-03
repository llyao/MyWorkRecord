using System.IO;
using UnityEngine ;

namespace PlatformCore
{
    public class LoaderFileStream:Loader
    {
        protected string mFullLocalUrl;
        public LoaderFileStream(string fullLocalUrl_ , string url_, AssetResource.DataTypeE dataType_) : base(url_, dataType_)
        {
            mFullLocalUrl = fullLocalUrl_;
        }

        public override void Load()
        {
            if (mData != null)
            {//已经有数据直接完成
                DispatchEvent(Event.COMPLETE, mData);
                return;
            }

            if (LoadState == LoadStateE.LOADING)
            {
                //正在加载中;
                Debug.LogWarning("FileStreamLoading:" + mFullLocalUrl);
                return;
            }

            if (File.Exists(mFullLocalUrl) == false)
            {//本地文件不存在
                mLoadState = LoadStateE.ERROR;
                string message = string.Format("FileStreamLoading error 文件不存在:{0} :", mFullLocalUrl);
                Debug.LogWarning(message);
                DispatchEvent(Event.FAILED, message);
                return;
            }

            switch (DataType)
            {
                case AssetResource.DataTypeE.PREFAB:
                case AssetResource.DataTypeE.ASSETBUNDLE:
                case AssetResource.DataTypeE.MANIFEST:
                    onAssetBundleHandle(AssetBundle.LoadFromFile(mFullLocalUrl));
                    break;
                case AssetResource.DataTypeE.TEXTURE:
                    mLoadState = LoadStateE.COMPLETE;
                    mData = FileFunc.LoadTexture2DFromFile(mFullLocalUrl);
                    DispatchEvent(Event.COMPLETE, mData);
                    break;

                case AssetResource.DataTypeE.BYTES:
                case AssetResource.DataTypeE.CONFIG:
                    mLoadState = LoadStateE.COMPLETE;
                    mData = File.ReadAllBytes(mFullLocalUrl);
                    DispatchEvent(Event.COMPLETE, mData);
                    break;
            }
        }
    }
}
