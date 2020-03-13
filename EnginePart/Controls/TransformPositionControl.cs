using System;

namespace EnginePart
{

	public sealed class TransformPositionControl : TransformControl
	{
		public TransformPositionControl (Transform transform) : base (transform)
		{
		}

		protected override void OnDrag (Vector2 point, Vector2 delta, Vector2 raw)
		{
			transform.position += delta;
		}

		protected override Vector2 GetCircleLocalScale ()
		{
			return Vector2.one * 0.4f;
		}
		protected override Vector2 GetCircleLocalPosition ()
		{
			return Vector2.zero;
		}
	}
	public static class Debug
	{
		public static void Log<T>(T arg)
		{
			WinFormsGraphics.GraphicsPanel.GraphicsPanelDebug.Log (arg.ToString());
		}
	}
}