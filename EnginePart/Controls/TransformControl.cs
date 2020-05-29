namespace EnginePart
{
	public abstract class TransformControl : MouseControlEvents.MouseHandler
	{
		protected readonly Transform positionCircle;
		protected readonly CircleRenderer positionCircleRenderer;
		protected readonly Transform transform;

		public override int layer => 1;

		public TransformControl (Transform transform)
		{
			this.transform = transform;
			positionCircle = new Transform (Matrix3x3.identity);
			positionCircle.parent = transform;
			positionCircle.localPosition = GetCircleLocalPosition ();
			positionCircle.localScale = GetCircleLocalScale ();

			positionCircleRenderer = new CircleRenderer (positionCircle, GetExitColor());
			positionCircleRenderer.layer = DrawLayer.UI;
		}

		protected abstract Vector2 GetCircleLocalPosition ();
		protected abstract Vector2 GetCircleLocalScale ();

		protected virtual Color32 GetEnterColor ()
		{
			return Color32.green;
		}
		protected virtual Color32 GetExitColor ()
		{
			return Color32.blue;
		}

		protected sealed override void OnDispose ()
		{
			positionCircleRenderer.Dispose ();
		}

		protected override void OnMouseDown (Vector2 point)
		{
			positionCircleRenderer.color = GetEnterColor ();
		}
		protected override void OnMouseUp (Vector2 point)
		{
			positionCircleRenderer.color = GetExitColor ();
		}

		protected sealed override void OnMouseEnter (Vector2 point)
		{
			if (!isSelected) positionCircleRenderer.color = GetEnterColor();
		}
		protected sealed override void OnMouseExit (Vector2 point)
		{
			if (!isSelected) positionCircleRenderer.color = GetExitColor ();
		}

		public sealed override bool Contains (Vector2 point)
		{
			Vector2 hScale = positionCircle.globalScale * 0.5f;
			Vector2 pos = positionCircle.position;
			
			return point.ContainsBox (pos - hScale, pos + hScale);
		}
	}
}