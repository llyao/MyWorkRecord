using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlatformCore;

namespace GameILruntime.Core
{

    public class UIView : UISkin
    {
        /// <summary>
        /// 资源名称，由继承类或者外部设置
        /// </summary>
        protected string mResName = "";
        public string ResName { get { return mResName; } }

        /// <summary>
        /// 最好不要直接在view中直接调用model
        /// </summary>
        protected UIMediator mBaseMediator;
        public UIMediator BaseMediator { get { return mBaseMediator; } }

        public virtual void SetModel(UIMediator mediator)
        {
            mBaseMediator = mediator;
        }

        /// <summary>
        /// 资源所在路径
        /// </summary>
        public string ResUrl
        {
            get
            {
                return UIManager.Instance.ResURLPrefix + mResName ;
            }
        }

	    public string ResUrlEditor
	    {
		    get
		    {
			    return "UI/" + mResName;
		    }
	    }

        /// <summary>
        /// 是否处于加载过程中
        /// </summary>
        protected bool mBLoading = false;
        public bool BLoading { get { return mBLoading; } }

        protected AssetResource mResource = null;

        //---------------------------------------------------------------------------

        public UIView()
        {
            
        }

        //开始异步加载皮肤
        virtual public bool LoadSkin()
        {
            if (string.IsNullOrEmpty(ResName) == true)
                return false;

            //已经处于加载过程中则直接返回
            if (BLoading == true)
                return true ;

#if UNITY_EDITOR

	        var go = Resources.Load<GameObject>(ResUrlEditor);
	        SetSkin(Object.Instantiate(go));
#else
						if (mResource != null)
            {//之前加载过皮肤则直接释放
                mResource = null;
            }

            mResource = PlatformCore.Core.AssetManager.GetResource(true , ResUrl, AssetResource.DataTypeE.PREFAB);
            PlatformCore.Event.AddCompleteAndFail(mResource , onLoadSkinComplete);
            mResource.Load();
#endif

			return true;
        }


        protected void onLoadSkinComplete(PlatformCore.Event e_)
        {
            mBLoading = false;
            AssetResource ar = e_.Target as AssetResource;
            PlatformCore.Event.RemoveCompleteAndFail(ar, onLoadSkinComplete);

            if (e_.Type == PlatformCore.Event.FAILED)
            {
                Debug.Log("加载UI资源失败:" + ResUrl);
                DispatchEvent(PlatformCore.Event.FAILED);
                return;
            }

            //绑定皮肤
            GameObject go = ar.InstantiatePrefab() as GameObject;
	        SetSkin(go);
        }

	    private void SetSkin(GameObject go)
	    {
			Skin = go;

		    //调用全都准备好事件
		    onReady();
		}

        virtual protected void onReady()
        {
            DispatchEvent(PlatformCore.Event.COMPLETE);
        }

        public void Destroy(bool bImmediate_ = false)
        {
            mBaseMediator = null;
            if(Skin != null)
            {
                if(bImmediate_ == false)
                    UnityEngine.Object.Destroy(Skin);
                else
                    UnityEngine.Object.DestroyImmediate(Skin);
            }
        }

        override protected void bindComponent()
        {
            base.bindComponent();
        }

        override protected void unbindComponent()
        {
            base.unbindComponent();
        }
    }
}
