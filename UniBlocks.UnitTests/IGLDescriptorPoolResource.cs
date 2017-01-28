namespace UniBlocks.UnitTests
{
	interface IGLDescriptorPoolResource<T>
	{
		T[] Items { get; }
		void Reset();
		bool Allocate(uint request, out GLPoolResourceInfo range);
	}
}