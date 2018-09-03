using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Config;
namespace GameILruntime.Core
{

    public class UIManager : Singleton<UIManager>
    {
        public string ResURLPrefix = "/all/ui/";

        protected GameObject mUIRoot = null;
        public GameObject UIRoot
        {
            get
            {
                if (mUIRoot == null)
                {
                    mUIRoot = new GameObject("__UIRoot");
                }
                return mUIRoot;
            }
        }


        private List<UIMediator> uiTasks = new List<UIMediator>();
        private UIMediator mainScneTask;

        static public void Init()
        {
            Debug.Log("UIManager---Init");
            ConfigManager.Instance.OnLoadOneCompletedEvent += OnLoadConfigCompleted;
            ConfigManager.Instance.Init();
        }
        static public void OnLoadConfigCompleted()
        {
            UIManager.Instance.ShowUI<TestUIMediator>();
            Debug.Log(ConfigManager.Instance.GetInfo<AccountInfo>(1).Name.ToStringUtf8());
        }

        /// <summary>
        /// 添加一个显示模块cv
        /// </summary>
        /// <param name="controller"></param>
        public void ShowUI<T>() where T : UIMediator, new()
        {
            int index = 0;
            index = uiTasks.FindIndex(it => it.GetType().ToString().Equals(typeof(T).Name.ToString()));
            if (index >= 0)
            {
                var temp = uiTasks[index];
                uiTasks.RemoveAt(index);
                uiTasks.Add(temp);
                uiTasks[index].Show();
                return;
            }
            T controller = new T();
            uiTasks.Add(controller);
            controller.Init();
        }
        /// <summary>
        /// 关闭之前场景，打开新的场景。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void GotoSceneAndUnloadOld<T>() where T : UIMediator, new()
        {

        }

        /// <summary>
        /// 根据引用情况，销毁一个模块cv
        /// </summary>
        /// <param name="controller"></param>
        public void Destory(UIMediator controller)
        {
            if (controller == null)
            {
                Debug.LogWarning("controller==null");
                return;
            }
            if (controller.View != null)
            {
                controller.View.Destroy();
            }
            Del(controller);
        }
        /// <summary>
        /// 立即强制销毁一个模块cv
        /// </summary>
        /// <param name="controller"></param>
        public void DestroyImmediate(UIMediator controller)
        {
            if (controller == null)
            {
                Debug.LogWarning("controller==null");
                return;
            }
            if (controller.View != null)
            {
                controller.View.Destroy(true);

            }
            Del(controller);

        }
        /// <summary>
        /// 从队列删除
        /// </summary>
        /// <param name="controller"></param>
        private void Del(UIMediator controller)
        {
            uiTasks.Remove(controller);
        }
        /// <summary>
        /// 清空队列所有内容
        /// </summary>
        private void Clear()
        {
            foreach (var item in uiTasks)
            {
                if (item != null && item.View != null)
                {
                    GameObject go = item.View.Skin ;
                    UnityEngine.Object.Destroy(go);

                }
            }
            uiTasks.Clear();
        }
    }
}