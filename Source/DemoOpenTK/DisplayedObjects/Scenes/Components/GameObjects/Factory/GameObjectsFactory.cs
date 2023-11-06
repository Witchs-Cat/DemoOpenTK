using Microsoft.Extensions.Logging;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System.Xml.Serialization;

namespace DemoOpenTK    
{
    public class GameObjectsFactory
    {
        private readonly Queue<BaseGameObject> _addQueue;
        private readonly Queue<BaseGameObject> _deleteQueue;

        private readonly LinkedList<BaseGameObject> _gameObjects;

        private readonly GraphicObjectsData _data;
        private readonly GameScene _scene;

        private readonly ILoggerFactory? _loggerFactory;
        private readonly ILogger? _logger;

        public GameObjectsFactory(GameScene scene, GraphicObjectsData data, ILoggerFactory? loggerFactory = null)
        {
            _data = data;
            _scene = scene;
            _addQueue = new();
            _gameObjects = new();
            _deleteQueue = new();

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


        private void UpdateGameObjects(FrameEventArgs args)
        {
            foreach (BaseGameObject entity in _gameObjects.Where(x => x is AnimatedGameObject))
            {
                entity.OnUpdateFrame(args);
                entity.GraphicObject.OnUpdateFrame(args);
            }
        }

        private void OnUpdateFrame(FrameEventArgs args)
        {
            UpdateGameObjects(args);
            //UpdateGraphicObjects(args);

            while (_deleteQueue.Any())
               _gameObjects.Remove(_deleteQueue.Dequeue());

            while (_addQueue.Any())
                _gameObjects.AddLast(_addQueue.Dequeue());
        }

        private void OnRenderFrame(FrameEventArgs args)
        {
            foreach (BaseGameObject entity in _gameObjects)
            {
                entity.GraphicObject.OnRenderFrame(args);
            }
        }

        public void AddToDeleteQueue(BaseGameObject gameObject)
        {
            _deleteQueue.Enqueue(gameObject);
        }

        public BaseGameObject Create(GameField field, GameObjectType type,  int positionX, int positionZ, float positionY = 0.0f)
        {
            if (!_data.Objects.TryGetValue(type, out GraphicObjectData? graphicData))
                throw new ArgumentException(null, nameof(type));

            BaseGraphicObject graphicObject;

            if (graphicData.Texture == null)
                graphicObject = new MeshGraphicObject(graphicData.Material, graphicData.Mesh);
            else
                graphicObject = new TextureGraphicObject(graphicData.Material, graphicData.Mesh, graphicData.Texture, _data.TexturesFilter);

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
                case GameObjectType.Monster:
                    config.Logger = _loggerFactory?.CreateLogger<Monster>();
                    gameObject = new Monster(config);
                    break;
                case GameObjectType.Bomb:
                    config.Logger = _loggerFactory?.CreateLogger<Bomb>();
                    gameObject = new Bomb(config);
                    break;
                default:
                    config.Logger = _loggerFactory?.CreateLogger<BaseGameObject>();
                    gameObject = new BaseGameObject(config);
                    break;
            }
            _addQueue.Enqueue(gameObject);

            return gameObject;
        }
    }
}
