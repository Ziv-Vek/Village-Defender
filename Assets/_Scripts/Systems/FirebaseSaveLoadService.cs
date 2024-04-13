using System;
using VillageDefender.Utils;
using System.Threading.Tasks;
using Firebase;
using Firebase.Database;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VillageDefender.Systems
{
    public class FirebaseSaveLoadService : Singleton<FirebaseSaveLoadService>
    {
        private DatabaseReference _dbReference;
        private TextAsset _jsonFile;
    
        //save data to firebase realtime database
        public async Task SaveData(string path, string data)
        {
            _dbReference = FirebaseDatabase.DefaultInstance.RootReference;
            await _dbReference.Child(path).SetRawJsonValueAsync(data);
        }
    
        //load data from firebase realtime database
        public async Task<string> GetRTData(string path)
        {
            UnityEngine.Debug.Log("GetRTData called for path: " + path);
            try
            {
                _dbReference = FirebaseDatabase.DefaultInstance.RootReference;
                var snapshot = await _dbReference.Child(path).GetValueAsync();
                Debug.Log($"fetch data from db {path} was successful.");
                return snapshot.GetRawJsonValue();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}