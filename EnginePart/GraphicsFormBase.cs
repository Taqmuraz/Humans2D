using System;
using System.Drawing;
using System.Windows.Forms;
using EnginePart;

namespace WinFormsGraphics
{
	public class GraphicsFormBase : Form
	{
		public GraphicsFormBase ()
		{
			var drawPanel = CreatePanel ();
			drawPanel.Parent = this;
			WindowState = FormWindowState.Maximized;
		}

		protected virtual GraphicsPanel CreatePanel ()
		{
			return new GraphicsPanel ();
		}
	}
}
