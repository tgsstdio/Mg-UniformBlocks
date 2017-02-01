namespace Magnesium.OpenGL
{
	public interface IGLDescriptorPool : IMgDescriptorPool
	{
		uint MaxSets { get; }
		IGLDescriptorPoolResource<GLImageDescriptor> CombinedImageSamplers { get; }
		IGLDescriptorPoolResource<GLBufferDescriptor> UniformBuffers { get; }
		IGLDescriptorPoolResource<GLBufferDescriptor> StorageBuffers { get; }
	}
}