using System;

namespace Magnesium.OpenGL
{
	public interface IGLUniformBlockEntrypoint
	{
		int GetMaxNoOfBindingPoints();
		int GetNoOfActiveUniformBlocks(int programId);
		string GetActiveUniformBlockName(int programId, uint activeIndex);
		GLActiveUniformBlockInfo GetActiveUniformBlockInfo(int programId, uint activeIndex);
		void SetUniformBlockiBindingPoint(int programId, uint activeIndex, int bindingPoint);
		void BindBuffersRange(GLBufferRangeTarget target, int first, int count, int[] buffers, IntPtr[] offsets, int[] sizes);
	}
}
