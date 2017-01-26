using System.Collections.Generic;

namespace Magnesium.OpenGL
{
	// should be merged with the pipeline layout
	public class GLUniformBindingPointLayout
	{
		public uint NoOfBindingPoints { get; internal set; }
		public SortedDictionary<uint, GLBindingPointOffsetInfo> Offsets { get; internal set; }

		public uint NoOfStorageBuffers { get; private set; }
		public uint NoOfExpectedDynamicOffsets { get; private set;}

		public DynamicStreetPost[] DynamicOffsets { get; private set;}

		public struct DynamicStreetPost
		{
			public GLBufferRangeTarget Target { get; set; }
			public uint DstIndex { get; set;} 
		}

		public GLUniformBindingPointLayout(IGLPipelineLayout layout)
		{
			NoOfBindingPoints = 0U;
			NoOfStorageBuffers = 0U;
			NoOfExpectedDynamicOffsets = 0U;
			BuildPointDirectory(layout);
		}

		void BuildPointDirectory(IGLPipelineLayout layout)
		{
			var count = 0U;
			Offsets = new SortedDictionary<uint, GLBindingPointOffsetInfo>();
			var signPosts = new List<DynamicStreetPost>();
			// build flat slots array for uniforms 
			foreach (var desc in layout.Bindings)
			{
				if (desc.DescriptorType == MgDescriptorType.UNIFORM_BUFFER
					|| desc.DescriptorType == MgDescriptorType.UNIFORM_BUFFER_DYNAMIC)
				{
					count += desc.DescriptorCount;
					Offsets.Add(desc.Binding,
						   new GLBindingPointOffsetInfo
						   {
							   Binding = desc.Binding,
							   First = 0U,
							   Last = desc.DescriptorCount - 1
						   });

					if (desc.DescriptorType == MgDescriptorType.UNIFORM_BUFFER_DYNAMIC)
					{
						signPosts.Add(new DynamicStreetPost
						{
							Target = GLBufferRangeTarget.UNIFORM_BUFFER,
							DstIndex = NoOfExpectedDynamicOffsets,
						});
						NoOfExpectedDynamicOffsets += desc.DescriptorCount;
					}
				}
				else if (desc.DescriptorType == MgDescriptorType.STORAGE_BUFFER
						 || desc.DescriptorType == MgDescriptorType.STORAGE_BUFFER_DYNAMIC)
				{
					NoOfStorageBuffers += desc.DescriptorCount;

					if (desc.DescriptorType == MgDescriptorType.STORAGE_BUFFER_DYNAMIC)
					{
						signPosts.Add(new DynamicStreetPost
						{
							Target = GLBufferRangeTarget.STORAGE_BUFFER,
							DstIndex = desc.Binding,
						});
						NoOfExpectedDynamicOffsets += desc.DescriptorCount;
					}
				}

			}

			var startingOffset = 0U;
			foreach (var g in Offsets.Values)
			{
				g.First += startingOffset;
				g.Last += startingOffset;
				startingOffset += g.Last + 1;
			}

			NoOfBindingPoints = count;

			DynamicOffsets = signPosts.ToArray();
		}
	}
}
