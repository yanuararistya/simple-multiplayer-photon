using UnityEngine;
using UnityEngine.UI;

public class MenuScene : MonoBehaviour
{
    InputField _nameField;
    Button _joinButton;

    void Awake ()
    {
        _nameField = GetComponentInChildren<InputField>();
        _joinButton = GetComponentInChildren<Button>();
    }

    void Start ()
    {
        _nameField.onValueChanged.AddListener(delegate{SetButtonInteractability();});
    }

    void SetButtonInteractability ()
    {
        _joinButton.interactable  = _nameField.text != "";
    }
}
