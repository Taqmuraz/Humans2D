using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace EnginePart
{
	public sealed class NativeDrawDevice : TextureDatabase, IDrawDevice
	{
		private readonly Graphics graphics;
		private readonly Pen pen;
		private ICanvas canvas;

		private void ApplyColor (ref Color32 color)
		{
			pen.Color = Color.FromArgb (color.a, color.r, color.g, color.b);
		}

		public NativeDrawDevice (Graphics graphics, ICanvas canvas)
		{
			this.graphics = graphics;
			pen = new Pen(Color.White, 5f);
			this.canvas = canvas;
		}

		public void DrawLine (Color32 color, Vector2 a, Vector2 b)
		{
			ApplyColor (ref color);

			LoadIdentity ();
			graphics.DrawLine (pen, a, b);
		}

		public void DrawText (Color32 color, Vector2 a, string text)
		{
			ApplyColor (ref color);

			graphics.ResetTransform ();
			a = InvertIdentity (a);
			graphics.DrawString (text, SystemFonts.DefaultFont, pen.Brush, (Point)a);
			
			LoadIdentity ();
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

		public void DrawImage (int index, Matrix3x3 matrix)
		{
			var identity = CreateIdentity ();

			LoadMatrix (identity.GetRotation () * matrix * identity);

			graphics.DrawImage (GetTexture(index), new RectangleF(-0.5f, -0.5f, 1f, 1f));
			LoadIdentity ();
		}

		private void ApplyMatrix (Matrix3x3 matrix)
		{
			matrix *= CreateIdentity ();
			LoadMatrix (matrix);
		}
		private void LoadMatrix (Matrix3x3 matrix)
		{
			graphics.Transform = new Matrix (matrix[0, 0], matrix[0, 1], matrix[1, 0], matrix[1, 1], matrix[2, 0], matrix[2, 1]);
		}
		private void RotateAt (Vector2 point, float rotation)
		{
			var trans = graphics.Transform;
			trans.RotateAt (rotation, point);
			graphics.Transform = trans;
		}
		private void Scale (Vector2 scale)
		{
			var trans = graphics.Transform;
			trans.Scale (scale.x, scale.y);
			graphics.Transform = trans;
		}

		private Matrix3x3 CreateIdentity ()
		{
			var size = canvas.GetSize ();

			var matrix = Matrix3x3.identity;
			matrix *= Matrix3x3.CreateScaleMatrix (new Vector2 (-1, 1f));
			matrix *= Matrix3x3.CreateRotationMatrix (180f);
			matrix *= Matrix3x3.CreateTranslationMatrix (new Vector2(0f, size.y));
			return matrix;
		}

		public void LoadIdentity ()
		{
			LoadMatrix (CreateIdentity());
		}

		private Vector2 InvertIdentity (Vector2 origin)
		{
			return (Vector2)(CreateIdentity ().GetInversed () * new Vector3(origin.x, origin.y, 1f));
		}

		public void DrawEllipse (Color32 color, Matrix3x3 matrix)
		{
			ApplyColor (ref color);
			ApplyMatrix (matrix);
			graphics.DrawEllipse (pen, new RectangleF(-0.5f, -0.5f, 1f, 1f));
			LoadIdentity ();
		}

		public void FillEllipse (Color32 color, Matrix3x3 matrix)
		{
			ApplyColor (ref color);
			ApplyMatrix (matrix);
			graphics.FillEllipse (pen.Brush, new RectangleF (-0.5f, -0.5f, 1f, 1f));
			LoadIdentity ();
		}
	}
}