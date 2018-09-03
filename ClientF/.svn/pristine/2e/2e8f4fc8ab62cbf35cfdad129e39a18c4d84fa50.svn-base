using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlatformCore
{
    public interface IEventDispatcher
    {
        void AddListener(string type_, Action<Event> listener_ , int count);
        bool HasListener(string type_);
        void RemoveListener(string type_, Action<Event> listener_);
        bool DispatchEvent(Event e_);
        bool DispatchEvent(string type_, object data_ = null);
        void ClearEvent();
    }
}
