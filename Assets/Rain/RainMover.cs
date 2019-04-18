/// ---------------------------------------------------
/// date ： 2015/01/17   
/// brief ： 雨を下に移動する
/// author ： Yamada Masamistu
/// ---------------------------------------------------

using UnityEngine;
using System.Collections;

public class RainMover : MonoBehaviour {

    float Force = -4.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(0, Force*Time.deltaTime, 0));
	}
}
