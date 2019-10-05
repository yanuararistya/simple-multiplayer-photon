using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] float _movementSpeed = 6f;
    #endregion

    #region PrivateVariables
    CharacterController _controller = null;
    #endregion

    #region UnityLifecycles
    void Awake ()
    {
        _controller = GetComponent<CharacterController>();
    }
    void FixedUpdate ()
    {
        float hInput = Input.GetAxis(Constants.HORIZONTAL_INPUT_NAME) * _movementSpeed * Time.deltaTime;
        float vInput = Input.GetAxis(Constants.VERTICAL_INPUT_NAME) * _movementSpeed * Time.deltaTime;

        Vector3 forwardMove = transform.forward * vInput;
        Vector3 sidewaysMove = transform.right * hInput;

        _controller.SimpleMove(forwardMove + sidewaysMove);
    }
    #endregion
}
