using DemoOpenTK.DisplayedObjects.Camera;
using DemoOpenTK.DisplayedObjects.Lights;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DemoOpenTK
{
    internal class TestScene : GameWindow
    {
        private List<GeometryEntity> _entities;
        private BaseCamera _camera;
        private BaseLight _light;

        public TestScene(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            _camera = new MovingCamera(KeyboardState, MouseState, radius: 2);
            _light = new BaseLight(LightName.Light0);
            _entities = new List<GeometryEntity>();
        }

        public void AddEntity(GeometryEntity entity)
        {
            _entities.Add(entity);
        }

        protected override void OnLoad()
        {
            //base.OnLoad();
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.AlphaTest);

            GL.Enable(EnableCap.Lighting);
            _light.Enable();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            //base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);

            _camera.OnResize(e);
       
            double elapsed = this.TimeSinceLastUpdate();
            this.ResetTimeSinceLastUpdate();

            OnUpdateFrame(new FrameEventArgs(elapsed));
            OnRenderFrame(new FrameEventArgs(elapsed));
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
            //base.OnUpdateFrame(args);

            _camera.OnUpdateFrame(args);
            _light.OnUpdateFrame(args);

            UpdateEntities(in args);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            //base.OnRenderFrame(args);
            GL.ClearColor(new Color4(0x00, 0x00, 0x00, 0xff));
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            _camera.OnRenderFrame(args);
            _light.OnRenderFrame(args);

            RenderEntities(in args);

            SwapBuffers();
        }


        private void RenderEntities(in FrameEventArgs args)
        {
            foreach (GeometryEntity entity in _entities)
            {
                entity.OnRenderFrame(in args);
            }
        }


        private void UpdateEntities(in FrameEventArgs args)
        {
            foreach (GeometryEntity entity in _entities)
            {
                entity.OnUpdateFrame(in args);
            }
        }

    }
}
