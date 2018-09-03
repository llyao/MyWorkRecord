using System;

namespace ClientCore.Runtime
{
	public enum WorkState
	{
		HOLD = 0,
		WAITING,
		PREPARING,
		PLAYING,
		FINISHED
	}

	public class CommonWork :IWork
	{
		private Action<Exception> exceptionHandler;
		private Action onFinish;
		private Action onStart;

		private WorkState state = WorkState.HOLD;

		public WorkState State
		{
			get
			{
				return state;
			}
			protected set
			{
				state = value;
			}
		}


		public virtual float GetTotal()
		{
			return 1;
		}

		public virtual float GetCurrent()
		{
			return 0;
		}

		public virtual float GetProgress()
		{
			if(State == WorkState.PLAYING)
			{
				var total = GetTotal();
				return total > 0 ? GetCurrent() / total : 0;
			}
			if(State < WorkState.PLAYING)
			{
				return 0;
			}
			return 1;
		}

		public virtual string GetLabel()
		{
			return null;
		}

		public bool IsFinished()
		{
			return state == WorkState.FINISHED;
		}

		public bool IsStarted()
		{
			return state > WorkState.HOLD;
		}

		public virtual void Start()
		{
			ChangeState(WorkState.WAITING);
			Tick(0);
		}

		public virtual void Finish()
		{
			ChangeState(WorkState.FINISHED);
		}

		public void Tick(float deltaTime)
		{
			if(WorksManager.Instance.IsShutdown)
			{
				return;
			}

			try
			{
				// 播放状态下，先Tick，保证动画结束的当前帧能执行一次Tick
				if(State != WorkState.PLAYING)
					KeepTryChangeState();
				DoTick(deltaTime);
				KeepTryChangeState();
			}
			catch(Exception e)
			{
				if(exceptionHandler != null)
				{
					exceptionHandler(e);
				}
				else
				{
					throw;
				}
			}
		}

		public void AddOnStart(Action startAction)
		{
			onStart += startAction;
		}

		public void AddOnFinish(Action finishAction)
		{
			onFinish += finishAction;
		}

		public void AddExceptionHandler(Action<Exception> handler)
		{
			exceptionHandler = handler;
		}

		protected virtual WorkState CanChangeState(WorkState currentState)
		{
			if(currentState == WorkState.WAITING)
			{
				return WorkState.PREPARING;
			}
			if(currentState == WorkState.PREPARING)
			{
				return WorkState.PLAYING;
			}
			return currentState;
		}

		protected virtual void DoFinish()
		{
		}

		protected virtual void DoInit()
		{
		}

		protected virtual void DoPrepare()
		{
		}

		protected virtual void DoStart()
		{
		}

		protected virtual void DoTick(float deltaTime)
		{
		}

		protected virtual void OnStateChanged(WorkState currentState)
		{
		}

		protected void ChangeState(WorkState newState)
		{
			if(newState > state)
			{
				state = newState;
				NotifyStateChanged(state);
				OnStateChanged(state);
			}
		}

		private void KeepTryChangeState()
		{
			bool changed;
			do
			{
				var newState = CanChangeState(state);
				changed = newState != state;
				ChangeState(newState);
			} while(changed);
		}

		private void NotifyStateChanged(WorkState currentState)
		{
			if(currentState == WorkState.PLAYING)
			{
				DoStart();
				if(onStart != null)
				{
					onStart();
				}
			}
			else if(currentState == WorkState.FINISHED)
			{
				DoFinish();
				if(onFinish != null)
				{
					onFinish();
				}
			}
			else if(currentState == WorkState.PREPARING)
			{
				DoPrepare();
			}
			else if(currentState == WorkState.WAITING)
			{
				DoInit();
			}
		}
	}

	public class EmptyWork : CommonWork
	{
		protected override WorkState CanChangeState(WorkState currentState)
		{
			if(currentState == WorkState.PLAYING)
			{
				return WorkState.FINISHED;
			}
			return base.CanChangeState(currentState);
		}
	}
}