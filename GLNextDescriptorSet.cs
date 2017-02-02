﻿using System;

namespace Magnesium.OpenGL
{
	public class GLNextDescriptorSet : IGLDescriptorSet
	{
		public uint Key { get; private set; }
		public IGLDescriptorPool Parent { get; private set; }
		public GLDescriptorPoolResourceInfo[] Resources { get; private set; }

		public GLNextDescriptorSet(uint key, IGLDescriptorPool parent)
		{
			Key = key;
			Parent = parent;
			IsValidDescriptorSet = false;
		}

		public void Initialise(GLDescriptorPoolResourceInfo[] resources)
		{
			Resources = resources;
			IsValidDescriptorSet = true;
		}

		public bool IsValidDescriptorSet { get; internal set; }

		public void Invalidate()
		{
			Parent = null;
			Resources = null;
			IsValidDescriptorSet = false;
		}

		#region IEquatable implementation

		public bool Equals(IGLDescriptorSet other)
		{
			if (other == null)
				return false;

			return Key == other.Key;
		}

		#endregion

		public override int GetHashCode()
		{
			unchecked // Overflow is fine, just wrap
			{
				int hash = 17;
				// Suitable nullity checks etc, of course :)
				//hash = hash * 23 + Pool.GetHashCode();
				hash = hash * 23 + Key.GetHashCode();
				return hash;
			}
		}
	}
}
