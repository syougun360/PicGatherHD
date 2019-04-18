using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Resetter : MonoBehaviour {

    [SerializeField]
    GameObject shareButton = null;

    [SerializeField]
    GameObject stamp = null;

    [SerializeField]
    GameObject drawingCampus = null;

    [SerializeField]
    GameObject stampGesture = null;

    [SerializeField]
    GameObject tutorialSkip = null;

    [SerializeField]
    AllDataClear DataClear = null;

    public void TreeReset()
    {
        Camera.main.GetComponent<ResetDirector>().enabled = true;

        NonActive(gameObject);
        NonActive(stampGesture);
        NonActive(tutorialSkip);
        NonActive(drawingCampus);
        NonActive(stamp);
        NonActive(shareButton);

        DataClear.Delete();

        ModeManager.ChangeResetMode();
    }

    /// <summary>
    /// UIを非アクティブにする
    /// </summary>
    /// <param name="obj"></param>
    void NonActive(GameObject obj)
    {
        if (obj != null)
        {
            obj.SetActive(false);
        }
    }
}
