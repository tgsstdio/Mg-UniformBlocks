using System;
namespace UniBlocks
{
	// STARTS FROM ZERO
	public class GLBufferBindingPointDirectory : IEquatable<GLBufferBindingPointDirectory>
	{
		//public int ProgramID { get; set; }
		public int Count { get; set; }
		public int[] Buffers { get; set; }
		public IntPtr[] Offsets { get; set; }
		public int Sizes { get; set; }

		public bool Equals(GLBufferBindingPointDirectory other)
		{
			throw new NotImplementedException();
		}
	}
}
