using Core.Contexts;
using Entitas;
using GameScene.ECS.Components;
using GameScene.ECS.Utils;
using GameScene.Factories;
using UnityEngine;

namespace GameScene.ECS.Systems
{
    public sealed class InitWorldSystem : IInitializeSystem
    {
        private readonly IGameContext _context;
        private readonly IBoxFactory _boxFactory;
        private readonly RandomPositionGenerator _randomPositionGenerator;
        private MonsterFactory _monsterFactory;
        public InitWorldSystem(IGameContext context, MonsterFactory monsterFactory,
            IBoxFactory boxFactory, RandomPositionGenerator randomPositionGenerator)
        {
            _boxFactory = boxFactory;
            _monsterFactory = monsterFactory;
            _context = context;
            _randomPositionGenerator = randomPositionGenerator;
        }

        public void Initialize()
        {
            var worldSize = _context.worldEntity.world.Size;
            var halfWidth = worldSize.x / 2;
            var halfHeight = worldSize.y / 2;
            for (int i = -halfWidth; i < 0; ++i)
            {
                for (int j = -halfHeight; j < halfHeight; ++j)
                {
                    var whiteCellEntity = _context.CreateEntity();
                    whiteCellEntity.AddCell(new Vector3Int(i, j, 0));
                    whiteCellEntity.AddReputation(1);
                    var blackCellEntity = _context.CreateEntity();
                    blackCellEntity.AddCell(new Vector3Int(-i-1, j, 0));
                    blackCellEntity.AddReputation(-1);
                }
            }
            

            for (int i = 0; i < 6; i++)
            {
                _monsterFactory.CreatePosition(_randomPositionGenerator.RandomPosition());
            }
            
            for (int i = 0; i < 6; i++)
            {
                _boxFactory.CreateEntity(_randomPositionGenerator.RandomPosition());
            }
        }
    }
}
