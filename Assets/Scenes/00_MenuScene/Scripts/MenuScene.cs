using UnityEngine;
using UnityEngine.UI;

public class MenuScene : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] InputField _nameField = null;
    [SerializeField] Button _joinButton = null;
    #endregion

    #region PrivateVariables
    string _lobbyIdentifier = "";
    string _playerName = "";
    #endregion

    #region UnityLifecycles
    void Start ()
    {
        _nameField.onValueChanged.AddListener(delegate{
            UpdateJoinButtonInteractability();
            SetPlayerName();
        });
    }
    #endregion

    #region PrivateMethods
    void UpdateJoinButtonInteractability ()
    {
        _joinButton.interactable  = _nameField.text != "";
    }

    void SetPlayerName ()
    {
        _playerName = _nameField.text;
    }
    #endregion
}
