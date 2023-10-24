using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoOpenTK
{
    public enum AnimationState
    {

        /// <summary>
        /// Ожидание анимации
        /// </summary>
        Inactive,
        /// <summary>
        /// Анимация была остановлена
        /// </summary>
        Stoped,
        /// <summary>
        /// Проигрывается анимация
        /// </summary>
        Played,
    }
}
