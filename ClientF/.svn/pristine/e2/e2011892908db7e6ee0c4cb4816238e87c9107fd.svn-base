using UnityEngine;
using System.Collections.Generic;
using ILRuntime.Other;
using System;
using System.Collections;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.CLR.Method;
using PlatformCore;

public class Adapt_EventDispatcher : CrossBindingAdaptor
{
    public override Type BaseCLRType
    {
        get
        {
            return typeof(EventDispatcher);
        }
    }

    public override Type AdaptorType
    {
        get
        {
            return typeof(Adaptor);
        }
    }
    public override object CreateCLRInstance(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
    {
        return new Adaptor(appdomain, instance);
    }
    //为了完整实现MonoBehaviour的所有特性，这个Adapter还得扩展，这里只抛砖引玉，只实现了最常用的Awake, Start和Update
    public class Adaptor : EventDispatcher, CrossBindingAdaptorType
    {
        ILTypeInstance instance;
        ILRuntime.Runtime.Enviorment.AppDomain appdomain;

        public Adaptor()
        {

        }

        public Adaptor(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
        {
            this.appdomain = appdomain;
            this.instance = instance;
        }

        public ILTypeInstance ILInstance { get { return instance; } set { instance = value; } }

        public ILRuntime.Runtime.Enviorment.AppDomain AppDomain { get { return appdomain; } set { appdomain = value; } }

        IMethod mMethodGetListener;
        bool meMethodGotGetListener;
        public EventListener GetListener(string type_)
        {
            //Unity会在ILRuntime准备好这个实例前调用Awake，所以这里暂时先不掉用
            if (instance != null)
            {
                if (!meMethodGotGetListener)
                {
                    mMethodGetListener = instance.Type.GetMethod("GetListener", 1);
                    meMethodGotGetListener = true;
                }

                if (mMethodGetListener != null)
                {
                   return appdomain.Invoke(mMethodGetListener, instance, type_) as EventListener;
                }
            }
            return null;

        }

        IMethod mMethodAddListener;
        bool meMethodAddListener;
        public void AddListener(string type_, Action<PlatformCore.Event> listener_)
        {
            //Unity会在ILRuntime准备好这个实例前调用Awake，所以这里暂时先不掉用
            if (instance != null)
            {
                if (!meMethodAddListener)
                {
                    mMethodAddListener = instance.Type.GetMethod("GetListener",2);
                    meMethodAddListener = true;
                }

                if (mMethodAddListener != null)
                {
                     appdomain.Invoke(mMethodAddListener, instance, type_, listener_);
                }
            }

        }
        IMethod mMethodDispatchEvent;
        bool meMethodDispatchEvent;
        public bool DispatchEvent(PlatformCore.Event e_)
        {
            //Unity会在ILRuntime准备好这个实例前调用Awake，所以这里暂时先不掉用
            if (instance != null)
            {
                if (!meMethodDispatchEvent)
                {
                    mMethodDispatchEvent = instance.Type.GetMethod("DispatchEvent", 1);
                    meMethodDispatchEvent = true;
                }

                if (mMethodDispatchEvent != null)
                {
                  return  (bool)appdomain.Invoke(mMethodDispatchEvent, instance, e_);
                }
            }
            return false;

        }
        IMethod mMethodDispatchEvent2;
        bool meMethodDispatchEvent2;
        public bool DispatchEvent(string type_, object data_ = null)
        {
            //Unity会在ILRuntime准备好这个实例前调用Awake，所以这里暂时先不掉用
            if (instance != null)
            {
                if (!meMethodDispatchEvent2)
                {
                    mMethodDispatchEvent2 = instance.Type.GetMethod("DispatchEvent", 2);
                    meMethodDispatchEvent2 = true;
                }

                if (mMethodDispatchEvent2 != null)
                {
                    return (bool)appdomain.Invoke(mMethodDispatchEvent2, instance, type_, data_);
                }
            }
            return false;

        }

        IMethod mMethodHasListener;
        bool meMethodHasListener;
        public bool HasListener(string type_)
        {
            //Unity会在ILRuntime准备好这个实例前调用Awake，所以这里暂时先不掉用
            if (instance != null)
            {
                if (!meMethodHasListener)
                {
                    mMethodHasListener = instance.Type.GetMethod("HasListener", 1);
                    meMethodHasListener = true;
                }

                if (mMethodHasListener != null)
                {
                    return (bool)appdomain.Invoke(mMethodHasListener, instance, type_);
                }
            }
            return false;

        }
        IMethod mMethodRemoveListener;
        bool meMethodRemoveListener;
        public void RemoveListener(string type_, Action<PlatformCore.Event> listener_)
        {
            //Unity会在ILRuntime准备好这个实例前调用Awake，所以这里暂时先不掉用
            if (instance != null)
            {
                if (!meMethodRemoveListener)
                {
                    mMethodRemoveListener = instance.Type.GetMethod("RemoveListener", 2);
                    meMethodRemoveListener = true;
                }

                if (mMethodRemoveListener != null)
                {
                    appdomain.Invoke(mMethodRemoveListener, instance, type_, listener_);
                }
            }

        }
        IMethod mMethodClearEvent;
        bool meMethodClearEvent;
        public void ClearEvent()
        {
            //Unity会在ILRuntime准备好这个实例前调用Awake，所以这里暂时先不掉用
            if (instance != null)
            {
                if (!meMethodClearEvent)
                {
                    mMethodClearEvent = instance.Type.GetMethod("RemoveListener", 2);
                    meMethodClearEvent = true;
                }

                if (mMethodClearEvent != null)
                {
                    appdomain.Invoke(mMethodClearEvent, instance, null);
                }
            }

        }

    }
}
