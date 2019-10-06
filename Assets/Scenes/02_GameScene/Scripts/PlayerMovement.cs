using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] float _movementSpeed = 6f;
    #endregion

    #region PrivateVariables
    Rigidbody _rigidbody = null;
    PhotonView _photonView = null;
    #endregion

    #region UnityLifecycles
    void Awake ()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _photonView = GetComponent<PhotonView>();
    }
    void FixedUpdate ()
    {
        if (!_photonView.IsMine || GameManager.Instance.isFinished) {
            return;
        }

        float hInput = Input.GetAxis(Constants.HORIZONTAL_INPUT_NAME) * _movementSpeed * Time.deltaTime;
        float vInput = Input.GetAxis(Constants.VERTICAL_INPUT_NAME) * _movementSpeed * Time.deltaTime;

        Vector3 forwardMove = transform.forward * vInput;
        Vector3 sidewaysMove = transform.right * hInput;

        _rigidbody.MovePosition(transform.position + forwardMove + sidewaysMove);
    }
    #endregion
}
