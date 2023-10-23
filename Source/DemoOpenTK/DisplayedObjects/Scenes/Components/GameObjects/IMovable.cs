using OpenTK.Mathematics;

namespace DemoOpenTK
{
    public interface IMovable
    {
        MovedObjectState State { get; }
        /// <summary>
        /// Перемещает объект на отрезок равный shift.
        /// </summary>
        /// <returns>Возвращает true в случае успешного перемещения, елси перемещение невозможно вернет false</returns>
        bool TryMove(Vector2i shift);
    }
}
