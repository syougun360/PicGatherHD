using UnityEngine;
using System.Collections;

/// <summary>
/// バスケットを運ぶ妖精の移動
/// </summary>
/// 
public class BasketCourier : MonoBehaviour {

    Vector3[] path = new Vector3[2];

    enum State
    {
        START,
        END
    };

    float startPosY;
    State state;
    float count = 0;

	// Use this for initialization
	void Start () {

        state = State.START;

        path[0] = new Vector3(2.0f, -2.5f, transform.position.z);
        path[1] = new Vector3(2.0f, transform.position.y, transform.position.z);
        
        startPosY = transform.position.y;

        iTween.MoveTo(gameObject, iTween.Hash(
            "time",8.0f,
            "x",2.0f,
            "easetype",iTween.EaseType.easeOutCubic));
	}
	
	// Update is called once per frame
	void Update () {
        StartMove();
        EndMove();
	}

    /// <summary>
    /// 木の近づきかごを置くの処理
    /// </summary>
    void StartMove()
    {
        if (state != State.START) return;
        if (transform.position.x == 2.0f)
        {

            iTween.MoveTo(gameObject, iTween.Hash(
               "time", 1.0f,
               "path", path,
               "easetype", iTween.EaseType.easeOutCubic));
            count += Time.deltaTime;
            if (count > Time.deltaTime * 2)
            {
                state = State.END;
            }

        }

    }


    /// <summary>
    /// 木に遠ざかる処理
    /// </summary>
    void EndMove()
    {
        if (state != State.END) return;

        if (transform.position.y > startPosY - 0.01f && transform.position.y < startPosY + 0.01f)
        {
            iTween.MoveTo(gameObject, iTween.Hash(
              "time", 8.0f,
              "x", -20.0f,
              "easetype", iTween.EaseType.easeOutCubic));
        }
        if (transform.position.x < -18.0f)
        {
            Destroy(gameObject);
        }
    }
}
