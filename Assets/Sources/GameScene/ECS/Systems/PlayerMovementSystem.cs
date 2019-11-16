using System.Collections.Generic;
using Core.Contexts;
using Entitas;

namespace GameScene.ECS.Systems
{
    public sealed class PlayerMovementSystem : ReactiveSystem<InputEntity>
    {
        readonly IGroup<GameEntity> _movableGroup;
        
        public PlayerMovementSystem(IInputContext inputContext, IGameContext gameContext) : base(inputContext)
        {
            _movableGroup = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.Body, GameMatcher.Player, GameMatcher.Speed));

        }
        
        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.Input.Added());
        }

        protected override bool Filter(InputEntity entity)
        {
            return entity.hasInput;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            foreach (var inputEntity in entities)
            {
                foreach (var entity in _movableGroup)
                {
                    entity.body.Value.velocity = inputEntity.input.Value * entity.speed.Value;
                }
            }
        }
    }
}