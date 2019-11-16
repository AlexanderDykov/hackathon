using System;
using Core.Contexts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.View
{
    public class LifeTimeView : MonoBehaviour, IView, ILifeListener, ILifeTimerListener
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _label;

        public void Link(IGameContext context, GameEntity entity)
        {
            entity.AddLifeListener(this);   
            entity.AddLifeTimerListener(this);   
        }

        public void OnLifeTimer(GameEntity entity, float Value)
        {
            _slider.value = Value;
        }

        public void OnLife(GameEntity entity, int Value)
        {
            _label.text = Value.ToString();
        }
    }
}