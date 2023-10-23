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
        private readonly GameField _gameField;
        private BaseCamera _camera;
        private BaseLight _light;
        
        private double _lastFpsUpdate;
        private ushort _framesCount;

        // карта проходимости

        public GameScene(GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings,
            GameObjectsFactory gameObjectsFactory, 
            GameField gameField,
            ILoggerFactory? loggerFactory = null)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            _lastFpsUpdate = 0;
            _framesCount = 0;

            _loggerFactory = loggerFactory;
            _logger = loggerFactory?.CreateLogger<GameScene>();

            _gameField = gameField;
            _gameObjectsFactory = gameObjectsFactory;
            _camera = new MovingCamera(KeyboardState, MouseState, radius: 40);
            _light = new BaseLight(LightName.Light0);
            _light.Position = new Vector4(30, 30, 20, 1.0f);
            _light.Diffuse = new Vector4(0.5f, 0.5f, 0.5f, 1.0f);
            _light.Ambient = new Vector4(0.1f, 0.1f, 0.1f, 1.0f);
            _light.Specular = new Vector4(0.1f, 0.1f, 0.1f, 1.0f);

        }

        protected override void OnLoad()
        {
            //base.OnLoad();
            GL.Enable(EnableCap.DepthTest);
            _light.Enable();
            
            _gameObjectsFactory.OnLoad();
            _gameField.Setup();
            
            _logger?.LogDebug("Приложение загруженно.");

            foreach (StringName value in Enum.GetValues<StringName>().SkipLast(2))
                _logger?.LogDebug($"{value} = {GL.GetString(value)}");
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

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            //base.OnKeyDown(e);
            _gameObjectsFactory.OnKeyDown(e);
        }
    }
}
