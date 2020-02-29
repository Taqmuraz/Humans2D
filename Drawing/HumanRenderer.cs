namespace EnginePart
{
	public class HumanRenderer : Rendering.Renderer
	{
		public struct HumanBone
		{
			public readonly string name;
			public readonly Transform bone;

			public HumanBone (string name, Transform bone)
			{
				this.name = name;
				this.bone = bone;
			}
		}
		public Transform root { get; private set; }
		public Transform bodyBone { get; private set; }
		private Transform neckBone;
		private Transform legLBone;
		private Transform legRBone;
		private Transform kneeLBone;
		private Transform kneeRBone;
		private Transform handLBone;
		private Transform handRBone;
		private Transform headBone;

		public HumanRenderer ()
		{
			root = new Transform (Mathf.CreateTransformMatrix(Vector2.zero, 0f));

			bodyBone = new Transform (Mathf.CreateTransformMatrix (Vector2.zero, 0f));
			bodyBone.parent = root;

			neckBone = new Transform (Mathf.CreateTransformMatrix (Vector2.up, 0f));
			neckBone.parent = bodyBone;

			legLBone = new Transform (Mathf.CreateTransformMatrix (Vector2.zero, -105f));
			legLBone.parent = bodyBone;

			legRBone = new Transform (Mathf.CreateTransformMatrix (Vector2.zero, -75f));
			legRBone.parent = bodyBone;

			kneeLBone = new Transform (Mathf.CreateTransformMatrix (Vector2.right, 0f));
			kneeLBone.parent = legLBone;

			kneeRBone = new Transform (Mathf.CreateTransformMatrix (Vector2.right, 0f));
			kneeRBone.parent = legRBone;

			handLBone = new Transform (Mathf.CreateTransformMatrix (Vector2.zero, -15f));
			handLBone.parent = neckBone;

			handRBone = new Transform (Mathf.CreateTransformMatrix (Vector2.zero, 195f));
			handRBone.parent = neckBone;

			headBone = new Transform (Mathf.CreateTransformMatrix (Vector2.zero, 0f));
			headBone.parent = neckBone;
		}

		public override void Draw (IDrawDevice device)
		{
			Color32 color = new Color32 (0x00, 0xff, 0xff, 0xff);
			device.FillEllipse (color, headBone.position + headBone.up * 0.35f, (headBone.globalScale) * 0.5f);
			device.DrawLine (color, neckBone.position, headBone.position + headBone.up * 0.35f);
			device.DrawLine (color, root.position, bodyBone.position);
			device.DrawLine (color, bodyBone.position, neckBone.position);
			device.DrawLine (color, legLBone.position, legLBone.right + legLBone.position);
			device.DrawLine (color, legRBone.position, legRBone.right + legRBone.position);
			device.DrawLine (color, handLBone.position, handLBone.right + handLBone.position);
			device.DrawLine (color, handRBone.position, handRBone.right + handRBone.position);
			device.DrawLine (color, kneeLBone.position, kneeLBone.right + kneeLBone.position);
			device.DrawLine (color, kneeRBone.position, kneeRBone.right + kneeRBone.position);
		}

		public override string ToString ()
		{
			return $"Root\n{root}\nNeck{neckBone}\nKneeL\n{kneeLBone}\nKneeR\n{kneeRBone}";
		}

		public HumanBone[] GetBones ()
		{
			return new HumanBone[10]
			{
				new HumanBone("Root", root),
				new HumanBone ("Body", bodyBone),
				new HumanBone ("Head", headBone),
				new HumanBone ("Neck", neckBone),
				new HumanBone ("HandL", handLBone),
				new HumanBone ("HandR", handRBone),
				new HumanBone ("LegL", legLBone),
				new HumanBone ("LegR", legRBone),
				new HumanBone("KneeL", kneeLBone),
				new HumanBone("KneeR", kneeRBone)
			};
		}
	}
}