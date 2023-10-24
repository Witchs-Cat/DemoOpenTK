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

        private readonly ILoggerFactory? _loggerFactory;
        private readonly ILogger? _logger;

        public GameObjectsFactory(GraphicObjectsData data, ILoggerFactory? loggerFactory = null)
        {
            _data = data;
            _gameObjects = new LinkedList<BaseGameObject>();
            _graphicObjects = new LinkedList<GraphicObject>();

            _loggerFactory = loggerFactory;
            _logger = _loggerFactory?.CreateLogger<GameObjectsFactory>();
        }


        public void OnLoad()
        {
            _data.Load();
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

        public void UpdateGameObjects(in FrameEventArgs args)
        {
            foreach (BaseGameObject entity in _gameObjects)
            {
                entity.OnUpdateFrame(in args);
            }
        }

        public void OnUpdateFrame(in FrameEventArgs args)
        {
            UpdateGameObjects(in args);
            UpdateGraphicObjects(in args);
        }

        public void OnRenderFrame(in FrameEventArgs args)
        {
            RenderGraphicObjects(in args);
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
            switch (type)
            {
                case GameObjectType.Player: 
                    gameObject = new Player(graphicObject, field, position, _loggerFactory?.CreateLogger<Player>());
                    break;
                case GameObjectType.LightObject:
                    gameObject = new LightObject(graphicObject, field, position, _loggerFactory?.CreateLogger<LightObject>());
                    break;
                default:
                    gameObject = new BaseGameObject(graphicObject, field, position, _loggerFactory?.CreateLogger<BaseGameObject>());
                    break;
            }

            _graphicObjects.AddLast(graphicObject);
            _gameObjects.AddLast(gameObject);

            return gameObject;
        }

        public void OnKeyDown(KeyboardKeyEventArgs e)
        {
            //foreach (Player player in _gameObjects.Where(x => x is Player).Cast<Player>())
            //{
            //    player.OnKeyDown(e);
            //}
        }
    }
}
