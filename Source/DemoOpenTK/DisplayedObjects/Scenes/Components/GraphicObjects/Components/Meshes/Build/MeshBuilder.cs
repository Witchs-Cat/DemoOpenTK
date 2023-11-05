using Microsoft.Extensions.Logging;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Globalization;
using System.Runtime.InteropServices;

namespace DemoOpenTK
{
    public class MeshBuilder
    {
        private static readonly CultureInfo _culture = new CultureInfo("Ru-ru", true);
        private static readonly NumberFormatInfo _numberFormat = _culture.NumberFormat;

        private readonly LinkedList<Vector3> _coordinates;
        private readonly LinkedList<Vector3> _normals;
        private readonly LinkedList<Vector3> _textures;
        private readonly LinkedList<Polygon> _faces;

        private bool _useEBO;

        static MeshBuilder()
        {
            _numberFormat.NumberDecimalSeparator = ".";
        }

        public MeshBuilder(ILogger<MeshBuilder>? logger = null)    
        {
            _coordinates = new LinkedList<Vector3>();
            _normals = new LinkedList<Vector3>();
            _textures = new LinkedList<Vector3>(); 
            _faces = new LinkedList<Polygon>();
            _useEBO = false;
        }

        public MeshBuilder LoadFromFile(string pathToFile)
        {
            IEnumerable<string> lines = File.ReadLines(pathToFile);
            
            foreach (string line in lines)
            {
                if (line.Length < 2)
                    continue;

                switch (line[..2])
                {
                    case "# ":
                        continue;
                    case "v ":
                        _coordinates.AddLast(ParseVector3(line[3..]));
                        break;
                    case "vn":
                        _normals.AddLast(ParseVector3(line[3..]));
                        break;
                    case "vt":
                        _textures.AddLast(ParseVector3(line[3..]));
                        break;
                    case "f ":
                        _faces.AddLast(ParsePolygon(line[2..]));
                        break;
                }
            }
            return this;
        }

        public MeshBuilder UseEBO(bool useEBO = true)
        {
            _useEBO = useEBO;
            return this;
        }

        private void GetMeshParams(out IEnumerable<Vertex> vertexts, out IEnumerable<uint> indexses)
        {
            Dictionary<Vector3i, uint> faceVertexes = new();
            LinkedList<uint> vertexIndexes = new();
            LinkedList<Vertex> resultVertexes = new();

            uint lastIndex = 0;
            foreach (Polygon polygon in _faces)
            {
                for (int i = 0; i < 3; i++)
                {
                    Vector3i currentVertex = polygon[i];

                    if (faceVertexes.TryGetValue(currentVertex, out uint currentIndex))
                    {
                        vertexIndexes.AddLast(currentIndex);
                    }
                    else
                    {
                        vertexIndexes.AddLast(lastIndex);
                        faceVertexes.Add(currentVertex, lastIndex);
                        lastIndex++;

                        Vector3 coordinate = _coordinates.ElementAt(currentVertex[0] - 1);
                        Vector2 texture = _textures.ElementAt(currentVertex[1] - 1).Xy;
                        Vector3 normal = _normals.ElementAt(currentVertex[2] - 1);

                        Vertex vertex = new(coordinate, texture, normal);
                        resultVertexes.AddLast(vertex);
                    }
                }
            }
            vertexts = resultVertexes;
            indexses = vertexIndexes;
        }

        private Mesh CreateMesh()
        {
            GetMeshParams(out IEnumerable<Vertex> vertexes, out IEnumerable<uint> indexes);
            return new Mesh(vertexes.Select(x => x.Coordinate).ToArray(), 
                vertexes.Select(x => x.Texture).ToArray(), 
                vertexes.Select(x => x.Normal).ToArray(), 
                indexes.ToArray());
        }

        private IMesh CreateEBOMesh()
        {
            int vertexBuffer = GL.GenBuffer();
            int indexBuffer = GL.GenBuffer();

            GetMeshParams(out IEnumerable<Vertex> vertexes, out IEnumerable<uint> indexes);

            Vertex[] vertexArray = vertexes.ToArray();
            uint[] indexArray = indexes.ToArray(); 
            int indexCount = indexArray.Length;

            BufferTarget bufferTarget = BufferTarget.ArrayBuffer;
            BufferUsageHint bufferUsage = BufferUsageHint.StaticDraw;
            int size = vertexArray.Length * Marshal.SizeOf<Vertex>();

            GL.BindBuffer(bufferTarget, vertexBuffer);
            GL.BufferData(bufferTarget, size, vertexArray, bufferUsage);
            GL.BindBuffer(bufferTarget, 0);

            bufferTarget = BufferTarget.ElementArrayBuffer;
            size = indexArray.Length * Marshal.SizeOf<uint>();
            
            GL.BindBuffer(bufferTarget, indexBuffer);
            GL.BufferData(bufferTarget, size, indexArray, bufferUsage);
            GL.BindBuffer(bufferTarget, 0);

            return new EBOMesh(vertexBuffer, indexBuffer, indexCount);
        }

        public IMesh Build()
        {
            if (_useEBO)
                return CreateEBOMesh();
            return CreateMesh();

        }


        private Polygon ParsePolygon(string str)
        {
            IEnumerable<Vector3i> polygon = str.Split(" ").Select(x => ParseVector3i(x, "/"));
            return new Polygon(polygon.ElementAt(0), polygon.ElementAt(1), polygon.ElementAt(2));
        }

        private static Vector3 ParseVector3(string str, string seporator = " ")
        {
            IEnumerable<float> vector = str.Split(seporator).Select(x => Single.Parse(x, _numberFormat));
            return new Vector3(vector.ElementAt(0), vector.ElementAt(1), vector.ElementAt(2));
        }

        private static Vector3i ParseVector3i(string str, string seporator = " ")
        {
            IEnumerable<int> vector = str.Split(seporator).Select(x => Int32.Parse(x));
            return new Vector3i(vector.ElementAt(0), vector.ElementAt(1), vector.ElementAt(2));
        }

    }
}
