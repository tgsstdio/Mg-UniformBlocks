using System;
namespace Magnesium.OpenGL
{
	public class GLDescriptorSetBinder : IGLDescriptorSetBinder
	{
		public GLDescriptorSetBinder()
		{
		}

		public void Bind(MgPipelineBindPoint pipelineBindPoint, IMgPipelineLayout layout, uint firstSet, uint descriptorSetCount, IMgDescriptorSet[] pDescriptorSets, uint[] pDynamicOffsets)
		{
			// wrap dynamic offsets in a parameter
		}
	}
}
