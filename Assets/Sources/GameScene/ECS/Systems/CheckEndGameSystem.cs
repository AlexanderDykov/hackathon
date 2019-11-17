using System;
using System.Collections.Generic;
using Core.Contexts;
using Entitas;
using GameScene.Utils;
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
                if (Math.Abs(entity.balance.Value) == Settings.MaxBalance)
                {
                    Debug.Log("You're looser!");
                    _context.isEndGame = true;
                }
            }
        }
    }
}
