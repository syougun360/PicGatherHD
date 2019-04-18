using UnityEngine;
using System.Collections;

public class ModeManager : MonoBehaviour {

    enum STATE
    {
        Game,
        Drawing,
        Ferver,
        Event,
        Reset,
    };

    static STATE State = STATE.Game;

	// Use this for initialization
	void Start () 
    {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// 今ゲームモードなのかどうか
    /// </summary>
    /// <returns></returns>
    public static bool IsGameMode
    {
        get { return (State == STATE.Game); }
    }

    /// <summary>
    /// 今お絵かきモードなのかどうか
    /// </summary>
    /// <returns></returns>
    public static bool IsDrawingMode
    {
        get { return (State == STATE.Drawing); }
    }

    /// <summary>
    /// 今リセットなのかどうか
    /// </summary>
    /// <returns></returns>
    public static bool IsResetMode
    {
        get { return (State == STATE.Reset); }
    }


    /// <summary>
    /// 今フィーバーモードなのかどうか
    /// </summary>
    /// <returns></returns>
    public static bool IsFerverMode
    {
        get { return (State == STATE.Ferver); }
    }

    /// <summary>
    /// 今イベントなのかどうか
    /// </summary>
    /// <returns></returns>
    public static bool IsEventMode
    {
        get { return (State == STATE.Event); }
    }


    /// <summary>
    /// お絵かきモードに切り替える
    /// </summary>
    /// <returns></returns>
    public static void ChangeDrawingMode()
    {
        State = STATE.Drawing;
    }
    
    /// <summary>
    /// ゲームモードに切り替える
    /// </summary>
    /// <returns></returns>
    public static void ChangeGameMode()
    {
        State = STATE.Game;
    }

    /// <summary>
    /// フィーバーモードに切り替える
    /// </summary>
    /// <returns></returns>
    public static void ChangeFerverMode()
    {
        State = STATE.Ferver;
    }

    /// <summary>
    /// イベントに切り替える
    /// </summary>
    /// <returns></returns>
    public static void ChangeEventMode()
    {
        State = STATE.Event;
    }

    /// <summary>
    /// リセットに切り替える
    /// </summary>
    /// <returns></returns>
    public static void ChangeResetMode()
    {
        State = STATE.Reset;
    }

}
