using ClientCore.FSM;

namespace ClientCore.Runtime.States
{
	public class GameBootState : GameFSMState
	{
		private StartAndWait bootWork;
		public GameBootState(Game content) : base(content)
		{
		}

		public override bool IsFinished
		{
			get
			{
				return bootWork == null;
			}
		}

		protected override void OnEnter(Event e, FiniteStateMachine<Game, float>.State lastState)
		{
			base.OnEnter(e, lastState);
			ManagerMan.Instance.BootAllManagers(true);
			bootWork = new StartAndWait(
				ManagerMan.Instance.BootLoadAllManagers()
			);
			WorksManager.Instance.AddStartRightAwayWork(bootWork);
		}

		protected override FiniteStateMachine<Game, float>.State DoTick(float deltaTime)
		{
			if(bootWork != null && bootWork.IsFinished())
			{
				bootWork = null;
			}
			return base.DoTick(deltaTime);
		}
	}
}