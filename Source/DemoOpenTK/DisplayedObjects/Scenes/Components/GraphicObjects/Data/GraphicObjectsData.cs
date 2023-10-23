using Microsoft.Extensions.Logging;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text.Json;

namespace DemoOpenTK
{
    public class GraphicObjectsData
    {
        private readonly string _pathToFile;
        private readonly ILoggerFactory? _loggerFactory;
        private readonly ILogger<GraphicObjectsData>? _logger;

        public GraphicObjectsData(string pathToFile, ILoggerFactory? loggerFactory = null)
        {
            _pathToFile = pathToFile;
            _loggerFactory = loggerFactory;
            _logger = loggerFactory?.CreateLogger<GraphicObjectsData>();
            Objects = ImmutableDictionary<GameObjectType, GraphicObjectData>.Empty;
        }
       
        public IImmutableDictionary<GameObjectType, GraphicObjectData> Objects { get; private set; }

        public void Load()
        {
            _logger?.LogDebug("Загрузка информации о графических объектах");
            Stopwatch stopwatch = Stopwatch.StartNew();

            LoadPrivate();

            stopwatch.Stop();
            _logger?.LogDebug($"Загрузка завершена. Затраченное время {stopwatch.Elapsed.TotalMilliseconds} ms.");
        }

        private void LoadPrivate()
        {
            using FileStream stream = File.OpenRead(_pathToFile);
            GraphicObjectsDataModel model = JsonSerializer.Deserialize<GraphicObjectsDataModel>(stream)
                ?? throw new SerializationException(_pathToFile);

            Dictionary<GameObjectType, GraphicObjectData> objects = new();
            BaseMaterial[] materials = new BaseMaterial[model.Materials.Count()];

            int index = 0;
            foreach (MaterialDataModel materialData in model.Materials)
            {
                BaseMaterial material = new(materialData);
                materials[index] = material;
                index++;
            }

            foreach (KeyValuePair<string, GraphicObjectDataModel> objectTypeData in model.Objects)
            {
                GameObjectType objectType = Enum.Parse<GameObjectType>(objectTypeData.Key, true);
                GraphicObjectDataModel objectModel = objectTypeData.Value;

                MeshBuilder builder = new MeshBuilder(_loggerFactory?.CreateLogger<MeshBuilder>());
                builder
                    .UseEBO()
                    .LoadFromFile(objectModel.PathToMesh);

                BaseMaterial material = materials[objectModel.MaterialIndex];

                GraphicObjectData objectData = new(builder.Build(), material);
                objects.Add(objectType, objectData);
            }
            Objects = objects.ToImmutableDictionary();
        }
    }
}
