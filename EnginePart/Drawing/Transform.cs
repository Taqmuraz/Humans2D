using System.Collections.Generic;
using System.Linq;

namespace EnginePart
{
	public sealed class Transform
	{
		private Matrix3x3 localMatrix;

		private List<Transform> childs;

		public Transform (Matrix3x3 matrix)
		{
			localMatrix = matrix;
			childs = new List<Transform> ();
		}
		public Transform () : this (Matrix3x3.identity)
		{
		}

		private Transform m_parent;
		public Transform parent
		{
			get => m_parent;
			set
			{
				if (m_parent == value) return;
				if (m_parent == this) throw new System.ArgumentException("Can't set parent as itself");

				if (value.IsChildOf(this)) value.parent = null;
				if (m_parent != null) m_parent.childs.Remove (this);
				value.childs.Add (this);
				m_parent = value;
			}
		}

		public bool IsChildOf (Transform transform)
		{
			if (parent == transform) return true;
			else if (parent != null) return parent.IsChildOf (transform);
			else return false;
		}
		public IEnumerable<Transform> GetChilds ()
		{
			return childs;
		}

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
				localPosition = (Vector2)(parentMatrix.GetInversed () * new Vector3 (value.x, value.y, 1f));
			}
		}
		public float rotation
		{
			get => ((Vector2)localMatrix[0]).GetDirectionAngle ();
			set => localRotation = ((Vector2)(parentMatrix.GetInversed() * new Vector3(value.Cos (), value.Sin(), 0f))).GetDirectionAngle ();
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
			set
			{
				localScale = (Vector2)(parentMatrix.GetScale().GetInversed () * new Vector3 (value.x, value.y, 0f));
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

		public void SetLocalMatrix (Matrix3x3 matrix)
		{
			localMatrix = matrix;
		}

		public Matrix3x3 Native => globalMatrix;

		public static Transform CreateDeepCopy (Transform transform)
		{
			var copy = new Transform (transform.Native);
			foreach (var child in transform.GetChilds())
			{
				var child_copy = CreateDeepCopy (child);
				child_copy.parent = copy;
			}
			return copy;
		}
	}
}