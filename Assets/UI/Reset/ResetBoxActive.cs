using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResetBoxActive : MonoBehaviour {

    [SerializeField]
    GameObject SelectBox = null;

    Button ResetButton = null;

    void Start()
    {
        ResetButton = GetComponent<Button>();
    }

    void Update()
    {
        if (UISelectManager.IsNoneMode) return;
        if (ModeManager.IsGameMode) return;

        SelectBox.SetActive(false);
        ResetButton.enabled = true;
        UISelectManager.ChangeNoneMode();
    }

    /// <summary>
    /// セレクトボックスをアクティブにする
    /// </summary>
    public void SetActiveSelectBox()
    {
        if (!UISelectManager.IsNoneMode) return;

        SelectBox.SetActive(true);
        UISelectManager.ChangeResetMode();
        ResetButton.enabled = false;
    }

}
