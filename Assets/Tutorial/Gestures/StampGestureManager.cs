using UnityEngine;
using System.Collections;

public class StampGestureManager : MonoBehaviour {


    /// <summary>
    /// 葉っぱの数を覚える
    /// </summary>
    public int LeafNumber {get;private set;}

    /// <summary>
    /// スタンプを押す状態かどうか
    /// </summary>
    public bool EnableImage {get;private set;}

    void Start()
    {
        LeafNumber = -1;
        EnableImage = false;
    }

    /// <summary>
    /// 葉の数を記憶する
    /// </summary>
    public void SetLeafNumber()
    {
        var StampGestureMngr = FindObjectOfType<StampGestureManager>();
        
        if (StampGestureMngr == null) return;

        StampGestureMngr.LeafNumber = GameObject.FindGameObjectsWithTag("Leaf").Length;
    }

    /// <summary>
    /// スタンプモードかどうかを設定する
    /// </summary>
    /// <param name="isStampMode">スタンプを押す状態...true 押す状態じゃない...false</param>
    public void ChangeStampState(bool isStampMode)
    {
        var StampGestureMngr = FindObjectOfType<StampGestureManager>();

        if (StampGestureMngr == null) return;

        StampGestureMngr.EnableImage = isStampMode;
    }
}
