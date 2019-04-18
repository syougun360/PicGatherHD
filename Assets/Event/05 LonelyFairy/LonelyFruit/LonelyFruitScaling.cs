using UnityEngine;
using System.Collections;

public class LonelyFruitScaling : MonoBehaviour {


    const float GainTime = 3.0f;

    [SerializeField]
    float MaxScale = 30.0f;

    // Use this for initialization
    void Start()
    {
        iTween.ScaleTo(gameObject, iTween.Hash("scale", new Vector3(MaxScale, MaxScale, MaxScale),
                        "time", GainTime, "easetype", iTween.EaseType.easeInExpo));
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
