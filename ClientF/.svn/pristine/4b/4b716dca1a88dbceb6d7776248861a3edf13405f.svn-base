using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using PlatformCore;
using UnityEngine;

namespace GameILruntime.Core
{
    /// <summary>
    /// UI同GameObject的转接类
    /// 用于所有UI控件的底层
    /// </summary>
    public class UISkin: InjectBase
    {
        public const string EVENT_BINDCOMPONENT = "event_bindComponent";
        public const string EVENT_UNBINDCOMPONENT = "event_unbindComponent";

        protected GameObject mSkin = null;
        protected RectTransform mRectTransform = null;
        protected bool mActive = true;
        protected string mName = "";
        protected Vector3 mPosition = Vector3.zero;
        protected Vector3 mScale = Vector3.one;
        protected Vector2 mSize = Vector3.zero;

        protected object mUserData = null;

        public object UserData
        {
            get { return mUserData; }
            set
            {
                mUserData = value;

                //数据刷新
                Refresh();
            }
        }

        protected virtual void doData()
        {
            
        }

        //强制刷新数据
        public void Refresh()
        {
            if (Skin == null)
                return;

            doData();
        }

        public Transform Transform
        {
            get
            {
                if (Skin == null)
                    return null;
                return Skin.transform;
            }
        }

        public RectTransform RectTransform
        {
            get
            {
                if (mSkin != null && mRectTransform == null)
                    mRectTransform = mSkin.GetComponent<RectTransform>();
                return mRectTransform;
            }
        }

        public bool Active
        {
            get { return mActive; }
            set
            {
                mActive = value;
                if (mSkin != null && mSkin.activeSelf != mActive)
                {
                    mSkin.SetActive(mActive);
                }
            }
        }

        public string Name
        {
            get
            {
                if (mSkin != null)
                    return mSkin.name;
                return mName;
            }
            set
            {
                mName = value;
                if (mSkin != null && mSkin.name != mName)
                {
                    if (string.IsNullOrEmpty(mName) == false)
                        mSkin.name = mName;
                    else
                        mName = mSkin.name;
                }
            }
        }

        public Vector3 Position
        {
            get
            {
                if (mSkin != null)
                    return mSkin.transform.localPosition;
                return mPosition;
            }
            set
            {
                mPosition = value;
                if (mSkin != null && mSkin.transform.localPosition != mPosition)
                {
                    if (mPosition != Vector3.zero)
                        mSkin.transform.localPosition = mPosition;
                    else
                        mPosition = mSkin.transform.localPosition;
                }
            }
        }

        public Vector3 Scale
        {
            get
            {
                if (mSkin != null)
                    return mSkin.transform.localScale;
                return mScale;
            }
            set
            {
                mScale = value;
                if (mSkin != null && mSkin.transform.localScale != mScale)
                {
                    if (mScale != Vector3.one)
                        mSkin.transform.localScale = mScale;
                    else
                        mScale = mSkin.transform.localScale;
                }
            }
        }

        public Vector2 Size
        {
            get
            {
                if (RectTransform != null)
                    return RectTransform.sizeDelta;
                return mSize;
            }
            set
            {
                mSize = value;
                if (RectTransform != null && RectTransform.sizeDelta != mSize)
                {
                    if (mSize != Vector2.zero)
                        RectTransform.sizeDelta = mSize;
                    else
                        mSize = RectTransform.sizeDelta;
                }
            }
        }

        public virtual GameObject Skin
        {
            get
            {
                return mSkin;
            }
            set
            {
                if (mSkin != null) 
                {
					removeEvent();
                    //如果之前已经有皮肤，则释放皮肤的所有绑定
                    unbindComponent();
                }

                //设置新的皮肤
                mSkin = value;

                if (mSkin != null)
                {
                    //可能在皮肤加载之前已经设置了某些属性
                    Active = mActive;
                    Name = mName;
                    Position = mPosition;
                    Scale = mScale;
                    Size = mSize;

                    //注入
                    inject();

                    //开始绑定
                    bindComponent();
					addEvent();

                    //刷新数据
                    Refresh();
                }
            }
        }

        public UISkin()
        {
            
        }

        public virtual GameObject FindGameObject(string pathName_)
        {
            if (Transform == null)
                return null;

            if (string.IsNullOrEmpty(pathName_) == true)
                return Skin;

	        Transform t = Transform.Find(pathName_);

	        if(t)
	        {
				GameObject go = t.gameObject;
		        return go;
			}

	        return null;
        }

        protected override void SetUIComponent(FieldInfo f_, UIComponentAttribute att_)
        {
            GameObject go = FindGameObject(att_.PathName);
            if (go == null)
            {
                Debug.LogWarning("无法获取节点:" + att_.PathName);
                return;
            }

            Type t = f_.FieldType;
            Component c = go.GetComponent(t.Name);
            if(c != null)
            {//如果是component组件，则直接赋值
                f_.SetValue(this, c);
            }
            else
            {
                if(f_.FieldType.IsSubclassOf(typeof(UISkin)) == true)
                {//是自定义的UISkin类型
                    object obj = Activator.CreateInstance(f_.FieldType);
                    UISkin skin = obj as UISkin;
                    skin.Skin = go;
                    f_.SetValue(this, obj);
                }
            }
        }

        virtual protected void bindComponent()
        {
            DispatchEvent(EVENT_BINDCOMPONENT);
        }

        virtual protected void unbindComponent()
        {
            DispatchEvent(EVENT_BINDCOMPONENT);
        }

	    virtual protected void addEvent()
	    {

	    }
	    virtual protected void removeEvent()
	    {

	    }
	}
}
