using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

public class GameManager : MonoBehaviourPunCallbacks
{
    #region SerializeFields
    [SerializeField] GameObject _tempCamera = null;
    [SerializeField] GameObject _countdownCanvas = null;
    [SerializeField] GameObject _winnerCanvas = null;
    [SerializeField] Text _winnerLabel  = null;
    [SerializeField] float _startDistanceFromCenter = 20f;
    #endregion

    #region PrivateVariables
    List<Player> _existingPlayers = new List<Player>();
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
        foreach (var p in PhotonNetwork.PlayerList) {
            _existingPlayers.Add(p);
        }

        _countdownCanvas.SetActive(false);

        float angularStart = (360f / PhotonNetwork.CurrentRoom.PlayerCount) * PhotonNetwork.LocalPlayer.GetPlayerNumber();
        float x = _startDistanceFromCenter * Mathf.Sin(angularStart * Mathf.Deg2Rad);
        float z = _startDistanceFromCenter * Mathf.Cos(angularStart * Mathf.Deg2Rad);
        Vector3 initPos = new Vector3(x, 0f, z);
        Quaternion initRot = Quaternion.Euler(0f, angularStart, 0f);

        Destroy(_tempCamera);
        PhotonNetwork.Instantiate(Constants.GAME_PLAYER_PREFAB_NAME, initPos, initRot);
    }

    void KillPlayer (Player player)
    {
        _existingPlayers.Remove(player);
        if (_existingPlayers.Count == 1) {
            _winnerCanvas.SetActive(true);
            _winnerLabel.text = string.Format(_winnerLabel.text, _existingPlayers[0]);
        }
    }
    #endregion

    #region PunCallbacks
    public override void OnPlayerPropertiesUpdate(Player target, Hashtable changedProps)
    {
        if (changedProps.ContainsKey(Constants.SET_PLAYER_HEALTH)) {
            object healthValue = null;
            if (changedProps.TryGetValue(Constants.SET_PLAYER_HEALTH, out healthValue)) {
                if ((int)healthValue == 0) {
                    KillPlayer(target);
                }
            }
        }

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

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        KillPlayer(otherPlayer);
    }
    #endregion
}
