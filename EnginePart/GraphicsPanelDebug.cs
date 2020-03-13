namespace WinFormsGraphics
{
	public partial class GraphicsPanel
	{
		private static string debug = string.Empty;

		public static class GraphicsPanelDebug
		{
			public static void Log (string arg)
			{
				debug = arg + '\n' + debug;

				if (debug.Length > 1024)
				{
					debug = debug.Remove (1024);
				}
			}
		}
	}
}
