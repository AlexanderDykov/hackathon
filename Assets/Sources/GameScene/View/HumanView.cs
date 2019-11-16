using System;
using Core.Contexts;
using Entitas.Unity;
using UnityEngine;

namespace GameScene.View
{
    public class HumanView : MonoBehaviour, IView, IDamagable
    {
        public GameEntity Entity => _entity;

        private GameEntity _entity;
        public void Link(IGameContext context, GameEntity entity)
        {
            _entity = entity;
            gameObject.Link(_entity, context);
        }

        public void Damage(int value)
        {
            _entity.AddDamage(value);
        }

        private void OnDestroy()
        {
            gameObject.Unlink();
        }
    }
}