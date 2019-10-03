using UnityEngine;
using UnityEngine.UI;

public class MenuScene : MonoBehaviour
{
    #region Constants
    const int MAX_LOBBY_ID = 9999;
    #endregion

    #region SerializeFields
    [SerializeField] InputField _nameField = null;
    [SerializeField] InputField _lobbyField = null;
    [SerializeField] Button _joinButton = null;
    [SerializeField] Button _randomizeButton = null;
    #endregion

    #region PrivateVariables
    int _lobbyIdentifier = 0;
    #endregion

    #region UnityLifecycles
    void Start ()
    {
        _nameField.onValueChanged.AddListener(delegate{SetButtonInteractability();});

        _lobbyField.onValueChanged.AddListener(delegate{SetButtonInteractability();});
        _lobbyField.onValueChanged.AddListener(delegate{SetLobbyIdentifier();});

        _randomizeButton.onClick.AddListener(delegate{RandomizeLobbyIdentifier();});
    }
    #endregion

    #region PrivateMethods
    void SetButtonInteractability ()
    {
        _joinButton.interactable  = _nameField.text != "" && _lobbyField.text != "";
    }

    void SetLobbyIdentifier ()
    {
        System.Int32.TryParse(_lobbyField.text, out _lobbyIdentifier);
    }

    void RandomizeLobbyIdentifier ()
    {
        _lobbyIdentifier = Random.Range(0, MAX_LOBBY_ID);
        _lobbyField.text = _lobbyIdentifier.ToString();
    }
    #endregion
}
