using Core.Contexts;
using Entitas;
using GameScene.ECS.Utils;
using GameScene.Factories;
using UnityEngine;

namespace GameScene.ECS.Systems
{
    public class CreateNewCreatureSystem : IExecuteSystem
    {
        readonly IGroup<GameEntity> _attackGroup;
        private readonly MonsterFactory _factory;
        private RandomPositionGenerator _randomPositionGenerator;
        
        public CreateNewCreatureSystem(IGameContext context, MonsterFactory factory, RandomPositionGenerator randomPositionGenerator)
        {
            _factory = factory;
            _attackGroup = context.GetGroup(GameMatcher.AllOf(
                GameMatcher.Creator,
                GameMatcher.Calldown,
                GameMatcher.InitialCalldown,
                GameMatcher.Target));
            _randomPositionGenerator = randomPositionGenerator;
        }
        
        public void Execute()
        {
            foreach (var gameEntity in _attackGroup)
            {
                if (!(gameEntity.calldown.Value < 0.001f)) continue;
                _factory.Create(gameEntity.creator.Value, gameEntity.target.Entity.view.Value.transform.position);
                
                if(gameEntity.target.Entity.creatureType.Value == CreatureType.Position) 
                    gameEntity.target.Entity.ReplaceInitialPosition(_randomPositionGenerator.RandomPosition());
                
                gameEntity.ReplaceCalldown(gameEntity.initialCalldown.Value);
            }
        }
    }
}