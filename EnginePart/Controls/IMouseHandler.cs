namespace EnginePart
{
	public static partial class MouseControlEvents
	{
		public interface IMouseHandler : IScreenTrigger
		{
			void OnMouseDown (Vector2 point);
			void OnMouseUp (Vector2 point);
			void OnMouseClick (Vector2 point);
			void OnMouseMove (Vector2 point);
			void OnMouseEnter (Vector2 point);
			void OnMouseExit (Vector2 point);
			void OnContextClick (Vector2 point);
		}
	}
}