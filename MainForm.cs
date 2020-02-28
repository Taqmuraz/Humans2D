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
	public partial class MainForm : Form
	{
		public static string debug;

		Point mouseLocation;
		HumanRenderer human;
		private IDrawDevice device;

		public MainForm ()
		{
			device = new NativeDrawDevice (CreateGraphics(), Pens.White);

			SetStyle (ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);

			InitializeComponent ();

			human = new HumanRenderer ();
			human.root.localScale = new Vector2 (55, 55);
			human.root.position = new Vector2 (Width >> 1, Height >> 1);
			CreateHumanControl ();

			Timer timer = new Timer ();
			timer.Interval = 10;
			timer.Tick += (s, e) => Refresh ();
			timer.Start ();
		}

		private void CreateHumanControl ()
		{
			int index = 0;
			foreach (var bone in human.GetBones ())
			{
				var label = new Label ();
				label.Text = bone.name;
				label.Bounds = new Rectangle (0, index * 25, 100, 25);
				label.Parent = this;

				var rotation = new HScrollBar ();
				float curRot = bone.bone.rotation;
				rotation.Maximum = 180;
				rotation.Minimum = -180;
				rotation.ValueChanged += (s, e) =>
				{
					bone.bone.rotation = curRot + rotation.Value;
				};
				rotation.Bounds = new Rectangle (200, 25 * index, 100, 10);
				rotation.Parent = this;

				var parent = bone.bone.parent;
				var pos = bone.bone.localPosition;
				var toggle = new CheckBox ();
				toggle.Checked = true;
				toggle.CheckedChanged += (s, e) =>
				{
					if (toggle.Checked)
					{
						bone.bone.localPosition = bone.bone.position;
						bone.bone.parent = parent;
					}
					else
					{
						bone.bone.parent = parent;
						bone.bone.localPosition = pos;
					}
					return;
				};
				toggle.Bounds = new Rectangle (100, 25 * index, 100, 25);
				toggle.Text = "Enable parent";
				toggle.Parent = this;

				index++;
			}
		}

		protected override void OnMouseMove (MouseEventArgs e)
		{
			base.OnMouseMove (e);
			mouseLocation = (Point)ScreenToView((Vector2)e.Location);
		}
		protected override void OnMouseClick (MouseEventArgs e)
		{
			base.OnMouseClick (e);
			human.root.position = (Vector2)mouseLocation;
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
			g.Clear (Color.Black);
			g.DrawString (human.ToString(), SystemFonts.DefaultFont, Brushes.White, 300f, 0f);

			var trans = g.Transform;

			Point center = new Point (Width >> 1, Height >> 1);
			trans.Scale (-1f, 1f);
			trans.RotateAt (180f, center);
			trans.Translate (Width, 0f);
			g.Transform = trans;

			Rendering.Draw (new NativeDrawDevice(g, Pens.White));
		}
	}
}
