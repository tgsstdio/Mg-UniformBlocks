﻿using System;
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
			var poolResource = new GLPoolResource<GLBufferResource>(NO_OF_ITEMS);

			Assert.IsNotNull(poolResource.Items);
			Assert.AreEqual(NO_OF_ITEMS, poolResource.Items.Length);

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
			var poolResource = new GLPoolResource<GLBufferResource>(NO_OF_ITEMS);
		}
	}
}