namespace Magnesium.OpenGL
{
	public interface IGLDescriptorPoolResource<T>
	{
		T[] Items { get; }
		void Reset();
		bool Allocate(uint request, out GLPoolResourceInfo range);
	}
}