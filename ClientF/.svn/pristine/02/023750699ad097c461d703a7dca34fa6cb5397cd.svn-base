using System;
using System.Collections.Generic;
using System.Timers;
using ClientCore.Core;
using ClientCore.Utils;

namespace ClientCore.Runtime
{
	public class Tick:Singleton<Tick>
	{
		private Timer timer;
		private long prevTime;
		private List<Action<float>> tickActions;
		public Tick()
		{
			tickActions = new List<Action<float>>();
			timer = new Timer(100);
			timer.Elapsed += new ElapsedEventHandler(OnTick);
		}

		public void Start()
		{
			prevTime = TimeUtils.GetTimeMillisecond(System.DateTime.Now);
			timer.Start();
		}

		private void OnTick(object sender, ElapsedEventArgs e)
		{
			//System.Diagnostics.Debug.WriteLine("========================Tick==========================");
			long time = TimeUtils.GetTimeMillisecond(System.DateTime.Now);
			float seconds = (time - prevTime) * 0.001f;
			foreach(var action in tickActions)
			{
				action(seconds);
			}
			prevTime = time;
		}

		public void AddTick(Action<float> action)
		{
			RemoveTick(action);
			tickActions.Add(action);
		}

		public void RemoveTick(Action<float> action)
		{
			for(int i = 0; i < tickActions.Count; i++)
			{
				if(tickActions[i].Equals(action))
				{
					tickActions.Remove(action);
					break;
				}
			}
		}
	}
}