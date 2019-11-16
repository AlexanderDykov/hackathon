using System.Collections.Generic;
using Core.Contexts;
using Entitas;
using UnityEngine;

namespace GameScene.ECS.Systems.Skill
{
    public abstract class CreateCreatureBySkillSystem: ReactiveSystem<GameEntity>
    {
        protected IGameContext Context;

        protected CreateCreatureBySkillSystem(IGameContext context) : base(context)
        {
            Context = context;
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
                CreateCreature(entity, entity.view.Value.transform.position);
            }
        }

        protected abstract bool CheckSkillType(GameEntity entity);
        protected abstract void CreateCreature(GameEntity soulEntity, Vector3 transformPosition);
    }
}