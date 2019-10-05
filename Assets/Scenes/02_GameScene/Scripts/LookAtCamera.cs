using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    #region PrivateVariables
    Transform _cameraTransform = null;
    #endregion

    #region UnityLifecycle
    void Start ()
    {
        _cameraTransform = Camera.main.transform;
    }

    void Update ()
    {
        transform.LookAt(_cameraTransform);
    }
    #endregion
}
