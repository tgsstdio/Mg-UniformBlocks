using System;
namespace Magnesium.OpenGL
{
	// STARTS FROM ZERO
	public class GLBufferBindingPointDirectory : IEquatable<GLBufferBindingPointDirectory>
	{
		public GLInternalCacheBlockBinding[] BlockBindings { get; private set; }
		public int Count { get; set; }
		public int[] Buffers { get; set; }
		public IntPtr[] Offsets { get; set; }
		public int[] Sizes { get; set; }

		public bool Equals(GLBufferBindingPointDirectory other)
		{
			throw new NotImplementedException();
		}
	}
}
