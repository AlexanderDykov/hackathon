using System.Linq;
using Core.Contexts;
using Entitas;
using UnityEngine;

namespace GameScene.ECS.Systems
{
    public class FindBuildTargetSystem : IExecuteSystem
    {
        readonly IGroup<GameEntity> _lookingGroup;
        readonly IGroup<GameEntity> _creaturesGroup;

        public FindBuildTargetSystem(IGameContext gameContext)
        {
            _lookingGroup = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.View, GameMatcher.Creator, GameMatcher.Side));
            _creaturesGroup = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.View, GameMatcher.CreatureType, GameMatcher.Side));
        }

        public void Execute()
        {
            foreach (var look in _lookingGroup.GetEntities())
            {
                var minDist = Mathf.Infinity;
                var lookPos = look.view.Value.transform.position;
                var direction = Vector2.zero;
                var stopMovement = false;

                if (look.creator.Value == CreatureType.Statue)
                {
                    if (!look.hasBuildTarget)
                    {
                        Vector3 rndPos = look.view.Value.transform.position + (Vector3) Random.insideUnitCircle * 5;
                        look.ReplaceBuildTarget(rndPos);
                    }
                    Vector3 tmpDirection =(Vector3) look.buildTarget.Position - lookPos;
                    var dist = Vector3.SqrMagnitude(tmpDirection);
                    if (dist <= 1)
                    {
                        stopMovement = true;
                    }
                    else
                    {
                        direction = tmpDirection.normalized;
                    }
                }
                else
                {
                    foreach (var target in _creaturesGroup.GetEntities()
                        .Where(x => x.creatureType.Value == look.creator.Value))
                    {
                        var tmpDirection = target.view.Value.transform.position - lookPos;
                        var dist = Vector3.SqrMagnitude(tmpDirection);
                        if (dist <= 1)
                        {
                            stopMovement = true;
                            look.ReplaceBuildTarget(target.view.Value.transform.position);
                            break;
                        }

                        if (!(dist < minDist)) continue;
                        direction = tmpDirection.normalized;
                        minDist = dist;
                    }
                }

                if (stopMovement)
                {
                    look.ReplaceDirection(Vector3.zero);
                    look.isCanBuild = true;
                    continue;
                }
                look.isCanBuild = false;
                look.ReplaceDirection(direction.normalized);
            }
        }
    }
}