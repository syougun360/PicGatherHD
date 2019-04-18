using UnityEngine;
using System.Collections;

public class SunRotater : MonoBehaviour {

    [SerializeField]
    float rotationValue = 0;

    float rotate = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        rotate += rotationValue * Time.deltaTime;
        transform.localRotation = Quaternion.AngleAxis(rotate, new Vector3(0, 0, 1));
	}
}
