namespace Magnesium.OpenGL
{
	public struct GLUniformBinding
	{
        public uint DescriptorCount { get; set; }
        public MgDescriptorType DescriptorType {
			get;
			set;
		}

		public uint Binding { get; set; }
        public MgShaderStageFlagBits StageFlags { get; set; }
    }
}

