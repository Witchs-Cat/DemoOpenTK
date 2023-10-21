using Microsoft.Extensions.Logging;

namespace DemoOpenTK
{
    public class BaseGameObject
    {
        private readonly GraphicObject _graphicObject;
        private readonly ILogger? _logger;

        public BaseGameObject(GraphicObject graphicObject, ILogger<BaseGameObject>? logger = null)
        {
            _graphicObject = graphicObject;
            _logger = logger;
        }
    }
}
