using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PlatformCore
{
    public class InnerAssetResource:AssetResource
    {
        protected object mData;

        public override object Data
        {
            get { return mData; }
        }

        public InnerAssetResource(string url) : base(url)
        {

        }
        protected override void loadImp(int priority = 0, bool progress = false, uint retryCount = 0)
        {
            string eventType = Event.COMPLETE;
            mData = Resources.Load(mUrl) as GameObject;

            if (mData == null)
            {
                mData = "_data is null";
                eventType = Event.FAILED;

                string message = string.Format("加载文件失败,Resources文件夹下不存在:{0} ", mUrl);
                Debug.LogWarning(message);
                resourceComplete(eventType, message);
            }
            else
            {
                resourceComplete(eventType);
            }

        }
    }
}
