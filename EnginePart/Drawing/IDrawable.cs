namespace EnginePart
{
	public interface IDrawable
	{
		void Draw (IDrawDevice device);
		DrawLayer layer { get; }
	}
}