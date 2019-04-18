using UnityEngine;
using System.Collections;

public class CurtainCreateTexture : MonoBehaviour {

    CharacterManager Character = null;

	// Use this for initialization
	void Start () {
        var RandomCharacter = Random.Range(0, 3);

        switch (RandomCharacter)
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

        var Scale = 0.25f;

        transform.localScale = new Vector3(Scale, Scale, Scale);
    }
}


