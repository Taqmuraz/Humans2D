using System;
using System.Drawing;

namespace EnginePart
{
	public class Resources
	{
		private static string path
		{
			get
			{
				return Environment.CurrentDirectory;
			}
		}

		public static Image LoadImage (string resourceName)
		{
			return new Bitmap (path + "/Resources/" + resourceName);
		}
	}
}