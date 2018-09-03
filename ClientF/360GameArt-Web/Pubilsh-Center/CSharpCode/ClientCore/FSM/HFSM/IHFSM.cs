using System.Collections.Generic;

namespace ClientCore.FSM
{
	//note by kun 2014.4.21;
	// 分层状态机接口;
	public interface IHFSM : IFiniteStateMachine
	{
		List<IHFSM> Children
		{
			get;
		}

		LayerTag Layer
		{
			get;
		}

		IHFSM Parent
		{
			get;
			set;
		}

		void AddChild(IHFSM child);
		bool DoEvent(LayerEvent e);
		void RaiseEvent(LayerEvent e);
	}
}