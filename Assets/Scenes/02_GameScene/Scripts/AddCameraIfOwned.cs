using UnityEngine;
using Photon.Pun;

public class AddCameraIfOwned : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] PhotonView _photonView = null;
    #endregion

    #region UnityLifecycles
    void Awake ()
    {
        if (_photonView.Owner == PhotonNetwork.LocalPlayer) {
            gameObject.AddComponent<Camera>();
            gameObject.AddComponent<AudioListener>();
        }

        Destroy(this);
    }
    #endregion
}
