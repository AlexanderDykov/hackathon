using System.Collections.Generic;
using Core.Contexts;
using Entitas;
using GameScene.ECS.Components;
using GameScene.Utils;
using UnityEngine;

namespace GameScene.ECS.Systems.Skill
{
    public class CreateSoulSkillSystem : CreateCreatureSkillSystem
    {
        protected override bool CheckSkillType(GameEntity entity)
        {
            return entity.skill.Type == SkillType.CreateSoul;
        }

        protected override void CreateCreature(GameEntity soulEntity)
        {
            soulEntity.AddResource(ResourceNames.Soul);
            soulEntity.AddCreatureType(CreatureType.Soul);
        }

        public CreateSoulSkillSystem(IGameContext context) : base(context)
        {
        }
    }
}