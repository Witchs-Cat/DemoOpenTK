using DemoOpenTK.DisplayedObjects.Scenes.Components.GraphicObjects;
using Microsoft.Extensions.Logging;
using OpenTK.Graphics.ES20;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace DemoOpenTK
{
    public class GraphicObjectsFactory
    {
        private readonly Queue<BaseGraphicObject> _addQueue;
        private readonly Queue<BaseGraphicObject> _deleteQueue;

        private readonly LinkedList<BaseGraphicObject> _graphicObjects;

        private readonly GraphicObjectsData _data;
        private readonly GameScene _gameScene;
        private readonly ILoggerFactory _loggerFactory;

        public GraphicObjectsFactory(GameScene gameScene, GraphicObjectsData data, ILoggerFactory loggerFactory) 
        {
            _addQueue = new();
            _deleteQueue = new();

            _graphicObjects = new();

            _gameScene = gameScene;
            _data = data;
            _loggerFactory = loggerFactory;

            _gameScene.Load += _data.Load;
            _gameScene.RenderFrame += OnRenderFrame;

        }

        private void RenderGameObjects(FrameEventArgs args)
        {
            foreach(BaseGraphicObject graphicObject in _graphicObjects)
                graphicObject.OnRenderFrame(args);
        }

        private void OnRenderFrame(FrameEventArgs args)
        {
            while (_deleteQueue.Any())
                _graphicObjects.Remove(_deleteQueue.Dequeue());

            while (_addQueue.Any())
                _graphicObjects.AddLast(_addQueue.Dequeue());

            RenderGameObjects(args);
        }
 
        public void AddToDeleteQueue(BaseGraphicObject graphicObject)
        {
            _deleteQueue.Enqueue(graphicObject);
        }


        public BaseGraphicObject Create(GraphicObjectType type, int positionX, float positionY, int positionZ)
        {
            if (!_data.Objects.TryGetValue((GraphicObjectType)type, out GraphicObjectData? graphicData))
                throw new ArgumentException(null, nameof(type));

            BaseGraphicObject graphicObject;

            if (graphicData.Texture == null)
                graphicObject = new MeshGraphicObject(graphicData.Material, graphicData.Mesh);
            else if (type != GraphicObjectType.BombHoleDecal)
                graphicObject = new TextureGraphicObject(graphicData.Material, graphicData.Mesh, graphicData.Texture, _data.TexturesFilter);
            else
                graphicObject = new Decal(graphicData.Material, graphicData.Mesh, graphicData.Texture, _data.TexturesFilter);

            Vector3 graphicPosition = new(positionX, positionY, positionZ);
            graphicObject.MoveTo(graphicPosition);

            _addQueue.Enqueue(graphicObject);

            return graphicObject;
        }
    }
}
