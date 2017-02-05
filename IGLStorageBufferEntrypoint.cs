using System;

namespace Magnesium.OpenGL
{
	public interface IGLStorageBufferEntrypoint
	{
		void BindBuffer(int binding, int buffer, IntPtr offset, int size);
	}
}
