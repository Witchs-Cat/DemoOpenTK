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

            if (animation.State == AnimationState.Complitied)
                AnimationsQueue.Dequeue();
        }

        public override bool TryRemove()
        {
            Field.Remove(this);
            return true;
        }
    }
}
