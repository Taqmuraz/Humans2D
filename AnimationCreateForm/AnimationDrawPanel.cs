using EnginePart;
using System;
using System.Windows.Forms;
using WinFormsGraphics;

namespace AnimationCreateForm
{
	public class AnimationDrawPanel : GraphicsPanel
	{
		private class PanelClicHandler : InteractiveObject
		{
			private AnimationDrawPanel panel;

			public override int layer => -1;

			public PanelClicHandler (AnimationDrawPanel panel) : base (new Transform(Matrix3x3.identity))
			{
				this.panel = panel;

				CreateContextMenu ("Main",
					new ContextMenuAction ("Create transform", panel.CreateTransform),
					new ContextMenuDropdown("Enable layer", new ContextMenuAction("DEFAULT", () => Rendering.SetLayerEnabled(DrawLayer.DEFAULT, true)), new ContextMenuAction ("UI", () => Rendering.SetLayerEnabled (DrawLayer.UI, true))),
					new ContextMenuDropdown("Disable layer", new ContextMenuAction("DEFAULT", () => Rendering.SetLayerEnabled(DrawLayer.DEFAULT, false)), new ContextMenuAction ("UI", () => Rendering.SetLayerEnabled (DrawLayer.UI, false)))
					);
			}

			public override bool Contains (Vector2 point)
			{
				return true;
			}
			public override void OnContextClick (Vector2 point)
			{
				transform.position = point;
				ShowContextMenu ("Main");
			}

			protected override Color32 color { set { return; } }
			protected override Color32 normalColor { get; }
			protected override Color32 hoveredColor { get; }
			protected override Color32 selectedColor { get; }

			protected override void InitalizeGraphics ()
			{
			}
		}

		public AnimationDrawPanel ()
		{
			new PanelClicHandler (this);
		}

		private void CreateTransform ()
		{
			var transform = new Transform ();
			transform.position = mouseLocation;
			transform.localScale = Vector2.one * 20f;
			new AnimationObject (transform);
		}
	}
}
