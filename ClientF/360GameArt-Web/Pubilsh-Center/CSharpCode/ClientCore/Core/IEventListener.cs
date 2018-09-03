using System;

namespace ClientCore.Core
{
	public interface IEventListener
	{
		void AddEventListener(string type, Action<object> listher);
		void RemoveEventListener(string type, Action<object> listener);
		void RemoveEventListener(string type);
		void RemoveEventListener(Action<object> listener);
		bool HasEventListener(string type);
		void DispatchEvent(string type, object data = null);
	}
}