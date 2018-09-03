using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlatformCore
{
    public abstract class EventDispatcherPoolItem: IObjectPoolItem , IEventDispatcher
    {
        protected EventDispatcher mEventDispatcher = new EventDispatcher();

        public EventDispatcherPoolItem()
        {
            mEventDispatcher.mTarget = this;
        }

        public void AddListener(string type_, Action<Event> listener_ , int count_)
        {
            mEventDispatcher.AddListener(type_, listener_ , count_);
        }

        public bool HasListener(string type_)
        {
            return mEventDispatcher.HasListener(type_);
        }

        public bool DispatchEvent(Event e_)
        {
            return mEventDispatcher.DispatchEvent(e_);
        }
        public bool DispatchEvent(string type_, object data_ = null)
        {
            return mEventDispatcher.DispatchEvent(type_, data_);
        }

        public void RemoveListener(string type_, Action<Event> listener_)
        {
            mEventDispatcher.RemoveListener(type_, listener_);
        }

        public void ClearEvent()
        {
            mEventDispatcher.ClearEvent();
        }
    }
}
