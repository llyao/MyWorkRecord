using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlatformCore
{
    public class EventListener
    {
        public class EventCBItem : ObjectPoolItem<EventCBItem>
        {
            protected Action<Event> mWeakCB = null;

            public Action<Event> mCB
            {
                get
                {
                    return mWeakCB;
                }
                set
                {
                    mWeakCB = value;
                }
            }
            public int mCount = 0;

            public static EventCBItem New(Action<Event> cb_, int count_)
            {
                EventCBItem item = ObjectPool<EventCBItem>.New();
                item.mCB = cb_;
                item.mCount = count_;
                return item;
            }

            public static void Delete(ref EventCBItem item_)
            {
                ObjectPool<EventCBItem>.Delete(item_);
                item_ = null;
            }

            internal override void poolInitial()
            {
                mCB = null;
                mCount = 1;
            }

            internal override void poolRecycle()
            {
                mCB = null;
                mCount = 1;
            }
        }

        protected List<EventCBItem> mListenerLst = new List<EventCBItem>();
        protected List<EventCBItem> mTempLst = new List<EventCBItem>();

        public int Count
        {
            get
            {
                for (int i = 0; i < mListenerLst.Count; i++)
                {
                    if (mListenerLst[i] == null)
                    {
                        mListenerLst.RemoveAt(i);
                        i--;
                    }
                }
                return mListenerLst.Count;
            }
        }

        public void Add(Action<Event> listener_, int count_)
        {
            for (int i = 0; i < mListenerLst.Count; i++)
            {
                if (mListenerLst[i].mCB == listener_)
                    return;
            }

            mListenerLst.Add(EventCBItem.New(listener_ , count_));
        }

        public void Remove(Action<Event> listener_)
        {
            for (int i = 0; i < mListenerLst.Count; i++)
            {
                if (mListenerLst[i].mCB == listener_ || mListenerLst[i].mCB == null)
                {
                    EventCBItem item = mListenerLst[i];
                    mListenerLst[i] = null;
                    EventCBItem.Delete(ref item);
                    mListenerLst.RemoveAt(i);
                    i--;
                }
            }
        }

        public void DispatchEvent(Event e_)
        {
            mTempLst.Clear(); 
            for (int i = 0; i < mListenerLst.Count; i++)
            {
                if(mListenerLst[i].mCB != null)
                {
                    mTempLst.Add(mListenerLst[i]);
                }
                else
                {
                    EventCBItem item = mListenerLst[i];
                    mListenerLst[i] = null;
                    EventCBItem.Delete(ref item);
                    mListenerLst.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < mTempLst.Count; i++)
            {
                if(mTempLst[i] != null)
                {
                    if(mTempLst[i].mCB != null)
                        mTempLst[i].mCB(e_);
                    if(mTempLst[i].mCount > 1)
                        mTempLst[i].mCount--;
                }
            }

            for (int i = 0; i < mListenerLst.Count; i++)
            {
                if (mListenerLst[i].mCount == 1)
                {
                    EventCBItem item = mListenerLst[i];
                    mListenerLst[i] = null;
                    EventCBItem.Delete(ref item);
                    mListenerLst.RemoveAt(i);
                    i--;
                }
            }
        }

        public bool BHaveListener(Action<Event> listener_)
        {
            for (int i = 0; i < mListenerLst.Count; i++)
            {
                if (mListenerLst[i].mCB == listener_)
                    return true;
            }
            return false;
        }

        public void ClearEvent()
        {
            for (int i = 0; i < mListenerLst.Count; i++)
            {
                EventCBItem item = mListenerLst[i];
                mListenerLst[i] = null;
                EventCBItem.Delete(ref item);
            }
            mListenerLst.Clear();
        }
    }
}
