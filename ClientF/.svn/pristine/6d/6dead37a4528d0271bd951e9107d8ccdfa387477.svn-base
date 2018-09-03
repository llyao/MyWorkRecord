using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PlatformCore
{
    public class UpdateResources : EventDispatcher , ICore
    {
        public int ThreadCount { get { return 4; } }

        protected List<h> needLoadList = new List<h>();
        private List<UpdateResItem> loadingList = new List<UpdateResItem>();
        private List<h> timeOutList = new List<h>();

        protected int mLoadedNum;//已经加载的个数
        protected int mTotalNum;//需要加载的个数
        protected int mLoadedByte;//已经加载的数据大小
        protected int mTotalByte;//需要记在的数据大小

        public void Load()
        {
            needLoadList.Clear();
            loadingList.Clear();
            timeOutList.Clear();

            for (int i = 0; i < Core.VersionHash.RemoteVersionFile.hl.Count; i++)
            {
                h remoteItem = Core.VersionHash.RemoteVersionFile.hl[i];
                h localItem = Core.VersionHash.LocalVersionFile.GetByUrl(remoteItem.Url);

                if (localItem == null)
                {//本地版本文件不存在则下载
                    addNeedLoad(remoteItem);
                }
                else if (remoteItem.Hash != localItem.Hash)
                {//哈希不同
                    addNeedLoad(remoteItem);
                }
                else
                {
                    string pPath = Core.PathDefine.GetPersistentPath(remoteItem.Url);
                    string sPath = Core.PathDefine.GetStreamingAssetsPath(remoteItem.Url);
                    if (File.Exists(pPath) == false && File.Exists(sPath) == false)
                    {//本地文件不存在
                        addNeedLoad(remoteItem);
                    }
                }
            }

            if (CheckAllComplete() == false)
            {
                initNeedLoadList();
            }
        }

        /// <summary>
        /// 添加到更新列表
        /// </summary>
        /// <param name="item_"></param>
        protected virtual void addNeedLoad(h item_)
        {
            needLoadList.Add(item_);
        }

        /// <summary>
        /// 
        /// </summary>
        private void initNeedLoadList()
        {
            mLoadedNum = 0;
            mTotalNum = needLoadList.Count;
            mLoadedByte = 1;
            mTotalByte = 1;

            foreach (h item in needLoadList)
            {
                mTotalByte += item.Size;
            }
            
            showAlertNeedDownload(mTotalNum, mTotalByte);
        }
        protected virtual float getByteM(int value)
        {
            float m = (value / 1024f) / 1024f;
            return ((int)m * 1000) / 1000f;
        }

        protected virtual void showAlertNeedDownload(int total, int need)
        {
            float m = getByteM(need);

            string msg = "有" + total + "个资源(" + m + "M)文件需要联网更新,是否更新";
            Debug.Log(msg);

            Core.Timer.AddTick(OnTick);
        }

        protected void OnTick(float deltaTime_)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                DispatchEvent(Event.FAILED, "网络不可访问,请重新检查更新!");
                Core.Timer.RemoveTick(OnTick);
                AutoNetWorkListener.Start(() => { Core.Timer.AddTick(OnTick); });
                return;
            }

            //判断是否已经更新完毕
            if (CheckAllComplete() == true)
                return;

            while (loadingList.Count < ThreadCount && needLoadList.Count > 0)
            {
                h item = needLoadList.Shift();
                UpdateResItem resItem = new UpdateResItem(item);
                resItem.AddListener(Event.PROGRESS, itemProgressHandle , 0);
                Event.AddCompleteAndFail(resItem , itemCompleteHandle);
                loadingList.Add(resItem);

                float m = getByteM(item.Size);
                Debug.Log("down:" + item.Url + " size: ~" + m + "M");

                resItem.Start();
            }
        }

        private void itemProgressHandle(Event e)
        {
            this.DispatchEvent(Event.PROGRESS, mLoadedByte / (float)mTotalByte);
        }

        private void itemCompleteHandle(Event e)
        {
            UpdateResItem downloadItem = (UpdateResItem)e.Target;
            Event.RemoveCompleteAndFail(downloadItem, itemCompleteHandle);
            downloadItem.RemoveListener(Event.PROGRESS, itemProgressHandle);

            h hashItem = downloadItem.HashItem;
            loadingList.Remove(downloadItem);

            string uri = hashItem.Url;
            if (e.Type != Event.COMPLETE)
            {
                string error = (string)e.Data;
                //不存在的文件就不管了
                if (error != "404")
                {
                    timeOutList.Add(hashItem);
                }
                Debug.LogWarning("updater:" + uri + " error:" + error);
                return;
            }

            byte[] bytes = (byte[])e.Data;
            if (bytes.Length > 0)
            {
                mLoadedByte += bytes.Length;
                string filePath = Core.PathDefine.GetPersistentPath(hashItem.Url);
                try
                {
                    //将更新的数据保存到文件
                    FileFunc.AutoCreateDirectory(filePath);
                    Debug.Log("createPath:" + filePath + "   length:" + bytes.Length);
                    File.WriteAllBytes(filePath, bytes);

                    string md5 = MD5Util.MD5Byte9(bytes);
                    h localItem = Core.VersionHash.LocalVersionFile.GetByUrl(hashItem.Url);
                    if (localItem == null)
                    {
                        localItem = new h(hashItem.Url, hashItem.Size, hashItem.Hash);
                        Core.VersionHash.LocalVersionFile.AddHashItem(localItem);
                    }
                    localItem.Hash = md5;
                }
                catch (Exception ex)
                {
                    timeOutList.Add(hashItem);
                    Debug.LogWarning("writeFileError:" + ex.Message);
                }
            }
            else
            {
                timeOutList.Add(hashItem);
                Debug.LogWarning("writeFileError:文件大小为0");
            }

            DispatchEvent(Event.PROGRESS, mLoadedByte / (float)mTotalByte);

            mLoadedNum++;
            if (mLoadedNum % 10 == 0)
            {//每10个保存一下版本文件
                SaveLocalHashFile();
            }
        }

        protected void SaveLocalHashFile()
        {
            Core.VersionHash.LocalVersionFile.SaveToJsonFile(Core.PathDefine.GetPersistentPath(Core.PathDefine.VHashFileName));
        }

        /// <summary>
        /// 检查是否已经全部更新
        /// </summary>
        protected virtual bool CheckAllComplete()
        {
            if (needLoadList.Count != 0 || loadingList.Count != 0)
                return false;

            SaveLocalHashFile();

            if (timeOutList.Count > 0)
            {
                foreach (h s in timeOutList)
                {
                    needLoadList.Add(s);
                }
                timeOutList.Clear();
                return false ;
            }


            Core.Timer.RemoveTick(OnTick);

            DispatchEvent(Event.COMPLETE);

            Resources.UnloadUnusedAssets();
            GC.Collect();
            Debug.Log("updateDownloader all complete!");

            return true;
        }
    }
}
