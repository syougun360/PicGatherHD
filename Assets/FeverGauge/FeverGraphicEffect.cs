using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FeverGraphicEffect : MonoBehaviour {

    [SerializeField]
    GameObject Prefab = null;

    [SerializeField]
    List<Texture> NoteSprite = new List<Texture>();

    [SerializeField]
    float CreateTime = 0.5f;

    float Count = 0;
	// Use this for initialization
	void Start () {
        Count = CreateTime;
	}
	
	// Update is called once per frame
	void Update () {
        if (!ModeManager.IsFerverMode) return;

        Count += Time.deltaTime;
        if (Count >= CreateTime)
        {
            var clone = (GameObject)Instantiate(Prefab, transform.position, transform.rotation);
            var index = Random.Range(0, NoteSprite.Count);
            
            clone.transform.parent = transform;
            clone.renderer.material.mainTexture = NoteSprite[index];

            Count = 0;
        }

	}
}
