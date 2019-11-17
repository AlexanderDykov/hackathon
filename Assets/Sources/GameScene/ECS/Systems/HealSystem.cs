using Core.Contexts;
using Entitas;

namespace GameScene.ECS.Systems
{
    public class HealSystem : IExecuteSystem
    {
        readonly IGroup<GameEntity> _healGroup;

        public HealSystem(IGameContext context)
        {
            _healGroup = context.GetGroup(GameMatcher.AllOf(GameMatcher.Target, GameMatcher.HealPower, GameMatcher.Calldown, GameMatcher.InitialCalldown));
        }

        public void Execute()
        {
            foreach (var gameEntity in _healGroup)
            {
                if (!(gameEntity.calldown.Value < 0.001f)) continue;
                
                var damage = gameEntity.target.Entity.hasDamage ? gameEntity.target.Entity.damage.Value : 0;
                gameEntity.target.Entity.ReplaceDamage(damage - gameEntity.healPower.Value);
                gameEntity.ReplaceCalldown(gameEntity.initialCalldown.Value);
            }
        }
    }
}
