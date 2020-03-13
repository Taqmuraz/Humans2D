using System.Drawing;
using System.Collections.Generic;

namespace EnginePart
{
	public class TextureDatabase
	{
		private static List<Image> textures = new List<Image> ();

		public static int LoadTexture (string resourcePath)
		{
			return LoadTexture (Resources.LoadImage(resourcePath));
		}

		public static int LoadTexture (Image image)
		{
			textures.Add (image);
			return textures.IndexOf (image);
		}

		protected static Image GetTexture (int index)
		{
			return textures[index];
		}
	}
}