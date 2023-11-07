using Microsoft.Extensions.Logging;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace DemoOpenTK
{
    public class BaseGameObject
    {
        private Vector2i _position;

        public readonly BaseGraphicObject GraphicObject;
        public readonly GameField Field; 
        
        public BaseGameObject(GameObjectConfig config)
        {
            _position = config.Position;
            Scene = config.Scene;
            Logger = config.Logger;
            GraphicObject = config.GraphicObject;
            Field = config.Field;
        }

        public GameScene Scene { get; }
        public ILogger? Logger { get; }

        public Vector2i Position 
        {
            get => _position;
            protected set {
                Vector2i prevPosition = _position;
                _position = value;
                Field.OnObjectMove(prevPosition, this);
                Logger?.LogTrace($"Новая позиция {value}");
            }
        }

        public virtual void OnUpdateFrame(FrameEventArgs args)
        { }

        public virtual bool TryRemove()
        {
            //Field.Remove(this);
            return false;
        }
    }
}
