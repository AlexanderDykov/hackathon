using Core.Contexts;
using Entitas;
using GameScene.ECS.Utils;
using UnityEngine;

namespace GameScene.ECS.Systems
{
    public class LifeTickSystem: IInitializeSystem, IExecuteSystem
    {
        private const float PeriodSeconds = 1;
        private float _timer = PeriodSeconds;
        private IGameContext _context;

        public LifeTickSystem(IGameContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            _context.ReplaceLifeTimer(Constants.MaxTimerValue);
        }

        public void Execute()
        {
            if (_context.lifeTimer.Value  <= 0) {
                _context.ReplaceLife(--_context.life.Value);
                _context.ReplaceLifeTimer(Constants.MaxTimerValue);
                return;
            }
            _context.ReplaceLifeTimer(_context.lifeTimer.Value - Time.deltaTime);
            
           
        }
    }

}