using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayingMelody : EventBase
{
    /// <summary>
    /// 開始してから時間をとる
    /// </summary>
    public float CountTime { get; private set; }

    /// <summary>
    /// １つ目の音符をタッチするタイムリミット
    /// </summary>
    private float FirstLimitTime = 5.0f;

    /// <summary>
    /// いくつ目のサウンドまで再生できるか
    /// </summary>
    private int CanPlaySoundNumber { get; set; }

    /// <summary>
    /// 現在いくつ目のサウンドを再生しているか
    /// </summary>
    private int PlayingSoundNumber = 0;

    /// <summary>
    /// 音楽データ１つあたりの時間
    /// </summary>
    public const float SoundSegmentSecond = 6.0f;

    /// <summary>
    /// 音楽データの数
    /// </summary>
    private const int MaxSoundNumber = 5;

    private const float MaxVolume = 0.1f;

    /// <summary>
    /// 音楽データをまとめて格納するリスト
    /// </summary>
    public List<AudioClip> SoundsList = null;

    // Use this for initialization
    void Start()
    {
        CanPlaySoundNumber = 0;

        CountTime = 0.0f;

        LoadSounds();

    }

    /// <summary>
    /// 音声ファイルを読み込む
    /// </summary>
    private void LoadSounds()
    {
        var Sounds = Resources.LoadAll<AudioClip>(GetComponent<MelodyList>().SoundFileName);
        foreach (var sound in Sounds)
        {
            SoundsList.Add(sound);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CountTime += Time.deltaTime;

        if (!audio.isPlaying && CanPlaySoundNumber > PlayingSoundNumber)
        {
            audio.clip = SoundsList[PlayingSoundNumber++];
            audio.volume = MaxVolume;
            audio.Play();
        }

        if (CanPlaySoundNumber == 0)
        {
            if(CountTime > FirstLimitTime) Finish();
        }
        else if (CountTime > SoundSegmentSecond * (CanPlaySoundNumber + 1))
        {
            Finish();
        }

    }

    /// <summary>
    /// 終了時の処理
    /// </summary>
    protected override void Finish()
    {
        base.Finish();
    }

    /// <summary>
    /// 再生するサウンドを増やす
    /// </summary>
    public void AddSoundNumber()
    {
        CanPlaySoundNumber++;
    }

}
