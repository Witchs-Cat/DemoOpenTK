using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace DemoOpenTK
{
    public class MeshGraphicObject : GraphicObject
    {
        public MeshGraphicObject(BaseMaterial material, IMesh mesh) : base(material)
        {
            Mesh = mesh;
        }

        public IMesh Mesh { get; }

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
