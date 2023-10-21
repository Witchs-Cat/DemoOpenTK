using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DemoOpenTK.ObjParser
{
    internal class MeshModel
    {
        IEnumerable<Vector3> Vertices { get; }
        IEnumerable<Vector3> VertexNormals { get; }
        IEnumerable<Vector3> TextureCoords { get; }
    }
}
