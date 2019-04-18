using UnityEngine;
using System.Collections;

public class OneMoreFever : EventBase
{

    /// <summary>
    /// 画面を隠すインスタンスのプレハブ
    /// </summary>
    [SerializeField]
    GameObject CurtainPrefab = null;

    /// <summary>
    /// フィーバーゲージを扱う
    /// </summary>
    FeverManager FeverMngr = null;
    
    /// <summary>
    /// 生成するインスタンスの量
    /// </summary>
    private const int MaxInstantiate = 1;

    /// <summary>
    /// 生成されたクローンの数
    /// </summary>
    private int InstantiatedNumber = 0;

    /// <summary>
    /// 生成し続ける時間
    /// </summary>
    private const float SpawningTime = 5.0f;

    /// <summary>
    /// 今生成されてから何秒経っているか
    /// </summary>
    private float NowLifeTime = 0.0f;

    /// <summary>
    /// 画面右端からさらに右への移動
    /// </summary>
    const float OffsetX = 50;

    /// <summary>
    /// 生成するY座標の乱数
    /// </summary>
    const int SegmentNumber = 60;

    /// <summary>
    /// 生成するタイミングかどうか true...生成するタイミング　false...生成しない
    /// </summary>
    private bool IsCreateTiming = true;

    // Use this for initialization
    void Start()
    {
        FeverMngr = GameObject.FindObjectOfType<FeverManager>();
        UIEnabled.Unavailable();
        InstantiatedNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        NowLifeTime += Time.deltaTime;

        CheckBeginTiming();

        CheckCreateCurtains();


    }

    /// <summary>
    /// イベントが発生してからの時間を見てFeverゲージを増加する。
    /// </summary>
    private void CheckBeginTiming()
    {

        var BeginAddScoreTime = 1.0f;
        if (NowLifeTime > BeginAddScoreTime)
        {
            FeverMngr.AddScore(FeverMngr.MaxFeverScore);
        }
    }

    /// <summary>
    /// カーテンを生成する
    /// 条件は内部に内包している
    /// </summary>
    private void CheckCreateCurtains()
    {
        if (NowLifeTime < SpawningTime)
        {
            if (IsCreateTiming)
            {
                CreateCurtain();
                IsCreateTiming = false;
            }
            else
            {
                IsCreateTiming = true;
            }
        }
        else
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
        UIEnabled.Unavailable();
    }

    private void CreateCurtain()
    {
        for (int i = 0; i < MaxInstantiate; i++)
        {
            Instantiate(CurtainPrefab,
                        Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + OffsetX, Random.Range(0, SegmentNumber) * Screen.height / SegmentNumber, 1.0f + (++InstantiatedNumber * 0.001f))),
                        Quaternion.identity);

        }
    }

}
