using Microsoft.Extensions.Logging;
using OpenTK.Windowing.Common;

namespace DemoOpenTK    
{
    public class GameObjectsFactory
    {
        private readonly LinkedList<BaseGameObject> _gameObjects;
        private readonly LinkedList<GraphicObject> _graphicObjects;
        private readonly GraphicObjectsData _data;

        private readonly ILoggerFactory? _loggerFactory;
        private readonly ILogger<GameObjectsFactory>? _logger;

        public GameObjectsFactory(GraphicObjectsData data, ILoggerFactory? loggerFactory = null)
        {
            _data = data;
            _gameObjects = new LinkedList<BaseGameObject>();
            _graphicObjects = new LinkedList<GraphicObject>();

            _loggerFactory = loggerFactory;
            _logger = _loggerFactory?.CreateLogger<GameObjectsFactory>();
        }



        public void RenderGraphicObjects(in FrameEventArgs args)
        {
            foreach (GraphicObject entity in _graphicObjects)
            {
                entity.OnRenderFrame(in args);
            }
        }


        public void UpdateGraphicObjects(in FrameEventArgs args)
        {
            foreach (GraphicObject entity in _graphicObjects)
            {
                entity.OnUpdateFrame(in args);
            }
        }

        public BaseGameObject Create(GameObjectType type,  float positionX, float positionZ, float positionY = 0.0f)
        {
            if (!_data.Objects.TryGetValue(type, out GraphicObjectData? graphicData))
                throw new ArgumentException(nameof(type));

            MeshGraphicObject graphicObject = new(graphicData.Material, graphicData.Mesh);
            graphicObject.MoveTo(positionX - 10, positionY, positionZ - 10 );
            BaseGameObject gameObject = new(graphicObject, _loggerFactory?.CreateLogger<BaseGameObject>());

            _graphicObjects.AddLast(graphicObject);
            _gameObjects.AddLast(gameObject);

            return gameObject;
        }
    }
}
