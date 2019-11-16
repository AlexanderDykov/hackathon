using System.Collections.Generic;
using Core.Contexts;
using GameScene.ECS.Components;
using GameScene.Utils;
using UnityEngine;

namespace GameScene.Factories
{
    public interface IBoxFactory
    {
        GameEntity CreateEntity(IGameContext context, Vector2 position, int level);
    }

    public class BoxFactory : IBoxFactory
    {
        private Skill statueSkill = new Skill("Create statue", SkillType.CreateStatue );

        public GameEntity CreateEntity(IGameContext context, Vector2 position, int level)
        {
            var playerEntity = context.CreateEntity();
            playerEntity.AddInitialPosition(position);
            playerEntity.AddBoxSkills(SkillsByLevel(level));
            playerEntity.AddResource(ResourceNames.Box);
            return playerEntity;
        }

        Dictionary<int, List<Skill>> hardcodedSkills = new Dictionary<int, List<Skill>>()
        {
            {0,
                new List<Skill>()
                {
                    new Skill("Create water tile", SkillType.CreateWater),
                    new Skill("Create air tile", SkillType.CreateAir ),
                    new Skill("Create fire tile", SkillType.CreateFire ),
                    new Skill("Create soul tile", SkillType.CreateSoul ), 
                    new Skill("Create earth tile", SkillType.CreateEarth )
                }
            }
        };
        private List<Skill> SkillsByLevel(int level)
        {
            return hardcodedSkills.ContainsKey(level) ? GetTwoRandomLevels(hardcodedSkills[level]) : new List<Skill>();
        }

        private List<Skill> GetTwoRandomLevels( List<Skill> skills)
        {
            int firstRndIndex = Random.Range(0, skills.Count);
            int secondRndIndex;
            do
            {
                secondRndIndex = Random.Range(0, skills.Count);
            } while (firstRndIndex == secondRndIndex);
            
            return new List<Skill>()
            {
                skills[firstRndIndex],
                skills[secondRndIndex]
            };
        }
    }
}