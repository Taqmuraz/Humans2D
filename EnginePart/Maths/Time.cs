using System;

namespace EnginePart
{
	public static class Time
	{
		static Time ()
		{
			startTime = DateTime.Now;
		}

		private static DateTime startTime;

		private static float totalTime
		{
			get => (float)(DateTime.Now - startTime).TotalSeconds;
		}

		public static float time => totalTime;
	}
}