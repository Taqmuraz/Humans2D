namespace EnginePart
{
	public static partial class MouseControlEvents
	{
		public interface IMouseControl
		{
			event System.Action<Vector2> ClickControlEvent;
			event System.Action<Vector2> DownControlEvent;
			event System.Action<Vector2> UpControlEvent;
			event System.Action<Vector2> MoveControlEvent;
			event System.Action<Vector2> ContextClickControlEvent;
		}
	}
}