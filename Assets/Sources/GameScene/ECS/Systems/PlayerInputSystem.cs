using Core.Contexts;
using Entitas;
using UnityEngine;

namespace GameScene.ECS.Systems
{
    public sealed class PlayerInputSystem : IExecuteSystem, IInitializeSystem
    {
        private readonly IInputContext _context;
        private const float StartMovingCoefficient = 0.05f;

        public PlayerInputSystem(IInputContext context)
        {
            _context = context;
        }
        
        public void Initialize()
        {
            _context.CreateEntity().AddInput(Vector2.zero);
        }
        
        public void Execute()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            _context.inputEntity.ReplaceInput(new Vector2(horizontal,vertical));
        }

    }
}