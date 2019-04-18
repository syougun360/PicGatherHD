using UnityEngine;
using System.Collections;

public class LonelyFairyStarter : EventStarterBase
{
    // Use this for initialization
    void Start()
    {
        GetManager();
    }

    // Update is called once per frame
    void Update()
    {
        StartJudgmentUpdate();

        /*イベントの開始条件*/
        if (!IsBeginTiming()) return;
           
        BeginEvent();
    }

    /// <summary>
    /// イベント開始
    /// </summary>
    protected override void BeginEvent()
    {
        base.BeginEvent();

        CanStart = false;
        EventMngr.BeginEvent(OriginEventPrefab);
    }


    /// <summary>
    /// イベント開始のタイミング（時間）かどうかを返す関数
    /// </summary>
    /// <returns>開始する...true 開始しない...false</returns>
    private bool IsBeginTiming()
    {
        if (Judgment()) return true;

        return false;
    }
}
