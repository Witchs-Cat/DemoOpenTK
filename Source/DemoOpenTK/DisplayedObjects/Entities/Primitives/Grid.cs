﻿using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;

namespace DemoOpenTK
{
    public class Grid : GeometryEntity
    {
        public Grid()
        { }

        public override void OnRenderFrame(in FrameEventArgs args)
        {
            GL.Begin(PrimitiveType.Lines);
            {
                GL.Color3(1.0f, 0.0f, 0.0f);
                GL.Vertex3(6.0f, 0.0f, 0.0f);
                GL.Vertex3(0.0f, 0.0f, 0.0f);

                GL.Color3(0.0f, 1.0f, 0.0f);
                GL.Vertex3(0.0f, 6.0f, 0.0f);
                GL.Vertex3(0.0f, 0.0f, 0.0f);

                GL.Color3(0.0f, 0.0f, 1.0f);
                GL.Vertex3(0.0f, 0.0f, 6.0f);
                GL.Vertex3(0.0f, 0.0f, 0.0f);
            }
            GL.End();

            //GL.Begin(PrimitiveType.Triangles);

            //GL.Color3(0.0f, 1.0f, 0.0f);
            //GL.Vertex3(-1.0f, -1.0f, 0);


            //GL.Color3(0.0f, 1.0f, 0.0f);
            //GL.Vertex3(1.0f, -1.0f, 0);


            //GL.Color3(0.0f, 1.0f, 0.0f);
            //GL.Vertex3(0.0f, 1.0f, 0);

            //GL.End();
        }
    }
}
