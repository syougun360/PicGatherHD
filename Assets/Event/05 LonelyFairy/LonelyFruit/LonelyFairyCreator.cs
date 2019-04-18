using UnityEngine;
using System.Collections;

public class LonelyFairyCreator : MonoBehaviour {


    /// <summary>
    /// 生成する”寂しがり屋の妖精”のプレハブ
    /// </summary>
    [SerializeField]
    private GameObject LonelyFairyPrefab = null;

    /// <summary>
    /// 木の実を落下させるかどうか
    /// </summary>
    public bool IsFallingFruit { get; private set; }


	// Use this for initialization
	void Start () {
        IsFallingFruit = false;
	}

    /// <summary>
    /// すでに生成した...true まだ生成していない...false
    /// </summary>
    private bool AlreadyInstantiated = false;

	// Update is called once per frame
	void Update () {
        if(AlreadyInstantiated) return;

        if (TouchManager.IsMouseButtonDown(gameObject) || 
            (TouchManager.IsTouching(this.gameObject) && TouchManager.IsPhaseTap))
        {

            CreateLonelyFairy();
            AlreadyInstantiated = true;
            IsFallingFruit = true;

            Camera.main.GetComponent<CameraMover>().enabled = false;
        }
	
	}

    private void CreateLonelyFairy()
    {
        var clone = (GameObject)Instantiate(LonelyFairyPrefab, this.transform.position, Quaternion.identity);
        clone.transform.parent = this.transform.parent;
    }
}
