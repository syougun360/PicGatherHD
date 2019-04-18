using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CampusSelectSoundPlayer : MonoBehaviour {

    [SerializeField]
    SoundEffectPlayer SEPlayer = null;

    [SerializeField]
    string SelectSoundResName = string.Empty;

    Button SelectButton = null;

	// Use this for initialization
	void Start () {
        SelectButton = GetComponent<Button>();
        SelectButton.onClick.AddListener(SoundPlay);
	}

    void SoundPlay()
    {
        SEPlayer.Play(SelectSoundResName);
    }
}
