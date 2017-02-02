using System;
using System.Collections.Generic;

namespace Magnesium.OpenGL
{
	public class GLNextDescriptorSetAllocator : IGLDescriptorSetAllocator
	{
		public Result AllocateDescriptorSets(MgDescriptorSetAllocateInfo pAllocateInfo, out IMgDescriptorSet[] pDescriptorSets)
		{
			if (pAllocateInfo == null)
				throw new ArgumentNullException(nameof(pAllocateInfo));

			var parentPool = (GLNextDescriptorPool)pAllocateInfo.DescriptorPool;
			pDescriptorSets = new IMgDescriptorSet[pAllocateInfo.DescriptorSetCount];

			var maxNoOfResources = 0U;
			var sortedResources = new List<GLNextDescriptorPoolResourceTicket>();
			for (var i = 0; i < pAllocateInfo.DescriptorSetCount; i += 1)
			{
				var bSetLayout = (GLDescriptorSetLayout)pAllocateInfo.SetLayouts[i];

				sortedResources.Clear();
				foreach (var uniform in bSetLayout.Uniforms)
				{
					maxNoOfResources = Math.Max(maxNoOfResources, uniform.Binding);
					GLPoolResourceTicket ticket;
					switch (uniform.DescriptorType)
					{
						case MgDescriptorType.COMBINED_IMAGE_SAMPLER:
							if (parentPool.CombinedImageSamplers.Allocate(uniform.DescriptorCount, out ticket))
							{
								sortedResources.Add(
									new GLNextDescriptorPoolResourceTicket
									{
									Binding = uniform.Binding,
									DescriptorCount = uniform.DescriptorCount,
									ResourceType = GLDescriptorBindingGroup.Image,
									Ticket = ticket,
									}
								);
							}
							else
							{
								// VK_ERROR_FRAGMENTED_POOL = -12
								return Result.ERROR_OUT_OF_HOST_MEMORY;
							}
							break;
						case MgDescriptorType.STORAGE_BUFFER:
							if (parentPool.StorageBuffers.Allocate(uniform.DescriptorCount, out ticket))
							{
								sortedResources.Add(
									new GLNextDescriptorPoolResourceTicket
									{
									Binding = uniform.Binding,
									DescriptorCount = uniform.DescriptorCount,
									ResourceType = GLDescriptorBindingGroup.Buffer,
									Ticket = ticket,
									}
								);
							}
							else
							{
								// VK_ERROR_FRAGMENTED_POOL = -12
								return Result.ERROR_OUT_OF_HOST_MEMORY;
							}
							break;
						case MgDescriptorType.UNIFORM_BUFFER:
							if (parentPool.UniformBuffers.Allocate(uniform.DescriptorCount, out ticket))
							{
								sortedResources.Add(
									new GLNextDescriptorPoolResourceTicket
									{
									Binding = uniform.Binding,
									DescriptorCount = uniform.DescriptorCount,
									ResourceType = GLDescriptorBindingGroup.Buffer,
									Ticket = ticket,
									}
								);
							}
							else
							{
								// VK_ERROR_FRAGMENTED_POOL = -12
								return Result.ERROR_OUT_OF_HOST_MEMORY;
							}
							break;
					}
				}

				var resources = new GLNextDescriptorPoolResourceTicket[maxNoOfResources];
				foreach (var res in sortedResources)
				{
					resources[res.Binding] = res;
				}
				pDescriptorSets[i] = new GLNextDescriptorSet(parentPool, resources);
			}

			return Result.SUCCESS;
		}
	}
}
