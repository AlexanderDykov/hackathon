using System;
using System.Collections.Generic;
using Core.Contexts;
using Entitas;
using GameScene.Utils;

namespace GameScene.ECS.Systems
{
    public class UpdateScoreSystem : ReactiveSystem<GameEntity>
    {
        private IGameContext _context;

        public UpdateScoreSystem(IGameContext context) : base(context)
        {
            _context = context;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Balance.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasBalance;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var scoreDiff = 1;
                if (Math.Abs(_context.balance.Value) > 0.9 * Settings.MaxBalance) {
                    scoreDiff = -1;
                }
                _context.ReplaceScore(_context.score.Value + scoreDiff);
            }
        }
    }

}
