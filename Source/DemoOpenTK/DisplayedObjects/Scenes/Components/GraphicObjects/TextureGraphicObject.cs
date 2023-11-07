using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace DemoOpenTK
{
    public class TextureGraphicObject : MeshGraphicObject
    {
        public TextureGraphicObject(BaseMaterial material, IMesh mesh, BaseTexture texture, ITextureFilter filter) : base(material, mesh)
        {
            Texture = texture;
            Filter = filter;
        }

        public BaseTexture Texture { get; }
        public ITextureFilter Filter { get; }

        public override void OnRenderFrame(FrameEventArgs args)
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.Enable(EnableCap.Texture2D);

            GetModelMatrix(out Matrix4 modelMatrix);

            GL.PushMatrix();
            GL.MultMatrix(ref modelMatrix);

            Texture.Apply();
            Filter.Apply();

            Material.Apply();
            Mesh.Apply();

            GL.PopMatrix();

            GL.Disable(EnableCap.Texture2D);
        }

    }
}
