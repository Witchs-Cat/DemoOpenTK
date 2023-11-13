namespace DemoOpenTK
{
    public class StaticGameObject : BaseGameObject
    {
        public StaticGameObject(GameObjectConfig config) : base(config)
        {

        }

        public override bool TryRemove()
        {
            return false;
        }
    }
}