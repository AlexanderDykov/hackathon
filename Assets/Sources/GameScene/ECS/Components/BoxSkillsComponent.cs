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
        CreateSoul,
        CreateBlackStatue,
    }
}
