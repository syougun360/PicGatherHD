using UnityEngine;
using System.Collections;
using System;

public class StarCollectedStarter : EventStarterBase
{
    [SerializeField]
    GameObject brightStarPrefab = null;

    GameObject brightStar = null;

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
        //      if()
        CrateBrightStar();
        EventStart();
    }


    void CrateBrightStar()
    {
        if (brightStar) return;
        if (!Judgment()) return;

        brightStar = Instantiate(brightStarPrefab) as GameObject;
    }

    /// <summary>
    /// イベント開始条件
    /// </summary>
    void EventStart()
    {
        if (!brightStar) return;

        BeginEvent();
    }

    /// <summary>
    /// イベント開始
    /// </summary>
    protected override void BeginEvent()
    {
        base.BeginEvent();

        ///イベントの発生条件を書く
        if (TouchManager.IsMouseButtonDown(brightStar) || TouchManager.IsTouching(brightStar))
        {
            EventMngr.BeginEvent(OriginEventPrefab);
        }
    }
}
