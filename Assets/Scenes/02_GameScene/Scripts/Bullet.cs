using UnityEngine;
using Photon.Realtime;

public class Bullet : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] float speed = 200f;
    #endregion

    #region PrivateVariables
    Player _shooter = null;
    #endregion

    #region PublicProperties
    public Player Shooter { get { return _shooter; } }
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
    public void InitializeBullet (Player shooter, Vector3 direction, float lag)
    {
        _shooter = shooter;

        transform.forward = direction;

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = direction * speed;
        rigidbody.position += rigidbody.velocity * lag; 
    }
    #endregion
}
