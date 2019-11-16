using Entitas;

namespace GameScene.ECS.Components
{
    [Game]
    public class CreatureTypeComponent : IComponent
    {
        public CreatureType Value;
    }

    public enum CreatureType
    {
        Soul,
        Statue,
        Zombie,
        Human,
        Skeleton
    }
}