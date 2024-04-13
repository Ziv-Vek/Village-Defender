using System;
using UnityEngine;

namespace VillageDefender
{
    [Serializable]
    public class FoodModel : MonoBehaviour
    {
        private float _foodRate = 0.5f;
        private int _foodCount;

        public float FoodRate => _foodRate;
        public int FoodCount => _foodCount;

        public Action<int> OnFoodCountChanged;

        public void IncreaseFoodCount(int amount)
        {
            _foodCount += amount;
            UpdateFoodCount();
        }

        public void DecreaseFoodCount(int amount)
        {
            _foodCount -= amount;
            UpdateFoodCount();
        }

        public void SetFoodRate(float foodRate)
        {
            _foodRate = foodRate;
        }

        public void SetFoodCount(int foodCount)
        {
            _foodCount = foodCount;
        }

        public void UpdateFoodCount()
        {
            OnFoodCountChanged?.Invoke(_foodCount);
        }
    }
}