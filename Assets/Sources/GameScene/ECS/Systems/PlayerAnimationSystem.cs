using System.Collections.Generic;
using Core.Contexts;
using Entitas;
using UnityEngine;

namespace GameScene.ECS.Systems
{
    public sealed class PlayerAnimationSystem : ReactiveSystem<InputEntity>
    {
        readonly IGroup<GameEntity> _movableGroup;
        
        private readonly int _horizontal = Animator.StringToHash("Horizontal");
        private readonly int _vertical = Animator.StringToHash("Vertical");

        
        public PlayerAnimationSystem(IInputContext inputContext, IGameContext gameContext) : base(inputContext)
        {
            _movableGroup = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.Animator, GameMatcher.Player));

        }
        
        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.Input.Added());
        }

        protected override bool Filter(InputEntity entity)
        {
            return entity.hasInput;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            foreach (var inputEntity in entities)
            {
                foreach (var entity in _movableGroup)
                {
                    entity.animator.Value.SetFloat(_horizontal, inputEntity.input.Value.x);
                    entity.animator.Value.SetFloat(_vertical, inputEntity.input.Value.y);
                }
            }
        }
    }
}