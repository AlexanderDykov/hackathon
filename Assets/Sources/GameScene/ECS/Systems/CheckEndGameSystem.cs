using System.Collections.Generic;
using Core.Contexts;
using Entitas;
using UnityEngine;

namespace GameScene.ECS.Systems
{
    public class CheckEndGameSystem: ReactiveSystem<GameEntity>
    {
        private IGameContext _context;
        public CheckEndGameSystem(IGameContext context) : base(context)
        {
            _context = context;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Life.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasLife;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.life.Value <= 0)
                {
                    _context.isEndGame = true;
                }
            }
        }
    }
}