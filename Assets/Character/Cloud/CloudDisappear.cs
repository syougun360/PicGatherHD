/// ---------------------------------------------------
/// date ： 2015/02/10    
/// brief ： フィーバー終了時に雲の姿を消す処理
/// author ： Yamada Masamistu
/// ---------------------------------------------------

using UnityEngine;
using System.Collections;

public class CloudDisappear : MonoBehaviour
{

    enum STATE
    {
        None,   //  なにもない
        Start,  //  開始した。
        Finish, //  終了した。
    };

    const float DisappearTime = 7.0f;

    CloudMover Mover = null;

    float Count = 0;
    STATE State = STATE.None;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        FerverStart();
        FerverFinish();
        Disappear();
	}


    /// <summary>
    /// フィーバーがスタートした処理
    /// </summary>
    void FerverStart()
    {
        if (!ModeManager.IsFerverMode) return;
        if (State != STATE.None) return;

        State = STATE.Start;
        Mover = GetComponent<CloudMover>();
    }

    /// <summary>
    /// フィーバー終了した時の処理
    /// </summary>
    void FerverFinish()
    {
        if (ModeManager.IsFerverMode) return;
        if (State != STATE.Start) return;
        if (!Mover.IsReturnlMove) return;

        State = STATE.Finish;
        iTween.ScaleTo(gameObject, Vector3.zero, DisappearTime);
    }


    /// <summary>
    /// 姿を消す処理
    /// </summary>
    void Disappear()
    {
        if (State != STATE.Finish) return;

        Count += Time.deltaTime;
        if (Count >= DisappearTime)
        {
            var manager = GameObject.FindObjectOfType(typeof(CloudManagerController)) as CloudManagerController;
            manager.ChildrensDataSave();
            manager.LimitCreateNumIncrease();

            Destroy(gameObject);

        }
    }
}
