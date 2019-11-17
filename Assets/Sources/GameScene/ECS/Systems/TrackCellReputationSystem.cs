using System.Collections.Generic;
using Core.Contexts;
using Entitas;
using GameScene.ECS.Components;
using UnityEngine;

namespace GameScene.ECS.Systems
{
    public class TrackCellReputationSystem : ReactiveSystem<GameEntity>
    {
        private IGameContext _context;
        private Grid _grid;

        public TrackCellReputationSystem(IGameContext context, Grid grid) : base(context)
        {
            _context = context;
            _grid = grid;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Reputation.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasCell && entity.hasReputation;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            Debug.Log("lol");
            foreach (var entity in entities)
            {
                TileType reputationTile = (entity.reputation.Value > 0) ? TileType.White : TileType.Black;
                if ((entity.hasTile == false) || (entity.tile.TileType != reputationTile)) {
                    var tilePos = _grid.CellToWorld(entity.cell.Position);
                    entity.ReplaceTile(tilePos, reputationTile);
                }
            }
        }
    }
}
