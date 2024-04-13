using System;
using Firebase.Firestore;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class SetCharacterData : MonoBehaviour
{
    [SerializeField] private string _characterPath = "character_sheets/one_cool_dude";
    [SerializeField] private TMP_InputField _nameField;
    [SerializeField] private TMP_InputField _descriptionField;
    [SerializeField] private TMP_InputField _attackField;
    [SerializeField] private TMP_InputField _defenseField;

    [SerializeField] private Button _submitButton;

    private void Start()
    {
        _submitButton.onClick.AddListener(() =>
        {
            var characterData = new CharacterData
            {
                Name = _nameField.text,
                Description = _descriptionField.text,
                Attack = int.Parse(_attackField.text),
                Defense = int.Parse(_defenseField.text)
            };
            
            var firestore = FirebaseFirestore.DefaultInstance;
            firestore.Document(_characterPath).SetAsync(characterData);
        });
    }

    
    
}
