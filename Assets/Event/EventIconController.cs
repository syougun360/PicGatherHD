using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EventIconController : MonoBehaviour {

    [SerializeField]
    float AlphaTime = 3.0f;

    Image Icon = null;
    RectTransform rectTrans = null;

	// Use this for initialization
	void Start () {

        rectTrans = transform as RectTransform;
        rectTrans.sizeDelta.Set(1,1);
        rectTrans.anchoredPosition3D = new Vector3(0, 0, 0);
        rectTrans.localScale = new Vector3(1, 1, 1);
        rectTrans.LookAt(Camera.main.transform.position);

        Icon = GetComponent<Image>();

        iTween.ValueTo(gameObject, iTween.Hash("from", 4, "to", 0, "time", AlphaTime, "onupdate", "UpdateHandler"));

	}
    void UpdateHandler(float value)
    {
        Icon.color = new Color(1,1,1,value);
    }

	// Update is called once per frame
	void Update () {
	}
}
