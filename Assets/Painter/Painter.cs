using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//　マウスでペンのように描ける機能・線描画
//
//　FIXME:マウスをゆっくり動かすと線が途切れ、途切れになる。
//
public class Painter : MonoBehaviour {

    //　線描画に必要な情報
    public Touch touch { get; private set; }
    public Vector3 inputPos { get; private set; }
    public bool isDraw { get; private set; }

    Vector3 oldInputPos = Vector3.zero;
    LineRenderer line = null;
    int lineCount = 0;
    int touchID = 0;

    //　一筆制御に必要な情報
    PaintManager lineManager = null;

    Vector2 campusOffSet;
    Vector2 campusSize;
    readonly Vector2 valueAdjustment = new Vector2(3, 8);

    const float zIndex = 1.0f;


    void Start()
    {
        //　線に必要な情報を取得

        isDraw = true;
        touch = new Touch();
        inputPos = Vector3.zero;

        lineManager = FindObjectOfType(typeof(PaintManager)) as PaintManager;
        line = GetComponent<LineRenderer>();
        var component = gameObject.GetComponent<LineRenderer>();
        var color = lineManager.lineColor;
        var offset = lineManager.lineCount;
        var width = lineManager.lineWidth;

        //　線の情報を設定
        component.renderer.material.color = color;
        gameObject.renderer.sortingOrder = offset;
        line.SetWidth(width, width);
        line.renderer.sortingLayerName = "Line";
        line.renderer.sortingOrder = offset;

        campusSize = lineManager.campusSize;
        campusOffSet = lineManager.campusOffSet;

        if (Input.touchCount > 0)
        {
            touchID = Input.touchCount;
            touch = Input.GetTouch(touchID - 1);
            oldInputPos = touch.position;
        }
        else
        {
            oldInputPos = Input.mousePosition;
        }
    }

    void Update()
    {
        if (!isDraw) return;

        if (touchID == 0)
        {
            LineDrawing(Input.mousePosition);

            lineManager.CheckMouse(this);

        }
        else if (Input.touchCount >= touchID)
        {            
            touch = Input.GetTouch(touchID - 1);

            lineManager.CheckTouch(this);

            LineDrawing(touch.position);

        }
    }

    /// <summary>
    /// 線を描画
    /// </summary>
    void LineDrawing(Vector3 inputPos)
    {
        if (!isDraw) return;

        SetInputPosition(inputPos);

        LimitInputPos();

        //　ワールド座標に変換
        var oldScreenPos = Camera.main.ScreenToWorldPoint(oldInputPos);
        var nowScreenPos = Camera.main.ScreenToWorldPoint(this.inputPos);

        SetLineRendererInfo(nowScreenPos, oldScreenPos);

        oldInputPos = inputPos;

    }

    /// <summary>
    /// 入力座標を設定
    /// </summary>
    /// <param name="pos"></param>
    void SetInputPosition(Vector3 pos)
    {
        inputPos = new Vector3(pos.x,pos.y, 1.0f);
        oldInputPos.z = 1.0f;
    }

    /// <summary>
    /// LineRendererの情報を設定
    /// </summary>
    void SetLineRendererInfo(Vector3 pos, Vector3 oldPos)
    {
        lineCount++;
        
        //　線の頂点情報を設定
        line.SetVertexCount(lineCount + 1);

        line.SetPosition(lineCount - 1, oldPos);
        line.SetPosition(lineCount, pos);

    }

    /// <summary>
    /// 範囲外に出たら範囲外の位置にポジションをセットする
    /// </summary>
    void LimitInputPos()
    {
        if (inputPos.x < 0 + campusOffSet.x)
        {
            StopDrawing(0 + campusOffSet.x, inputPos.y);
        }
        if (campusSize.x - valueAdjustment.x < inputPos.x)
        {
            StopDrawing(campusSize.x - valueAdjustment.x, inputPos.y);
        }
        if (inputPos.y < 0 + campusOffSet.y)
        {
            StopDrawing(inputPos.x, 0 + campusOffSet.y);
        }
        if (campusSize.y - valueAdjustment.y < inputPos.y)
        {
            StopDrawing(inputPos.x, campusSize.y - valueAdjustment.y);
        }

    }

    //　描画終了の制御
    void StopDrawing(float x,float y)
    {
        inputPos = new Vector3(x, y, zIndex);
        isDraw = false;
    }

    //　描画終了の制御
    public void StopDrawing()
    {
        isDraw = false;
    }
}