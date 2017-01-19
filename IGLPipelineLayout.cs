namespace Magnesium.OpenGL
{
    public interface IGLPipelineLayout : IMgPipelineLayout
    {
        GLUniformBinding[] Bindings { get; }
    }
}