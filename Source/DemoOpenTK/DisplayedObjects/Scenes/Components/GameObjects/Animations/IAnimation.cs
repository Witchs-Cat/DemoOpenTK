using OpenTK.Windowing.Common;
using System.Runtime.CompilerServices;

namespace DemoOpenTK
{
    public interface IAnimation
    {
        AnimationState State { get; }
        event Action? EndAnimation;

        void OnUpdateFrame( FrameEventArgs args);
    }
}