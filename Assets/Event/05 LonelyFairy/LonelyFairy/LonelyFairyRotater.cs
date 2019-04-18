using UnityEngine;
using System.Collections;

public class LonelyFairyRotater : MonoBehaviour {

    /// <summary>
    /// 振り向くのにかかる時間
    /// </summary>
    const float ArrivalTime = 1.0f;

	// Use this for initialization
	void Start () {
        transform.forward = Camera.main.transform.forward;

	}
	
	// Update is called once per frame
	void Update () {
        iTween.LookTo(gameObject, Camera.main.transform.position - new Vector3(0,0.5f,0), ArrivalTime);
	}
}
