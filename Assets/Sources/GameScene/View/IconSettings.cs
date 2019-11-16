using UnityEngine;

namespace GameScene.View
{
    [CreateAssetMenu(menuName = "IconSettings", fileName = "IconSettings")]
    public class IconSettings : ScriptableObject
    {
        public Sprite SearchSprite;
        public Sprite AttackSprite;
        public Sprite BuildSprite;
    }
}