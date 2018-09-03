using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlatformCore
{
    public abstract class ObjectPoolItem<T> : IObjectPoolItem where T:IObjectPoolItem , new()
    {
        internal override void onRecycle()
        {
            //引用计数清零
            mRefCount = 0;

            //回收对象
            ObjectPool<T>.Recycle(this);
        }
    }
}
