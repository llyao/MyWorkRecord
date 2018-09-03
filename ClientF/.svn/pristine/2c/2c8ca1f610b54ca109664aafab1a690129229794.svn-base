using System.Collections;
using UnityEngine ;

namespace PlatformCore
{
    public class PrefabPoolItem : MonoBehaviour
    {
        internal AssetResource mAssetResource;

        public bool isNew = true;
        public bool Recycle(float recycleTime = 0)
        {
            if (Application.isPlaying == false)
            {
                return false;
            }
            if (gameObject == null)
            {
                return false;
            }

            if (mAssetResource == null || Core.PoolContainer == null)
            {
                GameObject.Destroy(this.gameObject, recycleTime);
                return true;
            }

            gameObject.transform.SetParent(Core.PoolContainer.transform, true);
            gameObject.SetActive(false);
            if (recycleTime > 0)
            {
                StartCoroutine(laterRecycle(recycleTime));
            }
            else
            {
                mAssetResource.RecycleToPool(this);
            }
            return true;
        }


        public void Dispose()
        {
            if (isDisposed == true)
            {
                return;
            }
            isDisposed = true;
            GameObject.Destroy(this.gameObject);
        }

        private bool isDisposed = false;
        protected virtual void OnDestroy()
        {
            isDisposed = true;
        }

        private IEnumerator laterRecycle(float recycleTime)
        {
            if (recycleTime > 0)
            {
                yield return new WaitForSeconds(recycleTime);
            }

            mAssetResource.RecycleToPool(this);
        }
    }
}
