﻿namespace EnginePart
{
	public interface IDrawDevice
	{
		void DrawLine (Color32 color, Vector2 a, Vector2 b);
		void DrawText (Color32 color, Vector2 a, string text);
		void DrawLines (Color32 color, params Vector2[] args);
		void DrawEllipse (Color32 color, Vector2 center, Vector2 size);
		void FillEllipse (Color32 color, Vector2 center, Vector2 size);
	}
}