using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlatformCore
{
    public class FastList<T> where T:new()
    {
        protected List<T> mLst = new List<T>();
        protected Dictionary<T,bool> mMap = new Dictionary<T , bool>();

        public List<T> List { get { return mLst; } }
        public int Count { get { return mLst.Count; } }

        public void Add(T t_)
        {
            if (mMap.ContainsKey(t_) == true)
                return;
            mLst.Add(t_);
            mMap[t_] = true;
        }

        public void Remove(T t_)
        {
            if (mMap.ContainsKey(t_) == false)
                return;
            mMap.Remove(t_);
            mLst.Remove(t_);
        }

        public T this[int idx_]
        {
            get
            {
                if(idx_ < 0 || idx_ >= Count)
                    return default(T) ;
                return mLst[idx_];
            }
        }
    }
}
