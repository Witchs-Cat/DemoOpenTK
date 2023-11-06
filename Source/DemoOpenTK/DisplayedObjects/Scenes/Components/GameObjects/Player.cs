using Microsoft.Extensions.Logging;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Transactions;

namespace DemoOpenTK
{
    public class Player : AnimatedGameObject
    {
        private readonly KeyboardState _keyboardState;
        private readonly List<BaseGameObject> _bombs;
        private readonly int _bombsLimit;

        private bool _setBomb;

        public Player(GameObjectConfig config): base(config)
        {
            _keyboardState = Scene.KeyboardState;
            _bombs = new();
            _bombsLimit = 3;
            _setBomb = false;
        }

        public override void OnUpdateFrame( FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            if (AnimationsQueue.Any())
                return;

            if (_keyboardState.WasKeyDown(Keys.Space))
                _setBomb = true && _bombs.Count < _bombsLimit;

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
            Vector2i prevPosition = Position;

            if (Field.TryGetObstacle(newPostion, out BaseGameObject? obstacle))
            {
                if ( obstacle is Bomb bomb)
                {
                    bomb.Boom();
                    return true;
                }

                if (obstacle is not IMovable movedObstacle)
                    return false;

                if (!movedObstacle.TryMove(shift))
                    return false;
            }
   
            Position = newPostion;

            Vector3 graphicPosition = GraphicObject.Position;
            MoveAnimation moveAnimation = new(this.GraphicObject, graphicPosition, new Vector3(newPostion.X, graphicPosition.Y, newPostion.Y));
            AnimationsQueue.Enqueue(moveAnimation);

            if (_setBomb)
            {
                Bomb bomb = Field.SetBomb(prevPosition);
                bomb.Detonated += () => _bombs.Remove(bomb);
                bomb.StartTimer(3);
                _bombs.Add(bomb);
                _setBomb = false;
            }

            return true;
        }

        public override bool TryRemove()
        {
            Field.Remove(this);
            return true;
        }
    }
}
