using UnityEngine;
using System.Collections;

public class SceneChangedController : MonoBehaviour {

    [SerializeField]
    float FadeInTime = 2.0f;

    [SerializeField]
    Texture2D BlackTexture = null;

    float Alpha = 1.0f;

    bool IsFadeIn = true;

    // Use this for initialization
    void Awake()
    {
    }

    void Start()
    {
        iTween.ValueTo(gameObject, iTween.Hash("from", 1, "to", 0, "time", FadeInTime, "onupdate", "UpdateHandler"));
    }

    void OnGUI()
    {
        if (!IsFadeIn) return;

        GUI.color = new Color(0, 0, 0, Alpha);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), BlackTexture);
    }

    void UpdateHandler(float value)
    {
        Alpha = value;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsFadeIn) return;

        if (Alpha <= 0)
        {
            IsFadeIn = false;
        }
    }
}
