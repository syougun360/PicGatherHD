using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FairyAppear : MonoBehaviour {

    FairyAnimator Anima = null;

    Vector3 ArrivalPos = Vector3.zero;
    
    public bool IsStop {get;private set;}

    const float DownSpeed = -1.0f;
    const float ArrivalTime = 6.0f;

	void Start () 
    {

        Anima = GetComponent<FairyAnimator>();
        SetArrivalPos();
        Anima.ChangeMoveAnima();

	}

    /// <summary>
    /// 目的地を設定する。
    /// </summary>
    void SetArrivalPos()
    {
        var tree = GameObject.Find("Tree");

        var scale = tree.transform.lossyScale;
        var value = Random.Range(0, 100);
        var randomY = Random.Range(0, Screen.height/2);
        var randomZ = Random.Range(-scale.z , scale.z );

        var appearancePos = value > 50 ?
            Camera.main.ScreenToWorldPoint(new Vector3(-Screen.width/3, randomY, randomZ)) :
            Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/3, randomY, randomZ));

        var treePos = tree.transform.position;
        transform.position = Camera.main.WorldToScreenPoint(appearancePos);
        transform.LookAt(treePos);

        ArrivalPos = treePos + new Vector3(0, Random.Range(scale.y / 10, scale.y / 6),0);

    }


	// Update is called once per frame
    void Update()
    {
        Move();
    }

    /// <summary>
    /// 移動
    /// </summary>
    void Move()
    {
        if (IsStop) return;

        iTween.MoveUpdate(gameObject, iTween.Hash("position", ArrivalPos,
                        "time", ArrivalTime, "easetype", iTween.EaseType.easeInOutExpo));

    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Tree")
        {
            IsStop = true;
            enabled = false;

        }
    }
}
