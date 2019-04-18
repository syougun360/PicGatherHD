using UnityEngine;
using System.Collections;

public class FoundSE : MonoBehaviour {

    SoundEffectPlayer player = null;

    [SerializeField]
    string resName = string.Empty;

	// Use this for initialization
	void Start () {
        player = GameObject.FindObjectOfType(typeof(SoundEffectPlayer)) as SoundEffectPlayer;

        player.Play(resName);
	}
	
	// Update is called once per frame
	void Update () {

        if (player.IsPlaying(resName)) return;

        Destroy(gameObject);
        Destroy(GameObject.Find("Explosion(Clone)"));
	}
}
