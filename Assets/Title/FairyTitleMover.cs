using UnityEngine;
using System.Collections;

public class FairyTitleMover : MonoBehaviour {

    [SerializeField]
    TitleStartter Startter = null;

    [SerializeField]
    float CameraMoveToTime = 3.0f;

    [SerializeField]
    GameObject TreeObject = null;

    Animator Anima;

    
    float Duration = 0;

	// Use this for initialization
	void Start () {
        Anima = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    /// <summary>
    /// 近づいてくる処理
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitOnComing()
    {
        yield return new WaitForSeconds(Duration / 2);

        Anima.SetTrigger("OnGuideTrigger");

        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("CameraMoveToPath"), "time", CameraMoveToTime, "easetype", iTween.EaseType.easeOutSine));
        transform.parent = Camera.main.transform;
        StartCoroutine("LookToDirection");
    }

    const float FairyLookAtTime = 2.0f;

    /// <summary>
    /// 見る方向の処理
    /// </summary>
    /// <returns></returns>
    IEnumerator LookToDirection()
    {
        yield return new WaitForSeconds(CameraMoveToTime);

        iTween.LookTo(gameObject, TreeObject.transform.position, FairyLookAtTime);
        StartCoroutine("StartStartter");
    }


    /// <summary>
    /// タイトルのゲームスタートする処理
    /// </summary>
    /// <returns></returns>
    IEnumerator StartStartter()
    {
        yield return new WaitForSeconds(FairyLookAtTime);

        Startter.StartMove();
    }

    /// <summary>
    /// スタートアニメーション
    /// </summary>
    public void StartAnimation()
    {
        Anima.SetTrigger("OnWakeUpTrigger");

        var currentState = Anima.GetCurrentAnimatorStateInfo(0);
        Duration = currentState.length;
        
        StartCoroutine("WaitOnComing");
    }

}
