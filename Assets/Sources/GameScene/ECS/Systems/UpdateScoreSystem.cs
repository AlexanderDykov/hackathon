using System;
using Core.Contexts;
using Entitas;
using GameScene.Utils;
using UnityEngine;

namespace GameScene.ECS.Systems
{
    public class UpdateScoreSystem : IExecuteSystem
    {
        private IGameContext _context;
        private float _updateScoreTimer = Settings.UpdateScorePeriodSeconds;

        public UpdateScoreSystem(IGameContext context)
        {
            _context = context;
        }

        public void Execute()
        {
            _updateScoreTimer -= Time.deltaTime;
            if (_updateScoreTimer < 0) {
                var scoreDiff = 0;
                if (_context.balance.Value != 0)
                {
                    scoreDiff = 1;
                    if (Math.Abs(_context.balance.Value) > 0.2 * Settings.MaxBalance)
                    {
                        scoreDiff = -1;
                    }
                }
                _context.ReplaceScore(_context.score.Value + scoreDiff);
                _updateScoreTimer = Settings.UpdateScorePeriodSeconds;
            }

        }
    }

}
