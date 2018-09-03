using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PlatformCore
{
    public class DeletePoolAssetBundleUnload : ObjectPoolItem<DeletePoolAssetBundleUnload>, IDeletePoolItem
    {
        public AssetBundle mAssetBundle = null;

        public static DeletePoolAssetBundleUnload New(AssetBundle ab_)
        {
            if (ab_ == null)
                return null;

            DeletePoolAssetBundleUnload item = ObjectPool<DeletePoolAssetBundleUnload>.New();
            item.mAssetBundle = ab_;
            Core.DeletePool.Add(item);
            return item;
        }

        public static void Delete(DeletePoolAssetBundleUnload item_)
        {
            if (item_ == null)
                return;

            ObjectPool<DeletePoolAssetBundleUnload>.Delete(item_);
        }

        public void doDelete()
        {
            if (mAssetBundle != null)
                mAssetBundle.Unload(false);

            Delete(this);
        }

        internal override void poolInitial()
        {
            mAssetBundle = null;
        }

        internal override void poolRecycle()
        {
            mAssetBundle = null;
        }
    }
}
