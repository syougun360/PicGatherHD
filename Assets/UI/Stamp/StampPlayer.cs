using UnityEngine;
using System.Collections;

public class StampPlayer : MonoBehaviour {


    [SerializeField]
    string stampOpenResName = string.Empty;

    [SerializeField]
    string stampCloseResName = string.Empty;

    [SerializeField]
    SoundEffectPlayer Player = null;

    StampListMover mover = null;

    void Start()
    {
        mover = GameObject.Find("Stamp").GetComponent<StampListMover>();
    }


    public void Button()
    {
        if (mover.IsClosed)
        {
            Player.Play(stampCloseResName);
        }
        else
        {
            Player.Play(stampOpenResName);
        }
    }
}
