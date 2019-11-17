using Core.Contexts;
using Entitas.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.View
{
    public class PlayerHUD : MonoBehaviour, IView, IBalanceListener, IScoreListener, IDamagable
    {
        [SerializeField] private Slider _balanceProgress;
        [SerializeField] private TextMeshProUGUI _playerBalanceCounterLabel;
        [SerializeField] private TextMeshProUGUI _playerScoreCounterLabel;
        private GameEntity _entity;

        public void Link(IGameContext context, GameEntity entity)
        {
            _entity = entity;
            gameObject.Link(_entity, context);
            entity.AddBalanceListener(this);
            entity.AddScoreListener(this);
        }

        public void OnBalance(GameEntity entity, int Value)
        {
            _balanceProgress.value = Value;
            _playerBalanceCounterLabel.text = Value.ToString();
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
