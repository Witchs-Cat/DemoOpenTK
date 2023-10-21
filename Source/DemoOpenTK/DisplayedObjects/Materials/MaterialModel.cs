using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DemoOpenTK.DisplayedObjects.Materials
{
    public class MaterialModel
    {
        // фоновая составляющая
        [JsonPropertyName("ambient")]
        public float[] Ambient { get; set; }
        // диффузная составляющая
        [JsonPropertyName("diffuse")]
        public float[] Diffuse { get; set; }
        // зеркальная составляющая
        [JsonPropertyName("specular")]
        public float[] Specular { get; set; }
        // самосвечение
        [JsonPropertyName("emission")]
        public float[] Emission { get; set; }
        // степень отполированности
        [JsonPropertyName("shininess")]
        public float Shininess { get; set; }
        [JsonPropertyName("face")]
        public string Face { get; set; }

        public MaterialModel(float[] ambient, float[] diffuse, float[] specular, float[] emission, float shininess, string face)
        {
            Ambient = ambient;
            Diffuse = diffuse;
            Specular = specular;
            Emission = emission;
            Shininess = shininess;
            Face = face;
        }
    }
}
