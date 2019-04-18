using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResetNonSelecter : MonoBehaviour {

    [SerializeField]
    GameObject SelectBox = null;

    Button ResetButton = null;

    void Start()
    {
        ResetButton = GetComponent<Button>();
    }

    public void NonActiveSelectBox()
    {
        SelectBox.SetActive(false);
        UISelectManager.ChangeNoneMode();
        ResetButton.enabled = true;
    }
}
