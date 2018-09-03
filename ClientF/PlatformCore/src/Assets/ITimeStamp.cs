using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PlatformCore
{
    public class ITimeStamp : EventDispatcher , IDisposable
    {
        private float mTimeStamp = 0.0f;
        public float TimeStamp
        {
            get { return mTimeStamp; }
        }

        /// <summary>
        /// 设置时间戳
        /// </summary>
        public virtual void MakeTimeStamp()
        {
            mTimeStamp = Time.realtimeSinceStartup;
        }

        //--------------------------------------------------------------------------------

        #region IDisposable Support
        protected bool disposedValue = false; // 要检测冗余调用
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                disposedValue = true;
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
         ~ITimeStamp() {
           // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
           Dispose(false);
         }

        // 添加此代码以正确实现可处置模式。
        void IDisposable.Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
