using UnityEngine;
using Photon.Pun;

public class MatchmakingManager : MonoBehaviourPunCallbacks
{
    #region SerializeFields
    [SerializeField] MenuScene _menuScene = null;
    #endregion

    #region PrivateVariables
    bool isConnecting = false;
    #endregion

    #region UnityLifecycles
    void Awake ()
    {
    }
    #endregion

    #region UICallbacks
    public void OnJoinButtonClicked ()
    {
        isConnecting = true;
        PhotonNetwork.NickName = _menuScene.PlayerName;

        if (PhotonNetwork.IsConnected) {
            PhotonNetwork.JoinRandomRoom();
        } else {
            PhotonNetwork.ConnectUsingSettings();
        }

        LoadingScene.Instance.Show();
    }
    #endregion

    #region PunCallbacks
    public override void OnConnectedToMaster ()
    {
        if (isConnecting) {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinRandomFailed (short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions {MaxPlayers = (byte)Constants.MAX_PLAYERS});
    }

    public override void OnCreateRoomFailed (short returnCode, string message)
    {
        LoadingScene.Instance.Hide();
    }

    public override void OnDisconnected (Photon.Realtime.DisconnectCause cause)
    {
        isConnecting = false;
        LoadingScene.Instance.Hide();
    }

    public override void OnJoinedRoom ()
    {
        PhotonNetwork.LoadLevel(Constants.LOBBY_SCENE_NAME);
    }
    #endregion
}
