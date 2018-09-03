using System;

namespace ClientCore.Runtime
{
	public abstract class Module
	{
		protected enum Status
		{
			UNINIT,
			LOADING,
			RUNNING,
			UNLOADING,
			STOPPED
		}

		protected Status status;

		private IWork loader;
		private Action<Module> onload;

		public bool IsExpired
		{
			get
			{
				return status == Status.UNLOADING || status == Status.STOPPED;
			}
		}

		public bool IsWaitingOrLoading
		{
			get
			{
				return status <= Status.LOADING;
			}
		}

		public bool IsRunning
		{
			get
			{
				return status == Status.RUNNING;
			}
		}

		protected Module() { }

		protected Module(Module other)
		{

		}

		public void Init()
		{
			OnInit();
		}

		public IWork Load()
		{
			if (status == Status.UNINIT)
			{
				loader = DoLoad();
				status = loader != null ? Status.LOADING : Status.RUNNING;
				if (loader != null)
					loader.AddOnFinish(OnLoadFinished);
				else
					OnLoadFinished();
			}
			return loader;
		}

		public void Unload()
		{
			if (status == Status.RUNNING || status == Status.UNLOADING)
			{
				OnUnload();
				status = Status.STOPPED;
			}
			else if (status == Status.LOADING && loader != null)
			{
				status = Status.UNLOADING;
			}
			else
			{
				status = Status.STOPPED;
			}
		}

		protected virtual void AddEventListener()
		{

		}

		protected virtual void RemoveEventListener()
		{

		}

		public void Tick(float deltaTime)
		{
			switch (status)
			{
				case Status.RUNNING:
					OnTick(deltaTime);
					break;
			}
		}

		public void LateTick()
		{
			switch (status)
			{
				case Status.RUNNING:
					OnLateTick();
					break;
			}
		}

		public void AddOnLoaded(Action<Module> action)
		{
			onload += action;
			if (status >= Status.RUNNING && onload != null)
			{
				onload(this);
				onload = null;
			}
		}

		protected virtual void OnInit()
		{

		}

		protected virtual IWork DoLoad()
		{
			return null;
			//return ConfigManager.Instance.WaitForConfigLoaded();
		}

		protected virtual void OnLoaded()
		{
			AddEventListener();
		}

		protected virtual void OnUnload()
		{
			RemoveEventListener();
		}

		protected virtual void OnTick(float deltaTime)
		{

		}

		protected virtual void OnLateTick()
		{

		}

		private void OnLoadFinished()
		{
			loader = null;
			OnLoaded();
			if (onload != null)
			{
				onload(this);
				onload = null;
			}
			if (status == Status.LOADING)
			{
				status = Status.RUNNING;
			}
			else if (status == Status.UNLOADING)
			{
				Unload();
			}
		}
	}
}