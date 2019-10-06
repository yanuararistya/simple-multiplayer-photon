using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ShootBullet : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] GameObject _bulletPrefab = null;
    [SerializeField] Transform _barrel = null;
    [SerializeField] Transform _bulletStartTransform = null;
    #endregion

    #region PrivateVariables
    PhotonView _photonView = null;
    #endregion

    #region UnityLifecycles
    void Awake ()
    {
        _photonView = GetComponent<PhotonView>();
    }

    void Update ()
    {
        if (_photonView.Owner != PhotonNetwork.LocalPlayer || GameManager.Instance.isFinished) {
            return;
        }

        if (Input.GetMouseButtonDown(0)) {
            _photonView.RPC("Shoot", RpcTarget.AllViaServer, _photonView.Owner, _bulletStartTransform.position, _barrel.rotation);
        }
    }
    #endregion

    #region PunCallbacks
    [PunRPC]
    public void Shoot (Player shooter, Vector3 position, Quaternion rotation, PhotonMessageInfo info)
    {
        float lag = (float)(PhotonNetwork.Time - info.SentServerTime);
        GameObject bulletObject = Instantiate(_bulletPrefab, position, Quaternion.identity);
        bulletObject.GetComponent<Bullet>().InitializeBullet(shooter, rotation * Vector3.forward, lag);
    }
    #endregion
}
