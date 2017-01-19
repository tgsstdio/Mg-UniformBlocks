using System;
using System.Collections.Generic;

namespace Magnesium.OpenGL
{
	public class GLInternalCache
	{
		public class GLInternalBlockBinding
		{
			public int ActiveIndex { get; set; }
			public int BindingPoint { get; set; }
		}

		public GLInternalBlockBinding[] BlockBindings { get; private set;}
		public GLInternalCache(int programId,
		                       IGLPipelineLayout pipelineLayout, 
		                       IGLUniformBlockEntrypoint uniforms,
		                       IBlockTokenizer tokenizer)
		{
			// SETUP UNIFORMS
			var entries = SetupUniformBlocks(programId, uniforms, tokenizer);

			var layout = new GLUniformBindingPointLayout(pipelineLayout);

			var collator = new UniformBlockGroupCollator();
			foreach (var entry in entries)
			{
				collator.Add(entry.Token);
			}

			var groups = collator.Collate();

			BlockBindings = new GLInternalBlockBinding[entries.Count];
			var i = 0;
			foreach (var entry in entries)
			{
				BlockBindings[i] = new GLInternalBlockBinding
				{
					ActiveIndex = entry.ActiveIndex,
				};


				GLBindingPointOffsetInfo found;
				if (layout.Offsets.TryGetValue(entry.Token.BindingIndex, out found))
				{
					var offset = found.First;		
				
					//BlockBindings[i].BindingPoint = x;
				}

				i += 1;
			}
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
