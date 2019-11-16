using Core.Contexts;
using Entitas.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.View
{
    public class PlayerHUD : MonoBehaviour, IView, ILifeListener, ILifeTimerListener, IScoreListener, IDamagable
    {
        [SerializeField] private Slider _timerProgress;
        [SerializeField] private TextMeshProUGUI _playerLifeCounterLabel;
        [SerializeField] private TextMeshProUGUI _playerScoreCounterLabel;
        private GameEntity _entity;
        public void Link(IGameContext context, GameEntity entity)
        {
            _entity = entity;
            gameObject.Link(_entity, context);
            entity.AddLifeListener(this);   
            entity.AddLifeTimerListener(this);  
            entity.AddScoreListener(this);
        }

        public void OnLifeTimer(GameEntity entity, float Value)
        {
            _timerProgress.value = Value;
        }

        public void OnLife(GameEntity entity, int Value)
        {
            _playerLifeCounterLabel.text = Value.ToString();
        }

        public void OnScore(GameEntity entity, int Value)
        {
            _playerScoreCounterLabel.text = Value.ToString();
        }

        public void Damage(int value)
        {
            _entity.AddDamage(value);
        }
        
        private void OnDestroy()
        {
            gameObject.Unlink();
        }
    }
}