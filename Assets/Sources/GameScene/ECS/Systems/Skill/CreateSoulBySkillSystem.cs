using Core.Contexts;
using GameScene.ECS.Components;
using GameScene.Factories;
using GameScene.Utils;
using UnityEngine;

namespace GameScene.ECS.Systems.Skill
{
    public class CreateSoulBySkillSystem: CreateCreatureBySkillSystem
    {
        protected override bool CheckSkillType(GameEntity entity)
        {
            return entity.skill.Type == SkillType.CreateSoul;
        }

        protected override void CreateCreature(GameEntity entity, Vector3 transformPosition)
        {
            MonsterFactory.CreateSoul(transformPosition);
        }

        public CreateSoulBySkillSystem(IGameContext context, MonsterFactory monsterFactory) : base(context, monsterFactory)
        {
        }
    }
}