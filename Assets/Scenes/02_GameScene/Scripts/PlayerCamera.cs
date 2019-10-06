using UnityEngine;
using Photon.Pun;

public class PlayerCamera : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] Transform _playerBody = null;
    [SerializeField] PhotonView _photonView  = null;
    [SerializeField] float _mouseSensitivity = 150f;
    [SerializeField] float _minXRot = -90f;
    [SerializeField] float _maxXRot = 90f;
    #endregion

    #region PrivateVariables
    float _xAxisClamped = 0f;
    #endregion

    #region UnityLifecycles
    void Update ()
    {
        if (!_photonView.IsMine || GameManager.Instance.isFinished) {
            return;
        }

        float mouseX = Input.GetAxis(Constants.MOUSE_X_INPUT_NAME) * _mouseSensitivity;
        float mouseY = Input.GetAxis(Constants.MOUSE_Y_INPUT_NAME) * _mouseSensitivity;

        _xAxisClamped += mouseY;
        if (_xAxisClamped > _maxXRot) {
            _xAxisClamped = _maxXRot;
            mouseY = 0f;
            ClampXRot(360f - _maxXRot);
        } else if (_xAxisClamped < _minXRot) {
            _xAxisClamped = _minXRot;
            mouseY = 0f;
            ClampXRot(-_minXRot);
        }

        transform.Rotate(Vector3.left * mouseY);
        _playerBody.Rotate(Vector3.up * mouseX);
    }
    #endregion

    #region PrivateMethods
    void ClampXRot (float value)
    {
        Vector3 eulerRot = transform.eulerAngles;
        eulerRot.x = value;
        transform.eulerAngles = eulerRot;
    }
    #endregion
}
