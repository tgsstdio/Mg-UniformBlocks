namespace Magnesium.OpenGL
{
	public interface IGLDescriptorPoolResource<T>
	{
		T[] Items { get; }
		uint Count { get; }
		bool Allocate(uint request, out GLPoolResourceInfo ticket);
		bool Free(GLPoolResourceInfo ticket);
	}
}