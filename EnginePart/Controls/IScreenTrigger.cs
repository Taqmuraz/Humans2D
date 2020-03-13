namespace EnginePart
{
	public static partial class MouseControlEvents
	{
		public interface IScreenTrigger
		{
			bool Contains (Vector2 point);
			int layer { get; }
			bool isSelected { get; }
			bool isHovered { get; }
		}
	}
}