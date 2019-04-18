using UnityEngine;
using System.Collections;

public class FairyGuideMover : MonoBehaviour {

    [SerializeField]
    float GoalTime = 3.0f;

    TitleStartter Startter = null;

	// Use this for initialization
	void Start () {

        Startter = GameObject.FindObjectOfType(typeof(TitleStartter)) as TitleStartter;

        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("FairyMovePath"), "time", GoalTime, "easetype", iTween.EaseType.easeOutSine));
	}
	
	// Update is called once per frame
	void Update () {

        var goalPos = iTweenPath.GetPath("FairyMovePath");
        if (goalPos[goalPos.Length - 1].y >= transform.position.y)
        {
            Startter.StartMove();
        }
	}
}
