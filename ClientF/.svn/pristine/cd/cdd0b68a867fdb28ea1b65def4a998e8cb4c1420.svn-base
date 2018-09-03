using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace PlatformCore
{
    public class UpdateResItem : EventDispatcher
    {
        public h HashItem;
        public int downloadedBytes;
        private float preTime = 0;

        public UpdateResItem(h item_)
        {
            HashItem = item_;
        }

        public void Start()
        {
            Core.LoaderManager.StartCoroutine(doLoad());
        }

        private IEnumerator doLoad()
        {
            preTime = Time.realtimeSinceStartup;
            UnityWebRequest request = UnityWebRequest.Get(Core.PathDefine.GetRemotePath(HashItem.Url));
            AsyncOperation operation = request.SendWebRequest();
            bool isTimeout = false;
            while (!operation.isDone)
            {
                if (checkTimeout(request))
                {
                    isTimeout = true;
                    break;
                }
                yield return null;
            }

            if (isTimeout)
            {
                DispatchEvent(Event.FAILED, "timeOut");
            }
            else
            {
                long responseCode = request.responseCode;
                if (responseCode == 404)
                {
                    DispatchEvent(Event.FAILED, "404");
                }
                else if (request.isNetworkError)
                {
                    DispatchEvent(Event.FAILED, request.error);
                }
                else
                {
                    DispatchEvent(Event.COMPLETE, request.downloadHandler.data);
                }
            }

            request.Dispose();
            request = null;
        }

        private float preProgress = 0.0f;
        private int checkCount = 0;
        private bool checkTimeout(UnityWebRequest request)
        {
            float progress = request.downloadProgress;
            float time = Time.realtimeSinceStartup;
            if (time - preTime < 2.0f)
            {
                return false;
            }
            preTime = time;

            if (progress == preProgress)
            {
                checkCount++;

                if (checkCount > 2)
                {
                    return true;
                }
            }

            downloadedBytes = (int)request.downloadedBytes;
            preProgress = progress;
            DispatchEvent(Event.PROGRESS, progress);

            return false;
        }
    }
}
