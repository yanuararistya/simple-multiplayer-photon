using UnityEngine;
using Photon.Pun;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    #region Constants
    const int MAX_PLAYERS = 16;
    const string LOBBY_SCENE_NAME = "LobbyScene";
    #endregion

    #region PrivateVariables
    bool isConnecting = false;
    #endregion

    #region UnityLifecycles
    void Awake ()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    #endregion

    #region UICallbacks
    public void OnJoinButtonClicked ()
    {
        isConnecting = true;

        if (PhotonNetwork.IsConnected) {
            PhotonNetwork.JoinRandomRoom();
        } else {
            PhotonNetwork.ConnectUsingSettings();
        }

        LoadingScene.Instance.Show();
    }
    #endregion

    #region PunCallbacks
    public override void OnConnectedToMaster()
    {
        if (isConnecting) {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions {MaxPlayers = MAX_PLAYERS});
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        LoadingScene.Instance.Hide();
    }

    public override void OnDisconnected(Photon.Realtime.DisconnectCause cause)
    {
        isConnecting = false;
        LoadingScene.Instance.Hide();
    }

    public override void OnJoinedRoom()
    {
        LoadingScene.Instance.Hide();
        // only load if we are the first player
        // other than the first player scene will be synced automatically
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.LoadLevel(LOBBY_SCENE_NAME);
        }
    }
    #endregion
}
