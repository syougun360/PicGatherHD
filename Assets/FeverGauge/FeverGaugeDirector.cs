using UnityEngine;
using System.Collections;

public class FeverGaugeDirector : MonoBehaviour {

    FeverManager Manager = null;

	// Use this for initialization
	void Start () {
        Manager = GetComponent<FeverManager>();
	}

	// Update is called once per frame
	void Update () {

        ///常にカメラの方向を向くようにする。
        transform.LookAt(Camera.main.transform.position);

        ///FeverScoreの割合分だけ回転させる。
        transform.Rotate(-180 * (Manager.FeverScore * 1.0f / Manager.MaxFeverScore), 0, 0);

	}
}
