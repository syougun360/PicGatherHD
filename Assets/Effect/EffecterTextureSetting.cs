using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffecterTextureSetting : MonoBehaviour {

    [SerializeField]
    List<Texture> TextureList = new List<Texture>();

	// Use this for initialization
	void Start () {
        var index = Random.Range(0, TextureList.Count);
        renderer.material.mainTexture = TextureList[index];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
