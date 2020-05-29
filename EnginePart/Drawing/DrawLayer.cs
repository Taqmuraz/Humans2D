namespace EnginePart
{
	public struct DrawLayer
	{
		private uint layer;

		public const uint DEFAULT = 1;
		public const uint UI = 2;
		public const uint TEXT = 4;

		public DrawLayer (uint layer)
		{
			this.layer = layer;
		}

		public static implicit operator uint (DrawLayer drawLayer)
		{
			return drawLayer.layer;
		}
		public static implicit operator int (DrawLayer drawLayer)
		{
			return (int)drawLayer.layer;
		}
		public static implicit operator DrawLayer (uint value)
		{
			return new DrawLayer(value);
		}
		public static implicit operator DrawLayer (int value)
		{
			return new DrawLayer ((uint)value);
		}

		public static DrawLayer operator | (DrawLayer a, DrawLayer b)
		{
			return a.layer | b.layer;
		}
		public static DrawLayer operator & (DrawLayer a, DrawLayer b)
		{
			return a.layer & b.layer;
		}
		public static DrawLayer operator ^ (DrawLayer a, DrawLayer b)
		{
			return a.layer & b.layer;
		}
		public static DrawLayer operator ~ (DrawLayer a)
		{
			return ~a.layer;
		}
	}
}