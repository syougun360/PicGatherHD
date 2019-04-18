using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnableSaveButton : MonoBehaviour
{
    private TutorialManager TutorialMngr = null;
    private CampusCaptureController CCController = null;
    private CampusTemplateSetting CampusTemplate = null;
    private Image ThisImage = null;
    // Use this for initialization
    void Start()
    {
        TutorialMngr = FindObjectOfType<TutorialManager>();
        CCController = FindObjectOfType<CampusCaptureController>();
        CampusTemplate = FindObjectOfType(typeof(CampusTemplateSetting)) as CampusTemplateSetting;
        ThisImage = GetComponent<Image>();
    }


    // Update is called once per frame
    void Update()
    {
        ThisImage.enabled = EnableImage();

        CanDestroy();

    }

    private void CanDestroy()
    {
        if (TutorialMngr.AlreadyEndedList[(int)TutorialManager.TutorialList.DrawCloud] &&
           TutorialMngr.AlreadyEndedList[(int)TutorialManager.TutorialList.DrawFairy] &&
           TutorialMngr.AlreadyEndedList[(int)TutorialManager.TutorialList.DrawLeaf])
            Destroy(transform.parent.gameObject);
    }

    /// <summary>
    /// 描画する条件が満たされているか
    /// </summary>
    /// <returns>満たしている...true 満たしていない...false</returns>
    private bool EnableImage()
    {
        if (CCController.CharaManager == null) return false;
        if (!TutorialMngr.IsCampusMode) return false;
        if (CampusTemplate.IsSelect)
        if (CCController.CharaManager.Name == "Leaf" || CCController.CharaManager.Name == "Fairy" || CCController.CharaManager.Name == "Cloud")
        {
            return true;
        }

        return false;
    }
}
