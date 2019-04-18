using UnityEngine;
using System.Collections;

public class EffectScaling : MonoBehaviour {

    [SerializeField]
    private float MaxScale = 0.0f;

	// Use this for initialization
	void Start () {
        SetScaleByFloat(0.0f);
        iTween.ScaleTo(gameObject, iTween.Hash("x", MaxScale, "y", MaxScale, "z", MaxScale, "time", 3.0f, "easetype", iTween.EaseType.easeOutExpo));
        iTween.FadeTo(gameObject, iTween.Hash("alpha", 0, "time", 3.0f, "easetype", iTween.EaseType.easeOutExpo));
    }
	
	// Update is called once per frame
	void Update () {

    }
    /// <summary>
    /// スケールを引数１つの大きさにする
    /// </summary>
    /// <param name="scale">設定するスケール</param>
    private void SetScaleByFloat(float scale)
    {
        transform.localScale = new Vector3(scale, scale, scale); 
    }
}
