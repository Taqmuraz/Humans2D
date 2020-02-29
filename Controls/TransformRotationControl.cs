namespace EnginePart
{
	public sealed class TransformRotationControl : TransformControl
	{
		public TransformRotationControl (Transform transform) : base (transform)
		{
		}
		protected override void OnDrag (Vector2 point, Vector2 delta, Vector2 raw)
		{
			transform.rotation = (point - transform.position).GetDirectionAngle ();
		}
		protected override Vector2 GetCircleLocalScale ()
		{
			return Vector2.one * 0.6f;
		}
		protected override Vector2 GetCircleLocalPosition ()
		{
			return Vector2.right;
		}
		protected override Color32 GetEnterColor ()
		{
			return Color32.green;
		}
		protected override Color32 GetExitColor ()
		{
			return Color32.red;
		}
	}
}