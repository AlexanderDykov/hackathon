using Core.Contexts;
using GameScene.ECS.Components;
using GameScene.Utils;
using UnityEngine;

namespace GameScene.ECS.Systems.Skill
{
    public class CreateStatueBySkillSystem: CreateCreatureBySkillSystem
    {

        protected override bool CheckSkillType(GameEntity entity)
        {
            return entity.skill.Type == SkillType.CreateStatue;
        }

        protected override void CreateCreature(GameEntity entity, Vector3 transformPosition)
        {
//            soulEntity.AddResource(ResourceNames.Statue);
//            soulEntity.AddInitialPosition(transformPosition);
//            soulEntity.AddCreatureType(CreatureType.Statue);
        }

        public CreateStatueBySkillSystem(IGameContext context) : base(context)
        {
        }
    }
}
