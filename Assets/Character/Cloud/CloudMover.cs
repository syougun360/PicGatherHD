/// ---------------------------------------------------
/// date ： 2015/01/12  
/// brief ： 雲オブジェクトの移動処理 
/// author ： Yamada Masamistu
/// ---------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Text;
using System.Collections.Generic;

public class CloudMover : MonoBehaviour {

    GameObject TreeObject = null;

    Vector3 CreateRainPos = Vector3.zero;
    Vector3 TreePos = Vector3.zero;
    Vector3 RotationPos = Vector3.zero;

    float Radius = 0.0f;
    float RotationAngle = 0.0f;
    float Count = 0;
    float StartCreateRainTime = 0;
    float RadiusMoveSpeed = 0;
    float RotationSpeed = 0;
    float AppearanceSpeed = 0;
    float StopAppearancePosY = 0;
    const float ArrivalTime = 5.0f;

    enum STATE
    {
        Appearance,
        Normal,
        TreeTop,
        CreateRain,
        ReturnNormal,
    };

    STATE State = STATE.Appearance;

    RainCreator RainCreate = null;

    public bool IsReturnlMove { get { return (State == STATE.ReturnNormal); } }
    public bool IsRain { get { return (State == STATE.CreateRain); } }

    // Use this for initialization
	void Start () {

        var wroldPos = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height+ Random.Range(100.0f,200.0f), -Camera.main.transform.position.z));
        RotationPos.y = wroldPos.y;
        StartCreateRainTime = Random.Range(2.0f, 4.0f);
        RadiusMoveSpeed = Random.Range(0.5f, 0.7f);
        RotationSpeed = Random.Range(0.5f, 0.7f);
        StopAppearancePosY = Random.Range(Screen.height / 2, Screen.height / 2 + 200);
        AppearanceSpeed = Random.Range(1, 3);
        RainCreate = GetComponent<RainCreator>();
        TreeObject = GameObject.Find("TreeManager");

	}
	
	void Update () 
    {
        if (ModeManager.IsDrawingMode) return;

        CircleRotation();
        AppearanceMove();
        NormalMove();
        StartTreeTopMove();
        TreeTopMove();
        CreateRainMove();
        ReturnNormalPosition();

    }

    /// <summary>
    /// 円運動移動
    /// </summary>
    void CircleRotation()
    {

        TreePos = TreeObject.transform.position;
        RotationPos.x = TreePos.x + Mathf.Cos(RotationAngle) * Radius;
        RotationPos.z = TreePos.z + Mathf.Sin(RotationAngle) * Radius;

        RotationAngle += RotationSpeed * Time.deltaTime;
    }


    /// <summary>
    /// 登場移動
    /// </summary>
    void AppearanceMove()
    {
        if (State != STATE.Appearance) return;

        transform.position = RotationPos;
        RotationPos.y -= Time.deltaTime * AppearanceSpeed;

        var screenPos = Camera.main.WorldToScreenPoint(transform.position);

        RotationControl(screenPos.x);

        if (screenPos.y <= StopAppearancePosY)
        {
            State = STATE.Normal;
        }

    }


    /// <summary>
    /// 木の上に向けて移動中の処理
    /// </summary>
    void TreeTopMove()
    {
        if (State != STATE.TreeTop) return;

        CreateRainPos = RotationPos;
        transform.position = CreateRainPos;
        RotationPos.y += Time.deltaTime;

        var screenPos = Camera.main.WorldToScreenPoint(transform.position);

        if (screenPos.y >= Screen.height - 100)
        {
            State = STATE.CreateRain;
            RainCreate.StartCreate();
        }

    }

    /// <summary>
    /// 雨を生成しているときの移動処理
    /// </summary>
    void CreateRainMove()
    {
        if (State != STATE.CreateRain) return;

        transform.position = new Vector3(RotationPos.x, CreateRainPos.y, RotationPos.z);

        Radius -= RadiusMoveSpeed * Time.deltaTime;
        
        if (Radius <= 0)
        {
            RainCreate.StopCreate();
            Radius = 0;
            RotationAngle = 0;
            State = STATE.ReturnNormal;
        }

    }

    /// <summary>
    /// 通常状態に戻る
    /// </summary>
    void ReturnNormalPosition()
    {
        if (State != STATE.ReturnNormal) return;

        var NormalPos = RotationPos;
        transform.position = NormalPos;
        RotationPos.y -= Time.deltaTime * AppearanceSpeed;

        var screenPos = Camera.main.WorldToScreenPoint(transform.position);

        RotationControl(screenPos.x);

        if (screenPos.y <= 200)
        {
            State = STATE.Normal;
        }

    }


    /// <summary>
    /// 通常の移動
    /// </summary>
    void NormalMove()
    {
        if (State != STATE.Normal) return;

        transform.position = RotationPos;

    }

    /// <summary>
    /// 木の上に移動する
    /// </summary>
    void StartTreeTopMove()
    {
        if (State != STATE.Normal) return;

        Count += Time.deltaTime;
        if (Count <= StartCreateRainTime) return;

        Count = 0;
        
        if (!IsCheckLeafLive()) return;

        State = STATE.TreeTop;

    }

    /// <summary>
    /// 葉っぱが存在するかどうか
    /// </summary>
    /// <returns></returns>
    bool IsCheckLeafLive()
    {
        if (!ModeManager.IsGameMode) return false;

        var Leafs = GameObject.FindGameObjectsWithTag("Leaf");
        if (Leafs.Length == 0)
        {
            return false;

        }

        return true;

    }

    /// <summary>
    /// 半径を制御
    /// </summary>
    /// <param name="speed"></param>
    void RotationControl(float screenPosX)
    {
        Radius += Time.deltaTime * AppearanceSpeed;

        if (screenPosX <= 150 || screenPosX >= Screen.width - 150)
        {
            State = STATE.Normal;
        }

    }
}
