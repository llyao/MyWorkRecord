using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlatformCore;
using System;
using System.Reflection;

namespace GameILruntime.Core
{

    public class UIMediator: InjectBase
    {
        private UIView mBaseView;
        public UIView View { get { return mBaseView; } }
        protected override void SetView(UIView view_)
        {
            mBaseView = view_;
            mBaseView.SetModel(this);
        }

        /// <summary>
        /// 初始化，加载对应的prefab，完成之后挂载对应的UIView
        /// </summary>
        public virtual void Init()
        {
            //数据注入
            inject();

            PlatformCore.Event.AddCompleteAndFail(mBaseView, skinLoadedCompleted);

            //开始加载皮肤
            if(mBaseView.LoadSkin() == false)
            {//无法加载皮肤直接退出并且删除自身
                PlatformCore.Event.RemoveCompleteAndFail(mBaseView, skinLoadedCompleted);
                UIManager.Instance.DestroyImmediate(this);
                Debug.LogError("无法家在皮肤");
                return;
            }

            mBaseView.AddListener(UISkin.EVENT_BINDCOMPONENT , OnViewMsgReceiver, 1);
        }

        private void OnViewMsgReceiver(PlatformCore.Event e_)
        {
            if (e_.Type == UISkin.EVENT_BINDCOMPONENT)
            {
                this.bindComponent();
            }
            else if (e_.Type == UISkin.EVENT_UNBINDCOMPONENT)
            {
                this.unbindComponent();
            }
        }

        private void skinLoadedCompleted(PlatformCore.Event e_)
        {
            PlatformCore.Event.RemoveCompleteAndFail(mBaseView, skinLoadedCompleted);
            if(e_.Type == PlatformCore.Event.COMPLETE)
                OnLoadedCompleted(true);
            else
                OnLoadedCompleted(false);
        }

        public virtual void Show()
        {

        }
        public virtual void Hide()
        {

        }
        /// <summary>
        /// 加载完成之后的回调，挂载对应的view。
        /// </summary>
        public virtual void OnLoadedCompleted(bool success_)
        {
        }
        public virtual void Destory()
        {
            UnityEngine.Object.Destroy(mBaseView.Skin.gameObject);
            UIManager.Instance.Destory(this);
        }
        public virtual void DestroyImmediate()
        {
            UnityEngine.Object.DestroyImmediate(mBaseView.Skin.gameObject);
            UIManager.Instance.DestroyImmediate(this);

        }

        protected virtual void bindComponent()
        {
            
        }

        protected virtual void unbindComponent()
        {
            
        }
    }
}