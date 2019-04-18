/// ---------------------------------------------------
/// date ： 2015/01/08    
/// brief ： エフェクトを生成する
/// author ： Yamada Masamistu
/// ---------------------------------------------------

using UnityEngine;
using System.Collections;

public class EffectController : MonoBehaviour {

    [SerializeField]
    private GameObject GraphicEffectPrefab = null;

    [SerializeField]
    private GameObject SoundEffectPrefab = null;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () 
    {
        if (TouchManager.IsTouching(gameObject) || TouchManager.IsMouseButtonDown(gameObject))
        {
            Instantiate(GraphicEffectPrefab, gameObject.transform.position, Quaternion.identity);
            Instantiate(SoundEffectPrefab, gameObject.transform.position, Quaternion.identity);
        }

	}
}
