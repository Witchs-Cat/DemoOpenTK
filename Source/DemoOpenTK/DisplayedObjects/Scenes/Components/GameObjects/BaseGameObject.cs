using Microsoft.Extensions.Logging;

namespace DemoOpenTK
{
    public class BaseGameObject
    {
        internal readonly ILogger? _logger;

        public readonly GraphicObject GraphicObject;
        public readonly GameField Field; 
        
        public BaseGameObject(GraphicObject graphicObject, GameField field, ILogger<BaseGameObject>? logger = null)
        {
            _logger = logger;

            GraphicObject = graphicObject;
            Field = field;
        }

    }
}
