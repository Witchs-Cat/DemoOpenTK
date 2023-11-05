using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoOpenTK.DisplayedObjects.Scenes.Components.GameObjects.Animations
{
    public class MoveAnimation : IAnimation
    {
        private Vector3 _currentPosition;
        private Vector3 _endPosition;

        public MoveAnimation(BaseGraphicObject graphicObject)
        {
            _currentPosition = Vector3.Zero;
            _endPosition = Vector3.Zero;

            GraphicObject = graphicObject;
        }
        
        public BaseGraphicObject GraphicObject { get; }
        public AnimationState State { get; private set; }
        
        public void OnUpdateFrame(FrameEventArgs args)
        {
            if (State != AnimationState.Played)
                return;
            float speed = 4;

            float deltaX = _currentPosition.X - _endPosition.X;
            float deltaZ = _currentPosition.Z - _endPosition.Z;

            float step = (float)args.Time * speed;
            _currentPosition.X -= step * MathF.Sign(deltaX);
            _currentPosition.Z -= step * MathF.Sign(deltaZ);

            if (MathF.Abs(deltaX) < step)
                _currentPosition.X = _endPosition.X;

            if (MathF.Abs(deltaZ) < step)
                _currentPosition.Z = _endPosition.Z;

            GraphicObject.MoveTo(_currentPosition);

            if (_currentPosition == _endPosition)
                State = AnimationState.Inactive;
        }

        public void Play()
        {
            State = AnimationState.Played;
        }

        public void Play(in Vector3 startPosition, in Vector3 endPositin)
        {
            _currentPosition = startPosition;
            _endPosition = endPositin;
            State = AnimationState.Played;
        }

        public void Stop()
        {
            State = AnimationState.Stoped;
        }
    }
}
