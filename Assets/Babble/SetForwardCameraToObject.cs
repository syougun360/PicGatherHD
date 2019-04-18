using UnityEngine;
using System.Collections;

public class SetForwardCameraToObject : MonoBehaviour
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.forward = (this.transform.position - Camera.main.transform.position);
	}
}
