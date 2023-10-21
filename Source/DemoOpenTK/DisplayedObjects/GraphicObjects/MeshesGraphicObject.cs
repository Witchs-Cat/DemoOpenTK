using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace DemoOpenTK
{
    public class MeshGraphicObject : GraphicObject
    {
        public MeshGraphicObject(BaseMaterial material, Mesh mesh) : base(material)
        {
            Mesh = mesh;
        }

        public Mesh Mesh { get; }

        public override void OnRenderFrame(in FrameEventArgs args)
        {
            GetModelMatrix(out Matrix4 modelMatrix);
            GL.PushMatrix();
            GL.MultMatrix(ref modelMatrix);

            Material.Apply();
            Mesh.Apply();

            GL.PopMatrix();
        }

    }
}
