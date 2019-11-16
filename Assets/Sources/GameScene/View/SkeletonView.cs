using System;
using Core.Contexts;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.View
{
    public class SkeletonView : MonoBehaviour, IView
    {
        private int _attackValue = 2;
        private float _initialSpeed = 2;
        [SerializeField] private SpriteRenderer _actionIcon;

        [SerializeField] private  IconSettings _sprites;
        
        private float _attackSpeed = 2;
        private IDamagable _target = null;
        
        public void Link(IGameContext context, GameEntity entity)
        {
            
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Human")) return;
            _attackSpeed = _initialSpeed;
            _actionIcon.sprite = _sprites.AttackSprite;
            _target = other.gameObject.GetComponent<IDamagable>();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _target = null;
            _actionIcon.sprite = _sprites.SearchSprite;
        }

        private void Update()
        {
            if(_target == null)
                return;
            
            _attackSpeed -= Time.deltaTime;
 
            if (_attackSpeed <= 0.0f)
            {
                _attackSpeed = _initialSpeed;
                _target.Damage(_attackValue);
            }
        }
    }
}