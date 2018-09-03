using ClientCore.FSM;
using ClientCore.Runtime;
using ClientCore.Runtime.States;

namespace ClientApp.Runtime.States
{
	public class GameBootState:ClientCore.Runtime.States.GameBootState
	{
		public GameBootState(Game content) : base(content)
		{
		}

		protected override FiniteStateMachine<Game, float>.State DoTick(float deltaTime)
		{
			if(IsFinished)
			{
				return new GameEnterState(Content);
			}
			return base.DoTick(deltaTime);
		}
	}
}