using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PlatformCore
{
    public class DeletePoolGameObjectDestroy : ObjectPoolItem<DeletePoolGameObjectDestroy>, IDeletePoolItem
    {
        public UnityEngine.Object mObject = null;

        public static DeletePoolGameObjectDestroy New(UnityEngine.Object obj_)
        {
            if (obj_ == null)
                return null;

            DeletePoolGameObjectDestroy item = ObjectPool<DeletePoolGameObjectDestroy>.New();
            item.mObject = obj_;
            Core.DeletePool.Add(item);
            return item;
        }

        public static void Delete(DeletePoolGameObjectDestroy item_)
        {
            if (item_ == null)
                return;

            ObjectPool<DeletePoolGameObjectDestroy>.Delete(item_);
        }

        public void doDelete()
        {
            if (mObject != null)
                GameObject.DestroyImmediate(mObject , true);

            Delete(this);
        }

        internal override void poolInitial()
        {
            mObject = null;
        }

        internal override void poolRecycle()
        {
            mObject = null;
        }
    }
}
