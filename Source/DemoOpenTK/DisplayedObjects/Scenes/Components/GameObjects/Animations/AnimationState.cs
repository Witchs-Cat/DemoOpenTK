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
        /// Анимация была завершена
        /// </summary>
        Complitied,
        /// <summary>
        /// Проигрывается анимация
        /// </summary>
        Played,
    }
}
