using UnityEngine;

public class Spin : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] float rotationSpeed = 1f;
    #endregion

    // Update is called once per frame
    void FixedUpdate()
    {
        float currRot = transform.localEulerAngles.z;
        currRot = Mathf.Lerp(currRot, currRot + rotationSpeed, Time.deltaTime);
        transform.localEulerAngles = Vector3.forward * currRot;
    }
}
