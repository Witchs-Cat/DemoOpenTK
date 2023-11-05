using OpenTK.Windowing.Common;

namespace DemoOpenTK
{
    public interface IAnimation
    {
        AnimationState State { get; }

        void OnUpdateFrame( FrameEventArgs args);
    }
}