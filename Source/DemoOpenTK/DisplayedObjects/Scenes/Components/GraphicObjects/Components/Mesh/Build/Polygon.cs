using OpenTK.Mathematics;

namespace DemoOpenTK
{
    public class Polygon
    {

        public Polygon(Vector3i vertex0, Vector3i vertex1, Vector3i vertex2)
        {
            Vertex0 = vertex0;
            Vertex1 = vertex1;
            Vertex2 = vertex2;
        }

        public Vector3i Vertex0;
        public Vector3i Vertex1;
        public Vector3i Vertex2;

        public ref Vector3i this[int index]
        {
            get
            {
                if (index == 0) return ref Vertex0;
                if (index == 1) return ref Vertex1;
                if (index == 2) return ref Vertex2;
                throw new IndexOutOfRangeException();
            }
        }


        public override string ToString()
        {
            return $"{Vertex0} / {Vertex1} / {Vertex2}";
        }
    }
}
