using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace DemoOpenTK
{
    public abstract class BaseGraphicObject : IDisplayedObject
    {
        private Matrix4 _modelMatrix;

        public BaseGraphicObject(BaseMaterial material)
        {
            _modelMatrix = Matrix4.Identity;

            Material = material;
            Size = 1;
        }

        public BaseMaterial Material { get; protected set; }
        public int Size { get; private set; }
        public Vector3 Position => _modelMatrix.Row3.Xyz;

        public virtual void OnRenderFrame(FrameEventArgs args)
        { }

        public virtual void GetModelMatrix(out Matrix4 modelMatrix)
        {
            modelMatrix = _modelMatrix;
            modelMatrix *= Size;
        }

        public virtual void MoveTo(Vector3 position)
        {
            _modelMatrix.Row3.X = position.X;
            _modelMatrix.Row3.Y = position.Y;
            _modelMatrix.Row3.Z = position.Z;
        }
    }
}
