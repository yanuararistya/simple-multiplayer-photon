using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] float speed = 200f;
    #endregion

    #region UnityLifecycles
    void Awake ()
    {
        Destroy(gameObject, 3f);
    }

    void OnCollisionEnter (Collision collision)
    {
        Destroy(gameObject);
    }
    #endregion

    #region PublicMethods
    public void InitializeBullet (Vector3 direction, float lag)
    {
        transform.forward = direction;
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = direction * speed;
        rigidbody.position += rigidbody.velocity * lag; 
    }
    #endregion
}
