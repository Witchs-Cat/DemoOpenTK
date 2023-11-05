using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DemoOpenTK
{
    public class GraphicObjectDataModel
    {
        public GraphicObjectDataModel(string pathToMesh, string pathToTexture, uint materialIndex) 
        {  
            PathToMesh = pathToMesh;
            PathToTexture = pathToTexture;
            MaterialIndex = materialIndex; 
        }

        [JsonPropertyName("mesh")]
        public string PathToMesh { get; }
        [JsonPropertyName("texture")]
        public string PathToTexture { get; }
        [JsonPropertyName("materialIndex")]
        public uint MaterialIndex { get; }
    }
}
