using System;
using Core.Contexts;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.View
{
    public class SkeletonView : MonoBehaviour, IView
    {
        [SerializeField] private SpriteRenderer _actionIcon;
        [SerializeField] private  IconSettings _sprites;
        
        public void Link(IGameContext context, GameEntity entity)
        {
            
        }

        private void Update()
        {
//            if(_target == null)
//                return;
//            
//            _attackSpeed -= Time.deltaTime;
// 
//            if (_attackSpeed <= 0.0f)
//            {
//                _attackSpeed = _initialSpeed;
//                _target.Damage(_attackValue);
//            }
        }
    }
}