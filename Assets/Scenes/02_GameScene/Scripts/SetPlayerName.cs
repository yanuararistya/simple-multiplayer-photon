using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class SetPlayerName : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] PhotonView _photonView = null;
    #endregion

    #region PrivateVariables
    Text _nameLabel = null;
    #endregion

    #region UnityLifecycles
    void Awake ()
    {
        _nameLabel = GetComponent<Text>();
        _nameLabel.text = _photonView.Owner.NickName;
    }
    #endregion
}
