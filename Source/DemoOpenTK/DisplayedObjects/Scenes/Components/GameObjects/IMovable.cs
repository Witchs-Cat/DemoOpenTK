using OpenTK.Mathematics;

namespace DemoOpenTK
{
    public interface IMovable
    {
        /// <summary>
        /// Перемещает объект на отрезок равный shift.
        /// </summary>
        /// <returns>Возвращает true в случае успешного перемещения, елси перемещение невозможно вернет false</returns>
        bool TryMove(Vector2i shift);
    }
}
