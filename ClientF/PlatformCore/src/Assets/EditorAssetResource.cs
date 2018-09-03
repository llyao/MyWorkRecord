using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PlatformCore
{
    public class EditorAssetResource:AssetResource
    {
        protected object mData;

        public override object Data
        {
            get { return mData; }
        }

        public EditorAssetResource(string url) : base(url)
        {

        }
        protected override void loadImp(int priority = 0, bool progress = false, uint retryCount = 0)
        {
            string eventType = Event.COMPLETE;


#if UNITY_EDITOR
            mData = UnityEditor.AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(mUrl);
            if (mData == null)
            {
                mData = Resources.Load(mUrl);
            }
#else
               mData = Resources.Load(mUrl);
#endif


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
