namespace DemoOpenTK
{
    public class GraphicObjectData
    {
        public GraphicObjectData( IMesh mesh, BaseMaterial material) 
        {
            Mesh = mesh;
            Material = material;
        } 

        public IMesh Mesh { get; }
        public BaseMaterial Material { get; }
    }
}