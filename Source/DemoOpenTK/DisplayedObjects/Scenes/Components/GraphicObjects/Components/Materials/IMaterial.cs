
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace DemoOpenTK
{
    public interface IMaterial : IGraphicObjectComponent
    {
        /// <summary>
        /// фоновая составляющая
        /// </summary>
        Vector4 Ambient { get; }
        /// <summary>
        /// диффузная составляющая
        /// </summary>
        Vector4 Diffuse { get; }
        /// <summary>
        /// зеркальная составляющая
        /// </summary>
        Vector4 Specular { get; }
        /// <summary>
        /// самосвечение
        /// </summary>
        Vector4 Emission { get; }
        /// <summary>
        /// степень отполированности
        /// </summary>
        float Shininess { get; }

        MaterialFace Face { get; }
    }
}
