using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlatformCore
{
    public class SceneItem : EventDispatcher
    {
        public enum StateE
        {
            Null ,
            Loading,
            Unloading,
            Complete
        }

        public string SceneName = "";
        public string SceneUrl = "";
        public StateE State = StateE.Null;
        public Scene  Scene ;

        protected Coroutine mLoadSceneCoroutine = null;
        protected AsyncOperation mUnloadSceneAsyncOperation;

        public void Load()
        {
            if (State == StateE.Complete)
            {
                DispatchEvent(Event.COMPLETE);
                return;
            }
            if (State == StateE.Loading)
                return;

            State = StateE.Loading;
            
            AssetResource resource = Core.AssetManager.GetResource(true , SceneUrl, AssetResource.DataTypeE.PREFAB);
            resource.AddListener(Event.PROGRESS, progressHandle , 0);
            Event.AddCompleteAndFail(resource, OnLoadSceneComplete);
            resource.Load(3, true);
        }

        protected void progressHandle(Event e_)
        {
            
        }

        protected void OnLoadSceneComplete(Event e_)
        {
            AssetResource resource = (AssetResource)e_.Target;
            resource.RemoveListener(Event.PROGRESS, progressHandle);
            Event.RemoveCompleteAndFail(resource, OnLoadSceneComplete);

            if (e_.Type != Event.COMPLETE)
            {
                DispatchEvent(Event.FAILED);
                return;
            }

            string[] list = resource.AllScenePaths;
            if (list == null || list.Length == 0)
            {
                Debug.LogError("没有加载到场景");
                DispatchEvent(Event.FAILED);
                return;
            }
            string sceneName = Path.GetFileNameWithoutExtension(list[0]);
            if (string.IsNullOrEmpty(sceneName) == true)
            {
                Debug.LogError("没有加载到场景");
                DispatchEvent(Event.FAILED);
                return;
            }
            Core.SceneManager.StartCoroutine(loadSceneCoroutine(sceneName));
        }

        protected IEnumerator loadSceneCoroutine(string sceneName)
        {
            if (Core.SceneManager.IsUnLoading() == true)
                yield return null;

            //加载场景
            AsyncOperation async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            while (!async.isDone)
            {
                DispatchEvent(Event.PROGRESS, async.progress);
                yield return null;
            }

            Scene = SceneManager.GetSceneByName(sceneName);
            while (!SceneManager.SetActiveScene(Scene))
            {
                yield return null;
            }

            foreach (GameObject gameObject in Scene.GetRootGameObjects())
            {
                if (Application.isEditor)
                {//重新绑定shader
                    RenderUtils.ShaderRebind(gameObject);
                }
            }
            //重新绑定天空盒
            if (Application.isEditor)
            {
                RenderUtils.RebindMaterial(RenderSettings.skybox);
            }

            State = StateE.Complete;
            DispatchEvent(Event.COMPLETE);
        }

        public void UnLoad()
        {
            if (Scene == null)
                return;
            if (Scene.IsValid() == false)
                return;

            //开始卸载
            State = StateE.Unloading;
            Core.SceneManager.StartCoroutine(unloadSceneCoroutine());
        }

        protected IEnumerator unloadSceneCoroutine()
        {
            //异步卸载场景
            mUnloadSceneAsyncOperation = SceneManager.UnloadSceneAsync(Scene);
            while (!mUnloadSceneAsyncOperation.isDone)
            {
                yield return null;
            }
            //设置未加载状态
            State = StateE.Null;

            DispatchEvent(Event.UNLOAD_COMPLETE);
        }

        public bool IsUnLoading()
        {
            if (mUnloadSceneAsyncOperation == null)
                return false ;
            if (mUnloadSceneAsyncOperation.isDone == true)
            {
                mUnloadSceneAsyncOperation = null;
                return false;
            }
            return true;
        }
    }

    public class CoreSceneManager : EventDispatcherMono, ICore
    {
        protected Dictionary<string , SceneItem> mLoadedSceneMap = new Dictionary<string, SceneItem>();

        public Func<string, string> GetScenePathCB;

        public string GetScenePath(string name_)
        {
            return name_ ;
        }

        protected string mCurLoadSceneName = "";
        public void Load(string sceneName_ , bool bUnLoadPreScene_ = true)
        {
            mCurLoadSceneName = sceneName_;
            if (bUnLoadPreScene_ == true)
            {//先卸载之前的场景
                this.AddListener(Event.UNLOAD_COMPLETE , OnUnLoadSceneComplete , 0);
                this.UnLoad();
            }
            else
            {
                loadScene();
            }
            
        }

        protected void OnUnLoadSceneComplete(Event e_)
        {
            //之前的场景卸载成功
            this.RemoveListener(Event.UNLOAD_COMPLETE, OnUnLoadSceneComplete);
            //开始加载新场景
            loadScene();
        }

        protected void loadScene()
        {
            if (string.IsNullOrEmpty(mCurLoadSceneName) == true)
                return;

            string sceneName = mCurLoadSceneName;
            mCurLoadSceneName = "";

            if (mLoadedSceneMap.ContainsKey(sceneName) == true)
            {//如果是已经加载的场景
                SceneItem item = mLoadedSceneMap[sceneName];
                if (item.State == SceneItem.StateE.Complete)
                {//已经加载成功
                    DispatchEvent(Event.COMPLETE, item.SceneName);
                }
                return;
            }

            if (GetScenePathCB == null)
                GetScenePathCB = GetScenePath;
            SceneItem sceneItem = new SceneItem();
            sceneItem.SceneName = sceneName;
            sceneItem.SceneUrl = GetScenePathCB(sceneName);
            mLoadedSceneMap[sceneName] = sceneItem;
            Event.AddCompleteAndFail(sceneItem, OnLoadSceneComplete);
            sceneItem.Load();
        }

        protected void OnLoadSceneComplete(Event e_)
        {
            Event.RemoveCompleteAndFail(e_.Target , OnLoadSceneComplete);

            DispatchEvent(e_);
            if (e_.Type == Event.COMPLETE)
            {
                
            }
            else
            {
                
            }
        }

        public void UnLoad(string sceneName_ = "")
        {
            if (string.IsNullOrEmpty(sceneName_) == true)
            {//卸载所有场景
                if (mLoadedSceneMap.Count == 0)
                {//之前场景格式为零则直接返回成功
                    DispatchEvent(Event.UNLOAD_COMPLETE);
                }
                else
                {
                    foreach (var item in mLoadedSceneMap)
                    {
                        item.Value.AddListener(Event.UNLOAD_COMPLETE, OnUnloadAllSceneComplete, 1);
                        item.Value.UnLoad();
                    }
                }
            }
            else
            {
                if (mLoadedSceneMap.ContainsKey(sceneName_) == true)
                {//指定场景存在则开始卸载
                    mLoadedSceneMap[sceneName_].UnLoad();
                    mLoadedSceneMap[sceneName_].AddListener(Event.UNLOAD_COMPLETE , OnUnloadOneSceneComplete, 1);
                }
                else
                {//指定场景不存在则直接返回成功
                    DispatchEvent(Event.UNLOAD_COMPLETE);
                }
            }
        }

        protected void OnUnloadOneSceneComplete(Event e_)
        {
            SceneItem item = e_.Target as SceneItem;

            item.RemoveListener(Event.UNLOAD_COMPLETE , OnUnloadOneSceneComplete);
            DispatchEvent(Event.UNLOAD_COMPLETE);

            mLoadedSceneMap.Remove(item.SceneName);
        }

        protected void OnUnloadAllSceneComplete(Event e_)
        {
            SceneItem item = e_.Target as SceneItem;
            item.RemoveListener(Event.UNLOAD_COMPLETE, OnUnloadAllSceneComplete);

            mLoadedSceneMap.Remove(item.SceneName);
            if (IsUnLoading() == false)
                DispatchEvent(Event.UNLOAD_COMPLETE);
        }

        public bool IsUnLoading()
        {
            foreach (var item in mLoadedSceneMap)
            {
                if (item.Value.IsUnLoading() == true)
                    return true;
            }
            return false;
        }
    }
}
