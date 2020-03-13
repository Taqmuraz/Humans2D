using WinFormsGraphics;

namespace AnimationCreateForm
{
	public partial class AnimationDrawForm : GraphicsFormBase
	{
		public AnimationDrawForm ()
		{
			InitializeComponent ();
			Text = "Animation draw window";
		}
		protected override GraphicsPanel CreatePanel ()
		{
			return new AnimationDrawPanel ();
		}
	}
}
