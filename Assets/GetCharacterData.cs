using System;
using Firebase.Firestore;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class GetCharacterData : MonoBehaviour
{
    [SerializeField] private string _characterPath = "character_sheets/one_cool_dude";
    [SerializeField] private TMP_InputField _nameField;
    [SerializeField] private TMP_InputField _descriptionField;
    [SerializeField] private TMP_InputField _attackField;
    [SerializeField] private TMP_InputField _defenseField;
    //[SerializeField] private Button _getButton;
    
    private ListenerRegistration _listenerRegistration;

    private void Start()
    {
        var firestore = FirebaseFirestore.DefaultInstance;
        _listenerRegistration = firestore.Document(_characterPath).Listen(snapshot =>
        {
            var characterData = snapshot.ConvertTo<CharacterData>();
            //_nameField.text = characterData.Name;
            _nameField.text = $"Name: {characterData.Name}";
            _descriptionField.text = characterData.Description;
            _attackField.text = characterData.Attack.ToString();
            _defenseField.text = characterData.Defense.ToString();
        });
    }

    private void OnDestroy()
    {
        _listenerRegistration.Stop();
    }

    /*public void GetData()
    {
        var firestore = FirebaseFirestore.DefaultInstance;
        
        firestore.Document(_characterPath).GetSnapshotAsync().ContinueWith(task =>
        {
            Assert.IsNull(task.Exception);
            var snapshot = task.Result;
            var characterData = snapshot.ConvertTo<CharacterData>();
            
            //_nameField.text = characterData.Name;
            _nameField.text = $"Name: {characterData.Name}";
            _descriptionField.text = characterData.Description;
            _attackField.text = characterData.Attack.ToString();
            _defenseField.text = characterData.Defense.ToString();
        });
    }*/
}
