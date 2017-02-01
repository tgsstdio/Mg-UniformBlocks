using System;
using Magnesium.OpenGL;
using NUnit.Framework;

namespace UniBlocks.UnitTests
{
	[TestFixture]
	public class ArrayPoolTests
	{
		[TestCase]
		public void ConstructorTest0()
		{
			const uint NO_OF_ITEMS = 6;
			var items = new MockGLStaticBufferResource[0];
			var poolResource = new GLPoolResource<MockGLStaticBufferResource>(NO_OF_ITEMS, items);

			Assert.AreSame(items, poolResource.Items);
			Assert.AreEqual(NO_OF_ITEMS, poolResource.Count);

			var head = poolResource.Head;
			Assert.IsNotNull(head);
			Assert.AreEqual(NO_OF_ITEMS, head.Count);
			Assert.AreEqual(0, head.First);
			Assert.AreEqual(5, head.Last);
			Assert.IsNull(head.Next);
		}

		[TestCase]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void ConstructorTest1()
		{
			const uint NO_OF_ITEMS = 0;
			var poolResource = new GLPoolResource<MockGLStaticBufferResource>(NO_OF_ITEMS, null);
		}
	}
}
