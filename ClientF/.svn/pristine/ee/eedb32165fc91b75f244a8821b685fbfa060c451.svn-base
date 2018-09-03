using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PlatformCore
{
    public class CallLaterItem
    {
        protected float mEndTime;
        protected Action mCb1;
        protected Action<object> mCb2;
        protected object mData;
        protected int mCount;

        public CallLaterItem(float endTime_, Action cb1_, Action<object> cb2_, object data_, int count_)
        {
            mEndTime = endTime_;
            mCb1 = cb1_;
            mCb2 = cb2_;
            mData = data_;
            mCount = count_;
        }

        public bool DoCmd(float curTime_)
        {
            if (curTime_ < mEndTime)
                return false;

            if (mCb1 != null)
                mCb1();
            if (mCb2 != null)
                mCb2(mData);

            mCount--;
            return mCount == 0; 
        }
    }

    public class CoreTimer : EventDispatcherMono , ICore
    {
        protected List<CallLaterItem> mCallLaterItemLst = new List<CallLaterItem>();

        public float CurTime { get { return Time.realtimeSinceStartup; } }

        public List<Action<float>> mTickLst = new List<Action<float>>();

        void Start()
        {
            
        }

        void Update()
        {
            //清理
            Core.DeletePool.Update();

            //发送更新消息
            DispatchEvent(Event.TIMER_UPDATE);

            //处理延迟调用
            UpdateCallLater();

            UpdateTick();
        }

        public void AddTick(Action<float> cb_)
        {
            for (int i = 0; i < mTickLst.Count; i++)
            {
                if (mTickLst[i] == cb_)
                    return;
            }
            mTickLst.Add(cb_);
        }

        public void RemoveTick(Action<float> cb_)
        {
            for (int i = 0; i < mTickLst.Count; i++)
            {
                if (mTickLst[i] == cb_)
                {
                    mTickLst.RemoveAt(i) ;
                    i--;
                }
            }
        }

        protected void UpdateTick()
        {
            for (int i = 0; i < mTickLst.Count; i++)
            {
                if (mTickLst[i] != null)
                {
                    mTickLst[i](Time.deltaTime);
                }
                else
                {
                    mTickLst.RemoveAt(i) ;
                    i--;
                }
            }
        }

        protected void UpdateCallLater()
        {
            float curTime = CurTime;
            for (int i = 0; i < mCallLaterItemLst.Count; i++)
            {
                if (mCallLaterItemLst[i].DoCmd(curTime) == true)
                {
                    mCallLaterItemLst.RemoveAt(i) ;
                    i--;
                }
            }
        }

        public void CallLater(Action cb_, float delay_ , int count_ = 1)
        {
            mCallLaterItemLst.Add(new CallLaterItem(CurTime + delay_ , cb_ , null , null , count_));
        }

        public void CallLater(Action<object> cb_, object data_ , float delay_ , int count_ = 1)
        {
            mCallLaterItemLst.Add(new CallLaterItem(CurTime + delay_, null, cb_, data_, count_));
        }
    }
}
