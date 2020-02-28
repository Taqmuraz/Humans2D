using System.Drawing;
using System.Linq;

namespace EnginePart
{
	public class NativeDrawDevice : IDrawDevice
	{
		private readonly Graphics graphics;
		private readonly Pen pen;

		private void ApplyColor (ref Color32 color)
		{
			pen.Color = Color.FromArgb (color.a, color.r, color.g, color.b);
		}

		public NativeDrawDevice (Graphics graphics, Pen pen)
		{
			this.graphics = graphics;
			this.pen = new Pen(pen.Color, pen.Width);
		}

		public void DrawLine (Color32 color, Vector2 a, Vector2 b)
		{
			ApplyColor (ref color);
			graphics.DrawLine (pen, (Point)a, (Point)b);
		}

		public void DrawText (Color32 color, Vector2 a, string text)
		{
			graphics.DrawString (text, SystemFonts.DefaultFont, pen.Brush, (Point)a);
		}

		public void DrawLines (Color32 color, params Vector2[] args)
		{
			ApplyColor (ref color);
			graphics.DrawLines (pen, args.Select (v => (Point)v).ToArray());
		}

		public void DrawEllipse (Color32 color, Vector2 center, Vector2 size)
		{
			ApplyColor (ref color);
			graphics.DrawEllipse (pen, new RectangleF(center.x - size.x * 0.5f, center.y - size.y * 0.5f, size.x, size.y));
		}

		public void FillEllipse (Color32 color, Vector2 center, Vector2 size)
		{
			ApplyColor (ref color);
			graphics.FillEllipse (pen.Brush, new RectangleF (center.x - size.x * 0.5f, center.y - size.y * 0.5f, size.x, size.y));
		}
	}
}