using OpenTK.Windowing.Common;

namespace DemoOpenTK
{
    public interface IDisplayedObject
    {
        void OnUpdateFrame( FrameEventArgs args);
        void OnRenderFrame( FrameEventArgs args);
    }
}
