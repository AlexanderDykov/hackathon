using Core.Contexts;
using GameScene.Utils;
using UnityEngine;

namespace GameScene.View
{
    public class SoulView : MonoBehaviour, IView
    {
        private IGameContext _context;
        private GameEntity _entity;

        public void Link(IGameContext context, GameEntity entity)
        {
            _context = context;
            _entity = entity;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Statue")) return;

            var statueEntity = other.gameObject.GetComponent<StatueView>();
            Clear(statueEntity);
            CreateMob(other.transform.position);
        }

        private void CreateMob(Vector2 position)
        {
            var entity = _context.CreateEntity();
            entity.AddInitialPosition(position);
            entity.AddResource(ResourceNames.Human);
            entity.AddCreatureType(CreatureType.Human);
            entity.AddHealth(10);
            entity.isPhysic = true;
            entity.AddSpeed(1);
        }

        private void Clear(StatueView statueEntity)
        {
            statueEntity.Entity.isDestroy = true;
            _entity.isDestroy = true;
            Destroy(statueEntity.gameObject);
            Destroy(gameObject);
        }
    }
}