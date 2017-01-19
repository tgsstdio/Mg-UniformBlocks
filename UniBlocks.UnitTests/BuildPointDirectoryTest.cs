using System;
using System.Collections.Generic;
using Magnesium.OpenGL;
using NUnit.Framework;

namespace UniBlocks.UnitTests
{
	[TestFixture]
	public class BuildPointDirectoryTest
	{
		[Test()]
		public void BuildStructure0()
		{
			IGLPipelineLayout mock = new MockGLPipelineLayout
			{
				Bindings = new GLUniformBinding[]
				{
				}
			};
			var result = BuildPointDirectory(mock);
			Assert.IsNotNull(result);
			Assert.AreEqual(0, result.NoOfBindingPoints);
		}

		[Test()]
		public void BuildStructure1()
		{
			IGLPipelineLayout mock = new MockGLPipelineLayout
			{
				Bindings = new GLUniformBinding[]
				{
					new GLUniformBinding
					{
						Binding = 0,
						DescriptorType = Magnesium.MgDescriptorType.UNIFORM_BUFFER,
						DescriptorCount = 10,
					}
				}
			};
			var result = BuildPointDirectory(mock);
			Assert.IsNotNull(result);
			Assert.AreEqual(10, result.NoOfBindingPoints);
		}

		[Test()]
		public void BuildStructure2()
		{
			IGLPipelineLayout mock = new MockGLPipelineLayout
			{
				Bindings = new GLUniformBinding[]
				{
					new GLUniformBinding
					{
						Binding = 0,
						DescriptorType = Magnesium.MgDescriptorType.UNIFORM_BUFFER,
						DescriptorCount = 1,
					},
					new GLUniformBinding
					{
						Binding = 1,
						DescriptorType = Magnesium.MgDescriptorType.UNIFORM_BUFFER,
						DescriptorCount = 2,
					},
				}
			};
			var result = BuildPointDirectory(mock);
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.NoOfBindingPoints);
		}

		[Test()]
		public void BuildStructure3()
		{
			IGLPipelineLayout mock = new MockGLPipelineLayout
			{
				Bindings = new GLUniformBinding[]
				{
					new GLUniformBinding
					{
						Binding = 0,
						DescriptorType = Magnesium.MgDescriptorType.UNIFORM_BUFFER_DYNAMIC,
						DescriptorCount = 1,
					},
					new GLUniformBinding
					{
						Binding = 1,
						DescriptorType = Magnesium.MgDescriptorType.UNIFORM_BUFFER_DYNAMIC,
						DescriptorCount = 2,
					},
				}
			};
			var result = BuildPointDirectory(mock);
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.NoOfBindingPoints);
		}

		[Test()]
		public void BuildStructure4()
		{
			IGLPipelineLayout mock = new MockGLPipelineLayout
			{
				Bindings = new GLUniformBinding[]
				{
					new GLUniformBinding
					{
						Binding = 0,
						DescriptorType = Magnesium.MgDescriptorType.UNIFORM_BUFFER_DYNAMIC,
						DescriptorCount = 1,
					},
					new GLUniformBinding
					{
						Binding = 1,
						DescriptorType = Magnesium.MgDescriptorType.UNIFORM_BUFFER,
						DescriptorCount = 2,
					},
				}
			};
			var result = BuildPointDirectory(mock);
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.NoOfBindingPoints);
			Assert.AreEqual(2, result.Offsets.Length);

			{
				var g1 = result.Offsets[0];
				Assert.AreEqual(0, g1.Binding);
				Assert.AreEqual(0, g1.First);
				Assert.AreEqual(0, g1.Last);
			}

			{
				var g1 = result.Offsets[1];
				Assert.AreEqual(1, g1.Binding);
				Assert.AreEqual(1, g1.First);
				Assert.AreEqual(2, g1.Last);
			}
		}

		static GLBindingPointDirectoryLayout BuildPointDirectory(IGLPipelineLayout layout)
		{
			var count = 0U;
			var groups = new SortedList<uint, GLBindingPointOffsetInfo>();
			// build flat slots array for uniforms 
			foreach (var desc in layout.Bindings)
			{
				if (desc.DescriptorType == Magnesium.MgDescriptorType.UNIFORM_BUFFER
					|| desc.DescriptorType == Magnesium.MgDescriptorType.UNIFORM_BUFFER_DYNAMIC)
				{
					count += desc.DescriptorCount;
					groups.Add(desc.Binding,
						   new GLBindingPointOffsetInfo
						   {
							   Binding = desc.Binding,
							   First = 0U,
							   Last = desc.DescriptorCount - 1,
							});
				}


			}

			var startingOffset = 0U;
			foreach (var g in groups.Values)
			{
				g.First += startingOffset;
				g.Last += startingOffset;
				startingOffset += g.Last + 1;
			}

			var result = new GLBindingPointOffsetInfo[groups.Count];
			groups.Values.CopyTo(result, 0);
			return new GLBindingPointDirectoryLayout { 
				NoOfBindingPoints = count,
				Offsets = result,
			};
		}
	}
}
