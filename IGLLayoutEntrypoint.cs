using System;

namespace UniBlocks
{
    public interface IGLLayoutEntrypoint
    {
        void BindBuffersRange(int[] buffers, int[] bindingIndices, IntPtr offsets, int[] sizes);
    }
}
