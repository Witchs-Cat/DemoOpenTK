using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoOpenTK
{
    public class Bomb : AnimatedGameObject
    {
        public Bomb(GameObjectConfig config) : base(config)
        { }

        public event Action? Detonated;

        public override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

        }

        public void StartTimer(double seconds)
        {
            InactivityAnimation animation = new(this.GraphicObject, seconds);
            AnimationsQueue.Enqueue(animation);

            animation.EndAnimation += Boom;
        }

        public void Boom()
        {
            Vector2i bombPosition = Position;
            for (int i = -1; i < 2; i++)
            {
                for(int j = -1; j < 2; j++)
                {
                    if ((i & j) != 0)
                        continue;

                    for (int k = 1; k < 3; k++)
                    {
                        Vector2i position;
                        position = new Vector2i(bombPosition.X + i * k, bombPosition.Y + j * k);
                        if (Field.TryGetObstacle(position, out BaseGameObject? obstacle))
                        { 
                            obstacle!.TryRemove();
                            break;
                        }   
                    }
                }
            }
            Detonated?.Invoke();
            Field.SetDeacal(Position);
        }
    }
}
