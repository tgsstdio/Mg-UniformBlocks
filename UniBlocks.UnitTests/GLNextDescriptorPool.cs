using System;
namespace Magnesium.OpenGL
{

	// public Result CreateDescriptorPool(MgDescriptorPoolCreateInfo pCreateInfo, IMgAllocationCallbacks allocator, out IMgDescriptorPool pDescriptorPool) 
	public class GLNextDescriptorPool : IGLDescriptorPool
	{
		public IGLDescriptorPoolResource<GLImageDescriptor> CombinedImageSamplers { get; private set;}
		public IGLDescriptorPoolResource<GLBufferDescriptor> StorageBuffers { get; private set; }
		public IGLDescriptorPoolResource<GLBufferDescriptor> UniformBuffers { get; private set;}

		public uint MaxSets { get; set; }

		public GLNextDescriptorPool(MgDescriptorPoolCreateInfo createInfo)
		{
			MaxSets = createInfo.MaxSets;

			var noOfUniformBlocks = 0U;
			uint noOfStorageBlocks = 0U;
			uint noOfCombinedImageSamplers = 0U;

			foreach (var pool in createInfo.PoolSizes)
			{
				switch (pool.Type)
				{
					case MgDescriptorType.UNIFORM_BUFFER:
					case MgDescriptorType.UNIFORM_BUFFER_DYNAMIC:
						noOfUniformBlocks += pool.DescriptorCount;
						break;
					case MgDescriptorType.STORAGE_BUFFER:
					case MgDescriptorType.STORAGE_BUFFER_DYNAMIC:
						noOfStorageBlocks += pool.DescriptorCount;
						break;
					case MgDescriptorType.COMBINED_IMAGE_SAMPLER:
						noOfCombinedImageSamplers += pool.DescriptorCount;
						break;
				}
			}

			CombinedImageSamplers = new GLPoolResource<GLImageDescriptor>(noOfCombinedImageSamplers);
			UniformBuffers = new GLPoolResource<GLBufferDescriptor>(noOfUniformBlocks);
			StorageBuffers = new GLPoolResource<GLBufferDescriptor>(noOfStorageBlocks);
		}

		public void DestroyDescriptorPool(IMgDevice device, IMgAllocationCallbacks allocator)
		{
			throw new NotImplementedException();
		}

		public Result ResetDescriptorPool(IMgDevice device, uint flags)
		{
			throw new NotImplementedException();
		}
	}
}
