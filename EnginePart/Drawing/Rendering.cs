using System.Collections.Generic;
using System;

namespace EnginePart
{
	public static class Rendering
	{
		public abstract class Renderer : IDrawable, IDisposable
		{
			public Renderer ()
			{
				drawable.Add (this);
				layer = DrawLayer.DEFAULT;
			}

			public DrawLayer layer { get; set; }

			public abstract void Draw (IDrawDevice device);

			public void Dispose ()
			{
				drawable.Remove (this);
			}
		}

		private static DrawLayer layerMask = int.MaxValue;
		private static List<IDrawable> drawable = new List<IDrawable> ();

		public static void SetLayerEnabled (DrawLayer layer, bool enabled)
		{
			if (enabled) layerMask |= layer;
			else layerMask &= ~layer;
		}

		public static void Draw (IDrawDevice device)
		{
			foreach (var d in drawable)
			{
				if ((d.layer & layerMask) != 0) d.Draw (device);
			}
		}
	}
}