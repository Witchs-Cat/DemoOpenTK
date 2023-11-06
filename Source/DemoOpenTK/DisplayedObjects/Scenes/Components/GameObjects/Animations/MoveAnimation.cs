using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace DemoOpenTK
{
    public class MoveAnimation : IAnimation
    {
        private Vector3 _currentPosition;
        private Vector3 _endPosition;
        private float _speed;

        public MoveAnimation(BaseGraphicObject graphicObject, Vector3 currentPositon, Vector3 endPosition)
        {
            _currentPosition = currentPositon;
            _endPosition = endPosition;
            _speed = 4;
            State = AnimationState.Played;
            GraphicObject = graphicObject;
        }

        public BaseGraphicObject GraphicObject { get; }
        public AnimationState State { get; private set; }

        public event Action? EndAnimation;

        public void OnUpdateFrame(FrameEventArgs args)
        {
            if (State != AnimationState.Played)
                return;

            float deltaX = _currentPosition.X - _endPosition.X;
            float deltaZ = _currentPosition.Z - _endPosition.Z;

            float step = (float)args.Time * _speed;
            _currentPosition.X -= step * MathF.Sign(deltaX);
            _currentPosition.Z -= step * MathF.Sign(deltaZ);

            if (MathF.Abs(deltaX) < step)
                _currentPosition.X = _endPosition.X;

            if (MathF.Abs(deltaZ) < step)
                _currentPosition.Z = _endPosition.Z;

            GraphicObject.MoveTo(_currentPosition);

            if (_currentPosition == _endPosition)
            {
                State = AnimationState.Complitied;
                EndAnimation?.Invoke();
            }
        }
    }
}
