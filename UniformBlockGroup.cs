namespace UniBlocks
{
    public class UniformBlockGroup
    {
		public string Prefix { get;  set; }
        public int BindingIndex { get;  set; }
        public uint Count { get; set; }
		public uint ArrayStride { get; set; }
		public uint HighestRow { get; set; }

		public uint MatrixStride { get; set; }

    }
}