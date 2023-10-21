using Microsoft.Extensions.Logging;
using System.Collections.Immutable;
using System.Runtime.Serialization;
using System.Text.Json;

namespace DemoOpenTK
{
    public class GraphicObjectsData
    {
        public readonly IImmutableDictionary<GameObjectType, GraphicObjectData> Objects;

        public GraphicObjectsData(IImmutableDictionary<GameObjectType, GraphicObjectData> objects)
        {
            Objects = objects;

        }


        public static GraphicObjectsData LoadFromJsonFile(string pathToFile, ILoggerFactory? loggerFactory = null)
        {
            ILogger<GraphicObjectsData>? logger = loggerFactory?.CreateLogger<GraphicObjectsData>();

            using FileStream stream = File.OpenRead(pathToFile);
            GraphicObjectsDataModel model = JsonSerializer.Deserialize<GraphicObjectsDataModel>(stream)
                ?? throw new SerializationException(pathToFile);

            Dictionary<GameObjectType, GraphicObjectData> objects = new();
            BaseMaterial[] materials = new BaseMaterial[model.Materials.Count()];

            int index = 0;
            foreach (MaterialDataModel materialData in model.Materials)
            {
                BaseMaterial material = new(materialData);
                materials[index] = material;
                index++;
            }

            foreach (KeyValuePair<string,GraphicObjectDataModel> objectTypeData in model.Objects)
            {
                GameObjectType objectType = Enum.Parse<GameObjectType>(objectTypeData.Key, true);
                GraphicObjectDataModel objectModel = objectTypeData.Value;

                MeshBuilder builder = new MeshBuilder(loggerFactory?.CreateLogger<MeshBuilder>());
                builder.LoadFromFile(objectModel.PathToMesh);

                BaseMaterial material = materials[objectModel.MaterialIndex];

                GraphicObjectData objectData = new(builder.Build(), material);
                objects.Add(objectType, objectData);
            }

            return new(objects.ToImmutableDictionary());
        }
    }
}
