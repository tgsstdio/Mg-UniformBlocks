﻿namespace Magnesium.OpenGL
{
    public class GLUniformBlockGroupInfo
    {
		public string Prefix { get;  set; }
        public uint BindingIndex { get;  set; }
        public uint Count { get; set; }
		public uint ArrayStride { get; set; }
		public uint HighestRow { get; set; }
		public uint MatrixStride { get; set; }
		public uint HighestLayer { get; set; }
		public uint CubeStride { get; set; }
    }
}