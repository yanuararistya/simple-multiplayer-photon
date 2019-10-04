using UnityEngine;
using UnityEngine.UI;

public class PlayerListItem : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] Text _name = null;  
    #endregion

    #region PublicMethods
    public void SetName (string name)
    {
        _name.text = name;
    }
    #endregion
}
