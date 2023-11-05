using System.Text.Json.Serialization;

namespace DemoOpenTK
{
    public class GraphicObjectsDataModel
    {
        public GraphicObjectsDataModel(string texturesFilterType, IDictionary<string, GraphicObjectDataModel> objects, 
            IEnumerable<MaterialDataModel> materials)
        {
            TexturesFilterType = texturesFilterType;
            Objects = objects;
            Materials = materials;
        }

        [JsonPropertyName("filterType")]
        public string TexturesFilterType { get; }

        [JsonPropertyName("objects")]
        public IDictionary<string, GraphicObjectDataModel> Objects { get; }

        [JsonPropertyName("materials")]
        public IEnumerable<MaterialDataModel> Materials { get; }
    }
}
