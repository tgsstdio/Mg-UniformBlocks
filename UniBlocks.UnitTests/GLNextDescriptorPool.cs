using System;
namespace Magnesium.OpenGL
{

	// public Result CreateDescriptorPool(MgDescriptorPoolCreateInfo pCreateInfo, IMgAllocationCallbacks allocator, out IMgDescriptorPool pDescriptorPool) 
	public class GLNextDescriptorPool : IGLDescriptorPool
	{
		public uint MaxSets { get; set; }

		public IGLDescriptorPoolResource<GLImageDescriptor> CombinedImageSamplers { get; private set;}
		public IGLDescriptorPoolResource<GLBufferDescriptor> StorageBuffers { get; private set; }
		public IGLDescriptorPoolResource<GLBufferDescriptor> UniformBuffers { get; private set;}

		public GLNextDescriptorPool(MgDescriptorPoolCreateInfo createInfo, IGLImageDescriptorEntrypoint entrypoint)
		{
			MaxSets = createInfo.MaxSets;

			var noOfUniformBlocks = 0U;
			uint noOfStorageBuffers = 0U;
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
						noOfStorageBuffers += pool.DescriptorCount;
						break;
					case MgDescriptorType.COMBINED_IMAGE_SAMPLER:
						noOfCombinedImageSamplers += pool.DescriptorCount;
						break;
				}
			}

			SetupCombinedImageSamplers(entrypoint, noOfCombinedImageSamplers);
			SetupUniformBlocks(noOfUniformBlocks);
			SetupStorageBuffers(noOfStorageBuffers);
		}

		void SetupUniformBlocks(uint noOfUniformBlocks)
		{
			var blocks = new GLBufferDescriptor[noOfUniformBlocks];
			for (var i = 0; i < noOfUniformBlocks; i += 1)
			{
				blocks[i] = new GLBufferDescriptor();
			}

			UniformBuffers = new GLPoolResource<GLBufferDescriptor>(
				noOfUniformBlocks,
				blocks);
		}

		void SetupStorageBuffers(uint noOfStorageBuffers)
		{
			var buffers = new GLBufferDescriptor[noOfStorageBuffers];
			for (var i = 0; i < noOfStorageBuffers; i += 1)
			{
				buffers[i] = new GLBufferDescriptor();
			}

			StorageBuffers = new GLPoolResource<GLBufferDescriptor>(
				noOfStorageBuffers,
				buffers);
		}

		void SetupCombinedImageSamplers(IGLImageDescriptorEntrypoint entrypoint, uint noOfCombinedImageSamplers)
		{
			var cis = new GLImageDescriptor[noOfCombinedImageSamplers];
			for (var i = 0; i < noOfCombinedImageSamplers; i += 1)
			{
				cis[i] = new GLImageDescriptor(entrypoint);
			}
			CombinedImageSamplers = new GLPoolResource<GLImageDescriptor>(
				noOfCombinedImageSamplers,
				cis);
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
