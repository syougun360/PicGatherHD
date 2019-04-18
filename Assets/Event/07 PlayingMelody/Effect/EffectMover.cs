using UnityEngine;
using System.Collections;

public class EffectMover : MonoBehaviour {

    /// <summary>
    /// カメラからの距離
    /// </summary>
    [SerializeField]
    private float PositionZ = 0.0f;

    /// <summary>
    /// スクリーン座標
    /// </summary>
    private Vector3 PositionInScreen = Vector3.zero;

	// Use this for initialization
	void Start () {
        PositionInScreen = Camera.main.WorldToScreenPoint(transform.parent.transform.parent.transform.position);
        PositionInScreen.z = PositionZ;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Camera.main.ScreenToWorldPoint(PositionInScreen);
	
	}
}
