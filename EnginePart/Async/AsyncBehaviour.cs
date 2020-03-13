using System.Collections.Generic;
using System;
using System.Timers;

namespace EnginePart
{
	public abstract class AsyncBehaviour
	{
		private List<InstructionKeepWaiting> instructions;

		private class InstructionKeepWaiting
		{
			private class NotWait : IYieldInstruction
			{
				private NotWait ()
				{
				}
				public static readonly NotWait notWait = new NotWait ();

				bool IYieldInstruction.keepWaiting => false;
			}

			public InstructionKeepWaiting (IAsyncInstruction async)
			{
				asyncInstruction = async;
				yieldInstruction = NotWait.notWait;
			}

			private IAsyncInstruction asyncInstruction;
			private IYieldInstruction yieldInstruction;

			public bool MoveNext ()
			{
				bool move = asyncInstruction.MoveNext ();

				yieldInstruction = asyncInstruction.Current == null ? NotWait.notWait : asyncInstruction.Current;

				return move;
			}
			public bool IsWaiting ()
			{
				return yieldInstruction.keepWaiting;
			}
		}

		public AsyncBehaviour (int timeInterval)
		{
			instructions = new List<InstructionKeepWaiting> ();

			Timer timer = new Timer ();
			timer.Interval = timeInterval;
			timer.Elapsed += (s, e) => UpdateInstructions ();
			timer.Enabled = true;
		}

		public void AddInstruction (IAsyncInstruction instruction)
		{
			instructions.Add (new InstructionKeepWaiting(instruction));
		}

		private void UpdateInstructions ()
		{
			foreach (var instruction in instructions)
			{
				if (instruction.IsWaiting()) continue;
				if (!instruction.MoveNext ()) instructions.Remove (instruction);
			}
		}
	}
}