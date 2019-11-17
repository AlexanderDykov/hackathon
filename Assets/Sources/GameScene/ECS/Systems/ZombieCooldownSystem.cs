using Core.Contexts;
using Entitas;
using UnityEngine;

namespace GameScene.ECS.Systems
{
    public class ZombieCooldownSystem: IExecuteSystem
    {
        readonly IGroup<GameEntity> _zombieGroup;
        
        public ZombieCooldownSystem(IGameContext context)
        {
            _zombieGroup = context.GetGroup(GameMatcher.AllOf(GameMatcher.Target, GameMatcher.ZombieTimer));
        }
        
        public void Execute()
        {
            foreach (var gameEntity in _zombieGroup)
            {
                if (!(gameEntity.zombieTimer.Value > 0.001f)) continue;
                gameEntity.ReplaceZombieTimer(gameEntity.zombieTimer.Value - Time.deltaTime);
            }
        }
    }
}