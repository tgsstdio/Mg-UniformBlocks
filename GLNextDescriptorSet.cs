namespace Magnesium.OpenGL
{
	public class GLNextDescriptorPoolResourceTicket
	{
		public GLDescriptorBindingGroup ResourceType { get; set; }
		public uint Binding { get; set; }
		public uint DescriptorCount { get; set; }
		public GLPoolResourceTicket Ticket { get; set; }
	}

	public class GLNextDescriptorSet : IMgDescriptorSet
	{
		public IGLDescriptorPool Parent { get; private set; }
		public GLNextDescriptorPoolResourceTicket[] Resources { get; private set; }

		public GLNextDescriptorSet(IGLDescriptorPool parent, GLNextDescriptorPoolResourceTicket[] resources)
		{
			Parent = parent;
			Resources = resources;
		}
	}
}
