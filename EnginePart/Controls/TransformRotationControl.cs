namespace EnginePart
{
	public sealed class TransformScaleControl : TransformRotationControl
	{
		public TransformScaleControl (Transform transform) : base (transform)
		{
		}

		protected override void OnDrag (Vector2 point, Vector2 delta, Vector2 raw)
		{
			base.OnDrag (point, delta, raw);
			transform.globalScale = Vector2.one * ((point - transform.position).length / GetCircleLocalPosition().length);
		}
		protected override void OnMouseDown (Vector2 point)
		{
			base.OnMouseDown (point);
		}

		protected override Color32 GetEnterColor ()
		{
			return Color32.green;
		}
		protected override Color32 GetExitColor ()
		{
			return Color32.white * 0.75f;
		}
		protected override Vector2 GetCircleLocalPosition ()
		{
			return new Vector2 (0.5f, 0f);
		}
		protected override Vector2 GetCircleLocalScale ()
		{
			return Vector2.one * 0.3f;
		}
	}
	public class TransformRotationControl : TransformControl
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
			return Vector2.one * 0.2f;
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