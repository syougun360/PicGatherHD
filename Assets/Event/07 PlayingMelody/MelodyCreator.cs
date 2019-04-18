using UnityEngine;
using System.Collections;

public class MelodyCreator : MonoBehaviour {

    /// <summary>
    /// イベントで使用するボタンがあるキャンバス
    /// </summary>
    [SerializeField]
    private GameObject MelodyCanvas = null;


	// Use this for initialization
	void Start () {
        var clone = (GameObject)Instantiate(MelodyCanvas);
        clone.transform.SetParent(transform);
	}
	
}
