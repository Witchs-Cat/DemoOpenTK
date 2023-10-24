using Microsoft.Extensions.Logging;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoOpenTK
{
    internal class LightObject : MovedGameObject
    {
        public LightObject(GraphicObject graphicObject, GameField field, 
            Vector2i position, ILogger? logger = null) 
            : base(graphicObject, field, position, logger)
        {
        }
    }
}
