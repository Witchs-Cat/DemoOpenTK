using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DemoOpenTK
{
    public class EBOMesh : IMesh
    {
        private readonly int _vertexBuffer;
        private readonly int _indexBuffer;
        private readonly int _indexCount;

        public EBOMesh(int vertexBuffer, int indexBuffer, int indexCount) 
        {
            _indexBuffer = indexBuffer;
            _vertexBuffer = vertexBuffer;
            _indexCount = indexCount;
        }

        public void Apply()
        {
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.TextureCoordArray);
            GL.EnableClientState(ArrayCap.NormalArray);

            BufferTarget bufferTarget = BufferTarget.ArrayBuffer;
            GL.BindBuffer(bufferTarget, _vertexBuffer);
            {
                int vertexSize = Marshal.SizeOf<Vertex>();

                string fieldName = nameof(Vertex.Coordinate);
                nint offset = Marshal.OffsetOf<Vertex>(fieldName);
                GL.VertexPointer(3, VertexPointerType.Float, vertexSize, offset);

                fieldName = nameof(Vertex.Texture);
                offset = Marshal.OffsetOf<Vertex>(fieldName);
                GL.TexCoordPointer(2, TexCoordPointerType.Float, vertexSize, offset);

                fieldName = nameof(Vertex.Normal);
                offset = Marshal.OffsetOf<Vertex>(fieldName);
                GL.NormalPointer(NormalPointerType.Float, vertexSize, offset);

            }
            GL.BindBuffer(bufferTarget, 0);

            bufferTarget = BufferTarget.ElementArrayBuffer;
            GL.BindBuffer(bufferTarget, _indexBuffer);
            GL.DrawElements(BeginMode.Triangles, _indexCount, DrawElementsType.UnsignedInt, 0);
            GL.BindBuffer(bufferTarget, 0);

            GL.DisableClientState(ArrayCap.VertexArray);
            GL.DisableClientState(ArrayCap.TextureCoordArray);
            GL.DisableClientState(ArrayCap.NormalArray);
        }

        public void Dispose()
        {
            GL.DeleteBuffer(_vertexBuffer);
            GL.DeleteBuffer(_indexBuffer);
        }
    }
}
