using Microsoft.Extensions.Logging;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace DemoOpenTK
{
    public class MovedGameObject : BaseGameObject, IMovable
    {
        protected Vector3 _animationCurrentPosition;
        protected Vector3 _animationEndPosition;

        public MovedGameObject(GraphicObject graphicObject, GameField field, 
            Vector2i position, ILogger<BaseGameObject>? logger = null) 
            : base(graphicObject, field, position, logger)
        {

        }

        public MovedObjectState State { get; protected set; }

        public override void OnUpdateFrame(in FrameEventArgs args)
        {
            if (State != MovedObjectState.Animated)
                return;
            float speed = 4;

            float deltaX = _animationCurrentPosition.X - _animationEndPosition.X;
            float deltaZ = _animationCurrentPosition.Z - _animationEndPosition.Z;
            //float deltaY = _animationCurrentPosition.Y - _animationEndPosition.Y;

            float step = (float)args.Time * speed; 
            _animationCurrentPosition.X -= step * MathF.Sign(deltaX);
            _animationCurrentPosition.Z -= step * MathF.Sign(deltaZ);
            //_animationCurrentPosition.Y += step * MathF.Sign(deltaY);

            if (MathF.Abs(deltaX) < step)
                _animationCurrentPosition.X = _animationEndPosition.X;

            //if (MathF.Abs(deltaX) < 0.01)
            //    _animationCurrentPosition.Y = _animationEndPosition.Y;

            if (MathF.Abs(deltaZ) < step)
                _animationCurrentPosition.Z = _animationEndPosition.Z;

            GraphicObject.MoveTo(_animationCurrentPosition);

            if (_animationCurrentPosition == _animationEndPosition)
                State = MovedObjectState.Inactive;
        }

        ///<inheritdoc/>
        public virtual bool TryMove(Vector2i shift)
        {
            if (State != MovedObjectState.Inactive)
                return false;

            Vector2i newPostion = Position + shift;
            if (Field.Layout.ContainsKey(newPostion))
            {
                return false;
            }

            Field.OnObjectMove(newPostion, Position, this);
            Position = newPostion;

            Vector3 graphicPosition = GraphicObject.Position;
            _animationCurrentPosition = graphicPosition;
            _animationEndPosition = new Vector3(newPostion.X, graphicPosition.Y, newPostion.Y);
            State = MovedObjectState.Animated;

            return true;
        }
    }
}
