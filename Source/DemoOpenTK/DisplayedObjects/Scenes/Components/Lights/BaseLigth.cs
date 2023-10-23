using Microsoft.Extensions.Logging;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System.Runtime.InteropServices;

namespace DemoOpenTK
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

        protected ILogger<BaseLight>? _logger;

        public BaseLight(LightName lightNumber, ILogger<BaseLight>? logger = null)
        {
            LightNumber = lightNumber;
            _logger = logger;
        }

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
            // устанавливаем общую фоновую освещенность
            float[] globalAmbientColor = { 0.2f, 0.2f, 0.2f, 1.0f };
            GL.LightModel(LightModelParameter.LightModelAmbient, globalAmbientColor);

            GL.Enable(EnableCap.Lighting);
            GL.Enable(Enum.Parse<EnableCap>(LightNumber.ToString()));
        }
    }
}
