using System.Collections.Generic;

namespace EnginePart
{
	public class AnimationFrame
	{
		private Dictionary<string, Matrix3x3> boneMatrices;
		public float timeStartPercent { get; private set; }

		public AnimationFrame (AnimationFrameBone[] bones, float timeStartPercent)
		{
			this.timeStartPercent = timeStartPercent;

			boneMatrices = new Dictionary<string, Matrix3x3> (bones.Length);
			foreach (var bone in bones) boneMatrices.Add (bone.GetBoneName (), bone.GetMatrix ());
		}

		public void ApplyToSkeleton (HumanRenderer.HumanSkeleton skeleton)
		{
			foreach (var bone in skeleton.bones)
			{
				bone.transform.SetLocalMatrix (boneMatrices[bone.name]);
			}
		}
	}
}