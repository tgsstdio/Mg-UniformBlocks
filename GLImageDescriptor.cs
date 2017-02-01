using System;

namespace Magnesium.OpenGL
{
	public class GLImageDescriptor : IGLDescriptorSetResource
	{
		public IGLImageDescriptorEntrypoint Entrypoint { get; internal set; }

		public GLImageDescriptor(IGLImageDescriptorEntrypoint entrypoint)
		{
			Entrypoint = entrypoint;
		}

		public ulong? SamplerHandle { get; set; }

		public void Replace(ulong handle)
		{
			Destroy();
			SamplerHandle = handle;
		}

		public void Destroy()
		{
			if (SamplerHandle.HasValue)
			{
				Entrypoint.ReleaseHandle(SamplerHandle.Value);
				SamplerHandle = null;
			}
		}

		public void Reset()
		{
			Destroy();
		}
	}	
}


