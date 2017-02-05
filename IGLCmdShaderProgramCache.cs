using System;
namespace Magnesium.OpenGL
{
	public interface IGLCmdShaderProgramCache
	{
		int ProgramID { get; }
		void SetProgramID(int programId);
		int VAO { get; }
		void SetVAO(int vao);
		IGLDescriptorSet DescriptorSet { get; set;}
		void SetDynamicOffsets(uint[] offsets);
	}
}
