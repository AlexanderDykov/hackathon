using Core.Contexts;
using Entitas;
using UnityEngine;

namespace GameScene.ECS.Systems
{
    public class AttackSystem : IExecuteSystem
    {
        private IGameContext _context;
        readonly IGroup<GameEntity> _attackGroup;
        readonly IGroup<GameEntity> _resetCallDownGroup;
        
        public AttackSystem(IGameContext context)
        {
            _context = context;
            _attackGroup = context.GetGroup(GameMatcher.AllOf(GameMatcher.AttackTarget, GameMatcher.AttackPower, GameMatcher.Calldown, GameMatcher.InitialCalldown));
            _resetCallDownGroup = context.GetGroup(GameMatcher.AllOf(GameMatcher.AttackPower, GameMatcher.Calldown));
        }
        
        public void Execute()
        {
            foreach (var gameEntity in _resetCallDownGroup)
            {
                if (!(gameEntity.calldown.Value > 0.001f)) continue;
                gameEntity.ReplaceCalldown(gameEntity.calldown.Value - Time.deltaTime);
            }

            foreach (var gameEntity in _attackGroup)
            {
                if (!(gameEntity.calldown.Value < 0.001f)) continue;
                gameEntity.attackTarget.Value.AddDamage(gameEntity.attackPower.Value);
                gameEntity.ReplaceCalldown(gameEntity.initialCalldown.Value);
            }
        }
    }
}