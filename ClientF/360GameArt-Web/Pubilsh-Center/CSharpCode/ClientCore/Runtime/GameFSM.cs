using System.Web.UI.WebControls;
using ClientCore.FSM;
using ClientCore.Runtime;

namespace ClientCore.Runtime
{
	public class GameFSM : FiniteStateMachine<Game>
	{
		public GameFSM(Game content, GameFSMState enterState) : base(content, enterState)
		{
			
		}
	}
}
