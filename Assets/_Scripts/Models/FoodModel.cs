using System;
using System.Collections.Generic;

namespace VillageDefender.Models
{
    [Serializable]
    public class FoodProgression
    {
        public Dictionary<string, FoodProgressionData> Progression;

        public FoodProgression()
        {
            
        }
    }
}

[Serializable]
public struct FoodProgressionData
{
    public float Name;
    public int Count;
}