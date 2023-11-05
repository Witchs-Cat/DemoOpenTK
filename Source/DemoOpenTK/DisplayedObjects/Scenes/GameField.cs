using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace DemoOpenTK
{
    public class GameField
    {
        private readonly GameScene _scene;
        private readonly ImmutableArray<GameObjectType> _passabilityMap;
        private readonly IDictionary<Vector2i, BaseGameObject> _layout;
        private readonly GameObjectsFactory _gameObjectsFactory;
        private readonly Random _random;

        public GameField(GameScene scene, GameObjectsFactory gameObjectsFactory,  ImmutableArray<GameObjectType> passabilityMap)
        {
            _scene = scene;
            _passabilityMap = passabilityMap;
            _gameObjectsFactory = gameObjectsFactory;
            _random = new Random();
            _layout = new Dictionary<Vector2i, BaseGameObject>();

            _scene.Load += Setup;
        }

        public ImmutableDictionary<Vector2i, BaseGameObject> Layout => _layout.ToImmutableDictionary();

        public void Setup()
        {
            BaseGameObject gameObject;

            int size = (int)MathF.Sqrt(_passabilityMap.Length);
            bool[,] occupiedCells = new bool[size, size];

            for (int i = 0; i < _passabilityMap.Length; i++)
            {
                GameObjectType objectType = _passabilityMap[i];
                if (objectType == GameObjectType.None)
                    continue;

                int y = i % size, x = i / size;
                
                gameObject = _gameObjectsFactory.Create(this, objectType, x, y);
                _layout.Add(gameObject.Position, gameObject);

                occupiedCells[x, y] = true;
            }
            gameObject = _gameObjectsFactory.Create(this, GameObjectType.Field, 10, 10, -0.5f);
            
            if (!TryFindRandomFreeCell(occupiedCells, out Vector2i freeCell))
                return;

            gameObject = _gameObjectsFactory.Create(this, GameObjectType.Player, freeCell.X, freeCell.Y);
            _layout.Add(freeCell, gameObject);

            for (int i = 0; i < 3; i++)
            {
                if (!TryFindRandomFreeCell(occupiedCells, out freeCell))
                    return;

                gameObject = _gameObjectsFactory.Create(this, GameObjectType.Monster, freeCell.X, freeCell.Y);
            }
        }

        private static bool InRange(Vector2i cell, int minValue, int maxValue)
            => cell.X <= maxValue && cell.X >= minValue && cell.Y <= maxValue && cell.Y >= minValue;

        private bool TryFindRandomFreeCell(bool[,] occupiedCells, out Vector2i freeCell)
        {
            int size = occupiedCells.GetLength(0);

            int x = _random.Next(0, size);
            int y = _random.Next(0, size);

            bool[,] viewedCells = new bool[size, size];
            Stack<Vector2i> stack = new();
            stack.Push(new Vector2i(x, y));

            while (stack.Any())
            {
                Vector2i cell = stack.Pop();
                if (!occupiedCells[cell.X, cell.Y])
                {
                    freeCell = cell;
                    return true;
                }

                viewedCells[cell.X, cell.Y] = true;


                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        Vector2i newCell = new (cell.X + i, cell.Y+j);
                        if (!InRange(newCell, 0, size - 1))
                            continue;

                        if (viewedCells[newCell.X, newCell.Y])
                            continue;

                        stack.Push(newCell);
                    }
                }
            }

            freeCell = new Vector2i(-1, -1);
            return false;
        }

        public void OnObjectMove(Vector2i newPosition, Vector2i prevPosition, BaseGameObject gameObject)
        {
            _layout.Remove(prevPosition);
            _layout.Add(newPosition, gameObject);
        }
    }
}
