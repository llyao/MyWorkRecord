using System.Collections.Generic;

namespace ClientCore.FSM
{
	//note by kun 2014.4.21;
	// 分层状态机的一个通用实现;
	public abstract class HFSMImpl : IHFSM
	{
		public HFSMImpl(LayerTag layer)
		{
			Layer = layer;
		}

		public bool DoEvent(LayerEvent e)
		{
			if(DoEventImpl(e))
			{
				return true;
			}
			if(e.Used)
			{
				return false;
			}
			if(Children != null)
			{
				for(var i = 0; i < Children.Count; ++i)
				{
					var subFSM = Children[i];
					if(subFSM.DoEvent(e))
					{
						return true;
					}
				}
			}
			return false;
		}

		public void RaiseEvent(LayerEvent e)
		{
			if(Parent != null)
			{
				Parent.RaiseEvent(e);
			}
			else
			{
				DoEvent(e);
			}
		}

		public void AddChild(IHFSM child)
		{
			child.Parent = this;
			if(Children == null)
			{
				Children = new List<IHFSM>();
			}
			Children.Add(child);
		}

		public void Stop()
		{
			if(Children != null)
			{
				for(var i = 0; i < Children.Count; ++i)
				{
					var child = Children[i];
					child.Stop();
				}
			}
			_StopImpl();
		}

		public IHFSM Parent
		{
			get;
			set;
		}

		public LayerTag Layer
		{
			get;
			set;
		}

		public List<IHFSM> Children
		{
			get;
			set;
		}

		public abstract bool IsFinited
		{
			get;
		}

		public void RemoveALLChildren()
		{
			for(var i = 0; i < Children.Count; ++i)
			{
				var child = Children[i];
				child.Stop();
			}
			Children.Clear();
		}

		public void RemoveChild(LayerTag layer)
		{
			for(var i = 0; i < Children.Count; ++i)
			{
				var child = Children[i];
				if(child.Layer.CheckEquals(layer))
				{
					child.Stop();
					Children.Remove(child);
					return;
				}
			}
		}

		protected abstract void _StopImpl();

		protected abstract bool DoEventImpl(LayerEvent e);
	}
}