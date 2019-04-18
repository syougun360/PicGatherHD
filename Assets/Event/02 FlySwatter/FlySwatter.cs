using UnityEngine;
using System.Collections;

public class FlySwatter : EventBase
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var GameIsOver = false;
        if (GameIsOver)
        {
            Finish();
        }
    }

    /// <summary>
    /// 終了時の処理
    /// </summary>
    protected override void Finish()
    {
        base.Finish();


        Debug.Log("Game is Over");

    }
}
