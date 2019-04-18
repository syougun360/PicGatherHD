using UnityEngine;
using System.Collections;

/// <summary>
/// イベントのスタートを管理するスクリプトの継承元
/// </summary>
public class EventStarterBase : MonoBehaviour {


    [SerializeField]
    [Range(6, 20)]
    protected int StartTime = 0;

    protected bool CanStart = false;

    /// <summary>
    /// クラス固有のイベントを所持する
    /// </summary>
    [SerializeField]
    protected GameObject OriginEventPrefab = null;

    [SerializeField]
    Sprite StartIcon = null;

    [SerializeField]
    bool IsIconNeed = false;

    bool CanIcon = true;

    const float IconDrawTime = 2.0f;

    /// <summary>
    /// EventManagerを得る
    /// </summary>
    protected EventManager EventMngr = null;

    // Use this for initialization
    protected void GetManager()
    {
        EventMngr = GetComponent<EventManager>();
	}
	
	// Update is called once per frame
    protected void StartJudgmentUpdate()
    {
        if (!CanStart) return;

        if (DateTimeController.NowTime.Hour == StartTime + 1)
        {
            CanIcon = true;
            CanStart = false;
        }
    }

  
    /// <summary>
    /// イベントを開始する
    /// </summary>
    protected virtual void BeginEvent()
    {
    }

    /// <summary>
    /// 開始できるかどうかの判定
    /// </summary>
    /// <returns></returns>
    protected bool Judgment()
    {
        if (!ModeManager.IsGameMode) return false;

        if (CanIcon && DateTimeController.NowTime.Hour == StartTime)
        {
            StartIconDrawTiming();
            return false;
        }
        
        if (CanStart) return true;

        return false;
    }

    /// <summary>
    /// 始まりのアイコンを表示するタイミング
    /// </summary>
    private void StartIconDrawTiming()
    {
        if (!IsIconNeed) return;

        CanIcon = false;
        EventMngr.IconInstantiate(StartIcon);
        StartCoroutine("StartIconDestroyTiming");
    }


    /// <summary>
    /// 始まりのアイコンを削除するタイミング
    /// </summary>
    /// <returns></returns>
    IEnumerator StartIconDestroyTiming()
    {
        yield return new WaitForSeconds(IconDrawTime);

        EventMngr.IconDestroy();
        CanStart = true;
    }
}
