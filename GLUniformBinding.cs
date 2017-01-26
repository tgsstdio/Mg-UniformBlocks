using System;

namespace Magnesium.OpenGL
{
	public struct GLUniformBinding : IEquatable<GLUniformBinding>
	{
        public uint DescriptorCount { get; set; }
        public MgDescriptorType DescriptorType {
			get;
			set;
		}

		public uint Binding { get; set; }
        public MgShaderStageFlagBits StageFlags { get; set; }

		public bool Equals(GLUniformBinding other)
		{
			if (DescriptorType != other.DescriptorType)
			{
				return false;
			}

			if (DescriptorCount != other.DescriptorCount)
			{
				return false;
			}

			if (Binding != other.Binding)
			{
				return false;
			}

			if (StageFlags != other.StageFlags)
			{
				return false;
			}

			return true;
		}
	}
}

