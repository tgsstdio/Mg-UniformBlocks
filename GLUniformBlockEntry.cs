namespace Magnesium.OpenGL
{
	class GLUniformBlockEntry
	{
		public int ActiveIndex { get; internal set; }
		public string BlockName { get; internal set; }
		public int Stride { get; internal set; }
		public UniformBlockInfo Token { get; internal set; }
	}
}