/// ---------------------------------------------------
/// date ： 2015/02/06 
/// brief ： 果実を登場させる。
/// author ： Yamada Masamistu
/// ---------------------------------------------------

using UnityEngine;
using System.Collections;

public class FruitAppearance : MonoBehaviour {

    const float Velocity = 1;
    
    bool IsStop = false;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (IsStop) return;

        //transform.Translate(0, -Velocity * Time.deltaTime, 0);
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Branch")
        {
            IsStop = true;
        }
    }
}

