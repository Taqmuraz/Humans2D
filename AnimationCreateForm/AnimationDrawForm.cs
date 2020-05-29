using WinFormsGraphics;
using System;
using EnginePart;

namespace AnimationCreateForm
{
	public partial class AnimationDrawForm : GraphicsFormBase
	{
		public AnimationDrawForm ()
		{
			InitializeComponent ();
			Text = "Animation draw window";

			TransformOptimisationTest ();
		}
		protected override GraphicsPanel CreatePanel ()
		{
			return new AnimationDrawPanel ();
		}

		private void TransformOptimisationTest ()
		{
			Transform[] transforms = new Transform[10];

			for (int i = 0; i < transforms.Length; i++)
			{
				transforms[i] = new Transform ();
				if (i > 0) transforms[i].parent = transforms[i - 1];
			}

			foreach (var trans in transforms)
			{
				TestTransform (trans);
			}
		}
		private void TestTransform (Transform transform)
		{
			var start = DateTime.Now;
			var p = Vector2.zero;

			for (int i = 0; i < 500000; i++)
			{
				p = transform.position;
			}

			var end = DateTime.Now;

			GraphicsPanel.GraphicsPanelDebug.Log ((end - start).TotalMilliseconds.ToString());
		}
	}
}
