using System.Collections.Generic;
using System.Linq;

namespace EnginePart
{
	public static partial class MouseControlEvents
	{
		private static List<IMouseHandler> mouseHandlers = new List<IMouseHandler> ();

		public static void AssignControl (IMouseControl control)
		{
			control.ClickControlEvent += OnClick;
			control.DownControlEvent += OnDown;
			control.UpControlEvent += OnUp;
			control.MoveControlEvent += OnMove;
			control.ContextClickControlEvent += OnContextClick;
		}

		private static void OnDown (Vector2 point)
		{
			TraceHandler (point)?.OnMouseDown (point);
		}
		private static void OnUp (Vector2 point)
		{
			foreach (var m in mouseHandlers)
			{
				if (m.isSelected) m.OnMouseUp (point);
			}
		}

		private static Vector2 lastMousePosition;
		private static void OnMove (Vector2 point)
		{
			foreach (var m in mouseHandlers)
			{
				if (m.Contains (lastMousePosition))
				{
					if (!m.Contains (point)) m.OnMouseExit (point);
				}
				else
				{
					if (m.Contains (point)) m.OnMouseEnter (point);
				}
				m.OnMouseMove (point);
			}

			lastMousePosition = point;
		}
		private static void OnClick (Vector2 point)
		{
			TraceHandler(point)?.OnMouseClick (point);
		}
		private static void OnContextClick (Vector2 point)
		{
			TraceHandler (point)?.OnContextClick (point);
		}

		private static IMouseHandler TraceHandler (Vector2 point)
		{
			return mouseHandlers.OrderByDescending(m => m.layer).FirstOrDefault (m => m.Contains(point));
		}
	}
}