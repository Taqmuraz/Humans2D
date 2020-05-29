using EnginePart;
using System.Collections.Generic;


namespace AnimationCreateForm
{
	public class AnimationObject : InteractiveObject
	{
		private IEnumerable<TransformControl> transformControls;
		private LineRenderer line;

		public AnimationObject (Transform transform) : base (transform)
		{
			var tcList = new List<TransformControl> ();

			if (transform.parent == null) tcList.Add (new TransformPositionControl(transform));
			tcList.Add (new TransformRotationControl(transform));
			tcList.Add (new TransformScaleControl(transform));

			transformControls = tcList;
			CreateContextMenu ("Main",
				new ContextMenuDropdown("Add child", new ContextMenuAction ("At center", () => AddChild (Vector2.zero)), new ContextMenuAction ("At end", () => AddChild (Vector2.right))),
				new ContextMenuDropdown("Add primitive", new ContextMenuAction ("Line", () => new LineRenderer (transform)), new ContextMenuAction ("Ellipse", () => new CircleRenderer (transform, Color32.white))),
				new ContextMenuAction("Remove", Remove),
				new ContextMenuAction("Create copy", CreateCopy)
				);
		}

		protected override void InitalizeGraphics ()
		{
			line = new LineRenderer (transform);
			line.layer = DrawLayer.UI;
		}

		protected override Color32 normalColor => Color32.white * 0.75f;
		protected override Color32 hoveredColor => Color32.white;
		protected override Color32 selectedColor => Color32.green;

		protected override Color32 color
		{
			set
			{
				value.a = 64;
				line.color = value;
			}
		}

		public override bool Contains (Vector2 point)
		{
			Vector2 pos = transform.position;
			Vector2 start = pos;
			Vector2 end = pos + transform.right;

			Vector2 point_delta = point - start;
			Vector2 end_delta = end - start;

			float b = Vector2.Dot (point_delta, end_delta) / end_delta.length;

			if (b < 0 || b > end_delta.length) return false;

			b = b.Abs ();

			float a = point_delta.length;

			float c = ((a - b) * (a + b)).Abs ().Sqrt ();

			return c <= transform.globalScale.length * 0.1f;
		}

		public override void OnContextClick (Vector2 point)
		{
			ShowContextMenu ("Main");
		}

		private void AddChild (Vector2 position)
		{
			Transform child = new Transform (Matrix3x3.identity);
			child.parent = transform;
			child.localPosition = position;
			child.localRotation = 15f;
			new AnimationObject (child);
		}
		private void Remove ()
		{
			line.Dispose ();
			foreach (var t in transformControls) t.Dispose ();
			this.Dispose ();
		}
		private void CreateCopy ()
		{
			Transform copy = Transform.CreateDeepCopy (transform);

			copy.position += copy.globalScale * 0.5f;
			CreateCopy_Step (copy);
		}
		private void CreateCopy_Step (Transform transform)
		{
			new AnimationObject (transform);
			foreach (var child in transform.GetChilds())
			{
				CreateCopy_Step (child);
			}
		}
	}
}
