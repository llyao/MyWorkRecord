using System;

namespace ClientCore.FSM
{
	public struct LayerTag
	{
		private static LayerTag topLayer;

		public static LayerTag AnyLayer
		{
			get;
			private set;
		}

		public static LayerTag TopLayer
		{
			get
			{
				if(topLayer.layers == null)
				{
					topLayer.layers = new[]
					{
						0
					};
				}
				return topLayer;
			}
		}

		public int[] layers;

		static LayerTag()
		{
			AnyLayer = new LayerTag();
		}

		public int this[int index]
		{
			get
			{
				return layers[index];
			}
			set
			{
				layers[index] = value;
			}
		}

		public bool CheckEquals(LayerTag other)
		{
			if(layers == other.layers)
			{
				return true;
			}

			if(layers == null || other.layers == null)
			{
				return false;
			}

			for(var i = 0; i < layers.Length; ++i)
			{
				if(layers[i] != other[i])
				{
					return false;
				}
			}
			return true;
		}

		public LayerTag AddLayer(int subLayer)
		{
			var newlayers = new int[layers.Length + 1];
			Buffer.BlockCopy(layers, 0, newlayers, 0, layers.Length);
			newlayers[layers.Length] = subLayer;
			return new LayerTag
			{
				layers = newlayers
			};
		}
	}
}