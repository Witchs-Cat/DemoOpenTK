using Microsoft.Extensions.Logging;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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

            if (!AnimationsQueue.Any())
            {
                int randomNumber = _random.Next(0, 2);

                if (randomNumber == 0b1)
                {
                    AnimationsQueue.Enqueue(new InactivityAnimation(this.GraphicObject, liveTimeSeconds:0.1d));
                }
                else
                {
                    randomNumber = (_random.Next(0, 2) == 1)? 1 : -1 ;
                    int x = _random.Next(0, 2);
                    int y = (x == 1) ? 0 : 1;

                    Vector2i moveVector = new Vector2i(x, y);
                    moveVector *= randomNumber;

                    TryMove(moveVector);
                }
            }

        }

        private bool TryMove(Vector2i shift)
        {
            Vector2i newPostion = Position + shift;
            if (Field.Layout.TryGetValue(newPostion, out BaseGameObject? obstacle))
            {
                if (obstacle is not Player player)
                    return false;

                player.Remove();
            }

            Field.OnObjectMove(newPostion, Position, this);
            Position = newPostion;

            Vector3 graphicPosition = GraphicObject.Position;
            MoveAnimation moveAnimation = new(this.GraphicObject, graphicPosition, new Vector3(newPostion.X, graphicPosition.Y, newPostion.Y));
            AnimationsQueue.Enqueue(moveAnimation);
            Logger?.LogDebug($"Монстр переместился на позицию {newPostion}");
            return true;
        }
    }
}
