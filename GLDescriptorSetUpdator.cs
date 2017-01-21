using System;
namespace Magnesium.OpenGL
{
	public class GLDescriptorSetUpdator : IGLDescriptorSetUpdator
	{
		private readonly IGLImageDescriptorEntrypoint mImage;
		public GLDescriptorSetUpdator(IGLImageDescriptorEntrypoint image)
		{
			mImage = image;
		}

		public void Update(MgWriteDescriptorSet[] pDescriptorWrites, MgCopyDescriptorSet[] pDescriptorCopies)
		{
			if (pDescriptorWrites != null)
			{
				for (var i = 0; i < pDescriptorWrites.Length; i += 1)
				{
					var desc = pDescriptorWrites[i];
					var localSet = (GLDescriptorSet) desc.DstSet;
					if (localSet == null)
					{
						throw new NotSupportedException();
					}

					var offset = (int)desc.DstArrayElement;
					var count = (int)desc.DescriptorCount;

					//var lastIndex = localSet.Bindings.Length - 1;
					//var right = offset + count - 1;
					//if (right > lastIndex)
					//{
					//    // VULKAN WOULD CONTINUE ONTO WRITE ADDITIONAL VALUES TO NEXT BINDING
					//    // ONLY ONE SET OF BINDING USED
					//    throw new IndexOutOfRangeException();
					//}

					switch (desc.DescriptorType)
					{
						//case MgDescriptorType.SAMPLER:
						case MgDescriptorType.COMBINED_IMAGE_SAMPLER:
						case MgDescriptorType.SAMPLED_IMAGE:
							{
								GLDescriptorBinding descriptor;
								if (localSet.TryGetValue(desc.DstBinding, out descriptor))
								{

									// HOPEFULLY DESCRIPTOR SETS ARE GROUPED BY COMMON TYPES
									for (int j = 0; j < count; j += 1)
									{
										MgDescriptorImageInfo info = desc.ImageInfo[j];

										var localSampler = info.Sampler as GLSampler;
										var localView = info.ImageView as GLImageView;

										// Generate bindless texture handle 
										// FIXME : messy as F***

										var texHandle = mImage.CreateHandle(localView.TextureId, localSampler.SamplerId);

										var imageDesc = descriptor.Images[j];
										imageDesc.Replace(texHandle);
									}
								}
							}
							break;
						case MgDescriptorType.STORAGE_BUFFER:
						case MgDescriptorType.STORAGE_BUFFER_DYNAMIC:
						case MgDescriptorType.UNIFORM_BUFFER:
						case MgDescriptorType.UNIFORM_BUFFER_DYNAMIC:
							{
								GLDescriptorBinding descriptor;
								if (localSet.TryGetValue(desc.DstBinding, out descriptor))
								{

									// HOPEFULLY DESCRIPTOR SETS ARE GROUPED BY COMMON TYPES
									for (int j = 0; j < count; j += 1)
									{
										var info = desc.BufferInfo[j];

										var buf = info.Buffer as IGLBuffer;

										var isBufferFlags = 
													MgBufferUsageFlagBits.STORAGE_BUFFER_BIT
													| MgBufferUsageFlagBits.UNIFORM_BUFFER_BIT;

										if (buf != null && ((buf.Usage & isBufferFlags) == isBufferFlags))
										{
											var bufferDesc = descriptor.Buffers[offset + j];
											bufferDesc.BufferId = buf.BufferId;

											if (info.Offset > (ulong)long.MaxValue)
											{
												throw new ArgumentOutOfRangeException(
													nameof(pDescriptorWrites)
													+ "[" + i + "].BufferInfo[" + j + "].Offset is > long.MaxValue");
											}

											// CROSS PLATFORM ISSUE : VK_WHOLE_SIZE == ulong.MaxValue
											if (info.Range == ulong.MaxValue)
											{
												throw new ArgumentOutOfRangeException(
													"Magnesium.OpenGL : Cannot accept " + nameof(pDescriptorWrites)
													+ "[" + i + "].BufferInfo[" + j + "].Range  == ulong.MaxValue (VK_WHOLE_SIZE). Please use actual size of buffer instead.");
											}

											if(info.Range > (ulong)int.MaxValue)
											{
												throw new ArgumentOutOfRangeException(
													nameof(pDescriptorWrites) 
													+ "[" + i + "].BufferInfo[" + j + "].Range is > int.MaxValue");
											}

											bufferDesc.Offset = (long) info.Offset;
											// need to pass in whole 
											bufferDesc.Size = (int) info.Range;
											bufferDesc.IsDynamic =
												(
												   desc.DescriptorType == MgDescriptorType.STORAGE_BUFFER_DYNAMIC
												|| desc.DescriptorType == MgDescriptorType.UNIFORM_BUFFER_DYNAMIC
												);
										}
									}
								}
							}
							break;
						default:
							throw new NotSupportedException("UpdateDescriptorSets");
					}

				}
			}
		}
	}
}
