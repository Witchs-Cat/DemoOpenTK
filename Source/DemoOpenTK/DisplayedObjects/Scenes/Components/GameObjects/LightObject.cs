using OpenTK.Mathematics;

namespace DemoOpenTK
{
    internal class LightObject : AnimatedGameObject, IMovable
    {
        public LightObject(GameObjectConfig config) : base(config)
        {
        }

        public virtual bool TryMove(Vector2i shift)
        {
            if (AnimationsQueue.Any())
                return false;

            Vector2i newPostion = Position + shift;
            if (Field.Layout.ContainsKey(newPostion))
                return false;

            Field.OnObjectMove(newPostion, Position, this);
            Position = newPostion;

            Vector3 graphicPosition = GraphicObject.Position;
            MoveAnimation animation = new(this.GraphicObject, graphicPosition, new Vector3(newPostion.X, graphicPosition.Y, newPostion.Y));
            AnimationsQueue.Enqueue(animation);
            return true;
        }
    }
}