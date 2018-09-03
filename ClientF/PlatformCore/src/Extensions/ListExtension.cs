using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlatformCore
{
    public static class ListExtension
    {
        /// <summary>
        /// 弹出第一项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static T Shift<T>(this List<T> self)
        {
            if (self.Count > 0)
            {
                T item = self[0];
                self.RemoveAt(0);
                return item;
            }

            return default(T);
        }

        /// <summary>
        /// 复制一份数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static List<T> Clone<T>(this List<T> self)
        {
            List<T> result = new List<T>();
            foreach (T item in self)
            {
                result.Add(item);
            }
            return result;
        }

        public static List<T> Concat<T>(this List<T> self, List<T> other = null)
        {
            List<T> result = new List<T>();
            foreach (T item in self)
            {
                result.Add(item);
            }

            if (other != null)
            {
                foreach (T item in other)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        /// <summary>
        /// 弹出最后一项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static T Pop<T>(this List<T> self)
        {
            int last = self.Count - 1;
            if (last > -1)
            {
                T item = self[last];
                self.RemoveAt(last);
                return item;
            }

            return default(T);
        }
    }
}
