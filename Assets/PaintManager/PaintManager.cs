using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//
//　一筆生成機能
//
//　HACK:フラグを用意してペイント機能を制御している->マウスイベントだけで制御する
//
public class PaintManager : MonoBehaviour {

    [SerializeField]
    GameObject prefab = null;

    [SerializeField]
    GameObject parent = null;

    [SerializeField]
    GameObject drawingCampus = null;

    [SerializeField]
    GameObject campus = null;

    [SerializeField]
    SelectColorPenController SelectPen = null;

    GameObject characterCanvas = null;
    bool CanDrawing = false;

    public Vector2 campusSize;

    //　ペイントに必要な制御
    public Color lineColor { get; private set; }
    public int lineCount { get; private set; }
    public float lineWidth { get; private set; }
    public readonly Vector2 valueAdjustment = new Vector2(3, 8);

#if UNITY_METRO_8_1 && !UNITY_EDITOR
    public readonly Vector2 campusOffSet = new Vector2(180, 120);
#else    
    public readonly Vector2 campusOffSet = new Vector2(140, 100);
#endif

    bool isOver = false;

    void Start()
    {
        lineColor = Color.black;
        lineCount = 1;
        lineWidth = 0.03f;
        campusSize = campus.GetComponent<RectTransform>().rect.size;

#if UNITY_METRO_8_1 && !UNITY_EDITOR
        campusSize.x -= 1000;
        campusSize.y -= 520;
#else
        campusSize.x -= 250;
        campusSize.y -= 140;
#endif
    }

    void Update()
    {
        if (!ModeManager.IsDrawingMode) return;
        if (!CanDrawing) return;

        if (Input.touchCount == 0)
        {
            MouseEvent();
        }
        else
        {
            TouchEvent();
        }

    }

    public void CanDrawingEnable()
    {
        CanDrawing = true;
    }

    /// <summary>
    /// マウスイベント
    /// </summary>
    void MouseEvent()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CanDrawLine(Input.mousePosition);
        }

        if (Input.GetMouseButton(0) && isOver)
        {
            CanDrawLine(Input.mousePosition);
        }
    }

    /// <summary>
    /// タッチのイベント
    /// </summary>
    void TouchEvent()
    {
        foreach (var touch in Input.touches)
        { 
            if (touch.phase == TouchPhase.Began)
            {
                CanDrawLine(touch.position);
            }
            if (touch.phase == TouchPhase.Moved && isOver)
            {                  
                CanDrawLine(touch.position);
            }
        }
    }
    
    //　キャンパス内だったら描画する
    void CanDrawLine(Vector3 pos)
    {
        if (!(pos.x > 0 + campusOffSet.x && campusSize.x - valueAdjustment.x > pos.x)) return;
        if (!(pos.y > 0 + campusOffSet.y && campusSize.y - valueAdjustment.y > pos.y)) return;

        CreateLine();
    }

    void CreateLine()
    {
        StopLine();

        var originPos = Vector3.zero;

        if (!characterCanvas)
        {
            characterCanvas = (GameObject)Instantiate(parent, originPos, Quaternion.identity);
            characterCanvas.transform.parent = drawingCampus.transform;
            characterCanvas.name = parent.name;
        }

        var clone = (GameObject)Instantiate(prefab, originPos, Quaternion.identity);

        clone.name = prefab.name;
        clone.transform.parent = characterCanvas.transform;

    }


    void StopLine()
    {
        lineCount++;
        isOver = false;
    }

    /// <summary>
    /// lineの初期化
    /// </summary>
    public void InitLine()
    {
        StartCoroutine("WaitInitLine");
    }

    /// <summary>
    /// lineを初期化するWait関数
    /// 1秒待って下の処理が実行させる
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitInitLine()
    {
        yield return new WaitForSeconds(1.0f);

        lineCount = 1;
        isOver = false;
        lineColor = Color.black;
        SelectPen.Init();
        CanDrawing = false;
    }

    public void ChangeColor(Material material)
    {
        lineColor = material.color;
        lineWidth = 0.03f;
    }

    public void ChangeLineWidth(float width) { lineWidth = width; }

    /// <summary>
    /// マウスの状態をチェック
    /// </summary>
    /// <param name="children"></param>
    public void CheckMouse(Painter children)
    {
        if (Input.GetMouseButtonUp(0))
        {
            children.StopDrawing();
            StopLine();
        }

        if (Input.GetMouseButton(0))
        {
            InputCampusOver(children);
        }
    }

    /// <summary>
    /// タッチの状態をチェック
    /// </summary>
    /// <param name="children"></param>
    public void CheckTouch(Painter children)
    {
        if (children.touch.phase == TouchPhase.Ended)
        {
            children.StopDrawing();
            StopLine();
        }
        if (children.touch.phase == TouchPhase.Moved)
        {
            InputCampusOver(children);
        }
    }

    /// <summary>
    /// 入力情報がキャンパスの範囲外の処理
    /// </summary>
    /// <param name="children"></param>
    void InputCampusOver(Painter children)
    {
        if (!children.isDraw)
        {
            isOver = true;
        }
    }

}
