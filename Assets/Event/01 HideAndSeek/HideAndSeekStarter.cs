using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HideAndSeekStarter : EventStarterBase
{

    bool isStart = false;

    /// <summary>
    /// 太陽を強調するアイコン
    /// </summary>
    [SerializeField]
    private GameObject AnnouncePrefab = null;

    /// <summary>
    /// すでにアイコンを表示したかどうか
    /// している...true していない...false
    /// </summary>
    private bool AlreadyCreateAnnounce = false;

    // Use this for initialization
    void Start()
    {
        GetManager();
    }

    // Update is called once per frame
    void Update()
    {
        StartJudgmentUpdate();

        if (!Judgment()) return;
        if (!AlreadyCreateAnnounce) CreateSunAnnounce();
        if (!isStart) return;
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
        isStart = false;
        GameObject.Find("SunCharacter").GetComponent<Image>().enabled = false;
        AlreadyCreateAnnounce = false;
    }

    public void OnSunButton()
    {
        if (!ModeManager.IsGameMode) return;

        isStart = true;
    }

    /// <summary>
    /// 強調するアイコンを表示する
    /// </summary>
    private void CreateSunAnnounce()
    {
        var clone = (GameObject)Instantiate(AnnouncePrefab,AnnouncePrefab.transform.position,Quaternion.identity);
        clone.transform.SetParent(GameObject.Find("UIRoot").gameObject.transform);
        var cloneRectTrans = clone.transform as RectTransform;
        var announceRectTrans = AnnouncePrefab.transform as RectTransform;
        cloneRectTrans.anchoredPosition3D = announceRectTrans.anchoredPosition3D;

        AlreadyCreateAnnounce = true;
    }
}
