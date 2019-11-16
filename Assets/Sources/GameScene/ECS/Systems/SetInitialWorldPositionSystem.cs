using System.Collections.Generic;
using Core.Contexts;
using Entitas;

namespace GameScene.ECS.Systems
{
    public class SetInitialWorldPositionSystem: ReactiveSystem<GameEntity>
    {
        
        public SetInitialWorldPositionSystem(IGameContext gameContext) : base(gameContext)
        {

        }
        
        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.InitialPosition.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasView && entity.hasInitialPosition;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.view.Value.transform.position = entity.initialPosition.Value;
            }
        }
    }
}