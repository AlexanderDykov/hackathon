using System.Collections.Generic;
using System.Linq;
using Core.Contexts;
using Entitas;
using GameScene.ECS.Components;
using GameScene.ECS.Utils;
using GameScene.Factories;
using GameScene.View;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameScene.ECS.Systems
{
    public class DestroyBoxSystem : ReactiveSystem<GameEntity>
    {
        private IGameContext _context;
        private IBoxFactory _boxFactory;
        private readonly RandomPositionGenerator _positionGenerator;
        public DestroyBoxSystem(IGameContext context, IBoxFactory boxFactory,
            RandomPositionGenerator positionGenerator) : base(context)
        {
            _context = context;
            _boxFactory = boxFactory;
            _positionGenerator = positionGenerator;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Skill.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasSkill && entity.hasView;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.view.Value.GetComponent<BoxView>().Open(false);
                Object.Destroy(entity.view.Value);
                entity.isDestroy = true;

                var randomPosition = _positionGenerator.RandomPosition();
                
                var tiles = _context.GetEntitiesWithIndexTilePosition(randomPosition).Where(x => x.hasTile);

                var level = TileType.None;
                var gameEntities = tiles.ToList();
                if (gameEntities.Any())
                {
                    level = gameEntities.First().tile.TileType;
                }
                _boxFactory.CreateEntity(_context, randomPosition, level);
            }
        }
    }
}