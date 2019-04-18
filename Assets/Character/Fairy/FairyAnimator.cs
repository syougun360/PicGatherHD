using UnityEngine;
using System.Collections;

public class FairyAnimator : MonoBehaviour {

    Animator Anima;

    enum STATE
    {
        Move,
        Eat,
    };

    STATE State = STATE.Move;

	// Use this for initialization
	void Start () {
        Anima = GetComponent<Animator>();
	}

    public void ChangeMoveAnima()
    {
        if (State == STATE.Move) return;

        State = STATE.Move;
        Anima.SetTrigger("OnMoveTrigger");
    }

    public void ChangeEatAnima()
    {
        if (State == STATE.Eat) return;

        State = STATE.Eat;
        Anima.SetTrigger("OnEatTrigger");
    }

	// Update is called once per frame
	void Update () {
	}
}
