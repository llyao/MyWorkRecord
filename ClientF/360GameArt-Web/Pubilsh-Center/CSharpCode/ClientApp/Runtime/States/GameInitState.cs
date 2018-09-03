using ClientCore.FSM;
using ClientCore.Runtime;

namespace ClientApp.Runtime.States
{
	public class GameInitState:ClientCore.Runtime.States.GameInitState
	{
		public GameInitState(Game content) : base(content)
		{
		}

		protected override FiniteStateMachine<Game, float>.State DoTick(float deltaTime)
		{
			if(IsFinished)
			{
				return new GameBootState(Content);
			}
			return base.DoTick(deltaTime);
		}
	}
}