using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace DemoOpenTK
{
    public class Mesh : IMesh
    {
        private readonly Vector3[] _coordinates;
        private readonly Vector3[] _normals;
        private readonly Vector2[] _textures;
        private readonly uint[] _vertexIndexes;

        public Mesh(Vector3[] coordinates, Vector2[] textures,
             Vector3[] normals, uint[] vertexIndexes)
        {
            _coordinates = coordinates;
            _normals = normals;
            _textures = textures;
            _vertexIndexes = vertexIndexes;
        }

        public void Apply()
        {
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.VertexPointer(3, VertexPointerType.Float, 0, _coordinates);

            GL.EnableClientState(ArrayCap.NormalArray);
            GL.NormalPointer(NormalPointerType.Float, 0, _normals);

            GL.EnableClientState(ArrayCap.TextureCoordArray);
            GL.TexCoordPointer(3, TexCoordPointerType.Float, 0, _textures);

            GL.DrawElements(PrimitiveType.Triangles, _vertexIndexes.Length, DrawElementsType.UnsignedInt, _vertexIndexes);

            GL.DisableClientState(ArrayCap.VertexArray);
            GL.DisableClientState(ArrayCap.NormalArray);
            GL.DisableClientState(ArrayCap.TextureCoordArray);
        }

        public void Dispose()
        { }
    }
}
