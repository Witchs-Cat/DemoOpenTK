using OpenTK.Mathematics;

namespace DemoOpenTK
{
    public interface IMovable
    {
        /// <summary>
        /// Объект выполнит ход
        /// </summary>
        /// <param name="positon"></param>
        /// <returns>Вернет true если ход возможен, инчае false</returns>
        bool TryMove(Vector2i positon);
    }
}
