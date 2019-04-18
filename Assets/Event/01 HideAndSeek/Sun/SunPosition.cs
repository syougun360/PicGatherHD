using UnityEngine;
using System.Collections;

public class SunPosition : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        var pos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - Screen.width * 0.2f, Screen.height - Screen.height * 0.2f, 15));
        transform.localPosition = pos;

	}
}
