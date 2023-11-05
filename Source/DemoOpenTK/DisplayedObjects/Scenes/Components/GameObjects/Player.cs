using Microsoft.Extensions.Logging;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace DemoOpenTK
{
    public class Player : AnimatedGameObject
    {
        private readonly KeyboardState _keyboardState;
        public Player(GameObjectConfig config): base(config)
        {
            _keyboardState = Scene.KeyboardState;
        }

        public override void OnUpdateFrame( FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            if (AnimationsQueue.Any())
                return;

            if (_keyboardState.WasKeyDown(Keys.W))
                TryMove(new Vector2i(-1,0));
            else if (_keyboardState.WasKeyDown(Keys.S))
                TryMove(new Vector2i(1, 0));
            else if (_keyboardState.WasKeyDown(Keys.D))
                TryMove(new Vector2i(0, -1));
            else if (_keyboardState.WasKeyDown(Keys.A))
                TryMove(new Vector2i(0, 1));
        }

        ///<inheritdoc/>
        private bool TryMove(Vector2i shift)
        {
            Vector2i newPostion = Position + shift;
            if (Field.Layout.TryGetValue(newPostion, out BaseGameObject? obstacle))
            {
                if (obstacle is not IMovable movedObstacle)
                    return false;

                if (!movedObstacle.TryMove(shift))
                    return false;
            }

            Field.OnObjectMove(newPostion, Position, this);
            Position = newPostion;

            Vector3 graphicPosition = GraphicObject.Position;
            MoveAnimation moveAnimation = new(this.GraphicObject, graphicPosition, new Vector3(newPostion.X, graphicPosition.Y, newPostion.Y));
            AnimationsQueue.Enqueue(moveAnimation);
            Logger?.LogDebug($"Игрок переместился на позицию {newPostion}");
            return true;
        }
    }
}
