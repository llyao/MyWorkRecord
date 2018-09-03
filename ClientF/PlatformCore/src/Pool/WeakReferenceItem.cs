using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlatformCore
{
    public class WeakReferenceItem<T> : ObjectPoolItem<WeakReferenceItem<T>>
    {
        protected WeakReference mWR = null;

        public T Target
        {
            get
            {
                if (mWR == null)
                    return default(T);
                if (mWR.IsAlive == false)
                    return default(T);
                return (T)mWR.Target ;
            }
            set
            {
                if (mWR == null)
                    mWR = new WeakReference(value);
                else
                    mWR.Target = value;
            }
    }

        public static WeakReferenceItem<T> New(T t_)
        {
            WeakReferenceItem<T> ret = ObjectPool<WeakReferenceItem<T>>.New();
            ret.Target = t_;
            return ret;
        }

        public static void Delete(ref WeakReferenceItem<T> item_)
        {
            if (item_ == null)
                return;
            ObjectPool<WeakReferenceItem<T>>.Delete(item_);
            item_ = null;
        }

        internal override void poolInitial()
        {
            Target = default(T);
        }

        internal override void poolRecycle()
        {
            Target = default(T);
        }
    }
}
