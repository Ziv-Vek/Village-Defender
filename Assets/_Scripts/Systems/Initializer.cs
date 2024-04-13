using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using VillageDefender.Utils;
using UnityEngine;
using UnityEngine.Serialization;
using VillageDefender.Services;

namespace VillageDefender.Systems
{
    [DefaultExecutionOrder(-1)]
    public class Initializer : PersistentSingleton<Initializer>
    {
        [SerializeField] private SceneLoadingManager _sceneLoadingManager;
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private FirebaseInit _firebaseInit;
        [SerializeField] private LocalSaveLoadService _localSaveLoadService;
        [SerializeField] private LoadingScreen _loadingScreen;
        [SerializeField] private FirebaseSaveLoadService _firebaseSaveLoadService;
        [SerializeField] private PlayerDataManager _playerDataManager;

        protected override void Awake()
        {
            base.Awake();
        }

        private async void Start()
        {
            Debug.Log("Initializer started.");
#if !UNITY_EDITOR
            _loadingScreen.Init();
            Debug.Log("loading screen initialized.");
#endif
            await _firebaseInit.Init();
            Debug.Log("Firebase initialized.");
            await _playerDataManager.Init();
//#if !UNITY_EDITOR
            _sceneLoadingManager.LoadMainMenu();
//#endif
        }
    }
}