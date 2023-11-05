using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DemoOpenTK.DisplayedObjects.Scenes.Components.GameObjects
{
    public class Monster : MovedGameObject
    {
        public Monster(GameObjectConfig config) : base(config)
        {
        }

        public override bool TryMove(Vector2i shift)
        {
            return false;
        }
    }
}
