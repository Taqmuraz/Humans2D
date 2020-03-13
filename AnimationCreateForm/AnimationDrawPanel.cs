using EnginePart;
using System;
using System.Windows.Forms;
using WinFormsGraphics;

namespace AnimationCreateForm
{
	public class AnimationDrawPanel : GraphicsPanel
	{
		private class PanelClicHandler : MouseControlEvents.MouseHandler
		{
			private AnimationDrawPanel panel;
			private ContextMenuStrip cMenu;

			public override int layer => -1;

			public PanelClicHandler (AnimationDrawPanel panel)
			{
				this.panel = panel;
				cMenu = new ContextMenuStrip ();
				cMenu.Items.Add ("Create transform", null, panel.CreateTransform);
			}

			public override bool Contains (Vector2 point)
			{
				return true;
			}
			public override void OnContextClick (Vector2 point)
			{
				cMenu.Show (panel, MousePosition);
			}
		}

		public AnimationDrawPanel ()
		{
			new PanelClicHandler (this);
		}

		private void CreateTransform (object sender, EventArgs args)
		{
			var transform = new Transform ();
			transform.position = mouseLocation;
			transform.localScale = Vector2.one * 20f;
			new AnimationObject (transform);
		}
	}
}
