﻿using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace DemoOpenTK.DisplayedObjects.Camera
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
        public float AngleO { 
            get => _angleO;
            private set {
                //if (value > 85) value = 85;
                //else if (value < 5) value = 5; 

                //_angleO = value;

                if (value >= 0)
                    _angleO = value % 360;
                else
                    _angleO = 360 + value % 360;
            }
        }
        /// <summary>
        /// X ~ Z
        /// </summary>
        public float AngleF
        {
            get => _angleF;
            private set {
                if (value >= 0)
                    _angleF = value % 360;
                else
                    _angleF = 360 + value % 360;
            }
        }
        public float Radius {
            get => _radius;
            private set { 
                _radius = value;
                if (_radius < 0) 
                    _radius = 0;
            } 
        }

        public override void OnUpdateFrame(in FrameEventArgs args)
        {
            if (_mouse.ScrollDelta.Y == 0 && !_mouse.WasButtonDown(MouseButton.Left))
                return;

            Radius += _mouse.ScrollDelta.Y;
            AngleF += _mouse.Delta.X;
            AngleO -= _mouse.Delta.Y;

            UpdateCameraPosition();
        }

        private void UpdateCameraPosition()
        {
            float radF = AngleF / 180 * 3.14159f;
            float radO = AngleO / 180 * 3.14159f;

            Vector3 newEyePosition = new();
            newEyePosition.X = Convert.ToSingle(Radius * Math.Sin(radO) * Math.Cos(radF)) + TargetPosition.X;
            newEyePosition.Y = Convert.ToSingle(Radius * Math.Cos(radO)) + TargetPosition.Y;
            newEyePosition.Z = Convert.ToSingle(Radius * Math.Sin(radO) * Math.Sin(radF)) + TargetPosition.Z;

            EyePosition = newEyePosition;
            Console.WriteLine(AngleO);

            if (AngleO < 180)
                UpVector = new Vector3(0, 1, 0);
            else
                UpVector = new Vector3(0, -1, 0);
        }
    }
}
