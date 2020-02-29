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
			device.DrawEllipse(color, transform.position, transform.globalScale * 0.25f);
		}
	}
}