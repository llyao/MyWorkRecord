using System.Collections;
using UnityEngine ;
using UnityEngine.Networking;

namespace PlatformCore
{
    public class LoaderWebRequest:Loader
    {
        protected int mRetryedCount = 0;
        protected int mRetryCount = 0;

        public LoaderWebRequest(string url_, AssetResource.DataTypeE dataType_) : base(url_, dataType_)
        {

        }
        public override void Load()
        {
            if (string.IsNullOrEmpty(mUrl))
            {
                DispatchEvent(Event.FAILED, "文件路径为空:" + mUrl);
                return;
            }

            if (mData != null)
            {
                DispatchEvent(Event.COMPLETE, mData);
                return;
            }

            if (LoadState == LoadStateE.LOADING)
            {
                Debug.LogWarning("正在加载中:" + mUrl);
                return;
            }

            mRetryedCount = 0;

            mLoadState = LoadStateE.LOADING;
            StartCoroutine(doLoad(mUrl));
        }
        protected void retryLoad()
        {
            Debug.LogWarning(string.Format("{0}重试:{1}", mRetryedCount, mUrl));
            mLoadState = LoadStateE.LOADING;
            StartCoroutine(doLoad(mUrl));
        }

        private IEnumerator doLoad(string url_)
        {
            UnityWebRequest request;
            switch (DataType)
            {
                case AssetResource.DataTypeE.BYTES:
                case AssetResource.DataTypeE.CONFIG:
                case AssetResource.DataTypeE.TEXTURE:
                    request = UnityWebRequest.Get(url_);
                    break;
                case AssetResource.DataTypeE.MANIFEST:
                case AssetResource.DataTypeE.ASSETBUNDLE:
                    request = UnityWebRequest.Get(url_);
                    break;
                case AssetResource.DataTypeE.POST:
                    request = UnityWebRequest.Post(url_, PostData);
                    break;
                case AssetResource.DataTypeE.GET:
                    string fullPath = url_;
                    if (string.IsNullOrEmpty(PostData) == false)
                    {
                        fullPath = url_ + "?" + PostData;
                    }
                    request = UnityWebRequest.Get(fullPath);
                    break;
                default:
                    request = UnityWebRequest.Get(url_);
                    break;
            }

            float stratTime = Time.realtimeSinceStartup;
            bool isTimeout = false;
            if (Timeout > 0)
            {
                request.timeout = Timeout;
            }
            while (!request.isDone)
            {
                if (Timeout > 0 && Time.realtimeSinceStartup - stratTime > Timeout * 2)
                {
                    isTimeout = true;
                    break;
                }

                Update(request.downloadProgress);

                yield return request.SendWebRequest();
            }

            long responseCode = request.responseCode;
            if (request.isNetworkError || (responseCode != 200 && responseCode != 204) || isTimeout)
            {
                string error = "code=" + responseCode;
                if (isTimeout)
                {
                    error += ",error=isTimeout:" + Timeout;
                }
                else if (request.isNetworkError)
                {
                    error += ",error=" + request.error;
                }
                else
                {
                    if (responseCode == 404)
                    {
                        Core.LoaderManager.SetUrl404(mUrl);
                    }
                }
                mLoadState = LoadStateE.ERROR;
                string message = string.Format("下载文件失败:{0} reason:{1}", mUrl, error);
                Debug.LogWarning(message);

                request.Dispose();
                request = null;

                if (mRetryCount < Core.LoaderManager.LoadRetryCount)
                {
                    mRetryCount++;
                    ///本身加载需要时间,所以不必later太长
                    Core.Timer.CallLater(retryLoad, 1.0f);
                    yield break;
                }

                DispatchEvent(Event.FAILED, message);
            }
            else
            {
                mLoadState = LoadStateE.COMPLETE;
                switch (DataType)
                {
                    case AssetResource.DataTypeE.BYTES:
                    case AssetResource.DataTypeE.CONFIG:
                        mData = request.downloadHandler.data;
                        DispatchEvent(Event.COMPLETE, mData);
                        break;
                    case AssetResource.DataTypeE.ASSETBUNDLE:
                    case AssetResource.DataTypeE.MANIFEST:
                        onAssetBundleHandle(AssetBundle.LoadFromMemory(request.downloadHandler.data));
                        break;
                    case AssetResource.DataTypeE.TEXTURE:
                        //linner;
                        Texture2D tex = new Texture2D(2, 2, TextureFormat.ARGB32, false, false);
                        tex.LoadImage(request.downloadHandler.data);
                        mData = tex;
                        DispatchEvent(Event.COMPLETE, mData);
                        break;
                    default:
                        mData = request.downloadHandler.data;
                        DispatchEvent(Event.COMPLETE, mData);
                        break;
                }
                request.Dispose();
                request = null;
            }
        }

    }
}
