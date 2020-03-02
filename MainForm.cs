using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EnginePart;

namespace WinFormsGraphics
{
	public partial class MainForm : Form, MouseControlEvents.IMouseControl, ICanvas
	{
		Point mouseLocation;
		HumanRenderer human;

		public MainForm ()
		{
			SetStyle (ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
			WindowState = FormWindowState.Maximized;

			InitializeComponent ();

			human = new HumanRenderer ();
			human.root.localScale = new Vector2 (55, 55);
			human.root.position = new Vector2 (Width >> 1, Height >> 1);
			CreateHumanControl ();

			MouseControlEvents.AssignControl (this);

			Timer timer = new Timer ();
			timer.Interval = 10;
			timer.Tick += (s, e) => Refresh ();
			timer.Start ();
		}

		private void CreateHumanControl ()
		{
			foreach (var bone in human.GetBones ())
			{
				if (bone.bone.parent == null) new TransformPositionControl (bone.bone);
				new TransformRotationControl (bone.bone);
			}
		}

		protected override void OnMouseMove (MouseEventArgs e)
		{
			base.OnMouseMove (e);
			mouseLocation = (Point)ScreenToView((Vector2)e.Location);
			MoveControlEvent ((Vector2)mouseLocation);
		}
		protected override void OnMouseClick (MouseEventArgs e)
		{
			base.OnMouseClick (e);
			mouseLocation = (Point)ScreenToView ((Vector2)e.Location);
			ClickControlEvent ((Vector2)mouseLocation);
		}
		protected override void OnMouseDown (MouseEventArgs e)
		{
			base.OnMouseDown (e);
			mouseLocation = (Point)ScreenToView ((Vector2)e.Location);
			DownControlEvent ((Vector2)mouseLocation);
		}
		protected override void OnMouseUp (MouseEventArgs e)
		{
			base.OnMouseUp (e);
			mouseLocation = (Point)ScreenToView ((Vector2)e.Location);
			UpControlEvent ((Vector2)mouseLocation);
		}

		protected Vector2 ScreenToView (Vector2 origin)
		{
			origin.y = Height - origin.y;
			return origin;
		}

		protected override void OnPaint (PaintEventArgs e)
		{
			base.OnPaint (e);
			var g = e.Graphics;

			var device = new NativeDrawDevice (g, this);

			g.Clear (Color.Black);
			g.DrawString (debug, SystemFonts.DefaultFont, Brushes.White, 300f, 0f);

			device.LoadIdentity ();

			Rendering.Draw (device);
		}

		public event Action<Vector2> ClickControlEvent;
		public event Action<Vector2> DownControlEvent;
		public event Action<Vector2> UpControlEvent;
		public event Action<Vector2> MoveControlEvent;

		public Vector2 GetSize ()
		{
			return new Vector2 (Width, Height);
		}
	}
}
