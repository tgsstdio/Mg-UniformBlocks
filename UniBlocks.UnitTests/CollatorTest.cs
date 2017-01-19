using NUnit.Framework;
using System;
using Magnesium.OpenGL;

namespace UniBlocks.UnitTests
{
	[TestFixture()]
	public class CollatorTest
	{
		[Test()]
		public void TestCase()
		{
			IBlockTokenizer tokenizer = new BlockTokenizer();
			var token0 = tokenizer.Extract("UBO[0]");
			var collator = new UniformBlockGroupCollator();
			collator.Add(token0);
			var token1 = tokenizer.Extract("UBO[1]");
			collator.Add(token1);

			var groups = collator.Collate();
			Assert.AreEqual(1, groups.Length);
			var firstGroup = groups[0];
			Assert.AreEqual("UBO", firstGroup.Prefix);
			Assert.AreEqual(2, firstGroup.Count);
		}

		[Test()]
		public void TestCase2()
		{
			var token0 = new UniformBlockInfo
			{
				Prefix = "UBO",
				BindingIndex = 10,
				X = 0,
				Y = 0,
				Z = 0,
			};
			var collator = new UniformBlockGroupCollator();
			collator.Add(token0);
			var token1 = new UniformBlockInfo
			{
				Prefix = "UBO",
				BindingIndex = 15,
				X = 1,
				Y = 0,
				Z = 0,
			};
			collator.Add(token1);

			var groups = collator.Collate();
			Assert.AreEqual(1, groups.Length);
			var firstGroup = groups[0];
			Assert.AreEqual("UBO", firstGroup.Prefix);
			Assert.AreEqual(2, firstGroup.Count);
			Assert.AreEqual(token0.BindingIndex, firstGroup.BindingIndex);
		}

		[Test()]
		public void TestCase3()
		{
			var token0 = new UniformBlockInfo
			{
				Prefix = "UBO",
				BindingIndex = 10,
				X = 0,
				Y = 0,
				Z = 0,
			};
			var collator = new UniformBlockGroupCollator();
			collator.Add(token0);
			var token1 = new UniformBlockInfo
			{
				Prefix = "ubo",
				BindingIndex = 15,
				X = 1,
				Y = 0,
				Z = 0,
			};
			collator.Add(token1);

			var groups = collator.Collate();
			Assert.AreEqual(2, groups.Length);

			// SHOULD BE SORTED BY BINDING INDEX
			{
				var g = groups[0];
				Assert.AreEqual("UBO", g.Prefix);
				Assert.AreEqual(1, g.Count);
				Assert.AreEqual(10, g.BindingIndex);
			}

			{
				var g = groups[1];
				Assert.AreEqual("ubo", g.Prefix);
				Assert.AreEqual(1, g.Count);
				Assert.AreEqual(15, g.BindingIndex);
			}
		}

		[Test()]
		public void GroupsSorted1()
		{
			// SHOULD BE SORTED BY BINDING INDEX OF FIRST ELEMENT
			var token0 = new UniformBlockInfo
			{
				Prefix = "C",
				BindingIndex = 10,
				X = 0,
				Y = 0,
				Z = 0,
			};
			var collator = new UniformBlockGroupCollator();
			collator.Add(token0);
			var token1 = new UniformBlockInfo
			{
				Prefix = "A",
				BindingIndex = 12,
				X = 1,
				Y = 0,
				Z = 0,
			};
			collator.Add(token1);

			var token2 = new UniformBlockInfo
			{
				Prefix = "B",
				BindingIndex = 11,
				X = 1,
				Y = 0,
				Z = 0,
			};
			collator.Add(token2);

			var groups = collator.Collate();
			Assert.AreEqual(3, groups.Length);

			// SHOULD BE SORTED BY BINDING INDEX
			{
				var group = groups[0];
				Assert.AreEqual("C", group.Prefix);
				Assert.AreEqual(1, group.Count);
				Assert.AreEqual(token0.BindingIndex, group.BindingIndex);
			}

			{
				var group = groups[1];
				Assert.AreEqual("B", group.Prefix);
				Assert.AreEqual(1, group.Count);
				Assert.AreEqual(token2.BindingIndex, group.BindingIndex);
			}

			{
				var group = groups[2];
				Assert.AreEqual("A", group.Prefix);
				Assert.AreEqual(1, group.Count);
				Assert.AreEqual(token1.BindingIndex, group.BindingIndex);
			}
		}

