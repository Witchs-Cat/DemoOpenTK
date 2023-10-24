using Microsoft.Extensions.Logging;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoOpenTK
{
    public class GameObjectConfig
    {
        public GameObjectConfig(GameScene scene, GraphicObject graphicObject, GameField field, Vector2i position, ILogger? logger = null)
        {
            Scene = scene;
            GraphicObject = graphicObject;
            Field = field;
            Position = position;
            Logger = logger;
        }

        public GameScene Scene { get; set; }
        public GraphicObject GraphicObject { get; set; }
        public GameField Field { get; set; }
        public Vector2i Position { get; set; }
        public ILogger? Logger { get; set; }
    }
}
