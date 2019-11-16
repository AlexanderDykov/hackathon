using Core.Contexts;
using UnityEngine;

namespace GameScene.View
{
    public class StatueView : MonoBehaviour, IView
    {
        public GameEntity Entity => _entity;

        private GameEntity _entity;
        public void Link(IGameContext context, GameEntity entity)
        {
            _entity = entity;
        }
    }
}