using System;
using Core.Contexts;
using UnityEngine;
using Zenject;

namespace GameScene.View
{
    public class BoxView : MonoBehaviour, IView
    {
        IGameContext _context;

        private GameEntity _entity;
        private void Awake()
        {
            //TODO: remade it with DI
//            _context = Contexts.sharedInstance.game;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _entity.isShowSelectView = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _entity.isShowSelectView = false;
            }
        }
        
        public void Link(IGameContext context, GameEntity entity)
        {
            _context = context;
            _entity = entity;
        }
    }
}