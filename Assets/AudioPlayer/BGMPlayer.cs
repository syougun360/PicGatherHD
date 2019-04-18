/// ---------------------------------------------------
/// date ： 2015/02/22  
/// brief ： BGMの再生機
/// author ： Yamada Masamistu
/// ---------------------------------------------------
/// 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct FadeTimeData
{
    public FadeTimeData(float inTime, float outTime)
    {
        InTime = inTime;
        OutTime = outTime;
    }

    public static FadeTimeData Zero { get { return new FadeTimeData(0, 0); } }

    public float InTime;
    public float OutTime;
}

public class BGMPlayer : MonoBehaviour
{
    public class Data
    {
        public Data(string resName)
        {
            ResName = resName;
            Clip = Resources.Load("BGM/" + resName) as AudioClip;
        }
        public string ResName { get; set; }
        public AudioClip Clip { get; set; }
    }

    const float MinVolume = 0;
    const float MaxVolume = 0.1f;
    const float StartFadeInVolume = 0.005f;

    AudioSource Source = null;

    Dictionary<string, Data> AudioMap = new Dictionary<string, Data>();
    FadeTimeData FadeTime;

    public bool IsPlaying { get { return Source.isPlaying; } }

    void Awake()
    {
        Source = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 再生
    /// </summary>
    /// <param name="resName">Resources/Audios/の中にあるオーディオ名</param>
    /// <param name="fadeInTime">フェードイン時間</param>
    public void Play(string resName, FadeTimeData fadeTime)
    {
        if (!AudioMap.ContainsKey(resName))
        {
            AudioMap.Add(resName, new Data(resName));
        }

        FadeTime = fadeTime;
        Source.clip = AudioMap[resName].Clip;
        Source.Play();
        Source.volume = StartFadeInVolume;

        StartFadeIn(FadeTime.InTime);
    }

    /// <summary>
    /// 停止
    /// </summary>
    /// <param name="fadeOutTime">フェードアウト時間</param>
    public void Stop()
    {
        StartFadeOut(FadeTime.OutTime);
    }

    /// <summary>
    /// フェードアウトスタート
    /// </summary>
    /// <param name="time">時間</param>
    void StartFadeOut(float time)
    {
        iTween.ValueTo(gameObject, iTween.Hash("from", MaxVolume, "to", MinVolume, "time", time, "onupdate", "UpdateHandler"));
    }

    /// <summary>
    /// フェードインスタート
    /// </summary>
    /// <param name="time">時間</param>
    void StartFadeIn(float time)
    {

        iTween.ValueTo(gameObject, iTween.Hash("from", StartFadeInVolume, "to", MaxVolume, "time", time, "onupdate", "UpdateHandler"));
    }

    void UpdateHandler(float value)
    {
        Source.volume = value;

        if (Source.volume <= 0)
        {
            Source.Stop();
        }
    }
}
