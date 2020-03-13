using System;
using System.Linq;

namespace EnginePart
{
	public static class Mathf
	{
		public const float PI = (float)Math.PI;
		public const float Deg2Rad = PI / 180f;
		public const float Rad2Deg = 180f / PI;

		public static float Sin (this float a)
		{
			return (float)Math.Sin (a * Deg2Rad);
		}
		public static float Cos (this float a)
		{
			return (float)Math.Cos (a * Deg2Rad);
		}
		public static float Sqrt (this float a)
		{
			return (float)Math.Sqrt (a);
		}
		public static float ASin (this float a)
		{
			return (float)Math.Asin (a) * Rad2Deg;
		}
		public static float ACos (this float a)
		{
			return (float)Math.Acos (a) * Rad2Deg;
		}

		public static float ATan (this float a)
		{
			return (float)Math.Atan (a) * Rad2Deg;
		}

		public static float Sign (this float a)
		{
			return Math.Sign (a);
		}

		public static float Tan(this float v)
		{
			return Sin(v) / Cos(v);
		}

		public static void Swap<T>(this object obj, ref T a, ref T b)
		{
			T temp = a;
			a = b;
			b = temp;
		}

		public static float Min (float a, float b)
		{
			if (a > b) return b;
			return a;
		}
		public static float Min(float a, float b, float c)
		{
			if (a <= b && a <= c) return a;
			if (b <= c && b <= a) return b;
			return c;
		}
		public static float Max(float a, float b)
		{
			if (a < b) return b;
			return a;
		}
		public static float Max(float a, float b, float c)
		{
			if (a >= b && a >= c) return a;
			if (b >= c && b >= a) return b;
			return c;
		}
		public static float Abs (this float a)
		{
			if (a < 0) return -a;
			return a;
		}
		public static float Determinant (float a1, float b1, float a2, float b2)
		{
			return a1 * b2 - a2 * b1;
		}
		public static float Determinant (Vector2 axis_a, Vector2 axis_b)
		{
			return Determinant(axis_a.x, axis_b.x, axis_a.y, axis_b.y);
		}
		public static T Clamp<T> (this T a, T min, T max) where T : IComparable<T>
		{
			if (a.CompareTo (max) > 0) a = max;
			if (a.CompareTo (min) < 0) a = min;
			return a;
		}
		public static float GetDirectionAngle (this Vector2 dir)
		{
			float xSign = dir.x >= 0f ? 0f : 180f;
			return (dir.y / dir.x).ATan () + xSign;
		}
		public static Matrix3x3 CreateTransformMatrix (Vector2 position, float rotation)
		{
			return Matrix3x3.CreateTransformMatrix (position, Vector2.one, rotation);
		}

		public static float Pow (this float a, float b)
		{
			return (float)Math.Pow (a, b);
		}

		public static bool ContainsBox (this Vector2 point, Vector2 min, Vector2 max)
		{
			return (point.x <= max.x && point.y <= max.y && point.x >= min.x && point.y >= min.y);
		}
	}
}

