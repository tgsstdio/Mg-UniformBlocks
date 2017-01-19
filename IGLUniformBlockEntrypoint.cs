using System;
using Magnesium;

namespace Magnesium.OpenGL
{
	public struct BlockData
	{
		public MgShaderStageFlagBits Stage { get; set; }
		public int Stride { get; set; }
	}

	public enum GLBindingTarget
	{
		STORAGE_BUFFER,
		UNIFORM_BUFFER,
	}


	public interface IGLUniformBlockEntrypoint
	{
		int GetMaxNoOfBindingPoints();
		int GetNoOfActiveUniformBlocks(int programId);
		string GetActiveUniformBlockName(int programId, int activeIndex);
		BlockData GetActiveUniformBlockData(int programId, int activeIndex);
		void SetUniformBlockiBindingPoint(int programId, int activeIndex, int bindingPoint);
		void BindBuffersRange(GLBindingTarget target, int first, int count, int[] buffers, IntPtr[] offsets, int[] sizes);
	}
}
