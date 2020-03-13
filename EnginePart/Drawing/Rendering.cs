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
			}
			public abstract void Draw (IDrawDevice device);

			public void Dispose ()
			{
				drawable.Remove (this);
			}
		}

		private static List<IDrawable> drawable = new List<IDrawable> ();

		public static void Draw (IDrawDevice device)
		{
			foreach (var d in drawable) d.Draw (device);
		}
	}
}