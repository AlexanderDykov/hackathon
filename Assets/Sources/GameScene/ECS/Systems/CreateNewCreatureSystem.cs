using Core.Contexts;
using Entitas;
using GameScene.Factories;
using UnityEngine;

namespace GameScene.ECS.Systems
{
    public class CreateNewCreatureSystem : IExecuteSystem
    {
        readonly IGroup<GameEntity> _attackGroup;
        readonly IGroup<GameEntity> _resetCallDownGroup;
        private readonly MonsterFactory _factory;
        
        public CreateNewCreatureSystem(IGameContext context, MonsterFactory factory)
        {
            _factory = factory;
            _attackGroup = context.GetGroup(GameMatcher.AllOf(
                GameMatcher.Creator,
                GameMatcher.Calldown,
                GameMatcher.InitialCalldown));
            _resetCallDownGroup = context.GetGroup(GameMatcher.AllOf(GameMatcher.Creator, GameMatcher.Calldown));
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
                if (!gameEntity.hasBuildTarget || !gameEntity.isCanBuild || !(gameEntity.calldown.Value < 0.001f)) continue;
                _factory.Create(gameEntity.creator.Value, gameEntity.buildTarget.Position);
                gameEntity.ReplaceCalldown(gameEntity.initialCalldown.Value);
                
                gameEntity.isCanBuild = false;
                gameEntity.RemoveBuildTarget();
            }
        }
    }
}