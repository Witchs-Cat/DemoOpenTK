using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace DemoOpenTK
{
    internal class Program
    {
        static void Main()
        {
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
                builder
                .SetMinimumLevel(LogLevel.Debug)
                .AddSimpleConsole(options => options.ColorBehavior = LoggerColorBehavior.Enabled));

            NativeWindowSettings nativeWinSettings = new()
            {
                Size = new Vector2i(600, 450),
                WindowBorder = WindowBorder.Resizable,
                WindowState = WindowState.Normal,
                Title = "Чайник блять",

                Flags = ContextFlags.Default,
                APIVersion = new Version(3, 3),
                //Пддержка легаси
                Profile = ContextProfile.Compatability,
                API = ContextAPI.OpenGL,
            };

            GameWindowSettings gameWinSettings = new()
            {
                UpdateFrequency = 0,
            };

            GraphicObjectsData graphicData = GraphicObjectsData.LoadFromJsonFile(@"Assets\GraphicObjectsData.json");
            GameObjectsFactory gameObjectsFactory = new(graphicData);
            GameScene scene = new(gameWinSettings, nativeWinSettings, gameObjectsFactory, loggerFactory);

            scene.Run();
        }
    }
}
