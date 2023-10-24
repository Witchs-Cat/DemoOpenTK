using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Collections.Immutable;

namespace DemoOpenTK
{
    internal class Program
    {
        private static ImmutableArray<GameObjectType> _passabilityMap = new int[]{
            3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,
            3,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,2,0,0,0,3,
            3,0,2,1,2,0,2,0,2,2,2,1,2,0,2,0,2,0,2,2,3,
            3,0,2,0,2,0,0,0,2,0,2,0,0,0,2,0,1,0,0,0,3,
            3,0,1,0,2,2,1,2,2,0,2,0,2,2,2,1,2,0,2,0,3,
            3,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,2,0,2,0,3,
            3,0,2,2,1,1,2,0,2,0,2,2,2,2,2,0,2,2,2,0,3,
            3,0,2,0,0,0,2,0,2,0,0,0,0,0,2,4,0,0,0,0,3,
            3,0,2,0,2,2,2,0,2,0,2,2,1,2,2,2,1,2,2,0,3,
            3,0,0,0,2,0,0,0,2,0,2,0,0,0,0,0,0,0,1,0,3,
            3,2,2,2,2,0,2,2,2,0,2,0,2,2,2,2,2,2,2,0,3,
            3,0,0,0,2,0,0,0,1,0,2,0,0,0,2,0,0,0,0,0,3,
            3,0,2,0,2,2,2,0,2,1,2,0,2,2,2,0,2,2,2,2,3,
            3,0,2,0,0,0,2,0,1,0,2,0,0,0,2,0,2,0,0,0,3,
            3,2,2,2,2,0,2,2,2,0,2,2,2,0,0,0,2,2,2,0,3,
            3,0,0,0,0,0,2,0,2,0,0,0,2,0,1,0,0,0,2,0,3,
            3,0,2,0,2,1,2,0,2,0,2,2,2,0,2,2,2,0,2,0,3,
            3,0,1,0,1,0,0,0,0,0,2,0,0,0,2,0,0,0,0,0,3,
            3,0,2,1,2,0,2,2,2,2,2,0,2,0,2,0,2,2,2,2,3,
            3,0,0,0,0,0,0,0,0,0,0,0,2,0,2,0,0,0,0,0,3,
            3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3
        }.Select(x => (GameObjectType) x).ToImmutableArray();

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
            
            GraphicObjectsData graphicData = new(@"Assets\GraphicObjectsData.json", loggerFactory);
            GameObjectsFactory gameObjectsFactory = new(graphicData, loggerFactory);
            GameField gameField = new(gameObjectsFactory, _passabilityMap);

            GameScene scene = new(gameWinSettings, nativeWinSettings, gameObjectsFactory, gameField, loggerFactory);
            scene.Run();
        }
    }
}
