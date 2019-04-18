using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResetDirector : MonoBehaviour {

    [SerializeField]
    Transform centerObject = null;

    [SerializeField]
    float rotationSpeed = 2.0f;

    [SerializeField]
    float resetTime = 3.0f;

    [SerializeField]
    float fadeOutTime = 3.0f;

    [SerializeField]
    Texture2D blackTexture = null;

    float moveRadius = 0;
    float rotationAngle = 0;
    float count = 0;
    float alpha = 0.0f;
    bool isFadeOut = false;

	// Use this for initialization
	void Start () {
        moveRadius = Camera.main.GetComponent<CameraMover>().MoveRadius;
        Camera.main.GetComponent<CameraMover>().enabled = false;
	}


    void OnGUI()
    {
        GUI.color = new Color(0, 0, 0, alpha);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), blackTexture);
    }

	// Update is called once per frame
	void Update () {
        count += Time.deltaTime;
        rotationAngle += Mathf.PI * rotationSpeed * Time.deltaTime;
        transform.position = new Vector3(
            centerObject.position.x + Mathf.Sin(rotationAngle) * moveRadius,
            transform.position.y,
            centerObject.position.x + Mathf.Cos(rotationAngle) * moveRadius);

        transform.LookAt(new Vector3(0, transform.position.y - 2, 0));

        if (alpha >= 1)
        {
            ModeManager.ChangeGameMode();
            UISelectManager.ChangeNoneMode();
            Application.LoadLevel("GameMain");
        }

        Reset();
	}

    void Reset()
    {
        if (count < resetTime) return;
        if (isFadeOut) return;

        isFadeOut = true;
        iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", 1, "time", fadeOutTime, "onupdate", "UpdateHandler"));
    }

    void UpdateHandler(float value)
    {
        alpha = value;
    }
	
}
