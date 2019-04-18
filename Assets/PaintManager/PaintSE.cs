using UnityEngine;
using System.Collections;

public class PaintSE : MonoBehaviour {

    [SerializeField]
    string changeToPaintModeResName = string.Empty;

    [SerializeField]
    string changeToGameModeResName = string.Empty;

    [SerializeField]
    string saveSEResName = string.Empty;

    [SerializeField]
    string changeColorPenResName = string.Empty;

    [SerializeField]
    SoundEffectPlayer Player = null;

    CampusTemplateSetting CampusTemplate = null;

	// Use this for initialization
	void Start () {
        CampusTemplate = GameObject.Find("Campus").GetComponent<CampusTemplateSetting>();
	}
	
	// Update is called once per frame
    void Update()
    {
    }

    public void ChangeToPaintMode()
    {
        Player.Play(changeToPaintModeResName);
    }

    public void ChangeToGameMode()
    {
        Player.Play(changeToGameModeResName);
    }

    public void Save()
    {
        if (!CampusTemplate.IsSelect) return;
        Player.Play(saveSEResName);
    }

    public void ChangeColorPen()
    {
        Player.Play(changeColorPenResName);
    }
}
