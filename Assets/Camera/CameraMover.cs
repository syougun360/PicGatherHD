using UnityEngine;
using System.Collections;

public class CameraMover : MonoBehaviour
{

    /// <summary>
    /// カメラをY軸回転させる中心となるGameObject
    /// </summary>
    [SerializeField]
    private Transform CenterObject = null;

    [SerializeField]
    private float IncreaseRadiusValue = 0.01f;

    [SerializeField]
    private float UpVelocity = 1.0f;

    /// <summary>
    /// カメラの円運動量
    /// </summary>
    private float RotationAngle = 0.0f;

    /// <summary>
    /// カメラの円運動するときの中心からの距離
    /// </summary>
    public float MoveRadius { get; private set; }

    /// <summary>
    /// 半径を増加する
    /// </summary>
    private float IncreaseRadius = 0;
    
    /// <summary>
    /// カメラを動かすかどうか
    /// </summary>
    private bool CanMoveCamera = false;

    // Use this for initialization
    void Start()
    {
        MoveRadius = IncreaseRadius = transform.position.z;

        RotateCamera();
        
    }

    /// <summary>
    /// 半径を広げていく
    /// </summary>
    /// <param name="addRadius"></param>
    public void BroadenMoveRadius(float addRadius)
    {
        IncreaseRadius -= addRadius;
    }

    /// <summary>
    /// 半径を増加する
    /// </summary>
    void IncreaseRadiusControl()
    {
        if (MoveRadius <= IncreaseRadius) return;

        MoveRadius -= IncreaseRadiusValue;
        transform.Translate(Vector3.up * UpVelocity * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (ModeManager.IsDrawingMode) return;

        TurnCamera();

        RotateCamera();

        IncreaseRadiusControl();
    }

    /// <summary>
    /// 何にもタッチしていない（＝背景にRayが当たっている）ならカメラを回転させる。
    /// </summary>
    private void TurnCamera()
    {

        /// if分を使ってタッチ時にマウスカーソルが移動したと検知しないように分断する。
        if (Input.touchCount == 1)
        {
            TurnByTouch();
        }
        else if (Input.GetMouseButton(0))
        {
            TurnByMouse();
        }

        StopTurningCamera();
    }

    /// <summary>
    ///タッチもマウス入力もされていなかったらカメラを回せないようにする。
    /// </summary>
    private void StopTurningCamera()
    {
        if (Input.touchCount == 0 && !Input.GetMouseButton(0))
        {
            CanMoveCamera = false;
        }
    }



    /// <summary>
    /// マウスによるカメラの移動
    /// </summary>
    private void TurnByMouse()
    {
        if (Input.GetMouseButtonDown(0) && IsTouchingNothing())
        {
            CanMoveCamera = true;
        }

        if (CanMoveCamera)
        {
            const float increaseValue = 3.0f;
            AddAngle(increaseValue * Input.GetAxis("Mouse X"));

            RotateCamera();
        }

    }


    /// <summary>
    /// タッチによるカメラの移動
    /// </summary>
    private void TurnByTouch()
    {
        if (IsTouchingNothing() && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            CanMoveCamera = true;
        }

        if (CanMoveCamera && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            AddAngle(Input.GetTouch(0).deltaPosition.x);
        }

    }

    /// <summary>
    /// RotationAngleに応じた角度にカメラを回転させる
    /// </summary>
    private void RotateCamera()
    {
        transform.position = new Vector3(
            CenterObject.position.x + Mathf.Sin(RotationAngle) * MoveRadius,
            transform.position.y, 
            CenterObject.position.x + Mathf.Cos(RotationAngle) * MoveRadius);

        transform.LookAt(new Vector3(0,transform.position.y - 2,0));
    }

    /// <summary>
    /// カメラを回転させる角度を加算する
    /// * 画面上の移動量で回転させるため、角度で渡しているわけではない
    /// </summary>
    /// <param name="addValue">加える量</param>
    private void AddAngle(float addValue)
    {
        RotationAngle += 0.01f * addValue;

        if (RotationAngle > 2 * Mathf.PI) RotationAngle -= 2 * Mathf.PI;
        if (RotationAngle < 2 * Mathf.PI) RotationAngle += 2 * Mathf.PI;
    }


    /// <summary>
    /// 何とも当たっていないかどうかを判断する
    /// </summary>
    /// <returns>当たっている...true 当たっている...false</returns>
    private bool IsTouchingNothing()
    {
        var objects = GameObject.FindObjectsOfType<GameObject>();

        foreach (var obj in objects)
        {
            if (TouchManager.IsMouseButton(obj)) return false;
            if (TouchManager.IsTouching(obj)) return false;
        }

        if (IsBabbleInside(Input.mousePosition)) return false;

        foreach (var touch in Input.touches)
        {
            if (IsBabbleInside(touch.position)) return false;
        }

        return true;
    }

    /// <summary>
    /// 引数の座標がシャボン玉内にあるかどうかを返す
    /// </summary>
    /// <param name="point">調べる座標</param>
    /// <returns>内部にある...true 外にある...false</returns>
    private bool IsBabbleInside(Vector2 point)
    {
        var babbles = GameObject.FindGameObjectsWithTag("Babble");

        foreach (var babble in babbles)
        {
            var PositionInScreen = new Vector3(babble.transform.position.x, babble.transform.position.y);
            PositionInScreen = Camera.main.WorldToScreenPoint(PositionInScreen);
            var DistanceX = PositionInScreen.x - point.x;
            var DistanceY = PositionInScreen.y - point.y;

            var DistanceXY = Mathf.Sqrt(DistanceX * DistanceX + DistanceY * DistanceY);

            if (DistanceXY < BabbleMover.BabbleRadius) return true;

        }

        return false;
    }
}
