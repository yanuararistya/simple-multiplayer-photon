using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    #region SerializeFields
    [SerializeField] Transform _playerListContent = null;
    [SerializeField] PlayerListItem _playerListItemPrefab = null;
    [SerializeField] Text _playerCounterLabel = null;
    [SerializeField] Button _startButton = null;
    #endregion

    #region PrivateVariables
    Dictionary<Player, PlayerListItem> _playerListItemMap = new Dictionary<Player, PlayerListItem>();
    bool isStartingGame = false;
    #endregion

    #region PrivateProperties
    int _playerCount { get { return _playerListItemMap.Count; } }
    #endregion

    #region EventDelegates
    delegate void OnPlayerListChanged ();
    OnPlayerListChanged _onPlayerListChanged;
    #endregion

    #region UnityLifecycles
    void Awake ()
    {
        LoadingScene.Instance.Hide();

        _startButton.onClick.AddListener(OnStartButtonClicked);

        _onPlayerListChanged += UpdatePlayerCounter;
        _onPlayerListChanged += UpdateStartButtonInteractability;

        if (PhotonNetwork.IsConnected) {
            Player[] playerList = PhotonNetwork.PlayerList;
            for (int i = 0; i < playerList.Length; i++) {
                AddNewPlayer(playerList[i]);
            }

            _onPlayerListChanged();
        }
    }
    #endregion

    #region PrivateMethods
    void AddNewPlayer (Player player)
    {
        var playerListItem = Instantiate(_playerListItemPrefab, _playerListContent);
        playerListItem.SetName(player.NickName);
        _playerListItemMap.Add(player, playerListItem);
    }

    void RemovePlayer (Player player)
    {
        PlayerListItem playerListItem = null;
        if (_playerListItemMap.TryGetValue(player, out playerListItem)) {
            _playerListItemMap.Remove(player);
            Destroy(playerListItem.gameObject);
        }
    }

    void UpdatePlayerCounter ()
    {
        _playerCounterLabel.text = string.Format("{0}/{1}", _playerCount, Constants.MAX_PLAYERS);
    }

    void UpdateStartButtonInteractability ()
    {
        _startButton.interactable = _playerCount > 1;
    }
    #endregion

    #region UICallbacks
    public void OnBackButtonClicked ()
    {
        PhotonNetwork.LeaveRoom();
    }

    void OnStartButtonClicked ()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;

        Hashtable startButtonClicked = new Hashtable {
            { Constants.START_BUTTON_CLICKED, true }
        };
        PhotonNetwork.CurrentRoom.SetCustomProperties(startButtonClicked);
    }
    #endregion

    #region PunCallbacks
    public override void OnPlayerEnteredRoom (Player newPlayer)
    {
        AddNewPlayer(newPlayer);
        _onPlayerListChanged();
    }

    public override void OnPlayerLeftRoom (Player otherPlayer)
    {
        RemovePlayer(otherPlayer);
        _onPlayerListChanged();
    }

    public override void OnLeftRoom ()
    {
        PhotonNetwork.LoadLevel(Constants.MENU_SCENE_NAME);
    }

    public override void OnDisconnected (DisconnectCause cause)
    {
        PhotonNetwork.LoadLevel(Constants.MENU_SCENE_NAME);
    }

    public override void OnRoomPropertiesUpdate(Hashtable changedProps)
    {
        if (isStartingGame) {
            return;
        }

        object isStartButtonClicked;
        if (changedProps.TryGetValue(Constants.START_BUTTON_CLICKED, out isStartButtonClicked)) {
            if ((bool) isStartButtonClicked) {
                isStartingGame = true;
                PhotonNetwork.LoadLevel(Constants.GAME_SCENE_NAME);
            }
        }
    }
    #endregion
}
