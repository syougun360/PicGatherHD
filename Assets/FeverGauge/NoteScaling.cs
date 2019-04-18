/// ---------------------------------------------------
/// date ： 2015/02/17  
/// brief ： 音符のScale処理
/// author ： Yamada Masamistu
/// ---------------------------------------------------
/// 
using UnityEngine;
using System.Collections;

public class NoteScaling : MonoBehaviour {

    [SerializeField]
    float MaxTime = 3.0f;

    [SerializeField]
    float MaxScale = 3.0f;

    // Use this for initialization
    void Start()
    {
        var maxScale = Random.Range(MaxScale/2, MaxScale);
        iTween.ScaleTo(gameObject, iTween.Hash("scale", new Vector3(maxScale, maxScale, maxScale),
                        "time", MaxTime, "easetype", iTween.EaseType.easeInExpo));
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
