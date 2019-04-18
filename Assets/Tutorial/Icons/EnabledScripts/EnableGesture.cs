using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnableGesture : MonoBehaviour
{

    private TutorialManager TutorialMngr = null;
    private CampusCaptureController CCController = null;
    private CampusTemplateSetting CampusTemplate = null;
    private Image ThisImage = null;

    /// <summary>
    /// 描く絵の種類
    /// </summary>
    enum CampusType
    {
        Cloud,
        Fairy,
        Leaf,
        GUARD
    };

    /// <summary>
    /// すでに選ばれたキャンパスの絵が描かれているかどうか
    /// </summary>
    private bool[] AlreadyDrawn = new bool[(int)CampusType.GUARD];

    // Use this for initialization
    void Start()
    {
        CCController = FindObjectOfType<CampusCaptureController>();
        CampusTemplate = FindObjectOfType(typeof(CampusTemplateSetting)) as CampusTemplateSetting;
        TutorialMngr = FindObjectOfType<TutorialManager>();
        ThisImage = GetComponent<Image>();
        for(int i = 0;i < (int)CampusType.GUARD;i++)
        {
            AlreadyDrawn[i] = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        AlreadyDrawnCheck();
        ThisImage.enabled = EnableImage();

        if (TutorialMngr.AlreadyEndedList[(int)TutorialManager.TutorialList.DrawCloud] &&
            TutorialMngr.AlreadyEndedList[(int)TutorialManager.TutorialList.DrawFairy] &&
            TutorialMngr.AlreadyEndedList[(int)TutorialManager.TutorialList.DrawLeaf])
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
        if (!TutorialMngr.IsCampusMode) return false;

        if (CCController.CharaManager == null) return false;

        if (CampusTemplate.IsSelect)
        {
            if (!TutorialMngr.AlreadyEndedList[(int)TutorialManager.TutorialList.DrawCloud] &&
                CCController.CharaManager.Name == "Cloud" && !AlreadyDrawn[(int)CampusType.Cloud])
            {
                return true;
            }

            if (!TutorialMngr.AlreadyEndedList[(int)TutorialManager.TutorialList.DrawFairy] &&
                CCController.CharaManager.Name == "Fairy" && !AlreadyDrawn[(int)CampusType.Fairy])
            {
                return true;
            }

            if (!TutorialMngr.AlreadyEndedList[(int)TutorialManager.TutorialList.DrawLeaf] &&
                CCController.CharaManager.Name == "Leaf" && !AlreadyDrawn[(int)CampusType.Leaf])
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 選ばれた絵のキャンパスがすでに描かれたかどうかを調べ、選ばれていた場合AlreadyDrawnにtrueを入れる
    /// </summary>
    private void AlreadyDrawnCheck()
    {
        if (CampusTemplate.IsSelect == false) return;
        if (CCController.CharaManager == null) return;

        if (Input.touchCount > 0)
        {
            foreach (var touch in Input.touches)
            {
                if (CCController.CaptureRect.x > touch.position.x || CCController.CaptureRect.x + CCController.CaptureRect.width < touch.position.x ||
                    CCController.CaptureRect.y > touch.position.y || CCController.CaptureRect.y + CCController.CaptureRect.height < touch.position.y)
                    return;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (CCController.CaptureRect.x > Input.mousePosition.x || CCController.CaptureRect.x + CCController.CaptureRect.width < Input.mousePosition.x ||
                CCController.CaptureRect.y > Input.mousePosition.y || CCController.CaptureRect.y + CCController.CaptureRect.height < Input.mousePosition.y)
                return;

        }
        else return;

        switch(CCController.CharaManager.Name)
        {
            case "Cloud":
                AlreadyDrawn[(int)CampusType.Cloud] = true;
                break;
            case "Fairy":
                AlreadyDrawn[(int)CampusType.Fairy] = true;
                break;
            case "Leaf":
                AlreadyDrawn[(int)CampusType.Leaf] = true;
                break;
        }

    }
}
