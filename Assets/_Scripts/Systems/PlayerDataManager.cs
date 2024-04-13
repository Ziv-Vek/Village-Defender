using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VillageDefender.Models;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VillageDefender.Services;
using VillageDefender.Utils;

namespace VillageDefender.Systems
{
    public class PlayerDataManager : Singleton<PlayerDataManager>
    {
        private const string FILE_NAME = "playerData.json";
        
        ILocalDataService _localDataService;
        PlayerData _playerData;
        
        public int Coins => _playerData.Coins;
        public int Diamonds => _playerData.Diamonds;
        public int CurrentLevel => _playerData.CurrentLevel;
        public int FoodLevel => _playerData.FoodLevel;

        public async Task Init()
        {
            Debug.Log("Init PlayerDataManager");
            if (LocalSaveLoadService.Instance != null)
            {
                _localDataService = LocalSaveLoadService.Instance;
            }
            else
            {
                Debug.LogError("LocalSaveLoadService is not initialized");
            }
            
            await LoadPlayerData();
        }

        private async Task LoadPlayerData()
        {
            if (_localDataService.IsFileExists(FILE_NAME))
            {
                _playerData = await _localDataService.LoadFromFileAsync<PlayerData>(FILE_NAME);
            }
            else
            {
                Debug.Log("No player data found. Creating new data.");
                _playerData = new PlayerData(1, 1, 0, 0);
            }
        }
    }
}