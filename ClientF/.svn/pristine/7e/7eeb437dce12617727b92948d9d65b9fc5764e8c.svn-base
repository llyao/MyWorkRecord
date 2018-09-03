using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;

namespace PlatformCore
{
    public class Event : ObjectPoolItem<Event>
    {
        public static void AddCompleteAndFail(IEventDispatcher dispatcher_, Action<Event> handle)
        {
            dispatcher_.AddListener(Event.COMPLETE, handle, 1);
            dispatcher_.AddListener(Event.FAILED, handle, 1);
        }
        public static void RemoveCompleteAndFail(IEventDispatcher dispatcher_, Action<Event> handle)
        {
            dispatcher_.RemoveListener(Event.COMPLETE, handle);
            dispatcher_.RemoveListener(Event.FAILED, handle);
        }


        public const string COMPLETE = "event_complete";
        public const string FAILED = "event_failed";
        public const string DISPOSE = "event_dispose";
        public const string PROGRESS = "event_progress";
        public const string CANCEL = "event_cancel";
        public const string UNLOAD_COMPLETE = "unload_complete";

        public const string TIMER_UPDATE = "timer_update";

        protected string mType;
        protected object mData;

        protected IEventDispatcher mTarget;

        public string Type { get { return mType; } }
        public object Data { get { return mData; } }
        public IEventDispatcher Target { get { return mTarget; } }

        public Event()
        {
            
        }

        public static Event New(string type_ , object data_)
        {
            Event retE = ObjectPool<Event>.New();
            retE.Init(type_, data_);
            return retE;
        }

        public static void Delete(ref Event e_)
        {
            ObjectPool<Event>.Delete(e_);
            e_ = null;
        }
        
        public void Init(string type_, object data_)
        {
            mType = type_;
            mData = data_;
        }

        public void SetTarget(IEventDispatcher target_)
        {
            mTarget = target_;
        }

        internal override void poolInitial()
        {
            mType = "";
            mData = null;
            mTarget = null;
        }

        internal override void poolRecycle()
        {
            mType = "";
            mData = null;
            mTarget = null;
        }
    }
}
