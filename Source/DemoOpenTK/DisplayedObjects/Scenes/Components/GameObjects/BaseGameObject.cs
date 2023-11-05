using Microsoft.Extensions.Logging;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace DemoOpenTK
{
    public class BaseGameObject
    {

        public readonly BaseGraphicObject GraphicObject;
        public readonly GameField Field; 
        
        public BaseGameObject(GameObjectConfig config)
        {
            Scene = config.Scene;
            Logger = config.Logger;
            Position = config.Position;
            GraphicObject = config.GraphicObject;
            Field = config.Field;
        }

        public GameScene Scene { get; }
        public Vector2i Position { get; protected set; }
        public ILogger? Logger { get; }

        public virtual void OnUpdateFrame(FrameEventArgs args)
        { }
    }
}
