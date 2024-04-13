using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace VillageDefender
{
    public class FoodCountPresenter : MonoBehaviour
    {
        private UIDocument _actionsUIDocument;
        private VisualElement _rootElement;
        private Label _foodCountLabel;
        private FoodModel _foodModel;

        private void Start()
        {
            _actionsUIDocument = GetComponent<UIDocument>();
            _rootElement = _actionsUIDocument.rootVisualElement;
            _foodCountLabel = _rootElement.Q<Label>("foodCount");
            _foodModel = GetComponent<FoodModel>();
            _foodModel.OnFoodCountChanged += UpdateFoodCount;
            UpdateFoodCount(_foodModel.FoodCount);
        }

        private void OnDisable()
        {
            if (_foodModel != null)
            {
                _foodModel.OnFoodCountChanged -= UpdateFoodCount;
            }
        }

        private void UpdateFoodCount(int foodCount)
        {
            _foodCountLabel.text = foodCount.ToString();
        }

        private void OnApplicationQuit()
        {
            if (_foodModel != null && _foodModel.OnFoodCountChanged != null)
            {
                _foodModel.OnFoodCountChanged -= UpdateFoodCount;
            }
        }
    }
}