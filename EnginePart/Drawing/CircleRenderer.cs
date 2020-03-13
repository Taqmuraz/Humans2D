namespace EnginePart
{
	public class CircleRenderer : Rendering.Renderer
	{
		private Transform transform;
		public Color32 color { private get; set; }

		public CircleRenderer (Transform transform, Color32 color)
		{
			this.transform = transform;
			this.color = color;
		}

		public override void Draw (IDrawDevice device)
		{
			device.DrawEllipse(color, transform.position, transform.globalScale);
		}
	}
	public class LineRenderer : Rendering.Renderer
	{
		Transform transform;
		public Color32 color { private get; set; }

		public LineRenderer (Transform transform) : this (transform, Color32.white)
		{
		}

		public LineRenderer (Transform transform, Color32 color)
		{
			this.transform = transform;
			this.color = color;
		}

		public override void Draw (IDrawDevice device)
		{
			Vector2 pos = transform.position;
			device.DrawLine (color, pos, pos + transform.right);
		}
	}
}