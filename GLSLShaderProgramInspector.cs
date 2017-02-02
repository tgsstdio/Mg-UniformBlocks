using System.Collections.Generic;

namespace Magnesium.OpenGL
{
	public class GLSLShaderProgramInspector : IGLShaderModuleInspector
	{
		readonly IGLUniformBlockEntrypoint mEntrypoint;
		readonly IGLUniformBlockNameParser mParser;

		public GLSLShaderProgramInspector(IGLUniformBlockEntrypoint entrypoint, IGLUniformBlockNameParser parser)
		{
			mEntrypoint = entrypoint;
			mParser = parser;
		}

		public GLUniformBlockEntry[] Inspect(int programId)
		{
			var count = mEntrypoint.GetNoOfActiveUniformBlocks(programId);
			var entries = new List<GLUniformBlockEntry>();
			for (uint i = 0; i < count; i += 1)
			{
				string blockName = mEntrypoint.GetActiveUniformBlockName(programId, i);
				var token = mParser.Parse(blockName);
				var blockInfo = mEntrypoint.GetActiveUniformBlockInfo(programId, i);
				token.BindingIndex = blockInfo.BindingIndex;

				var entry = new GLUniformBlockEntry
				{
					BlockName = blockName,
					ActiveIndex = i,
					Stride = blockInfo.Stride,
					Token = token,
				};
				entries.Add(entry);
			}
			return entries.ToArray();
		}
	}
}
