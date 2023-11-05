using Microsoft.Extensions.Logging;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace DemoOpenTK
{
    public class Player : MovedGameObject
    {
        private readonly KeyboardState _keyboardState;
        public Player(GameObjectConfig config): base(config)
        {
            _keyboardState = Scene.KeyboardState;
        }

        public override void OnUpdateFrame( FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            if (Animation.State != AnimationState.Inactive)
                return;

            if (_keyboardState.WasKeyDown(Keys.W))
                TryMove(new Vector2i(-1,0));
            if (_keyboardState.WasKeyDown(Keys.S))
                TryMove(new Vector2i(1, 0));
            if (_keyboardState.WasKeyDown(Keys.D))
                TryMove(new Vector2i(0, -1));
            if (_keyboardState.WasKeyDown(Keys.A))
                TryMove(new Vector2i(0, 1));
        }

        ///<inheritdoc/>
        public override bool TryMove(Vector2i shift)
        {
            if (Animation.State != AnimationState.Inactive)
                return false;

            Vector2i newPostion = Position + shift;
            if (Field.Layout.TryGetValue(newPostion, out BaseGameObject? obstacle))
            {
                if (obstacle is not MovedGameObject movedObstacle)
                    return false;

                if (!movedObstacle.TryMove(shift))
                    return false;
            }

            Field.OnObjectMove(newPostion, Position, this);
            Position = newPostion;

            Vector3 graphicPosition = GraphicObject.Position;
            Animation.Play( graphicPosition, new Vector3(newPostion.X, graphicPosition.Y, newPostion.Y));

            Logger?.LogDebug($"Игрок переместился на позицию {newPostion}");
            return true;

        }
    }
}
