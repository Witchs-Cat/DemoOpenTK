using OpenTK.Windowing.Common;

namespace DemoOpenTK.DisplayedObjects.Scenes.Components.GameObjects.Animations
{
    internal interface IAnimation
    {
        AnimationState State { get; }

        void OnUpdateFrame( FrameEventArgs args);
        void Play();
        void Stop();
    }
}