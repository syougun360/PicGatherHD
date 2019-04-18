using UnityEngine;
using System.Collections;

/// <summary>
/// 光っている星の動き
/// </summary>
/// 
public class StarRotater : MonoBehaviour {

    float rotate = 0;
    float rotateValue = 0.02f;
    Light light = new Light();
    float intensityValue = 3;

	// Use this for initialization
	void Start () {
        light = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(25 * Mathf.Cos(rotate), 15.4f, 25 * Mathf.Sin(rotate));
        rotate += rotateValue * Time.deltaTime;
        Bright();
	}

    /// <summary>
    /// 眩しさの挙動
    /// </summary>
    void Bright()
    {
        light.intensity += intensityValue * Time.deltaTime;
        if (light.intensity == 8 || light.intensity == 0)
        {
            intensityValue *= -1;
        }
    }
}
