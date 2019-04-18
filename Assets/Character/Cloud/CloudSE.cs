using UnityEngine;
using System.Collections;

public class CloudSE : MonoBehaviour {

    CloudMover mover = null;

    [SerializeField]
    SoundEffectPlayer Player = null;

    [SerializeField]
    string RainResName = null;

	// Use this for initialization
	void Start () {
        Player = GameObject.FindObjectOfType(typeof(SoundEffectPlayer)) as SoundEffectPlayer;
        mover = GetComponent<CloudMover>();
	}
	
	// Update is called once per frame
	void Update () {
        if (mover.IsRain)
        {
            if (Player.IsPlaying(RainResName)) return;

            Player.Play(RainResName);
        }
	}
}
