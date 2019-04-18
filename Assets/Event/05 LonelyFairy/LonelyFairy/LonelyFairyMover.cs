using UnityEngine;
using System.Collections;

public class LonelyFairyMover : MonoBehaviour {

    /// <summary>
    /// 空に帰るときに発生するパーティクル
    /// </summary>
    [SerializeField]
    private ParticleSystem ParticlePrefab = null;

    private Animator Anima = null;

    /// <summary>
    /// 距離を測定する
    /// </summary>
    private float Distance = 0.0f;

    /// <summary>
    /// 妖精を移動させる際の目標
    /// </summary>
    private Vector3 TargetPosition = new Vector3(0.0f, 0.0f, 0.0f);

    /// <summary>
    /// 妖精を誘導する角度
    /// </summary>
    private float ShakeAngle = 0.0f;

    private float AnimaDuration = 0;
    private float GreetingCount = 0;
    private float UpPos = 0;

    /// <summary>
    /// 妖精の行動パターン
    /// </summary>
    public enum MoveMode
    {
        ToCamera,
        Animation,
        ToSky,
        OutOfScreen
    }

    /// <summary>
    /// 今の妖精の動いている状態を得る
    /// </summary>
    public MoveMode NowMoveMode{ get; private set;}

	// Use this for initialization
	void Start () {
        NowMoveMode = MoveMode.ToCamera;
        Anima = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        switch(NowMoveMode)
        {
            case MoveMode.ToCamera :
                MoveToCamera();
                break;
            case MoveMode.Animation:
                Greeting();
                break;
            case MoveMode.ToSky:
                MoveToSky();
                break;
        }
	}

    /// <summary>
    /// 挨拶状態の行動
    /// </summary>
    private void Greeting()
    {
        GreetingCount += Time.deltaTime;
        if (GreetingCount >= AnimaDuration)
        {
            Anima.SetTrigger("OnMoveTrigger");
            ChangeToSky();
            GreetingCount = 0;
            UpPos = transform.position.y;
        }
    }

    /// <summary>
    /// カメラに移動しているときに妖精を左右に振る
    /// </summary>
    private void TargetShake()
    {
        var CanShakeDistance = 1.1f;
        var posY = Screen.height / 2 - transform.lossyScale.y * 100;
        if (Distance > CanShakeDistance)
        {
            ShakeAngle += 0.1f;
            TargetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2 + Distance * (Mathf.Sin(ShakeAngle) * Screen.width / 3), posY, 1.0f));
        }
        else
        {
            TargetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, posY, 1.0f));
        }
    }


    /// <summary>
    /// カメラに向かって移動する
    /// </summary>
    private void MoveToCamera()
    {
        Distance = Vector3.Distance(TargetPosition, transform.position);

        TargetShake();

        var CanMoveDistance = 0.1f;

        var MoveVelocity = (TargetPosition - transform.position).normalized;

        var Soeed = 1.0f;

        transform.position += MoveVelocity * Distance / 2 * Soeed * Time.deltaTime;

        if (Distance < CanMoveDistance)
        {
            NowMoveMode = MoveMode.Animation;
            Anima.SetTrigger("OnGreetingTrigger");
            var currentState = Anima.GetCurrentAnimatorStateInfo(0);
            AnimaDuration = currentState.length + 5;
        }

    }

    /// <summary>
    /// 行動パターンを空に向かうようにする
    /// </summary>
    private void ChangeToSky()
    {
        NowMoveMode = MoveMode.ToSky;

        TargetPosition = new Vector3(0.0f, transform.position.y, 0.0f);
        
        ShakeAngle = Mathf.Atan2(transform.position.z - TargetPosition.z,
              transform.position.x - TargetPosition.x);
        
        Distance = Vector3.Distance(TargetPosition, transform.position);

        ParticleInstantiate();

    }

    /// <summary>
    /// 空に移動する
    /// </summary>
    private void MoveToSky()
    {
        transform.position = new Vector3(Distance * Mathf.Cos(ShakeAngle),
                                            UpPos, Distance * Mathf.Sin(ShakeAngle));

        ShakeAngle += 1.5f * Time.deltaTime;
        UpPos += 0.5f * Time.deltaTime;
        Distance *= 0.99f;

        if (Camera.main.WorldToScreenPoint(transform.position).y > Screen.height * 1.3f)
        {
            NowMoveMode = MoveMode.OutOfScreen;
        }
    }

    /// <summary>
    /// パーティクルを生成する
    /// </summary>
    private void ParticleInstantiate()
    {
        var clone = (ParticleSystem)Instantiate(ParticlePrefab, transform.position, Quaternion.identity);

        clone.transform.parent = transform;
        clone.Play();
    }

}
