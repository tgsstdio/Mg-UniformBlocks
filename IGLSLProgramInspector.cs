namespace Magnesium.OpenGL
{
	public interface IGLSLProgramInspector
	{
		GLUniformBlockEntry[] ExtractBlockEntries(int programId);
	}
}