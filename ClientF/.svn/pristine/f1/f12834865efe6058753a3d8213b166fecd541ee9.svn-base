using Google.Protobuf;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 namespace GameILruntime.Core
{

    public class EventManager : Singleton<EventManager>
    {
        public enum EventType
        {
            GameStart = 0,
            GameEnd = 1
        }
        private Dictionary<int, Action> NoArguEvents = new Dictionary<int, Action>();
        private Dictionary<int, Action<IMessage>> NetEvents = new Dictionary<int, Action<IMessage>>();

        public void RegisterEvent(int id, Action act)
        {
            Action temp;
            if (NoArguEvents.TryGetValue(id, out temp))
            {
                temp += act;
            }
            else
            {
                temp += act;
                NoArguEvents.Add(id, temp);
            }
        }
        public void RegisterNetEvent(int id, Action<IMessage> act)
        {
            Action<IMessage> temp;
            if (NetEvents.TryGetValue(id, out temp))
            {
                temp += act;
            }
            else
            {
                temp += act;
                NetEvents.Add(id, temp);
            }
        }
        public void CancelEvent(int id)
        {
            NoArguEvents.Remove(id);
        }
        public void CancelNetEvent(int id, Action<IMessage> act)
        {
            Action<IMessage> temp;
            if (NetEvents.TryGetValue(id, out temp))
            {
                temp -= act;
            }
        }
        public void EmitEvet(int id)
        {
            Action temp;

            if (NoArguEvents.TryGetValue(id, out temp))
            {
                temp();
            }
        }
        public void EmitNetEvet(int id, IMessage obj)
        {
            Action<IMessage> temp;

            if (NetEvents.TryGetValue(id, out temp))
            {
                temp(obj);
            }
        }
    }

}
