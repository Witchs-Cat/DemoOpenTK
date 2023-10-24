using Microsoft.Extensions.Logging;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace DemoOpenTK
{
    public class Player : MovedGameObject
    {
        public Player(GraphicObject graphicObject, GameField field, Vector2i position, ILogger? logger = null) 
            : base(graphicObject, field, position, logger)
        {
        }

        public override void OnUpdateFrame(in FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            
            if (Field.KeyboardState.IsKeyDown(Keys.W))
            {
                TryMove(new Vector2i(-1,0));
            }
            if (Field.KeyboardState.IsKeyDown(Keys.S))
            {
                TryMove(new Vector2i(1, 0));
            }
            if (Field.KeyboardState.IsKeyDown(Keys.D))
            {
                TryMove(new Vector2i(0, -1));
            }
            if (Field.KeyboardState.IsKeyDown(Keys.A))
            {
                TryMove(new Vector2i(0, 1));
            }

        }

        ///<inheritdoc/>
        public override bool TryMove(Vector2i shift)
        {
            if (State != MovedObjectState.Inactive)
            {
                return false;
            }


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
            _animationCurrentPosition = graphicPosition;
            _animationEndPosition = new Vector3(newPostion.X, graphicPosition.Y, newPostion.Y);

            State = MovedObjectState.Animated;

            _logger?.LogDebug($"Player moveTo {newPostion}");
            return true;

        }
    }
}
