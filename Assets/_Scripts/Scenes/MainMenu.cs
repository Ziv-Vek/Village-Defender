using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using VillageDefender.Systems;

namespace VillageDefender.Scenes
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private UIDocument _document;
        [SerializeField] private VisualTreeAsset _mainMenuView;

        private Label _coinsLabel;
        private Label _diamondsLabel;
        private Label _levelNumberLabel;
        private PlayerDataManager _playerDataManager;
        private Button _playButton;
        private Button _settingsButton;
        private Button[] _screenButtons = new Button[5];
        private Button _shopButton;
        private Button _farmButton;
        private Button _homeButton;
        private Button _toolsButton;
        private Button _loreButton;

        IEnumerator Start()
        {
            if (PlayerDataManager.Instance == null)
            {
                Debug.LogError("PlayerDataManager is not initialized");
                yield return new WaitUntil(() => PlayerDataManager.Instance != null);
            }

            _playerDataManager = PlayerDataManager.Instance;

            _coinsLabel = _document.rootVisualElement.Q<Label>("coinCountText");
            _diamondsLabel = _document.rootVisualElement.Q<Label>("diamondCountText");
            _levelNumberLabel = _document.rootVisualElement.Q<Label>("levelNumberText");
            _playButton = _document.rootVisualElement.Q<Button>("playButton");
            _settingsButton = _document.rootVisualElement.Q<Button>("settingButton");
            
            _screenButtons[0] = _document.rootVisualElement.Q<Button>("shopButton");
            _screenButtons[1] = _document.rootVisualElement.Q<Button>("progressionScreenButton");
            _screenButtons[2] = _document.rootVisualElement.Q<Button>("homeScreenButton");
            _screenButtons[3] = _document.rootVisualElement.Q<Button>("powerupsScreenButton");
            _screenButtons[4] = _document.rootVisualElement.Q<Button>("loreScreenButton");
            
            /*_shopButton = _document.rootVisualElement.Q<Button>("shopButton");
            _farmButton = _document.rootVisualElement.Q<Button>("progressionScreenButton");
            _homeButton = _document.rootVisualElement.Q<Button>("homeScreenButton");
            _toolsButton = _document.rootVisualElement.Q<Button>("powerupsScreenButton");
            _loreButton = _document.rootVisualElement.Q<Button>("loreScreenButton");*/

            _playButton.clicked += () => SceneLoadingManager.Instance.LoadSceneAsync(2);
            foreach (var button in _screenButtons)
            {
                button.clicked += () => OnScreenSelected(button);
            }
            /*_shopButton.clicked += () => OnScreenSelected(_shopButton);
            _farmButton.clicked += () => OnScreenSelected(_farmButton);
            _homeButton.clicked += () => OnScreenSelected(_homeButton);
            _toolsButton.clicked += () => OnScreenSelected(_toolsButton);
            _loreButton.clicked += () => OnScreenSelected(_loreButton);*/

            UpdateCoinsLabel();
            UpdateDiamondsLabel();
            UpdateLevelNumberLabel();
            PaintScreenButtonActive(_screenButtons[2]);
        }

        private void UpdateCoinsLabel()
        {
            _coinsLabel.text = _playerDataManager.Coins.ToString();
        }

        private void UpdateDiamondsLabel()
        {
            _diamondsLabel.text = _playerDataManager.Diamonds.ToString();
        }

        private void UpdateLevelNumberLabel()
        {
            _levelNumberLabel.text = "Level " + _playerDataManager.CurrentLevel;
        }
        
        private void OnScreenSelected(Button button)
        {
            PaintScreenButtonActive(button);
        }

        private void PaintScreenButtonActive(Button activeButton)
        {
            activeButton.AddToClassList("screens-container__button--active");
            foreach (var button in _screenButtons)
            {
                if (button != activeButton)
                {
                    button.RemoveFromClassList("screens-container__button--active");
                }
            }
        }

        private void OnDisable()
        {
            _playButton.clicked -= () => SceneLoadingManager.Instance.LoadSceneAsync(2);

            foreach (var button in _screenButtons)
            {
                button.clicked -= () => OnScreenSelected(button);
            }
            /*_shopButton.clicked -= () => OnScreenSelected(_shopButton);
            _farmButton.clicked -= () => OnScreenSelected(_farmButton);
            _homeButton.clicked -= () => OnScreenSelected(_homeButton);
            _toolsButton.clicked -= () => OnScreenSelected(_toolsButton);
            _loreButton.clicked -= () => OnScreenSelected(_loreButton);*/
        }

        private void OnApplicationQuit()
        {
            if (_playButton != null && SceneLoadingManager.Instance != null)
            {
                _playButton.clicked -= () => SceneLoadingManager.Instance.LoadSceneAsync(2);
            }
            
            foreach (var button in _screenButtons)
            {
                button.clicked -= () => OnScreenSelected(button);
            }

            /*if (_shopButton != null) _shopButton.clicked -= () => OnScreenSelected(_shopButton);
            if (_farmButton != null) _farmButton.clicked -= () => OnScreenSelected(_farmButton);
            if (_homeButton != null) _homeButton.clicked -= () => OnScreenSelected(_homeButton);
            if (_toolsButton != null) _toolsButton.clicked -= () => OnScreenSelected(_toolsButton);
            if (_loreButton != null) _loreButton.clicked -= () => OnScreenSelected(_loreButton);*/
        }
    }
}