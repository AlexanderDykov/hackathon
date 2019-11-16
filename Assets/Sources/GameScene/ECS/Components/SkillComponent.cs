using Entitas;

namespace GameScene.ECS.Components
{
    [Game]
    public class SkillComponent : IComponent
    {
        public SkillType Type;
    }
}