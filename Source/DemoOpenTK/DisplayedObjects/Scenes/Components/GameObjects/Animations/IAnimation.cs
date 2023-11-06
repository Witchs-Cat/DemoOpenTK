using OpenTK.Windowing.Common;
using System.Runtime.CompilerServices;

namespace DemoOpenTK
{
    public interface IAnimation
    {
        AnimationState State { get; }

        void OnUpdateFrame( FrameEventArgs args);
    }
}