using System.Collections.Generic;

namespace Magnesium.OpenGL
{
	public class GLDescriptorSet : IGLDescriptorSet
	{
		public int Key { get; private set; }

		IGLImageDescriptorEntrypoint mImageEntrypoint;

		public GLDescriptorSet (int key, IGLImageDescriptorEntrypoint imageEntrypoint)
		{
			Key = key;
			mImageEntrypoint = imageEntrypoint;
            mBindings = new Dictionary<uint, GLDescriptorBinding>();

        }

        private Dictionary<uint, GLDescriptorBinding> mBindings;

		public void Populate(GLDescriptorSetLayout layout)
		{
            // LET'S USE ARRAY INDEXING
            var count = layout.Uniforms.Length;       
			int index = 0;
			foreach (var bind in layout.Uniforms)
			{
				if (bind.DescriptorType == MgDescriptorType.SAMPLER || bind.DescriptorType == MgDescriptorType.COMBINED_IMAGE_SAMPLER)
				{
                    var noOfArrayItems = bind.DescriptorCount;
                    var images = new GLImageDescriptor[noOfArrayItems];
                    for (var i = 0; i < noOfArrayItems; i += 1)
                    {
                        images[i] = new GLImageDescriptor(mImageEntrypoint);
                    }

					mBindings.Add(bind.Binding, new GLDescriptorBinding (bind.Binding, images));
				}
				else if
                (
                    bind.DescriptorType == MgDescriptorType.STORAGE_BUFFER
                    || bind.DescriptorType == MgDescriptorType.STORAGE_BUFFER_DYNAMIC 
                    || bind.DescriptorType == MgDescriptorType.UNIFORM_BUFFER
                    || bind.DescriptorType == MgDescriptorType.UNIFORM_BUFFER_DYNAMIC
                )
				{
                    var noOfArrayItems = bind.DescriptorCount;
                    var buffers = new GLBufferDescriptor[noOfArrayItems];
                    for (var i = 0; i < noOfArrayItems; i += 1)
                    {
                        buffers[i] = new GLBufferDescriptor
                        {
                            IsDynamic =
                                (
                                   bind.DescriptorType == MgDescriptorType.STORAGE_BUFFER_DYNAMIC
                                || bind.DescriptorType == MgDescriptorType.UNIFORM_BUFFER_DYNAMIC
                                ),
                                
                        };
                    }

                    mBindings.Add(bind.Binding, new GLDescriptorBinding(bind.Binding, buffers));
				}
				++index;
			}
		}

		public void Destroy ()
		{
			foreach (var binding in mBindings.Values)
			{
				binding.Destroy ();
			}
			mBindings = null;
		}

		#region IEquatable implementation

		public bool Equals (IGLDescriptorSet other)
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

        public bool TryGetValue(uint binding, out GLDescriptorBinding result)
        {
            return mBindings.TryGetValue(binding, out result);
        }
    }
}

