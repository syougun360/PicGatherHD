using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GestureMover : MonoBehaviour {

    /// <summary>
    /// ジェスチャーの1ループにかかる時間
    /// </summary>
    [SerializeField]
    private float LoopTime = 2.5f;

    /// <summary>
    /// ジェスチャー開始からの時間を計るの変数
    /// </summary>
    private float NowAnimetionTime = 0.0f;

    /// <summary>
    /// ジェスチャーのスクリーン座標
    /// </summary>
    private Vector3 PositionInScreen = Vector3.zero;

    /// <summary>
    /// ジェスチャーの開始位置
    /// </summary>
    private Vector3 StartScreenPosition = Vector3.zero;

    /// <summary>
    /// ジェスチャーの移動量
    /// </summary>
    private Vector3 Velocity = Vector3.zero;

    /// <summary>
    /// このGameObjectの持つImage
    /// </summary>
    private Image ThisImage = null;

	// Use this for initialization
	void Start () {
        ThisImage = GetComponent<Image>();
        StartScreenPosition = PositionInScreen = Camera.main.WorldToScreenPoint(transform.position);
        InitializeVelocity();

    }
	
	// Update is called once per frame
	void Update () {

        if (ThisImage.enabled)
        {
            Move();
        }
        else
        {
            InitializeIcons();

        }
	}

    /// <summary>
    /// 移動量を初期値にする
    /// </summary>
    private void InitializeVelocity()
    {
        Velocity.x = Screen.width / 5 * 2;
        Velocity.y = Screen.height / LoopTime * 2;
    }

    /// <summary>
    /// アイコンの位置、移動量、時間の初期化
    /// </summary>
    private void InitializeIcons()
    {
        NowAnimetionTime = 0.0f;
        PositionInScreen = StartScreenPosition;
        InitializeVelocity();
    }

    /// <summary>
    /// アイコンの移動
    /// </summary>
    private void Move()
    {
        if (NowAnimetionTime >= LoopTime)
        {
            InitializeIcons();
        }

        NowAnimetionTime += Time.deltaTime;

        PositionInScreen += Velocity * Time.deltaTime;

        Bounding();

        transform.position = Camera.main.ScreenToWorldPoint(PositionInScreen);
    }


    /// <summary>
    /// 範囲内で動くように調節する
    /// </summary>
    private void Bounding()
    {
        var TopLimit = Screen.height * 3 / 5;
        var BottomLimit = Screen.height * 1 / 5;
        if (PositionInScreen.y > TopLimit)
        {
            PositionInScreen.y = TopLimit;
            Velocity.x *= -0.5f;
            Velocity.y *= -1;
        }

        if (PositionInScreen.y < BottomLimit)
        {
            PositionInScreen.y = BottomLimit;
            Velocity.x *= -2.0f;
            Velocity.y *= -1;
        }

    }

}
