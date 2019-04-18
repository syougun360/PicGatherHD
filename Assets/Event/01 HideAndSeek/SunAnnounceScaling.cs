using UnityEngine;
using System.Collections;

public class SunAnnounceScaling : MonoBehaviour {

    [SerializeField]
    private float LoopTime = 1.0f;

    [SerializeField]
    private float MaxScale = 5.0f;

    private float MinScale = 0.0f;
	// Use this for initialization
	void Start () {
        transform.localScale = new Vector3(MaxScale,MaxScale,MaxScale);
        iTween.ScaleTo(gameObject, iTween.Hash("x", MinScale, "y", MinScale, "z", MinScale, "time", LoopTime, "easetype", iTween.EaseType.easeOutQuart, "looptype", iTween.LoopType.loop));
        iTween.FadeTo(gameObject, iTween.Hash("alpha", 0, "time", LoopTime, "easetype", iTween.EaseType.easeOutQuart, "looptype", iTween.LoopType.loop));
	}
	
}
