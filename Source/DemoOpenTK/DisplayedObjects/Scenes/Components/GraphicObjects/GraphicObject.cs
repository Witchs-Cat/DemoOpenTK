using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace DemoOpenTK
{
    public abstract class GraphicObject : IDisplayedObject
    {
        private Matrix4 _modelMatrix;

        public GraphicObject(BaseMaterial material)
        {
            _modelMatrix = Matrix4.Identity;

            Material = material;
            Size = 1;
        }

        public BaseMaterial Material { get; protected set; }
        public int Size { get; private set; }

        public virtual void OnRenderFrame(in FrameEventArgs args)
        { }
        public virtual void OnUpdateFrame(in FrameEventArgs args)
        { }

        public virtual void GetModelMatrix(out Matrix4 modelMatrix)
        {
            modelMatrix = _modelMatrix;
            modelMatrix *= Size;
        }

        public virtual void MoveTo(float x, float y, float z)
        {
            _modelMatrix.Row3.X = x;
            _modelMatrix.Row3.Y = y;
            _modelMatrix.Row3.Z = z;
        }
    }
}
