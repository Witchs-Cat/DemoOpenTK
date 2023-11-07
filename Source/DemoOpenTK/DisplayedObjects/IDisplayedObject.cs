using OpenTK.Windowing.Common;

namespace DemoOpenTK
{
    public interface IDisplayedObject
    {
        void OnRenderFrame( FrameEventArgs args);
    }
}
