using UnityEngine;
using System.Collections;

public class LonelyFairyScaling : MonoBehaviour {

    /// <summary>
    /// 最大Scale
    /// </summary>
    private const float MaxScale = 2;

    	// Use this for initialization
	void Start () {
        transform.localScale = Vector3.one;
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.localScale.x > MaxScale) return;

        var IncreaseValue = 0.05f;

        transform.localScale += new Vector3(IncreaseValue * (MaxScale - transform.localScale.x),
                                            IncreaseValue * (MaxScale - transform.localScale.y),
                                            IncreaseValue * (MaxScale - transform.localScale.z));
	
	}
}
