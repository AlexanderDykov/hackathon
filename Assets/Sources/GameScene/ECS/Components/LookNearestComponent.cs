using System;
using Entitas;

namespace GameScene.ECS.Components
{
    public class LookNearestComponent: IComponent
    {
        public Predicate<GameEntity> Value;
    }
}
