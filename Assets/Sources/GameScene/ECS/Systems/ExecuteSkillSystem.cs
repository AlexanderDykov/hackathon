using System.Collections.Generic;
using Core.Contexts;
using Entitas;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameScene.ECS.Systems
{
    public class ExecuteSkillSystem : ReactiveSystem<GameEntity>
    {
        public ExecuteSkillSystem(IGameContext context) : base(context)
        {
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
                // TODO: move it to next position
                Debug.Log("Execute " + entity.skill.Type);
                Object.Destroy(entity.view.Value);
            }
        }
    }
}