using System.Collections.Generic;
using System.Linq;
using Core.Contexts;
using GameScene.ECS.Components;
using GameScene.Utils;
using UnityEngine;

namespace GameScene.Factories
{
    public interface IBoxFactory
    {
        GameEntity CreateEntity(IGameContext context, Vector2 position, TileType tileType);
    }

    public class BoxFactory : IBoxFactory
    {
        private static Skill _statueSkill = new Skill("Create statue", SkillType.CreateStatue );
        private static readonly Skill CreateWaterTileSkill = new Skill("Create water tile", SkillType.CreateWater);
        private static readonly Skill CreateAirTileSkill = new Skill("Create air tile", SkillType.CreateAir );
        private static readonly Skill CreateFireTileSkill = new Skill("Create fire tile", SkillType.CreateFire );
        private static readonly Skill CreateEarthTileSkill = new Skill("Create earth tile", SkillType.CreateEarth );
        private static readonly Skill CreateSoulSkill = new Skill("Create soul", SkillType.CreateSoul );

        public GameEntity CreateEntity(IGameContext context, Vector2 position, TileType tileType)
        {
            var playerEntity = context.CreateEntity();
            playerEntity.AddInitialPosition(position);
            playerEntity.AddBoxSkills(SkillsByTileType(tileType));
            playerEntity.AddResource(ResourceNames.Box);
            return playerEntity;
        }

        private readonly Dictionary<TileType, List<SkillAvl>> _hardcodedSkills = new Dictionary<TileType, List<SkillAvl>>()
        {
            {
                TileType.None,
                new List<SkillAvl>
                {
                    new SkillAvl(CreateWaterTileSkill, 50),
                    new SkillAvl(CreateAirTileSkill, 50),
                    new SkillAvl(CreateFireTileSkill, 25),
                    new SkillAvl(CreateEarthTileSkill, 25)
                }
            },
            {
                TileType.Air,
                new List<SkillAvl>
                {
                    new SkillAvl(CreateSoulSkill, 100),
                }
        }
        };
        
        private List<Skill> SkillsByTileType(TileType level)
        {
            return _hardcodedSkills.ContainsKey(level) ? GetTwoRandomLevels(_hardcodedSkills[level]) : new List<Skill>(){new SkillAvl(CreateSoulSkill, 100)};
        }

        private List<Skill> GetTwoRandomLevels( List<SkillAvl> skills)
        {
            if (skills.Count <= 1)
            {
                return skills.Select(skill => (Skill) skill).ToList();
            }
            
            var firstRndIndex = GetRandomIndex(skills);
            if (firstRndIndex == -1) return new List<Skill>();
            int secondRndIndex;
            do
            {
                secondRndIndex = GetRandomIndex(skills);
            } while (firstRndIndex == secondRndIndex);
                
            return new List<Skill>()
            {
                skills[firstRndIndex],
                skills[secondRndIndex]
            };
        }

        private static int GetRandomIndex(List<SkillAvl> skills)
        {
            var sum = skills.Sum(t => t.Probability);
            var rndVal = Random.Range(0, sum + 1);
            var defValue = 0;
            for (var i = 0; i < skills.Count; i++)
            {
                defValue += skills[i].Probability;
                if (rndVal <= defValue)
                {
                    return i;
                }
            }
            
            return -1;
        }

        class SkillAvl
        {
            public Skill Skill;
            public int Probability;

            public SkillAvl(Skill skill, int probability)
            {
                Skill = skill;
                Probability = probability;
            }
            
            public static implicit operator Skill(SkillAvl param)
            {
                return param.Skill;
            }
        }
    }
}