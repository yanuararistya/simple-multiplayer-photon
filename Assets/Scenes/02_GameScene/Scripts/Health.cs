using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using Photon.Pun;

public class Health : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] Text _healthLabel = null;
    #endregion

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

    #region PublicMethods
    public void Decrement ()
    {
        Value--;
        _healthLabel.text = string.Format("{0}/10", Value);
    }
    #endregion
}