		[Test()]
		public void RowOffset0()
		{
			var token0 = new UniformBlockInfo
			{
				Prefix = "UBO",
				BindingIndex = 10,
				X = 0,
				Y = 0,
				Z = 0,
			};
			var collator = new UniformBlockGroupCollator();
			collator.Add(token0);
			var token1 = new UniformBlockInfo
			{
				Prefix = "UBO",
				BindingIndex = 10,
				X = 1,
				Y = 0,
				Z = 0,
			};
			collator.Add(token1);

			var groups = collator.Collate();
			Assert.AreEqual(1, groups.Length);
			{
				var firstGroup = groups[0];
				Assert.AreEqual("UBO", firstGroup.Prefix);
				Assert.AreEqual(2, firstGroup.Count);
				Assert.AreEqual(token0.BindingIndex, firstGroup.BindingIndex);
				Assert.AreEqual(2, firstGroup.ArrayStride);
				Assert.AreEqual(0, firstGroup.HighestRow);
				Assert.AreEqual(2, firstGroup.MatrixStride);
			}
		}

		[Test()]
		public void RowOffset1()
		{
			var token0 = new UniformBlockInfo
			{
				Prefix = "UBO",
				BindingIndex = 10,
				X = 0,
				Y = 0,
				Z = 0,
			};
			var collator = new UniformBlockGroupCollator();
			collator.Add(token0);
			var token1 = new UniformBlockInfo
			{
				Prefix = "UBO",
				BindingIndex = 10,
				X = 2,
				Y = 2,
				Z = 0,
			};
			collator.Add(token1);

			var groups = collator.Collate();
			Assert.AreEqual(1, groups.Length);
			{
				var firstGroup = groups[0];
				Assert.AreEqual("UBO", firstGroup.Prefix);
				Assert.AreEqual(2, firstGroup.Count);
				Assert.AreEqual(token0.BindingIndex, firstGroup.BindingIndex);
				Assert.AreEqual(3, firstGroup.ArrayStride);
				Assert.AreEqual(2, firstGroup.HighestRow);
				Assert.AreEqual(6, firstGroup.MatrixStride);
			}
		}


		[Test()]
		public void RowOffset2()
		{
			var collator = new UniformBlockGroupCollator();
			collator.Add(new UniformBlockInfo
			{
				Prefix = "UBO",
				BindingIndex = 10,
				X = 4,
				Y = 0,
				Z = 0,
			});

			collator.Add(new UniformBlockInfo
			{
				Prefix = "UBO",
				BindingIndex = 10,
				X = 0,
				Y = 0,
				Z = 0,
			});

			collator.Add(new UniformBlockInfo
			{
				Prefix = "UBO",
				BindingIndex = 10,
				X = 8,
				Y = 0,
				Z = 0,
			});

			var groups = collator.Collate();
			Assert.AreEqual(1, groups.Length);
			{
				var firstGroup = groups[0];
				Assert.AreEqual("UBO", firstGroup.Prefix);
				Assert.AreEqual(3, firstGroup.Count);
				Assert.AreEqual(10, firstGroup.BindingIndex);
				Assert.AreEqual(9, firstGroup.ArrayStride);
				Assert.AreEqual(0, firstGroup.HighestRow);
				Assert.AreEqual(9, firstGroup.MatrixStride);
			}
		}
		[Test()]
		public void RowOffset3()
		{
			// TODO : row major or column major
			var collator = new UniformBlockGroupCollator();
			collator.Add(new UniformBlockInfo
			{
				Prefix = "UBO",
				BindingIndex = 10,
				X = 4,
				Y = 0,
				Z = 0,
			});
	
			collator.Add(new UniformBlockInfo
			{
				Prefix = "UBO",
				BindingIndex = 10,
				X = 2,
				Y = 3,
				Z = 0,
			});

			collator.Add(new UniformBlockInfo
			{
				Prefix = "UBO",
				BindingIndex = 10,
				X = 2,
				Y = 2,
				Z = 0,
			});

			var groups = collator.Collate();
			Assert.AreEqual(1, groups.Length);
			{
				var firstGroup = groups[0];
				Assert.AreEqual("UBO", firstGroup.Prefix);
				Assert.AreEqual(3, firstGroup.Count);
				Assert.AreEqual(10, firstGroup.BindingIndex);
				Assert.AreEqual(5, firstGroup.ArrayStride);
				Assert.AreEqual(3, firstGroup.HighestRow);
				Assert.AreEqual(15, firstGroup.MatrixStride);
			}
		}
	}
}
