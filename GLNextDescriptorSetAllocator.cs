using System;
using System.Collections.Generic;

namespace Magnesium.OpenGL
{
	public class GLNextDescriptorSetAllocator : IGLDescriptorSetAllocator
	{
		#region AllocateDescriptorSets methods

		public Result AllocateDescriptorSets(MgDescriptorSetAllocateInfo pAllocateInfo, out IMgDescriptorSet[] pDescriptorSets)
		{
			if (pAllocateInfo == null)
				throw new ArgumentNullException(nameof(pAllocateInfo));

			var parentPool = (IGLDescriptorPool)pAllocateInfo.DescriptorPool;
			pDescriptorSets = new IMgDescriptorSet[pAllocateInfo.DescriptorSetCount];

			var maxNoOfResources = 0U;
			var sortedResources = new List<GLDescriptorPoolResourceInfo>();
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
									new GLDescriptorPoolResourceInfo
									{
									Binding = uniform.Binding,
									DescriptorCount = uniform.DescriptorCount,
									ResourceType = GLDescriptorBindingGroup.CombinedImageSampler,
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
									new GLDescriptorPoolResourceInfo
									{
									Binding = uniform.Binding,
									DescriptorCount = uniform.DescriptorCount,
									ResourceType = GLDescriptorBindingGroup.StorageBuffer,
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
									new GLDescriptorPoolResourceInfo
									{
									Binding = uniform.Binding,
									DescriptorCount = uniform.DescriptorCount,
									ResourceType = GLDescriptorBindingGroup.UniformBuffer,
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

				var resources = new GLDescriptorPoolResourceInfo[maxNoOfResources];
				foreach (var res in sortedResources)
				{
					resources[res.Binding] = res;
				}

				IGLDescriptorSet item;
				if (parentPool.TryTake(out item))
				{
					item.Initialise(resources);
					parentPool.AllocatedSets.Add(item.Key, item);
				}
			}

			return Result.SUCCESS;
		}

		#endregion

		#region FreeDescriptorSets methods

		public Result FreeDescriptorSets(IMgDescriptorPool descriptorPool, IMgDescriptorSet[] pDescriptorSets)
		{
			if (descriptorPool == null)
			{
				throw new ArgumentNullException(nameof(descriptorPool));
			}

			if (pDescriptorSets == null)
			{
				throw new ArgumentNullException(nameof(pDescriptorSets));
			}

			var parentPool = (IGLDescriptorPool) descriptorPool;

			foreach (var descSet in pDescriptorSets)
			{
				var bDescSet = (IGLDescriptorSet) descSet;
				if (bDescSet != null && ReferenceEquals(parentPool, bDescSet.Parent))
				{
					if (bDescSet.IsValidDescriptorSet)
					{
						foreach (var resource in bDescSet.Resources)
						{
							parentPool.ResetResource(resource);
						}
						bDescSet.Invalidate();
						parentPool.AllocatedSets.Remove(bDescSet.Key);
					}
				}
			}

			return Result.SUCCESS;
		}

		#endregion
	}
}
