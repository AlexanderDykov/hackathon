using Core.Contexts;
using Entitas;
using GameScene.ECS.Components;
using UnityEngine;

namespace GameScene.ECS.Systems
{
    public sealed class FindTargetSystem : IExecuteSystem
    {
        readonly IGroup<GameEntity> _lookingGroup;
        readonly IGroup<GameEntity> _creaturesGroup;

        public FindTargetSystem(IInputContext inputContext, IGameContext gameContext)
        {
            _lookingGroup = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.View, GameMatcher.CreatureType, GameMatcher.LookNearest));
            _creaturesGroup = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.View, GameMatcher.CreatureType));
        }

        public void Execute()
        {
            foreach (var look in _lookingGroup.GetEntities())
            {
                GameEntity nearestTarget = null;
                float minDist = Mathf.Infinity;
                Vector3 lookPos = look.view.Value.transform.position;
                foreach (var target in _creaturesGroup.GetEntities())
                {
                    if (target.creatureType.Value == look.lookNearest.Value)
                    {
                        float dist = Vector3.Distance(target.view.Value.transform.position, lookPos);
                        if (dist < minDist)
                        {
                            nearestTarget = target;
                            minDist = dist;
                        }
                    }
                }
                if (nearestTarget != null) {
                    Vector2 direction = nearestTarget.view.Value.transform.position - lookPos;
                    look.ReplaceDirection(direction.normalized);
                } else {
                    look.ReplaceDirection(Vector3.zero);
                }

            }
        }
    }
}
