using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace DemoOpenTK
{
    public class Monster : AnimatedGameObject
    {
        private readonly Random _random;

        public Monster(GameObjectConfig config) : base(config)
        {
            _random = new Random();
        }

        public override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            if (AnimationsQueue.Any())
                return;

            int vectorDirection = (_random.Next(0, 2) == 1)? 1 : -1 ;

            int x = _random.Next(0, 2);
            int y = (x == 1) ? 0 : 1;

            Vector2i moveVector = new(x, y);
            moveVector *= vectorDirection;

            TryMove(moveVector);
        }

        public bool TryMove(Vector2i shift)
        {
            Vector2i newPostion = Position + shift;

            if (Field.TryGetObstacle(newPostion, out BaseGameObject? obstacle))
            {
                if (obstacle is Bomb bomb)
                {
                    bomb.Boom();
                    return true;
                }

                if (obstacle is not Player player)
                    return false;

                player.TryRemove();
            }

            Position = newPostion;
            Vector3 graphicPosition = GraphicObject.Position;
            MoveAnimation moveAnimation = new(this.GraphicObject, graphicPosition, new Vector3(newPostion.X, graphicPosition.Y, newPostion.Y));
           
            AnimationsQueue.Enqueue(moveAnimation);

            return true;
        }
    }
}
