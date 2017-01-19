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
			var result = new GLUniformBindingPointLayout(mock);
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
			var result = new GLUniformBindingPointLayout(mock);
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
			var result = new GLUniformBindingPointLayout(mock);
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
			var result = new GLUniformBindingPointLayout(mock);
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
			var result = new GLUniformBindingPointLayout(mock);
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.NoOfBindingPoints);
			Assert.AreEqual(2, result.Offsets.Keys.Count);

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


	}
}
