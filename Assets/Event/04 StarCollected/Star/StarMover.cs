using UnityEngine;
using System.Collections;

/// <summary>
/// 星が空から振る機能
/// </summary>
/// 
public class StarMover : MonoBehaviour {

    Vector3 velocity = Vector3.zero;
    bool isHit = false;
    bool isCollect = false;
    float rotate = 0;
    float rotateValue = 0;

    GameObject basket;

	// Use this for initialization
	void Start () {
        
        Vector3 randomPos = new Vector3(
            Random.Range(-0.5f, 0.5f), 
            Random.Range(0.0f, 5.0f), 
            Random.Range(-0.5f, 0.5f));

        var dis = Vector3.Distance(randomPos, transform.position);
        var randomVelocity = (transform.position - randomPos) / dis * Random.Range(2.0f, 4.0f);
        
        velocity = randomVelocity;
        rotateValue = Random.Range(60, 120);

        basket = GameObject.Find("Basket(Clone)");
	}
	
	// Update is called once per frame
	void Update () {
        BillBoard();
        Touch();
        Move();
        Death();
        Collected();
	}

    void OnCollisionEnter(Collision col)
    {
        if (!(col.gameObject.name == "Branch" || col.gameObject.name == "Stem")) return;
        isHit = true;
        rigidbody.isKinematic = true;
    }

    /// <summary>
    /// 移動
    /// </summary>
    void Translate()
    {
        transform.position += -velocity * Time.deltaTime;
    }

    /// <summary>
    /// 星が空から降ってくる
    /// </summary>
    void Move()
    {
        if (isHit) return;
        Translate();
        Rotate();
    }

    /// <summary>
    /// ビルボードの設定
    /// </summary>
    void BillBoard()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.back);

        transform.rotation *= Quaternion.Euler(0, 180, rotate);
    }

    /// <summary>
    /// 回転
    /// </summary>
    void Rotate()
    {
        rotate += rotateValue * Time.deltaTime;
    }

    /// <summary>
    /// タッチ
    /// </summary>
    void Touch()
    {
        if (!isHit) return;
        if (!(TouchManager.IsMouseButtonDown(gameObject) || TouchManager.IsTouching(gameObject))) return;
        isCollect = true;
        renderer.sortingLayerName = "Star";

    }

    /// <summary>
    /// 木から遠ざかると消える
    /// </summary>
    void Death()
    {
        if (transform.position.y < -8.0f || transform.position.x > 26 || transform.position.x < -26 || transform.position.z > 26 || transform.position.z < -26)
        {
            Destroy(gameObject);
        }
    }


    /// <summary>
    /// バスケットに移動する
    /// </summary>
    void Collected()
    {
        if (!isCollect) return;
        iTween.MoveUpdate(gameObject, iTween.Hash(
          "position", basket.transform.position,
          "time", 2.0f,
          "easetype", iTween.EaseType.easeInBack));
        if (transform.position.x > basket.transform.position.x - 0.1f && transform.position.x < basket.transform.position.x + 0.1f)
        {
            tag = "Untagged";
        }
    }
}
