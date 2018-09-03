using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlatformCore
{
    public class EventDispatcher : IEventDispatcher
    {
        protected Dictionary<string , EventListener> mEventListener = new Dictionary<string, EventListener>();
        public IEventDispatcher mTarget = null;

        public EventDispatcher()
        {
        }

        public EventListener GetListener(string type_)
        {
            EventListener retEL = null;
            if (mEventListener.TryGetValue(type_, out retEL) == false)
            {
                retEL = new EventListener();
                mEventListener.Add(type_, retEL);
            }
            return retEL;
        }

        public void AddListener(string type_, Action<Event> listener_ , int count_)
        {
            EventListener retEL = GetListener(type_);
            retEL.Add(listener_ , count_);
        }

        public bool DispatchEvent(Event e_)
        {
            if (e_ == null)
                return false;

            EventListener el = GetListener(e_.Type);
            if (el == null)
                return false;

            IEventDispatcher previousTarget = e_.Target;

            //如果是中转消息器则设置原始的发送者
            if (mTarget != null)
                e_.SetTarget(mTarget);
            else
                e_.SetTarget(this);

            el.DispatchEvent(e_);

            e_.SetTarget(previousTarget);
            
            return true;
        }

        public bool DispatchEvent(string type_, object data_ = null)
        {
            Event e = Event.New(type_, data_);

            bool retB = DispatchEvent(e);

            Event.Delete(ref e);

            return retB;
        }

        public bool HasListener(string type_)
        {
            EventListener el = GetListener(type_);
            if (el == null)
                return false;
            return el.Count > 0;
        }

        public void RemoveListener(string type_, Action<Event> listener_)
        {
            if (listener_ == null)
                return ;

            EventListener el = GetListener(type_);
            if (el == null)
                return ;

            el.Remove(listener_);
        }

        public void ClearEvent()
        {
            foreach(KeyValuePair<string , EventListener> item in mEventListener)
            {
                if (item.Value != null)
                {
                    item.Value.ClearEvent();
                }
            }
            mEventListener.Clear();
        }
    }
}
