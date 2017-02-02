namespace Magnesium.OpenGL
{
	public interface IGLDescriptorSetAllocator
	{
		Result AllocateDescriptorSets(MgDescriptorSetAllocateInfo pAllocateInfo, out IMgDescriptorSet[] pDescriptorSets);
		Result FreeDescriptorSets(IMgDescriptorPool descriptorPool, IMgDescriptorSet[] pDescriptorSets);
	}
}