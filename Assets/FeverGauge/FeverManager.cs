using UnityEngine;
using System.Collections;
using System.Text;
using System.Collections.Generic;

public class FeverManager : MonoBehaviour {

    FeverSoundController Sound = null;

    [SerializeField]
    AllDataSave AllSave = null;

    [SerializeField]
    float FeverTime = 30.0f;

    [SerializeField]
    float AddSpeed = 2.0f;

    [SerializeField]
    FeverGaugeParticle Particle = null;

    [SerializeField]
    TreeChanger TreeChange = null;

    float Count = 0;

    /// <summary>
    /// Feverゲージの上限、下限
    /// </summary>
    public float MaxFeverScore {get;private set;}
    public const float MinFeverScore = 0;

    /// <summary>
    /// Feverゲージの量
    /// </summary>
    public float FeverScore { get; private set; }
    
    /// <summary>
    /// 回数
    /// </summary>
    public int NumTimes { get; private set; }

    float IncreaseScore = 0;
    bool IsIncrease = false;
    FeverDataController Data = null;

	// Use this for initialization
	void Start () {
        MaxFeverScore = 5;

        FeverScore = MinFeverScore;
        Sound = GetComponent<FeverSoundController>();
        Data = GetComponent<FeverDataController>();

        if (Data.GetLoadData().Times < 0) return;

        NumTimes = Data.GetLoadData().Times;
        MaxFeverScore = Data.GetLoadData().MaxScore;
        IncreaseScore = Data.GetLoadData().NowScore;
	}
	
	// Update is called once per frame
	void Update () {

        if (ModeManager.IsResetMode) return;

        Increase();
        LimitCheck();

        if (ModeManager.IsFerverMode)
        {
            IsIncrease = false;
            Particle.Stop();
            Sound.Play();
        }
    }

    /// <summary>
    /// フィーバーゲージが増加する
    /// </summary>
    void Increase()
    {
        if (IsIncrease)
        {
            FeverScore += AddSpeed * Time.deltaTime;
            if (FeverScore >= IncreaseScore)
            {
                Particle.Stop();
                IsIncrease = false;
                AllSave.AllSave();
            }

        }

    }

    /// <summary>
    /// 引数に与えた整の値だけ加算される
    /// </summary>
    /// <param name="addValue"></param>
    public void AddScore(float addValue)
    {
        IncreaseScore += addValue;
        IsIncrease = true;
        Particle.Play();

    }
    
    /// <summary>
    /// もし上限を超えていたり、下限を下回っていたら直す
    /// </summary>
    void LimitCheck()
    {
        if (FeverScore > MaxFeverScore && ModeManager.IsGameMode)
        {
            NumTimes++;
            MaxFeverScore *= 2;
            FeverScore = MaxFeverScore;

            Data.Write(new FeverData(NumTimes, 0, MaxFeverScore));
            TreeChange.NextChange();

            IncreaseScore = 0;
            Sound.Stop();
            ModeManager.ChangeFerverMode();
            UIEnabled.Unavailable();
            Ferver();

        }

        if (ModeManager.IsFerverMode)
        {
            Count += Time.deltaTime;
            if (FeverScore <= MinFeverScore || Count >= FeverTime)
            {
                iTween.Stop(gameObject);

                Count = 0;
                FeverScore = MinFeverScore;
                ModeManager.ChangeGameMode();
                Sound.Stop();
                UIEnabled.Enabled();
            }

        }
    }

    void Ferver()
    {
        iTween.ValueTo(gameObject, iTween.Hash("from", MaxFeverScore, "to", MinFeverScore, "time", FeverTime, "onupdate", "UpdateHandler"));
    }

    void UpdateHandler(float value)
    {
        FeverScore = value;
    }

    public void Save()
    {
        Data.Write(new FeverData(NumTimes, FeverScore,MaxFeverScore));
    }
}
