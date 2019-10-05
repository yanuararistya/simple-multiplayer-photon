using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

public class GameManager : MonoBehaviourPunCallbacks
{
    #region SerializeFields
    [SerializeField] GameObject _tempCamera = null;
    [SerializeField] GameObject _countdownCanvas = null;
    [SerializeField] float _startDistanceFromCenter = 20f;
    #endregion

    #region UnityLifecycles
    void Start ()
    {
        Cursor.visible = false;
        var levelLoaded = new Hashtable {
            { Constants.PLAYER_LOADED_LEVEL, true }
        };

        PhotonNetwork.LocalPlayer.SetCustomProperties(levelLoaded);
    }

    public override void OnEnable ()
    {
        base.OnEnable();
        CountdownTimer.OnCountdownTimerHasExpired += StartGame;
    }

    public override void OnDisable ()
    {
        base.OnDisable();
        CountdownTimer.OnCountdownTimerHasExpired -= StartGame;
    }
    #endregion

    #region PrivateMethods
    bool IsAllPlayersLoaded ()
    {
        foreach (var p in PhotonNetwork.PlayerList) {
            object isPlayerLoaded = false;
            if (p.CustomProperties.TryGetValue(Constants.PLAYER_LOADED_LEVEL, out isPlayerLoaded)) {
                if ((bool) isPlayerLoaded) {
                    continue;
                }

                return false;
            }
        }

        return true;
    }

    void StartGame ()
    {
        _countdownCanvas.SetActive(false);

        float angularStart = (360f / PhotonNetwork.CurrentRoom.PlayerCount) * PhotonNetwork.LocalPlayer.GetPlayerNumber();
        float x = _startDistanceFromCenter * Mathf.Sin(angularStart * Mathf.Deg2Rad);
        float z = _startDistanceFromCenter * Mathf.Cos(angularStart * Mathf.Deg2Rad);
        Vector3 initPos = new Vector3(x, 0f, z);
        Quaternion initRot = Quaternion.Euler(0f, angularStart, 0f);

        Destroy(_tempCamera);
        PhotonNetwork.Instantiate(Constants.GAME_PLAYER_PREFAB_NAME, initPos, initRot);
    }
    #endregion

    #region PunCallbacks
    public override void OnPlayerPropertiesUpdate(Player target, Hashtable changedProps)
    {
        if (!PhotonNetwork.IsMasterClient) {
            return;
        }

        if (changedProps.ContainsKey(Constants.PLAYER_LOADED_LEVEL)) {
            if (IsAllPlayersLoaded()) {
                Hashtable startCountdown = new Hashtable {
                    { CountdownTimer.CountdownStartTime, (float) PhotonNetwork.Time }
                };

                PhotonNetwork.CurrentRoom.SetCustomProperties(startCountdown);
            }
        }
    }
    #endregion
}
