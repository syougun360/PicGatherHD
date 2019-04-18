using UnityEngine;
using System.Collections;

public class PlayingMelodyStarter : EventStarterBase
{
    [SerializeField]
    BGMManager BGM = null;

    // Use this for initialization
    void Start()
    {
        GetManager();

        EventMngr = GetComponent<EventManager>();

    }


    // Update is called once per frame
    void Update()
    {
        StartJudgmentUpdate();

        if (!IsBeginTiming()) return;


        BeginEvent();
    }

    /// <summary>
    /// イベント開始
    /// </summary>
    protected override void BeginEvent()
    {
        base.BeginEvent();

        ///イベントの発生条件を書く

        CanStart = false;
        EventMngr.BeginEvent(OriginEventPrefab);
        BGM.Stop();
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
