using System.Collections.Generic;
using UnityEngine;

namespace PlatformCore
{
    /// <summary>
    /// 资源缓冲池，使用强引用
    /// </summary>
    public class AssetResourceRefPool:ICore
    {
        /// <summary>
        /// 自动释放延迟时间
        /// </summary>
        public static float AutoReleaseTime = 30.0f;
        /// <summary>
        /// 每秒钟释放的资源的个数
        /// </summary>
        public static int ReleaseCountPerFrame = 20;

        /// <summary>
        /// 当前判断到的位置
        /// </summary>
        protected int mCurIdx = 0;

        /// <summary>
        /// 缓存的资源的列表
        /// </summary>
        protected FastList<ITimeStamp> mAssetLst = new FastList<ITimeStamp>();

        public AssetResourceRefPool()
        {
        }
        
        public void Add(ITimeStamp value_)
        {
            if (value_ == null)
                return;

            //设置时间戳
            value_.MakeTimeStamp();
            
            //添加引用
            mAssetLst.Add(value_);

            //启动定时器
            Core.Timer.AddTick(onTick);
        }

        /// <summary>
        /// 正常情况下外部不需要调用这个接口
        /// </summary>
        /// <param name="value_"></param>
        public void Remove(ITimeStamp value_)
        {
            if (value_ == null)
                return;

            //删除一个引用
            mAssetLst.Remove(value_);

            //停止定时器
            stopTick();
        }

        /// <summary>
        /// 如果没有资源了，则停止定时器
        /// </summary>
        private void stopTick()
        {
            if (mAssetLst.Count > 0)
                return;

            Core.Timer.RemoveTick(onTick);
        }
        
        /// <summary>
        /// 定时器，每次只取消指定个数的资源引用
        /// </summary>
        /// <param name="deltaTime"></param>
        private void onTick(float deltaTime)
        {
            float curTime = Time.realtimeSinceStartup;
            int count = 0;

            if (mCurIdx >= mAssetLst.Count)
                mCurIdx = 0;

            for (int i = mCurIdx; i < mAssetLst.Count; i++)
            {
                if (mAssetLst[i].TimeStamp + AutoReleaseTime < curTime)
                {//取消超时的资源的引用
                    mAssetLst.Remove(mAssetLst[i]);
                    i--;
                    count++;
                }

                mCurIdx = i + 1;

                //超过一定个数，则留到下一帧处理
                if (count > ReleaseCountPerFrame)
                    break;
            }

            //停止定时器
            stopTick();
        }
    }
}
