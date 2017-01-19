using System;
using Magnesium;
using Magnesium.OpenGL;

namespace UniBlocks.UnitTests
{
	class MockGLPipelineLayout : IGLPipelineLayout
	{
		public GLUniformBinding[] Bindings
		{
			get;
			set;
		}

		public void DestroyPipelineLayout(IMgDevice device, IMgAllocationCallbacks allocator)
		{
			throw new NotImplementedException();
		}
	}
}