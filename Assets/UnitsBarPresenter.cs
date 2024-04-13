using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class UnitsBarPresenter : MonoBehaviour
{
    private UIDocument _actionsUIDocument;
    private VisualElement _rootElement;
    private Label _unitCostLabel;
    private Button _unitButton;
    
    private void Start()
    {
        _actionsUIDocument = GetComponent<UIDocument>();
        _rootElement = _actionsUIDocument.rootVisualElement;
        _unitCostLabel = _rootElement.Q<Label>("unitCostText");
        _unitCostLabel.text = $"{1}";

        _unitButton = _rootElement.Q<Button>("unitButton");
        _unitButton.clicked += OnUnitButtonClicked;
    }

    private void OnDisable()
    {
        _unitButton.clicked -= OnUnitButtonClicked;
    }

    private void OnUnitButtonClicked()
    {
        Debug.Log("clicked unit button");
    }
}
