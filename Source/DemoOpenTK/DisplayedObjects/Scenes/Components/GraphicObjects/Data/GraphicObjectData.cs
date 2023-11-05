namespace DemoOpenTK
{
    public class GraphicObjectData
    {
        public GraphicObjectData( IMesh mesh, BaseMaterial material, BaseTexture? texture = null) 
        {
            Mesh = mesh;
            Material = material;
            Texture = texture;
        } 

        public IMesh Mesh { get; }
        public BaseMaterial Material { get; }
        public BaseTexture? Texture { get; }
    }
}