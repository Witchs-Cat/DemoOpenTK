using OpenTK.Graphics.ES11;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace DemoOpenTK
{
    internal class Program
    {
        static void Main()
        {
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

            TestScene scene = new(gameWinSettings, nativeWinSettings);

            scene.AddEntity(new Grid());

            scene.Run();
        }
    }
}
