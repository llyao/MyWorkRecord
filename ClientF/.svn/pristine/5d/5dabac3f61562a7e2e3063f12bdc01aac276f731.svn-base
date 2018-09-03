using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PlatformCore
{
    [Serializable]
    public class h
    {
        public string Url;
        public int Size;
        public string Hash;

        public h(string uri_ , int size_ , string hash_)
        {
            Url = uri_;
            Size = size_;
            Hash = hash_;
        }

        public long GetSortWeight()
        {
            return Size;
        }
    }

    [Serializable]
    public class VersionFile
    {
        public List<h> hl = new List<h>(); 
        private Dictionary<string , h> hm = new Dictionary<string, h>();

        public VersionFile()
        {
            
        }

        public h GetByUrl(string url_)
        {
            h retH = null;

            if (hm.TryGetValue(url_, out retH) == true)
                return retH;

            return null;
        }

        public void MakeMapFromList()
        {
            hm.Clear(); 
            for (int i = 0; i < hl.Count; i++)
            {
                hm[hl[i].Url] = hl[i];
            }
        }

        public void AddHashItem(h item_)
        {
            if (hm.ContainsKey(item_.Url) == true)
                return;

            hm.Add(item_.Url, item_);
            hl.Add(item_);
        }

        protected void Init()
        {
            hl.Clear();
            hm.Clear();
        }

        protected void MakeMap()
        {
            hm.Clear();
            foreach (h item in hl)
            {
                hm[item.Url] = item;
            }
        }

        /// <summary>
        /// 通过v.txt读取数据
        /// </summary>
        /// <param name="str_"></param>
        public void LoadFromVFile(string str_)
        {
            Init();

            if (string.IsNullOrEmpty(str_) == true)
                return;

            StringReader reader = new StringReader(str_);
            string key = reader.ReadLine();
            string[] keyHash;
            while (key != null)
            {
                keyHash = key.Split(':');

                int size = 0;
                int.TryParse(keyHash[2], out size);

                h hashFile = new h(keyHash[0] , size , keyHash[1]);

                hm[hashFile.Url] = hashFile;
                hl.Add(hashFile);

                key = reader.ReadLine();
            }
        }

        public void SaveToJsonFile(string filePath_)
        {
            FileFunc.SaveObject(this, filePath_);
        }

        public static VersionFile LoadFromJsonFile(string filePath_)
        {
            VersionFile vf = FileFunc.LoadObject<VersionFile>(filePath_);
            vf.MakeMap();
            return vf;
        }

        public static VersionFile LoadFromJsonStr(string str_)
        {
            VersionFile obj = JsonUtility.FromJson<VersionFile>(str_);
            obj.MakeMapFromList();
            return obj;
        }
    }

    public class CoreVersionHash : EventDispatcherMono , ICore
    {

        public VersionFile LocalVersionFile = new VersionFile();
        public VersionFile RemoteVersionFile = new VersionFile();

        public void Load()
        {
            //加载本地版本文件
            AssetResource ar = Core.AssetManager.GetResource(false , Core.PathDefine.VHashFileName , AssetResource.DataTypeE.BYTES);
            Event.AddCompleteAndFail(ar, OnLoadLoaclVersionComplete);
            ar.Load();
        }

        /// <summary>
        /// 加载本地hash文件
        /// </summary>
        /// <param name="e_"></param>
        protected void OnLoadLoaclVersionComplete(Event e_)
        {
            if (e_.Type == Event.COMPLETE)
            {//加载成功
                string v = Encoding.UTF8.GetString(e_.Data as byte[]);
                AssetResource a = e_.Target as AssetResource;
                if (a.FromType == AssetResource.FromTypeE.StreamingAssets)
                {//从包内加载的
                    LocalVersionFile.LoadFromVFile(v);
                }
                else
                {
                    LocalVersionFile = VersionFile.LoadFromJsonStr(v);
                }
            }
            else
            {//加载失败
                
            }

            //强制加载远程版本文件
            AssetResource ar = Core.AssetManager.GetResource(false , Core.PathDefine.GetRemotePath(Core.PathDefine.VHashFileName), AssetResource.DataTypeE.BYTES);
            Event.AddCompleteAndFail(ar, OnLoadRemoteVersionComplete);
            ar.IsForceRemote = true;
            ar.Load();
        }

        /// <summary>
        /// 加载远程哈希文件
        /// </summary>
        /// <param name="e_"></param>
        protected void OnLoadRemoteVersionComplete(Event e_)
        {
            if (e_.Type == Event.COMPLETE)
            {//加载成功
                string v = Encoding.UTF8.GetString(e_.Data as byte[]);
                RemoteVersionFile.LoadFromVFile(v);
                Event.AddCompleteAndFail(Core.UpdateResource , OnLoadResourceComplete);
                Core.UpdateResource.Load();
            }
            else
            {//加载失败
                DispatchEvent(Event.FAILED, null);
            }
        }

        /// <summary>
        /// 更新资源成功
        /// </summary>
        protected void OnLoadResourceComplete(Event e_)
        {
            Event.RemoveCompleteAndFail(Core.UpdateResource, OnLoadResourceComplete);
            if (e_.Type == Event.COMPLETE)
            {
                DispatchEvent(Event.COMPLETE, null);
            }
            else if (e_.Type == Event.FAILED)
            {
                DispatchEvent(Event.FAILED, null);
            }
        }
    }
}
