using Core.Contexts;
using Entitas;
using UnityEngine;

namespace GameScene.ECS.Systems
{
    public class CallDownSystem : IExecuteSystem
    {
        readonly IGroup<GameEntity> _resetCallDownGroup;
        readonly IGroup<GameEntity> _resetUpgradeCooldownGroup;

        public CallDownSystem(IGameContext context)
        {
            _resetCallDownGroup = context.GetGroup(GameMatcher.AllOf(GameMatcher.Calldown));
            _resetUpgradeCooldownGroup = context.GetGroup(GameMatcher.UpgradeCooldown);
        }

        public void Execute()
        {
            foreach (var gameEntity in _resetCallDownGroup.GetEntities())
            {
                if (gameEntity.calldown.Value > 0f)
                {
                    gameEntity.ReplaceCalldown(gameEntity.calldown.Value - Time.deltaTime);
                }
            }
            foreach (var gameEntity in _resetUpgradeCooldownGroup.GetEntities())
            {
                if (gameEntity.upgradeCooldown.Value > 0f)
                {
                    gameEntity.ReplaceUpgradeCooldown(gameEntity.upgradeCooldown.Value - Time.deltaTime);
                }
            }
        }
    }
}
