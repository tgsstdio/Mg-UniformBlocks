﻿
using System;

namespace Magnesium.OpenGL
{
	public class GLBufferDescriptor
	{
		public GLBufferDescriptor()
		{
			BufferId = 0;
		}

		public int BufferId { get; set; }
		public bool IsDynamic { get; internal set; }
		public long Offset { get; internal set; }
		public int Size { get; internal set; }

		public void Destroy()
		{

		}

		public void Reset()
		{
			BufferId = 0;
			IsDynamic = false;
			Offset = 0L;
			Size = 0;
		}
	}
}

