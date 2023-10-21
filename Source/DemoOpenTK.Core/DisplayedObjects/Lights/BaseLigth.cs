using Microsoft.Extensions.Logging;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace DemoOpenTK.DisplayedObjects.Lights
{
    public class BaseLight : IDisplayedObject
    {
        //Номер источника света
        public readonly LightName LightNumber;
        // позиция источника света
        public Vector4 Position;
        // фоновая составляющая источника света
        public Vector4 Ambient;
        // диффузная составляющая
        public Vector4 Diffuse;        
        // зеркальная составляющая
        public Vector4 Specular;

        protected ILogger<BaseLight> _logger;

        public BaseLight(LightName lightNumber)
        {
            LightNumber = lightNumber;
        }

        public ILoggerFactory LoggerFactory { get; }

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
