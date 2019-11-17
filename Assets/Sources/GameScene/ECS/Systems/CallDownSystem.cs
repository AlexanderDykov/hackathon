using Core.Contexts;
using Entitas;
using UnityEngine;

namespace GameScene.ECS.Systems
{
    public class CallDownSystem : IExecuteSystem
    {
        readonly IGroup<GameEntity> _resetCallDownGroup;
        
        public CallDownSystem(IGameContext context)
        {
            _resetCallDownGroup = context.GetGroup(GameMatcher.AllOf(GameMatcher.Calldown));
        }
        
        public void Execute()
        {
            foreach (var gameEntity in _resetCallDownGroup)
            {
                if (!(gameEntity.calldown.Value > 0.001f)) continue;
                gameEntity.ReplaceCalldown(gameEntity.calldown.Value - Time.deltaTime);
            }
        }
    }
}