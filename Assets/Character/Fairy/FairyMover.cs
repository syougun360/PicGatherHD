/// ---------------------------------------------------
/// date ： 2015/01/12    
/// brief ： 妖精の動き処理
/// author ： Yamada Masamistu
/// ---------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FairyMover : MonoBehaviour {

    [SerializeField]
    string FeverGaugeHitResName = string.Empty;

    GameObject FeverGauge = null;
    FairyAppear Appear = null;
    FairyAnimator Anima = null;
    Vector3 FruitPos = Vector3.zero;

    public bool IsMove { get { return (State == STATE.Move); } }
    public bool IsAbsorption { get { return (State == STATE.Absorption); } }

    float Count = 0;

    float ArrivalTime = 0.0f;
    float StandbyTime = 0.0f;

    enum STATE
    {
        Stop,
        Move,
        Absorption,
    };
    STATE State = STATE.Stop;

	// Use this for initialization
	void Start () {

        FeverGauge = GameObject.Find("FeverGauge");
        Appear = GetComponent<FairyAppear>();
        Anima = GetComponent<FairyAnimator>();
        StandbyTime = Random.Range(5.0f, 10.0f);
        ArrivalTime = Random.Range(3.0f, 6.0f);

	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!ModeManager.IsGameMode) return;

        StartMove();
        Arrival();
        MoveToFerveGauge();

	}

    /// <summary>
    /// カメラの方向に向く
    /// </summary>
    void CameraForLookAt()
    {
        iTween.LookTo(gameObject, Camera.main.transform.position, ArrivalTime);
    }

    /// <summary>
    /// 移動処理
    /// 木をタップしたらそこに向かって移動していく
    /// </summary>
    void StartMove()
    {
        if (State != STATE.Stop) return;
        if (!Appear.IsStop) return;

        CameraForLookAt();

        Count += Time.deltaTime;
        if (Count >= StandbyTime)
        {
            SetMoveTo();
        }

    }

    /// <summary>
    /// 木の実の番地をランダムで設定。
    /// そこに向かって移動する。
    /// </summary>
    void SetMoveTo()
    {
        var fruits = GameObject.FindGameObjectsWithTag("Fruit");
        if (fruits.Length == 0) return;

        // AddDebugData("SetMoveTo Begin");

        var randomNum = Random.Range(0, fruits.Length);
        FruitPos = fruits[randomNum].transform.position;

        iTween.MoveTo(gameObject, iTween.Hash("position", FruitPos - new Vector3(0,0.8f,0),
                        "time", ArrivalTime, "easetype", iTween.EaseType.easeInOutExpo));

        State = STATE.Move;
        Count = 0;
        Anima.ChangeMoveAnima();

    }

    /// <summary>
    /// 到着時間に来たら状態が止まるにする
    /// </summary>
    void Arrival()
    {
        if (State != STATE.Move) return;

        iTween.LookTo(gameObject, FruitPos, ArrivalTime);

        Count += Time.deltaTime;
        if (Count >= ArrivalTime / 4)
        {
            Anima.ChangeEatAnima();
        }

        if(Count >= ArrivalTime)
        {
            Count = 0;
            State = STATE.Stop;
        }
    }

    /// <summary>
    /// 吸収状態に設定する
    /// </summary>
    public void SetStateAbsorption()
    {
        Anima.ChangeMoveAnima();
        State = STATE.Absorption;
    }

    /// <summary>
    /// フィーバーゲージに向かって移動
    /// </summary>
    void MoveToFerveGauge()
    {
        if (State != STATE.Absorption) return;

        var ferverGaugePos = FeverGauge.transform.position;
        iTween.MoveTo(gameObject, iTween.Hash("position", ferverGaugePos,
                        "time", ArrivalTime, "easetype", iTween.EaseType.easeInOutExpo));

        iTween.LookTo(gameObject, ferverGaugePos, ArrivalTime);

    }


    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == FeverGauge.name)
        {
            FerverGaugeHit();
        }
    }

    /// <summary>
    /// フィーバーゲージに当たった時の処理
    /// </summary>
    void FerverGaugeHit()
    {
        var value = gameObject.transform.lossyScale.x;
        FeverGauge.GetComponent<FeverManager>().AddScore(value);

        var manager = GameObject.FindObjectOfType(typeof(FairyManagerController)) as FairyManagerController;
        manager.LimitCreateNumIncrease();
        manager.ChildrensDataSave();

        var sePlayer = GameObject.FindObjectOfType(typeof(SoundEffectPlayer)) as SoundEffectPlayer;
        sePlayer.Play(FeverGaugeHitResName);

        Destroy(gameObject);

    }

    /// <summary>
    /// 移動アニメーションに切り替える
    /// </summary>
    public void ChangeMoveAnimation()
    {
        Anima.ChangeMoveAnima();
    }    
}
