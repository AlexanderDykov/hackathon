using Core.Contexts;
using GameScene.ECS.Components;
using GameScene.Factories;
using GameScene.Utils;
using UnityEngine;

namespace GameScene.ECS.Systems.Skill
{
    public class CreateStatueBySkillSystem: CreateCreatureBySkillSystem
    {

        protected override bool CheckSkillType(GameEntity entity)
        {
            return entity.skill.Type == SkillType.CreateStatue || entity.skill.Type == SkillType.CreateBlackStatue;
        }

        protected override void CreateCreature(GameEntity entity, Vector3 transformPosition)
        {
            if(entity.skill.Type == SkillType.CreateStatue)
                MonsterFactory.CreateStatue(Grid.WorldToCell(transformPosition));
            else
                MonsterFactory.CreateBlackStatue(Grid.WorldToCell(transformPosition));
        }

        public CreateStatueBySkillSystem(IGameContext context, MonsterFactory monsterFactory, Grid grid) : base(context, monsterFactory, grid)
        {
        }
    }
}
