using System;
using System.Collections.Generic;

namespace ClientCore.Core
{
	/// <summary>
	/// 事件派发基类
	/// </summary>
	public class EventDispatcher: IEventListener
	{
		private Dictionary<string, List<Action<object>>> _map;

		public EventDispatcher()
		{
			_map = new Dictionary<string, List<Action<object>>>();
		}

		/// <summary>
		/// 是否注册了侦听 
		/// </summary>
		/// <param name="type">监听类型</param>
		/// <returns></returns>
		public bool HasEventListener(string type)
		{
			return _map.ContainsKey(type);
		}

		/// <summary>  
		/// 注册事件监听  
		/// </summary>  
		/// <param name="type">监听类型</param>  
		/// <param name="listher">监听对象</param>  
		public void AddEventListener(string type, Action<object> listher)
		{
			if (!_map.ContainsKey(type))
			{
				_map.Add(type, new List<Action<object>>());
			}
			_map[type].Add(listher);
		}

		public virtual void Destroy()
		{
			ClearEventListener();
			_map = null;
		}

		/// <summary>  
		/// 移除对type的所有监听  
		/// </summary>  
		/// <param name="type"></param>  
		/// 
		public void RemoveEventListener(string type, Action<object> listener)
		{
			if (_map != null)
			{
				if (_map.ContainsKey(type))
				{
					if (_map[type].Contains(listener))
					{
						_map[type].Remove(listener);
						if (_map[type].Count == 0)
						{
							_map.Remove(type);
						}
					}
				}
			}
		}

		/// <summary>  
		/// 移除对type的所有监听  
		/// </summary>  
		/// <param name="type"></param>  
		/// 
		public void RemoveEventListener(string type)
		{
			if (_map.ContainsKey(type))
			{
				_map.Remove(type);
			}
		}

		/// <summary>  
		/// 移除监听者的所有监听  
		/// </summary>  
		/// <param name="listener">监听者</param>  
		public void RemoveEventListener(Action<object> listener)
		{
			foreach (var item in _map)
			{
				if (item.Value.Contains(listener))
				{
					item.Value.Remove(listener);
					if (item.Value.Count == 0)
					{
						_map.Remove(item.Key);
					}
				}
			}
		}

		/// <summary>  
		/// 清空所有监听事件  
		/// </summary>  
		public void ClearEventListener()
		{
			//Debug.Log("清空对所有所有所有事件的监听");
			if (_map != null)
			{
				_map.Clear();
			}
		}

		/// <summary>  
		/// 派发事件  
		/// </summary>  
		/// <param name="type">事件类型</param>  
		/// <param name="data">事件传达的数据</param>  
		public void DispatchEvent(string type, object data = null)
		{
			if (!_map.ContainsKey(type))
			{
				//Debug.Log("事件：" + type + "没有注册任何监听！");
				return;
			}

			List<Action<object>> list = _map[type];
			for (int i = 0; i < list.Count; i++)
			{
				list[i](data);
			}
		}
	}
}