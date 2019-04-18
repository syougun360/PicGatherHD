/// ---------------------------------------------------
/// date ： 2015/01/20    
/// brief ： フィーバー時の雨のコントローラー
/// author ： Yamada Masamistu
/// ---------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RainFerverController : MonoBehaviour {

    CharacterManager Character = null;

	// Use this for initialization
	void Start () {
        if (!ModeManager.IsFerverMode) return;
        var RandomCharacter = Random.Range(0, 3);

        switch(RandomCharacter)
        {
            case 0:
                Character = FindObjectOfType(typeof(FairyManagerController)) as FairyManagerController;
                break;
            case 1:
                Character = FindObjectOfType(typeof(CloudManagerController)) as CloudManagerController;
                break;
            case 2:
                Character = FindObjectOfType(typeof(LeafStampManagerController)) as LeafStampManagerController;
                break;
        }

        renderer.material.mainTexture = TextureAlLoading.RandomLoadTexture(Character);

        var Scale = 0.8f;
        transform.localScale = new Vector3(Scale*1.5f, Scale, Scale);
    }
	
	// Update is called once per frame
	void Update () {
	    
	}
}
