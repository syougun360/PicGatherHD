using UnityEngine;
using System.Collections;

public class TreeGrowthEffectDestoryer : MonoBehaviour {

    [SerializeField]
    float DesTime = 10.0f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, DesTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
