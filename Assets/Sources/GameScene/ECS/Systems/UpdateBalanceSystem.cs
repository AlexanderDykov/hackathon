using System;
using System.Collections.Generic;
using Core.Contexts;
using Entitas;
using GameScene.Utils;
using UnityEngine;

namespace GameScene.ECS.Systems
{
    public class UpdateBalanceSystem : ReactiveSystem<GameEntity>
    {
        private IGameContext _context;

        public UpdateBalanceSystem(IGameContext context) : base(context)
        {
            _context = context;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Reputation.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasReputation;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                int newBalance = _context.balance.Value + entity.reputation.Value;
                if (Math.Abs(newBalance) > Settings.MaxBalance) {
                    newBalance = Math.Sign(newBalance) * Settings.MaxBalance;
                }
                _context.ReplaceBalance(newBalance);
            }
        }
    }

}
