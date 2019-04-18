using UnityEngine;
using System.Collections;

public class MoonRotation : MonoBehaviour {

    [SerializeField]
    float rotationValue = 0.1f;

    [SerializeField]
    float Angle = 0;

    float rotate = 0;
    float swingValue = 0.5f;

    RectTransform RectTrans = null;

	// Use this for initialization
	void Start () {
        RectTrans = transform as RectTransform;
    }

    float Speed = 0;
	// Update is called once per frame
	void Update () {

        rotate += rotationValue * Time.deltaTime;
        Speed += swingValue * Mathf.Sin(rotate);
        RectTrans.localEulerAngles = new Vector3(0, 0, Speed + Angle);
	}

}
