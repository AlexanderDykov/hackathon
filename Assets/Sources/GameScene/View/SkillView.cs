using GameScene.ECS.Components;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.View
{
    public class SkillView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _skillName;
        [SerializeField] private Button _selectBtn;
        private Skill _model;
        private GameEntity _entity;
        private void Awake()
        {
            _selectBtn.onClick.AddListener(OnSelectClick);
        }

        private void OnSelectClick()
        {
            _entity.ReplaceSkill(_model.SkillType);
        }

        public void SetModel(Skill skill, GameEntity entity)
        {
            _model = skill;
            _entity = entity;
            _skillName.text = skill.Description;
        }

        private void OnDestroy()
        {
            _selectBtn.onClick.RemoveListener(OnSelectClick);
        }
    }
}