using System;
namespace Magnesium.OpenGL
{
	public interface IGLDescriptorSet : IMgDescriptorSet, IEquatable<IGLDescriptorSet>
	{
		uint Key { get; }
		IGLDescriptorPool Parent { get; }
		GLDescriptorPoolResourceInfo[] Resources { get;  }

		void Initialise(GLDescriptorPoolResourceInfo[] resources);
		bool IsValidDescriptorSet { get; }
		void Invalidate();
	}
}
