using OpenTK.Mathematics;

namespace DemoOpenTK
{
    public interface ICamera : IDisplayedObject
    {
        Vector3 TargetPosition { get; }
        Vector3 EyePosition { get; }
        Vector3 UpVector { get; }

        void GetViewMatrix(out Matrix4 viewMatrix);
    }
}
