using Core.Contexts;
using Entitas;
using GameScene.ECS.Components;
using GameScene.Factories;
using UnityEngine;

namespace GameScene.ECS.Systems
{
    public sealed class InitSystem : IInitializeSystem
    {
        private readonly IGameContext _context;
        private readonly IPlayerFactory _playerFactory;
        private readonly IBoxFactory _boxFactory;
        private readonly UIFactory _uiFactory;
        private readonly MonsterFactory _monsterFactory;

        public InitSystem(IGameContext context,
            IPlayerFactory playerFactory,
            IBoxFactory boxFactory,
            UIFactory uiFactory,
            MonsterFactory monsterFactory)
        {
            _context = context;
            _playerFactory = playerFactory;
            _boxFactory = boxFactory;
            _uiFactory = uiFactory;
            _monsterFactory = monsterFactory;
        }

        public void Initialize()
        {
            _playerFactory.CreatePlayer(_context);

            _uiFactory.CreatePlayerHUD(_context);

            _uiFactory.CreateCamera(_context);

            _boxFactory.CreateEntity(_context, Vector2.zero, TileType.None);

            _monsterFactory.CreateHuman(new Vector2(-2, 0));
            _monsterFactory.CreateWarrior(new Vector2(-2, 4));
            _monsterFactory.CreateWorker(new Vector2(-2, 8));
            _monsterFactory.CreateSkeleton(new Vector2(2, 0));
        }
    }
}
