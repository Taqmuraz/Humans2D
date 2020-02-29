namespace EnginePart
{
	public abstract class TransformControl : MouseControlEvents.MouseHandler
	{
		protected readonly Transform positionCircle;
		protected readonly CircleRenderer positionCircleRenderer;
		protected readonly Transform transform;

		public TransformControl (Transform transform)
		{
			this.transform = transform;
			positionCircle = new Transform (Matrix3x3.identity);
			positionCircle.parent = transform;
			positionCircle.localPosition = GetCircleLocalPosition ();
			positionCircle.localScale = GetCircleLocalScale ();

			positionCircleRenderer = new CircleRenderer (positionCircle, GetExitColor());
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

		protected sealed override void OnMouseEnter (Vector2 point)
		{
			positionCircleRenderer.color = GetEnterColor();
		}
		protected sealed override void OnMouseExit (Vector2 point)
		{
			positionCircleRenderer.color = GetExitColor ();
		}

		public sealed override bool Contains (Vector2 point)
		{
			return (positionCircle.position - point).length <= positionCircle.globalScale.length.Sqrt ();
		}
	}
}