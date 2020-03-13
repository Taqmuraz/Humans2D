using EnginePart;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;

namespace AnimationCreateForm
{
	public abstract class InteractiveObject : MouseControlEvents.MouseHandler
	{
		protected Transform transform { get; private set; }

		protected struct ContextMenuItem
		{
			public string text;
			public Action action;

			public ContextMenuItem (string text, Action action)
			{
				this.text = text;
				this.action = action;
			}
		}

		private Dictionary<string, ContextMenuStrip> contextMenus;

		protected abstract Color32 color { set; }

		protected abstract Color32 normalColor { get; }
		protected abstract Color32 hoveredColor { get; }
		protected abstract Color32 selectedColor { get; }

		public InteractiveObject (Transform transform)
		{
			this.transform = transform;
			contextMenus = new Dictionary<string, ContextMenuStrip> ();
			InitalizeGraphics ();
			color = normalColor;
		}

		protected abstract void InitalizeGraphics ();

		protected override void OnMouseDown (Vector2 point)
		{
			color = selectedColor;
		}
		protected override void OnMouseUp (Vector2 point)
		{
			color = isHovered ? hoveredColor : normalColor;
		}
		protected override void OnMouseEnter (Vector2 point)
		{
			color = isSelected ? selectedColor : hoveredColor;
		}
		protected override void OnMouseExit (Vector2 point)
		{
			color = isSelected ? selectedColor : normalColor;
		}

		protected void CreateContextMenu (string menuName, params ContextMenuItem[] items)
		{
			var menu = new ContextMenuStrip ();
			foreach (var item in items) menu.Items.Add (item.text, null, (s, e) => item.action ());
			contextMenus.Add (menuName, menu);
		}

		protected void ShowContextMenu (string menuName)
		{
			if (contextMenus.ContainsKey (menuName))
			{
				contextMenus[menuName].Show (Form.ActiveForm, (Point)transform.position);
			}
			else throw new ArgumentException ($"There are no menu with name {menuName}");
		}
	}
}
