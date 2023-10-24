using DemoOpenTK.DisplayedObjects.Scenes.Components.GameObjects.Animations;
using Microsoft.Extensions.Logging;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System.Xml;

namespace DemoOpenTK
{
    public class MovedGameObject : BaseGameObject, IMovable
    {
        protected readonly MoveAnimation Animation;
        public MovedGameObject(GameObjectConfig config) : base(config)
        {
            Animation = new(config.GraphicObject);
        }

        public override void OnUpdateFrame(FrameEventArgs args)
        {
            Animation.OnUpdateFrame(args);   
        }

        ///<inheritdoc/>
        public virtual bool TryMove(Vector2i shift)
        {
            if (Animation.State != AnimationState.Inactive)
                return false;

            Vector2i newPostion = Position + shift;
            if (Field.Layout.ContainsKey(newPostion))
                return false;

            Field.OnObjectMove(newPostion, Position, this);
            Position = newPostion;

            Vector3 graphicPosition = GraphicObject.Position;
            Animation.Play(graphicPosition, new Vector3(newPostion.X, graphicPosition.Y, newPostion.Y));
            return true;
        }
    }
}
