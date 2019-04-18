using UnityEngine;
using System.Collections;

public class IconPositioning : MonoBehaviour {

    /// <summary>
    /// 親の情報
    /// </summary>
    private RectTransform ParentPosition = null;


    /// <summary>
    /// スクリーン座標
    /// </summary>
    private Vector3 PositionInScreen = Vector3.zero;

	// Use this for initialization
	void Start () {
        PositionInScreen = Camera.main.WorldToScreenPoint(transform.position);
        ParentPosition = transform.parent.transform as RectTransform;
	}
    // Update is called once per frame
	void Update () {
        Debug.Log(ParentPosition);

        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(PositionInScreen.x, PositionInScreen.y, 0.93f));

      
    }
}
