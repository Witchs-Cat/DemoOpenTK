using DemoOpenTK.DisplayedObjects;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace DemoOpenTK
{
    public abstract class GeometryEntity : IDisplayedObject
    {
        private Matrix4 _modelMatrix;

        public GeometryEntity()
        { }

        public virtual void OnRenderFrame(in FrameEventArgs args)
        { }
        public virtual void OnUpdateFrame(in FrameEventArgs args)
        { }

        public virtual void GetModelMatrix(out Matrix4 modelMatrix)
        {
            modelMatrix = _modelMatrix;
        }
    }
}
