using DemoOpenTK.ObjParser;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DemoOpenTK
{
    public class MeshModel
    {
        public MeshModel(IEnumerable<Vector3> vertices, IEnumerable<Vector3> normals,
            IEnumerable<Vector3> materials, IEnumerable<Polygon> faces ) 
        {
            Vertices = vertices;
            VertexNormals = normals;
            TextureCoords = materials;
            Faces = faces;
        }

        IEnumerable<Vector3> Vertices { get; }
        IEnumerable<Vector3> VertexNormals { get; }
        IEnumerable<Vector3> TextureCoords { get; }
        IEnumerable<Polygon> Faces { get; }
    }
}
