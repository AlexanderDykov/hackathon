using System.Collections.Generic;
using Core.Contexts;
using Entitas;
using GameScene.ECS.Components;
using UnityEngine;

namespace GameScene.ECS.Systems.Skill
{
    public class CreateSoulSkillSystem : ReactiveSystem<GameEntity>
    {
        public CreateSoulSkillSystem(IGameContext context) : base(context)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Skill.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasSkill && entity.hasView && entity.skill.Type == SkillType.CreateSoul;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                // TODO: move it to next position
                Debug.Log("Execute " + entity.skill.Type);
            }
        }
    }
}