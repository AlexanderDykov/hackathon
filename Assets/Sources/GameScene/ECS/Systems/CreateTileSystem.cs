using System.Collections.Generic;
using Core.Contexts;
using Entitas;

namespace GameScene.ECS.Systems
{
    public class CreateTileSystem  : ReactiveSystem<GameEntity>
    {
        private IGameContext _context;
        public CreateTileSystem(IGameContext context) : base(context)
        {
            _context = context;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Tile.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasTile;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.ReplaceResource(entity.tile.TileType.ToString());
                entity.ReplaceInitialPosition(entity.tile.Position);
            }
        }
    }
}
