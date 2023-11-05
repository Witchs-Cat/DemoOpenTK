using OpenTK.Graphics.OpenGL;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Imaging;
using SystemPixelFormat = System.Drawing.Imaging.PixelFormat;
using OpenTKPixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;

namespace DemoOpenTK
{
    public class BaseTexture : IGraphicObjectComponent, IDisposable
    {
        private readonly int _index;
        private readonly int _width;
        private readonly int _height;
        private TextureTarget _target;

        public BaseTexture(int textureIndex, int width, int height, TextureTarget target)
        {
            _index = textureIndex;
            _width = width;
            _height = height;
            _target = target;
        }

        public void Apply()
        {
            GL.BindTexture(_target, _index);
        }

        #pragma warning disable CA1416 // Проверка совместимости платформы
        public static BaseTexture LoadFromImgFile(string pathToFile)
        {
            using Bitmap bitmap = new(pathToFile);
            BitmapData bitmapData = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, SystemPixelFormat.Format32bppArgb);

            GL.ActiveTexture(TextureUnit.Texture0);
            int textureIndex = GL.GenTexture();

            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);

            TextureTarget target = TextureTarget.Texture2D;

            GL.BindTexture(target, textureIndex);
            GL.TexImage2D(target, level: 0, PixelInternalFormat.Rgba, bitmap.Width, bitmap.Height,
                border: 0, OpenTKPixelFormat.Bgra, PixelType.UnsignedByte, bitmapData.Scan0);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            bitmap.UnlockBits(bitmapData);

            return new BaseTexture(textureIndex, bitmap.Width, bitmap.Height, target);
        }
        #pragma warning restore CA1416 // Проверка совместимости платформы

        public void Dispose()
        {
            GL.DeleteTexture(_index);
        }
    }
}
