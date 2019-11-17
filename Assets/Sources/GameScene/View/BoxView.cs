using System;
using Core.Contexts;
using UnityEngine;
using Zenject;

namespace GameScene.View
{
    public class BoxView : MonoBehaviour, IView
    {
        private GameEntity _entity;
        
        [SerializeField] private Animator _animator;
        private static readonly int IsOpen = Animator.StringToHash("isOpen");

        public void Open(Boolean open)
        {
            _entity.isShowSelectView = open;
            _animator.SetBool(IsOpen, open);
        }

//        private void OnTriggerEnter2D(Collider2D other)
//        {
//            if (!other.CompareTag("Player")) return;
//            _entity.isShowSelectView = true;
//            _animator.SetBool(IsOpen, true);
//        }
//
//        private void OnTriggerExit2D(Collider2D other)
//        {
//            if (!other.CompareTag("Player")) return;
//            _entity.isShowSelectView = false;
//            _animator.SetBool(IsOpen, false);
//        }
        
        public void Link(IGameContext context, GameEntity entity)
        {
            _entity = entity;
        }
    }
}
