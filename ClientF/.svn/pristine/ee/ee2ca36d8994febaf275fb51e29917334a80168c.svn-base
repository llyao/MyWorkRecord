using ClientCore.Logging;

namespace ClientCore.Runtime
{
	public abstract class App
	{
		public virtual void Awake()
		{
			Tick.Instance.Start();
			InitGame();
		}

		protected virtual void InitGame()
		{
			Logger.AddAppender(new MemoryLogAppender());
			Logger.AddAppender(new ConsoleLogAppender());
			Game.Instance.Init(CreateEnterState(Game.Instance));
			Tick.Instance.AddTick(OnTick);
		}

		protected abstract GameFSMState CreateEnterState(Game game);

		private void OnTick(float deltaSeconds)
		{
			Game.Instance.Tick(deltaSeconds);
		}
	}
}