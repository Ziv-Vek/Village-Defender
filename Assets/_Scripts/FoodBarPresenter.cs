using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

namespace VillageDefender
{
    public class FoodBarPresenter : MonoBehaviour
    {
        private UIDocument _actionsUIDocument;
        private VisualElement _rootElement;
        private ProgressBar _foodBar;
        private FoodModel _foodModel;
        private bool _isBarActive = true;
        private const float INCREMENT_AMOUNT = 1f;

        public void Start()
        {
            _rootElement = GetComponent<UIDocument>().rootVisualElement;
            _foodModel = GetComponent<FoodModel>();
            _foodBar = _rootElement.Q<ProgressBar>("foodBar");
            _foodBar.value = 0;
            _isBarActive = true;

            //StartFoodBar();
            StartCoroutine(ProgressFoodBar());
        }

        private void StartFoodBar()
        {
            var tween = DOVirtual.Float(0, 1, 1f, v => _foodBar.value = v).SetLoops(-1, LoopType.Restart)
                .SetSpeedBased(true).SetEase(Ease.Linear).OnStepComplete(
                    () => Debug.Log("food bar is full. do something"));
        }

        private IEnumerator ProgressFoodBar()
        {
            float incrementAmount = _foodModel.FoodRate;

            while (_isBarActive)
            {
                if (_foodBar.value + incrementAmount > 1)
                {
                    _foodBar.value = 0;
                    _foodModel.IncreaseFoodCount(1);
                }
                else
                {
                    float startTime = Time.time;
                    float startValue = _foodBar.value;
                    float endValue = startValue + incrementAmount;
                    while (Time.time - startTime < INCREMENT_AMOUNT)
                    {
                        _foodBar.value = Mathf.Lerp(startValue, endValue, (Time.time - startTime) / INCREMENT_AMOUNT);
                        yield return null;
                    }

                    _foodBar.value = endValue;
                }
            }
        }
    }
}