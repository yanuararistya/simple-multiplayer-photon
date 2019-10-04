using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    #region Constants
    const string MENU_SCENE_NAME = "MenuScene";
    #endregion

    #region SerializeFields
    [SerializeField] Transform _playerListContent = null;
    [SerializeField] PlayerListItem _playerListItemPrefab = null;
    [SerializeField] Text _playerCounter = null;
    #endregion

    #region PrivateVariables
    Dictionary<Player, PlayerListItem> playerListItemMap = new Dictionary<Player, PlayerListItem>();
    #endregion

    #region UnityLifecycles
    void Awake ()
    {
        LoadingScene.Instance.Hide();
        if (PhotonNetwork.IsConnected) {
            Player[] playerList = PhotonNetwork.PlayerList;
            for (int i = 0; i < playerList.Length; i++) {
                AddNewPlayer(playerList[i]);
            }

            UpdatePlayerCounter();
        }
    }
    #endregion

    #region PrivateMethods
    void AddNewPlayer (Player player)
    {
        var playerListItem = Instantiate(_playerListItemPrefab, _playerListContent);
        playerListItem.SetName(player.NickName);
        playerListItemMap.Add(player, playerListItem);
    }

    void RemovePlayer (Player player)
    {
        PlayerListItem playerListItem = null;
        if (playerListItemMap.TryGetValue(player, out playerListItem)) {
            playerListItemMap.Remove(player);
            Destroy(playerListItem.gameObject);
        }
    }

    void UpdatePlayerCounter ()
    {
        _playerCounter.text = string.Format("{0}/16", playerListItemMap.Count);
    }
    #endregion

    #region UICallbacks
    public void OnBackButtonClicked ()
    {
        PhotonNetwork.LeaveRoom();
    }
    #endregion

    #region PunCallbacks
    public override void OnPlayerEnteredRoom (Player newPlayer)
    {
        AddNewPlayer(newPlayer);
        UpdatePlayerCounter();
    }

    public override void OnPlayerLeftRoom (Player otherPlayer)
    {
        RemovePlayer(otherPlayer);
        UpdatePlayerCounter();
    }

    public override void OnLeftRoom ()
    {
        PhotonNetwork.LoadLevel(MENU_SCENE_NAME);
    }

    public override void OnDisconnected (DisconnectCause cause)
    {
        PhotonNetwork.LoadLevel(MENU_SCENE_NAME);
    }
    #endregion
}
