using System;
namespace Magnesium.OpenGL
{
	public class GLPoolResourceInfo
	{
		public uint First { get; internal set; }
		public uint Last { get; internal set; }
		public uint Count { get; internal set; }
	}
}
