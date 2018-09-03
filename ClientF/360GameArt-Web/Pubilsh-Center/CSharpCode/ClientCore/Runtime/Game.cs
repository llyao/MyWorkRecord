using System;
using System.Collections.Generic;
using ClientCore.Core;
using ClientCore.FSM;

namespace ClientCore.Runtime
{
	public class Game : Singleton<Game>
	{
		public Action OnGameQuit; // 会在Quit的时候第一时间调用
		private GameFSM fsm;
		private Queue<Event> eventQueue = new Queue<Event>();
		private bool isQuiting;

		public static bool IsQuiting
		{
			get
			{
				return Instance.isQuiting;
			}
		}

		public GameFSMState GetFSMState()
		{
			return fsm != null ? fsm.CurrentState as GameFSMState : null;
		}

		public string GetFSMStateName()
		{
			var state = GetFSMState();
			return state != null ? state.GetType().Name : null;
		}

		public virtual void Init(GameFSMState enterState)
		{
			fsm = new GameFSM(this, enterState);
			fsm.Reset();
		}

		public void InputEvent(int eventType)
		{
			fsm.Input(new Event(eventType));
		}

		public void InputGameEvent(string eventType, object parameter = null)
		{
			fsm.Input(new GameEvent(eventType, parameter));
		}

		public void EnqueueGameEvent(string eventType, object parameter = null)
		{
			eventQueue.Enqueue(new GameEvent(eventType, parameter));
		}


		public void InputGameEvent(GameEvent evt)
		{
			fsm.Input(evt);
		}

		public void Quit()
		{
			isQuiting = true;
			if (OnGameQuit != null)
			{
				OnGameQuit();
			}
			ManagerMan.Instance.ShutdownAllManagers();
			ManagerMan.Instance.QuitAllManagers();
		}

		public virtual void Tick(float deltaTime)
		{
			while (eventQueue.Count > 0)
			{
				var e = eventQueue.Dequeue();
				fsm.Input(e);
			}

			if (fsm != null)
				fsm.Tick(deltaTime);
			ManagerMan.Instance.Tick(deltaTime);
		}
	}
}