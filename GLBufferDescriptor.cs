
namespace Magnesium.OpenGL
{
	public class GLBufferDescriptor
	{
		public GLBufferDescriptor ()
		{
			BufferId = 0;
		}

		public int BufferId { get; set; }
        public bool IsDynamic { get; internal set; }
        public long Offset { get; internal set; }
		public int Size { get; internal set; }

		public void Destroy ()
		{

		}
	}
}

