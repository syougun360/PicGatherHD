using UnityEngine;
using System.Collections;

public class MoonTouchEffecter : MonoBehaviour {

    [SerializeField]
    GameObject EffecterPrefab = null;

    [SerializeField]
    int CreateNum = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (TouchManager.IsMouseButtonDown(gameObject) || TouchManager.IsTouching(gameObject))
        {
            CreateEffect();
        }
	}

    /// <summary>
    /// エフェクトを生成
    /// </summary>
    void CreateEffect()
    {
        for (int i = 0; i < CreateNum; i++)
        {
            var clone = (GameObject)Instantiate(EffecterPrefab, TouchManager.TapPos, Quaternion.identity);
            clone.transform.parent = transform;
        }
    }
}
