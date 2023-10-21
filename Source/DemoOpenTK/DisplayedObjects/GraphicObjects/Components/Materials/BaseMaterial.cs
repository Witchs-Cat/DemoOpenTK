using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Runtime.Serialization;
using System.Text.Json;

namespace DemoOpenTK
{
    public class BaseMaterial
    {

        public BaseMaterial(MaterialDataModel model)
        {
            Ambient = new(model.Ambient[0], model.Ambient[1], model.Ambient[2], model.Ambient[3]);
            Diffuse = new(model.Diffuse[0], model.Diffuse[1], model.Diffuse[2], model.Diffuse[3]);
            Specular = new(model.Specular[0], model.Specular[1], model.Specular[2], model.Specular[3]);
            Emission = new(model.Emission[0], model.Emission[1], model.Emission[2], model.Emission[3]);
            Face = Enum.Parse<MaterialFace>(model.Face);
        }

        // фоновая составляющая
        public Vector4 Ambient { get; protected set; }
        // диффузная составляющая
        public Vector4 Diffuse { get; protected set; }
        // зеркальная составляющая
        public Vector4 Specular { get; protected set; }
        // самосвечение
        public Vector4 Emission { get; protected set; }
        // степень отполированности
        public float Shininess { get; protected set; }

        public MaterialFace Face { get; protected set; }

        public virtual void Apply()
        {
            GL.Material(Face, MaterialParameter.Ambient, Ambient);
            GL.Material(Face, MaterialParameter.Diffuse, Diffuse);
            GL.Material(Face, MaterialParameter.Specular, Specular);
            GL.Material(Face, MaterialParameter.Emission, Emission);
            GL.Material(Face, MaterialParameter.Shininess, Shininess);
        }

        public static BaseMaterial LoadFromJsonFile(string pathToFile)
        {
            using FileStream stream = File.OpenRead(pathToFile);
            MaterialDataModel model = JsonSerializer.Deserialize<MaterialDataModel>(stream)
                ?? throw new SerializationException(pathToFile);
            return new BaseMaterial(model);
        }
    }
}
