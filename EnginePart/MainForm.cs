using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EnginePart;

namespace WinFormsGraphics
{
	public partial class MainForm : GraphicsFormBase
	{
		HumanRenderer human;

		public MainForm ()
		{
			InitializeComponent ();

			human = new HumanRenderer ();
			human.root.localScale = new Vector2 (55, 55);
			human.root.position = new Vector2 (Width >> 1, Height >> 1);

			CreateHumanControl ();
		}

		private void CreateHumanControl ()
		{
			foreach (var bone in human.GetBones ())
			{
				if (bone.transform.parent == null) new TransformPositionControl (bone.transform);
				new TransformRotationControl (bone.transform);
			}
		}
	}
}
