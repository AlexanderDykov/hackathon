using System;
using Core.Contexts;
using UnityEngine;
using Zenject;

namespace GameScene.View
{
    public class BoxView : MonoBehaviour, IView
    {
        IGameContext _context;
        [SerializeField] private Animator _animator;
        
        private void Awake()
        {
            //TODO: remade it with DI
            _context = Contexts.sharedInstance.game;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _context.isShowSelectView = true;
                _animator.SetBool("isOpen", true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _context.isShowSelectView = false;
                _animator.SetBool("isOpen", false);
            }
        }
        
        public void Link(IGameContext context, GameEntity entity)
        {
            _context = context;
        }
    }
}
