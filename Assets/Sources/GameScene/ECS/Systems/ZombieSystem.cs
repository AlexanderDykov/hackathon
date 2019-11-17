using Core.Contexts;
using Entitas;
using GameScene.Factories;
using UnityEngine;

namespace GameScene.ECS.Systems
{
    public class ZombieSystem: IExecuteSystem
    {
        readonly IGroup<GameEntity> _zombieGroup;
        private readonly MonsterFactory _monsterFactory;
        public ZombieSystem(IGameContext context, MonsterFactory monsterFactory)
        {
            _monsterFactory = monsterFactory;
            _zombieGroup = context.GetGroup(GameMatcher.AllOf(GameMatcher.Target, GameMatcher.ZombieTimer, GameMatcher.Calldown, GameMatcher.InitialCalldown));
        }

        public void Execute()
        {
            foreach (var gameEntity in _zombieGroup)
            {
                if ((gameEntity.calldown.Value <= 0f) && (gameEntity.zombieTimer.Value <= 0f))
                {
                    gameEntity.target.Entity.isDestroy = true;
                    Object.Destroy( gameEntity.target.Entity.view.Value);

                    _monsterFactory.CreateSkeleton(gameEntity.target.Entity.view.Value.transform.position);

                    gameEntity.ReplaceCalldown(gameEntity.initialCalldown.Value);
                    gameEntity.ReplaceZombieTimer(3);
                }
            }
        }
    }
}
