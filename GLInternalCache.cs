using System;
using System.Collections.Generic;

namespace Magnesium.OpenGL
{
	public class GLInternalCache
	{
		public GLInternalCache(int programId,
		                       IGLPipelineLayout layout, 
		                       IGLUniformBlockEntrypoint uniforms,
		                       IBlockTokenizer tokenizer)
		{
			// SETUP UNIFORMS
			var entries = SetupUniformBlocks(programId, uniforms, tokenizer);

			//BuildPointDirectory(layout);
		}



		private List<GLUniformBlockEntry> SetupUniformBlocks(int programId, IGLUniformBlockEntrypoint uniforms, IBlockTokenizer tokenizer)
		{
			var count = uniforms.GetNoOfActiveUniformBlocks(programId);
			var entries = new List<GLUniformBlockEntry>();
			for (var i = 0; i < count; i += 1)
			{
				string blockName = uniforms.GetActiveUniformBlockName(programId, i);
				var token = tokenizer.Extract(blockName);
				var blockInfo = uniforms.GetActiveUniformBlockData(programId, i);

				var entry = new GLUniformBlockEntry
				{
					BlockName = blockName,
					ActiveIndex = i,
					Stride = blockInfo.Stride,
					Token = token,
				};
				entries.Add(entry);
			}
			return entries;
		}
	}
}
