using System.Drawing;
using System.Windows.Forms;
using EnginePart;
using System;

namespace WinFormsGraphics
{
	public partial class GraphicsPanel : Panel, ICanvas, MouseControlEvents.IMouseControl
	{
		protected Vector2 mouseLocation { get; private set; }

		public GraphicsPanel ()
		{
			SetStyle (ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);

			ResizeRedraw = false;
			Timer timer = new Timer ();
			timer.Interval = 1;
			timer.Tick += (s, e) => Redraw ();
			timer.Start ();

			MouseControlEvents.AssignControl (this);
		}

		private void Redraw ()
		{
			if (Parent != null) Bounds = Parent.Bounds;
			Refresh ();
		}

		protected override void OnPaint (PaintEventArgs e)
		{
			var g = e.Graphics;

			var device = new NativeDrawDevice (g, this);
			g.Clear (Color.Black);


			e.Graphics.DrawString (debug, SystemFonts.DefaultFont, Brushes.Red, 15, 15);


			device.LoadIdentity ();

			Rendering.Draw (device);
		}
		public Vector2 GetSize ()
		{
			return new Vector2 (Width, Height);
		}

		protected override void OnMouseMove (MouseEventArgs e)
		{
			base.OnMouseMove (e);
			mouseLocation = ScreenToView ((Vector2)e.Location);
			MoveControlEvent (mouseLocation);
		}
		protected override void OnMouseClick (MouseEventArgs e)
		{
			base.OnMouseClick (e);
			mouseLocation = ScreenToView ((Vector2)e.Location);
			switch (e.Button)
			{
				case MouseButtons.Left:
					ClickControlEvent (mouseLocation);
					break;
				case MouseButtons.Right:
					ContextClickControlEvent (mouseLocation);
					break;
			}
		}
		protected override void OnMouseDown (MouseEventArgs e)
		{
			base.OnMouseDown (e);
			mouseLocation = ScreenToView ((Vector2)e.Location);
			DownControlEvent (mouseLocation);
		}
		protected override void OnMouseUp (MouseEventArgs e)
		{
			base.OnMouseUp (e);
			mouseLocation = ScreenToView ((Vector2)e.Location);
			UpControlEvent (mouseLocation);
		}

		protected Vector2 ScreenToView (Vector2 origin)
		{
			origin.y = Height - origin.y;
			return origin;
		}

		public event Action<Vector2> ClickControlEvent;
		public event Action<Vector2> DownControlEvent;
		public event Action<Vector2> UpControlEvent;
		public event Action<Vector2> MoveControlEvent;
		public event Action<Vector2> ContextClickControlEvent;
	}
}
