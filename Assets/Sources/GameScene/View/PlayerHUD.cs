using Core.Contexts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.View
{
    public class PlayerHUD : MonoBehaviour, IView, ILifeListener, ILifeTimerListener, IScoreListener
    {
        [SerializeField] private Slider _timerProgress;
        [SerializeField] private TextMeshProUGUI _playerLifeCounterLabel;
        [SerializeField] private TextMeshProUGUI _playerScoreCounterLabel;

        public void Link(IGameContext context, GameEntity entity)
        {
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
    }
}