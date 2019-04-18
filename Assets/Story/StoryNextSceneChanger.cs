using UnityEngine;
using System.Collections;

public class StoryNextSceneChanger : MonoBehaviour {

    [SerializeField]
    StoryBoardAnimator StoryAnim = null;

    [SerializeField]
    float FadeOutTime = 2.0f;

    [SerializeField]
    Texture2D BlackTexture = null;

    StorySoundPlayer Player = null;
    float Alpha = 0.0f;
    bool IsFadeOut = false;

	// Use this for initialization
	void Start () {
        Player = GetComponent<StorySoundPlayer>();
	}

    void OnGUI()
    {
        GUI.color = new Color(0, 0, 0, Alpha);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), BlackTexture);
    }

    void UpdateHandler(float value)
    {
        Alpha = value;
    }
	
	// Update is called once per frame
	void Update () {
        StartFadeOut();

        if (Alpha >= 1)
        {
            Application.LoadLevel("GameMain");
        }
	}

    /// <summary>
    /// フェードアウトをスタートする
    /// </summary>
    void StartFadeOut()
    {
        if (!StoryAnim.IsFinish) return;
        if (IsFadeOut) return;

        IsFadeOut = true;
        Player.Stop();
        iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", 1, "time", FadeOutTime, "onupdate", "UpdateHandler"));

    }
}
