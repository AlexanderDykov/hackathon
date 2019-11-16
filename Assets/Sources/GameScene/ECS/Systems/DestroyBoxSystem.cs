using System.Collections.Generic;
using Core.Contexts;
using Entitas;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameScene.ECS.Systems
{
    public class DestroyBoxSystem : ReactiveSystem<GameEntity>
    {
        private IGameContext _context;
        public DestroyBoxSystem(IGameContext context) : base(context)
        {
            _context = context;
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
                Object.Destroy(entity.view.Value);
                entity.isDestroy = true;
            }
        }
    }
}