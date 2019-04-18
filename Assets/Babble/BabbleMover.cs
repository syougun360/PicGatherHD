using UnityEngine;
using System.Collections;

public class BabbleMover : MonoBehaviour {

    /// <summary>
    /// 移動量の最大値、最小値
    /// </summary>
    private const float MaxMoveValue = 5.0f;
    private const float MinMoveValue = 0.0f;

    /// <summary>
    /// 移動速度をx,yそれぞれの速度
    /// </summary>
    private Vector2 Velocity = new Vector2(MinMoveValue, MinMoveValue);

    /// <summary>
    /// スクリーン座標を保存するための変数
    /// </summary>
    private Vector3 PositionInScreen = new Vector3(0.0f,0.0f,0.0f);

    /// <summary>
    /// スクリーン上でのシャボン玉の半径
    /// Screen.width = 1280,Screen.height = 720
    /// </summary>
    public const float BabbleRadius = 70.0f;

    // Use this for initialization
    void Start()
    {
        InitializePosition();

        /// キャンパス等へ飛ぶUIとの干渉を避ける
        PositionInScreen.z = 1.3f;

    }
    
    /// <summary>
    /// 移動前の座標を覚える変数
    /// </summary>
    private Vector2 LastPositionInScreen = new Vector2(0.0f, 0.0f);

    // Update is called once per frame
    void Update()
    {
        LastPositionInScreen = PositionInScreen;

        TouchMover();

        ///速度調整する
        DecreaseMoveSpeed();

        ///スクリーン座標上での位置で確認し、画面端なら跳ね返る
        Velocity.x *= ChangeVelocity(ref PositionInScreen.x, Screen.width);
        Velocity.y *= ChangeVelocity(ref PositionInScreen.y, Screen.height);


        transform.position = Camera.main.ScreenToWorldPoint(PositionInScreen);


    }

    /// <summary>
    /// 自動で速度減少させる
    /// </summary>
    /// <param name="index">減少させる変数</param>
    private void DecreaseMoveSpeed()
    {
        /// 移動速度の減少すると判断する閾値（いきち）
        const float ThresholdMoveSpeed = 3.0f;
        /// 減少量
        const float DecreaseValue = 0.95f;

        ///x,yの移動量の合計で減速する判断をする
        float AbsoluteMoveSpeed = Mathf.Sqrt((Velocity.x * Velocity.x) + (Velocity.y * Velocity.y));
        if (AbsoluteMoveSpeed > ThresholdMoveSpeed)
        {
            Velocity *= DecreaseValue;
        }
    }

    /// <summary>
    /// もし画面端ならばMoveSpeedの符号を変える
    /// </summary>
    /// <param name="position">超えているかどうかを見る座標</param>
    /// <param name="maxSize">座標の最大値</param>
    /// <returns>もし超えていれば負の値,超えていなければ正の値を返す。</returns>
    private int ChangeVelocity(ref float position, int maxSize)
    {
        var BabbleRadius = transform.localScale.x * maxSize * 0.3f;

        if (position < 0.0f + BabbleRadius)
        {
            ///一度押し戻す
            position = 0.0f + BabbleRadius + 1;

            return -1;
        }

        if (position > maxSize - BabbleRadius)
        {
            ///一度押し戻す
            position = maxSize - BabbleRadius - 1;

            return -1;
        }

        return 1;
    }

    /// <summary>
    /// タッチとドラッグで移動するようにする関数
    /// </summary>
    private void TouchMover()
    {

        if (IsTouchig() && Input.GetMouseButton(0))
        {
            SetPosition(Input.mousePosition);
            SetVelocity();

            return;
        }
        else
        {
            AddPosition(Velocity);
        }

        if (IsTouchig() && Input.touchCount > 0 )
        {
            SetPosition(Input.GetTouch(0).position);
            SetVelocity();
            
            return;
        }
        else
        {
            AddPosition(Velocity);
        }
       

    }

    /// <summary>
    /// スクリーン上の座標でPositionInScreenを設定する
    /// </summary>
    /// <param name="velocity">設定するスクリーン座標</param>
    private void SetPosition(Vector2 velocity)
    {
        PositionInScreen.x = velocity.x;
        PositionInScreen.y = velocity.y;
    }

    /// <summary>
    /// 前フレームとの位置の差を得てVelocityを設定する
    /// </summary>
    private void SetVelocity()
    {
        Velocity.x = PositionInScreen.x - LastPositionInScreen.x;
        Velocity.y = PositionInScreen.y - LastPositionInScreen.y;
    }

    /// <summary>
    /// PositionInScreenの値に加算する
    /// </summary>
    /// <param name="velocity">加算する値</param>
    private void AddPosition(Vector2 velocity)
    {
        PositionInScreen.x += velocity.x;
        PositionInScreen.y += velocity.y;
    }

    /// <summary>
    /// シャボン玉をクリックもしくはタッチされているかどうかを判定する
    /// </summary>
    /// <returns>触っている...true 触っていない...false</returns>
    private bool IsTouchig()
    {

        if (IsHitBabble(Input.mousePosition.x, Input.mousePosition.y)) return true;
                
        var touches = Input.touches;

        foreach(var touche in touches)
        {
            if (IsHitBabble(touche.position.x, touche.position.y)) return true;
        }

        return false;
    }


    /// <summary>
    /// 引数の座標がシャボン玉の半径内かどうかを確かめる
    /// </summary>
    /// <param name="x">ｘ座標</param>
    /// <param name="y">ｙ座標</param>
    /// <returns>シャボン玉の半径の中...true 外...false</returns>
    private bool IsHitBabble(float x, float y)
    {        
        var DistanceX = PositionInScreen.x - x;
        var DistanceY = PositionInScreen.y - y;

        var DistanceXY = Mathf.Sqrt(DistanceX * DistanceX + DistanceY * DistanceY);

        if (DistanceXY < BabbleRadius) return true;

        return false;
    }

    /// <summary>
    /// シャボン玉の生成位置を設定する
    /// </summary>
    private void InitializePosition()
    {
        PositionInScreen.x = Screen.width;
        PositionInScreen.y = 0;
    }
}
