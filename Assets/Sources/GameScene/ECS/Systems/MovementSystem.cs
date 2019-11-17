using System.Collections.Generic;
using Core.Contexts;
using Entitas;

namespace GameScene.ECS.Systems
{
    public sealed class MovementSystem : ReactiveSystem<GameEntity>
    {
        readonly IGroup<GameEntity> _movableGroup;

        public MovementSystem(IGameContext gameContext) : base(gameContext)
        {
            _movableGroup = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.Body, GameMatcher.Direction, GameMatcher.Speed));
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Direction.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasDirection;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in _movableGroup)
            {
                entity.body.Value.velocity = entity.direction.Value * entity.speed.Value;
            }
        }
    }
}
