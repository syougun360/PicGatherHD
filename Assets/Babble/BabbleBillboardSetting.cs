using UnityEngine;
using System.Collections;

public class BabbleBillboardSetting : MonoBehaviour {

    /// <summary>
    /// SphereのY軸回転するオイラー角
    /// </summary>
    private const float RotateAngle = 100.0f;
    
    // Use this for initialization
	void Start () {
	}


	// Update is called once per frame
	void Update () {

        RotateByCameraForword();

    }

    /// <summary>
    /// カメラの向いている方向を元にY軸を回転させる。
    /// </summary>
    private void RotateByCameraForword()
    {
        transform.forward = Camera.main.transform.forward;

        transform.rotation *= Quaternion.Euler(new Vector3(0, RotateAngle, 0));
    }
}
