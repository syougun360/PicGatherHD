using UnityEngine;
using System.Collections;

public class HideAndSeek : EventBase
{
    [SerializeField]
    GameObject openOurEyes = null;

    OpenOurEyesMover eventObject = null;

    FeverManager feverManager = null;

    // Use this for initialization
    void Start()
    {
        var cameraPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - Screen.width * 0.1f, Screen.height - Screen.height * 0.15f, 15));
        Instantiate(openOurEyes, cameraPos, Quaternion.identity);
        eventObject = GameObject.Find("OpenOurEyes(Clone)").GetComponent<OpenOurEyesMover>();
        feverManager = GameObject.Find("FeverGauge").GetComponent<FeverManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (eventObject.state != OpenOurEyesMover.State.FOUND) return;
        Finish();
    }

    /// <summary>
    /// 終了時の処理
    /// </summary>
    protected override void Finish()
    {
        feverManager.AddScore(feverManager.MaxFeverScore);
        base.Finish();

    }
}
