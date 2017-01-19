namespace Magnesium.OpenGL
{
	public interface IGLDeviceEntrypoint
	{
		IGLSamplerEntrypoint Sampler { get; }
		IGLImageDescriptorEntrypoint ImageDescriptor { get; }
    }
}

