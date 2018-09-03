namespace ClientCore.FSM
{
	//note by kun 2014.4.21;
	// 用于跨层的消息;
	public class LayerEvent : Event
	{
		public LayerTag Layer;

		public bool Used;

		public LayerEvent(int id, LayerTag layer)
			: base(id)
		{
			Layer = layer;
		}
	}
}