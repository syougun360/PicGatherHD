/// ---------------------------------------------------
/// date ： 2015/01/12 
/// brief ： 葉っぱ処理
/// author ： Yamada Masamistu
/// ---------------------------------------------------

using UnityEngine;
using System.Collections;

public class LeafWitherController : MonoBehaviour
{

    public bool IsDead { get { return (State == STATE.Dead); } }

    [SerializeField]
    Color DeadLeafColor = Color.white;

    [SerializeField]
    const float WitherTime = 10.0f;

    LeafStampManagerController Manager = null;

    FruitManagerController FruitManager = null;

    Vector3 SwayVelocity = new Vector3(0, -3, 0);

    float LifeTime = 0;

    enum STATE
    {
        None,
        Live,
        Wither,
        Dead,
    };

    STATE State = STATE.None;

	// Use this for initialization
	void Start () {
        State = STATE.Live;
	}
	
	// Update is called once per frame
	void Update () 
    {
        WitheringTime();
        Fall();
	}

    /// <summary>
    /// 枯れる状態にする。
    /// </summary>
    void StartWither()
    {
        if (State != STATE.Live) return;

        State = STATE.Wither;
        Decolorization();
    }

    /// <summary>
    /// 枯葉になっていく時間
    /// </summary>
    void WitheringTime()
    {
        if (State != STATE.Wither) return;

        LifeTime += Time.deltaTime;
        if (LifeTime >= WitherTime)
        {
            State = STATE.Dead;
            FruitManager = GameObject.FindObjectOfType(typeof(FruitManagerController)) as FruitManagerController;
            FruitManager.ChildrenCreate(transform.position);

            Manager = GameObject.FindObjectOfType(typeof(LeafStampManagerController)) as LeafStampManagerController;
            Manager.ChildrensDataSave();
        }
    }

    /// <summary>
    /// 脱色処理
    /// </summary>
    void Decolorization()
    {
        iTween.ColorTo(gameObject, DeadLeafColor, WitherTime - 2);
    }

    /// <summary>
    /// 枯れ落ちる
    /// 画面から消えたら削除するようにしてあります。
    /// </summary>
    void Fall()
    {
        if (State != STATE.Dead) return;

        transform.Translate(SwayVelocity * Time.deltaTime);

        var screenPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        if (screenPos.y <= -100)
        {
            Manager.ChildrensDataSave();
            Destroy(gameObject);
        }
    }


    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Rain")
        {
            StartWither();
        }

    }

}
