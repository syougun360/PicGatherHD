using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour {

    /// <summary>
    /// イベントを起こす際に必要になる情報
    /// </summary>
    public struct EventInfo
    {
        /// <summary>
        /// 発生させるイベント
        /// </summary>
        public GameObject PlayingEventPrefab;

        /// <summary>
        /// イベントを開始するフレームならtrue
        /// </summary>
        public bool BeginEvent;

        /// <summary>
        /// 現在イベント中かどうかを判断する
        /// </summary>
        public bool NowPlaying;
    }

    /// <summary>
    /// イベントの情報を統括するメンバ変数
    /// </summary>
    public EventInfo EventInformation = new EventInfo();

    [SerializeField]
    List<GameObject> HideGameObjects = new List<GameObject>();

    [SerializeField]
    GameObject IconPrefab = null;

    GameObject Icon = null;

    void Start()
    {
        EventInformation.BeginEvent = false;
        EventInformation.NowPlaying = false;
    }

	// Update is called once per frame
	void Update () {
        if(EventInformation.BeginEvent)
        {
            Instantiate(EventInformation.PlayingEventPrefab);
            EventInformation.NowPlaying = true;
            EventInformation.BeginEvent = false;
            
            HideUI();
            ModeManager.ChangeEventMode();
        }
	}

    /// <summary>
    /// アイコンを生成
    /// </summary>
    /// <param name="icon"></param>
    public void IconInstantiate(Sprite icon)
    {
        Icon = (GameObject)Instantiate(IconPrefab, IconPrefab.transform.position, Quaternion.identity);
        Icon.GetComponent<Image>().sprite = icon;
        Icon.transform.parent = GameObject.Find("UIRoot").transform;
    }

    /// <summary>
    /// UIを隠す
    /// </summary>
    void HideUI()
    {
        UIEnabled.Unavailable();
        foreach (var obj in HideGameObjects)
        {
            obj.SetActive(false);
        }

    }

    /// <summary>
    /// アイコンを削除
    /// </summary>
    public void IconDestroy()
    {
        Destroy(Icon);
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    public void Finish()
    {
        UIEnabled.Enabled();

        foreach (var obj in HideGameObjects)
        {
            obj.SetActive(true);
        }

        ModeManager.ChangeGameMode();

    }

    /// <summary>
    /// 外部からイベントを開始させる
    /// </summary>
    /// <param name="eventPrefab">開始するイベント</param>
    public void BeginEvent(GameObject eventPrefab)
    {
        /// イベント中なら発生させない
        if (EventInformation.NowPlaying) return;

        EventInformation.PlayingEventPrefab = eventPrefab;
        EventInformation.BeginEvent = true;
    }

}
