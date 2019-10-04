using UnityEngine;
using UnityEngine.UI;

public class MenuScene : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] InputField _nameField = null;
    [SerializeField] InputField _lobbyField = null;
    [SerializeField] Button _joinButton = null;
    [SerializeField] Button _randomizeButton = null;
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

        _lobbyField.onValueChanged.AddListener(delegate{
            UpdateJoinButtonInteractability();
            SetLobbyIdentifier();
        });

        _randomizeButton.onClick.AddListener(delegate{RandomizeLobbyIdentifier();});
    }
    #endregion

    #region PrivateMethods
    void UpdateJoinButtonInteractability ()
    {
        _joinButton.interactable  = _nameField.text != "" && _lobbyField.text.Length == 4;
    }

    void SetPlayerName ()
    {
        _playerName = _nameField.text;
    }

    void SetLobbyIdentifier ()
    {
        _lobbyIdentifier = _lobbyField.text;
    }

    void RandomizeLobbyIdentifier ()
    {
        _lobbyIdentifier = "";
        for (int i = 0; i < _lobbyField.characterLimit; i++) {
            _lobbyIdentifier += Random.Range(0, 9).ToString();
        }

        _lobbyField.text = _lobbyIdentifier.ToString();
    }
    #endregion
}
