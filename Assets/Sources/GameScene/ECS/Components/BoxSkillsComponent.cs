using System;
using System.Collections.Generic;
using Entitas;

namespace GameScene.ECS.Components
{
    [Game]
    public class BoxSkillsComponent : IComponent
    {
        public List<Skill> Skills = new List<Skill>();
    }

    [Serializable]
    public class Skill
    {
        public Skill( string description, SkillType skillType)
        {
            Description = description;
            SkillType = skillType;
        }
        
        public string Description;
        public SkillType SkillType;
    }

    [Flags]
    public enum SkillType
    {
        CreateStatue,
        CreateEarth = 1 << 1,
        CreateWater = 1 << 2,
        CreateAir = 1 << 3,
        CreateFire = 1 << 4,
        CreateSoul = 1 << 5,
        Tile = CreateWater | CreateFire | CreateAir | CreateEarth | CreateSoul
    }
}
