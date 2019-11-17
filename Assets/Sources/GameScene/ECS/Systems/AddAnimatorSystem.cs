using System.Collections.Generic;
using Core.Contexts;
using Entitas;
using UnityEngine;

namespace GameScene.ECS.Systems
{
    public sealed class AddAnimatorSystem : ReactiveSystem<GameEntity>
    {
        public AddAnimatorSystem(IGameContext context) : base(context)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.View.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasView && entity.isAnimated && !entity.hasAnimator;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.AddAnimator(entity.view.Value.GetComponent<Animator>());
            }
        }
    }
}