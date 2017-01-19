using System;

namespace Magnesium.OpenGL
{
    public interface IGLDescriptorSet : IMgDescriptorSet, IEquatable<IGLDescriptorSet>
    {
        int Key { get; }
        bool TryGetValue(uint binding, out GLDescriptorBinding result);
    }
}