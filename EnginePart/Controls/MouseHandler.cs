using System;

namespace EnginePart
{
	public static partial class MouseControlEvents
	{
		public abstract class MouseHandler : IMouseHandler, IDisposable
		{
			public MouseHandler ()
			{
				mouseHandlers.Add (this);
			}

			public virtual int layer => 0;

			public bool isSelected { get; private set; }
			public bool isHovered { get; private set; }

			private Vector2 dragRaw;

			void IMouseHandler.OnMouseDown (Vector2 point)
			{
				isSelected = true;
				dragRaw = point;
				OnMouseDown (point);
			}
			protected virtual void OnMouseDown (Vector2 point) { }


			void IMouseHandler.OnMouseUp (Vector2 point)
			{
				if (isSelected)
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

			public void Dispose ()
			{
				mouseHandlers.Remove (this);
				OnDispose ();
			}

			protected virtual void OnDispose () { }

			void IMouseHandler.OnMouseClick (Vector2 point)
			{
				OnMouseClick (point);
			}
			void IMouseHandler.OnMouseMove (Vector2 point)
			{
				if (isSelected) OnDrag (point, point - lastMousePosition, dragRaw);
			}

			public virtual void OnContextClick (Vector2 point) { }

			void IMouseHandler.OnMouseEnter (Vector2 point)
			{
				isHovered = true;
				OnMouseEnter (point);
			}

			void IMouseHandler.OnMouseExit (Vector2 point)
			{
				isHovered = false;
				OnMouseExit (point);
			}
		}
	}
}