using System.Collections;
using System.Collections.Generic;

namespace EnginePart
{
	public class HumanRenderer : Rendering.Renderer
	{
		public struct HumanBone
		{
			public readonly string name;
			public readonly Transform transform;

			public HumanBone (string name, Transform bone)
			{
				this.name = name;
				this.transform = bone;
			}
		}
		public class HumanSkeleton
		{
			public IEnumerable<HumanBone> bones { get; private set; }

			public HumanSkeleton (HumanRenderer humanRenderer)
			{
				bones = new HumanBone[]
				{
					new HumanBone("Root", humanRenderer.root),
					new HumanBone ("Body", humanRenderer.bodyBone),
					new HumanBone ("Head", humanRenderer.headBone),
					new HumanBone ("Neck", humanRenderer.neckBone),
					new HumanBone ("HandL", humanRenderer.handLBone),
					new HumanBone ("HandR", humanRenderer.handRBone),
					new HumanBone ("LegL", humanRenderer.legLBone),
					new HumanBone ("LegR", humanRenderer.legRBone),
					new HumanBone("KneeL", humanRenderer.kneeLBone),
					new HumanBone("ElbowL", humanRenderer.elbowLBone),
					new HumanBone("ElbowR", humanRenderer.elbowRBone),
					new HumanBone("KneeR", humanRenderer.kneeRBone)
				};
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
		private Transform elbowLBone;
		private Transform elbowRBone;
		private Transform handRBone;
		private Transform headBone;

		public HumanSkeleton skeleton { get; private set; }

		private int headTexture;
		private int bodyTexture;
		private int legTexture;
		private int kneeTexture;
		private int armTexture;
		private int handTexture;

		public HumanRenderer ()
		{
			InitalizeBones ();

			skeleton = new HumanSkeleton (this);

			InitializeTextures ();
		}

		private void InitializeTextures ()
		{
			headTexture = TextureDatabase.LoadTexture ("Images/Human/Head.png");
			armTexture = TextureDatabase.LoadTexture ("Images/Human/Arm.png");
			handTexture = TextureDatabase.LoadTexture ("Images/Human/Hand.png");
			bodyTexture = TextureDatabase.LoadTexture ("Images/Human/Body.png");
			legTexture = TextureDatabase.LoadTexture ("Images/Human/Leg.png");
			kneeTexture = TextureDatabase.LoadTexture ("Images/Human/Knee.png");
		}

		private void InitalizeBones ()
		{
			root = new Transform (Mathf.CreateTransformMatrix (Vector2.zero, 0f));

			bodyBone = new Transform (Mathf.CreateTransformMatrix (Vector2.zero, 0f));
			bodyBone.parent = root;

			neckBone = new Transform (Mathf.CreateTransformMatrix (Vector2.up, 0f));
			neckBone.parent = bodyBone;

			legLBone = new Transform (Mathf.CreateTransformMatrix (new Vector2 (-0.15f, 0f), -105f));
			legLBone.parent = bodyBone;

			legRBone = new Transform (Mathf.CreateTransformMatrix (new Vector2 (0.15f, 0f), -75f));
			legRBone.parent = bodyBone;

			kneeLBone = new Transform (Mathf.CreateTransformMatrix (Vector2.right, 0f));
			kneeLBone.parent = legLBone;

			kneeRBone = new Transform (Mathf.CreateTransformMatrix (Vector2.right, 0f));
			kneeRBone.parent = legRBone;

			handLBone = new Transform (Mathf.CreateTransformMatrix (new Vector2 (-0.25f, 0.8f), 195f));
			handLBone.parent = bodyBone;
			handLBone.localScale = Vector2.one * 0.75f;

			handRBone = new Transform (Mathf.CreateTransformMatrix (new Vector2 (0.25f, 0.8f), -15f));
			handRBone.parent = bodyBone;
			handRBone.localScale = Vector2.one * 0.75f;

			elbowLBone = new Transform (Mathf.CreateTransformMatrix (Vector2.right, 15f));
			elbowLBone.parent = handLBone;

			elbowRBone = new Transform (Mathf.CreateTransformMatrix (Vector2.right, -15f));
			elbowRBone.parent = handRBone;

			headBone = new Transform (Mathf.CreateTransformMatrix (Vector2.up * 0.4f, 0f));
			headBone.parent = neckBone;
			headBone.localScale = new Vector2 (0.5f, 0.6f);
		}

		public override void Draw (IDrawDevice device)
		{
			/*
			Color32 color = new Color32 (0x00, 0xff, 0xff, 0xff);
			device.FillEllipse (color, headBone.Native);
			device.DrawLine (color, neckBone.position, headBone.position);
			device.DrawLine (color, root.position, bodyBone.position);
			device.DrawLine (color, bodyBone.position, neckBone.position);
			device.DrawLine (color, legLBone.position, legLBone.right + legLBone.position);
			device.DrawLine (color, legRBone.position, legRBone.right + legRBone.position);
			device.DrawLine (color, handLBone.position, handLBone.right + handLBone.position);
			device.DrawLine (color, handRBone.position, handRBone.right + handRBone.position);
			device.DrawLine (color, kneeLBone.position, kneeLBone.right + kneeLBone.position);
			device.DrawLine (color, kneeRBone.position, kneeRBone.right + kneeRBone.position);
			device.DrawLine (color, elbowLBone.position, elbowLBone.right + elbowLBone.position);
			device.DrawLine (color, elbowRBone.position, elbowRBone.right + elbowRBone.position);
			*/

			device.DrawImage (headTexture, Matrix3x3.CreateTransformMatrix (new Vector2 (0f, 0f), new Vector2 (2f, 1f / 0.6f), 0f) * headBone.Native);

			device.DrawImage (bodyTexture, Matrix3x3.CreateTransformMatrix (new Vector2 (0f, 0.5f), new Vector2 (1f, 1f), 0f) * bodyBone.Native);

			device.DrawImage (armTexture, Matrix3x3.CreateTransformMatrix (new Vector2 (0.4f, 0f), new Vector2 (1.2f, 1.2f), 90f) * handLBone.Native);
			device.DrawImage (armTexture, Matrix3x3.CreateTransformMatrix (new Vector2 (0.4f, 0f), new Vector2 (1.2f, 1.2f), 90f) * handRBone.Native);

			device.DrawImage (handTexture, Matrix3x3.CreateTransformMatrix (new Vector2 (0.3f, 0f), new Vector2 (1.3f, 1.3f), 90f) * elbowLBone.Native);
			device.DrawImage (handTexture, Matrix3x3.CreateTransformMatrix (new Vector2 (0.3f, 0f), new Vector2 (1.3f, 1.3f), 90f) * elbowRBone.Native);

			device.DrawImage (legTexture, Matrix3x3.CreateTransformMatrix (new Vector2 (0.5f, 0f), new Vector2 (1f, 1f), 90f) * legLBone.Native);
			device.DrawImage (legTexture, Matrix3x3.CreateTransformMatrix (new Vector2 (0.5f, 0f), new Vector2 (1f, 1f), 90f) * legRBone.Native);

			device.DrawImage (kneeTexture, Matrix3x3.CreateTransformMatrix (new Vector2 (0.5f, 0f), new Vector2 (1f, 1f), 90f) * kneeLBone.Native);
			device.DrawImage (kneeTexture, Matrix3x3.CreateTransformMatrix (new Vector2 (0.5f, 0f), new Vector2 (1f, 1f), 90f) * kneeRBone.Native);
		}

		public override string ToString ()
		{
			return $"Root\n{root}\nNeck{neckBone}\nKneeL\n{kneeLBone}\nKneeR\n{kneeRBone}";
		}

		public IEnumerable<HumanBone> GetBones ()
		{
			return skeleton.bones;
		}
	}
}