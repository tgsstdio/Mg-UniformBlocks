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

			var sortedResources = new SortedDictionary<uint, GLNextDescriptorPoolResourceTicket>();
			for (var i = 0; i < pAllocateInfo.DescriptorSetCount; i += 1)
			{
				var bSetLayout = (GLDescriptorSetLayout)pAllocateInfo.SetLayouts[i];

				sortedResources.Clear();
				foreach (var uniform in bSetLayout.Uniforms)
				{
					GLPoolResourceInfo ticket;
					switch (uniform.DescriptorType)
					{
						case MgDescriptorType.COMBINED_IMAGE_SAMPLER:
							if (parentPool.CombinedImageSamplers.Allocate(uniform.DescriptorCount, out ticket))
							{
								sortedResources.Add(
									uniform.Binding,
									new GLNextDescriptorPoolResourceTicket
									{

									}
								);
							}
							else
							{
								throw new Exception(); // check specific error
							}
							break;
						case MgDescriptorType.STORAGE_BUFFER:
							if (parentPool.StorageBuffers.Allocate(uniform.DescriptorCount, out ticket))
							{
								sortedResources.Add(
									uniform.Binding,
									new GLNextDescriptorPoolResourceTicket
									{

									}
								);
							}
							else
							{
								throw new Exception(); // check specific error
							}
							break;
						case MgDescriptorType.UNIFORM_BUFFER
							if (parentPool.UniformBuffers.Allocate(uniform.DescriptorCount, out ticket))
							{
								sortedResources.Add(
									uniform.Binding,
									new GLNextDescriptorPoolResourceTicket
									{

									}
								);
							}
							else
							{
								throw new Exception(); // check specific error
							}
							break;
					}
				}

				var resources = new GLNextDescriptorPoolResourceTicket[sortedResources.Count];
				sortedResources.Values.CopyTo(resources, 0);
				pDescriptorSets[i] = new GLNextDescriptorSet(parentPool, resources);
			}

			return Result.SUCCESS;
		}
	}
}
