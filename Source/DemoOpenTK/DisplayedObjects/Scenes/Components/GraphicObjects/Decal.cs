using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DemoOpenTK.DisplayedObjects.Scenes.Components.GraphicObjects
{
    public class Decal : TextureGraphicObject
    {
        public Decal(BaseMaterial material, IMesh mesh, BaseTexture texture, ITextureFilter filter) :
            base(material, mesh, texture, filter)
        {
        }

        public override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Enable(EnableCap.PolygonOffsetFill);

            GL.PolygonOffset(-1, -3);
            base.OnRenderFrame(args);

            GL.Disable(EnableCap.PolygonOffsetFill);
        }
    }

}
