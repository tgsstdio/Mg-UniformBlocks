using System;
using Magnesium;
using Magnesium.OpenGL;

namespace UniBlocks.UnitTests
{
	class MockGLDescriptorSetLayout : IGLDescriptorSetLayout
	{
		public GLUniformBinding[] Uniforms
		{
			get;
			set;
		}

		public void DestroyDescriptorSetLayout(IMgDevice device, IMgAllocationCallbacks allocator)
		{
			throw new NotImplementedException();
		}
	}
}