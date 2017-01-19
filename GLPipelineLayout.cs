using UniBlocks;

namespace Magnesium.OpenGL
{
	public class GLPipelineLayout : IGLPipelineLayout
	{
		public GLUniformBinding[] Bindings {
			get;
			private set;
		}

		public GLPipelineLayout (MgPipelineLayoutCreateInfo pCreateInfo)
		{
			if (pCreateInfo.SetLayouts.Length == 1)
			{
				var layout = (IGLDescriptorSetLayout) pCreateInfo.SetLayouts [0];
				Bindings = layout.Uniforms;
			} 
			else
			{
				Bindings = new GLUniformBinding[0];
			}
		}

		#region IMgPipelineLayout implementation
		private bool mIsDisposed = false;
		public void DestroyPipelineLayout (IMgDevice device, IMgAllocationCallbacks allocator)
		{
			if (mIsDisposed)
				return;

			mIsDisposed = true;
		}

		#endregion
	}
}

