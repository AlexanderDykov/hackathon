using Core.Contexts;
using Entitas;
using UnityEngine;

namespace GameScene.ECS.Systems
{
    public class AttackSystem : IExecuteSystem
    {
        readonly IGroup<GameEntity> _attackGroup;
        
        public AttackSystem(IGameContext context)
        {
            _attackGroup = context.GetGroup(GameMatcher.AllOf(GameMatcher.Target, GameMatcher.AttackPower, GameMatcher.Calldown, GameMatcher.InitialCalldown));
        }
        
        public void Execute()
        {
            foreach (var gameEntity in _attackGroup)
            {
                if (gameEntity.calldown.Value <= 0f)
                {
                    var damage = gameEntity.target.Entity.hasDamage ? gameEntity.target.Entity.damage.Value : 0;
                    gameEntity.target.Entity.ReplaceDamage(damage + gameEntity.attackPower.Value);
                    gameEntity.ReplaceCalldown(gameEntity.initialCalldown.Value);
                }
            }
        }
    }
}
