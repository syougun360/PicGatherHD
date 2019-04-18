using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnableCloudButton : MonoBehaviour
{
    private TutorialManager TutorialMngr = null;
    private CampusCaptureController CCController = null;
    private CampusTemplateSetting CampusTemplate = null;
    private Image ThisImage = null;
    // Use this for initialization
    void Start()
    {
        CCController = FindObjectOfType<CampusCaptureController>();
        CampusTemplate = FindObjectOfType(typeof(CampusTemplateSetting)) as CampusTemplateSetting;
        TutorialMngr = FindObjectOfType<TutorialManager>();
        ThisImage = GetComponent<Image>();
    }


    // Update is called once per frame
    void Update()
    {
        ThisImage.enabled = EnableImage();

        if (TutorialMngr.AlreadyEndedList[(int)TutorialManager.TutorialList.DrawCloud])
        {
            Destroy(transform.parent.gameObject);
        }

    }

    /// <summary>
    /// 描画する条件が満たされているか
    /// </summary>
    /// <returns>満たしている...true 満たしていない...false</returns>
    private bool EnableImage()
    {
        if (TutorialMngr.IsCampusMode &&
            !TutorialMngr.AlreadyEndedList[(int)TutorialManager.TutorialList.DrawCloud] &&
            (CampusTemplate.IsSelect == false || CCController.CharaManager.Name != "Cloud"))
        {
            return true;
        }

        return false;
    }
}
