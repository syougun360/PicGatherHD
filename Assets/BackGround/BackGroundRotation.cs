using UnityEngine;
using System.Collections;

public class BackGroundRotation : MonoBehaviour {

    [SerializeField]
    float RotationSpeed = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.down * RotationSpeed * Time.deltaTime);
	}
}
