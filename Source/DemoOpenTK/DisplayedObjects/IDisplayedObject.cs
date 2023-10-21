﻿using OpenTK.Windowing.Common;

namespace DemoOpenTK
{
    public interface IDisplayedObject
    {
        void OnUpdateFrame(in FrameEventArgs args);
        void OnRenderFrame(in FrameEventArgs args);
    }
}
