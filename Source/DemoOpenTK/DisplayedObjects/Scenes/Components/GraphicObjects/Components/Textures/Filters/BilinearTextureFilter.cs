using OpenTK.Graphics.OpenGL;

namespace DemoOpenTK
{
    public class BilinearTextureFilter : ITextureFilter
    {
        public void Apply()
        {
            TextureTarget target = TextureTarget.Texture2D;
            GL.TexParameter(target, TextureParameterName.TextureMinFilter, (float)TextureMinFilter.Linear);
            GL.TexParameter(target, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Linear);
        }
    }
}
