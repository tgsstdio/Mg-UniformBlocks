using System.Collections.Generic;

namespace Magnesium.OpenGL
{
	public class GLInternalCacheArrayMapper
	{
		private readonly GLUniformBindingPointLayout mLayout;
		private readonly SortedDictionary<uint, GLUniformBlockGroupInfo> mGroups;

		public GLInternalCacheArrayMapper(GLUniformBindingPointLayout layout, GLUniformBlockEntry[] blockEntries)
		{
			mLayout = layout;

			var collator = new GLUniformBlockGroupCollator();
			foreach (var entry in blockEntries)
			{
				collator.Add(entry.Token);
			}

			var groups = collator.Collate();
			mGroups = groups;
		}

		public uint CalculateArrayIndex(GLUniformBlockEntry entry)
		{
			uint bindingPoint = 0U;

			var mapGroup = mGroups[entry.Token.BindingIndex];

			// ROW-ORDER 
			bindingPoint += entry.Token.X;
			bindingPoint += (mapGroup.ArrayStride * entry.Token.Y);
			bindingPoint += (mapGroup.MatrixStride * entry.Token.Z);

			var arrayOffset = mLayout.Offsets[entry.Token.BindingIndex];
			bindingPoint += arrayOffset.First;
			return bindingPoint;
		}
	}
}
