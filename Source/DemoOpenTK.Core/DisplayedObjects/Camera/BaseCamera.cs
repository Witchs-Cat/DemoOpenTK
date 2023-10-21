using Microsoft.Extensions.Logging;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace DemoOpenTK.DisplayedObjects.Camera
{
    public class BaseCamera : ICamera
    {
        private readonly float _fovy;

        public BaseCamera() 
            : this(new Vector3(0, 1, 1), Vector3.Zero, Vector3.UnitY)
        { }

        public BaseCamera(Vector3 eyePosition, Vector3 targetPosition, Vector3 upVector)
        {
            EyePosition = eyePosition;
            TargetPosition = targetPosition;
            UpVector = upVector;

            _fovy = (30.0f / 180.0f) * MathF.PI;
        }

        public Vector3 EyePosition { get; protected set; }
        public Vector3 TargetPosition { get; protected set; }
        public Vector3 UpVector{ get; protected set; }


        public virtual void GetViewMatrix(out Matrix4 viewMatrix)
        {
            viewMatrix = Matrix4.LookAt(EyePosition, TargetPosition, UpVector);
        }

        public virtual void OnUpdateFrame(in FrameEventArgs args)
        { }

        public virtual void OnRenderFrame(in FrameEventArgs args)
        {
            GL.MatrixMode(MatrixMode.Modelview);
            GetViewMatrix(out Matrix4 viewMatrix);
            GL.LoadMatrix(ref viewMatrix);
        }

        public virtual void OnResize(ResizeEventArgs args)
        {
            GL.LoadIdentity();
            GL.MatrixMode(MatrixMode.Projection);
            Matrix4.CreatePerspectiveFieldOfView(
                fovy: _fovy, 
                aspect: (float)args.Width / args.Height, 
                depthNear: 0.2f,  
                depthFar: 70.0f, 
                result: out Matrix4 perspective);

            GL.LoadMatrix(ref perspective);
        }
    }
}
