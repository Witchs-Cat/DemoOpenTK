using System.Text.Json.Serialization;

namespace DemoOpenTK
{
    public class GraphicObjectsDataModel
    {
        public GraphicObjectsDataModel(IDictionary<string, GraphicObjectDataModel> objects, 
            IEnumerable<MaterialDataModel> materials)
        {
            Objects = objects;
            Materials = materials;
        }
        [JsonPropertyName("objects")]
        public IDictionary<string, GraphicObjectDataModel> Objects { get; }
        [JsonPropertyName("materials")]
        public IEnumerable<MaterialDataModel> Materials { get; }
    }
}
