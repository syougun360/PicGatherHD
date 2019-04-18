using UnityEngine;
using System.Collections;

public class OneMoreFeverStarter : EventStarterBase
{

    // Use this for initialization
    void Start()
    {

        DeltaFeverTime = AgainCoolTime;

        GetManager();

    }

    /// <summary>
    /// すでに１度スコアゲージがマックスになったかどうか
    /// </summary>
    private bool IsAlreadyMaxScore = false;

    /// <summary>
    /// 前回フィーバーした時からの経過時間
    /// </summary>
    private float DeltaFeverTime = 0.0f;

    /// <summary>
    /// フィーバー確変を行わないクールタイム
    /// </summary>
    private const float AgainCoolTime = 60.0f;

    // Update is called once per frame
    void Update()
    {

        /*イベントの開始条件*/
        /// フィーバーゲージがMAX→MINになった時
        if (ModeManager.IsFerverMode && DeltaFeverTime >= AgainCoolTime)
        {
            IsAlreadyMaxScore = true;
        }

        if(IsAlreadyMaxScore && !ModeManager.IsFerverMode)
        {
            IsAlreadyMaxScore = false;

            /// 生成する確率
            const int MaxRange = 5;
            if (Random.Range(0, MaxRange) == 0)
            {
                BeginEvent();
                DeltaFeverTime = 0.0f;
            }
        }

        if(DeltaFeverTime < AgainCoolTime)
        {
            DeltaFeverTime += Time.deltaTime;
        }
    }

    /// <summary>
    /// イベント開始
    /// </summary>
    protected override void BeginEvent()
    {
        base.BeginEvent();

        EventMngr.BeginEvent(OriginEventPrefab);
    }
}
