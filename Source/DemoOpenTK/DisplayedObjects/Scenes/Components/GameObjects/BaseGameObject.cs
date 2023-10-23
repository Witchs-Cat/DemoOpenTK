using Microsoft.Extensions.Logging;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace DemoOpenTK
{
    public class BaseGameObject
    {
        internal readonly ILogger? _logger;

        public readonly GraphicObject GraphicObject;
        public readonly GameField Field; 
        
        public BaseGameObject(GraphicObject graphicObject, GameField field, Vector2i position, ILogger<BaseGameObject>? logger = null)
        {
            _logger = logger;

            Position = position;
            GraphicObject = graphicObject;
            Field = field;
        }

        public Vector2i Position { get; protected set; }

        public virtual void OnUpdateFrame(in FrameEventArgs args)
        { }
    }
}
