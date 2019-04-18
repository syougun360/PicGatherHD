using UnityEngine;
using System.Collections;

public class FlySwatterStarter : EventStarterBase
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
        if (!Judgment()) return;

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
}
