using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoOpenTK
{
    public enum GameObjectType : byte
    {
        None,
        LightObject,
        HeavyObject,
        BorderObject,
        Player,
        Bomb,
        Monster,
        Field
    }
}
