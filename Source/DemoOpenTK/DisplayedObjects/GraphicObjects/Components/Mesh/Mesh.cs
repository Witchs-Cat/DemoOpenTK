
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace DemoOpenTK
{
    public class Mesh
    {
        private readonly Vector3[] _coordinates;
        private readonly Vector3[] _normals;
        private readonly Vector2[] _textures;

        public Mesh(Vector3[] coordinates, Vector3[] normals,
             Vector2[] materials)
        {
            _coordinates = coordinates;
            _normals = normals;
            _textures = materials;
        }

        public void Apply()
        {
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.VertexPointer(3, VertexPointerType.Float, 0, _coordinates);

            GL.EnableClientState(ArrayCap.NormalArray);
            GL.NormalPointer(NormalPointerType.Float, 0, _normals);

            GL.EnableClientState(ArrayCap.TextureCoordArray);
            GL.TexCoordPointer(3, TexCoordPointerType.Float, 0, _textures);

            GL.DrawArrays(PrimitiveType.Triangles,0, _textures.Count());

            GL.DisableClientState(ArrayCap.VertexArray);
            GL.DisableClientState(ArrayCap.NormalArray);
            GL.DisableClientState(ArrayCap.TextureCoordArray);
        }
    }
}
