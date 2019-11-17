using System.Linq;
using Core.Contexts;
using Entitas;
using UnityEngine;

namespace GameScene.ECS.Systems
{
    public sealed class FindTargetSystem : IExecuteSystem
    {
        readonly IGroup<GameEntity> _lookingGroup;
        readonly IGroup<GameEntity> _creaturesGroup;

        public FindTargetSystem(IGameContext gameContext)
        {
            _lookingGroup = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.View, GameMatcher.Attackable, GameMatcher.Side, GameMatcher.AttackDistance));
            _creaturesGroup = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.View, GameMatcher.Damagable, GameMatcher.Side));
        }

        public void Execute()
        {
            foreach (var look in _lookingGroup.GetEntities())
            {
                var minDist = Mathf.Infinity;
                var lookPos = look.view.Value.transform.position;
                var direction = Vector2.zero;
                var stopEndAttack = false;
                foreach (var target in _creaturesGroup.GetEntities().Where(x => x.side.Value != look.side.Value))
                {
                    var tmpDirection = target.view.Value.transform.position - lookPos;
                    var dist = Vector3.SqrMagnitude(tmpDirection);
                    if (dist <= look.attackDistance.Value)
                    {
                        look.ReplaceAttackTarget(target);
                        stopEndAttack = true;
                        break;
                    }
                    
                    if (!(dist < minDist)) continue;
                    direction = tmpDirection.normalized;
                    minDist = dist;
                }

                if (stopEndAttack)
                {
                    look.ReplaceDirection(Vector3.zero);
                    continue;
                }
                if(look.hasAttackTarget)
                    look.RemoveAttackTarget();
                look.ReplaceDirection(direction.normalized);
            }
        }
    }
}
