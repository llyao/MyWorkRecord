﻿using System;
using System.Collections.Generic;
using System.Linq;
using ClientCore.Collections;
using ClientCore.Core;

namespace ClientCore.Runtime
{
	public interface IManager
	{
		void Reset();
		void Init();
		void Boot(bool reboot);
		IWork BootLoad();
		void Shutdown();
		void Quit();
		void Tick(float deltaTime);
		void BootLoadFinished();
	}

	public abstract class Manager<T> : Singleton<T>, IManager where T : Manager<T>, new()
	{
		protected readonly EventDispatcher EventDispatcher = new EventDispatcher();

		private readonly Dictionary<Type, Module> modules = new Dictionary<Type, Module>();
		private ManagerState state = ManagerState.UNINIT;
		private IWork bootLoader;

		public bool IsInited => state >= ManagerState.INIT && state <= ManagerState.RUNNING;

		public bool IsRunning => state == ManagerState.RUNNING;

		public bool IsShutdown => state >= ManagerState.SHUTDOWN;

		public virtual void Reset()
		{
			Instance = new T();
		}

		public void Init()
		{
			if(state == ManagerState.UNINIT)
			{
				OnInit();
				state = ManagerState.INIT;
			}
		}

		public void Boot(bool reboot)
		{
			if(state == ManagerState.RUNNING && reboot)
			{
				state = ManagerState.INIT;
			}
			if(state == ManagerState.INIT)
			{
				OnBoot();
			}
		}

		public IWork BootLoad()
		{
			if(state == ManagerState.INIT)
			{
				state = ManagerState.BOOT_LOADING;
				var loaders = TempList<IWork>.Alloc();

				var loader = DoLoad();
				if(loader != null)
				{
					loaders.Add(loader);
				}

				foreach(var module in modules.Values)
				{
					var work = module.Load();
					if(work != null)
					{
						loaders.Add(work);
					}
				}
				if(loaders.Count > 0)
				{
					return bootLoader = new ParallelWork("", loaders);
				}
			}

			return null;
		}

		public void BootLoadFinished()
		{
			OnBootLoadFinished();
		}

		public void Shutdown()
		{
			if(state < ManagerState.SHUTDOWN)
			{
				OnShutdown();
				modules.Clear();
				state = ManagerState.SHUTDOWN;
			}
		}

		public void Quit()
		{
			if(state != ManagerState.QUIT)
			{
				OnQuit();
				state = ManagerState.QUIT;
			}
		}

		public virtual void Tick(float deltaTime)
		{
			if(state == ManagerState.BOOT_LOADING && (bootLoader == null || bootLoader.IsFinished()))
			{
				bootLoader = null;
				state = ManagerState.RUNNING;
			}
			OnTick(deltaTime);
		}

		public void AddEventListener(string type, Action<object> listher)
		{
			EventDispatcher.AddEventListener(type, listher);
		}

		public void RemoveEventListener(string type, Action<object> listher)
		{
			EventDispatcher.RemoveEventListener(type, listher);
		}

		public void DispatchEvent(string type, object data)
		{
			EventDispatcher.DispatchEvent(type, data);
		}

		public TM GetModule<TM>() where TM : Module
		{
			Module m;
			if(modules.TryGetValue(typeof(TM), out m))
			{
				return m as TM;
			}
			return null;
		}

		protected virtual void OnInit()
		{
		}

		protected virtual void OnBoot()
		{
		}

		protected virtual void OnBootLoadFinished()
		{
		}

		protected virtual void OnShutdown()
		{
		}

		protected virtual void OnQuit()
		{
		}

		protected virtual void OnTick(float deltaTime)
		{
		}

		protected virtual IWork DoLoad()
		{
			return null;
		}

		protected TM AddModule<TM>() where TM : Module
		{
			var t = typeof(TM);
			if(modules.ContainsKey(t))
			{
				modules.Remove(t);
			}

			var module = ModuleManager.Instance.AddDefaultModule(t);
			if(module != null)
			{
				modules.Add(t, module);
				if(IsRunning)
				{
					WorksManager.Instance.AddStartRightAwayWork(module.Load());
				}
			}
			return module as TM;
		}

		protected TM AddModule<TM>(TM module) where TM : Module
		{
			var t = typeof(TM);
			if(modules.ContainsKey(t))
			{
				modules.Remove(t);
			}

			ModuleManager.Instance.AddModule(t, module);

			modules.Add(typeof(TM), module);
			if(IsRunning)
			{
				WorksManager.Instance.AddStartRightAwayWork(module.Load());
			}

			return module;
		}

		protected void UnloadModule(Module module)
		{
			if(module == null)
			{
				return;
			}
			var records = modules.Where(p => p.Value == module).ToList();
			foreach(var r in records)
			{
				modules.Remove(r.Key);
			}
			module.Unload();
		}

		private enum ManagerState
		{
			UNINIT,
			INIT,
			BOOT_LOADING,
			RUNNING,
			SHUTDOWN,
			QUIT
		}
	}
}