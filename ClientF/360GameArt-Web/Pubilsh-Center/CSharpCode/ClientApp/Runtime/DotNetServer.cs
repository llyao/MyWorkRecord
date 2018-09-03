using ClientApp.Runtime.States;
using ClientCore.DB;
using ClientCore.Runtime;

namespace ClientApp.Runtime
{
	public class DotNetServer:App
	{
		protected override GameFSMState CreateEnterState(Game game)
		{
			DbManager.Instance.Connect("127.0.0.1", 3306, "gameartpublishcenter", "root", "qifun");
			return new GameInitState(game);
		}
	}
}