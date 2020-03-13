using System.Collections.Generic;

namespace EnginePart
{
	public class Animation
	{
		private AnimationFrame[] frames;
		private float timeLength;

		public Animation (AnimationFrame[] frames, float timeLength)
		{
			this.frames = frames;
			this.timeLength = timeLength;
		}

		private class NextFrameWait : IYieldInstruction
		{
			public float neededTime { private get; set; }

			bool IYieldInstruction.keepWaiting => Time.time >= neededTime;
		}

		private IEnumerator<IYieldInstruction> Play (HumanRenderer humanRenderer)
		{
			NextFrameWait wait = new NextFrameWait ();
			float startTime = Time.time;

			for (int i = 0; i < frames.Length; i++)
			{
				frames[i].ApplyToSkeleton (humanRenderer.skeleton);

				wait.neededTime = startTime + frames[i].timeStartPercent * timeLength;
				yield return wait;
			}
		}
	}
}