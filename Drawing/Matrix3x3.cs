namespace EnginePart
{
	public struct Matrix3x3
	{
		Vector3 row_0;
		Vector3 row_1;
		Vector3 row_2;

		public static readonly Matrix3x3 identity = new Matrix3x3 (Vector3.right, Vector3.up, Vector3.forward);

		public Matrix3x3 (Vector3 row_0, Vector3 row_1, Vector3 row_2)
		{
			this.row_0 = row_0;
			this.row_1 = row_1;
			this.row_2 = row_2;
		}

		public Vector3 this[int index]
		{
			get
			{
				switch (index)
				{
					case 0: return row_0;
					case 1: return row_1;
					case 2: return row_2;
					default:throw new System.ArgumentException ("Row index out of matrix range");
				}
			}
			set
			{
				switch (index)
				{
					case 0: row_0 = value; break;
					case 1: row_1 = value; break;
					case 2: row_2 = value; break;
					default: throw new System.ArgumentException ("Row index out of matrix range");
				}
			}
		}
		public float this[int index, int element]
		{
			get
			{
				return this[index][element];
			}
		}

		public static Matrix3x3 operator * (Matrix3x3 a, float b)
		{
			a.row_0 *= b;
			a.row_1 *= b;
			a.row_2 *= b;
			return a;
		}

		public static Vector3 operator * (Matrix3x3 m, Vector3 v)
		{
			Matrix3x3 lines = m.GetTransponed ();
			float x = Vector3.Dot (v, lines.row_0);
			float y = Vector3.Dot (v, lines.row_1);
			float z = Vector3.Dot (v, lines.row_2);
			return new Vector3 (x, y, z);
		}

		public static Matrix3x3 operator * (Matrix3x3 a, Matrix3x3 b)
		{
			Vector3 c0 = b * a.row_0;
			Vector3 c1 = b * a.row_1;
			Vector3 c2 = b * a.row_2;
			return new Matrix3x3 (c0, c1, c2);
		}

		public static Matrix3x3 CreateTransformMatrix (Vector2 position, Vector2 scale, float rotation)
		{
			Matrix3x3 matrix = CreateRotationMatrix (rotation);
			matrix.row_2 = new Vector3 (position.x, position.y, 1f);
			matrix.row_0 *= scale.x;
			matrix.row_1 *= scale.y;
			return matrix;
		}
		public static Matrix3x3 CreateRotationMatrix (float rotation)
		{
			float sin = rotation.Sin ();
			float cos = rotation.Cos ();
			Vector3 right = new Vector3 (cos, sin, 0f);
			Vector3 up = new Vector3 (-sin, cos, 0f);
			return new Matrix3x3 (right, up, Vector3.forward);
		}
		public static Matrix3x3 CreateTranslationMatrix (Vector2 vector)
		{
			var m = identity;
			m.row_2 = new Vector3 (vector.x, vector.y, 1f);
			return m;
		}

		public float GetDeterminant ()
		{
			return row_0.x * row_1.y * row_2.z + row_0.z * row_1.x * row_2.y + row_0.y * row_1.z * row_2.x
				- row_0.z * row_1.y * row_2.x - row_0.x * row_1.z * row_2.y - row_0.y * row_1.x * row_2.z;
		}

		public Matrix3x3 GetTransponed ()
		{
			return new Matrix3x3
				(
				new Vector3 (row_0.x, row_1.x, row_2.x),
				new Vector3 (row_0.y, row_1.y, row_2.y),
				new Vector3 (row_0.z, row_1.z, row_2.z)
				);
		}

		public Matrix3x3 GetInversed ()
		{
			return GetTransponed () * (1f / GetDeterminant ());
		}

		public override string ToString ()
		{
			return $"{row_0.x}, {row_1.x}, {row_2.x}\n{row_0.y}, {row_1.y}, {row_2.y}\n{row_0.z}, {row_1.z}, {row_2.z}\nDet = {GetDeterminant()}";
		}
	}
}