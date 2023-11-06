using OpenTK.Windowing.Common;

namespace DemoOpenTK
{
    public class InactivityAnimation : IAnimation
    {
        private double _remainingLifeTime;

        public InactivityAnimation(BaseGraphicObject graphicObject, double liveTimeSeconds) 
        {
            State = AnimationState.Played;
            _remainingLifeTime = liveTimeSeconds;
        }

        public AnimationState State { get; private set; }

        public event Action? EndAnimation;

        public void OnUpdateFrame(FrameEventArgs args)
        {
            if (State != AnimationState.Played)
                return;

            _remainingLifeTime -= args.Time;

            if (_remainingLifeTime < 0)
            { 
                State = AnimationState.Complitied;
                EndAnimation?.Invoke();
            }
        }
    }
}
