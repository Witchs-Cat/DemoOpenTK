using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace DemoOpenTK.DisplayedObjects.Lights
{
    internal class BaseLight : IDisplayedObject
    {
        public BaseLight(LightName lightNumber) 
        {
            LightNumber = lightNumber;
        }

        public LightName LightNumber { get; }
        // позиция источника света
        public Vector4 Position { get; protected set; }
        // фоновая составляющая источника света
        public Vector4 Ambient { get; protected set; }
        // диффузная составляющая
        public Vector4 Diffuse { get; protected set; }
        // зеркальная составляющая
        public Vector4 Specular { get; protected set; }

        public virtual void OnRenderFrame(in FrameEventArgs args)
        {
            GL.Light(LightNumber, LightParameter.Ambient, Ambient);
            GL.Light(LightNumber, LightParameter.Diffuse, Diffuse);
            GL.Light(LightNumber, LightParameter.Specular, Specular);
            GL.Light(LightNumber, LightParameter.Position, Position);
        }

        public virtual void OnUpdateFrame(in FrameEventArgs args)
        {
        }

        public virtual void Enable()
        {
            GL.Enable(Enum.Parse<EnableCap>(LightNumber.ToString()));
        }
    }
}
