using Core.Contexts;
using Entitas;
using GameScene.ECS.Components;
using GameScene.ECS.Utils;
using GameScene.Factories;
using GameScene.Utils;
using UnityEngine;

namespace GameScene.ECS.Systems
{
    public sealed class InitSystem : IInitializeSystem
    {
        private readonly IGameContext _context;
        private readonly IPlayerFactory _playerFactory;
        private readonly IBoxFactory _boxFactory;
        private readonly UIFactory _uiFactory;
        private readonly RandomPositionGenerator _positionGenerator;
        
        public InitSystem(IGameContext context,
            IPlayerFactory playerFactory,
            IBoxFactory boxFactory,UIFactory uiFactory,
            RandomPositionGenerator positionGenerator)
        {
            _context = context;
            _playerFactory = playerFactory;
            _boxFactory = boxFactory;
            _positionGenerator = positionGenerator;
            _uiFactory = uiFactory;
        }
        
        public void Initialize()
        {
            _playerFactory.CreatePlayer(_context);

            _uiFactory.CreatePlayerHUD(_context);

            _uiFactory.CreateCamera(_context);

            for (var i = 0; i < 15; i++)
            {
                _boxFactory.CreateEntity(_context, _positionGenerator.RandomPosition());
            }
            
//            var entity = _context.CreateEntity();
//            entity.AddInitialPosition(new Vector2(2, 0));
//            entity.AddResource(ResourceNames.Human);
//            entity.AddCreatureType(CreatureType.Human);
//            entity.AddHealth(10);
//            entity.isPhysic = true;
//            
//            
//            var sEntity = _context.CreateEntity();
//            sEntity.AddInitialPosition(new Vector2(-2, 0));
//            sEntity.AddResource(ResourceNames.Skeleton);
//            sEntity.AddCreatureType(CreatureType.Skeleton);
//            sEntity.AddHealth(10);
//            sEntity.isPhysic = true;
//            sEntity.AddSpeed(1);
//            sEntity.AddLookNearest(CreatureType.Human);
        }
    }
}