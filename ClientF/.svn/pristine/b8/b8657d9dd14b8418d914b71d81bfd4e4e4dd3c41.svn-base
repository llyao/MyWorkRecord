using ClientCore.FSM;
using ClientCore.Logging;

namespace ClientCore.Runtime
{
	public class GameFSMState : GameFSM.State
	{

		protected GameFSMState(Game content) : base(content)
		{

		}

		public override void Enter(Event e, GameFSM.State lastState)
		{
			if(lastState != null)
			{
				Logger.Info("{0} -> {1}", lastState != null ? lastState.GetType().Name : "NONE", GetType().Name);
			}
			base.Enter(e, lastState);
		}
	}
}
