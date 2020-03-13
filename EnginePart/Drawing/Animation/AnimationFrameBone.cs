namespace EnginePart
{
	public struct AnimationFrameBone
	{
		private string boneName;
		private Matrix3x3 transformation;

		public AnimationFrameBone (string boneName, Matrix3x3 transformation)
		{
			this.boneName = boneName;
			this.transformation = transformation;
		}

		public string GetBoneName ()
		{
			return boneName;
		}
		public Matrix3x3 GetMatrix ()
		{
			return transformation;
		}
	}
}