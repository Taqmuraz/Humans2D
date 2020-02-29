using System.Collections.Generic;

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
		}

		private static void OnDown (Vector2 point)
		{
			foreach (var m in mouseHandlers) m.OnMouseDown (point);
		}
		private static void OnUp (Vector2 point)
		{
			foreach (var m in mouseHandlers) m.OnMouseUp (point);
		}

		private static Vector2 lastMousePosition;
		private static void OnMove (Vector2 point)
		{
			foreach (var m in mouseHandlers) m.OnMouseMove (point);
			lastMousePosition = point;
		}
		private static void OnClick (Vector2 point)
		{
			foreach (var m in mouseHandlers) m.OnMouseClick (point);
		}
	}
}