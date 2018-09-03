using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PlatformCore
{
    public abstract class CoreApp:EventDispatcherMono
    {
        protected static CoreApp sInstance = null;

        public static CoreApp Instance
        {
            get { return sInstance; }
        }

        protected abstract void OnInitialCompleted();
        protected bool bInitialed = false;

        void Awake()
        {
            sInstance = this;
        }

        void Start()
        {
            //先播放logo动画文件
            //...
            OnPlayLogoComplete();
        }

        protected virtual void OnPlayLogoComplete()
        {
            Debug.Log("OnPlayLogoComplete");
            //加载ShellConfig
            Event.AddCompleteAndFail(Core.ShellConfig, OnLoadShellConfigComplete);
            Core.ShellConfig.Load();
        }

        protected virtual void OnLoadShellConfigComplete(Event e_)
        {
            Event.RemoveCompleteAndFail(Core.ShellConfig , OnLoadShellConfigComplete);
            if (e_.Type == Event.COMPLETE)
            {//加载成功
                //加载版本文件
                Event.AddCompleteAndFail(Core.VersionHash, OnLoadVersionComplete);
                Core.VersionHash.Load();
            }
        }

        protected virtual void OnLoadVersionComplete(Event e_)
        {
            Event.RemoveCompleteAndFail(Core.VersionHash, OnLoadVersionComplete);
            if (e_.Type == Event.COMPLETE)
            {//加载版本文件成功
                bInitialed = true;
                InitialComplete();
            }
        }
        
        virtual protected void InitialComplete()
        {
            if (bInitialed == false)
                return;

            OnInitialCompleted();
        }

        public void OnLoadSceneComplete(Event e_)
        {
            Core.SceneManager.RemoveListener(Event.COMPLETE, OnLoadSceneComplete);
            Core.Timer.CallLater(DestroyScene, 10.0f);
        }

        protected void DestroyScene()
        {
            Core.SceneManager.UnLoad();
            GC.Collect();
            Core.Timer.CallLater(LoadSceneAgain, 10.0f);
        }

        protected void LoadSceneAgain()
        {
            Core.SceneManager.AddListener(Event.COMPLETE, OnLoadSceneComplete, 1);
            Core.SceneManager.Load("/all/scenes/1");
        }
    }
}
