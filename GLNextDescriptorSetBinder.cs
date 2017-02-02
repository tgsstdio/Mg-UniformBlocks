﻿using System;
namespace Magnesium.OpenGL
{
	public class GLNextDescriptorSetBinder : IGLDescriptorSetBinder
	{
		public GLNextDescriptorSetBinder()
		{
			Clear();
		}

		public IGLPipelineLayout BoundPipelineLayout { get; private set;}
		public uint[] BoundDynamicOffsets { get; private set;}
		public IGLDescriptorSet BoundDescriptorSet { get; private set;}

		public void Clear()
		{
			BoundPipelineLayout = null;
			BoundDynamicOffsets = null;
			BoundDescriptorSet = null;
		}

		public bool IsInvalid { get; private set; }

		public void Bind(MgPipelineBindPoint pipelineBindPoint, IMgPipelineLayout layout, uint firstSet, 
		                 uint descriptorSetCount, IMgDescriptorSet[] pDescriptorSets, uint[] pDynamicOffsets)
		{
			if (layout == null)
			{
				throw new ArgumentNullException(nameof(layout));
			}

			if (pDescriptorSets == null)
			{
				throw new ArgumentNullException(nameof(pDescriptorSets));
			}

			if (firstSet != 0)
			{
				throw new InvalidOperationException("Mg.GL : only descriptor set 0 can be bound.");
			}

			var bLayout = (IGLPipelineLayout)layout;

			IsInvalid = false;
			if (!BoundPipelineLayout.Equals(bLayout))
			{
				BoundPipelineLayout = bLayout;
				IsInvalid = true;
			}

			var isArrayDifferent = CopyDynamicOffsetsIfDifferent(pDynamicOffsets);
			IsInvalid = IsInvalid && isArrayDifferent;

			var bDescSet = (IGLDescriptorSet)pDescriptorSets[0];
			// EXACT DSET ONLY
			if (!BoundDescriptorSet.Equals(bDescSet))
			{
				BoundDescriptorSet = bDescSet;
				IsInvalid = true;
			}
		}

		bool CopyDynamicOffsetsIfDifferent(uint[] pDynamicOffsets)
		{
			bool needsChange = false;

			if (pDynamicOffsets == null)
			{
				BoundDynamicOffsets = new uint[BoundPipelineLayout.NoOfExpectedDynamicOffsets];
				needsChange = true;
			}

			var suppliedLength = pDynamicOffsets == null ? 0 : pDynamicOffsets.Length;
			var finalLoopCount = Math.Min(suppliedLength, BoundDynamicOffsets.Length);

			for (var i = 0; i < finalLoopCount; i += 1)
			{
				if (pDynamicOffsets[i] != BoundDynamicOffsets[i])
				{
					BoundDynamicOffsets[i] = pDynamicOffsets[i];
					needsChange = true;
				}
			}

			return needsChange;
		}


	}
}
