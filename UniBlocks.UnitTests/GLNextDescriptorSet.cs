using System;
namespace Magnesium.OpenGL
{
	public class GLNextDescriptorPoolResourceTicket
	{
		public GLDescriptorBindingGroup ResourceType { get; set; }
		public uint Binding { get; set; } 
		public uint DescriptorCount { get; set; }
		public GLPoolResourceInfo Ticket { get; set; }
	}

	public class GLNextDescriptorSet : IMgDescriptorSet
	{
		private IGLDescriptorPool mParent;
		private GLNextDescriptorPoolResourceTicket[] mResources;

		public GLNextDescriptorSet(GLNextDescriptorPoolResourceTicket[] resources)
		{
			mResources = resources;
		}
	}
}
