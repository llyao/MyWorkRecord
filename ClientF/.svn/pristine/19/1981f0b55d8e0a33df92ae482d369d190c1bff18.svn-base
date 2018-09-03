using ClientCore.FSM;
using ClientCore.Logging;

namespace ClientCore.Runtime.States
{
	public class GameEnterState:GameFSMState
	{
		public GameEnterState(Game content) : base(content)
		{
		}

		protected override void OnEnter(Event e, FiniteStateMachine<Game, float>.State lastState)
		{
			base.OnEnter(e, lastState);
			Logger.AddAppender(new FileLogAppender());
		}
	}
}