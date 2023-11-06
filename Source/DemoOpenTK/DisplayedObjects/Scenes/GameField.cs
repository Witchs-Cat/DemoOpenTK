using OpenTK.Mathematics;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Xml;
using static DemoOpenTK.MatrixHelper;

namespace DemoOpenTK
{
    public class GameField
    {
        private readonly GameScene _scene;
        private readonly GameObjectsFactory _gameObjectsFactory;

        private readonly ImmutableArray<GameObjectType> _passabilityMap;
        private readonly IDictionary<Vector2i, BaseGameObject> _obstacles;

        public GameField(GameScene scene, GameObjectsFactory gameObjectsFactory,  ImmutableArray<GameObjectType> passabilityMap)
        {
            _scene = scene;
            _passabilityMap = passabilityMap;
            _gameObjectsFactory = gameObjectsFactory;

            _obstacles = new Dictionary<Vector2i, BaseGameObject>();

            _scene.Load += Setup;
        }

        /// <summary>
        /// Вызывается при передвижении объекта.
        /// </summary>
        public event Action<BaseGameObject>? ObjectMove;

        public bool TryGetObstacle(Vector2i position, out BaseGameObject? gameObject)
        { 
            return _obstacles.TryGetValue(position, out gameObject);
        }

        public bool CellIsOccupied(Vector2i newPosition)
        {
            return _obstacles.ContainsKey(newPosition);
        }

        public void Remove(BaseGameObject gameObject)
        {
            _obstacles.Remove(gameObject.Position);
            _gameObjectsFactory.AddToDeleteQueue(gameObject);
        }

        public Bomb SetBomb(Vector2i prevPosition)
        {
            Bomb bomb = _gameObjectsFactory.Create(this, GameObjectType.Bomb, prevPosition.X, prevPosition.Y) as Bomb 
                ?? throw new NullReferenceException();
            _obstacles.Add(prevPosition, bomb);
            return bomb;
        }

        public void Setup()
        {
            int size = (int)MathF.Sqrt(_passabilityMap.Length);
            bool[,] occupiedCells = new bool[size, size];

            PlaceObstacles(occupiedCells, size); 
            PlacePlayer(occupiedCells, size);
            PlaceMonsters(occupiedCells, size);
        }

        public void OnObjectMove(Vector2i prevPosition, BaseGameObject gameObject)
        {
            _obstacles.Remove(prevPosition);
            _obstacles.Add(gameObject.Position, gameObject);    

            ObjectMove?.Invoke(gameObject);
        }

        private void PlaceObstacles(bool[,] occupiedCells, int size)
        {
            BaseGameObject gameObject;
            for (int i = 0; i < _passabilityMap.Length; i++)
            {
                GameObjectType objectType = _passabilityMap[i];
                if (objectType == GameObjectType.None)
                    continue;
                int y = i % size, x = i / size;

                gameObject = _gameObjectsFactory.Create(this, objectType, x, y);
                _obstacles.Add(gameObject.Position, gameObject);

                occupiedCells[x, y] = true;
            }
            gameObject = _gameObjectsFactory.Create(this, GameObjectType.Field, 10, 10, -0.5f);
        }

        private void PlaceMonsters(bool[,] occupiedCells, int size)
        {
            BaseGameObject gameObject;
            for (int i = 0; i < 3; i++)
            {
                if (!TryFindRandomFreeCell(occupiedCells, out Vector2i freeCell))
                    return;

                gameObject = _gameObjectsFactory.Create(this, GameObjectType.Monster, freeCell.X, freeCell.Y);
                occupiedCells[freeCell.X, freeCell.Y] = true;
                _obstacles.Add(freeCell, gameObject);
            }
        }

        private void PlacePlayer(bool[,] occupiedCells, int size)
        {
            BaseGameObject gameObject;
            if (!TryFindRandomFreeCell(occupiedCells, out Vector2i freeCell))
                return;

            gameObject = _gameObjectsFactory.Create(this, GameObjectType.Player, freeCell.X, freeCell.Y);
            occupiedCells[freeCell.X, freeCell.Y] = true;
            _obstacles.Add(freeCell, gameObject);
            return;
        }
    }
}
