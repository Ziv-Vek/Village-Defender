using System;
using System.Collections.Generic;

namespace VillageDefender.Models
{
    [Serializable]
    public class PlayerData
    {
        public int CurrentLevel { get; private set; }
        public int FoodLevel { get; private set; }
        public int Diamonds { get; private set; }
        public int Coins { get; private set; }
        
        public PlayerData(int currentLevel, int foodLevel, int diamonds, int coins)
        {
            CurrentLevel = currentLevel;
            FoodLevel = foodLevel;
            Diamonds = diamonds;
            Coins = coins;
        }
        
        public void SetCoins(int coins)
        {
            Coins = coins;
        }
        
        public void SetDiamonds(int diamonds)
        {
            Diamonds = diamonds;
        }
        
        public void SetFoodLevel(int foodLevel)
        {
            FoodLevel = foodLevel;
        }
        
        public void SetCurrentLevel(int currentLevel)
        {
            CurrentLevel = currentLevel;
        }
    }
}