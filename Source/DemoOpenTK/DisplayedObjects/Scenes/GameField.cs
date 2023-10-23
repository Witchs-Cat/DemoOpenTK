using OpenTK.Mathematics;
using System.Collections.Immutable;

namespace DemoOpenTK
{
    public class GameField
    {
        private readonly ImmutableArray<GameObjectType> _passabilityMap;
        private readonly IDictionary<Vector2i, BaseGameObject> _layout;
        private readonly GameObjectsFactory _gameObjectsFactory;

        public GameField(GameObjectsFactory gameObjectsFactory,  ImmutableArray<GameObjectType> passabilityMap)
        {
            _passabilityMap = passabilityMap;
            _gameObjectsFactory = gameObjectsFactory;
            _layout = new Dictionary<Vector2i, BaseGameObject>();
        }

        public ImmutableDictionary<Vector2i, BaseGameObject> Layout => _layout.ToImmutableDictionary();

        public void Setup()
        {
            BaseGameObject gameObject;

            for (int i = 0; i < _passabilityMap.Length; i++)
            {
                GameObjectType objectType = _passabilityMap[i];
                if (objectType == GameObjectType.None)
                    continue;
                gameObject = _gameObjectsFactory.Create(this, objectType, i % 21, i / 21);
            }
            gameObject = _gameObjectsFactory.Create(this, GameObjectType.Field, 10, 10, -0.5f);
        }
    }
}
