using System.Collections.Generic;
using Core.Contexts;
using GameScene.ECS.Components;
using GameScene.Utils;
using UnityEngine;

namespace GameScene.Factories
{
    public interface IBoxFactory
    {
        GameEntity CreateEntity(IGameContext context, Vector2 position);
    }

    public class BoxFactory : IBoxFactory
    {
        private Skill soulSkill = new Skill("Create soul", SkillType.CreateSoul );
        private Skill statueSkill = new Skill("Create statue", SkillType.CreateStatuya );
        public GameEntity CreateEntity(IGameContext context, Vector2 position)
        {
            var playerEntity = context.CreateEntity();
            playerEntity.AddInitialPosition(position);
            playerEntity.AddBoxSkills(new List<Skill>()
            {
                soulSkill,
                statueSkill
            });
            playerEntity.AddResource(ResourceNames.Box);
            return playerEntity;
        }
    }
}
