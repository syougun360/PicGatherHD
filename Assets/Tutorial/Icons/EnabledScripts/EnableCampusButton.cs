using UnityEngine;
using System.Collections;

public class EnableCampusButton : MonoBehaviour
{
    private TutorialManager TutorialMngr = null;

    // Use this for initialization
    void Start()
    {
        TutorialMngr = FindObjectOfType<TutorialManager>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!EnableImage())
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
        if (TutorialMngr.AlreadyEndedList[(int)TutorialManager.TutorialList.DrawLeaf] &&
            TutorialMngr.AlreadyEndedList[(int)TutorialManager.TutorialList.DrawFairy] &&
            TutorialMngr.AlreadyEndedList[(int)TutorialManager.TutorialList.DrawCloud])
        {
            return false;
        }

        return true;
    }
}
