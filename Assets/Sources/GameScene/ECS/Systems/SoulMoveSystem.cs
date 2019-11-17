using System.Linq;
using Core.Contexts;
using Entitas;
using UnityEngine;

namespace GameScene.ECS.Systems
{
    public class SoulMoveSystem : IExecuteSystem
    {
        readonly IGroup<GameEntity> _attackGroup;
        IGameContext _context;
        
        public SoulMoveSystem(IGameContext context)
        {
            _context = context;
            _attackGroup = context.GetGroup(GameMatcher.AllOf(GameMatcher.Target, GameMatcher.Soul));
        }
        
        public void Execute()
        {
            foreach (var gameEntity in _attackGroup)
            {
                Object.Destroy(gameEntity.view.Value);
                gameEntity.isDestroy = true;
                _context.CreateEntity().AddUpgrade(gameEntity.target.Entity);
            }
        }
    }
}