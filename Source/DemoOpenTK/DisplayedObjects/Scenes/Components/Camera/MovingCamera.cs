using Microsoft.Extensions.Logging;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace DemoOpenTK
{
    public class MovingCamera : BaseCamera
    {
        private float _angleO;
        private float _angleF;
        private float _radius;

        private readonly KeyboardState _keyboard;
        private readonly MouseState _mouse;

        public MovingCamera(KeyboardState keyboard, MouseState mouse,
            float angleO = 0, float angleF = 0, float radius = 0) 
            : base()
        {
            _keyboard = keyboard;
            _mouse = mouse;

            AngleO = angleO;
            AngleF = angleF;
            Radius = radius;

            UpdateCameraPosition();
        }

        /// <summary>
        /// Y ~ XZ
        /// </summary>
        public float AngleO
        {
            get => _angleO;
            private set
            {
                //if (value > 85) value = 85;
                //else if (value < 5) value = 5; 

                //_angleO = value;
                float newValue;
                if (value >= 0)
                    newValue = value % 360;
                else
                    newValue = 360 + value % 360;

                if (newValue == 0)
                    newValue += 0.0001f;

                _angleO = newValue;
            }
        }
        /// <summary>
        /// X ~ Z
        /// </summary>
        public float AngleF
        {
            get => _angleF;
            private set
            {
                if (value >= 0)
                    _angleF = value % 360;
                else
                    _angleF = 360 + value % 360;
            }
        }
        public float Radius
        {
            get => _radius;
            private set
            {
                _radius = value;
                if (_radius < 0)
                    _radius = 0;
            }
        }

        public override void OnUpdateFrame( FrameEventArgs args)
        {
            if (_mouse.ScrollDelta.Y == 0 && !_mouse.WasButtonDown(MouseButton.Left))
                return;

            Radius += _mouse.ScrollDelta.Y;
            AngleF += _mouse.Delta.X / 10;
            AngleO -= _mouse.Delta.Y / 10;

            UpdateCameraPosition();
        }

        public void MoveTarget(Vector3 newTargetPosition)
        {
            TargetPosition = newTargetPosition;
            UpdateCameraPosition();
        }

        private void UpdateCameraPosition()
        {
            float radF = AngleF / 180 * 3.14159f;
            float radO = AngleO / 180 * 3.14159f;

            float cosO = MathF.Cos(radO), sinO = MathF.Sin(radO);
            float cosF = MathF.Cos(radF), sinF = MathF.Sin(radF);

            Vector3 newEyePosition = new();
            newEyePosition.X = Radius * sinO * cosF + TargetPosition.X;
            newEyePosition.Y = Radius * cosO + TargetPosition.Y;
            newEyePosition.Z = Radius * sinO * sinF + TargetPosition.Z;

            EyePosition = newEyePosition;
            UpVector = new Vector3(0, sinO, 0);
        }
    }
}
