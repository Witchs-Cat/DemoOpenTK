using Microsoft.Extensions.Logging;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoOpenTK.DisplayedObjects.Scenes.Components.GameObjects
{
    internal class LightObject : BaseGameObject, IMovable
    {
        public LightObject(GraphicObject graphicObject, GameField field, ILogger<BaseGameObject>? logger = null) : base(graphicObject, field, logger)
        {
        }

        public bool TryMove(Vector2i positon)
        {
            throw new NotImplementedException();
        }
    }
}
