using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PlatformCore
{
    public class Core : EventDispatcherMono
    {
        protected static Core sInstance = null;

        public static Core Instance
        {
            get
            {
                if (sInstance == null)
                {
                    GameObject go = new GameObject("__Core");
                    GameObject.DontDestroyOnLoad(go);
                    sInstance = go.AddComponent<Core>();
                }
                return sInstance;
            }
        }

        protected List<ICore> mCoreLst = new List<ICore>();
        
        protected CoreTimer mCoreTimer;
        
        public static CoreTimer Timer
        {
            get
            {
                if (Instance.mCoreTimer == null)
                {
                    Instance.mCoreTimer = Instance.gameObject.AddComponent<CoreTimer>();
                    Instance.mCoreLst.Add(Instance.mCoreTimer);
                }
                return Instance.mCoreTimer;
            }
        }

        protected CoreLoaderManager mLoaderManager;

        public static CoreLoaderManager LoaderManager
        {
            get
            {
                if (Instance.mLoaderManager == null)
                {
                    Instance.mLoaderManager = Instance.gameObject.AddComponent<CoreLoaderManager>();
                    Instance.mCoreLst.Add(Instance.mLoaderManager);
                }
                return Instance.mLoaderManager;
            }
        }

        protected CorePathDefine mCorePathDefine;

        public static CorePathDefine PathDefine
        {
            get
            {
                if (Instance.mCorePathDefine == null)
                {
                    Instance.mCorePathDefine = Instance.gameObject.AddComponent<CorePathDefine>();
                    Instance.mCoreLst.Add(Instance.mCorePathDefine);
                }
                return Instance.mCorePathDefine;
            }
        }

        protected CoreAssetsManager mAssetsManager;

        public static CoreAssetsManager AssetManager
        {
            get
            {
                if (Instance.mAssetsManager == null)
                {
                    Instance.mAssetsManager = Instance.gameObject.AddComponent<CoreAssetsManager>();
                    Instance.mCoreLst.Add(Instance.mAssetsManager);
                }
                return Instance.mAssetsManager;
            }
        }

        protected CoreVersionHash mVersionHash;

        public static CoreVersionHash VersionHash
        {
            get
            {
                if (Instance.mVersionHash == null)
                {
                    Instance.mVersionHash = Instance.gameObject.AddComponent<CoreVersionHash>();
                    Instance.mCoreLst.Add(Instance.mVersionHash);
                }
                return Instance.mVersionHash;
            }
        }

        protected GameObject mPoolContainer;
        public static GameObject PoolContainer
        {
            get
            {
                if (Instance.mPoolContainer == null)
                {
                    Instance.mPoolContainer = new GameObject("__PoolContainer");
                }
                return Instance.mPoolContainer;
            }
        }

        protected AssetResourceRefPool mAssetResourceRefPool;

        public static AssetResourceRefPool AssetResourceRefPool
        {
            get
            {
                if (Instance.mAssetResourceRefPool == null)
                {
                    Instance.mAssetResourceRefPool = new AssetResourceRefPool();
                    Instance.mCoreLst.Add(Instance.mAssetResourceRefPool);
                }
                return Instance.mAssetResourceRefPool;
            }
        }

        protected CoreShellConfig mShellConfig;

        public static CoreShellConfig ShellConfig
        {
            get
            {
                if (Instance.mShellConfig == null)
                {
                    Instance.mShellConfig = Instance.gameObject.AddComponent<CoreShellConfig>();
                    Instance.mCoreLst.Add(Instance.mShellConfig);
                }
                return Instance.mShellConfig;
            }
        }

        protected UpdateResources mUpdateResources;

        public static UpdateResources UpdateResource
        {
            get
            {
                if (Instance.mUpdateResources == null)
                {
                    Instance.mUpdateResources = new UpdateResources();
                    Instance.mCoreLst.Add(Instance.mUpdateResources);
                }
                return Instance.mUpdateResources;
            }
        }

        protected CoreSceneManager mSceneManager;

        public static CoreSceneManager SceneManager
        {
            get
            {
                if (Instance.mSceneManager == null)
                {
                    Instance.mSceneManager = Instance.gameObject.AddComponent<CoreSceneManager>();
                    Instance.mCoreLst.Add(Instance.mSceneManager);
                }
                return Instance.mSceneManager;
            }
        }

        protected CoreABPool mABPool;

        public static CoreABPool ABPool
        {
            get
            {
                if (Instance.mABPool == null)
                {
                    Instance.mABPool = new CoreABPool();
                    Instance.mCoreLst.Add(Instance.mABPool);
                }
                return Instance.mABPool;
            }
        }

        protected CoreDeletePool mDeletePool;

        public static CoreDeletePool DeletePool
        {
            get
            {
                if (Instance.mDeletePool == null)
                {
                    Instance.mDeletePool = new CoreDeletePool();
                    Instance.mCoreLst.Add(Instance.mDeletePool);
                }
                return Instance.mDeletePool;
            }
        }
    }
}
