using System.Collections.Generic;
using Core.Contexts;
using Entitas;
using GameScene.ECS.Components;
using GameScene.Utils;

namespace GameScene.ECS.Systems.Skill
{
    public abstract class CreateCreatureSkillSystem: ReactiveSystem<GameEntity>
    {
        private IGameContext _context;

        protected CreateCreatureSkillSystem(IGameContext context) : base(context)
        {
            _context = context;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Skill.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasSkill && entity.hasView && CheckSkillType(entity);
        }


        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var soulEntity = _context.CreateEntity();
                soulEntity.AddInitialPosition(entity.view.Value.transform.position);
                CreateCreature(soulEntity);
            }
        }

        protected abstract bool CheckSkillType(GameEntity entity);
        protected abstract void CreateCreature(GameEntity soulEntity);
    }
}