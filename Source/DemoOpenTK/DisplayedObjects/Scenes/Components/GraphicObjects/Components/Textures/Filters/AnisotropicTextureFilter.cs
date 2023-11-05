using OpenTK.Graphics.OpenGL;

namespace DemoOpenTK
{
    public class AnisotropicTextureFilter : ITextureFilter
    {
        public void Apply()
        {
            TextureTarget target = TextureTarget.Texture2D;
            GL.TexParameter(target, TextureParameterName.TextureMinFilter, (float)TextureMinFilter.LinearMipmapLinear);
            GL.TexParameter(target, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Linear);
        }
    }
}
