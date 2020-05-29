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

		protected class ContextMenuAction : IToolStripItemTemplate
		{
			string text;
			Action action;

			public ContextMenuAction (string text, Action action)
			{
				this.text = text;
				this.action = action;
			}

			public void AddItemToStrip (ToolStripItemCollection items)
			{
				items.Add (text, null, (s, e) => action ());
			}
		}
		protected class ContextMenuDropdown : IToolStripItemTemplate
		{
			string text;
			IToolStripItemTemplate[] elements;

			public ContextMenuDropdown (string text, params IToolStripItemTemplate[] elements)
			{
				this.text = text;
				this.elements = elements;
			}

			public void AddItemToStrip (ToolStripItemCollection items)
			{
				ToolStripMenuItem toolStripDropDown = new ToolStripMenuItem (text);
				foreach (var item in elements) item.AddItemToStrip (toolStripDropDown.DropDownItems);
				items.Add (toolStripDropDown);
			}
		}
		protected interface IToolStripItemTemplate
		{
			void AddItemToStrip (ToolStripItemCollection items);
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

		protected void CreateContextMenu (string menuName, params IToolStripItemTemplate[] items)
		{
			var menu = new ContextMenuStrip ();
			foreach (var item in items) item.AddItemToStrip (menu.Items);
			contextMenus.Add (menuName, menu);
		}

		protected void ShowContextMenu (string menuName)
		{
			if (contextMenus.ContainsKey (menuName))
			{
				Vector2 pos = transform.position;
				pos.y = Form.ActiveForm.Height - pos.y;
				contextMenus[menuName].Show (Form.ActiveForm, (Point)pos);
			}
			else throw new ArgumentException ($"There are no menu with name {menuName}");
		}
	}
}
