using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;

public class CheckBulletHit : MonoBehaviour
{
    #region PrivateVariables
    Health _health = null;
    PlayerMovement _playerMovement = null;
    ShootBullet _shootBullet = null;
    #endregion

    #region UnityLifecycles
    void Awake ()
    {
        _health = GetComponent<Health>();
        _playerMovement = GetComponent<PlayerMovement>();
        _shootBullet = GetComponent<ShootBullet>();
    }

    void OnCollisionEnter (Collision collision)
    {
        if (_health.Value == 0) {
            return;
        }

        if (collision.gameObject.CompareTag("Bullet")) {
            _health.Decrement();

            Hashtable reducePlayerHealth = new Hashtable{
                { Constants.SET_PLAYER_HEALTH, _health.Value }
            };

            PhotonNetwork.LocalPlayer.SetCustomProperties(reducePlayerHealth);

            if (_health.Value == 0) {
                Destroy(_playerMovement);
                Destroy(_shootBullet);
            }
        }
    }
    #endregion
}
