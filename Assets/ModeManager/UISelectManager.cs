using UnityEngine;
using System.Collections;

public class UISelectManager : MonoBehaviour {

    enum STATE
    {
        None,
        Share,
        Reset,
    };

    static STATE State = STATE.None;

    // Use this for initialization
    void Start()
    {
        State = STATE.None;
    }

    // Update is called once per frame
    void Update()
    {

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
    /// 今シェア機能なのかどうか
    /// </summary>
    /// <returns></returns>
    public static bool IsShareMode
    {
        get { return (State == STATE.Share); }
    }

    /// <summary>
    /// 何も選択してないかどうか
    /// </summary>
    /// <returns></returns>
    public static bool IsNoneMode
    {
        get { return (State == STATE.None); }
    }


    /// <summary>
    /// リセットに切り替える
    /// </summary>
    /// <returns></returns>
    public static void ChangeResetMode()
    {
        State = STATE.Reset;
    }

    /// <summary>
    /// シェアに切り替える
    /// </summary>
    /// <returns></returns>
    public static void ChangeShareMode()
    {
        State = STATE.Share;
    }

    /// <summary>
    /// 無選択に切り替える
    /// </summary>
    /// <returns></returns>
    public static void ChangeNoneMode()
    {
        State = STATE.None;
    }

}
