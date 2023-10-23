using Microsoft.Extensions.Logging;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace DemoOpenTK
{
    public class Player : BaseGameObject, IMovable
    {
        public Player(GraphicObject graphicObject, GameField field, ILogger<BaseGameObject>? logger = null) : base(graphicObject, field, logger)
        {
        }

        public void OnKeyDown(KeyboardKeyEventArgs args)
        {
            Keys selectedKeys = args.Key;

            if (selectedKeys.HasFlag(Keys.W))
            {
                TryMove(new Vector2i(1,0));
            }
            else if (selectedKeys.HasFlag(Keys.S))
            {
                TryMove(new Vector2i(-1, 0));
            }
            else if (selectedKeys.HasFlag(Keys.D))
            {
                TryMove(new Vector2i(0, -1));
            }
            else if (selectedKeys.HasFlag(Keys.A))
            {
                TryMove(new Vector2i(0, 1));
            }
        }

        public bool TryMove(Vector2i positon)
        {
            throw new NotImplementedException();
        }
    }
}
