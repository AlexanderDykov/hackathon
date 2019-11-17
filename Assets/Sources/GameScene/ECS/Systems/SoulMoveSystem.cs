using System.Linq;
using Core.Contexts;
using Entitas;
using UnityEngine;

namespace GameScene.ECS.Systems
{
    public class SoulMoveSystem: IExecuteSystem
    {
        readonly IGroup<GameEntity> _lookingGroup;
        readonly IGroup<GameEntity> _creaturesGroup;
        private IGameContext _gameContext;

        public SoulMoveSystem(IGameContext gameContext)
        {
            _gameContext = gameContext;
            _lookingGroup = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.Soul, GameMatcher.View));
            _creaturesGroup = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.View, GameMatcher.CreatureType));
        }

        public void Execute()
        {
            foreach (var look in _lookingGroup.GetEntities())
            {
                var minDist = Mathf.Infinity;
                var lookPos = look.view.Value.transform.position;
                var direction = Vector2.zero;
                foreach (var target in _creaturesGroup.GetEntities().Where(x => x.creatureType.Value != CreatureType.Soul))
                {
                    var tmpDirection = target.view.Value.transform.position - lookPos;
                    var dist = Vector3.SqrMagnitude(tmpDirection);
                    if (dist <= 0.01f)
                    {
                        Object.Destroy(look.view.Value);
                        look.isDestroy = true;
                        _gameContext.CreateEntity().AddUpgrade(target);
                        break;
                    }
                    
                    if (!(dist < minDist)) continue;
                    direction = tmpDirection.normalized;
                    minDist = dist;
                }

                look.ReplaceDirection(direction.normalized);
            }
        }
    }
}