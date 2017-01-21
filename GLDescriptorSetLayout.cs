﻿using System.Collections.Generic;

namespace Magnesium.OpenGL
{
	public class GLDescriptorSetLayout : IGLDescriptorSetLayout
	{
		public GLDescriptorSetLayout (MgDescriptorSetLayoutCreateInfo pCreateInfo)
		{
			var bindings = new List<GLUniformBinding>();

			if (pCreateInfo.Bindings != null)
			{
				foreach (var binding in pCreateInfo.Bindings)
				{
					var uniform = new GLUniformBinding{                        
                        Binding = binding.Binding,
                        DescriptorType = binding.DescriptorType,
                        DescriptorCount = binding.DescriptorCount,
                        StageFlags = binding.StageFlags,
                    };
					bindings.Add (uniform);
				}
			}
            Uniforms = bindings.ToArray();
		}

		public GLUniformBinding[] Uniforms { get; private set; }

		#region IMgDescriptorSetLayout implementation
		private bool mIsDisposed = false;
		public void DestroyDescriptorSetLayout (IMgDevice device, IMgAllocationCallbacks allocator)
		{
			if (mIsDisposed)
				return;

			mIsDisposed = true;
		}

		#endregion
	}
}

