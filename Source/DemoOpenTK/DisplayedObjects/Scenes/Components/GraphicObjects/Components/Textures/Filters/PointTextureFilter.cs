using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoOpenTK
{
    public class PointTextureFilter : ITextureFilter
    {
        public void Apply()
        {
            TextureTarget target = TextureTarget.Texture2D;
            GL.TexParameter(target, TextureParameterName.TextureMinFilter, (float)TextureMinFilter.Nearest);
            GL.TexParameter(target, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Nearest);
        }
    }
}
