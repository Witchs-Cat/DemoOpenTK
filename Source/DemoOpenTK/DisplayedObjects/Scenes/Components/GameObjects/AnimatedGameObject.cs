using OpenTK.Windowing.Common;

namespace DemoOpenTK
{
    public class AnimatedGameObject : BaseGameObject
    {
        public AnimatedGameObject(GameObjectConfig config) : base(config)
        {
            AnimationsQueue = new();
        }

        protected Queue<IAnimation> AnimationsQueue { get; }

        public override void OnUpdateFrame(FrameEventArgs args)
        {
            if (!AnimationsQueue.Any())
                return;

            IAnimation animation = AnimationsQueue.First();
            animation.OnUpdateFrame(args);

            if (animation.State == AnimationState.Inactive)
                AnimationsQueue.Dequeue();
        }

        ///<inheritdoc/>
        //public virtual bool TryMove(Vector2i shift)
        //{
        //    if (Animation.State != AnimationState.Inactive)
        //        return false;

        //    Vector2i newPostion = Position + shift;
        //    if (Field.Layout.ContainsKey(newPostion))
        //        return false;

        //    Field.OnObjectMove(newPostion, Position, this);
        //    Position = newPostion;

        //    Vector3 graphicPosition = GraphicObject.Position;
        //    Animation.Play(graphicPosition, new Vector3(newPostion.X, graphicPosition.Y, newPostion.Y));
        //    return true;
        //}
    }
}
