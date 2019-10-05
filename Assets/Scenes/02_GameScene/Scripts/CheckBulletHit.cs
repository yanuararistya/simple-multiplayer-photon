using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;

public class CheckBulletHit : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] Health _health = null;
    #endregion

    #region UnityLifecycles
    void Awake ()
    {
        _health = GetComponent<Health>();
    }

    void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) {
            Hashtable reducePlayerHealth = new Hashtable{
                { Constants.SET_PLAYER_HEALTH, _health.Value-- }
            };

            PhotonNetwork.LocalPlayer.SetCustomProperties(reducePlayerHealth);
        }
    }
    #endregion
}
