using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlatformCore
{
    public class ObjectPool<T> where T: IObjectPoolItem , new()
    {
        private static Stack<IObjectPoolItem> mStack = new Stack<IObjectPoolItem>();

        public static int CountAll { get; private set; }
        public static int StackCount { get { return mStack.Count; } }
        
        public ObjectPool()
        {
            
        }

        public static T New()
        {
            T item;
            if (StackCount <= 0)
            {//生成一个新的
                item = new T();
                CountAll++;
            } 
            else
            {//弹出一个已有的
                item = (T)mStack.Pop() ;
            }

            if (item is ObjectPoolItem<T> == true)
            {//增加引用计数
                (item as ObjectPoolItem<T>).AddRef();
            }

            //初始化
            item.poolInitial();

            return item;
        }

        public static void Delete(T item_)
        {
            if (item_ is ObjectPoolItem<T> == true)
            {//减引用计数
                (item_ as ObjectPoolItem<T>).DelRef();
            }
            else
            {//不使用引用计数的则直接回收
                Recycle(item_) ;
            }
        }

        public static void Recycle(T item_)
        {//直接回收类型
            foreach (T item in mStack)
            {//先判断是否是已经被回收的，这步操作比较浪费，后期可以去去掉
                if (ReferenceEquals(item, item_) == true)
                    return;
            }

            if (item_ is IObjectPoolItem)
            {
                (item_ as IObjectPoolItem).poolRecycle();
            }

            mStack.Push(item_);
        }
        public static void Recycle<TT>(ObjectPoolItem<TT> objectReference) where TT : IObjectPoolItem , new()
        {//按指定类型回收
            foreach (T item in mStack)
            {
                if (ReferenceEquals(item, objectReference) == true)
                    return;
            }

            if (objectReference is IObjectPoolItem)
            {
                (objectReference as IObjectPoolItem).poolRecycle();
            }

            mStack.Push(objectReference as IObjectPoolItem);
        }
    }
}
 