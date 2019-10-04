using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    #region UnityLifecycles
    void Awake ()
    {
        DontDestroyOnLoad(this);
    }
    #endregion
}
