namespace DemoOpenTK
{
    public class GraphicObjectData
    {
        public GraphicObjectData( Mesh mesh, BaseMaterial material) 
        {
            Mesh = mesh;
            Material = material;
        } 

        public Mesh Mesh { get; }
        public BaseMaterial Material { get; }
    }
}