using Core.Contexts;
using Entitas;
using GameScene.View;
using UnityEngine;

namespace GameScene.ECS.Systems
{
    public class OpenChestSystem : IExecuteSystem
    {
        private float radius = 0.02f;
        private IGameContext _context;
        private int layerMask = 1 << LayerMask.NameToLayer("Box");

        public OpenChestSystem(IGameContext context)
        {
            _context = context;
        }
        
        public void Execute()
        {
            if(Input.GetMouseButtonUp(0))
            {
                //TODO: refactor Camera.main
                Vector3 currPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
                Vector2 curr2DPos = new Vector2 (currPos.x, currPos.y);

                var overlapCircle = Physics2D.OverlapCircle(curr2DPos, radius, layerMask);
                if (overlapCircle)
                {
                    overlapCircle.GetComponent<BoxView>().Open(true);
                }
            }
        }
    }
}