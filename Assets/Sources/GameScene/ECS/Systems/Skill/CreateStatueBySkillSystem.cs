using Core.Contexts;
using GameScene.ECS.Components;
using GameScene.Utils;

namespace GameScene.ECS.Systems.Skill
{
    public class CreateStatueBySkillSystem: CreateCreatureBySkillSystem
    {
        protected override bool CheckSkillType(GameEntity entity)
        {
            return entity.skill.Type == SkillType.CreateStatue;
        }

        protected override void CreateCreature(GameEntity soulEntity)
        {
            soulEntity.AddResource(ResourceNames.Statue);
            soulEntity.AddCreatureType(CreatureType.Statue);
        }

        public CreateStatueBySkillSystem(IGameContext context) : base(context)
        {
        }
    }
}