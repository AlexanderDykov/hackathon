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
                gameEntity.target.Entity.AddDamage(-gameEntity.healPower.Value);
                gameEntity.ReplaceCalldown(gameEntity.initialCalldown.Value);
            }
        }
    }
}
