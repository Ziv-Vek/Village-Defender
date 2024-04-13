using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using VillageDefender.Systems;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class LoadingScreen : MonoBehaviour
{
    // cached:
    [SerializeField] private UIDocument _document;
    [SerializeField] private StyleSheet _styleSheet;

    private VisualElement _loadingImgEle;
    private Image _loadingCircleImg;
    private string[] _sentences;
    private Label _wisdomSentenceLabel;

    [Space(10)]
    // config:
    [SerializeField, Tooltip("Loading circle rotation speed")]
    private float _rotationSpeed = 1f;

    [SerializeField, Tooltip("Delay between each sentence switch")]
    private float _sentencesCycleTime = 5f;

    // states:
    private bool _isShown = false;

    #region Public Methods

    /** Renders loading screen without connectivity to server */
    public void Init()
    {
        _document.enabled = true;
        _isShown = true;
        GenerateDocument();
    }

    /** Renders loading screen with connectivity to server (sentences, etc.) */
    public async void Show()
    {
        _document.enabled = true;
        if (_isShown) _document.rootVisualElement.Clear();

        _isShown = true;
        Debug.Log("showing loading screen...");

        var resJson = await FirebaseSaveLoadService.Instance.GetRTData("sentencesOfWisdom");
        if (resJson != null)
        {
            _sentences = JsonConvert.DeserializeObject<string[]>(resJson);
        }

        GenerateDocument();

        StartCoroutine(RenderSentences());
    }


    /** Unrender the loading screen */
    public void Hide()
    {
        _document.rootVisualElement.Clear();
        Debug.Log("Loading screen is hidden.");
        _isShown = false;
        _document.enabled = false;
    }

    #endregion

    #region Unity Methods

    private void Update()
    {
        if (_isShown && _loadingCircleImg != null)
        {
            _loadingImgEle.transform.rotation = Quaternion.Euler(0, 0,
                _loadingImgEle.transform.rotation.eulerAngles.z + _rotationSpeed * Time.deltaTime);
        }
    }

    #endregion

    #region Private Methods

    IEnumerator RenderSentences()
    {
        while (_isShown)
        {
            _wisdomSentenceLabel.text = _sentences[UnityEngine.Random.Range(0, _sentences.Length)];

            yield return new WaitForSeconds(_sentencesCycleTime);
        }
    }

    private void GenerateDocument()
    {
        if (!_document.isActiveAndEnabled) return;
        
        _document.sortingOrder = 100;
        var root = _document.rootVisualElement;
        root.styleSheets.Add(_styleSheet);
        // container
        var container = new VisualElement();
        root.Add(container);
        container.AddToClassList("container");
        // man image
        var manImage = new Image();
        manImage.sprite = Resources.Load<Sprite>("Sprites/man");
        container.Add(manImage);
        manImage.AddToClassList("man-image");
        // wisdom sentence
        _wisdomSentenceLabel = new Label();
        _wisdomSentenceLabel.AddToClassList("wisdom-sentence");
        container.Add(_wisdomSentenceLabel);
        // loading circle container
        _loadingImgEle = new VisualElement();
        container.Add(_loadingImgEle);
        _loadingImgEle.AddToClassList("loading-circle-container");
        // loading circle
        _loadingCircleImg = new Image();
        _loadingCircleImg.sprite = Resources.Load<Sprite>("Sprites/loading-circle");
        _loadingCircleImg.AddToClassList("loading-circle");
        _loadingImgEle.Add(_loadingCircleImg);
        // loading text
        var loadingText = new Label();
        loadingText.text = "Loading...";
        loadingText.AddToClassList("loading-text");
        container.Add(loadingText);
    }

    #endregion
}