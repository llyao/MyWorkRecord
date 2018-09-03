using System.Resources;
using ClientCore.Config;
using ClientCore.DB;
using ClientCore.FSM;

namespace ClientCore.Runtime.States
{
	public class GameInitState : GameFSMState
	{
		private IWork initWork;

		public override bool IsFinished
		{
			get
			{
				return initWork == null || initWork.IsFinished();
			}
		}

		public GameInitState(Game content) : base(content)
		{

		}

		protected override void OnEnter(Event e, GameFSM.State lastState)
		{
			ManagerMan.Instance.RegisterManager(WorksManager.Instance);
			ManagerMan.Instance.RegisterManager(ModuleManager.Instance);
			ManagerMan.Instance.RegisterManager(DbManager.Instance);

			ManagerMan.Instance.InitAllManagers();

			//Logger.AddAppender(new FileLogAppender());

			initWork = new SequenceWork("",
				ConfigManager.Instance.LoadAllConfig()
			);

			WorksManager.Instance.AddStartRightAwayWork(initWork);
		}

		protected override GameFSM.State DoTick(float deltaTime)
		{
			if (initWork != null && initWork.IsFinished())
			{
				initWork = null;
			}
			return this;
		}
	}
}