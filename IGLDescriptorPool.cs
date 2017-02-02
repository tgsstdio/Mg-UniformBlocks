using System.Collections.Generic;

namespace Magnesium.OpenGL
{
	public interface IGLDescriptorPool : IMgDescriptorPool
	{
		uint MaxSets { get; }
		IDictionary<uint, IGLDescriptorSet> AllocatedSets { get; }

		IGLDescriptorPoolResource<GLImageDescriptor> CombinedImageSamplers { get; }
		IGLDescriptorPoolResource<GLBufferDescriptor> UniformBuffers { get; }
		IGLDescriptorPoolResource<GLBufferDescriptor> StorageBuffers { get; }

		void ResetResource(GLDescriptorPoolResourceInfo resource);

		bool TryTake(out IGLDescriptorSet result);
	}
}