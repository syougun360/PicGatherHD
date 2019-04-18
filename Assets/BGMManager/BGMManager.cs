using UnityEngine;
using System.Collections;

/// <summary>
/// BGMを切り替える機能
/// </summary>
public class BGMManager : MonoBehaviour {

    [SerializeField]
    BGMPlayer player = null;

    [SerializeField]
    string morningResName = string.Empty;

    [SerializeField]
    string noonResName = string.Empty;

    [SerializeField]
    string nightResName = string.Empty;

    [SerializeField]
    string drawingResName = string.Empty;

    [SerializeField]
    FadeTimeData fadeTime = new FadeTimeData(2.0f, 2.0f);

    /// <summary
    /// BGMを切り替える
    /// </summary>
    void SelectPlay()
    {
        if (ModeManager.IsDrawingMode)
        {
            Drawing();
        }
        else if (ModeManager.IsGameMode)
        {
            TimeZone();
        }
    }

    /// <summary>
    /// お絵かきモード
    /// </summary>
    void Drawing()
    {
        player.Play(drawingResName, fadeTime);
    }

    /// <summary>
    /// 時間帯の再生
    /// </summary>
    void TimeZone()
    {
        if (DateTimeController.IsMorning)
        {
            player.Play(morningResName, fadeTime);
        }
        else if (DateTimeController.IsNoon)
        {
            player.Play(noonResName, fadeTime);
        }
        else if (DateTimeController.IsNight)
        {
            player.Play(nightResName, fadeTime);
        }
        else if (DateTimeController.IsSleep)
        {
            player.Play(nightResName, fadeTime);
        }
    }

    // Use this for initialization
    void Start()
    {
        SelectPlay();
    }

    /// <summary>
    /// BGMを止める
    /// </summary>
    void TimeZoneStop()
    {
        if (!DateTimeController.IsChanged) return;
        
        Stop();
    }

    /// <summary>
    /// 停止
    /// </summary>
    public void Stop()
    {
        player.Stop();
    }

    /// <summary>
    /// BGMを再生する
    /// </summary>
    void Play()
    {
        if (player.IsPlaying) return;

        SelectPlay();
    }

    // Update is called once per frame
    void Update()
    {
        TimeZoneStop();
        Play();

    }
}
