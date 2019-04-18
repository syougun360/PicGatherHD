using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// バスケットの移動
/// </summary>
/// 
public class BasketMover : MonoBehaviour {

    Vector3[] path = new Vector3[2];

	// Use this for initialization
	void Start () {

        path[0] = new Vector3(1.0f, 1.0f, transform.position.z);
        path[1] = new Vector3(0.0f, -1.0f, transform.position.z);

        iTween.MoveTo(gameObject, iTween.Hash(
        "time", 8.0f,
        "x", 2.0f,
        "easetype", iTween.EaseType.easeOutCubic));
	}
	
	// Update is called once per frame
    void Update()
    {
        if (transform.position.x != 2.0f) return;

        iTween.MoveTo(gameObject, iTween.Hash(
            "time", 1.0f,
            "path", path,
            "easetype", iTween.EaseType.easeOutCubic));
    }
}
