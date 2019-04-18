using UnityEngine;
using System.Collections;

public class IconScaling : MonoBehaviour
{

    /// <summary>
    /// 最大scale
    /// </summary>
    [SerializeField]
    private float MaxScale = 0.0016f;

//    private const float MinScale = 0.0014f;

    /// <summary>
    /// 何秒かけて変化するか
    /// </summary>
    private const float ChangingTime = 0.5f;

	// Use this for initialization
	void Start () {
        var MinScale = MaxScale * 0.8f;
        transform.localScale = new Vector3(MinScale, MinScale, MinScale);


        iTween.ScaleTo(gameObject, iTween.Hash("x", MaxScale, "y", MaxScale, "z", MaxScale, "time", ChangingTime, "easetype", iTween.EaseType.easeOutExpo, "looptype", iTween.LoopType.pingPong));
        iTween.FadeTo(gameObject, iTween.Hash("alpha", 0, "time", ChangingTime, "easetype", iTween.EaseType.easeInExpo, "looptype", iTween.LoopType.pingPong));
	}

}
