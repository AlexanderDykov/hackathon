using Core.Contexts;
using Entitas;
using GameScene.ECS.Components;
using GameScene.ECS.Utils;
using GameScene.Factories;
using UnityEngine;

namespace GameScene.ECS.Systems
{
    public sealed class InitSystem : IInitializeSystem
    {
        private readonly IGameContext _context;
        private readonly IPlayerFactory _playerFactory;
        private readonly UIFactory _uiFactory;
        private readonly MonsterFactory _monsterFactory;

        public InitSystem(IGameContext context,
            IPlayerFactory playerFactory,
            UIFactory uiFactory,
            MonsterFactory monsterFactory)
        {
            _context = context;
            _playerFactory = playerFactory;
            _uiFactory = uiFactory;
            _monsterFactory = monsterFactory;
        }

        public void Initialize()
        {
            _playerFactory.CreatePlayer(_context);

            _uiFactory.CreatePlayerHUD(_context);

            _uiFactory.CreateCamera(_context);

            _context.ReplaceWorld(new Vector2Int(10, 10));

            _monsterFactory.CreateWorker(new Vector3Int(1, 0, 0));
//            _monsterFactory.CreateWarrior(new Vector2(-2, 4));
//            _monsterFactory.CreateBlackBuilding(new Vector2(-2, 0));
//            _monsterFactory.CreateLich(new Vector2(2, 0));
//            _monsterFactory.CreateSkeleton(new Vector2(2, 0));
//            _monsterFactory.CreateHealer(new Vector2(0, 0));
        }
    }
}
