
using OpenTK.Mathematics;

namespace DemoOpenTK
{ 
    public struct Vertex
    {
        public Vertex(Vector3 coordinate, Vector2 texture, Vector3 normal)
        {
            Coordinate = coordinate;
            Texture = texture;
            Normal = normal;
        }

        public Vector3 Coordinate;
        public Vector2 Texture;
        public Vector3 Normal;

        public override string ToString()
        {
            return $"{Coordinate} / {Texture} / {Normal}";
        }
    }
}
