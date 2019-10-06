using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;

public class CheckBulletHit : MonoBehaviour
{
    #region PrivateVariables
    Health _health = null;
    PlayerMovement _playerMovement = null;
    ShootBullet _shootBullet = null;
    PhotonView _photonView = null;
    #endregion

    #region UnityLifecycles
    void Awake ()
    {
        _health = GetComponent<Health>();
        _playerMovement = GetComponent<PlayerMovement>();
        _shootBullet = GetComponent<ShootBullet>();
        _photonView = GetComponent<PhotonView>();
    }

    void OnCollisionEnter (Collision collision)
    {
        if (_health.Value == 0) {
            return;
        }

        if (collision.gameObject.CompareTag("Bullet")) {
            var bullet = collision.gameObject.GetComponent<Bullet>();

            // bullets should not damage the shooter
            if (bullet.Shooter == _photonView.Owner) {
                return;
            }

            if (bullet.Shooter == PhotonNetwork.LocalPlayer) {
                OnDamaged();
                _photonView.RPC("Damage", RpcTarget.Others);
            }
        }
    }
    #endregion

    #region PrivateMethods
    void OnDamaged ()
    {
        _health.Decrement();
        if (_photonView.Owner == PhotonNetwork.LocalPlayer) {
            GameManager.Instance.UpdateHUDHealthLabel(_health.Value);
        }
                
        if (_health.Value == 0) {
            Destroy(_playerMovement);
            Destroy(_shootBullet);
            GameManager.Instance.KillPlayer(_photonView.Owner);
        }
    }
    #endregion

    #region PunCallbacks
    [PunRPC]
    public void Damage (PhotonMessageInfo info)
    {
        OnDamaged();
    }
    #endregion
}
