namespace EnginePart
{
	public class TransformHierarchyRenderer : Rendering.Renderer
	{
		private Transform root;

		public TransformHierarchyRenderer (Transform root)
		{
			this.root = root;
		}

		public override void Draw (IDrawDevice device)
		{
			DrawChilds (root, device);
		}

		private void DrawChilds (Transform transform, IDrawDevice device)
		{
			foreach (var child in transform.GetChilds())
			{
				device.DrawLine (Color32.blue, child.position, child.position + child.right);
				DrawChilds (child, device);
			}
		}
	}
}