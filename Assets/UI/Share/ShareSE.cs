using UnityEngine;
using System.Collections;

public class ShareSE : MonoBehaviour {

    [SerializeField]
    string shareSEResName = string.Empty;

    [SerializeField]
    SoundEffectPlayer Player = null;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SE()
    {
        if (UISelectManager.IsResetMode) return;
        if (Player.IsPlaying(shareSEResName)) return;

        Player.Play(shareSEResName);
    }
}
