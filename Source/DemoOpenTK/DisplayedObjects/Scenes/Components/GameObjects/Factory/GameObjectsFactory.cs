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

        private readonly GraphicObjectsFactory _graphicFactory;
        private readonly GameScene _scene;

        private readonly ILoggerFactory? _loggerFactory;
        private readonly ILogger? _logger;

        public GameObjectsFactory(GameScene scene, GraphicObjectsFactory graphicFactory, ILoggerFactory? loggerFactory = null)
        {
            _addQueue = new();
            _gameObjects = new();
            _deleteQueue = new();

            _graphicFactory = graphicFactory;
            _scene = scene;


            _loggerFactory = loggerFactory;
            _logger = _loggerFactory?.CreateLogger<GameObjectsFactory>();

            _scene.UpdateFrame += OnUpdateFrame;
        }

        public GraphicObjectsFactory GraphicFactory => _graphicFactory;

        private void UpdateGameObjects(FrameEventArgs args)
        {
            foreach (BaseGameObject entity in _gameObjects.Where(x => x is AnimatedGameObject))
            {
                entity.OnUpdateFrame(args);
                entity.GraphicObject.OnRenderFrame(args);
            }
        }

        private void OnUpdateFrame(FrameEventArgs args)
        {
            while (_deleteQueue.Any())
               _gameObjects.Remove(_deleteQueue.Dequeue());

            while (_addQueue.Any())
                _gameObjects.AddLast(_addQueue.Dequeue());

            UpdateGameObjects(args);
            //UpdateGraphicObjects(args);
        }

        public void AddToDeleteQueue(BaseGameObject gameObject)
        {
            _deleteQueue.Enqueue(gameObject);
        }

        public BaseGameObject Create(GameField field, GameObjectType type,  int positionX, int positionZ, float positionY = 0.0f)
        { 
            BaseGraphicObject graphicObject = _graphicFactory.Create((GraphicObjectType)type, positionX, positionY, positionZ);

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
