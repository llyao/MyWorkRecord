using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlatformCore
{
    public interface IDeletePoolItem
    {
        void doDelete();
    }
    public class CoreDeletePool :ICore
    {
        protected Stack<IDeletePoolItem> mItemLst = new Stack<IDeletePoolItem>();

        public void Add(IDeletePoolItem item_)
        {
            if (item_ == null)
                return;
            mItemLst.Push(item_);
        }

        public void Update()
        {
            while (mItemLst.Count > 0)
            {
                IDeletePoolItem item = mItemLst.Pop();
                if(item != null)
                    item.doDelete(); 
            }
        }
    }
}
