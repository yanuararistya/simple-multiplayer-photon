using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;

public class Health : MonoBehaviour
{
    #region PublicProperties
    public int Value { get; set; }
    #endregion

    #region UnityLifecycles
    void Awake ()
    {
        Value = 10;

        Hashtable setInitialHealth = new Hashtable{
            { Constants.SET_PLAYER_HEALTH, Value }
        };

        PhotonNetwork.LocalPlayer.SetCustomProperties(setInitialHealth);
    }
    #endregion
}
