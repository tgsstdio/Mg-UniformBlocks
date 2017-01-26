using System.Collections.Generic;

namespace Magnesium.OpenGL
{
	public class GLInternalCache
	{
		public int[] Strides { get; set; }
		public GLInternalCacheBlockBinding[] BlockBindings { get; private set; }
		private GLInternalCacheArrayMapper mMapLocator;
		public GLInternalCache(
			//int programId,
			// IGLPipelineLayout pipelineLayout,
			IGLPipelineLayout pipelineLayout,
							   ///IGLUniformBlockEntrypoint uniforms,
							GLUniformBlockEntry[] blockEntries,
							   GLInternalCacheArrayMapper arrayLocator
								// IBlockTokenizer tokenizer
								)
		{
			// SETUP UNIFORMS
			//var entries = SetupUniformBlocks(programId, uniforms, tokenizer);

			//var layout = new GLUniformBindingPointLayout(pipelineLayout);

			//mMapLocator = new GLInternalCacheArrayMapper(layout, groups);
			mMapLocator = arrayLocator;
			SetupBlockBindings(blockEntries, mMapLocator);
			SetupStrides(blockEntries, pipelineLayout, mMapLocator);
		}

		void SetupStrides(GLUniformBlockEntry[] blockEntries, IGLPipelineLayout layout, GLInternalCacheArrayMapper locator)
		{
			Strides = new int[layout.NoOfBindingPoints];
			for (var i = 0; i < layout.NoOfBindingPoints; i += 1)
			{
				Strides[i] = 0;
			}

			foreach (var entry in blockEntries)
			{
				var arrayIndex = locator.CalculateArrayIndex(entry);
				Strides[arrayIndex] = entry.Stride;
			}
		}

		void SetupBlockBindings(GLUniformBlockEntry[] entries, GLInternalCacheArrayMapper locator)
		{
			BlockBindings = new GLInternalCacheBlockBinding[entries.Length];
			for (var i = 0; i < entries.Length; i += 1)
			{
				var entry = entries[i];
				BlockBindings[i] = new GLInternalCacheBlockBinding
				{
					BlockName = entry.BlockName,
					ActiveIndex = entry.ActiveIndex,
					BindingPoint = locator.CalculateArrayIndex(entry),
				};
			}
		}

	}
}
