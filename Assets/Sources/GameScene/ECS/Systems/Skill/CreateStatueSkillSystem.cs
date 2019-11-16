using Core.Contexts;
using GameScene.ECS.Components;
using GameScene.Utils;

namespace GameScene.ECS.Systems.Skill
{
    public class CreateStatueSkillSystem: CreateCreatureSkillSystem
    {
        protected override bool CheckSkillType(GameEntity entity)
        {
            return entity.skill.Type == SkillType.CreateStatuya;
        }

        protected override void CreateCreature(GameEntity soulEntity)
        {
            soulEntity.AddResource(ResourceNames.Statue);
            soulEntity.AddCreatureType(CreatureType.Statue);
        }

        public CreateStatueSkillSystem(IGameContext context) : base(context)
        {
        }
    }
}