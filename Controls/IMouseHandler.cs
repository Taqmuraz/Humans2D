namespace EnginePart
{
	public static partial class MouseControlEvents
	{
		public interface IMouseHandler
		{
			void OnMouseDown (Vector2 point);
			void OnMouseUp (Vector2 point);
			void OnMouseClick (Vector2 point);
			void OnMouseMove (Vector2 point);
		}
	}
}