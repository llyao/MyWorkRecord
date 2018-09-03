using System.Collections.Generic;

namespace ClientCore.Collections
{
	public class ObjectPool<T> where T : class, new()
	{
		private static readonly Stack<T> cache = new Stack<T>();

		public static T Alloc()
		{
			if(cache.Count > 0)
			{
				return cache.Pop();
			}
			return new T();
		}

		public static void Dealloc(T t)
		{
			if(t != null)
			{
				cache.Push(t);
			}
		}
	}
}