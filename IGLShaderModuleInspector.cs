namespace Magnesium.OpenGL
{
	public interface IGLShaderModuleInspector
	{
		GLUniformBlockEntry[] Inspect(int programId);
	}
}