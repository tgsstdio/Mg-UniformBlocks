using System;
using System.Collections.Generic;
using Magnesium.OpenGL;

namespace UniBlocks
{
	class UniformBlockGroupCollator
	{
		Dictionary<string, UniformBlockGroup> mGroups;

		public UniformBlockGroupCollator()
		{
			mGroups = new Dictionary<string, UniformBlockGroup>();
		}
		public void Add(UniformBlockInfo entry)
		{
			UniformBlockGroup found;
			if (mGroups.TryGetValue(entry.Prefix, out found))
			{
				if (entry.X == 0 && entry.Y == 0 && entry.Z == 0)
				{
					found.BindingIndex = entry.BindingIndex;
				}

				found.ArrayStride = Math.Max(found.ArrayStride, entry.X + 1);
				found.HighestRow = Math.Max(found.HighestRow, entry.Y);
				found.Count += 1;
			}
			else
			{
				found = new UniformBlockGroup
				{
					Prefix = entry.Prefix,
					BindingIndex = entry.BindingIndex,
					Count = 1,
					ArrayStride = entry.X + 1,
					HighestRow = entry.Y,
				};
				mGroups.Add(found.Prefix, found);
			}
		}

		public UniformBlockGroup[] Collate()
		{
			var sortedResults = new SortedList<int, UniformBlockGroup>();
			foreach (var blockGroup in mGroups.Values)
			{
				blockGroup.MatrixStride = (blockGroup.ArrayStride * Math.Max(blockGroup.HighestRow, 1));
				sortedResults.Add(blockGroup.BindingIndex, blockGroup);
			}
			var results = new UniformBlockGroup[sortedResults.Count];
			sortedResults.Values.CopyTo(results, 0);
			return results;
		}
	}
}