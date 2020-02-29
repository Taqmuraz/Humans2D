namespace EnginePart
{
	public sealed class Transform
	{
		private Matrix3x3 localMatrix;

		public Transform (Matrix3x3 matrix)
		{
			localMatrix = matrix;
		}

		public Transform parent { get; set; }

		private Matrix3x3 parentMatrix
		{
			get
			{
				if (parent == null) return Matrix3x3.identity;
				else return parent.globalMatrix;
			}
		}

		private Matrix3x3 globalMatrix
		{
			get
			{
				return localMatrix * parentMatrix;
			}
		}

		public Vector2 localPosition
		{
			get => (Vector2)localMatrix[2];
			set => localMatrix[2] = new Vector3 (value.x, value.y, 1f);
		}
		public float localRotation
		{
			get => ((Vector2)localMatrix[0]).GetDirectionAngle ();
			set => localMatrix = Matrix3x3.CreateTransformMatrix (localPosition, localScale, value);
		}

		public Vector2 position
		{
			get => (Vector2)globalMatrix[2];
			set
			{
				Vector2 pos = (Vector2)(parentMatrix * Vector3.forward);
				Vector2 delta = value - pos;

				Debug.Log (parentMatrix * Vector3.right + " ||| " + parentMatrix * (Vector3.right + Vector3.forward));
				localPosition = (Vector2)(parentMatrix.GetInversed () * new Vector3 (delta.x, delta.y, 1f));
			}
		}
		public float rotation
		{
			get => ((Vector2)localMatrix[0]).GetDirectionAngle ();
			set => localRotation = ((Vector2)(parentMatrix.GetInversed() * new Vector3(value.Cos (), value.Sin(), 1f))).GetDirectionAngle ();
		}

		public Vector2 right => (Vector2)globalMatrix[0];

		public Vector2 up => (Vector2)globalMatrix[1];

		public Vector2 localScale 
		{
			get => new Vector2 (localMatrix[0].length, localMatrix[1].length);
			set
			{
				localMatrix[0] = localMatrix[0].normalized * value.x;
				localMatrix[1] = localMatrix[1].normalized * value.y;
			} 
		}
		public Vector2 globalScale
		{
			get
			{
				var gm = globalMatrix;
				return new Vector2 (gm[0].length, gm[1].length);
			}
		}

		public Vector2 MultiplyPoint (Vector2 point)
		{
			return (Vector2)(globalMatrix * new Vector3(point.x, point.y, 1f));
		}

		public Vector2 MultiplyDirection (Vector2 direction)
		{
			return (Vector2)(globalMatrix * new Vector3 (direction.x, direction.y, 0f));
		}

		public Vector2 InversePoint (Vector2 point)
		{
			return (Vector2)(globalMatrix.GetInversed() * new Vector3 (point.x, point.y, 1f));
		}

		public Vector2 InverseDirection (Vector2 direction)
		{
			return (Vector2)(globalMatrix.GetInversed () * new Vector3 (direction.x, direction.y, 0f));
		}

		public Matrix3x3 GetInversed ()
		{
			return globalMatrix.GetInversed ();
		}

		public Matrix3x3 Multiply (Matrix3x3 matrix)
		{
			return globalMatrix * matrix;
		}
		public override string ToString ()
		{
			return $"position = {position}\nrotation = {rotation}\nlocalPosition = {localPosition}\nlocalRotation = {localRotation}\n";
		}

		public Matrix3x3 Native => globalMatrix;
	}
}