using UnityEngine;

public class LoadingScene : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] GameObject _loadingPanel = null;
    #endregion

    #region Singleton
    public static LoadingScene Instance { get; private set; }
    #endregion

    #region UnityLifecycles
    void Awake ()
    {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }
    #endregion

    #region PublicStaticMethods
    public void Show ()
    {
        _loadingPanel.SetActive(true);
    }

    public void Hide ()
    {
        _loadingPanel.SetActive(false);
    }
    #endregion
}
