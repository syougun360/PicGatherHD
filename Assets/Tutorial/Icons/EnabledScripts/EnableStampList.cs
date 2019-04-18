using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnableStampList : MonoBehaviour {

    private TutorialManager TutorialMngr = null;
    private Image ThisImage = null;

    private StampListMover SLMover = null;

    // Use this for initialization
    void Start()
    {
        TutorialMngr = FindObjectOfType<TutorialManager>();
        ThisImage = GetComponent<Image>();
        SLMover = FindObjectOfType<StampListMover>();
    }


    // Update is called once per frame
    void Update()
    {
        ThisImage.enabled = EnableImage();

        if (TutorialMngr.AlreadyEndedList[(int)TutorialManager.TutorialList.PutStamp] &&
            SLMover.State == StampListMover.STATE.Close)
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
            !TutorialMngr.AlreadyEndedList[(int)TutorialManager.TutorialList.SelectStampList] &&
            SLMover.State == StampListMover.STATE.Close)
        {
            return true;
        }

        if(TutorialMngr.AlreadyEndedList[(int)TutorialManager.TutorialList.PutStamp] &&
            SLMover.State == StampListMover.STATE.Stop)
        {
            return true;
        }

        return false;
    }
}
