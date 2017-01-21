namespace Magnesium.OpenGL
{
	public class GLDescriptorBinding
	{
		public GLDescriptorBinding(uint location, GLImageDescriptor[] images)
			: this(location)
		{
			Group = GLDescriptorBindingGroup.Image;
			Images = images;
		}

		public GLDescriptorBinding(uint location, GLBufferDescriptor[] buffers)
			: this(location)
		{
			Group = GLDescriptorBindingGroup.Buffer;
			Buffers = buffers;
		}

		private GLDescriptorBinding(uint location)
		{
			Location = location;
		}

		public void Destroy()
		{
			if (Group == GLDescriptorBindingGroup.Image)
			{
				for (var i = 0; i < Images.Length; i += 1)
				{
					if (Images[i] != null)
					{
						Images[i].Destroy();
						Images[i] = null;
					}
				}
			}
			else
			{
				for (var i = 0; i < Buffers.Length; i += 1)
				{
					if (Buffers[i] != null)
					{
						Buffers[i].Destroy();
						Buffers[i] = null;
					}
				}
			}
		}

		public uint Location { get; private set; }
		public GLDescriptorBindingGroup Group { get; private set; }
		public GLImageDescriptor[] Images { get; private set; }
		public GLBufferDescriptor[] Buffers { get; private set; }
	}
}

