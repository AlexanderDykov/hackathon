using System.Collections.Generic;
using UnityEngine;

namespace GameScene.View
{
    public class SelectPanel : MonoBehaviour
    {
        [SerializeField] private SkillView _prefab;

        private List<SkillView> _list = new List<SkillView>();
        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            for (int i = 0; i < 2; i++)
            {
                _list.Add(Instantiate(_prefab, transform, false));
            }
        }

        public void Show(GameEntity entity)
        {
            gameObject.SetActive(true);
            
            for (var i = 0; i < entity.boxSkills.Skills.Count; i++)
            {
                _list[i].SetModel(entity.boxSkills.Skills[i], entity);
            }
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            for (var i = 0; i < _list.Count; i++)
            {
                _list[i].gameObject.SetActive(false);
            }
        }
    }
}