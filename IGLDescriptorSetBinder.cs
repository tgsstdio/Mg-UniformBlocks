﻿namespace Magnesium.OpenGL
{
	public interface IGLDescriptorSetBinder
	{
		void Bind(MgPipelineBindPoint pipelineBindPoint, IMgPipelineLayout layout, uint firstSet, uint descriptorSetCount, IMgDescriptorSet[] pDescriptorSets, uint[] pDynamicOffsets);
	}
}