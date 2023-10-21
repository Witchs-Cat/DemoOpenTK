using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoOpenTK.DisplayedObjects
{
    public interface IDisplayedObject
    {
        void OnUpdateFrame(in FrameEventArgs args);
        void OnRenderFrame(in FrameEventArgs args);
    }
}
