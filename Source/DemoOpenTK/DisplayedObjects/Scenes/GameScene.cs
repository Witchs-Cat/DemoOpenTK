using Microsoft.Extensions.Logging;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Collections.Immutable;

namespace DemoOpenTK
{
    internal class GameScene : GameWindow
    {
        private readonly ILoggerFactory? _loggerFactory;
        private readonly ILogger<GameScene>? _logger;
        private readonly GameObjectsFactory _gameObjectsFactory;
        private BaseCamera _camera;
        private BaseLight _light;
        
        private double _lastFpsUpdate;
        private ushort _framesCount;

        // карта проходимости
        private readonly ImmutableArray<GameObjectType> _passabilityMap = new int[]{
            3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,
            3,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,2,0,0,0,3,
            3,0,2,1,2,0,2,0,2,2,2,1,2,0,2,0,2,0,2,2,3,
            3,0,2,0,2,0,0,0,2,0,2,0,0,0,2,0,1,0,0,0,3,
            3,0,1,0,2,2,1,2,2,0,2,0,2,2,2,1,2,0,2,0,3,
            3,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,2,0,2,0,3,
            3,0,2,2,1,1,2,0,2,0,2,2,2,2,2,0,2,2,2,0,3,
            3,0,2,0,0,0,2,0,2,0,0,0,0,0,2,0,0,0,0,0,3,
            3,0,2,0,2,2,2,0,2,0,2,2,1,2,2,2,1,2,2,0,3,
            3,0,0,0,2,0,0,0,2,0,2,0,0,0,0,0,0,0,1,0,3,
            3,2,2,2,2,0,2,2,2,0,2,0,2,2,2,2,2,2,2,0,3,
            3,0,0,0,2,0,0,0,1,0,2,0,0,0,2,0,0,0,0,0,3,
            3,0,2,0,2,2,2,0,2,1,2,0,2,2,2,0,2,2,2,2,3,
            3,0,2,0,0,0,2,0,0,0,2,0,0,0,2,0,2,0,0,0,3,
            3,2,2,2,2,0,2,2,2,0,2,2,2,0,1,0,2,2,2,0,3,
            3,0,0,0,0,0,2,0,2,0,0,0,2,0,1,0,0,0,2,0,3,
            3,0,2,0,2,1,2,0,2,0,2,2,2,0,2,2,2,0,2,0,3,
            3,0,1,0,1,0,0,0,0,0,2,0,0,0,2,0,0,0,0,0,3,
            3,0,2,1,2,0,2,2,2,2,2,0,2,0,2,0,2,2,2,2,3,
            3,0,0,0,0,0,0,0,0,0,0,0,2,0,2,0,0,0,0,0,3,
            3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3
        }.Select(x => (GameObjectType)x).ToImmutableArray();

        public GameScene(GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings,
            GameObjectsFactory gameObjectsFactory,
            ILoggerFactory? loggerFactory = null)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            _lastFpsUpdate = 0;
            _framesCount = 0;

            _loggerFactory = loggerFactory;
            _logger = loggerFactory?.CreateLogger<GameScene>();

            _gameObjectsFactory = gameObjectsFactory;
            _camera = new MovingCamera(KeyboardState, MouseState, radius: 2);
            _light = new BaseLight(LightName.Light0);
            _light.Position = new Vector4(0.0f, 10.0f, 0.0f, 1.0f);
            _light.Diffuse = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            _light.Ambient = new Vector4(0.1f, 0.1f, 0.1f, 1.0f);
            _light.Specular = new Vector4(0.1f, 0.1f, 0.1f, 1.0f);

        }

        protected override void OnLoad()
        {
            base.OnLoad();

            for (int i = 0; i < _passabilityMap.Length; i++)
            {
                GameObjectType objectType = _passabilityMap[i];
                if (objectType == GameObjectType.None)
                    continue;
                _gameObjectsFactory.Create(objectType, i % 21, i / 21);
            }

            _gameObjectsFactory.Create(GameObjectType.Field, 10, 10, -0.5f);

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.AlphaTest);
            GL.Enable(EnableCap.Lighting);

            _light.Enable();

            _logger?.LogDebug("Приложение запущенно.");
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            //base.OnResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);

            _camera.OnResize(e);

            double elapsed = TimeSinceLastUpdate();
            ResetTimeSinceLastUpdate();

            OnUpdateFrame(new FrameEventArgs(elapsed));
            OnRenderFrame(new FrameEventArgs(elapsed));

            _logger?.LogTrace($"Изменен размер окна ({e.Width}, {e.Height})");
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

            _gameObjectsFactory.UpdateGraphicObjects(in args);
            _logger?.LogTrace($"Обновление фрейма [elapsed = {args.Time} ms.]");
        }

        private void CalculateFps(FrameEventArgs args)
        {
            _lastFpsUpdate += args.Time;
            _framesCount++;

            if (_lastFpsUpdate > 1)
            { 
                Title = "(Vsync: " + VSync.ToString() + ") " + "  FPS: " + (_framesCount/_lastFpsUpdate).ToString("0.");
                _framesCount = 0;
                _lastFpsUpdate = 0;
            }
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            //base.OnRenderFrame(args);
            GL.ClearColor(new Color4(0x00, 0x00, 0x00, 0xff));
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            CalculateFps(args);

            _camera.OnRenderFrame(args);
            _light.OnRenderFrame(args);


            _gameObjectsFactory.RenderGraphicObjects(in args);

            SwapBuffers();
            _logger?.LogTrace($"Рендер фрейма [elapsed = {args.Time} ms.]");
        }

    }
}
