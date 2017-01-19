using System.Collections.Generic;

namespace Magnesium.OpenGL
{
	public class GLUniformBindingPointLayout
	{
		public uint NoOfBindingPoints { get; internal set; }
		public SortedDictionary<uint, GLBindingPointOffsetInfo> Offsets { get; internal set; }

		public GLUniformBindingPointLayout(IGLPipelineLayout layout)
		{
			BuildPointDirectory(layout);
		}

		void BuildPointDirectory(IGLPipelineLayout layout)
		{
			var count = 0U;
			Offsets = new SortedDictionary<uint, GLBindingPointOffsetInfo>();
			// build flat slots array for uniforms 
			foreach (var desc in layout.Bindings)
			{
				if (desc.DescriptorType == Magnesium.MgDescriptorType.UNIFORM_BUFFER
					|| desc.DescriptorType == Magnesium.MgDescriptorType.UNIFORM_BUFFER_DYNAMIC)
				{
					count += desc.DescriptorCount;
					Offsets.Add(desc.Binding,
						   new GLBindingPointOffsetInfo
						   {
							   Binding = desc.Binding,
							   First = 0U,
							   Last = desc.DescriptorCount - 1,
						   });
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
		}
	}
}
