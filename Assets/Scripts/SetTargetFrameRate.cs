using UnityEngine;

public class SetTargetFrameRate : MonoBehaviour
{
    void Awake ()
    {
        Application.targetFrameRate = 60;
    }
}
