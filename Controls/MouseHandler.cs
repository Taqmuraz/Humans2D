using System;

namespace EnginePart
{
	public static partial class MouseControlEvents
	{
		public abstract class MouseHandler : IMouseHandler, IScreenTrigger, IDisposable
		{
			public MouseHandler ()
			{
				mouseHandlers.Add (this);
			}

			protected bool isSelected { get; private set; }
			protected bool isHovered { get; private set; }
			private Vector2 dragRaw;

			void IMouseHandler.OnMouseDown (Vector2 point)
			{
				if (Contains (point))
				{
					isSelected = true;
					dragRaw = point;
					OnMouseDown (point);
				}
			}
			protected virtual void OnMouseDown (Vector2 point) { }


			void IMouseHandler.OnMouseUp (Vector2 point)
			{
				if (isSelected || Contains(point))
				{
					isSelected = false;
					OnMouseUp (point);
				}
			}
			protected virtual void OnMouseUp (Vector2 point) { }

			protected virtual void OnMouseClick (Vector2 point) {}

			protected virtual void OnMouseEnter (Vector2 point) {}
			protected virtual void OnMouseExit (Vector2 point) {}
			protected virtual void OnDrag (Vector2 point, Vector2 delta, Vector2 raw) {}
			public abstract bool Contains (Vector2 point);

			private void OnEnter (Vector2 point)
			{
				isHovered = true;
				OnMouseEnter (point);
			}
			private void OnExit (Vector2 point)
			{
				isHovered = false;
				OnMouseExit (point);
			}

			void IDisposable.Dispose ()
			{
				mouseHandlers.Remove (this);
				OnDispose ();
			}

			protected virtual void OnDispose () { }

			void IMouseHandler.OnMouseClick (Vector2 point)
			{
				if (Contains(point)) OnMouseClick (point);
			}
			void IMouseHandler.OnMouseMove (Vector2 point)
			{
				if (Contains (lastMousePosition))
				{
					if (!Contains (point)) OnExit (point);
				}
				else
				{
					if (Contains (point)) OnEnter (point);
				}
				if (isSelected) OnDrag (point, point - lastMousePosition, dragRaw);
			}
		}
	}
}