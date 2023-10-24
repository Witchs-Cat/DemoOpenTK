using Microsoft.Extensions.Logging;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System.Xml.Serialization;

namespace DemoOpenTK    
{
    public class GameObjectsFactory
    {
        private readonly LinkedList<BaseGameObject> _gameObjects;
        private readonly LinkedList<GraphicObject> _graphicObjects;
        private readonly GraphicObjectsData _data;
        private readonly GameScene _scene;

        private readonly ILoggerFactory? _loggerFactory;
        private readonly ILogger? _logger;

        public GameObjectsFactory(GameScene scene, GraphicObjectsData data, ILoggerFactory? loggerFactory = null)
        {
            _data = data;
            _scene = scene;
            _gameObjects = new LinkedList<BaseGameObject>();
            _graphicObjects = new LinkedList<GraphicObject>();

            _loggerFactory = loggerFactory;
            _logger = _loggerFactory?.CreateLogger<GameObjectsFactory>();

            _scene.Load += OnLoad;
            _scene.RenderFrame += OnRenderFrame;
            _scene.UpdateFrame += OnUpdateFrame;
        }


        public void OnLoad()
        {
            _data.Load();
        }

        private void RenderGraphicObjects(FrameEventArgs args)
        {
            foreach (GraphicObject entity in _graphicObjects)
            {
                entity.OnRenderFrame(args);
            }
        }

        private void UpdateGraphicObjects(FrameEventArgs args)
        {
            foreach (GraphicObject entity in _graphicObjects)
            {
                entity.OnUpdateFrame(args);
            }
        }

        private void UpdateGameObjects(FrameEventArgs args)
        {
            foreach (BaseGameObject entity in _gameObjects.Where(x => x is MovedGameObject))
            {
                entity.OnUpdateFrame(args);
            }
        }

        private void OnUpdateFrame(FrameEventArgs args)
        {
            UpdateGameObjects(args);
            //UpdateGraphicObjects(args);
        }

        private void OnRenderFrame(FrameEventArgs args)
        {
            RenderGraphicObjects(args);
        }

        public BaseGameObject Create(GameField field, GameObjectType type,  int positionX, int positionZ, float positionY = 0.0f)
        {
            if (!_data.Objects.TryGetValue(type, out GraphicObjectData? graphicData))
                throw new ArgumentException(null, nameof(type));

            MeshGraphicObject graphicObject = new(graphicData.Material, graphicData.Mesh);
            Vector3 graphicPosition = new(positionX, positionY, positionZ);
            graphicObject.MoveTo(graphicPosition);


            BaseGameObject gameObject;
            Vector2i position = new(positionX, positionZ);
            GameObjectConfig config = new(_scene, graphicObject, field, position, null);

            switch (type)
            {
                case GameObjectType.Player:
                    config.Logger = _loggerFactory?.CreateLogger<Player>();
                    gameObject = new Player(config);
                    break;
                case GameObjectType.LightObject:
                    config.Logger = _loggerFactory?.CreateLogger<LightObject>();
                    gameObject = new LightObject(config);
                    break;
                default:
                    config.Logger = _loggerFactory?.CreateLogger<BaseGameObject>();
                    gameObject = new BaseGameObject(config);
                    break;
            }

            _graphicObjects.AddLast(graphicObject);
            _gameObjects.AddLast(gameObject);

            return gameObject;
        }
    }
}
