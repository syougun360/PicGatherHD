using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class OpenOurEyesMover : MonoBehaviour {

    [SerializeField]
    GameObject GraphicEffectPrefab = null;

    [SerializeField]
    List<Texture>textures = new List<Texture>();

    [SerializeField]
    GameObject FoundSE = null;

    public enum State
    {
        APPEARANCE,
        HIDE,
        FOUND,
    };

    public State state { get; private set; }
    float RotationAngle = 0;
    float goalPosY;

	// Use this for initialization
	void Start () {
        state = State.APPEARANCE;
        renderer.material.mainTexture = textures[0];
        renderer.sortingLayerName = "OpenOurEyes";
        var worldPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2 - Screen.height * 0.2f, 2 - Camera.main.GetComponent<CameraMover>().MoveRadius));
        goalPosY = worldPos.y;

	}
	
	// Update is called once per frame
	void Update () {

        Appearance();
        Hide();
	}

    void Appearance()
    {

        if (state != State.APPEARANCE) return;
        iTween.MoveTo(gameObject,
            iTween.Hash("position", Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2 - Screen.height * 0.2f, 2 - Camera.main.GetComponent<CameraMover>().MoveRadius)),
            "time", 3.0f,
            "easetype", iTween.EaseType.easeOutQuad));


        AppearanceToHide();

    }

    void Hide()
    {
        if (state != State.HIDE) return;
        HideMove();
        OnMouseCollision();
    }

    void AppearanceToHide()
    {
        if (goalPosY + 0.01f < transform.position.y) return;
        state = State.HIDE;
        renderer.material.mainTexture = textures[1];
        RotationAngle = Mathf.Atan2(transform.position.x, transform.position.z);

    }

    void OnMouseCollision()
    {
        if (!(TouchManager.IsMouseButtonDown(gameObject)|| TouchManager.IsTouching(gameObject))) return;
        
        state = State.FOUND;
        
        var effect = (GameObject)Instantiate(GraphicEffectPrefab, gameObject.transform.position, Quaternion.identity);
        const float ScaleTime = 1.0f;
        iTween.ScaleTo(effect, new Vector3(3, 3, 3), ScaleTime);
        
        Instantiate(FoundSE);
        Destroy(gameObject);
        GameObject.Find("SunCharacter").GetComponent<Image>().enabled = true;
    }

    void HideMove()
    {
        var pos = Camera.main.WorldToScreenPoint(transform.localPosition);
        if (Screen.width / 2 - 0.01f > pos.x)
        {
            RotationAngle += 0.01f;

        }
        else if (Screen.width / 2 + 0.01f < pos.x)
        {
            RotationAngle += -0.01f;
        }
        transform.localPosition = new Vector3(
            Mathf.Sin(RotationAngle) * 2,
            transform.localPosition.y,
            Mathf.Cos(RotationAngle) * 2);
    }
}